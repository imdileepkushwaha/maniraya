using System;

public class JewelleryPriceResult
{
    public decimal GoldAmount { get; set; }
    public decimal SilverAmount { get; set; }
    public decimal DiamondAmount { get; set; }
    public decimal Subtotal { get; set; }
    public decimal GstAmount { get; set; }
    public decimal Price { get; set; }
    public decimal Mrp { get; set; }
}

public static class JewelleryPriceHelper
{
    public const decimal MrpMarkupPercent = 15m;

    public static JewelleryPriceResult Calculate(
        decimal goldWeight,
        decimal silverWeight,
        decimal diamondCarat,
        decimal makingCharges,
        decimal gstPercent,
        decimal goldRate,
        decimal silverRate,
        decimal diamondRate)
    {
        decimal goldAmount = Round2(goldWeight * goldRate);
        decimal silverAmount = Round2(silverWeight * silverRate);
        decimal diamondAmount = Round2(diamondCarat * diamondRate);
        decimal subtotal = Round2(goldAmount + silverAmount + diamondAmount + makingCharges);
        decimal gstAmount = Round2(subtotal * gstPercent / 100m);
        decimal price = Round2(subtotal + gstAmount);
        decimal mrp = Round2(Math.Ceiling(price * (1m + MrpMarkupPercent / 100m) / 10m) * 10m);

        return new JewelleryPriceResult
        {
            GoldAmount = goldAmount,
            SilverAmount = silverAmount,
            DiamondAmount = diamondAmount,
            Subtotal = subtotal,
            GstAmount = gstAmount,
            Price = price,
            Mrp = mrp
        };
    }

    public static decimal Round2(decimal value)
    {
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}
