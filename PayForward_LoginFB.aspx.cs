using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Eventomatic
{
    public partial class PayForward_LoginFB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btncheckinpaypal_Click(object sender, EventArgs e)
        {
            string thereturnpage = HttpContext.Current.Request.Url.AbsoluteUri.Replace("PayForward_LoginFB","PayForward");
            if (thereturnpage.Contains("frompc"))
            {
                thereturnpage = thereturnpage.Replace("&frompc=true", "frompc");
            }
            Response.Redirect("http://www.facebook.com/dialog/oauth?client_id=" + ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + thereturnpage + "&display=touch&scope=email,publish_stream,offline_access&display=touch");
        }
    }
}