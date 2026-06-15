-- Run once on the application database (optional but recommended).
CREATE OR ALTER PROCEDURE dbo.sp_add_CategoryMaster
    @CategoryName NVARCHAR(200),
    @MentionBy NVARCHAR(100),
    @img NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CategoryMaster (CategoryName, MentionBy, img)
    VALUES (@CategoryName, @MentionBy, ISNULL(@img, ''));

    SELECT 't';
END
GO
