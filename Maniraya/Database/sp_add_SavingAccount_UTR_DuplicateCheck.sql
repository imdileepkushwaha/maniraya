-- Harden UTR / OnlineTransactionId duplicate checks for saving purchase + installment.
-- Rejected rows are ignored so a rejected request can resubmit the same UTR.
-- Cross-table: same UTR cannot be used on purchase and installment together.

ALTER PROC sp_add_SavingAccountDetail
@OrderId NVARCHAR(100),
@UserId NVARCHAR(100),
@Amount DECIMAL(18,2),
@OnlineTransactionId NVARCHAR(100),
@ImageName NVARCHAR(max),
@EntryBy NVARCHAR(100),
@quantity INT
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

    DECLARE @ordertype NVARCHAR(100);
    IF (EXISTS (SELECT userid FROM SavingAccountDetail WHERE userid = @UserId AND orderid != @OrderId AND status != 'Rejected'))
        SET @ordertype = 'Repurchase';
    ELSE
        SET @ordertype = 'FreshPurchase';

    IF (@ordertype <> 'FreshPurchase')
    BEGIN
        SELECT 'r';
        RETURN;
    END

    DECLARE @productid INT;
    SET @productid = ISNULL((
        SELECT TOP 1 sd.productid
        FROM SavingMonthlyProductDetail sd WITH (NOLOCK)
        LEFT JOIN SavingProductMaster pd WITH (NOLOCK) ON sd.productid = pd.id
        WHERE sd.Status = 1
    ), 0);

    DECLARE @gst DECIMAL(18,2);
    SELECT @gst = gst FROM SavingProductMaster pd WITH (NOLOCK) WHERE id = @productid;

    DECLARE @counter INT = 1;
    WHILE (@counter <= @quantity)
    BEGIN
        DECLARE @couponcode NVARCHAR(10);
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

        INSERT INTO SavingAccountDetail (
            OrderId, UserId, Amount, OnlineTransactionId, ImageName, Status, EntryBy, EntryDate,
            productid, couponcode, sgst, cgst, igst, ordertype
        )
        VALUES (
            @OrderId, @UserId, @Amount, @Utr, @ImageName, 'Pending', @EntryBy, GETDATE(),
            @productid, @couponcode, @sgst, @cgst, @igst, @ordertype
        );

        SET @counter = @counter + 1;
    END

    SELECT 't';
END
GO

ALTER PROC sp_add_SavingAccountInstallmentDetail
@id INT,
@OnlineTransactionId NVARCHAR(100),
@ImageName NVARCHAR(max)
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
                FROM SavingAccountInstallmentDetail WITH (NOLOCK)
                WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@Utr)
                  AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
                  AND id <> @id
            )
            OR EXISTS (
                SELECT 1
                FROM SavingAccountDetail WITH (NOLOCK)
                WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@Utr)
                  AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
            )
        )
    )
    BEGIN
        SELECT 'u';
        RETURN;
    END

    DECLARE @productid INT;
    SET @productid = ISNULL((
        SELECT TOP 1 sd.productid
        FROM SavingMonthlyProductDetail sd WITH (NOLOCK)
        LEFT JOIN SavingProductMaster pd WITH (NOLOCK) ON sd.productid = pd.id
        WHERE sd.Status = 1
    ), 0);

    IF (EXISTS (SELECT id FROM SavingAccountInstallmentDetail WHERE id = @id AND status = 'Pending'))
    BEGIN
        UPDATE SavingAccountInstallmentDetail
        SET Status = 'Processing',
            Onlinetransactionid = @Utr,
            imagename = @ImageName,
            RequestDate = GETDATE(),
            productid = @productid
        WHERE Id = @id;

        SELECT 't';
        RETURN;
    END

    IF (EXISTS (SELECT id FROM SavingAccountInstallmentDetail WHERE id = @id AND status = 'Rejected'))
    BEGIN
        INSERT INTO SavingAccountInstallmentHistory (Status, Onlinetransactionid, imagename, RequestDate, MentionBy, MentionDate)
        SELECT Status, Onlinetransactionid, imagename, RequestDate, EntryBy, GETDATE()
        FROM SavingAccountInstallmentDetail
        WHERE Id = @id;

        UPDATE SavingAccountInstallmentDetail
        SET Status = 'Processing',
            Onlinetransactionid = @Utr,
            imagename = @ImageName,
            RequestDate = GETDATE(),
            productid = @productid
        WHERE Id = @id;

        SELECT 't';
        RETURN;
    END

    SELECT 'f';
END
GO
