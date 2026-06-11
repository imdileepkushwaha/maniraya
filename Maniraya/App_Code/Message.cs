using System.Data;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Web;
using System.Web.Security;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Dynamic;
using System;
/// <summary>
/// Summary description for Message
/// </summary>
public class Message
{
    public static void Show(string Message)
    {
        string printMessage = Message.Replace("'", "\\'");
        string script = "alert('" + printMessage + "');";
        Page popup = HttpContext.Current.CurrentHandler as Page;
        if (popup == null)
        {
            return;
        }

        ScriptManager sm = ScriptManager.GetCurrent(popup);
        if (sm != null)
        {
            ScriptManager.RegisterStartupScript(popup, typeof(Page), "alert", script, true);
        }
        else
        {
            popup.ClientScript.RegisterStartupScript(typeof(Page), "alert", script, true);
        }
    }

    public static DateTime GetIndianDate(string Datedt)
    {
        DateTime myDateTime = new DateTime();

        if (Datedt != "")
        {
            //System.Globalization.CultureInfo ukCulture = new System.Globalization.CultureInfo("en-GB");
            //myDateTime = DateTime.Parse(Datedt, ukCulture.DateTimeFormat);
            string[] str = Datedt.Split('/');
            string str_new = str[1].ToString() + " / " + str[0].ToString() + " / " + str[2].ToString();
            myDateTime = Convert.ToDateTime(str[1].ToString() + "/" + str[0].ToString() + "/" + str[2].ToString());
            return myDateTime;
        }
        else
        {
            myDateTime = Convert.ToDateTime("1/1/1900 12:00:00 AM");
            return myDateTime;
        }

    }
}