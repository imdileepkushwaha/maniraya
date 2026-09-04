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
