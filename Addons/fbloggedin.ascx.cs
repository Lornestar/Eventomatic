using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventomatic.Addons
{
    public partial class fbloggedin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Setuser(Int64 fbid)
        {
            Site Sitetemp = new Site();
            imgfbuser.ImageUrl = "http://graph.facebook.com/" + fbid.ToString() + "/picture";
            lblfbuser.Text += Sitetemp.graph_getfbname(fbid);
        }

        public void Setmsg(string message)
        {
            lblmessage.Text = message;
        }
    }
}