using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventomatic
{
    public partial class Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Site sitetemp = new Site();

            string strmobile = sitetemp.getMobileOS();
            if (strmobile == "ios")
            {
                Response.Redirect("itms-apps://itunes.com/apps/snappay/id492807738");
            }
            else if (strmobile == "android")
            {
                Response.Redirect("market://details?id=com.webviewdemo");
            }
            else
            {
                Response.Redirect("http://www.thegroupstore.com/login/signin.aspx");
            }
        }
    }
}