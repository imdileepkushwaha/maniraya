using DataTier;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Direct Active Rank based on SavingStatus=1 directs under SponserId.
/// Rules: 0 Member, 1-9 Distributor, 10-24 Bronze, 25-49 Silver, 50-99 Gold, 100+ Diamond.
/// </summary>
public static class DirectRankHelper
{
    public const string RankMember = "Member";
    public const string RankDistributor = "Distributor";
    public const string RankBronze = "Bronze";
    public const string RankSilver = "Silver";
    public const string RankGold = "Gold";
    public const string RankDiamond = "Diamond";

    public class RankTier
    {
        public string Name { get; set; }
        public int MinDirects { get; set; }
        public int? MaxDirects { get; set; }
        public string RequirementLabel { get; set; }
    }

    public class RankProgress
    {
        public int ActiveDirectCount { get; set; }
        public string CurrentRank { get; set; }
        public string NextRank { get; set; }
        public int NextRankMin { get; set; }
        public int RemainingForNext { get; set; }
        public bool IsMaxRank { get; set; }
        public int ProgressPercent { get; set; }
        public string SummaryText { get; set; }
    }

    public class LadderRow
    {
        public string RankName { get; set; }
        public string RequiredLabel { get; set; }
        public int MinDirects { get; set; }
        public string Status { get; set; }
        public int Remaining { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsAchieved { get; set; }
    }

    static readonly RankTier[] Tiers = new[]
    {
        new RankTier { Name = RankDistributor, MinDirects = 1, MaxDirects = 9, RequirementLabel = "1 – 9 Active Directs" },
        new RankTier { Name = RankBronze, MinDirects = 10, MaxDirects = 24, RequirementLabel = "10 – 24 Active Directs" },
        new RankTier { Name = RankSilver, MinDirects = 25, MaxDirects = 49, RequirementLabel = "25 – 49 Active Directs" },
        new RankTier { Name = RankGold, MinDirects = 50, MaxDirects = 99, RequirementLabel = "50 – 99 Active Directs" },
        new RankTier { Name = RankDiamond, MinDirects = 100, MaxDirects = null, RequirementLabel = "100+ Active Directs" }
    };

    public static IList<RankTier> GetAllTiers()
    {
        return Tiers;
    }

    public static string GetRank(int activeDirects)
    {
        if (activeDirects <= 0)
        {
            return RankMember;
        }
        if (activeDirects >= 100)
        {
            return RankDiamond;
        }
        if (activeDirects >= 50)
        {
            return RankGold;
        }
        if (activeDirects >= 25)
        {
            return RankSilver;
        }
        if (activeDirects >= 10)
        {
            return RankBronze;
        }
        return RankDistributor;
    }

    public static RankProgress GetProgress(int activeDirects)
    {
        if (activeDirects < 0)
        {
            activeDirects = 0;
        }

        string current = GetRank(activeDirects);
        RankProgress progress = new RankProgress
        {
            ActiveDirectCount = activeDirects,
            CurrentRank = current
        };

        RankTier nextTier = null;
        foreach (RankTier tier in Tiers)
        {
            if (activeDirects < tier.MinDirects)
            {
                nextTier = tier;
                break;
            }
        }

        if (nextTier == null)
        {
            progress.IsMaxRank = true;
            progress.NextRank = string.Empty;
            progress.NextRankMin = 100;
            progress.RemainingForNext = 0;
            progress.ProgressPercent = 100;
            progress.SummaryText = "You have achieved the highest rank: Diamond.";
            return progress;
        }

        progress.IsMaxRank = false;
        progress.NextRank = nextTier.Name;
        progress.NextRankMin = nextTier.MinDirects;
        progress.RemainingForNext = Math.Max(0, nextTier.MinDirects - activeDirects);

        int prevMin = 0;
        foreach (RankTier tier in Tiers)
        {
            if (tier.Name == nextTier.Name)
            {
                break;
            }
            prevMin = tier.MinDirects;
        }
        if (current == RankMember)
        {
            prevMin = 0;
        }

        int span = Math.Max(1, nextTier.MinDirects - prevMin);
        int done = Math.Max(0, activeDirects - prevMin);
        progress.ProgressPercent = Math.Min(100, (int)Math.Round((done * 100.0) / span));

        progress.SummaryText = progress.RemainingForNext == 1
            ? "1 more Active Direct needed for " + progress.NextRank + "."
            : progress.RemainingForNext + " more Active Directs needed for " + progress.NextRank + ".";

        return progress;
    }

