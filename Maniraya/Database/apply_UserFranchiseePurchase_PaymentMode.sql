-- PaymentMode on repurchase master is varchar(10).
-- "Bulk Coupon" is 11 chars and was truncated to "Bulk Coupo".
ALTER TABLE dbo.UserFranchiseePurchaseMaster
ALTER COLUMN PaymentMode VARCHAR(50) NULL;
GO
