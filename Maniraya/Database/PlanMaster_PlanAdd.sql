-- Run once if Planmaster is missing the new plan fields.
IF COL_LENGTH('dbo.Planmaster', 'CreateDate') IS NULL
BEGIN
    ALTER TABLE dbo.Planmaster ADD CreateDate DATETIME NULL;
END
GO

IF COL_LENGTH('dbo.Planmaster', 'cappingamount') IS NULL
BEGIN
    ALTER TABLE dbo.Planmaster ADD cappingamount DECIMAL(18, 2) NULL;
END
GO

UPDATE dbo.Planmaster
SET CreateDate = GETDATE()
WHERE CreateDate IS NULL;
GO

UPDATE dbo.Planmaster
SET cappingamount = ISNULL(MonthlyAmount, 0)
WHERE cappingamount IS NULL;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.default_constraints dc
    INNER JOIN sys.columns c ON c.default_object_id = dc.object_id
    INNER JOIN sys.tables t ON t.object_id = c.object_id
    WHERE t.name = 'Planmaster' AND c.name = 'CreateDate'
)
BEGIN
    ALTER TABLE dbo.Planmaster ADD CONSTRAINT DF_Planmaster_CreateDate DEFAULT (GETDATE()) FOR CreateDate;
END
GO
