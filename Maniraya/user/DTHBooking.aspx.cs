using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_DTHBooking : System.Web.UI.Page
{
    clsdthsuscription objS = new clsdthsuscription();
    clsState objState = new clsState();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                loadserviceprovider();
                loadstate();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = "1";
        dt = objState.getState(objState);

        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstate.Items.Insert(0, li);
    }
    void loadcity()
    {
        ddcity.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = ddstate.SelectedValue.ToString();
        dt = objState.getCity(objState);
        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();     
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);

    }
    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcity();
    }
    void loadserviceprovider()
    {
        DDlserviceprovider.Items.Clear();
        DataTable dt = new DataTable();
        dt = objS.getserviceprovider(objS);
        DDlserviceprovider.DataSource = dt;
        DDlserviceprovider.DataTextField = "Operator";
        DDlserviceprovider.DataValueField = "id";
        DDlserviceprovider.DataBind();
        ListItem li = new ListItem("Select Service Provider", "0");
        DDlserviceprovider.Items.Insert(0, li);
    }
    void loadsettopbox()
    {
        DDlsettopbox.Items.Clear();
        DataTable dt = new DataTable();
        objS.opid = DDlserviceprovider.SelectedValue.ToString();
        dt = objS.getSetTopBoxbyid(objS);

        DDlsettopbox.DataSource = dt;
        DDlsettopbox.DataTextField = "STBName";
        DDlsettopbox.DataValueField = "ID";
        DDlsettopbox.DataBind();
        ListItem li = new ListItem("Select SettopBox", "0");
        DDlsettopbox.Items.Insert(0, li);
    }
    void loadpachege()
    {
        DDLpackage.Items.Clear();
        DataTable dt = new DataTable();
        objS.Settopboxid = DDlsettopbox.SelectedValue.ToString();
        dt = objS.getActivepackagebyid(objS);

        DDLpackage.DataSource = dt;
        DDLpackage.DataTextField = "packagename";
        DDLpackage.DataValueField = "ID";
        DDLpackage.DataBind();
        ListItem li = new ListItem("Select Package", "0");
        DDLpackage.Items.Insert(0, li);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(DDlserviceprovider.SelectedIndex ==0)
        {
            Message.Show("select service provider");
            return;
        }
        if (DDlsettopbox.SelectedIndex == -1)
        {
            Message.Show("select settop box");
            return;
        }
        if (DDlsettopbox.SelectedIndex == 0)
        {
            Message.Show("select settop box");
            return;
        }
        if (DDLpackage.SelectedIndex == -1)
        {
            Message.Show("select package");
            return;
        }
        if (DDLpackage.SelectedIndex == 0)
        {
            Message.Show("select package");
            return;
        }
        if (TxtPrice.Text == string.Empty)
        {
            Message.Show("select package");
            return;
        }
        if (Txtname.Text == string.Empty)
        {
            Message.Show("enter name");
            return;
        }
        if (TxtMobile.Text == string.Empty)
        {
            Message.Show("enter mobile");
            return;
        }
        if (Txtaddress.Text == string.Empty)
        {
            Message.Show("enter address");
            return;
        }
        if (ddstate.SelectedIndex==0)
        {
            Message.Show("Select State");
            return;
        }
        if (ddcity.SelectedIndex==0)
        {
            Message.Show("Select City");
            return;
        }
        if (txtdistrict.Text.Trim() == "")
        {
            Message.Show("Enter District");
            return;
        }
        if (txtLandMark.Text.Trim() == "")
        {
            Message.Show("Enter Landmark");
            return;
        }
        objS.ServiceProviderId = DDlserviceprovider.SelectedValue;
        objS.Settopboxid = DDlsettopbox.SelectedValue;
        objS.packageid = DDLpackage.SelectedValue;
        objS.Mrp = TxtPrice.Text;
        objS.name = Txtname.Text;
        objS.address = Txtaddress.Text;
        objS.mobileno = TxtMobile.Text;
        objS.pincode = TxtPincode.Text;
        objS.entryuser = Session["userId"].ToString();
        objS.ipaddress = GetIp();
        objS.state = ddstate.SelectedValue;
        objS.City = ddcity.SelectedValue;
        objS.LandMark = txtLandMark.Text;
        objS.District = txtdistrict.Text;
        DataTable Dt = objS.insertDTHBooking(objS);
        if (Dt.Rows.Count > 0)
        {
            if (Dt.Rows[0][0].ToString() == "1")
            {
                string popupScript = "alert('" + Dt.Rows[0][1].ToString() + "');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                clearvalue();
            }
            else
            {
                string popupScript = "alert('" + Dt.Rows[0][1].ToString() + "');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        else
        {
            string popupScript = "alert('error occured');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }

    }
    public string GetIp()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }
    void clearvalue()
    {
        DDlserviceprovider.SelectedIndex = 0;
        DDlsettopbox.Items.Clear();
        DDLpackage.Items.Clear();
        TxtPrice.Text = string.Empty;
        Txtname.Text = string.Empty;
        Txtaddress.Text = string.Empty;
        TxtMobile.Text = string.Empty;
        TxtPincode.Text = string.Empty;
    }
    protected void DDlserviceprovider_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsettopbox();
    }
    protected void DDlsettopbox_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadpachege();
    }
    protected void DDLpackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLpackage.SelectedIndex == 0)
        {
            txtrprice.Text = "0";
            txtcomm.Text = "0";
            div_row_retailer.Visible = false;
        }

        DataTable dt = new DataTable();
        DataTable dtcom = new DataTable();
        objS.Settopboxid = DDlsettopbox.SelectedValue.ToString();
        objS.packageid = DDLpackage.SelectedValue.ToString();
        dt = objS.getpackagebyidown(objS);
        dtcom = objS.getCommissionDTH(objS);        
        if (dt.Rows.Count > 0)
        {
            TxtPrice.Text = dt.Rows[0]["pmrp"].ToString();

        }
        else
        {
            TxtPrice.Text = "";
        }
        if (dtcom.Rows.Count > 0)
        {
            
            if (dtcom.Rows[0]["ComType"].ToString() == "0")
            {
                if (dtcom.Rows[0]["AmtType"].ToString() == "0")
                {
                    Decimal Com = Math.Round(Convert.ToDecimal(TxtPrice.Text) * Convert.ToDecimal(dtcom.Rows[0]["atrate"].ToString())/100,2);
                    TxtRetailerPrice.Text = Convert.ToString(Convert.ToDecimal(TxtPrice.Text) - Com);
                     TxtCommssion.Text= Com.ToString();
                }
                else
                {
                    TxtCommssion.Text= dtcom.Rows[0]["atrate"].ToString();
                    TxtRetailerPrice.Text = Convert.ToString(Convert.ToDecimal(TxtPrice.Text) - Convert.ToDecimal(dtcom.Rows[0]["atrate"].ToString()));
                }

            }
            else
            {
                  if (dtcom.Rows[0]["AmtType"].ToString() == "0")
                {
                    Decimal Com = Math.Round(Convert.ToDecimal(TxtPrice.Text) * Convert.ToDecimal(dtcom.Rows[0]["atrate"].ToString()) / 100, 2);
                    TxtRetailerPrice.Text = Convert.ToString(Convert.ToDecimal(TxtPrice.Text) + Com);
                      TxtCommssion.Text= Com.ToString();
                }
                else
                {
                    TxtCommssion.Text= dtcom.Rows[0]["atrate"].ToString();
                    TxtRetailerPrice.Text = Convert.ToString(Convert.ToDecimal(TxtPrice.Text) + Convert.ToDecimal(dtcom.Rows[0]["atrate"].ToString()));
                }
            }


            txtrprice.Text = TxtRetailerPrice.Text;
            txtcomm.Text = TxtCommssion.Text;
            div_row_retailer.Visible = true;
           
        }
        else
        {
           
        }
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
}