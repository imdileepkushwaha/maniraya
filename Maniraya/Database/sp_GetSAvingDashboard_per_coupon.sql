-- Per-coupon saving dashboard metrics (UserId + CouponCode / OrderId).
-- Fixes shared OrderId: installments are matched by approval batch, not OrderId alone.

IF OBJECT_ID('sp_GetSAvingDashboard', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetSAvingDashboard;
GO

CREATE PROCEDURE sp_GetSAvingDashboard
    @userid NVARCHAR(100),
    @CouponCode NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH UserAccounts AS (
        SELECT sd.*,
            CASE
                WHEN LOWER(LTRIM(RTRIM(ISNULL(sd.status, '')))) IN ('approved', '1', 'active')
                THEN ROW_NUMBER() OVER (PARTITION BY sd.orderid, sd.userid ORDER BY sd.id)
                ELSE NULL
            END AS AcctSeq
        FROM SavingAccountDetail sd WITH (NOLOCK)
        WHERE sd.userid = @userid
    ),
    InstallmentBatches AS (
        SELECT ld.*,
            ((ROW_NUMBER() OVER (PARTITION BY ld.orderid, ld.userid ORDER BY ld.Id) - 1) / 18) + 1 AS BatchSeq
        FROM SavingAccountInstallmentDetail ld WITH (NOLOCK)
        WHERE ld.userid = @userid
    )
    SELECT
        ua.CouponCode,
        UPPER(ua.OrderId) AS OrderId,
        ISNULL((
            SELECT SUM(ld.amount)
            FROM SavingLevelIncomeDetail ld WITH (NOLOCK)
            WHERE ld.userid = ua.userid
        ), 0) AS levelincome,
        COUNT(ib.Id) AS totalemi,
        SUM(CASE WHEN ib.Status = 'Approved' THEN 1 ELSE 0 END) AS paidemi,
        SUM(CASE WHEN ib.Status = 'Pending' THEN 1 ELSE 0 END) AS pendingemi,
        CASE
            WHEN LOWER(LTRIM(RTRIM(ISNULL(ua.Status, '')))) IN ('rejected', '2', 'cancelled', 'canceled', 'deactive', 'inactive')
            THEN 0
            ELSE ISNULL(SUM(ib.Amount), 0) + 2000
        END AS maturityamount,
        ua.ApproveDate AS approvedate,
        ua.EntryDate AS entrydate,
        ua.MaturityDate AS maturitydate
    FROM UserAccounts ua
    LEFT JOIN InstallmentBatches ib
        ON ib.orderid = ua.orderid
        AND ib.userid = ua.userid
        AND ua.AcctSeq IS NOT NULL
        AND ib.BatchSeq = ua.AcctSeq
    WHERE (@CouponCode IS NULL OR @CouponCode = ''
        OR ua.CouponCode = @CouponCode
        OR ua.OrderId = @CouponCode
        OR UPPER(ua.OrderId) = UPPER(@CouponCode))
    GROUP BY
        ua.Id,
        ua.CouponCode,
        ua.OrderId,
        ua.userid,
        ua.ApproveDate,
        ua.EntryDate,
        ua.MaturityDate
    ORDER BY ua.Id DESC;
END
GO
