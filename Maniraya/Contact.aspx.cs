using System;
using System.Web.UI;

public partial class Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtMessage.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please fill out all required fields.');", true);
            return;
        }

        // Add email sending logic or database save logic here
        
        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Thank you for contacting us. We will get back to you shortly!');", true);
        
        // Clear form
        txtName.Text = "";
        txtEmail.Text = "";
        txtSubject.Text = "";
        txtMessage.Text = "";
    }
}
