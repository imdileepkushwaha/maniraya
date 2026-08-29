using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTier;

public partial class admin_AdminSettings : Page
{
    Data ObjData = new Data();

    protected string SelectedUserId
    {
        get { return ViewState["SelectedUserId"] as string ?? string.Empty; }
        set { ViewState["SelectedUserId"] = value; }
    }

    static readonly string[] AvatarColors = {
        "#0ea5e9", "#8b5cf6", "#f59e0b", "#10b981", "#ef4444", "#6366f1", "#14b8a6", "#ec4899"
    };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        string role = Session["role"] != null ? Convert.ToString(Session["role"]) : string.Empty;
        if (string.Equals(role, "Subadmin", StringComparison.OrdinalIgnoreCase))
        {
            Response.Redirect("Dashboard.aspx");
            return;
        }

        if (!IsPostBack)
        {
            EnsureMenusSeeded();
            LoadUsers();
        }
    }

    void ShowAlert(string message)
    {
        string safe = (message ?? string.Empty).Replace("\\", "\\\\").Replace("'", "\\'");
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(),
            "alert('" + safe + "');", true);
    }

    static string Escape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    void EnsureMenusSeeded()
    {
        int mainCount = 0;
        try
        {
            ObjData.StartConnection();
            DataTable dt = ObjData.RunDataTable("SELECT COUNT(*) AS Cnt FROM MainMenu");
            if (dt != null && dt.Rows.Count > 0)
            {
                mainCount = Convert.ToInt32(dt.Rows[0]["Cnt"]);
            }
        }
        catch
        {
            mainCount = -1;
        }
        finally
        {
            ObjData.EndConnection();
        }

        if (mainCount > 0)
        {
            EnsureSettingsMenuExists();
            EnsureVirtualFranchiseMenuExists();
            return;
        }

        var groups = GetDefaultMenuGroups();
        try
        {
            ObjData.StartConnection();
            ObjData.RunInsUpDelQuery("DELETE FROM AdminMenuPermission");
            ObjData.RunInsUpDelQuery("DELETE FROM Menu");
            ObjData.RunInsUpDelQuery("DELETE FROM MainMenu");

            try
            {
                ObjData.RunInsUpDelQuery("DBCC CHECKIDENT ('MainMenu', RESEED, 0)");
                ObjData.RunInsUpDelQuery("DBCC CHECKIDENT ('Menu', RESEED, 0)");
            }
            catch
            {
            }

            foreach (var group in groups)
            {
                ObjData.RunInsUpDelQuery(
                    "INSERT INTO MainMenu (MainMenuName, URL, Status) VALUES ('" + Escape(group.Name) + "', '', 1)");

                DataTable idDt = ObjData.RunDataTable("SELECT MAX(id) AS Id FROM MainMenu");
                int mainId = Convert.ToInt32(idDt.Rows[0]["Id"]);

                foreach (var item in group.Items)
                {
                    ObjData.RunInsUpDelQuery(
                        "INSERT INTO Menu (MainMenuId, MenuName, URL, Status) VALUES (" +
                        mainId + ", '" + Escape(item.Name) + "', '" + Escape(item.Url) + "', 1)");
                }
            }
        }
        catch
        {
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    void EnsureSettingsMenuExists()
    {
        try
        {
            ObjData.StartConnection();

            // Hide legacy subadmin pages from assignable menus
            ObjData.RunInsUpDelQuery(@"
UPDATE Menu SET Status = 0
WHERE LOWER(LTRIM(RTRIM(URL))) IN ('subadmin.aspx','subadminreport.aspx','adminmenupermission.aspx')");

            DataTable dt = ObjData.RunDataTable(
                "SELECT TOP 1 id FROM Menu WHERE URL = 'AdminSettings.aspx' AND ISNULL(Status,1)=1");
            if (dt != null && dt.Rows.Count > 0)
            {
                return;
            }

            DataTable mainDt = ObjData.RunDataTable(
                "SELECT TOP 1 id FROM MainMenu WHERE MainMenuName IN ('Settings','SubAdmin Management') ORDER BY CASE WHEN MainMenuName='Settings' THEN 0 ELSE 1 END");
            int mainId;
            if (mainDt == null || mainDt.Rows.Count == 0)
            {
                ObjData.RunInsUpDelQuery(
                    "INSERT INTO MainMenu (MainMenuName, URL, Status) VALUES ('Settings', '', 1)");
                mainDt = ObjData.RunDataTable("SELECT MAX(id) AS Id FROM MainMenu");
            }
            mainId = Convert.ToInt32(mainDt.Rows[0][0]);

            ObjData.RunInsUpDelQuery(
                "INSERT INTO Menu (MainMenuId, MenuName, URL, Status) VALUES (" +
                mainId + ", 'Admin users & menu access', 'AdminSettings.aspx', 1)");
        }
        catch
        {
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    void EnsureVirtualFranchiseMenuExists()
    {
        try
        {
            ObjData.StartConnection();
            DataTable dt = ObjData.RunDataTable(
                "SELECT TOP 1 id FROM Menu WHERE URL = 'VirtualFranchisePlanRequestReport.aspx'");
            if (dt != null && dt.Rows.Count > 0)
            {
                return;
            }

            DataTable mainDt = ObjData.RunDataTable(
                "SELECT TOP 1 id FROM MainMenu WHERE MainMenuName = 'Virtual Franchise'");
            int mainId;
            if (mainDt == null || mainDt.Rows.Count == 0)
            {
                ObjData.RunInsUpDelQuery(
                    "INSERT INTO MainMenu (MainMenuName, URL, Status) VALUES ('Virtual Franchise', '', 1)");
                mainDt = ObjData.RunDataTable("SELECT MAX(id) AS Id FROM MainMenu");
            }
            mainId = Convert.ToInt32(mainDt.Rows[0][0]);

            ObjData.RunInsUpDelQuery(
                "INSERT INTO Menu (MainMenuId, MenuName, URL, Status) VALUES (" +
                mainId + ", 'Plan Request Approval', 'VirtualFranchisePlanRequestReport.aspx', 1)");
        }
        catch
        {
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    class MenuGroupDef
    {
        public string Name;
        public List<MenuItemDef> Items = new List<MenuItemDef>();
    }

    class MenuItemDef
    {
        public string Name;
        public string Url;
        public MenuItemDef(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }

    List<MenuGroupDef> GetDefaultMenuGroups()
    {
        var list = new List<MenuGroupDef>();

        list.Add(MakeGroup("Utility Management",
            "Add Country|CountryAdd.aspx",
            "Add State|StateAdd.aspx",
            "Add City|CityAdd.aspx",
            "Add Bank|BankAdd.aspx",
            "Bank Account Add|BankAccountAdd.aspx",
            "Deduction Master|deductioncharge.aspx",
            "News Add|NewsAdd.aspx",
            "Add Plan|PlanAdd.aspx",
            "Package Plan Master|PackagePlanMaster.aspx",
            "Direct Member Login|MemberLoginPanel.aspx"));

        list.Add(MakeGroup("Settings",
            "Admin users & menu access|AdminSettings.aspx"));

        list.Add(MakeGroup("Franchisee Master",
            "Franchisee Type Master|FranchiseetypeMaster.aspx",
            "Franchisee Add|franchiseeAdd.aspx",
            "Franchisee Report|FranchiseeReport.aspx",
            "Product Purchase|FranchiseePurchaseMaster.aspx",
            "Product Purchase Report|FranchiseePurchaseDetail.aspx",
            "Stock Details|FranchiseeStockDetail.aspx"));

        list.Add(MakeGroup("Website Management",
            "Add Slider|AddSlider.aspx",
            "Add Image Gallery|AddGalleryImage.aspx",
            "Add Video Gallery|VideosAdd.aspx",
            "Add FAQ|FAQAdd.aspx",
            "Contact Settings|ContactAdd.aspx",
            "Add Popup|PopupAdd.aspx"));

        list.Add(MakeGroup("Product Management",
            "Add Category|CategoryAdd.aspx",
            "Add Sub-Category|SubcategoryAdd.aspx",
            "Add Size|SizeAdd.aspx",
            "Add Color|ColorAdd.aspx",
            "Subcategory Setting|ProductSizeColorMaster.aspx",
            "Add Product|ProductAdd.aspx",
            "Product Details|ProductDetails.aspx",
            "Change Status|ChangeProductStatus.aspx",
            "Stock Report|StockReport.aspx",
            "Vendor Master|VendorMaster.aspx",
            "Stock Purchase|VendorPurchaseMaster.aspx",
            "Purchase Details|VendorPurchaseDetail.aspx",
            "Add Commodities Price|MetalPriceMaster.aspx"));

        list.Add(MakeGroup("My Network",
            "Add User|UserAdd.aspx",
            "User Report|UserReport.aspx",
            "Direct Rank Report|DirectRankReport.aspx",
            "Approve KYC|kycApprovalForUser.aspx",
            "Downline Report|DownlineReport.aspx",
            "Treeview Report|treeview.aspx"));

        list.Add(MakeGroup("Accounts",
            "Transaction Report|TransactionReport.aspx",
            "Daily Closing Report|Dailypayoutdetail.aspx",
            "Saving Level Income Report|SavingLevelIncomeReport.aspx",
            "Saving Direct Income Report|SavingDirectIncomeReport.aspx",
            "Saving Level Inst Income|SavingLevelinstDetail.aspx",
            "Saving Level Payout Report|PayoutReportSavingLevel.aspx",
            "Saving Product GST Reports|SavingProductGSTReport.aspx"));

        list.Add(MakeGroup("Epin Management",
            "Epin Add|epinadd.aspx",
            "Epin Transfer|epintransfer.aspx",
            "Epin Report|epinreport.aspx"));

        list.Add(MakeGroup("Saving Product",
            "Add Saving Product|SavingProductAdd.aspx",
            "Add Monthly Saving Product|SavingProductMonthlyAdd.aspx",
            "Saving Purchase Report|SavingProductPurchaseReport.aspx",
            "Assign Saving Product|SavingProductAssign.aspx",
            "Add Saving Product Stock|SavingProductStockAdd.aspx",
            "Add Saving Session|SavingSessionDetailAdd.aspx",
            "EMI Purchase Report|SavingInstallmentReport.aspx",
            "Bulk EMI Payment Report|SavingBulkInstallmentPaymentReport.aspx",
            "Pending Installment Report|SavingPendingInstallmentReport.aspx",
            "Coupon Report|CouponReport.aspx"));

        list.Add(MakeGroup("Virtual Franchise",
            "Plan Request Approval|VirtualFranchisePlanRequestReport.aspx"));

        list.Add(MakeGroup("Purchase Management",
            "Approve Purchase|UserRepurchaseReport.aspx",
            "Purchase Invoice|UserPurchaseDetail.aspx",
            "Saving Purchase Orders|SavingProductOrderDetails.aspx",
            "Saving Installment Orders|SavingInstallmentOrderDetails.aspx"));

        list.Add(MakeGroup("Prize",
            "Prize Master|PrizeMaster.aspx",
            "Assign Prize|AssignPrize.aspx",
            "Bonanza|Bonanza.aspx"));

        return list;
    }

    MenuGroupDef MakeGroup(string name, params string[] items)
    {
        var g = new MenuGroupDef { Name = name };
        foreach (string item in items)
        {
            string[] parts = item.Split('|');
            g.Items.Add(new MenuItemDef(parts[0], parts[1]));
        }
        return g;
    }

    void LoadUsers()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UserId");
        dt.Columns.Add("RoleLabel");
        dt.Columns.Add("StatusText");
        dt.Columns.Add("Initial");
        dt.Columns.Add("AvatarColor");

        string sql = @"
SELECT ld.Username AS UserId,
       ISNULL(NULLIF(LTRIM(RTRIM(sd.outletName)), ''), 'SubAdmin') AS RoleLabel,
       CASE WHEN ISNULL(ld.Status,'0') = '1' THEN 'Active' ELSE 'Inactive' END AS StatusText
FROM LoginDetail ld WITH (NOLOCK)
LEFT JOIN subadmindetail sd WITH (NOLOCK) ON sd.UserId = ld.Username
WHERE ld.Role = 'Subadmin'
ORDER BY ld.Username";

        DataTable src = null;
        try
        {
            ObjData.StartConnection();
            src = ObjData.RunDataTable(sql);
        }
        catch
        {
            src = null;
        }
        finally
        {
            ObjData.EndConnection();
        }

        if (src != null)
        {
            for (int i = 0; i < src.Rows.Count; i++)
            {
                string userId = Convert.ToString(src.Rows[i]["UserId"]);
                DataRow row = dt.NewRow();
                row["UserId"] = userId;
                row["RoleLabel"] = Convert.ToString(src.Rows[i]["RoleLabel"]);
                row["StatusText"] = Convert.ToString(src.Rows[i]["StatusText"]);
                row["Initial"] = string.IsNullOrWhiteSpace(userId) ? "?" : userId.Substring(0, 1).ToUpperInvariant();
                row["AvatarColor"] = AvatarColors[i % AvatarColors.Length];
                dt.Rows.Add(row);
            }
        }

        rptUsers.DataSource = dt;
        rptUsers.DataBind();
        pnlNoUsers.Visible = dt.Rows.Count == 0;

        if (!string.IsNullOrWhiteSpace(SelectedUserId))
        {
            bool stillExists = false;
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToString(row["UserId"]) == SelectedUserId)
                {
                    stillExists = true;
                    break;
                }
            }
            if (stillExists)
            {
                LoadMenusForUser(SelectedUserId);
            }
            else
            {
                SelectedUserId = string.Empty;
                ShowMenuEditor(false);
            }
        }
        else
        {
            ShowMenuEditor(false);
        }
    }

    void ShowMenuEditor(bool show)
    {
        pnlMenuEditor.Visible = show;
        pnlMenuEmpty.Visible = !show;
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        string username = (txtUsername.Text ?? string.Empty).Trim();
        string password = (txtPassword.Text ?? string.Empty).Trim();
        string roleLabel = (txtRoleLabel.Text ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(username))
        {
            ShowAlert("Enter Username");
            return;
        }
        if (!Regex.IsMatch(username, @"^[A-Za-z0-9_]+$"))
        {
            ShowAlert("Username can contain only letters, numbers and underscore");
            return;
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            ShowAlert("Enter Password");
            return;
        }
        if (password.Length < 4)
        {
            ShowAlert("Password must be at least 4 characters");
            return;
        }
        if (string.IsNullOrWhiteSpace(roleLabel))
        {
            roleLabel = "SubAdmin";
        }

        if (UsernameExists(username))
        {
            ShowAlert("This Username already exists");
            return;
        }

        string mentionBy = Convert.ToString(Session["useradmin"]);
        bool ok = false;
        try
        {
            ObjData.StartConnection();
            ObjData.RunInsUpDelQuery(
                "INSERT INTO LoginDetail (Username, Password, Role, Status, MentionBy, MentionDate) VALUES (" +
                "'" + Escape(username) + "', '" + Escape(password) + "', 'Subadmin', '1', '" + Escape(mentionBy) + "', GETDATE())");

            int nextId = 1;
            DataTable idDt = ObjData.RunDataTable("SELECT ISNULL(MAX(id),0)+1 AS NextId FROM subadmindetail");
            if (idDt != null && idDt.Rows.Count > 0)
            {
                nextId = Convert.ToInt32(idDt.Rows[0]["NextId"]);
            }

            ObjData.RunInsUpDelQuery(
                "INSERT INTO subadmindetail (id, UserId, UserName, outletName, ActiveStatus, Status, MentionBy, MentionDate, RegDate, DeleteStatus, BalanceAmount) VALUES (" +
                nextId + ", '" + Escape(username) + "', '" + Escape(username) + "', '" + Escape(roleLabel) +
                "', '1', 1, '" + Escape(mentionBy) + "', GETDATE(), GETDATE(), '0', 0)");
            ok = true;
        }
        catch
        {
            ok = false;
        }
        finally
        {
            ObjData.EndConnection();
        }

        if (!ok)
        {
            ShowAlert("Unable to create user. Please try again.");
            return;
        }

        txtUsername.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtRoleLabel.Text = string.Empty;
        SelectedUserId = username;
        LoadUsers();
        ShowAlert("SubAdmin created successfully");
    }

    bool UsernameExists(string username)
    {
        try
        {
            ObjData.StartConnection();
            DataTable dt = ObjData.RunDataTable(
                "SELECT TOP 1 Username FROM LoginDetail WHERE Username = '" + Escape(username) + "'");
            return dt != null && dt.Rows.Count > 0;
        }
        catch
        {
            return true;
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName != "select") return;
        SelectedUserId = Convert.ToString(e.CommandArgument);
        LoadUsers();
    }

    void LoadMenusForUser(string userId)
    {
        hdnSelectedUser.Value = userId;
        litSelectedUser.Text = Server.HtmlEncode(userId);
        if (txtNewPassword != null) txtNewPassword.Text = string.Empty;
        if (txtConfirmPassword != null) txtConfirmPassword.Text = string.Empty;

        string roleLabel = "SubAdmin";
        try
        {
            ObjData.StartConnection();
            DataTable roleDt = ObjData.RunDataTable(
                "SELECT ISNULL(NULLIF(LTRIM(RTRIM(outletName)), ''), 'SubAdmin') AS RoleLabel FROM subadmindetail WHERE UserId = '" + Escape(userId) + "'");
            if (roleDt != null && roleDt.Rows.Count > 0)
            {
                roleLabel = Convert.ToString(roleDt.Rows[0]["RoleLabel"]);
            }
        }
        catch
        {
        }
        finally
        {
            ObjData.EndConnection();
        }
        litSelectedRole.Text = Server.HtmlEncode(roleLabel);

        DataSet ds = GetMenuPermission(userId);
        DataTable main = new DataTable();
        main.Columns.Add("MainMenuId", typeof(int));
        main.Columns.Add("MainMenuName", typeof(string));
        main.Columns.Add("Checked", typeof(bool));

        Dictionary<int, DataTable> subMap = new Dictionary<int, DataTable>();

        if (ds != null && ds.Tables.Count > 0)
        {
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                int mainId = Convert.ToInt32(dt1.Rows[i]["id"]);
                DataRow row = main.NewRow();
                row["MainMenuId"] = mainId;
                row["MainMenuName"] = Convert.ToString(dt1.Rows[i]["MainMenuName"]);
                row["Checked"] = Convert.ToInt32(dt1.Rows[i]["Checked"]) == 1;
                main.Rows.Add(row);

                DataTable sub = new DataTable();
                sub.Columns.Add("MenuId", typeof(int));
                sub.Columns.Add("MainMenuId", typeof(int));
                sub.Columns.Add("MenuName", typeof(string));
                sub.Columns.Add("Url", typeof(string));
                sub.Columns.Add("Checked", typeof(bool));

                if (dt2.Rows.Count > 0)
                {
                    DataRow[] rows = dt2.Select("MainMenuId = " + mainId);
                    foreach (DataRow r in rows)
                    {
                        string url = Convert.ToString(r["URL"]);
                        string urlLower = (url ?? string.Empty).Trim().ToLowerInvariant();
                        if (urlLower == "subadmin.aspx" || urlLower == "subadminreport.aspx"
                            || urlLower == "adminmenupermission.aspx" || urlLower == "adminsettings.aspx")
                        {
                            continue;
                        }

                        DataRow s = sub.NewRow();
                        s["MenuId"] = Convert.ToInt32(r["id"]);
                        s["MainMenuId"] = mainId;
                        s["MenuName"] = Convert.ToString(r["MenuName"]);
                        s["Url"] = url;
                        s["Checked"] = Convert.ToInt32(r["Checked"]) == 1;
                        sub.Rows.Add(s);
                    }
                }

                // Skip empty main groups (e.g. only hidden legacy pages)
                if (sub.Rows.Count == 0)
                {
                    main.Rows.Remove(row);
                    continue;
                }
                subMap[mainId] = sub;
            }
        }

        ViewState["SubMenuMap"] = null;
        rptMainMenus.DataSource = main;
        rptMainMenus.DataBind();

        for (int i = 0; i < rptMainMenus.Items.Count; i++)
        {
            HiddenField hdnMainId = (HiddenField)rptMainMenus.Items[i].FindControl("hdnMainId");
            Repeater rptSub = (Repeater)rptMainMenus.Items[i].FindControl("rptSubMenus");
            if (hdnMainId == null || rptSub == null) continue;
            int mainId = Convert.ToInt32(hdnMainId.Value);
            rptSub.DataSource = subMap.ContainsKey(mainId) ? subMap[mainId] : new DataTable();
            rptSub.DataBind();
        }

        ShowMenuEditor(true);
    }

    protected void rptMainMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // Nested binding handled after parent DataBind in LoadMenusForUser.
    }

    DataSet GetMenuPermission(string userId)
    {
        SqlConnection cn = null;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        try
        {
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.ReadCommitted);
            SqlParameter[] parameter = { new SqlParameter("@userid", userId) };
            ds = ObjData.RunDataSetProcedureTRans("GetAdminPermissionMenu", tr, parameter);
            tr.Commit();
        }
        catch
        {
            if (tr != null) tr.Rollback();
            ds = new DataSet();
        }
        finally
        {
            ObjData.EndConnection();
            if (tr != null) tr.Dispose();
        }
        return ds;
    }

    protected void btnSaveMenus_Click(object sender, EventArgs e)
    {
        string userId = hdnSelectedUser.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            ShowAlert("Select a user first");
            return;
        }

        DataTable dtMain = new DataTable();
        dtMain.Columns.Add("MainMenuId", typeof(int));
        DataTable dtSub = new DataTable();
        dtSub.Columns.Add("MainMenuId", typeof(int));
        dtSub.Columns.Add("MenuId", typeof(int));

        foreach (RepeaterItem mainItem in rptMainMenus.Items)
        {
            CheckBox chkMain = (CheckBox)mainItem.FindControl("chkMain");
            HiddenField hdnMainId = (HiddenField)mainItem.FindControl("hdnMainId");
            Repeater rptSub = (Repeater)mainItem.FindControl("rptSubMenus");
            if (hdnMainId == null || rptSub == null) continue;

            int mainId = Convert.ToInt32(hdnMainId.Value);
            bool anySub = false;

            foreach (RepeaterItem subItem in rptSub.Items)
            {
                CheckBox chkSub = (CheckBox)subItem.FindControl("chkSub");
                HiddenField hdnSubId = (HiddenField)subItem.FindControl("hdnSubId");
                if (chkSub == null || hdnSubId == null || !chkSub.Checked) continue;

                anySub = true;
                DataRow s = dtSub.NewRow();
                s["MainMenuId"] = mainId;
                s["MenuId"] = Convert.ToInt32(hdnSubId.Value);
                dtSub.Rows.Add(s);
            }

            if ((chkMain != null && chkMain.Checked) || anySub)
            {
                DataRow m = dtMain.NewRow();
                m["MainMenuId"] = mainId;
                dtMain.Rows.Add(m);
            }
        }

        int result = UpdateMenuPermission(userId, dtMain, dtSub);
        if (result == 1)
        {
            ShowAlert("Menu access saved successfully");
            SelectedUserId = userId;
            LoadUsers();
        }
        else
        {
            ShowAlert("Unable to save menu access");
        }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        string userId = hdnSelectedUser.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            ShowAlert("Select a user first");
            return;
        }

        string newPass = (txtNewPassword.Text ?? string.Empty).Trim();
        string confirmPass = (txtConfirmPassword.Text ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(newPass))
        {
            ShowAlert("Enter new password");
            return;
        }
        if (newPass.Length < 4)
        {
            ShowAlert("Password must be at least 4 characters");
            return;
        }
        if (newPass != confirmPass)
        {
            ShowAlert("Password and confirm password do not match");
            return;
        }

        if (!IsSubAdminUser(userId))
        {
            ShowAlert("Invalid SubAdmin user");
            return;
        }

        bool ok = false;
        try
        {
            ObjData.StartConnection();
            ObjData.RunInsUpDelQuery(
                "UPDATE LoginDetail SET Password = '" + Escape(newPass) + "' " +
                "WHERE Username = '" + Escape(userId) + "' AND Role = 'Subadmin'");
            ok = true;
        }
        catch
        {
            ok = false;
        }
        finally
        {
            ObjData.EndConnection();
        }

        txtNewPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;

        if (ok)
        {
            ShowAlert("Password updated successfully");
            SelectedUserId = userId;
            LoadUsers();
        }
        else
        {
            ShowAlert("Unable to update password");
        }
    }

    protected void btnDeleteUser_Click(object sender, EventArgs e)
    {
        string userId = hdnSelectedUser.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            ShowAlert("Select a user first");
            return;
        }

        string currentAdmin = Convert.ToString(Session["useradmin"]);
        if (string.Equals(userId, currentAdmin, StringComparison.OrdinalIgnoreCase))
        {
            ShowAlert("You cannot delete the currently logged-in user");
            return;
        }

        if (!IsSubAdminUser(userId))
        {
            ShowAlert("Invalid SubAdmin user");
            return;
        }

        bool ok = false;
        try
        {
            ObjData.StartConnection();
            ObjData.RunInsUpDelQuery("DELETE FROM AdminMenuPermission WHERE UserId = '" + Escape(userId) + "'");
            ObjData.RunInsUpDelQuery("DELETE FROM LoginDetail WHERE Username = '" + Escape(userId) + "' AND Role = 'Subadmin'");
            ObjData.RunInsUpDelQuery("DELETE FROM subadmindetail WHERE UserId = '" + Escape(userId) + "'");
            ok = true;
        }
        catch
        {
            ok = false;
        }
        finally
        {
            ObjData.EndConnection();
        }

        if (ok)
        {
            SelectedUserId = string.Empty;
            hdnSelectedUser.Value = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            LoadUsers();
            ShowAlert("User deleted successfully");
        }
        else
        {
            ShowAlert("Unable to delete user");
        }
    }

    bool IsSubAdminUser(string userId)
    {
        try
        {
            ObjData.StartConnection();
            DataTable dt = ObjData.RunDataTable(
                "SELECT TOP 1 Username FROM LoginDetail WHERE Username = '" + Escape(userId) + "' AND Role = 'Subadmin'");
            return dt != null && dt.Rows.Count > 0;
        }
        catch
        {
            return false;
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    int UpdateMenuPermission(string userId, DataTable mainMenu, DataTable subMenu)
    {
        SqlConnection cn = null;
        SqlTransaction tr = null;
        try
        {
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            SqlParameter pUser = new SqlParameter("@userid", userId);
            SqlParameter pMain = new SqlParameter("@tmainmenu", mainMenu);
            pMain.SqlDbType = SqlDbType.Structured;
            pMain.TypeName = "dbo.TMainMenu";
            SqlParameter pSub = new SqlParameter("@tmenu", subMenu);
            pSub.SqlDbType = SqlDbType.Structured;
            pSub.TypeName = "dbo.TMenu";
            SqlParameter[] parameter = { pUser, pMain, pSub };
            DataTable dt = ObjData.RunDataTableProcedureTRans("UpdateAdminPermissionMenu", tr, parameter);
            tr.Commit();
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
        catch
        {
            if (tr != null) tr.Rollback();
            return 0;
        }
        finally
        {
            ObjData.EndConnection();
            if (tr != null) tr.Dispose();
        }
    }
}
