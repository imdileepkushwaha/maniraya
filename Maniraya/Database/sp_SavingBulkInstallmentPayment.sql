-- Bulk pay remaining 17 EMIs (InstNo 2-18) after first purchase is approved.
-- User submits one UTR + one attachment. Admin approves/rejects in one shot.
-- On approve, Inst 2-18 ApproveDate is staggered one month each from the pay date:
--   Pay 26/08/2026 -> Inst 2 = 26/09/2026, Inst 3 = 26/10/2026, ...
-- Also credits: 20000 shopping point (locked 18 months) + 2000 coupon in TransactionDetail_dummy.
-- Run this script on the database (app pages also create these objects if missing).

IF OBJECT_ID('dbo.SavingBulkInstallmentPayment', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.SavingBulkInstallmentPayment
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        UserId NVARCHAR(100) NOT NULL,
        OrderId NVARCHAR(100) NULL,
        CouponCode NVARCHAR(100) NULL,
        AccountId INT NULL,
        Amount DECIMAL(18,2) NULL,
        InstCount INT NULL,
        OnlineTransactionId NVARCHAR(100) NULL,
        ImageName NVARCHAR(MAX) NULL,
        Status NVARCHAR(50) NOT NULL CONSTRAINT DF_SavingBulkInstPay_Status DEFAULT ('Processing'),
        RequestDate DATETIME NOT NULL CONSTRAINT DF_SavingBulkInstPay_RequestDate DEFAULT (GETDATE()),
        ApproveDate DATETIME NULL,
        ApproveBy NVARCHAR(100) NULL,
        Remark NVARCHAR(MAX) NULL,
        EntryBy NVARCHAR(100) NULL,
        EntryDate DATETIME NOT NULL CONSTRAINT DF_SavingBulkInstPay_EntryDate DEFAULT (GETDATE())
    );
END
GO

IF COL_LENGTH('dbo.SavingAccountInstallmentDetail', 'BulkPaymentId') IS NULL
BEGIN
    ALTER TABLE dbo.SavingAccountInstallmentDetail ADD BulkPaymentId INT NULL;
END
GO

IF OBJECT_ID('dbo.SavingBulkReward', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.SavingBulkReward
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        BulkPaymentId INT NOT NULL,
        UserId NVARCHAR(100) NOT NULL,
        OrderId NVARCHAR(100) NULL,
        CouponCode NVARCHAR(100) NULL,
        ShoppingPointAmount DECIMAL(18,2) NOT NULL CONSTRAINT DF_SavingBulkReward_Shop DEFAULT (20000),
        CouponAmount DECIMAL(18,2) NOT NULL CONSTRAINT DF_SavingBulkReward_Coupon DEFAULT (2000),
        EntryDate DATETIME NOT NULL CONSTRAINT DF_SavingBulkReward_Entry DEFAULT (GETDATE()),
        RedeemDate DATETIME NOT NULL,
        Status NVARCHAR(50) NOT NULL CONSTRAINT DF_SavingBulkReward_Status DEFAULT ('Locked'),
        ApproveBy NVARCHAR(100) NULL,
        DummyTransactionId INT NULL,
        Remark NVARCHAR(MAX) NULL
    );
    CREATE UNIQUE INDEX UQ_SavingBulkReward_BulkPaymentId ON dbo.SavingBulkReward(BulkPaymentId);
END
GO

IF OBJECT_ID('dbo.TransactionDetail_dummy', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TransactionDetail_dummy
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        transactionid INT NULL,
        cramount DECIMAL(18,2) NULL,
        dramount DECIMAL(18,2) NULL,
        oldBalance DECIMAL(18,2) NULL,
        currentBalance DECIMAL(18,2) NULL,
        userid NVARCHAR(100) NULL,
        transactiontype NVARCHAR(200) NULL,
        remark NVARCHAR(MAX) NULL,
        mentionby NVARCHAR(100) NULL,
        mentiondate DATETIME NULL,
        type INT NULL
    );
