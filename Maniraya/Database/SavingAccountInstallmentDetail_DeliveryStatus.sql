-- Delivery / dispatch tracking for approved saving installment orders

IF COL_LENGTH('SavingAccountInstallmentDetail', 'DeliveryStatus') IS NULL
BEGIN
    ALTER TABLE SavingAccountInstallmentDetail ADD DeliveryStatus NVARCHAR(50) NULL;
END
GO

IF COL_LENGTH('SavingAccountInstallmentDetail', 'DeliveryStatusUpdatedOn') IS NULL
BEGIN
    ALTER TABLE SavingAccountInstallmentDetail ADD DeliveryStatusUpdatedOn DATETIME NULL;
END
GO

IF COL_LENGTH('SavingAccountInstallmentDetail', 'DeliveryStatusUpdatedBy') IS NULL
BEGIN
    ALTER TABLE SavingAccountInstallmentDetail ADD DeliveryStatusUpdatedBy NVARCHAR(100) NULL;
END
GO

UPDATE SavingAccountInstallmentDetail
SET DeliveryStatus = 'Confirmed'
WHERE LOWER(LTRIM(RTRIM(ISNULL(status, '')))) IN ('approved', '1', 'active')
  AND (DeliveryStatus IS NULL OR LTRIM(RTRIM(DeliveryStatus)) = '');
GO
