-- One product per installment (1 to 18).
-- New user join uses InstallmentNo = 1.
-- Later EMI payment uses that installment's mapped product.

IF OBJECT_ID('dbo.SavingInstallmentProductAssign', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.SavingInstallmentProductAssign
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        InstallmentNo INT NOT NULL,
        ProductId INT NOT NULL,
        Status BIT NOT NULL CONSTRAINT DF_SavingInstallmentProductAssign_Status DEFAULT (1),
        EntryBy NVARCHAR(100) NULL,
        EntryDate DATETIME NOT NULL CONSTRAINT DF_SavingInstallmentProductAssign_EntryDate DEFAULT (GETDATE()),
        CONSTRAINT UQ_SavingInstallmentProductAssign_Inst UNIQUE (InstallmentNo)
    );
END
GO
