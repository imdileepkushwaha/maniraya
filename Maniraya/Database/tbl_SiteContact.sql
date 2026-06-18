IF OBJECT_ID('tbl_SiteContact', 'U') IS NULL
BEGIN
    CREATE TABLE tbl_SiteContact (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ContactType NVARCHAR(20) NOT NULL,
        Title NVARCHAR(200) NULL,
        ContactValue NVARCHAR(MAX) NOT NULL,
        DisplayOrder INT NOT NULL DEFAULT 0,
        IsPrimary BIT NOT NULL DEFAULT 0,
        Status BIT NOT NULL DEFAULT 1,
        CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
    );
END
