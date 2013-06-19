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
    public partial class DecidePath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            Site sitetemp = new Site();            
            string strtemp = Request.Url.AbsoluteUri.ToString();
            lbltest.Text = strtemp;
            //if ((strtemp.Contains("fb_")) || (strtemp.Contains("?ref=bookmarks")))
            if ((sitetemp.Applocation()) && (!strtemp.Contains("addons")))
            {
                Session["fbuser"] = null;
                Response.Redirect("Default.aspx" + strtemp.Replace("http://www.thegroupstore.com/DecidePath.aspx", ""));
                //Response.Redirect(sitetemp.GetNavigateurl() + "Default.aspx");
                //Response.Redirect("http://apps.facebook.com/groupstore/default.aspx");
            }
            else
            {
                //Response.Redirect("http://unbouncepages.com/groupstore/");
                //Response.Redirect("http://promo.thegroupstore.com");
                Response.Redirect("http://www.snap-pay.com");
            }
            
        }

        protected void btntest_Click(object sender, EventArgs e)
        {
            Site sitetemp = new Site();            
            //string strtemp = Request.Url.AbsoluteUri.ToString();
            //lbltest.Text = strtemp;
            //if (strtemp.Contains("fb_"))
            if (sitetemp.Applocation())            
            {
                Response.Redirect(sitetemp.GetNavigateurl() + "Default.aspx");
                //Response.Redirect("Default.aspx" + strtemp.Replace("http://www.thegroupstore.com/DecidePath.aspx", ""));
            }
            else
            {
                Response.Redirect("http://promo.thegroupstore.com");
            }
        }
    }
}
