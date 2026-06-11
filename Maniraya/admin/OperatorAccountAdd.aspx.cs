using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class admin_OperatorAccountAdd : System.Web.UI.Page
{
    clsOperator objoperator = new clsOperator();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    public void FillGrid()
    {
        DDlOpertor.DataSource = objoperator.selectOpertorWithOption();
        DDlOpertor.DataTextField = "Operator";
        DDlOpertor.DataValueField = "OperatorCodeId";
        DDlOpertor.DataBind();
        //GridView1.DataSource = Obal.selectOpertorWithOption();
        //GridView1.DataBind();

    }
    public void filldetail(string OpertorId)
    {
        DataTable dt = objoperator.selectOpertorWithOption();
        string searchExpression = "OperatorCodeId = '" + DDlOpertor.SelectedValue + "'";
        DataRow[] foundRows = dt.Select(searchExpression);
        if (foundRows.Length > 0)
        {
            Txtre1.Text = foundRows[0][3].ToString();
            TxtField1.Text = foundRows[0][4].ToString();
            Txtre2.Text = foundRows[0][5].ToString();
            TxtField2.Text = foundRows[0][6].ToString();
            Txtre3.Text = foundRows[0][7].ToString();
            TxtField3.Text = foundRows[0][8].ToString();
            Txtre4.Text = foundRows[0][9].ToString();
            TxtField4.Text = foundRows[0][10].ToString();

        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        lBLmESSAGE.Text = "";
        string option1 = "", option2 = "", option3 = "", option4 = "", Replace1 = "", Replace2 = "", Replace3 = "", Replace4 = "";
        if (CheckBox1.Checked == true)
        {
            if (Txtre1.Text == "" || TxtField1.Text == "")
            {
                lBLmESSAGE.Text = "Fill Option 1 Value";
                return;
            }
            else
            {
                option1 = Txtre1.Text; Replace1 = TxtField1.Text;

            }
        }
        else
            if (CheckBox2.Checked == true )
            {
                if (Txtre2.Text == "" || TxtField2.Text == "")
                {
                    lBLmESSAGE.Text = "Fill Option 2 Value";
                    return;
                }
                else
                {
                    option2 = Txtre2.Text; Replace2 = TxtField2.Text;
                }
            }
            else
                if (CheckBox3.Checked == true )
                {
                    if (Txtre3.Text == "" || TxtField3.Text == "")
                    {
                        lBLmESSAGE.Text = "Fill Option 3 Value";
                        return;
                    }
                    else
                    {
                        option3 = Txtre3.Text; Replace3 = TxtField3.Text;
                    }
                }
                else
                    if (CheckBox4.Checked == true )
                    {
                        if (Txtre4.Text == "" || TxtField4.Text == "")
                        {
                            lBLmESSAGE.Text = "Fill Option 4 Value";
                            return;
                        }
                        else
                        {
                            option4 = Txtre4.Text; Replace4 = TxtField4.Text;
                        }
                    }
        objoperator.OpertorOption(DDlOpertor.SelectedItem.Value, option1, option2, option3, option4, Replace1, Replace2, Replace3, Replace4);
        lBLmESSAGE.Text = "Update Successfully";
    }
    protected void DDlOpertor_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldetail(DDlOpertor.SelectedValue);
    }
}