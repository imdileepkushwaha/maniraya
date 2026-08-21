-- Product weight in grams. Safe to run multiple times.
IF COL_LENGTH('dbo.ProductMaster', 'Weight') IS NULL
BEGIN
    ALTER TABLE dbo.ProductMaster ADD Weight DECIMAL(18, 2) NULL;
END
GO
