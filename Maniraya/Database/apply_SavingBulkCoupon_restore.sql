-- Rejected repurchase should reactivate the same bulk coupon.
IF COL_LENGTH('dbo.SavingBulkReward', 'RedeemOrderNo') IS NULL
BEGIN
    ALTER TABLE dbo.SavingBulkReward ADD RedeemOrderNo NVARCHAR(100) NULL;
END
GO

IF OBJECT_ID('dbo.sp_redeemSavingBulkCoupon', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_redeemSavingBulkCoupon;
GO

CREATE PROC dbo.sp_redeemSavingBulkCoupon
    @id INT,
    @UserId NVARCHAR(100),
    @Remark NVARCHAR(MAX) = NULL,
    @OrderNo NVARCHAR(100) = NULL
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
    DECLARE @order NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@OrderNo, '')));
    DECLARE @note NVARCHAR(MAX) = N'Redeem coupon ' + ISNULL(@rewardCode, '') + N' against DP billing | Reward #' + CONVERT(NVARCHAR(20), @id);
    IF (@order <> '')
        SET @note = @note + N' | Order ' + @order;
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
    SET CouponRedeemStatus = N'Redeemed',
        CouponRedeemDate = @now,
        RedeemOrderNo = CASE WHEN @order = '' THEN RedeemOrderNo ELSE @order END
    WHERE Id = @id;
    SELECT 't';
END
GO

IF OBJECT_ID('dbo.sp_restoreSavingBulkCoupon', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_restoreSavingBulkCoupon;
GO

CREATE PROC dbo.sp_restoreSavingBulkCoupon
    @UserId NVARCHAR(100),
    @OrderNo NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @user NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@UserId, '')));
    DECLARE @order NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@OrderNo, '')));
    IF (@user = '' OR @order = '') BEGIN SELECT 'n'; RETURN; END

    DECLARE @rewardId INT = NULL;
    DECLARE @couponAmt DECIMAL(18,2) = 0;
    DECLARE @rewardCode NVARCHAR(50);
    DECLARE @payMode NVARCHAR(50);
    DECLARE @txn NVARCHAR(100);

    IF COL_LENGTH('dbo.SavingBulkReward', 'RedeemOrderNo') IS NOT NULL
    BEGIN
        SELECT TOP 1
            @rewardId = r.Id,
            @couponAmt = ISNULL(r.CouponAmount, 0),
            @rewardCode = r.RewardCouponCode
        FROM SavingBulkReward r
        WHERE LTRIM(RTRIM(r.UserId)) = @user
          AND LTRIM(RTRIM(ISNULL(r.RedeemOrderNo, ''))) = @order
          AND UPPER(LTRIM(RTRIM(ISNULL(r.CouponRedeemStatus, 'Pending')))) = 'REDEEMED'
        ORDER BY r.Id DESC;
    END

    IF OBJECT_ID('dbo.UserFranchiseePurchaseMaster', 'U') IS NOT NULL
    BEGIN
        SELECT TOP 1
            @payMode = LTRIM(RTRIM(ISNULL(PaymentMode, ''))),
            @txn = LTRIM(RTRIM(ISNULL(Onlinetransactionid, '')))
        FROM UserFranchiseePurchaseMaster
        WHERE LTRIM(RTRIM(UserId)) = @user
          AND (LTRIM(RTRIM(ISNULL(OrderNo, ''))) = @order OR CONVERT(NVARCHAR(40), PurchaseId) = @order)
        ORDER BY PurchaseId DESC;
    END

    IF (@rewardId IS NULL AND ISNULL(@txn, '') <> '')
    BEGIN
        SELECT TOP 1
            @rewardId = r.Id,
            @couponAmt = ISNULL(r.CouponAmount, 0),
            @rewardCode = r.RewardCouponCode
        FROM SavingBulkReward r
        WHERE LTRIM(RTRIM(r.UserId)) = @user
          AND LTRIM(RTRIM(ISNULL(r.RewardCouponCode, ''))) = @txn
          AND UPPER(LTRIM(RTRIM(ISNULL(r.CouponRedeemStatus, 'Pending')))) = 'REDEEMED'
        ORDER BY r.CouponRedeemDate DESC, r.Id DESC;
    END

    IF (@rewardId IS NULL AND (@payMode LIKE N'%Coupo%' OR @payMode LIKE N'%COUPO%'))
    BEGIN
        SELECT TOP 1
            @rewardId = r.Id,
            @couponAmt = ISNULL(r.CouponAmount, 0),
            @rewardCode = r.RewardCouponCode
        FROM SavingBulkReward r
        WHERE LTRIM(RTRIM(r.UserId)) = @user
          AND UPPER(LTRIM(RTRIM(ISNULL(r.CouponRedeemStatus, 'Pending')))) = 'REDEEMED'
        ORDER BY r.CouponRedeemDate DESC, r.Id DESC;
    END

    IF (@rewardId IS NULL AND OBJECT_ID('dbo.TransactionDetail_dummy', 'U') IS NOT NULL)
    BEGIN
        SELECT TOP 1
            @rewardId = r.Id,
            @couponAmt = ISNULL(r.CouponAmount, 0),
            @rewardCode = r.RewardCouponCode
        FROM SavingBulkReward r
        WHERE LTRIM(RTRIM(r.UserId)) = @user
          AND UPPER(LTRIM(RTRIM(ISNULL(r.CouponRedeemStatus, 'Pending')))) = 'REDEEMED'
          AND EXISTS (
                SELECT 1
                FROM TransactionDetail_dummy d
                WHERE LTRIM(RTRIM(d.userid)) = @user
                  AND ISNULL(d.transactiontype, '') LIKE N'%Bulk Coupon Redeem%'
                  AND ISNULL(d.remark, '') LIKE N'%' + @order + N'%'
            )
        ORDER BY r.CouponRedeemDate DESC, r.Id DESC;
    END

    IF (@rewardId IS NULL) BEGIN SELECT '0'; RETURN; END
    IF (ISNULL(@couponAmt, 0) <= 0) BEGIN SELECT 'n'; RETURN; END

    DECLARE @now DATETIME = GETDATE();
    DECLARE @dummyBal DECIMAL(18,2) = 0;
    IF OBJECT_ID('dbo.TransactionDetail_dummy', 'U') IS NOT NULL
    BEGIN
        SELECT @dummyBal = ISNULL(SUM(ISNULL(cramount, 0)), 0) - ISNULL(SUM(ISNULL(dramount, 0)), 0)
        FROM TransactionDetail_dummy WITH (NOLOCK)
        WHERE LTRIM(RTRIM(userid)) = @user;
        DECLARE @dummyNew DECIMAL(18,2) = @dummyBal + @couponAmt;
        DECLARE @dummyTxn INT = 0;
        SELECT @dummyTxn = ISNULL(MAX(transactionid), 0) + 1 FROM TransactionDetail_dummy;
        INSERT INTO TransactionDetail_dummy (
            transactionid, cramount, dramount, oldBalance, currentBalance,
            userid, transactiontype, remark, mentionby, mentiondate, type
        )
        VALUES (
            @dummyTxn, @couponAmt, 0, @dummyBal, @dummyNew,
            @user, N'Bulk Coupon Restore',
            N'Restore coupon ' + ISNULL(@rewardCode, '') + N' after purchase reject | Order ' + @order,
            @user, @now, 4
        );
    END

    UPDATE SavingBulkReward
    SET CouponRedeemStatus = N'Pending',
        CouponRedeemDate = NULL,
        RedeemOrderNo = NULL
    WHERE Id = @rewardId;
    SELECT 't';
END
GO
