IF OBJECT_ID('dbo.SavingCashfreePayment', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.SavingCashfreePayment
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        OrderId NVARCHAR(60) NOT NULL,
        UserId NVARCHAR(50) NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        CustomerName NVARCHAR(200) NULL,
        CustomerPhone NVARCHAR(20) NULL,
        CustomerEmail NVARCHAR(200) NULL,
        PaymentSessionId NVARCHAR(200) NULL,
        CfOrderId NVARCHAR(100) NULL,
        CfPaymentId NVARCHAR(100) NULL,
        Status NVARCHAR(30) NOT NULL CONSTRAINT DF_SavingCashfreePayment_Status DEFAULT ('Pending'),
        SavingInserted BIT NOT NULL CONSTRAINT DF_SavingCashfreePayment_Inserted DEFAULT (0),
        SavingResult NVARCHAR(20) NULL,
        RawPayload NVARCHAR(MAX) NULL,
        EntryDate DATETIME NOT NULL CONSTRAINT DF_SavingCashfreePayment_Entry DEFAULT (GETDATE()),
        PaidDate DATETIME NULL
    );
    CREATE UNIQUE INDEX UX_SavingCashfreePayment_OrderId ON dbo.SavingCashfreePayment (OrderId);
END
GO