END
GO

IF OBJECT_ID('dbo.sp_add_SavingBulkInstallmentPayment', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_add_SavingBulkInstallmentPayment;
GO

CREATE PROC dbo.sp_add_SavingBulkInstallmentPayment
    @UserId NVARCHAR(100),
    @CouponCode NVARCHAR(100),
    @OnlineTransactionId NVARCHAR(100),
    @ImageName NVARCHAR(MAX),
    @EntryBy NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @user NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@UserId, '')));
    DECLARE @coupon NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@CouponCode, '')));
    DECLARE @utr NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@OnlineTransactionId, '')));
    DECLARE @image NVARCHAR(MAX) = LTRIM(RTRIM(ISNULL(@ImageName, '')));
    DECLARE @by NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@EntryBy, @user)));

    IF (@user = '' OR @coupon = '')
    BEGIN
        SELECT 'n';
        RETURN;
    END

    IF (
        @utr <> ''
        AND @utr NOT LIKE 'CASH-%'
        AND (
            EXISTS (
                SELECT 1 FROM SavingAccountDetail WITH (NOLOCK)
                WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@utr)
                  AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
            )
            OR EXISTS (
                SELECT 1 FROM SavingAccountInstallmentDetail WITH (NOLOCK)
                WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@utr)
                  AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
            )
            OR EXISTS (
                SELECT 1 FROM SavingBulkInstallmentPayment WITH (NOLOCK)
                WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@utr)
                  AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
            )
        )
    )
    BEGIN
        SELECT 'u';
        RETURN;
    END

    DECLARE @accountId INT;
    DECLARE @orderId NVARCHAR(100);
    DECLARE @planType NVARCHAR(50);

    SELECT TOP 1
        @accountId = sd.id,
        @orderId = sd.orderid
    FROM SavingAccountDetail sd WITH (NOLOCK)
    WHERE LTRIM(RTRIM(sd.UserId)) = @user
      AND LTRIM(RTRIM(ISNULL(sd.couponcode, ''))) = @coupon
      AND UPPER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('APPROVED', 'APPROVE', '1', 'ACTIVE')
    ORDER BY sd.id DESC;

    IF (@accountId IS NULL OR ISNULL(@orderId, '') = '')
    BEGIN
        SELECT 'n';
        RETURN;
    END

    IF COL_LENGTH('SavingAccountDetail', 'PlanType') IS NOT NULL
    BEGIN
        SELECT @planType = PlanType FROM SavingAccountDetail WITH (NOLOCK) WHERE id = @accountId;
        IF (UPPER(LTRIM(RTRIM(ISNULL(@planType, '')))) = 'BULK18')
        BEGIN
            SELECT 'n';
            RETURN;
        END
    END

    IF EXISTS (
        SELECT 1 FROM SavingBulkInstallmentPayment WITH (NOLOCK)
        WHERE LTRIM(RTRIM(UserId)) = @user
          AND LTRIM(RTRIM(ISNULL(CouponCode, ''))) = @coupon
          AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) = 'PROCESSING'
    )
    BEGIN
        SELECT 'f';
        RETURN;
    END

    DECLARE @pendingCount INT = 0;
    DECLARE @blockedCount INT = 0;
    DECLARE @totalAmount DECIMAL(18,2) = 0;
    DECLARE @hasInstCoupon BIT = CASE WHEN COL_LENGTH('SavingAccountInstallmentDetail', 'CouponCode') IS NOT NULL THEN 1 ELSE 0 END;

    SELECT
        @pendingCount = SUM(CASE
            WHEN UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) NOT IN ('PROCESSING', 'APPROVED', 'APPROVE', '1', 'ACTIVE', 'PAID')
            THEN 1 ELSE 0 END),
        @blockedCount = SUM(CASE
            WHEN UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) = 'PROCESSING'
            THEN 1 ELSE 0 END),
        @totalAmount = SUM(CASE
            WHEN UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) NOT IN ('PROCESSING', 'APPROVED', 'APPROVE', '1', 'ACTIVE', 'PAID')
            THEN ISNULL(sa.Amount, 0) ELSE 0 END)
    FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
    WHERE LTRIM(RTRIM(sa.UserId)) = @user
      AND ISNULL(TRY_CONVERT(INT, sa.InstNo), 0) BETWEEN 2 AND 18
      AND (
            (@hasInstCoupon = 1 AND (
                LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))) = @coupon
                OR (
                    NULLIF(LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))), '') IS NULL
                    AND sa.OrderId = @orderId
                )
            ))
            OR (@hasInstCoupon = 0 AND sa.OrderId = @orderId)
          );

    IF (ISNULL(@pendingCount, 0) <= 0 OR ISNULL(@blockedCount, 0) <> 0)
    BEGIN
        SELECT 'n';
        RETURN;
    END

    INSERT INTO SavingBulkInstallmentPayment (
        UserId, OrderId, CouponCode, AccountId, Amount, InstCount,
        OnlineTransactionId, ImageName, Status, RequestDate, EntryBy, EntryDate
    )
    VALUES (
        @user, @orderId, @coupon, @accountId, ISNULL(@totalAmount, 0), ISNULL(@pendingCount, 0),
        @utr, @image, 'Processing', GETDATE(), @by, GETDATE()
    );

    DECLARE @bulkId INT = SCOPE_IDENTITY();

    UPDATE SavingAccountInstallmentDetail
    SET
        Status = 'Processing',
        OnlineTransactionId = @utr,
        ImageName = @image,
        RequestDate = GETDATE(),
        Remark = NULL,
        BulkPaymentId = @bulkId
    WHERE LTRIM(RTRIM(UserId)) = @user
      AND ISNULL(TRY_CONVERT(INT, InstNo), 0) BETWEEN 2 AND 18
      AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) NOT IN ('PROCESSING', 'APPROVED', 'APPROVE', '1', 'ACTIVE', 'PAID')
      AND (
            (@hasInstCoupon = 1 AND (
                LTRIM(RTRIM(ISNULL(CouponCode, ''))) = @coupon
                OR (
                    NULLIF(LTRIM(RTRIM(ISNULL(CouponCode, ''))), '') IS NULL
                    AND OrderId = @orderId
                )
            ))
            OR (@hasInstCoupon = 0 AND OrderId = @orderId)
          );

    SELECT 't';
