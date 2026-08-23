-- Saving product monthly add: EntryDate is mandatory (1st of month).
-- One product per calendar month (by EntryDate). Starts from 01-Sep-2026 in UI.
CREATE OR ALTER PROCEDURE dbo.sp_add_SavingProductMaster
    @ProductName NVARCHAR(100),
    @MRP DECIMAL(18, 2),
    @DP DECIMAL(18, 2),
    @ImageName NVARCHAR(MAX),
    @EntryBy NVARCHAR(100),
    @GST DECIMAL(18, 2),
    @HSNCode NVARCHAR(100),
    @EntryDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EntryDay DATE = CONVERT(DATE, @EntryDate);

    -- Must be 1st of month
    IF DAY(@EntryDay) <> 1
    BEGIN
        SELECT 'd'; -- invalid entry date (not 1st)
        RETURN;
    END

    -- One monthly product per calendar month (only rows dated 1st of month).
    -- Catalog add (SavingProductAdd) uses GETDATE() and must not block monthly add.
    IF EXISTS (
        SELECT 1
        FROM SavingProductMaster WITH (NOLOCK)
        WHERE YEAR(EntryDate) = YEAR(@EntryDay)
          AND MONTH(EntryDate) = MONTH(@EntryDay)
          AND DAY(EntryDate) = 1
    )
    BEGIN
        SELECT 'm'; -- month already used
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM SavingProductMaster WITH (NOLOCK)
        WHERE ProductName = @ProductName
    )
    BEGIN
        SELECT 'f'; -- product name already exists
        RETURN;
    END

    INSERT INTO SavingProductMaster
        (ProductName, mrp, dp, ImageName, entryby, entrydate, status, GST, HSNCode)
    VALUES
        (@ProductName, @MRP, @DP, @ImageName, @EntryBy, @EntryDay, 1, @GST, @HSNCode);

    SELECT 't';
END
GO
