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