    public static RankProgress GetProgressForUser(string userId)
    {
        return GetProgress(GetActiveDirectCount(userId));
    }

    public static IList<LadderRow> GetLadder(int activeDirects)
    {
        string current = GetRank(activeDirects);
        List<LadderRow> rows = new List<LadderRow>();

        // Member row for clarity when count is 0
        rows.Add(new LadderRow
        {
            RankName = RankMember,
            RequiredLabel = "0 Active Directs",
            MinDirects = 0,
            IsCurrent = current == RankMember,
            IsAchieved = activeDirects >= 0,
            Remaining = 0,
            Status = current == RankMember ? "Current" : "Achieved"
        });

        foreach (RankTier tier in Tiers)
        {
            bool achieved = activeDirects >= tier.MinDirects;
            bool isCurrent = current == tier.Name;
            int remaining = achieved ? 0 : Math.Max(0, tier.MinDirects - activeDirects);

            string status;
            if (isCurrent)
            {
                status = "Current";
            }
            else if (achieved)
            {
                status = "Achieved";
            }
            else
            {
                status = "Locked";
            }

            rows.Add(new LadderRow
            {
                RankName = tier.Name,
                RequiredLabel = tier.RequirementLabel,
                MinDirects = tier.MinDirects,
                IsCurrent = isCurrent,
                IsAchieved = achieved,
                Remaining = remaining,
                Status = status
            });
        }

        return rows;
    }

    public static int GetActiveDirectCount(string userId)
    {
        string uid = (userId ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(uid))
        {
            return 0;
        }

        string sql = @"
SELECT COUNT(1) AS ActiveDirectCount
FROM UserDetail d WITH (NOLOCK)
WHERE LTRIM(RTRIM(d.SponserId)) = '" + SqlEscape(uid) + @"'
  AND ISNULL(d.SavingStatus, 0) = 1";

        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                DataTable dt = objData.RunDataTable(sql);
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            return 0;
        }

        return 0;
    }

