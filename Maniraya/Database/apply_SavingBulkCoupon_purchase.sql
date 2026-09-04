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
