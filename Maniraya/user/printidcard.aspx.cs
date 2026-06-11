using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using  System.Globalization;


public partial class user_idcard : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DataTable dt = (DataTable)Session["dt"];
            if (dt.Rows.Count > 0)
            {
                lbluserid.Text = dt.Rows[0]["userid"].ToString();
                lblusername.Text = dt.Rows[0]["username"].ToString();
                lblDob.Text = dt.Rows[0]["DateofBirth"].ToString();

                //lblState.Text = dt.Rows[0]["statename"].ToString();

                // lblcity.Text = dt.Rows[0]["citynew"].ToString();
                //  LblSponserId.Text = dt.Rows[0]["sponserId"].ToString();
                // LblParentId.Text = dt.Rows[0]["parentuserid"].ToString();
                ImgMyPhoto.ImageUrl = "../ProductImage/" + dt.Rows[0]["PhotoImage"].ToString();
                //  lbljoiningdate.Text = dt.Rows[0]["parentuserid"].ToString();
                //  LblParentName.Text = dt.Rows[0]["parentname"].ToString();
                //  LblSponserName.Text = dt.Rows[0]["sponsername"].ToString();
                //  lbljoiningdate.Text = dt.Rows[0]["regdate"].ToString();
                //   lbladdress.Text = dt.Rows[0]["address"].ToString();
                //ddcountry.SelectedValue = dt.Rows[0]["countryid"].ToString();
                //loadstate();
                ddstate.Text = dt.Rows[0]["StateName"].ToString();
                //loadcity();
                ddcity.Text = dt.Rows[0]["CityName"].ToString();
                lblmobile.Text = dt.Rows[0]["mobile"].ToString();
                lblemail.Text = dt.Rows[0]["email"].ToString();

                // Lblactivatedate.Text = dt.Rows[0]["activationdate"].ToString();


            }

            string rptName = Request.QueryString["rptName"];
            Page.Header.Title = rptName;

            if (Request.QueryString["ete"] == "1")
            {
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("Content-Disposition", "inline;filename=" + rptName + ".xls");
            }
            if (Request.QueryString["print"] == "1")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "window.print();", true);
            }

        }
        
    }
    
}