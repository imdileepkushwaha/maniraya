-- Bulk Purchase (18 months prepaid). New or existing saving users can buy.
-- Each bulk order is a separate package with its own coupon after approve.
-- Run this script on the database before using the Bulk Purchase page.
-- App pages also call sp_processSavingBulkSchedule so ship-date / month-end
-- catch up even if a SQL Agent job is not set up. Optional daily job:
--   EXEC dbo.sp_processSavingBulkSchedule
--
IF COL_LENGTH('dbo.SavingAccountDetail', 'PlanType') IS NULL
BEGIN
    ALTER TABLE dbo.SavingAccountDetail ADD PlanType NVARCHAR(50) NULL;
END
GO

IF COL_LENGTH('dbo.SavingAccountInstallmentDetail', 'IsBulkPrepaid') IS NULL
BEGIN
    ALTER TABLE dbo.SavingAccountInstallmentDetail ADD IsBulkPrepaid BIT NULL;
END
GO

IF COL_LENGTH('dbo.SavingAccountInstallmentDetail', 'IncomeReleased') IS NULL
BEGIN
    ALTER TABLE dbo.SavingAccountInstallmentDetail ADD IncomeReleased BIT NULL;
END
GO

IF OBJECT_ID('dbo.sp_add_SavingAccountBulkDetail', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_add_SavingAccountBulkDetail;
GO

CREATE PROC dbo.sp_add_SavingAccountBulkDetail
    @OrderId NVARCHAR(100),
    @UserId NVARCHAR(100),
    @Amount DECIMAL(18,2),
    @OnlineTransactionId NVARCHAR(100),
    @ImageName NVARCHAR(MAX),
    @EntryBy NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Utr NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@OnlineTransactionId, '')));

    IF (
        @Utr <> ''
        AND @Utr NOT LIKE 'CASH-%'
        AND (
            EXISTS (
                SELECT 1
                FROM SavingAccountDetail WITH (NOLOCK)
                WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@Utr)
                  AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
            )
            OR EXISTS (
                SELECT 1
                FROM SavingAccountInstallmentDetail WITH (NOLOCK)
                WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@Utr)
                  AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
            )
        )
    )
    BEGIN
        SELECT 'u';
        RETURN;
    END

    IF (EXISTS (SELECT userid FROM SavingAccountDetail WHERE userid = @UserId AND status IN ('Pending')))
    BEGIN
        SELECT 'f';
        RETURN;
    END

    DECLARE @productid INT = 0;
    IF OBJECT_ID('dbo.SavingInstallmentProductAssign', 'U') IS NOT NULL
    BEGIN
        SET @productid = ISNULL((
            SELECT TOP 1 a.ProductId
            FROM SavingInstallmentProductAssign a WITH (NOLOCK)
            WHERE ISNULL(a.Status, 1) = 1 AND a.InstallmentNo = 1
        ), 0);
    END
    IF (@productid = 0)
    BEGIN
        SET @productid = ISNULL((
            SELECT TOP 1 sd.productid
            FROM SavingMonthlyProductDetail sd WITH (NOLOCK)
            LEFT JOIN SavingProductMaster pd WITH (NOLOCK) ON sd.productid = pd.id
            WHERE sd.Status = 1
        ), 0);
    END

    DECLARE @monthly DECIMAL(18,2) = ISNULL((
        SELECT TOP 1 CASE WHEN ISNULL(pd.DP, 0) > 0 THEN pd.DP ELSE pd.MRP END
        FROM SavingProductMaster pd WITH (NOLOCK)
        WHERE pd.id = @productid
    ), 1000);
    IF (@monthly <= 0) SET @monthly = 1000;

    DECLARE @expected DECIMAL(18,2) = @monthly * 18;
    IF (ISNULL(@Amount, 0) <= 0 OR ABS(@Amount - @expected) > 1)
        SET @Amount = @expected;

    DECLARE @gst DECIMAL(18,2);
    SELECT @gst = gst FROM SavingProductMaster pd WITH (NOLOCK) WHERE id = @productid;

    DECLARE @sgst DECIMAL(18,2), @cgst DECIMAL(18,2), @igst DECIMAL(18,2), @stateid INT;
    SET @stateid = ISNULL((
        SELECT cm.stateid
        FROM UserDetail ud WITH (NOLOCK)
        LEFT JOIN CityMaster cm WITH (NOLOCK) ON cm.CityId = ud.CityId
        WHERE ud.userid = @UserId
    ), 0);

    IF (@stateid = 14)
    BEGIN
        SET @sgst = (@gst / 2);
        SET @cgst = (@gst / 2);
        SET @igst = 0;
    END
    ELSE
    BEGIN
        SET @sgst = 0;
        SET @cgst = 0;
        SET @igst = @gst;
    END

    DECLARE @ordertype NVARCHAR(100) = 'FreshPurchase';
    IF (EXISTS (
        SELECT userid FROM SavingAccountDetail
        WHERE userid = @UserId AND orderid != @OrderId AND status != 'Rejected'
    ))
        SET @ordertype = 'Repurchase';

    INSERT INTO SavingAccountDetail (
        OrderId, UserId, Amount, OnlineTransactionId, ImageName, Status, EntryBy, EntryDate,
        productid, couponcode, sgst, cgst, igst, ordertype, PlanType
    )
    VALUES (
        @OrderId, @UserId, @Amount, @Utr, @ImageName, 'Pending', @EntryBy, GETDATE(),
        @productid, NULL, @sgst, @cgst, @igst, @ordertype, 'Bulk18'
    );

    SELECT 't';
