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
    public partial class IsAppUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string strtemp = "https://graph.facebook.com/oauth/authorize?client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/default.aspx&scope=publish_stream,user_events,friends_events,user_groups,friends_groups,read_friendlists";
            //Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('Your Changes have been Saved');location.href = '" + strtemp + "';</script>");
            string strurl = "default.aspx";
            if (Request.QueryString["url"] != null)
            {
                if (Request.QueryString["url"] != "")
                {
                    strurl = Request.QueryString["url"];
                }            
            }            
            hdredirect.Value = "https://graph.facebook.com/oauth/authorize?client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + strurl + "&scope=publish_stream,user_events,user_groups,read_friendlists";
        }
    }
}
