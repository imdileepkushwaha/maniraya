using DataTier;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Fast Track: self needs 10 qualifying directs, and 10 of those directs
/// must each have 10 qualifying directs of their own.
/// Qualifying direct = approved ₹18,000 / Bulk18 saving purchase in the offer window.
/// </summary>
public static class FastTrackHelper
{
    public const int RequiredSelfDirects = 10;
    public const int RequiredDirectsPerLeg = 10;
    public const int RequiredCompleteLegs = 10;
    public const decimal QualifyingSaleAmount = 18000m;
    public static readonly DateTime OfferStartDate = new DateTime(2026, 8, 25);
    public static readonly DateTime OfferEndDate = new DateTime(2026, 10, 30);

    public class Progress
    {
        public int SelfDirects { get; set; }
        public int SelfDirectsPending { get; set; }
        public int CompleteLegs { get; set; }
        public int CompleteLegsPending { get; set; }
        public int SelfPercent { get; set; }
        public int LegsPercent { get; set; }
        public bool IsAchieved { get; set; }
        public string StatusLabel { get; set; }
    }

    public static Progress GetProgress(string userId)
    {
        return GetProgress(userId, LoadQualifyingSales());
    }

    public static Progress GetProgress(string userId, DataTable sales)
    {
        return GetProgress(userId, BuildDirectMap(sales));
    }

    public static Progress GetProgress(string userId, Dictionary<string, HashSet<string>> map)
    {
        userId = (userId ?? string.Empty).Trim();
        HashSet<string> selfDirects = GetDirectSet(map, userId);

        int completeLegs = 0;
        foreach (string directId in selfDirects)
        {
            if (GetDirectSet(map, directId).Count >= RequiredDirectsPerLeg)
            {
                completeLegs++;
            }
        }

        bool achieved = selfDirects.Count >= RequiredSelfDirects && completeLegs >= RequiredCompleteLegs;
        return new Progress
        {
            SelfDirects = selfDirects.Count,
            SelfDirectsPending = Math.Max(0, RequiredSelfDirects - selfDirects.Count),
            CompleteLegs = completeLegs,
            CompleteLegsPending = Math.Max(0, RequiredCompleteLegs - completeLegs),
            SelfPercent = Percent(selfDirects.Count, RequiredSelfDirects),
            LegsPercent = Percent(completeLegs, RequiredCompleteLegs),
            IsAchieved = achieved,
            StatusLabel = achieved ? "Achieved" : "Not Achieved"
        };
    }

    public static DataTable GetDirectBreakup(string userId)
    {
        return GetDirectBreakup(userId, LoadQualifyingSales());
    }

