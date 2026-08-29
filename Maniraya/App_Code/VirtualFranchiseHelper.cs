using DataTier;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public static class VirtualFranchiseHelper
{
    const int SchemaVersion = 1;
    static int AppliedSchemaVersion;

    public static void EnsureSchema()
    {
        if (AppliedSchemaVersion == SchemaVersion)
        {
            return;
        }

        CreateTables();
        SeedMasters();
        RecreateProcedure("sp_add_VirtualFranchiseRequest", GetAddRequestProcSql());
        RecreateProcedure("sp_reject_VirtualFranchiseRequest", GetRejectRequestProcSql());
        RecreateProcedure("sp_approve_VirtualFranchiseRequest", GetApproveRequestProcSql());
        RecreateProcedure("sp_processVirtualFranchiseROI", GetProcessRoiProcSql());

        if (HasTable("Virtual_Franchise_Plan_Master")
            && HasTable("Virtual_Level_Master")
            && HasTable("Virtual_Franchise_Request")
            && ProcedureExists("sp_add_VirtualFranchiseRequest")
            && ProcedureExists("sp_approve_VirtualFranchiseRequest"))
        {
            AppliedSchemaVersion = SchemaVersion;
        }
    }

    public static void ProcessDueRoi()
    {
        try
        {
            EnsureSchema();
            ExecuteScalarProc("sp_processVirtualFranchiseROI", new SqlParameter[0]);
        }
        catch
        {
        }
    }

    public static DataTable GetActivePlans()
    {
        EnsureSchema();
        return RunSelect(@"
SELECT id, plan_planname, plan_amount, roi, directroi, roitime, status, createdate
FROM Virtual_Franchise_Plan_Master WITH (NOLOCK)
WHERE UPPER(LTRIM(RTRIM(ISNULL(status, '')))) = 'ACTIVE'
ORDER BY plan_amount");
    }

    public static bool IsOnlineTransactionIdUsed(string onlineTransactionId)
    {
        string utr = (onlineTransactionId ?? string.Empty).Trim();
        if (utr.Length == 0 || utr.StartsWith("CASH-", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        EnsureSchema();
        string escaped = Escape(utr);
        DataTable dt = RunSelect(@"
SELECT CASE WHEN EXISTS (
    SELECT 1 FROM Virtual_Franchise_Request WITH (NOLOCK)
    WHERE UPPER(LTRIM(RTRIM(ISNULL(onlinetransactionid, '')))) = UPPER('" + escaped + @"')
      AND UPPER(LTRIM(RTRIM(ISNULL(status, '')))) <> 'REJECTED'
) OR EXISTS (
    SELECT 1 FROM SavingAccountDetail WITH (NOLOCK)
    WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER('" + escaped + @"')
      AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED'
) THEN 1 ELSE 0 END");

        if (dt == null || dt.Rows.Count == 0 || dt.Rows[0][0] == DBNull.Value)
        {
            return false;
        }

        return Convert.ToInt32(dt.Rows[0][0]) == 1;
    }

    public static string ExecuteScalarProc(string procName, SqlParameter[] parameters)
    {
        Data objData = new Data();
        string res = "0";
        SqlConnection cn = null;
        SqlTransaction tr = null;
        try
        {
            cn = objData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            res = objData.RunInsUpDelQueryTransProcScalar(procName, tr, parameters ?? new SqlParameter[0]);
            tr.Commit();
        }
        catch
        {
            res = "0";
            if (tr != null)
            {
                try { tr.Rollback(); }
                catch { }
            }
        }
        finally
        {
            try { objData.EndConnection(); }
            catch { }
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        return string.IsNullOrWhiteSpace(res) ? "0" : res.Trim();
    }

    static void CreateTables()
    {
        RunNonQuery(@"
IF OBJECT_ID('dbo.Virtual_Franchise_Plan_Master', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Virtual_Franchise_Plan_Master
    (
        id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        plan_planname NVARCHAR(100) NOT NULL,
        plan_amount DECIMAL(18,2) NOT NULL,
        roi DECIMAL(18,2) NOT NULL,
        directroi DECIMAL(18,2) NOT NULL CONSTRAINT DF_VFPlan_DirectRoi DEFAULT (0),
        createdate DATETIME NOT NULL CONSTRAINT DF_VFPlan_CreateDate DEFAULT (GETDATE()),
        roitime INT NOT NULL CONSTRAINT DF_VFPlan_RoiTime DEFAULT (40),
        status NVARCHAR(50) NOT NULL CONSTRAINT DF_VFPlan_Status DEFAULT ('Active')
    );
END");

        RunNonQuery(@"
IF OBJECT_ID('dbo.Virtual_Level_Master', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Virtual_Level_Master
    (
        id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        levelno INT NOT NULL,
        incomeper DECIMAL(18,2) NOT NULL,
        status NVARCHAR(50) NOT NULL CONSTRAINT DF_VFLevel_Status DEFAULT ('Active'),
        createdate DATETIME NOT NULL CONSTRAINT DF_VFLevel_CreateDate DEFAULT (GETDATE())
    );
END");

        RunNonQuery(@"
IF OBJECT_ID('dbo.Virtual_Franchise_Request', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Virtual_Franchise_Request
    (
        id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        userid NVARCHAR(100) NOT NULL,
        planid INT NOT NULL,
        planname NVARCHAR(100) NULL,
        planamount DECIMAL(18,2) NOT NULL,
        roi DECIMAL(18,2) NULL,
        roitime INT NULL,
        monthlyroi DECIMAL(18,2) NULL,
        totalcashback DECIMAL(18,2) NULL,
        paymentmethod NVARCHAR(50) NULL,
        onlinetransactionid NVARCHAR(100) NULL,
        imagename NVARCHAR(MAX) NULL,
        status NVARCHAR(50) NOT NULL CONSTRAINT DF_VFReq_Status DEFAULT ('Pending'),
        remark NVARCHAR(MAX) NULL,
        entryby NVARCHAR(100) NULL,
        entrydate DATETIME NOT NULL CONSTRAINT DF_VFReq_EntryDate DEFAULT (GETDATE()),
        approveby NVARCHAR(100) NULL,
        approvedate DATETIME NULL
    );
END");

        RunNonQuery(@"
IF OBJECT_ID('dbo.Virtual_Franchise_ROI_Detail', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Virtual_Franchise_ROI_Detail
    (
        id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        requestid INT NOT NULL,
        userid NVARCHAR(100) NOT NULL,
        planname NVARCHAR(100) NULL,
        monthno INT NOT NULL,
        roidate DATETIME NOT NULL,
        roiamount DECIMAL(18,2) NOT NULL,
        status NVARCHAR(50) NOT NULL CONSTRAINT DF_VFRoi_Status DEFAULT ('Pending'),
        paiddate DATETIME NULL,
        transactionid INT NULL,
        mentiondate DATETIME NOT NULL CONSTRAINT DF_VFRoi_MentionDate DEFAULT (GETDATE())
    );
END");

        RunNonQuery(@"
IF OBJECT_ID('dbo.Virtual_Level_Income_Detail', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Virtual_Level_Income_Detail
    (
        id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        requestid INT NOT NULL,
        userid NVARCHAR(100) NOT NULL,
        fromuserid NVARCHAR(100) NULL,
        levelno INT NOT NULL,
        planname NVARCHAR(100) NULL,
        planamount DECIMAL(18,2) NULL,
        incomeper DECIMAL(18,2) NULL,
        income DECIMAL(18,2) NOT NULL,
        adminper DECIMAL(18,2) NULL,
        admincharge DECIMAL(18,2) NULL,
        tdsper DECIMAL(18,2) NULL,
        tdscharge DECIMAL(18,2) NULL,
        payableamount DECIMAL(18,2) NOT NULL,
        transactionid INT NULL,
        status NVARCHAR(50) NOT NULL CONSTRAINT DF_VFInc_Status DEFAULT ('Paid'),
        mentiondate DATETIME NOT NULL CONSTRAINT DF_VFInc_MentionDate DEFAULT (GETDATE())
    );
END");
    }

    static void SeedMasters()
    {
        RunNonQuery(@"
IF NOT EXISTS (SELECT 1 FROM dbo.Virtual_Franchise_Plan_Master WITH (NOLOCK) WHERE plan_amount = 100000 AND plan_planname = N'Basic Tier')
    INSERT INTO dbo.Virtual_Franchise_Plan_Master (plan_planname, plan_amount, roi, directroi, createdate, roitime, status)
    VALUES (N'Basic Tier', 100000, 5, 0, '2026-08-27', 40, N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.Virtual_Franchise_Plan_Master WITH (NOLOCK) WHERE plan_amount = 500000 AND plan_planname = N'Premium Tier')
    INSERT INTO dbo.Virtual_Franchise_Plan_Master (plan_planname, plan_amount, roi, directroi, createdate, roitime, status)
    VALUES (N'Premium Tier', 500000, 5, 0, '2026-08-27', 40, N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.Virtual_Franchise_Plan_Master WITH (NOLOCK) WHERE plan_amount = 1000000 AND plan_planname = N'Executive Tier')
    INSERT INTO dbo.Virtual_Franchise_Plan_Master (plan_planname, plan_amount, roi, directroi, createdate, roitime, status)
    VALUES (N'Executive Tier', 1000000, 5, 0, '2026-08-27', 40, N'Active');");

        RunNonQuery(@"
IF NOT EXISTS (SELECT 1 FROM dbo.Virtual_Level_Master WITH (NOLOCK) WHERE levelno = 1)
    INSERT INTO dbo.Virtual_Level_Master (levelno, incomeper, status, createdate) VALUES (1, 5, N'Active', GETDATE());
IF NOT EXISTS (SELECT 1 FROM dbo.Virtual_Level_Master WITH (NOLOCK) WHERE levelno = 2)
    INSERT INTO dbo.Virtual_Level_Master (levelno, incomeper, status, createdate) VALUES (2, 2, N'Active', GETDATE());
IF NOT EXISTS (SELECT 1 FROM dbo.Virtual_Level_Master WITH (NOLOCK) WHERE levelno = 3)
    INSERT INTO dbo.Virtual_Level_Master (levelno, incomeper, status, createdate) VALUES (3, 3, N'Active', GETDATE());
IF NOT EXISTS (SELECT 1 FROM dbo.Virtual_Level_Master WITH (NOLOCK) WHERE levelno = 4)
    INSERT INTO dbo.Virtual_Level_Master (levelno, incomeper, status, createdate) VALUES (4, 4, N'Active', GETDATE());
IF NOT EXISTS (SELECT 1 FROM dbo.Virtual_Level_Master WITH (NOLOCK) WHERE levelno = 5)
    INSERT INTO dbo.Virtual_Level_Master (levelno, incomeper, status, createdate) VALUES (5, 5, N'Active', GETDATE());");
    }

    static string GetAddRequestProcSql()
    {
        return @"
CREATE PROC dbo.sp_add_VirtualFranchiseRequest
    @UserId NVARCHAR(100),
    @PlanId INT,
    @PaymentMethod NVARCHAR(50),
    @OnlineTransactionId NVARCHAR(100),
    @ImageName NVARCHAR(MAX),
    @EntryBy NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @user NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@UserId, '')));
    DECLARE @method NVARCHAR(50) = LTRIM(RTRIM(ISNULL(@PaymentMethod, 'Online')));
    DECLARE @utr NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@OnlineTransactionId, '')));
    DECLARE @image NVARCHAR(MAX) = LTRIM(RTRIM(ISNULL(@ImageName, '')));
    DECLARE @by NVARCHAR(100) = LTRIM(RTRIM(ISNULL(@EntryBy, @user)));
    IF (@user = '' OR ISNULL(@PlanId, 0) <= 0) BEGIN SELECT 'n'; RETURN; END
    IF NOT EXISTS (SELECT 1 FROM UserDetail WITH (NOLOCK) WHERE LTRIM(RTRIM(UserId)) = @user) BEGIN SELECT 'n'; RETURN; END
    DECLARE @planName NVARCHAR(100), @planAmount DECIMAL(18,2), @roi DECIMAL(18,2), @roiTime INT;
    SELECT @planName = plan_planname, @planAmount = plan_amount, @roi = roi, @roiTime = roitime
    FROM Virtual_Franchise_Plan_Master WITH (NOLOCK)
    WHERE id = @PlanId AND UPPER(LTRIM(RTRIM(ISNULL(status, '')))) = 'ACTIVE';
    IF (@planName IS NULL OR ISNULL(@planAmount, 0) <= 0) BEGIN SELECT 'n'; RETURN; END
    IF (
        @utr <> '' AND @utr NOT LIKE 'CASH-%'
        AND (
            EXISTS (SELECT 1 FROM Virtual_Franchise_Request WITH (NOLOCK) WHERE UPPER(LTRIM(RTRIM(ISNULL(onlinetransactionid, '')))) = UPPER(@utr) AND UPPER(LTRIM(RTRIM(ISNULL(status, '')))) <> 'REJECTED')
            OR (
                OBJECT_ID('dbo.SavingAccountDetail', 'U') IS NOT NULL
                AND EXISTS (SELECT 1 FROM SavingAccountDetail WITH (NOLOCK) WHERE UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId, '')))) = UPPER(@utr) AND UPPER(LTRIM(RTRIM(ISNULL(Status, '')))) <> 'REJECTED')
            )
        )
    ) BEGIN SELECT 'u'; RETURN; END
    IF EXISTS (SELECT 1 FROM Virtual_Franchise_Request WITH (NOLOCK) WHERE LTRIM(RTRIM(userid)) = @user AND UPPER(LTRIM(RTRIM(ISNULL(status, '')))) = 'PENDING')
    BEGIN SELECT 'f'; RETURN; END
    DECLARE @monthly DECIMAL(18,2) = ROUND(@planAmount * ISNULL(@roi, 0) / 100.0, 2);
    DECLARE @total DECIMAL(18,2) = ROUND(@monthly * ISNULL(@roiTime, 40), 2);
    INSERT INTO Virtual_Franchise_Request (
        userid, planid, planname, planamount, roi, roitime, monthlyroi, totalcashback,
        paymentmethod, onlinetransactionid, imagename, status, entryby, entrydate
    )
    VALUES (
        @user, @PlanId, @planName, @planAmount, @roi, @roiTime, @monthly, @total,
        @method, @utr, @image, 'Pending', @by, GETDATE()
    );
    SELECT 't';
END";
    }

    static string GetRejectRequestProcSql()
    {
        return @"
CREATE PROC dbo.sp_reject_VirtualFranchiseRequest
    @id INT,
    @Approveby NVARCHAR(100),
    @Remark NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @status NVARCHAR(50);
    SELECT @status = status FROM Virtual_Franchise_Request WHERE id = @id;
    IF (@status IS NULL) BEGIN SELECT 'n'; RETURN; END
    IF (UPPER(LTRIM(RTRIM(ISNULL(@status, '')))) <> 'PENDING') BEGIN SELECT 'f'; RETURN; END
    IF (LTRIM(RTRIM(ISNULL(@Remark, ''))) = '') BEGIN SELECT 'r'; RETURN; END
    UPDATE Virtual_Franchise_Request
    SET status = 'Rejected', remark = @Remark, approveby = @Approveby, approvedate = GETDATE()
    WHERE id = @id;
    SELECT 't';
END";
    }

    static string GetApproveRequestProcSql()
    {
        return @"
CREATE PROC dbo.sp_approve_VirtualFranchiseRequest
    @id INT,
    @Approveby NVARCHAR(100),
    @Remark NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @user NVARCHAR(100), @planName NVARCHAR(100), @planAmount DECIMAL(18,2), @roi DECIMAL(18,2), @roiTime INT, @monthly DECIMAL(18,2), @status NVARCHAR(50);
    SELECT @user = LTRIM(RTRIM(userid)), @planName = planname, @planAmount = planamount, @roi = roi,
           @roiTime = ISNULL(roitime, 40), @monthly = monthlyroi, @status = status
    FROM Virtual_Franchise_Request WHERE id = @id;
    IF (@user IS NULL) BEGIN SELECT 'n'; RETURN; END
    IF (UPPER(LTRIM(RTRIM(ISNULL(@status, '')))) <> 'PENDING') BEGIN SELECT 'f'; RETURN; END
    IF (ISNULL(@monthly, 0) <= 0) SET @monthly = ROUND(ISNULL(@planAmount, 0) * ISNULL(@roi, 0) / 100.0, 2);
    UPDATE Virtual_Franchise_Request
    SET status = 'Approved', remark = @Remark, approveby = @Approveby, approvedate = GETDATE()
    WHERE id = @id;
    IF NOT EXISTS (SELECT 1 FROM Virtual_Franchise_ROI_Detail WITH (NOLOCK) WHERE requestid = @id)
    BEGIN
        DECLARE @i INT = 1;
        WHILE (@i <= @roiTime)
        BEGIN
            INSERT INTO Virtual_Franchise_ROI_Detail (requestid, userid, planname, monthno, roidate, roiamount, status, mentiondate)
            VALUES (@id, @user, @planName, @i, DATEADD(MONTH, @i, GETDATE()), @monthly, 'Pending', GETDATE());
            SET @i = @i + 1;
        END
    END
    DECLARE @sponsor NVARCHAR(100) = NULL;
    SELECT @sponsor = LTRIM(RTRIM(ISNULL(SponserId, ''))) FROM UserDetail WITH (NOLOCK) WHERE LTRIM(RTRIM(UserId)) = @user;
    DECLARE @levelPer DECIMAL(18,2) = 0;
    SELECT @levelPer = incomeper FROM Virtual_Level_Master WITH (NOLOCK)
    WHERE levelno = 1 AND UPPER(LTRIM(RTRIM(ISNULL(status, '')))) = 'ACTIVE';
    IF (ISNULL(@sponsor, '') <> '' AND @sponsor NOT IN ('0', 'admin', 'Admin') AND ISNULL(@levelPer, 0) > 0
        AND NOT EXISTS (SELECT 1 FROM Virtual_Level_Income_Detail WITH (NOLOCK) WHERE requestid = @id AND levelno = 1))
    BEGIN
        DECLARE @income DECIMAL(18,2) = ROUND(@planAmount * @levelPer / 100.0, 2);
        DECLARE @adminPer DECIMAL(18,2) = 10;
        DECLARE @tdsPer DECIMAL(18,2) = 2;
        DECLARE @adminCharge DECIMAL(18,2) = ROUND(@income * @adminPer / 100.0, 2);
        DECLARE @tdsCharge DECIMAL(18,2) = ROUND(@income * @tdsPer / 100.0, 2);
        DECLARE @payable DECIMAL(18,2) = ROUND(@income - @adminCharge - @tdsCharge, 2);
        DECLARE @oldBal DECIMAL(18,2) = 0, @newBal DECIMAL(18,2) = 0, @txnId INT = 0;
        SELECT @oldBal = ISNULL(BalanceAmount, 0) FROM UserDetail WITH (NOLOCK) WHERE LTRIM(RTRIM(UserId)) = @sponsor;
        SET @newBal = @oldBal + @payable;
        IF OBJECT_ID('dbo.TransactionDetail', 'U') IS NOT NULL
        BEGIN
            BEGIN TRY
                SELECT @txnId = ISNULL(MAX(transactionid), 0) + 1 FROM TransactionDetail;
                INSERT INTO TransactionDetail (transactionid, cramount, dramount, oldBalance, currentBalance, userid, transactiontype, remark, mentionby, mentiondate, type)
                VALUES (@txnId, @payable, 0, @oldBal, @newBal, @sponsor, N'Virtual Level Income',
                    N'Level 1 income from ' + @user + N' | Plan ' + ISNULL(@planName, '') +
                    N' | Gross ' + CONVERT(NVARCHAR(40), @income) + N' | Admin 10% ' + CONVERT(NVARCHAR(40), @adminCharge) +
                    N' | TDS 2% ' + CONVERT(NVARCHAR(40), @tdsCharge) + N' | Net ' + CONVERT(NVARCHAR(40), @payable),
                    ISNULL(@Approveby, 'admin'), GETDATE(), 1);
            END TRY
            BEGIN CATCH
                SET @txnId = 0;
            END CATCH
        END
        BEGIN TRY
            UPDATE UserDetail SET BalanceAmount = ISNULL(BalanceAmount, 0) + @payable WHERE LTRIM(RTRIM(UserId)) = @sponsor;
        END TRY
        BEGIN CATCH
        END CATCH
        INSERT INTO Virtual_Level_Income_Detail (
            requestid, userid, fromuserid, levelno, planname, planamount, incomeper, income,
            adminper, admincharge, tdsper, tdscharge, payableamount, transactionid, status, mentiondate)
        VALUES (
            @id, @sponsor, @user, 1, @planName, @planAmount, @levelPer, @income,
            @adminPer, @adminCharge, @tdsPer, @tdsCharge, @payable, NULLIF(@txnId, 0), 'Paid', GETDATE());
    END
    SELECT 't';
END";
    }

    static string GetProcessRoiProcSql()
    {
        return @"
CREATE PROC dbo.sp_processVirtualFranchiseROI
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @id INT, @user NVARCHAR(100), @amount DECIMAL(18,2), @planName NVARCHAR(100), @monthNo INT, @requestId INT;
    DECLARE @oldBal DECIMAL(18,2), @newBal DECIMAL(18,2), @txnId INT;
    DECLARE due CURSOR LOCAL FAST_FORWARD FOR
        SELECT id, userid, roiamount, planname, monthno, requestid
        FROM Virtual_Franchise_ROI_Detail
        WHERE UPPER(LTRIM(RTRIM(ISNULL(status, '')))) = 'PENDING'
          AND CONVERT(date, roidate) <= CONVERT(date, GETDATE());
    OPEN due;
    FETCH NEXT FROM due INTO @id, @user, @amount, @planName, @monthNo, @requestId;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @oldBal = 0; SET @newBal = 0; SET @txnId = 0;
        SELECT @oldBal = ISNULL(BalanceAmount, 0) FROM UserDetail WITH (NOLOCK) WHERE LTRIM(RTRIM(UserId)) = LTRIM(RTRIM(@user));
        SET @newBal = @oldBal + ISNULL(@amount, 0);
        IF OBJECT_ID('dbo.TransactionDetail', 'U') IS NOT NULL AND ISNULL(@amount, 0) > 0
        BEGIN
            BEGIN TRY
                SELECT @txnId = ISNULL(MAX(transactionid), 0) + 1 FROM TransactionDetail;
                INSERT INTO TransactionDetail (transactionid, cramount, dramount, oldBalance, currentBalance, userid, transactiontype, remark, mentionby, mentiondate, type)
                VALUES (@txnId, @amount, 0, @oldBal, @newBal, @user, N'Virtual Franchise ROI',
                    N'Month ' + CONVERT(NVARCHAR(10), @monthNo) + N' ROI for ' + ISNULL(@planName, N'Virtual Franchise') +
                    N' | Request #' + CONVERT(NVARCHAR(20), @requestId), N'SYSTEM-VF', GETDATE(), 1);
            END TRY
            BEGIN CATCH
                SET @txnId = 0;
            END CATCH
        END
        BEGIN TRY
            UPDATE UserDetail SET BalanceAmount = ISNULL(BalanceAmount, 0) + ISNULL(@amount, 0)
            WHERE LTRIM(RTRIM(UserId)) = LTRIM(RTRIM(@user));
        END TRY
        BEGIN CATCH
        END CATCH
        UPDATE Virtual_Franchise_ROI_Detail SET status = 'Paid', paiddate = GETDATE(), transactionid = NULLIF(@txnId, 0) WHERE id = @id;
        FETCH NEXT FROM due INTO @id, @user, @amount, @planName, @monthNo, @requestId;
    END
    CLOSE due;
    DEALLOCATE due;
    SELECT 't';
END";
    }

    static bool HasTable(string tableName)
    {
        DataTable dt = RunSelect("SELECT OBJECT_ID('dbo." + Escape(tableName) + "', 'U') AS ObjId");
        return dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ObjId"] != DBNull.Value;
    }

    static bool ProcedureExists(string procName)
    {
        DataTable dt = RunSelect("SELECT OBJECT_ID('dbo." + Escape(procName) + "', 'P') AS ObjId");
        return dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ObjId"] != DBNull.Value;
    }

    static void RecreateProcedure(string procName, string createSql)
    {
        RunNonQuery("IF OBJECT_ID('dbo." + Escape(procName) + "', 'P') IS NOT NULL DROP PROCEDURE dbo." + procName + ";");
        RunNonQuery(createSql == null ? string.Empty : createSql.Trim());
    }

    static DataTable RunSelect(string sql)
    {
        Data objData = new Data();
        DataTable dt = new DataTable();
        try
        {
            objData.StartConnection();
            try
            {
                dt = objData.RunDataTable(sql);
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            dt = new DataTable();
        }
        return dt ?? new DataTable();
    }

    static bool RunNonQuery(string sql)
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                objData.RunInsUpDelQuery(sql);
                return true;
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            return false;
        }
    }

    static string Escape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }
}