    public static string GetCurrentMonthLabel()
    {
        return DateTime.Now.ToString("MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Top ranking for the current month by Active Renewals Direct
    /// (approved renewal installment InstNo &gt; 1 in this calendar month).
    /// Highest monthly renewals appear first.
    /// </summary>
    public static DataTable GetTopDirectRanking(int topCount)
    {
        if (topCount <= 0)
        {
            topCount = 10;
        }

        string sql = @"
;WITH RenewedDirects AS (
    SELECT DISTINCT LTRIM(RTRIM(sa.UserId)) AS UserId
    FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
    WHERE ISNULL(sa.InstNo, 0) > 1
      AND " + ApprovedInstallmentStatusFilter("sa") + @"
      AND " + CurrentMonthRenewalDateFilter("sa") + @"
),
Ranked AS (
    SELECT
        LTRIM(RTRIM(s.UserId)) AS userid,
        ISNULL(s.UserName, '') AS username,
        COUNT(d.UserId) AS TotalDirectCount,
        SUM(CASE WHEN ISNULL(d.SavingStatus, 0) = 1 THEN 1 ELSE 0 END) AS ActiveDirectCount,
        SUM(CASE WHEN rd.UserId IS NOT NULL THEN 1 ELSE 0 END) AS ActiveRenewalDirectCount
    FROM UserDetail d WITH (NOLOCK)
    INNER JOIN UserDetail s WITH (NOLOCK)
        ON LTRIM(RTRIM(d.SponserId)) = LTRIM(RTRIM(s.UserId))
    LEFT JOIN RenewedDirects rd
        ON rd.UserId = LTRIM(RTRIM(d.UserId))
    WHERE NULLIF(LTRIM(RTRIM(d.SponserId)), '') IS NOT NULL
      AND ISNULL(s.ActiveStatus, 0) = 1
    GROUP BY LTRIM(RTRIM(s.UserId)), s.UserName
    HAVING SUM(CASE WHEN rd.UserId IS NOT NULL THEN 1 ELSE 0 END) > 0
)
SELECT TOP (" + topCount + @")
    ROW_NUMBER() OVER (ORDER BY ActiveRenewalDirectCount DESC, ActiveDirectCount DESC, TotalDirectCount DESC, userid ASC) AS RankNo,
    userid,
    username,
    TotalDirectCount,
    ActiveDirectCount,
    ActiveRenewalDirectCount,
    ActiveDirectCount AS DirectCount
FROM Ranked
ORDER BY ActiveRenewalDirectCount DESC, ActiveDirectCount DESC, TotalDirectCount DESC, userid ASC";

        return AppendDirectRankColumn(RunTable(sql));
    }

    public static DataTable GetUserMonthlyRank(string userId)
    {
        string uid = (userId ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(uid))
        {
            return new DataTable();
        }

        string sql = @"
;WITH RenewedDirects AS (
    SELECT DISTINCT LTRIM(RTRIM(sa.UserId)) AS UserId
    FROM SavingAccountInstallmentDetail sa WITH (NOLOCK)
    WHERE ISNULL(sa.InstNo, 0) > 1
      AND " + ApprovedInstallmentStatusFilter("sa") + @"
      AND " + CurrentMonthRenewalDateFilter("sa") + @"
),
Ranked AS (
    SELECT
        LTRIM(RTRIM(s.UserId)) AS userid,
        COUNT(d.UserId) AS TotalDirectCount,
        SUM(CASE WHEN ISNULL(d.SavingStatus, 0) = 1 THEN 1 ELSE 0 END) AS ActiveDirectCount,
        SUM(CASE WHEN rd.UserId IS NOT NULL THEN 1 ELSE 0 END) AS ActiveRenewalDirectCount,
        ROW_NUMBER() OVER (
            ORDER BY SUM(CASE WHEN rd.UserId IS NOT NULL THEN 1 ELSE 0 END) DESC,
                     SUM(CASE WHEN ISNULL(d.SavingStatus, 0) = 1 THEN 1 ELSE 0 END) DESC,
                     COUNT(d.UserId) DESC,
                     LTRIM(RTRIM(s.UserId)) ASC
        ) AS RankNo
    FROM UserDetail d WITH (NOLOCK)
    INNER JOIN UserDetail s WITH (NOLOCK)
        ON LTRIM(RTRIM(d.SponserId)) = LTRIM(RTRIM(s.UserId))
    LEFT JOIN RenewedDirects rd
        ON rd.UserId = LTRIM(RTRIM(d.UserId))
    WHERE NULLIF(LTRIM(RTRIM(d.SponserId)), '') IS NOT NULL
      AND ISNULL(s.ActiveStatus, 0) = 1
    GROUP BY LTRIM(RTRIM(s.UserId))
    HAVING SUM(CASE WHEN rd.UserId IS NOT NULL THEN 1 ELSE 0 END) > 0
)
SELECT RankNo, TotalDirectCount, ActiveDirectCount, ActiveRenewalDirectCount
FROM Ranked
WHERE userid = '" + SqlEscape(uid) + "'";

        return RunTable(sql);
    }

    static string ApprovedInstallmentStatusFilter(string alias)
    {
        return "(" + alias + ".status = 'Approved'"
            + " OR " + alias + ".status = '1'"
            + " OR LOWER(LTRIM(RTRIM(ISNULL(" + alias + ".status, '')))) IN ('approved', 'approve'))";
    }

    static string CurrentMonthRenewalDateFilter(string alias)
    {
        return "YEAR(CONVERT(date, ISNULL(" + alias + ".approvedate, " + alias + @".installmentdate))) = YEAR(GETDATE())
      AND MONTH(CONVERT(date, ISNULL(" + alias + ".approvedate, " + alias + ".installmentdate))) = MONTH(GETDATE())";
    }

    static DataTable AppendDirectRankColumn(DataTable dt)
    {
        if (dt == null)
        {
            return new DataTable();
        }

        if (!dt.Columns.Contains("DirectRank"))
        {
            dt.Columns.Add("DirectRank", typeof(string));
        }

        foreach (DataRow row in dt.Rows)
        {
            int directCount = 0;
            string countColumn = dt.Columns.Contains("ActiveDirectCount") ? "ActiveDirectCount" : "DirectCount";
            if (row[countColumn] != null && row[countColumn] != DBNull.Value)
            {
                int.TryParse(Convert.ToString(row[countColumn]), out directCount);
            }
            row["DirectRank"] = GetRank(directCount);
        }

        return dt;
    }

    static DataTable RunTable(string sql)
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                return objData.RunDataTable(sql) ?? new DataTable();
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            return new DataTable();
        }
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}
