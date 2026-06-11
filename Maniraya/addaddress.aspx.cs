using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net.Mime;
using DataTier;
using ARA_StringHunt;
using System.Security.Policy;

public partial class addaddress : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    clsState objState = new clsState();
    clsProduct objProduct = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
       // Session["userid"] = "TW000001";
        if (Session["userid"] != null)
        {

            if (!IsPostBack)
            {

                loadaddress(Session["userid"].ToString());
                loadstate();


                // loadProduct(1);
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
       
            objuser.UserId = Session["userid"].ToString();
            objuser.Address = txtAddress1.Text.Trim();
            objuser.AddressSec = txtAddress2.Text.Trim();
            objuser.CityId = DDlastCity.SelectedValue;
            objuser.Pincode = txtZip.Text.Trim();
            objuser.AreaName = TxtAreaname.Text.Trim();
            objuser.Mobile = txtPhone.Text.Trim();
            objuser.RegType = ddlType.SelectedValue;
            if(chkDefault.Checked==true)
            {
                objuser.UPINo = "1";
            }
            else
            {
                objuser.UPINo = "0";
            }

            string res;
            string successMessage;
            bool isEdit = !string.IsNullOrEmpty(hfEditAddressId.Value);

            if (isEdit)
            {
                res = objuser.Update_useraddress(objuser, hfEditAddressId.Value);
                successMessage = "Address updated successfully";
            }
            else
            {
                res = objuser.Insert_useraddress(objuser);
                successMessage = "Address added successfully";
            }

            if (res == "t")
            {
                ClearAddressForm();
                loadaddress(Session["userid"].ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "addrSaved", "closeAddAddressModal();alert('" + successMessage + "');", true);
            }
            else if (res == "f")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "addrSaveFail", "showAddAddressModal();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "addrSaveError", "alert('Unknown error occurred');showAddAddressModal();", true);
            }
        
    }
    void loadaddress(string id)
    {
        DataTable Dt = objuser.getUseraddress(id);
        gvAddress.DataSource = Dt;
        gvAddress.DataBind();
    }

    void loadstate()
    {
        DDlststate.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = "1";
        dt = objState.getState(objState);     

        DDlststate.DataSource = dt;
        DDlststate.DataTextField = "StateName";
        DDlststate.DataValueField = "StateID";
        DDlststate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        DDlststate.Items.Insert(0, li);
    }

    void loadcity()
    {
        DDlastCity.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = DDlststate.SelectedValue.ToString();
        dt = objState.getCity(objState);
        DDlastCity.DataSource = dt;
        DDlastCity.DataTextField = "CityName";
        DDlastCity.DataValueField = "CityID";
        DDlastCity.DataBind();
        ListItem li = new ListItem("Select City", "0");
        DDlastCity.Items.Insert(0, li);
    }

    void ClearAddressForm()
    {
        hfEditAddressId.Value = string.Empty;
        txtPhone.Text = string.Empty;
        txtAddress1.Text = string.Empty;
        txtAddress2.Text = string.Empty;
        TxtAreaname.Text = string.Empty;
        txtZip.Text = string.Empty;
        chkDefault.Checked = false;
        ddlType.SelectedIndex = 0;
        if (DDlststate.Items.Count > 0)
        {
            DDlststate.SelectedIndex = 0;
        }
        DDlastCity.Items.Clear();
        DDlastCity.Items.Insert(0, new ListItem("Select City", "0"));
        btnSave.Text = "Save address";
    }

    void LoadAddressForEdit(string addressId)
    {
        DataTable dt = objuser.getUseraddressById(Session["userid"].ToString(), addressId);
        if (dt == null || dt.Rows.Count == 0)
        {
            Message.Show("Address not found");
            return;
        }

        DataRow row = dt.Rows[0];
        hfEditAddressId.Value = addressId;
        txtPhone.Text = Convert.ToString(row["mobile"]);
        txtAddress1.Text = Convert.ToString(row["Addressfirst"]);
        txtAddress2.Text = Convert.ToString(row["AddressSecond"]);
        TxtAreaname.Text = Convert.ToString(row["areaname"]);
        txtZip.Text = Convert.ToString(row["Pincode"]);

        string addressType = Convert.ToString(row["Type"]);
        if (ddlType.Items.FindByValue(addressType) != null)
        {
            ddlType.SelectedValue = addressType;
        }

        string stateId = Convert.ToString(row["Stateid"]);
        if (DDlststate.Items.FindByValue(stateId) != null)
        {
            DDlststate.SelectedValue = stateId;
        }

        loadcity();

        string cityId = Convert.ToString(row["CityId"]);
        if (DDlastCity.Items.FindByValue(cityId) != null)
        {
            DDlastCity.SelectedValue = cityId;
        }

        chkDefault.Checked = Convert.ToString(row["ISdefault"]) == "YES";
        btnSave.Text = "Update address";
    }

    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        ClearAddressForm();
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openAddAddress", "setAddressModalMode('add');showAddAddressModal();", true);
    }

    protected void gvAddress_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditAddress")
        {
            LoadAddressForEdit(Convert.ToString(e.CommandArgument));
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openEditAddress", "setAddressModalMode('edit');showAddAddressModal();", true);
        }
    }

    protected void btnConfirmDelete_Click(object sender, EventArgs e)
    {
        string addressId = hfDeleteAddressId.Value;
        if (string.IsNullOrEmpty(addressId))
        {
            return;
        }

        string res = objuser.Delete_useraddress(Session["userid"].ToString(), addressId);
        if (res == "t")
        {
            hfDeleteAddressId.Value = string.Empty;
            loadaddress(Session["userid"].ToString());
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "addrDeleted", "closeDeleteAddressModal();alert('Address deleted successfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "addrDeleteFail", "closeDeleteAddressModal();alert('Unable to delete address');", true);
        }
    }

    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcity();
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "keepAddressModal", "showAddAddressModal();", true);
    }

    protected void gvAddress_RowDataBound(object sender,
    GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RadioButton rb =
                (RadioButton)e.Row.FindControl("rbSelect");

            Label isdf =
               (Label)e.Row.FindControl("Isdefault");

           if(isdf.Text=="YES")
            {
                rb.Checked = true;
            }
           else
            {
                rb.Checked = false;
            }

           
        }
    }


    protected void btngotopayment_Click(object sender, EventArgs e)
    {
        string addressid = "0";
        if(gvAddress.Rows.Count==0)
        {
            Message.Show("Add address");
            return;
        }
        foreach (GridViewRow Gr in gvAddress.Rows)
        {
         
            RadioButton rb =
           (RadioButton)Gr.FindControl("rbSelect");
            if (rb != null && rb.Checked)
            {

                Label hf =
                    (Label)Gr.FindControl("lblAddressId");

                addressid = hf.Text;

                break;
            }
        }
            string d = objProduct.Update_Addresscartitem(addressid,Session["userid"].ToString());
        if(d=="t")
        {
            Response.Redirect("processpayment.aspx");
        }
        
    }
}