    public static DataTable GetDirectBreakup(string userId, DataTable sales)
    {
        userId = (userId ?? string.Empty).Trim();
        DataTable result = CreateDirectTable();
        if (string.IsNullOrWhiteSpace(userId) || sales == null)
        {
            return result;
        }

        Dictionary<string, HashSet<string>> map = BuildDirectMap(sales);
        Dictionary<string, DataRow> firstSale = new Dictionary<string, DataRow>(StringComparer.OrdinalIgnoreCase);

        foreach (DataRow sale in sales.Rows)
        {
            string sponsor = Convert.ToString(sale["Level1SponsorId"]).Trim();
            string buyerId = Convert.ToString(sale["BuyerId"]).Trim();
            if (!string.Equals(sponsor, userId, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(buyerId))
            {
                continue;
            }

            if (!firstSale.ContainsKey(buyerId))
            {
                firstSale[buyerId] = sale;
            }
        }

        foreach (KeyValuePair<string, DataRow> pair in firstSale)
        {
            DataRow sale = pair.Value;
            int under = GetDirectSet(map, pair.Key).Count;
            DataRow row = result.NewRow();
            row["buyerid"] = pair.Key;
            row["buyername"] = sale["BuyerName"];
            row["orderid"] = sale["orderid"];
            row["amount"] = GetDecimal(sale["amount"]);
            row["saledate"] = GetDate(sale["saledate"]);
            row["underdirects"] = under;
            row["underpending"] = Math.Max(0, RequiredDirectsPerLeg - under);
            row["iscounted"] = under >= RequiredDirectsPerLeg;
            result.Rows.Add(row);
        }

        result.DefaultView.Sort = "iscounted DESC, underdirects DESC, buyerid ASC";
        return result.DefaultView.ToTable();
    }

    public static DataTable LoadQualifyingSales()
    {
        SavingProductHelper.EnsureBulkColumns();
        Data ObjData = new Data();
        ObjData.StartConnection();
        try
        {
            try
            {
                return ObjData.RunDataTable(BuildQualifyingSalesSql(true)) ?? new DataTable();
            }
            catch
            {
                return ObjData.RunDataTable(BuildQualifyingSalesSql(false)) ?? new DataTable();
            }
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    public static Dictionary<string, HashSet<string>> BuildDirectMap(DataTable sales)
    {
        var map = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        if (sales == null)
        {
            return map;
        }

        foreach (DataRow sale in sales.Rows)
        {
            string sponsor = Convert.ToString(sale["Level1SponsorId"]).Trim();
            string buyerId = Convert.ToString(sale["BuyerId"]).Trim();
            if (string.IsNullOrWhiteSpace(sponsor) || string.IsNullOrWhiteSpace(buyerId)
                || string.Equals(sponsor, "0", StringComparison.OrdinalIgnoreCase)
                || string.Equals(sponsor, buyerId, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            HashSet<string> children;
            if (!map.TryGetValue(sponsor, out children))
            {
                children = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                map[sponsor] = children;
            }

            children.Add(buyerId);
        }

        return map;
    }

    static HashSet<string> GetDirectSet(Dictionary<string, HashSet<string>> map, string userId)
    {
        HashSet<string> children;
        if (map != null && map.TryGetValue(userId ?? string.Empty, out children))
        {
            return children;
        }

        return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }

    static string BuildQualifyingSalesSql(bool includePlanType)
    {
        string planFilter = includePlanType
            ? @"(LTRIM(RTRIM(ISNULL(sd.PlanType, ''))) = 'Bulk18' OR ISNULL(sd.Amount, 0) >= " + QualifyingSaleAmount.ToString("0") + ")"
            : "ISNULL(sd.Amount, 0) >= " + QualifyingSaleAmount.ToString("0");

        return @"
SELECT
    LTRIM(RTRIM(sd.UserId)) AS BuyerId,
    ISNULL(buyer.UserName, '') AS BuyerName,
    ISNULL(buyer.Mobile, '') AS BuyerMobile,
    LTRIM(RTRIM(ISNULL(buyer.SponserId, ''))) AS Level1SponsorId,
    LTRIM(RTRIM(ISNULL(mid.SponserId, ''))) AS Level2SponsorId,
    sd.orderid,
    sd.amount,
    COALESCE(sd.ApproveDate, sd.EntryDate) AS saledate
FROM SavingAccountDetail sd WITH (NOLOCK)
INNER JOIN UserDetail buyer WITH (NOLOCK)
    ON LTRIM(RTRIM(buyer.UserId)) = LTRIM(RTRIM(sd.UserId))
LEFT JOIN UserDetail mid WITH (NOLOCK)
    ON LTRIM(RTRIM(mid.UserId)) = LTRIM(RTRIM(buyer.SponserId))
WHERE LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('approved', '1', 'active', 'approve')
  AND " + planFilter + @"
  AND CONVERT(date, COALESCE(sd.ApproveDate, sd.EntryDate)) >= CONVERT(date, '" + OfferStartDate.ToString("yyyy-MM-dd") + @"')
  AND CONVERT(date, COALESCE(sd.ApproveDate, sd.EntryDate)) <= CONVERT(date, '" + OfferEndDate.ToString("yyyy-MM-dd") + @"')";
    }

    static DataTable CreateDirectTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("buyerid", typeof(string));
        dt.Columns.Add("buyername", typeof(string));
        dt.Columns.Add("orderid", typeof(string));
        dt.Columns.Add("amount", typeof(decimal));
        dt.Columns.Add("saledate", typeof(DateTime));
        dt.Columns.Add("underdirects", typeof(int));
        dt.Columns.Add("underpending", typeof(int));
        dt.Columns.Add("iscounted", typeof(bool));
        return dt;
    }

    static int Percent(int done, int required)
    {
        if (required <= 0)
        {
            return 100;
        }

        int pct = (int)Math.Round((done * 100.0) / required);
        if (pct < 0)
        {
            return 0;
        }
        return pct > 100 ? 100 : pct;
    }

    static decimal GetDecimal(object value)
    {
        decimal parsed;
        if (value == null || value == DBNull.Value)
        {
            return 0m;
        }
        return decimal.TryParse(Convert.ToString(value), out parsed) ? parsed : 0m;
    }

    static DateTime GetDate(object value)
    {
        DateTime parsed;
        if (value == null || value == DBNull.Value)
        {
            return DateTime.MinValue;
        }
        return DateTime.TryParse(Convert.ToString(value), out parsed) ? parsed : DateTime.MinValue;
    }
}
