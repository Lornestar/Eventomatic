using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventomatic
{
    public partial class mobiledownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Site sitetemp = new Site();
            if ((!sitetemp.isMobile()) || (!sitetemp.iscompatiblePayPalMobile()))
            {
                Response.Redirect("http://promo.thegroupstore.com/downloadmobile.aspx");
            }
            else
            {
                Response.Redirect("http://www.thegroupstore.com/mobile/bb/RSSReader.jad");
            }            
        }
    }
}