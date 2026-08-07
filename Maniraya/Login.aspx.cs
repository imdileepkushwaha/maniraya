using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class Login : System.Web.UI.Page
{
    const string CaptchaSessionKey = "LoginCaptcha";
    clsLogin objlogin = new clsLogin();
    clsUser objuser = new clsUser();
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClearLoginError();
            GenerateCaptcha();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            RegisterHideLoaderScript();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        ClearLoginError();

        string userId = txtEmail.Text.Trim();
        string password = loginPassword.Text.Trim();

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
        {
            ShowLoginError("Please enter your User ID and password.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtCaptcha.Text))
        {
            ShowLoginError("Please enter the security code.");
            txtCaptcha.Focus();
            return;
        }

        if (!ValidateCaptcha())
        {
            ShowLoginError("Invalid security code. Please try again.");
            GenerateCaptcha();
            txtCaptcha.Text = string.Empty;
            return;
        }

        DataTable dt = TryDirectUserLogin(userId, password);
        if (dt == null || dt.Rows.Count == 0)
        {
            objlogin.username = userId;
            objlogin.password = password;
            ViewState["pwd"] = password;
            objlogin.ipaddress = GetIp();
            dt = objlogin.Chk_UserLoginDetails(objlogin);
        }

        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dt2 = objlogin.Chk_userDetails(userId);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                Session["userid"] = userId;
                Session["username"] = dt.Rows[0]["username2"].ToString();
                Session["UserImage"] = dt.Rows[0]["UserImage"].ToString();
                Session["status"] = dt.Rows[0]["status1"].ToString();
                Session["Mobile"] = dt.Columns.Contains("Mobile")
                    ? dt.Rows[0]["Mobile"].ToString()
                    : dt.Rows[0]["status1"].ToString();

                if (dt.Rows[0]["status123"].ToString() == "1")
                {
                    ViewState["otp"] = dt.Rows[0]["otp"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal12(); hideLoginLoader();", true);
                    return;
                }

                string redirectUrl = Session["returnUrl"] != null
                    ? Session["returnUrl"].ToString()
                    : ResolveUrl("~/index.aspx");
                Session.Remove("returnUrl");
                // Sliding session: touch timeout so active users stay logged in longer
                Session.Timeout = 480;
                RedirectAfterLogin(redirectUrl);
                return;
            }

            objlogin.username = userId;
            objlogin.password = password;
            objlogin.ipaddress = GetIp();
            dt = objlogin.Chk_UserTempLoginDetails(objlogin);
            if (dt != null && dt.Rows.Count > 0)
            {
                byte[] byt = System.Text.Encoding.UTF8.GetBytes(userId);
                RedirectAfterLogin(ResolveUrl("~/VerifyRegistration.aspx?UserId=" + Convert.ToBase64String(byt)));
                return;
            }

            ShowLoginError("Invalid login details. Please check your User ID and password.");
            GenerateCaptcha();
            txtCaptcha.Text = string.Empty;
            return;
        }

        ShowLoginError("Invalid login details. Please check your User ID and password.");
        GenerateCaptcha();
        txtCaptcha.Text = string.Empty;
    }

    protected void lnkRefreshCaptcha_Click(object sender, EventArgs e)
    {
        ClearLoginError();
        GenerateCaptcha();
        txtCaptcha.Text = string.Empty;
    }

    void GenerateCaptcha()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        var random = new Random(Guid.NewGuid().GetHashCode());
        char[] code = new char[6];

        for (int i = 0; i < code.Length; i++)
        {
            code[i] = chars[random.Next(chars.Length)];
        }

        string captcha = new string(code);
        Session[CaptchaSessionKey] = captcha;
        lblCaptchaCode.Text = captcha;
    }

    bool ValidateCaptcha()
    {
        string expected = Convert.ToString(Session[CaptchaSessionKey]);
        string entered = txtCaptcha.Text.Trim();

        if (string.IsNullOrEmpty(expected) || string.IsNullOrEmpty(entered))
        {
            return false;
        }

        return string.Equals(expected, entered, StringComparison.OrdinalIgnoreCase);
    }

    void RedirectAfterLogin(string redirectUrl)
    {
        string safeUrl = redirectUrl.Replace("'", "\\'");
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "LoginRedirect",
            "hideLoginLoader(); window.location.replace('" + safeUrl + "');",
            true);
    }

    DataTable TryDirectUserLogin(string username, string password)
    {
        const string query = @"SELECT 0 AS status123, 0 AS otp, ld.*, ud.username AS username2,
            ud.PhotoImage AS UserImage, ISNULL(ud.Status, 0) AS status1, ud.Mobile
            FROM LoginDetail ld
            LEFT JOIN UserDetail ud ON ld.Username = ud.UserId
            WHERE ld.Username = @username AND ld.Password = @password
            AND ld.Role = 'User' AND ld.Status = 1";

        ObjData.StartConnection();
        try
        {
            SqlParameter[] parameter = {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };
            return ObjData.RunDataTableParam(query, parameter);
        }
        catch
        {
            return null;
        }
        finally
        {
            ObjData.EndConnection();
        }
    }

    void ClearLoginError()
    {
        lblLoginError.Text = string.Empty;
        lblLoginError.Visible = false;
        lblLoginError.CssClass = "auth-feedback";
    }

    void ShowLoginError(string message)
    {
        lblLoginError.Text = message;
        lblLoginError.CssClass = "auth-feedback is-error";
        lblLoginError.Visible = true;
    }

    void RegisterHideLoaderScript()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoginLoader", "hideLoginLoader();", true);
    }

    public string GetIp()
    {
        string ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ipaddress))
        {
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        return ipaddress;
    }
}
