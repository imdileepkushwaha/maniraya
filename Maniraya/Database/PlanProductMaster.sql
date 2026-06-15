-- Run once if PlanProductMaster table does not exist.
IF OBJECT_ID('dbo.PlanProductMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PlanProductMaster
    (
        id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        planid INT NOT NULL,
        productid INT NOT NULL,
        quantity INT NOT NULL CONSTRAINT DF_PlanProductMaster_quantity DEFAULT (1)
    );
END
GO

IF COL_LENGTH('dbo.PlanProductMaster', 'quantity') IS NULL AND COL_LENGTH('dbo.PlanProductMaster', 'Qnty') IS NULL
BEGIN
    ALTER TABLE dbo.PlanProductMaster ADD quantity INT NOT NULL CONSTRAINT DF_PlanProductMaster_quantity_add DEFAULT (1);
END
GO