END
GO

IF OBJECT_ID('dbo.sp_approveSavingBulkInstallmentPayment', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_approveSavingBulkInstallmentPayment;
GO

CREATE PROC dbo.sp_approveSavingBulkInstallmentPayment
    @id INT,
    @Approveby NVARCHAR(100),
    @Remark NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @status NVARCHAR(50);
    DECLARE @orderId NVARCHAR(100);
    DECLARE @userId NVARCHAR(100);
    DECLARE @couponCode NVARCHAR(100);
    DECLARE @requestDate DATETIME;
    DECLARE @bulkId INT = @id;

    SELECT
        @status = bp.Status,
        @orderId = bp.OrderId,
        @userId = bp.UserId,
        @couponCode = bp.CouponCode,
        @requestDate = bp.RequestDate
    FROM SavingBulkInstallmentPayment bp
    WHERE bp.Id = @bulkId;

    IF (@status IS NULL)
    BEGIN
        SELECT '0';
        RETURN;
    END

    IF (UPPER(LTRIM(RTRIM(ISNULL(@status, '')))) <> 'PROCESSING')
    BEGIN
        SELECT 'f';
        RETURN;
    END

    DECLARE @baseDate DATE = CONVERT(date, ISNULL(@requestDate, GETDATE()));
    DECLARE @remarkText NVARCHAR(MAX) = LTRIM(RTRIM(ISNULL(@Remark, '')));
    DECLARE @adminUser NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@Approveby, '')));
    DECLARE @now DATETIME = GETDATE();
    DECLARE @hasDelivery BIT = CASE WHEN COL_LENGTH('SavingAccountInstallmentDetail', 'DeliveryStatus') IS NOT NULL THEN 1 ELSE 0 END;

    UPDATE SavingBulkInstallmentPayment
    SET
        Status = 'Approved',
        ApproveDate = @now,
        ApproveBy = @adminUser,
        Remark = CASE WHEN @remarkText = '' THEN Remark ELSE @remarkText END
    WHERE Id = @bulkId;

    UPDATE sa
    SET
        sa.Status = 'Approved',
        sa.ApproveDate = DATEADD(MONTH, ISNULL(TRY_CONVERT(INT, sa.InstNo), 2) - 1, @baseDate),
        sa.OnlineTransactionId = ISNULL(NULLIF(LTRIM(RTRIM(sa.OnlineTransactionId)), ''), bp.OnlineTransactionId),
        sa.ImageName = ISNULL(NULLIF(LTRIM(RTRIM(sa.ImageName)), ''), bp.ImageName),
        sa.Remark = CASE WHEN @remarkText = '' THEN sa.Remark ELSE @remarkText END,
        sa.BulkPaymentId = @bulkId,
        sa.productid = CASE WHEN ISNULL(ipa.ProductId, 0) > 0 THEN ipa.ProductId ELSE sa.productid END
    FROM SavingAccountInstallmentDetail sa
    INNER JOIN SavingBulkInstallmentPayment bp ON bp.Id = @bulkId
    LEFT JOIN SavingInstallmentProductAssign ipa WITH (NOLOCK)
        ON ISNULL(ipa.Status, 1) = 1
       AND ipa.InstallmentNo = TRY_CONVERT(INT, sa.InstNo)
    WHERE (
            ISNULL(sa.BulkPaymentId, 0) = @bulkId
            OR (
                LTRIM(RTRIM(sa.UserId)) = LTRIM(RTRIM(@userId))
                AND LTRIM(RTRIM(ISNULL(sa.CouponCode, ''))) = LTRIM(RTRIM(ISNULL(bp.CouponCode, '')))
                AND ISNULL(TRY_CONVERT(INT, sa.InstNo), 0) BETWEEN 2 AND 18
                AND UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) = 'PROCESSING'
            )
          )
      AND UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) IN ('PROCESSING', 'PENDING');

    IF (@hasDelivery = 1)
    BEGIN
        UPDATE sa
        SET
            sa.DeliveryStatus = 'Scheduled',
            sa.DeliveryStatusUpdatedOn = GETDATE(),
            sa.DeliveryStatusUpdatedBy = @adminUser
        FROM SavingAccountInstallmentDetail sa
        WHERE ISNULL(sa.BulkPaymentId, 0) = @bulkId
          AND UPPER(LTRIM(RTRIM(ISNULL(sa.Status, '')))) IN ('APPROVED', 'APPROVE', '1');
    END

    IF OBJECT_ID('dbo.SavingBulkReward', 'U') IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM SavingBulkReward WITH (NOLOCK) WHERE BulkPaymentId = @bulkId)
    BEGIN
        DECLARE @couponAmt DECIMAL(18,2) = 2000;
        DECLARE @shopAmt DECIMAL(18,2) = 20000;
        DECLARE @oldBal DECIMAL(18,2) = 0;
        DECLARE @newBal DECIMAL(18,2) = 0;
        DECLARE @txnId INT = 0;

        IF OBJECT_ID('dbo.TransactionDetail_dummy', 'U') IS NOT NULL
        BEGIN
            SELECT @oldBal = ISNULL(SUM(ISNULL(cramount, 0)), 0) - ISNULL(SUM(ISNULL(dramount, 0)), 0)
            FROM TransactionDetail_dummy WITH (NOLOCK)
            WHERE LTRIM(RTRIM(userid)) = LTRIM(RTRIM(@userId));

            SET @newBal = @oldBal + @couponAmt;
            SELECT @txnId = ISNULL(MAX(transactionid), 0) + 1 FROM TransactionDetail_dummy;

            INSERT INTO TransactionDetail_dummy (
                transactionid, cramount, dramount, oldBalance, currentBalance,
                userid, transactiontype, remark, mentionby, mentiondate, type
            )
            VALUES (
                @txnId, @couponAmt, 0, @oldBal, @newBal,
                @userId, N'Bulk Coupon',
                N'Bulk EMI coupon reward | Coupon ' + ISNULL(@couponCode, '') + N' | Request #' + CONVERT(NVARCHAR(20), @bulkId),
                @adminUser, @now, 4
            );
        END

        INSERT INTO SavingBulkReward (
            BulkPaymentId, UserId, OrderId, CouponCode,
            ShoppingPointAmount, CouponAmount, EntryDate, RedeemDate,
            Status, ApproveBy, DummyTransactionId, Remark
        )
        VALUES (
            @bulkId, @userId, @orderId, @couponCode,
            @shopAmt, @couponAmt, @now, DATEADD(MONTH, 18, @now),
            N'Locked', @adminUser, NULLIF(@txnId, 0),
            N'Bulk EMI shopping point 20000 (redeem after 18 months) + coupon 2000'
        );
    END

    SELECT 't';
