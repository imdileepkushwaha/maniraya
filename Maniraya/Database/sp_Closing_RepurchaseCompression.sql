-- Repurchase closing with sponsor-wise 10-level compression.
-- Self cashback 5%. Level % from RepurchaseLevelMaster.ROILEVELCOMMISSION (Direct = 0).
-- Qualifier: SelfBv entry in UserBuisnessVolumeRepurchase inside the closing dates.
-- If an upline has no purchase in this period, that slot compresses to the next qualifying upline.
-- TransactionDetail credit is kept commented until wallet posting is enabled.
-- First run dates: 03 Aug 2026 to 30 Aug 2026.
--
-- EXEC dbo.Closing_RepurchaseCompression @FromDate = '2026-08-03', @ToDate = '2026-08-30';

IF OBJECT_ID('dbo.RepurchaseCompressionMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RepurchaseCompressionMaster
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FromDate DATETIME NOT NULL,
        ToDate DATETIME NOT NULL,
        CreateDate DATETIME NOT NULL CONSTRAINT DF_RepurchaseCompressionMaster_CreateDate DEFAULT (GETDATE()),
        ClosingNo INT NOT NULL,
        CashbackCount INT NULL,
        CashbackAmount DECIMAL(18,2) NULL,
        LevelCount INT NULL,
        LevelAmount DECIMAL(18,2) NULL,
        Status INT NOT NULL CONSTRAINT DF_RepurchaseCompressionMaster_Status DEFAULT (0)
    );
END
GO

