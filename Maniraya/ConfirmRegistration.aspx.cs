using System;
using System.Web.UI;
using BusinessLogicTier;

public partial class user_ConfirmRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!HasRegistrationSession())
            {
                Response.Redirect("signup.aspx");
                return;
            }

            laoddata();
        }
    }

    bool HasRegistrationSession()
    {
        return Session["LoginId1"] != null
            && Session["Password1"] != null
            && Session["UserName2"] != null;
    }

    string GetSessionText(string key)
    {
        object value = Session[key];
        return value == null ? string.Empty : value.ToString();
    }

    void laoddata()
    {
        LblLoginId.Text = GetSessionText("LoginId1");
        LblPassword.Text = GetSessionText("Password1");
        LblSponsorName.Text = "Necta Network";
        LblSponsorId.Text = GetSessionText("SponserId1");
        lblName.Text = GetSessionText("UserName2");
    }
}
