-- Run once on the application database.
CREATE OR ALTER PROCEDURE dbo.sp_add_JewelleryProduct
    @Title NVARCHAR(300),
    @ShortDescription NVARCHAR(1000) = NULL,
    @Description NVARCHAR(MAX) = NULL,
    @Image1 NVARCHAR(500) = NULL,
    @Image2 NVARCHAR(500) = NULL,
    @Image3 NVARCHAR(500) = NULL,
    @Image4 NVARCHAR(500) = NULL,
    @MetalType NVARCHAR(50) = NULL,
    @JewelleryType NVARCHAR(100) = NULL,
    @SizeId INT = NULL,
    @SizeName NVARCHAR(100) = NULL,
    @GoldWeight DECIMAL(18,3) = 0,
    @SilverWeight DECIMAL(18,3) = 0,
    @DiamondCarat DECIMAL(18,3) = 0,
    @MakingCharges DECIMAL(18,2) = 0,
    @GstPercent DECIMAL(5,2) = 0,
    @GoldRate DECIMAL(18,2) = 0,
    @SilverRate DECIMAL(18,2) = 0,
    @DiamondRate DECIMAL(18,2) = 0,
    @GoldAmount DECIMAL(18,2) = 0,
    @SilverAmount DECIMAL(18,2) = 0,
    @DiamondAmount DECIMAL(18,2) = 0,
    @Subtotal DECIMAL(18,2) = 0,
    @GstAmount DECIMAL(18,2) = 0,
    @Price DECIMAL(18,2) = 0,
    @MRP DECIMAL(18,2) = 0,
    @BV DECIMAL(18,2) = 0,
    @HSNCode NVARCHAR(50) = NULL,
    @CreatedBy NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO tbl_JewelleryProduct (
        Title, ShortDescription, Description,
        Image1, Image2, Image3, Image4,
        MetalType, JewelleryType, SizeId, SizeName,
        GoldWeight, SilverWeight, DiamondCarat, MakingCharges, GstPercent,
        GoldRate, SilverRate, DiamondRate,
        GoldAmount, SilverAmount, DiamondAmount,
        Subtotal, GstAmount, Price, MRP, BV,
        HSNCode, Status, CreatedBy
    )
    VALUES (
        @Title, @ShortDescription, @Description,
        ISNULL(@Image1, ''), ISNULL(@Image2, ''), ISNULL(@Image3, ''), ISNULL(@Image4, ''),
        ISNULL(@MetalType, ''), ISNULL(@JewelleryType, ''), @SizeId, ISNULL(@SizeName, ''),
        @GoldWeight, @SilverWeight, @DiamondCarat, @MakingCharges, @GstPercent,
        @GoldRate, @SilverRate, @DiamondRate,
        @GoldAmount, @SilverAmount, @DiamondAmount,
        @Subtotal, @GstAmount, @Price, @MRP, @BV,
        ISNULL(@HSNCode, ''), 1, @CreatedBy
    );

    SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR(20));
END
GO