IF OBJECT_ID('dbo.Closing_RepurchaseCompression', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Closing_RepurchaseCompression;
GO

CREATE PROCEDURE dbo.Closing_RepurchaseCompression
    @FromDate DATETIME = '2026-08-03',
    @ToDate DATETIME = '2026-08-30'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @From DATETIME = CONVERT(date, @FromDate);
    DECLARE @To DATETIME = CONVERT(date, @ToDate);
    DECLARE @MaxLevel INT = 10;
    DECLARE @CashbackPer DECIMAL(18,2) = 5;
    DECLARE @AdminPer DECIMAL(18,2) = 0;
    DECLARE @TdsPer DECIMAL(18,2) = 0;
    DECLARE @ClosingNo INT;
    DECLARE @CashbackCount INT = 0;
    DECLARE @CashbackAmount DECIMAL(18,2) = 0;
    DECLARE @LevelCount INT = 0;
    DECLARE @LevelAmount DECIMAL(18,2) = 0;

    IF (@From IS NULL OR @To IS NULL OR @From > @To)
    BEGIN
        RAISERROR('Invalid from / to date.', 16, 1);
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM dbo.RepurchaseCompressionMaster WITH (NOLOCK)
        WHERE CONVERT(date, FromDate) = @From
          AND CONVERT(date, ToDate) = @To
    )
    BEGIN
        RAISERROR('Closing Already generated', 16, 1);
        RETURN;
    END

    IF OBJECT_ID('dbo.tbl_Deduction', 'U') IS NOT NULL
    BEGIN
        SELECT TOP 1
            @AdminPer = ISNULL(admincharge, 0),
            @TdsPer = ISNULL(tdswithoutpan, 0)
        FROM dbo.tbl_Deduction;
    END

    SET @ClosingNo = ISNULL((SELECT MAX(ClosingNo) FROM dbo.RepurchaseCompressionMaster), 0) + 1;

    BEGIN TRY
        IF OBJECT_ID('tempdb..#Qualified') IS NOT NULL DROP TABLE #Qualified;
        IF OBJECT_ID('tempdb..#Purchases') IS NOT NULL DROP TABLE #Purchases;
        IF OBJECT_ID('tempdb..#LevelPct') IS NOT NULL DROP TABLE #LevelPct;
        IF OBJECT_ID('tempdb..#Pay') IS NOT NULL DROP TABLE #Pay;

        -- Users who purchased in this closing window (SelfBv only; Left/Right BV is team, not self purchase).
        SELECT DISTINCT
            LTRIM(RTRIM(UserId)) AS UserId
        INTO #Qualified
        FROM dbo.UserBuisnessVolumeRepurchase WITH (NOLOCK)
        WHERE CONVERT(date, CreateDate) BETWEEN @From AND @To
          AND ISNULL(SelfBv, 0) > 0
          AND NULLIF(LTRIM(RTRIM(ISNULL(UserId, ''))), '') IS NOT NULL;

        CREATE UNIQUE CLUSTERED INDEX IX_Qualified_UserId ON #Qualified (UserId);

        -- Date-wise self purchase rows used as income source.
        SELECT
            BV.Id,
            LTRIM(RTRIM(BV.UserId)) AS BuyerId,
            CONVERT(decimal(18,2), ISNULL(BV.SelfBv, 0)) AS SelfBv,
            ISNULL(BV.RefrenceId, '') AS OrderNo,
            BV.CreateDate
        INTO #Purchases
        FROM dbo.UserBuisnessVolumeRepurchase BV WITH (NOLOCK)
        WHERE CONVERT(date, BV.CreateDate) BETWEEN @From AND @To
          AND ISNULL(BV.SelfBv, 0) > 0
          AND NULLIF(LTRIM(RTRIM(ISNULL(BV.UserId, ''))), '') IS NOT NULL;

        CREATE CLUSTERED INDEX IX_Purchases_Id ON #Purchases (Id);

        -- One % per level: Direct = 0 slab. If that slab is missing, lowest Direct for the level.
        SELECT
            Lvl.[Level] AS LevelNo,
            CONVERT(decimal(18,2), ISNULL(Lvl.ROILEVELCOMMISSION, 0)) AS IncomePer
        INTO #LevelPct
        FROM (
            SELECT
                [Level],
                ROILEVELCOMMISSION,
                ROW_NUMBER() OVER (
                    PARTITION BY [Level]
                    ORDER BY CASE WHEN ISNULL(Direct, 0) = 0 THEN 0 ELSE 1 END, ISNULL(Direct, 0), Id
                ) AS Rn
            FROM dbo.RepurchaseLevelMaster WITH (NOLOCK)
            WHERE ISNULL(ROILEVELCOMMISSION, 0) >= 0
        ) Lvl
        WHERE Lvl.Rn = 1
          AND Lvl.[Level] BETWEEN 1 AND @MaxLevel;

        CREATE UNIQUE CLUSTERED INDEX IX_LevelPct_LevelNo ON #LevelPct (LevelNo);

        ;WITH Chain AS
        (
            SELECT
                p.Id AS PurchaseId,
                p.BuyerId,
                p.SelfBv,
                p.OrderNo,
                p.CreateDate,
                CAST(LTRIM(RTRIM(ISNULL(u.SponserId, ''))) AS NVARCHAR(100)) AS UplineId,
                1 AS GenLevel
            FROM #Purchases p
            INNER JOIN dbo.UserDetail u WITH (NOLOCK)
                ON LTRIM(RTRIM(u.UserId)) = p.BuyerId
            WHERE NULLIF(LTRIM(RTRIM(ISNULL(u.SponserId, ''))), '') IS NOT NULL
              AND LTRIM(RTRIM(u.SponserId)) NOT IN ('0', p.BuyerId)

            UNION ALL

            SELECT
                c.PurchaseId,
                c.BuyerId,
                c.SelfBv,
                c.OrderNo,
                c.CreateDate,
                CAST(LTRIM(RTRIM(ISNULL(u.SponserId, ''))) AS NVARCHAR(100)),
                c.GenLevel + 1
            FROM Chain c
            INNER JOIN dbo.UserDetail u WITH (NOLOCK)
                ON LTRIM(RTRIM(u.UserId)) = c.UplineId
            WHERE c.GenLevel < @MaxLevel
              AND NULLIF(LTRIM(RTRIM(ISNULL(u.SponserId, ''))), '') IS NOT NULL
              AND LTRIM(RTRIM(u.SponserId)) NOT IN ('0', c.BuyerId, c.UplineId)
        ),
        Compressed AS
        (
            SELECT
                PurchaseId,
                BuyerId,
                SelfBv,
                OrderNo,
                CreateDate,
                UplineId,
                GenLevel,
                ROW_NUMBER() OVER (PARTITION BY PurchaseId ORDER BY GenLevel) AS PaidLevel
            FROM Chain
            WHERE EXISTS (
                SELECT 1 FROM #Qualified q WHERE q.UserId = Chain.UplineId
            )
            AND NULLIF(UplineId, '') IS NOT NULL
            AND UplineId <> BuyerId
        )
        SELECT
            c.UplineId AS UserId,
            c.BuyerId AS FromUserId,
            c.SelfBv AS FromUserCommission,
            ISNULL(lp.IncomePer, 0) AS IncomePer,
            ROUND(c.SelfBv * ISNULL(lp.IncomePer, 0) / 100.0, 2) AS Income,
            c.PaidLevel AS LevelNo,
            c.OrderNo,
            c.CreateDate,
            CAST('Level' AS VARCHAR(20)) AS IncomeType
        INTO #Pay
        FROM Compressed c
        LEFT JOIN #LevelPct lp ON lp.LevelNo = c.PaidLevel
        WHERE c.PaidLevel <= @MaxLevel
          AND ROUND(c.SelfBv * ISNULL(lp.IncomePer, 0) / 100.0, 2) > 0
        OPTION (MAXRECURSION 10);

        -- Self 5% cashback on each purchase row.
        INSERT INTO #Pay (
            UserId, FromUserId, FromUserCommission, IncomePer, Income, LevelNo, OrderNo, CreateDate, IncomeType
        )
        SELECT
            p.BuyerId,
            p.BuyerId,
            p.SelfBv,
            @CashbackPer,
            ROUND(p.SelfBv * @CashbackPer / 100.0, 2),
            0,
            p.OrderNo,
            p.CreateDate,
            'Cashback'
        FROM #Purchases p
        WHERE ROUND(p.SelfBv * @CashbackPer / 100.0, 2) > 0;

        INSERT INTO dbo.ROIDailyLevelIncomeTB (
            Userid, Fromuserid, FROMuserCommission, IncomePer, income, LevelNo,
            Entrydate, Status, TDSPer, TDS, AdminPer, Admincharge, Paybleamount,
            FromDate, Todate, OrderNO, dailyid
        )
        SELECT
            p.UserId,
            p.FromUserId,
            p.FromUserCommission,
            p.IncomePer,
            p.Income,
            p.LevelNo,
            GETDATE(),
            0,
            @TdsPer,
            ROUND(p.Income * @TdsPer / 100.0, 2),
            @AdminPer,
            ROUND(p.Income * @AdminPer / 100.0, 2),
            ROUND(p.Income - (p.Income * @AdminPer / 100.0) - (p.Income * @TdsPer / 100.0), 2),
            @From,
            @To,
            p.OrderNo,
            @ClosingNo
        FROM #Pay p;

        SELECT
            @CashbackCount = SUM(CASE WHEN IncomeType = 'Cashback' THEN 1 ELSE 0 END),
            @CashbackAmount = SUM(CASE WHEN IncomeType = 'Cashback' THEN Income ELSE 0 END),
            @LevelCount = SUM(CASE WHEN IncomeType = 'Level' THEN 1 ELSE 0 END),
            @LevelAmount = SUM(CASE WHEN IncomeType = 'Level' THEN Income ELSE 0 END)
        FROM #Pay;

        SET @CashbackCount = ISNULL(@CashbackCount, 0);
        SET @CashbackAmount = ISNULL(@CashbackAmount, 0);
        SET @LevelCount = ISNULL(@LevelCount, 0);
        SET @LevelAmount = ISNULL(@LevelAmount, 0);

        /*
        -- Wallet posting. Keep commented until closing is verified.
        DECLARE @TxnStart INT;
        SELECT @TxnStart = ISNULL(MAX(TransactionId), 0) FROM dbo.TransactionDetail;

        INSERT INTO dbo.TransactionDetail (
            TransactionId, CrAmount, DrAmount, oldBalance, currentBalance, UserID,
            TransactionType, Remark, MentionBy, MentionDate, type
        )
        SELECT
            @TxnStart + ROW_NUMBER() OVER (ORDER BY p.UserId, p.LevelNo, p.OrderNo),
            ROUND(p.Income - (p.Income * @AdminPer / 100.0) - (p.Income * @TdsPer / 100.0), 2),
            0,
            ISNULL(ud.BalanceAmount, 0),
            ISNULL(ud.BalanceAmount, 0) + ROUND(p.Income - (p.Income * @AdminPer / 100.0) - (p.Income * @TdsPer / 100.0), 2),
            p.UserId,
            CASE WHEN p.IncomeType = 'Cashback' THEN N'Cashback Income' ELSE N'Repurchase Level Income' END,
            CASE
                WHEN p.IncomeType = 'Cashback'
                    THEN N'Self 5% cashback on repurchase ' + ISNULL(p.OrderNo, '') + N' BV ' + CONVERT(NVARCHAR(40), p.FromUserCommission)
                ELSE N'Level ' + CONVERT(NVARCHAR(10), p.LevelNo) + N' income from ' + p.FromUserId
                    + N' | Order ' + ISNULL(p.OrderNo, '') + N' | BV ' + CONVERT(NVARCHAR(40), p.FromUserCommission)
            END,
            N'admin',
            GETDATE(),
            1
        FROM #Pay p
        INNER JOIN dbo.UserDetail ud ON LTRIM(RTRIM(ud.UserId)) = p.UserId
        WHERE ROUND(p.Income - (p.Income * @AdminPer / 100.0) - (p.Income * @TdsPer / 100.0), 2) > 0;

        UPDATE ud
        SET ud.BalanceAmount = ISNULL(ud.BalanceAmount, 0) + x.PayAmount
        FROM dbo.UserDetail ud
        INNER JOIN (
            SELECT
                UserId,
                SUM(ROUND(Income - (Income * @AdminPer / 100.0) - (Income * @TdsPer / 100.0), 2)) AS PayAmount
            FROM #Pay
            GROUP BY UserId
        ) x ON LTRIM(RTRIM(ud.UserId)) = x.UserId;
        */

        INSERT INTO dbo.RepurchaseCompressionMaster (
            FromDate, ToDate, CreateDate, ClosingNo,
            CashbackCount, CashbackAmount, LevelCount, LevelAmount, Status
        )
        VALUES (
            @From, @To, GETDATE(), @ClosingNo,
            @CashbackCount, @CashbackAmount, @LevelCount, @LevelAmount, 0
        );

        SELECT
            't' AS Result,
            @ClosingNo AS ClosingNo,
            @From AS FromDate,
            @To AS ToDate,
            @CashbackCount AS CashbackCount,
            @CashbackAmount AS CashbackAmount,
            @LevelCount AS LevelCount,
            @LevelAmount AS LevelAmount;
    END TRY
    BEGIN CATCH
        DECLARE @Err NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Err, 16, 1);
    END CATCH
END
GO
