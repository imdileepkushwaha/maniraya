-- Run if tbl_JewelleryProduct already exists (adds new columns).
IF COL_LENGTH('tbl_JewelleryProduct', 'MetalType') IS NULL
    ALTER TABLE tbl_JewelleryProduct ADD MetalType NVARCHAR(50) NULL;

IF COL_LENGTH('tbl_JewelleryProduct', 'JewelleryType') IS NULL
    ALTER TABLE tbl_JewelleryProduct ADD JewelleryType NVARCHAR(100) NULL;

IF COL_LENGTH('tbl_JewelleryProduct', 'SizeId') IS NULL
    ALTER TABLE tbl_JewelleryProduct ADD SizeId INT NULL;

IF COL_LENGTH('tbl_JewelleryProduct', 'SizeName') IS NULL
    ALTER TABLE tbl_JewelleryProduct ADD SizeName NVARCHAR(500) NULL;

IF OBJECT_ID('tbl_JewelleryProductSize', 'U') IS NULL
BEGIN
    CREATE TABLE tbl_JewelleryProductSize (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        JewelleryId INT NOT NULL,
        SizeId INT NOT NULL,
        SizeName NVARCHAR(100) NOT NULL,
        CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
    );
    CREATE INDEX IX_tbl_JewelleryProductSize_JewelleryId ON tbl_JewelleryProductSize(JewelleryId);
END
