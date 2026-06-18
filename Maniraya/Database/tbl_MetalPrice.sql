IF OBJECT_ID('tbl_MetalPrice', 'U') IS NULL
BEGIN
    CREATE TABLE tbl_MetalPrice (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MetalType NVARCHAR(20) NOT NULL,
        Price DECIMAL(18,2) NOT NULL DEFAULT 0,
        PriceUnit NVARCHAR(30) NOT NULL DEFAULT 'Per Gram',
        UpdatedOn DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedBy NVARCHAR(100) NULL
    );

    CREATE UNIQUE INDEX UX_tbl_MetalPrice_MetalType ON tbl_MetalPrice(MetalType);

    INSERT INTO tbl_MetalPrice (MetalType, Price, PriceUnit, UpdatedBy)
    VALUES
        ('Gold', 0, 'Per Gram', 'System'),
        ('Silver', 0, 'Per Gram', 'System'),
        ('Diamond', 0, 'Per Carat', 'System');
END
