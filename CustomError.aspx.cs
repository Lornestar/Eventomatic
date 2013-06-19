using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Eventomatic
{
    public partial class CustomError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewErrorsLastone(0).GetDataSet();
            string strEventTime = "Time";
            if (dstemp.Tables[0].Rows[0]["EventTime"] != DBNull.Value)
            {
                strEventTime = dstemp.Tables[0].Rows[0]["EventTime"].ToString();
            }
            string strRequestURL = "URL";
            if (dstemp.Tables[0].Rows[0]["RequestURL"] != DBNull.Value)
            {
                strRequestURL = dstemp.Tables[0].Rows[0]["RequestURL"].ToString();
            }
            string strDetails = "Details";
            if (dstemp.Tables[0].Rows[0]["Details"] != DBNull.Value)
            {
                strDetails = dstemp.Tables[0].Rows[0]["Details"].ToString();
            }

            string thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/ErrorEmail.txt"));
            thebody = thebody.Replace("TIMEERROR", strEventTime);
            thebody = thebody.Replace("URLERROR", strRequestURL);
            thebody = thebody.Replace("DETAILSERROR", strDetails);
            string Toemail = System.Configuration.ConfigurationSettings.AppSettings.Get("ErrorToEmail").ToString();
            Send_Email SE = new Send_Email();
            SE.Send_Email_Function("Error@theGroupstore.com", Toemail, "An Error has occured", thebody,0);
        }
    }
}