END
GO

IF OBJECT_ID('dbo.sp_markSavingBulkPrepaid', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_markSavingBulkPrepaid;
GO

CREATE PROC dbo.sp_markSavingBulkPrepaid
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @orderId NVARCHAR(100);
    DECLARE @userId NVARCHAR(100);
    DECLARE @planType NVARCHAR(50);
    DECLARE @status NVARCHAR(50);
    DECLARE @joinDate DATETIME;
    DECLARE @utr NVARCHAR(100);
    DECLARE @parentAmount DECIMAL(18,2);

    SELECT
        @orderId = sd.orderid,
        @userId = sd.userid,
        @planType = sd.PlanType,
        @status = sd.status,
        @joinDate = ISNULL(sd.approvedate, GETDATE()),
        @utr = sd.OnlineTransactionId,
        @parentAmount = sd.Amount
    FROM SavingAccountDetail sd
    WHERE sd.id = @id;

    IF (@orderId IS NULL OR ISNULL(@planType, '') <> 'Bulk18')
    BEGIN
        SELECT 't';
        RETURN;
    END

    IF (UPPER(LTRIM(RTRIM(ISNULL(@status, '')))) NOT IN ('APPROVED', 'APPROVE', '1'))
    BEGIN
        SELECT 'f';
        RETURN;
    END

    UPDATE sa
    SET
        sa.Status = 'Paid',
        sa.ApproveDate = NULL,
        sa.RequestDate = GETDATE(),
        sa.OnlineTransactionId = CASE
            WHEN NULLIF(LTRIM(RTRIM(ISNULL(sa.OnlineTransactionId, ''))), '') IS NULL
                THEN LEFT('BULK-' + ISNULL(@orderId, '') + '-' + CONVERT(NVARCHAR(10), ISNULL(sa.InstNo, 0)), 100)
            ELSE sa.OnlineTransactionId
        END,
        sa.productid = CASE
            WHEN ISNULL(ipa.ProductId, 0) > 0 THEN ipa.ProductId
            ELSE sa.productid
        END,
        sa.InstallmentDate = DATEADD(MONTH, ISNULL(TRY_CONVERT(INT, sa.InstNo), 2) - 1, CONVERT(date, @joinDate)),
        sa.Amount = CASE
            WHEN ISNULL(@parentAmount, 0) >= 15000 THEN CAST(ROUND(@parentAmount / CAST(18 AS DECIMAL(18,2)), 2) AS DECIMAL(18,2))
            ELSE sa.Amount
        END,
        sa.DeliveryStatus = 'Scheduled',
        sa.DeliveryStatusUpdatedOn = GETDATE(),
        sa.DeliveryStatusUpdatedBy = 'SYSTEM-BULK',
        sa.IsBulkPrepaid = 1,
        sa.IncomeReleased = 0
    FROM SavingAccountInstallmentDetail sa
    LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
        ON ISNULL(ipa.Status, 1) = 1
       AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
    WHERE sa.OrderId = @orderId
      AND LTRIM(RTRIM(sa.UserId)) = LTRIM(RTRIM(@userId))
      AND ISNULL(TRY_CONVERT(INT, sa.InstNo), 0) > 1;

    SELECT 't';
END
GO

IF OBJECT_ID('dbo.sp_cloneSavingBulkInstallmentIncome', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_cloneSavingBulkInstallmentIncome;
GO

CREATE PROC dbo.sp_cloneSavingBulkInstallmentIncome
    @InstallmentId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @orderId NVARCHAR(100);
    DECLARE @userId NVARCHAR(100);
    DECLARE @instNo INT;
    DECLARE @joinDate DATETIME;

    SELECT
        @orderId = sa.orderid,
        @userId = sa.userid,
        @instNo = TRY_CONVERT(INT, sa.InstNo),
        @joinDate = ISNULL(sd.approvedate, sd.entrydate)
    FROM SavingAccountInstallmentDetail sa
    INNER JOIN SavingAccountDetail sd
        ON sd.orderid = sa.orderid
       AND LTRIM(RTRIM(sd.UserId)) = LTRIM(RTRIM(sa.UserId))
    WHERE sa.id = @InstallmentId;

    IF (@orderId IS NULL OR ISNULL(@instNo, 0) <= 1)
        RETURN;

    DECLARE @buyerCol SYSNAME = NULL;
    IF COL_LENGTH('SavingLevelIncomeDetail', 'JuniorUserId') IS NOT NULL
        SET @buyerCol = 'JuniorUserId';
    ELSE IF COL_LENGTH('SavingLevelIncomeDetail', 'Fromuserid') IS NOT NULL
        SET @buyerCol = 'Fromuserid';
    ELSE IF COL_LENGTH('SavingLevelIncomeDetail', 'FromUserId') IS NOT NULL
        SET @buyerCol = 'FromUserId';

    IF (@buyerCol IS NULL)
        RETURN;

    DECLARE @dateCol SYSNAME = 'MentionDate';
    IF COL_LENGTH('SavingLevelIncomeDetail', 'MentionDate') IS NULL
        AND COL_LENGTH('SavingLevelIncomeDetail', 'Entrydate') IS NOT NULL
        SET @dateCol = 'Entrydate';

    DECLARE @sql NVARCHAR(MAX);
    DECLARE @cols NVARCHAR(MAX) = '';
    DECLARE @sels NVARCHAR(MAX) = '';

    SELECT
        @cols = @cols + CASE WHEN @cols = '' THEN '' ELSE ',' END + QUOTENAME(c.name),
        @sels = @sels + CASE WHEN @sels = '' THEN '' ELSE ',' END
            + CASE
                WHEN c.name IN ('InstNo', 'instno', 'INSTNO') THEN CONVERT(NVARCHAR(20), @instNo)
                WHEN c.name IN ('MentionDate', 'mentiondate', 'Entrydate', 'entrydate', 'EntryDate') THEN 'GETDATE()'
                ELSE 'src.' + QUOTENAME(c.name)
              END
    FROM sys.columns c
    WHERE c.object_id = OBJECT_ID('dbo.SavingLevelIncomeDetail')
      AND c.is_identity = 0
      AND c.is_computed = 0
    ORDER BY c.column_id;

    IF (@cols = '')
        RETURN;

    SET @sql = N'
    IF NOT EXISTS (
        SELECT 1 FROM SavingLevelIncomeDetail WITH (NOLOCK)
        WHERE ISNULL(InstNo, 0) = @pInst
          AND LTRIM(RTRIM(' + QUOTENAME(@buyerCol) + N')) = LTRIM(RTRIM(@pUser))
    )
    BEGIN
        INSERT INTO SavingLevelIncomeDetail (' + @cols + N')
        SELECT ' + @sels + N'
        FROM SavingLevelIncomeDetail src
        WHERE LTRIM(RTRIM(src.' + QUOTENAME(@buyerCol) + N')) = LTRIM(RTRIM(@pUser))
          AND CONVERT(date, src.' + QUOTENAME(@dateCol) + N') = CONVERT(date, @pJoin)
          AND ISNULL(src.InstNo, 1) <= 1;
    END';

    BEGIN TRY
        EXEC sp_executesql @sql,
            N'@pInst INT, @pUser NVARCHAR(100), @pJoin DATETIME',
            @pInst = @instNo, @pUser = @userId, @pJoin = @joinDate;
    END TRY
    BEGIN CATCH
        -- Keep approve/ship flow even if income clone cannot map every column.
    END CATCH

    IF OBJECT_ID('dbo.TransactionDetail', 'U') IS NOT NULL
    BEGIN
        DECLARE @txSql NVARCHAR(MAX);
        DECLARE @txCols NVARCHAR(MAX) = '';
        DECLARE @txSels NVARCHAR(MAX) = '';

        SELECT
            @txCols = @txCols + CASE WHEN @txCols = '' THEN '' ELSE ',' END + QUOTENAME(c.name),
            @txSels = @txSels + CASE WHEN @txSels = '' THEN '' ELSE ',' END
                + CASE
                    WHEN c.name IN ('MentionDate', 'mentiondate', 'Entrydate', 'entrydate', 'EntryDate', 'TransactionDate') THEN 'GETDATE()'
                    WHEN c.name IN ('Narration', 'Remark', 'Remarks', 'Description') THEN
                        '''Bulk Inst ' + CONVERT(NVARCHAR(10), @instNo) + ' Order ' + REPLACE(@orderId, '''', '') + ''''
                    ELSE 'src.' + QUOTENAME(c.name)
                  END
        FROM sys.columns c
        WHERE c.object_id = OBJECT_ID('dbo.TransactionDetail')
          AND c.is_identity = 0
          AND c.is_computed = 0
        ORDER BY c.column_id;

        IF (@txCols <> '')
        BEGIN
            DECLARE @txDateCol SYSNAME = 'MentionDate';
            IF COL_LENGTH('TransactionDetail', 'MentionDate') IS NULL
                AND COL_LENGTH('TransactionDetail', 'Entrydate') IS NOT NULL
                SET @txDateCol = 'Entrydate';

            SET @txSql = N'
            INSERT INTO TransactionDetail (' + @txCols + N')
            SELECT ' + @txSels + N'
            FROM TransactionDetail src
            WHERE src.TransactionType = ''Saving Level Income''
              AND CONVERT(date, src.' + QUOTENAME(@txDateCol) + N') = CONVERT(date, @pJoin)
              AND src.UserId IN (
                    SELECT DISTINCT inc.UserId
                    FROM SavingLevelIncomeDetail inc
                    WHERE LTRIM(RTRIM(inc.' + QUOTENAME(@buyerCol) + N')) = LTRIM(RTRIM(@pUser))
                      AND ISNULL(inc.InstNo, 0) = @pInst
              )
              AND NOT EXISTS (
                    SELECT 1 FROM TransactionDetail t2
                    WHERE t2.TransactionType = ''Saving Level Income''
                      AND t2.UserId = src.UserId
                      AND CONVERT(date, t2.' + QUOTENAME(@txDateCol) + N') = CONVERT(date, GETDATE())
                      AND ISNULL(t2.CrAmount, 0) = ISNULL(src.CrAmount, 0)
              );';

            BEGIN TRY
                EXEC sp_executesql @txSql,
                    N'@pInst INT, @pUser NVARCHAR(100), @pJoin DATETIME',
                    @pInst = @instNo, @pUser = @userId, @pJoin = @joinDate;
            END TRY
            BEGIN CATCH
            END CATCH
        END
    END
END
GO

IF OBJECT_ID('dbo.sp_processSavingBulkSchedule', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_processSavingBulkSchedule;
GO

CREATE PROC dbo.sp_processSavingBulkSchedule
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @today DATE = CONVERT(date, GETDATE());

    DECLARE @catchId INT;
    DECLARE catchup CURSOR LOCAL FAST_FORWARD FOR
        SELECT sd.id
        FROM SavingAccountDetail sd
        WHERE LTRIM(RTRIM(ISNULL(sd.PlanType, ''))) = 'Bulk18'
          AND UPPER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('APPROVED', 'APPROVE', '1')
          AND EXISTS (
                SELECT 1
                FROM SavingAccountInstallmentDetail sa
                WHERE sa.OrderId = sd.orderid
                  AND LTRIM(RTRIM(sa.UserId)) = LTRIM(RTRIM(sd.UserId))
                  AND ISNULL(TRY_CONVERT(INT, sa.InstNo), 0) > 1
                  AND ISNULL(sa.IsBulkPrepaid, 0) = 0
                  AND UPPER(LTRIM(RTRIM(ISNULL(sa.status, '')))) IN ('PENDING', '0', '')
            );

    OPEN catchup;
    FETCH NEXT FROM catchup INTO @catchId;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC dbo.sp_markSavingBulkPrepaid @catchId;
        FETCH NEXT FROM catchup INTO @catchId;
    END
    CLOSE catchup;
    DEALLOCATE catchup;

    -- Product ship + prize/draw/rank date = join-day anniversary.
    UPDATE sa
    SET
        sa.Status = 'Approved',
        sa.ApproveDate = CASE WHEN sa.ApproveDate IS NULL THEN GETDATE() ELSE sa.ApproveDate END,
        sa.DeliveryStatus = 'Confirmed',
        sa.DeliveryStatusUpdatedOn = GETDATE(),
        sa.DeliveryStatusUpdatedBy = 'SYSTEM-BULK',
        sa.productid = CASE WHEN ISNULL(ipa.ProductId, 0) > 0 THEN ipa.ProductId ELSE sa.productid END
    FROM SavingAccountInstallmentDetail sa
    LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
        ON ISNULL(ipa.Status, 1) = 1
       AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
    WHERE ISNULL(sa.IsBulkPrepaid, 0) = 1
      AND UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) IN ('PAID', 'APPROVED', 'APPROVE')
      AND @today >= CONVERT(date, ISNULL(sa.InstallmentDate, '99991231'))
      AND (
            UPPER(LTRIM(RTRIM(ISNULL(sa.DeliveryStatus, '')))) IN ('SCHEDULED', '')
            OR sa.DeliveryStatus IS NULL
            OR (UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) = 'PAID')
          );

    DECLARE @id INT;
    DECLARE due CURSOR LOCAL FAST_FORWARD FOR
        SELECT sa.id
        FROM SavingAccountInstallmentDetail sa
        WHERE ISNULL(sa.IsBulkPrepaid, 0) = 1
          AND ISNULL(sa.IncomeReleased, 0) = 0
          AND ISNULL(TRY_CONVERT(INT, sa.InstNo), 0) > 1
          AND @today >= EOMONTH(CONVERT(date, ISNULL(sa.InstallmentDate, '99991231')));

    OPEN due;
    FETCH NEXT FROM due INTO @id;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC dbo.sp_cloneSavingBulkInstallmentIncome @id;

        UPDATE SavingAccountInstallmentDetail
        SET IncomeReleased = 1
        WHERE id = @id;

        FETCH NEXT FROM due INTO @id;
    END
    CLOSE due;
    DEALLOCATE due;

    SELECT 't';
END
GO
