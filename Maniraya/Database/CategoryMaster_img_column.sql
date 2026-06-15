-- Run once on the application database if category images are not saving.
IF COL_LENGTH('dbo.CategoryMaster', 'img') IS NULL
BEGIN
    ALTER TABLE dbo.CategoryMaster ADD img NVARCHAR(500) NULL;
    UPDATE dbo.CategoryMaster SET img = '' WHERE img IS NULL;
END
GO
