-- Delivery / dispatch tracking for approved saving product orders

IF COL_LENGTH('SavingAccountDetail', 'DeliveryStatus') IS NULL
BEGIN
    ALTER TABLE SavingAccountDetail ADD DeliveryStatus NVARCHAR(50) NULL;
END
GO

IF COL_LENGTH('SavingAccountDetail', 'DeliveryStatusUpdatedOn') IS NULL
BEGIN
    ALTER TABLE SavingAccountDetail ADD DeliveryStatusUpdatedOn DATETIME NULL;
END
GO

IF COL_LENGTH('SavingAccountDetail', 'DeliveryStatusUpdatedBy') IS NULL
BEGIN
    ALTER TABLE SavingAccountDetail ADD DeliveryStatusUpdatedBy NVARCHAR(100) NULL;
END
GO

UPDATE SavingAccountDetail
SET DeliveryStatus = 'Confirmed'
WHERE LOWER(LTRIM(RTRIM(ISNULL(status, '')))) IN ('approved', '1', 'active')
  AND (DeliveryStatus IS NULL OR LTRIM(RTRIM(DeliveryStatus)) = '');
GO
