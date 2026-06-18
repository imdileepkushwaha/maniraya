using System;

public static class JewelleryMetalTypes
{
    public const string Gold = "Gold";
    public const string Silver = "Silver";
    public const string Diamond = "Diamond";
    public const string GoldDiamond = "GoldDiamond";
    public const string GoldSilver = "GoldSilver";
    public const string SilverDiamond = "SilverDiamond";
    public const string GoldSilverDiamond = "GoldSilverDiamond";

    public static bool RequiresGold(string metalType)
    {
        return metalType == Gold
            || metalType == GoldDiamond
            || metalType == GoldSilver
            || metalType == GoldSilverDiamond;
    }

    public static bool RequiresSilver(string metalType)
    {
        return metalType == Silver
            || metalType == GoldSilver
            || metalType == SilverDiamond
            || metalType == GoldSilverDiamond;
    }

    public static bool RequiresDiamond(string metalType)
    {
        return metalType == Diamond
            || metalType == GoldDiamond
            || metalType == SilverDiamond
            || metalType == GoldSilverDiamond;
    }

    public static void NormalizeWeights(string metalType, ref decimal goldWeight, ref decimal silverWeight, ref decimal diamondCarat)
    {
        if (!RequiresGold(metalType))
        {
            goldWeight = 0m;
        }

        if (!RequiresSilver(metalType))
        {
            silverWeight = 0m;
        }

        if (!RequiresDiamond(metalType))
        {
            diamondCarat = 0m;
        }
    }
}
