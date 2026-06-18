using System;
using System.Web.UI;

public partial class Contact : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindContactInfo();
        }
    }

    private void BindContactInfo()
    {
        string phone = SiteContactHelper.GetPrimaryPhone();
        string email = SiteContactHelper.GetPrimaryEmail();
        string address = SiteContactHelper.GetPrimaryAddress();

        if (lnkContactPhone != null)
        {
            lnkContactPhone.HRef = SiteContactHelper.BuildTelHref(phone);
        }

        if (lblContactPhone != null)
        {
            lblContactPhone.InnerText = phone;
        }

        if (lnkContactEmail != null)
        {
            lnkContactEmail.HRef = SiteContactHelper.BuildMailtoHref(email);
        }

        if (lblContactEmail != null)
        {
            lblContactEmail.InnerText = email;
        }

        if (lblContactAddress != null)
        {
            lblContactAddress.InnerText = address;
        }

        if (lnkContactWhatsApp != null)
        {
            lnkContactWhatsApp.HRef = SiteContactHelper.BuildWhatsAppHref(phone, "Hi Maniraya, I need help");
        }
    }
}