END
GO

IF OBJECT_ID('dbo.sp_rejectSavingBulkInstallmentPayment', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_rejectSavingBulkInstallmentPayment;
GO

CREATE PROC dbo.sp_rejectSavingBulkInstallmentPayment
    @id INT,
    @Approveby NVARCHAR(100),
    @Remark NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @status NVARCHAR(50);
    DECLARE @orderId NVARCHAR(100);
    DECLARE @userId NVARCHAR(100);
    DECLARE @bulkId INT = @id;
    DECLARE @remarkText NVARCHAR(MAX) = LTRIM(RTRIM(ISNULL(@Remark, '')));
    DECLARE @adminUser NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@Approveby, '')));

    IF (@remarkText = '')
    BEGIN
        SELECT 'r';
        RETURN;
    END

    SELECT
        @status = bp.Status,
        @orderId = bp.OrderId,
        @userId = bp.UserId
    FROM SavingBulkInstallmentPayment bp
    WHERE bp.Id = @bulkId;

    IF (@status IS NULL)
    BEGIN
        SELECT '0';
        RETURN;
    END

    IF (UPPER(LTRIM(RTRIM(ISNULL(@status, '')))) <> 'PROCESSING')
    BEGIN
        SELECT 'f';
        RETURN;
    END

    UPDATE SavingBulkInstallmentPayment
    SET
        Status = 'Rejected',
        ApproveDate = GETDATE(),
        ApproveBy = @adminUser,
        Remark = @remarkText
    WHERE Id = @bulkId;

    UPDATE SavingAccountInstallmentDetail
    SET
        Status = 'Rejected',
        Remark = @remarkText,
        BulkPaymentId = @bulkId
    WHERE (
            ISNULL(BulkPaymentId, 0) = @bulkId
            OR (
                LTRIM(RTRIM(UserId)) = LTRIM(RTRIM(@userId))
                AND LTRIM(RTRIM(ISNULL(CouponCode, ''))) = (
                    SELECT LTRIM(RTRIM(ISNULL(CouponCode, '')))
                    FROM SavingBulkInstallmentPayment
                    WHERE Id = @bulkId
                )
                AND ISNULL(TRY_CONVERT(INT, InstNo), 0) BETWEEN 2 AND 18
                AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) = 'PROCESSING'
            )
          );

    SELECT 't';
END
GO
