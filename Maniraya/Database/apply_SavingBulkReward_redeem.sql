IF COL_LENGTH('dbo.SavingBulkReward', 'RewardCouponCode') IS NULL
BEGIN
    ALTER TABLE dbo.SavingBulkReward ADD RewardCouponCode NVARCHAR(50) NULL;
END
GO

IF COL_LENGTH('dbo.SavingBulkReward', 'CouponRedeemStatus') IS NULL
BEGIN
    ALTER TABLE dbo.SavingBulkReward ADD CouponRedeemStatus NVARCHAR(50) NULL;
END
GO

IF COL_LENGTH('dbo.SavingBulkReward', 'CouponRedeemDate') IS NULL
BEGIN
    ALTER TABLE dbo.SavingBulkReward ADD CouponRedeemDate DATETIME NULL;
END
GO

UPDATE SavingBulkReward
SET RewardCouponCode = 'CP' + CONVERT(NVARCHAR(20), Id) + RIGHT('0000' + CONVERT(NVARCHAR(20), ISNULL(BulkPaymentId, 0)), 4)
WHERE NULLIF(LTRIM(RTRIM(ISNULL(RewardCouponCode, ''))), '') IS NULL;
GO

UPDATE SavingBulkReward
SET CouponRedeemStatus = 'Pending'
WHERE NULLIF(LTRIM(RTRIM(ISNULL(CouponRedeemStatus, ''))), '') IS NULL;
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
        DECLARE @rewardCode NVARCHAR(50) = N'CP' + CONVERT(NVARCHAR(20), @bulkId) + UPPER(LEFT(REPLACE(CONVERT(NVARCHAR(36), NEWID()), '-', ''), 4));

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
                N'Bulk EMI coupon ' + ISNULL(@rewardCode, '') + N' | Request #' + CONVERT(NVARCHAR(20), @bulkId),
                @adminUser, @now, 4
            );
        END

        INSERT INTO SavingBulkReward (
            BulkPaymentId, UserId, OrderId, CouponCode, RewardCouponCode,
            ShoppingPointAmount, CouponAmount, EntryDate, RedeemDate,
            Status, CouponRedeemStatus, ApproveBy, DummyTransactionId, Remark
        )
        VALUES (
            @bulkId, @userId, @orderId, @couponCode, @rewardCode,
            @shopAmt, @couponAmt, @now, DATEADD(MONTH, 18, @now),
            N'Locked', N'Pending', @adminUser, NULLIF(@txnId, 0),
            N'Bulk EMI shopping point 20000 (purchase after 18 months) + coupon 2000'
        );
    END

    SELECT 't';
END
GO

IF OBJECT_ID('dbo.sp_redeemSavingBulkCoupon', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_redeemSavingBulkCoupon;
GO

CREATE PROC dbo.sp_redeemSavingBulkCoupon
    @id INT,
    @UserId NVARCHAR(100),
    @Remark NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @user NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@UserId, '')));
    DECLARE @rewardUser NVARCHAR(100);
    DECLARE @couponAmt DECIMAL(18,2);
    DECLARE @rewardCode NVARCHAR(50);
    DECLARE @redeemStatus NVARCHAR(50);
    IF (@user = '' OR ISNULL(@id, 0) <= 0) BEGIN SELECT 'n'; RETURN; END
    SELECT
        @rewardUser = LTRIM(RTRIM(UserId)),
        @couponAmt = ISNULL(CouponAmount, 0),
        @rewardCode = RewardCouponCode,
        @redeemStatus = CouponRedeemStatus
    FROM SavingBulkReward
    WHERE Id = @id;
    IF (@rewardUser IS NULL) BEGIN SELECT '0'; RETURN; END
    IF (LTRIM(RTRIM(@rewardUser)) <> @user) BEGIN SELECT 'n'; RETURN; END
    IF (UPPER(LTRIM(RTRIM(ISNULL(@redeemStatus, 'Pending')))) = 'REDEEMED') BEGIN SELECT 'f'; RETURN; END
    IF (ISNULL(@couponAmt, 0) <= 0) BEGIN SELECT 'n'; RETURN; END
    DECLARE @now DATETIME = GETDATE();
    DECLARE @dummyBal DECIMAL(18,2) = 0;
    DECLARE @note NVARCHAR(MAX) = N'Redeem coupon ' + ISNULL(@rewardCode, '') + N' against product MRP | Reward #' + CONVERT(NVARCHAR(20), @id);
    IF (LTRIM(RTRIM(ISNULL(@Remark, ''))) <> '')
        SET @note = @note + N' | ' + LTRIM(RTRIM(@Remark));
    IF OBJECT_ID('dbo.TransactionDetail_dummy', 'U') IS NOT NULL
    BEGIN
        SELECT @dummyBal = ISNULL(SUM(ISNULL(cramount, 0)), 0) - ISNULL(SUM(ISNULL(dramount, 0)), 0)
        FROM TransactionDetail_dummy WITH (NOLOCK)
        WHERE LTRIM(RTRIM(userid)) = @user;
        IF (@dummyBal < @couponAmt) BEGIN SELECT 'b'; RETURN; END
        DECLARE @dummyNew DECIMAL(18,2) = @dummyBal - @couponAmt;
        DECLARE @dummyTxn INT = 0;
        SELECT @dummyTxn = ISNULL(MAX(transactionid), 0) + 1 FROM TransactionDetail_dummy;
        INSERT INTO TransactionDetail_dummy (
            transactionid, cramount, dramount, oldBalance, currentBalance,
            userid, transactiontype, remark, mentionby, mentiondate, type
        )
        VALUES (
            @dummyTxn, 0, @couponAmt, @dummyBal, @dummyNew,
            @user, N'Bulk Coupon Redeem',
            @note,
            @user, @now, 4
        );
    END
    UPDATE SavingBulkReward
    SET CouponRedeemStatus = N'Redeemed', CouponRedeemDate = @now
    WHERE Id = @id;
    SELECT 't';
END
GO
