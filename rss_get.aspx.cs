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
    public partial class rss_get : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Site sitetemp = new Site();
                string resourcekeytemp = Master.GetResourceKey();
                txtrssoutput.Text = "<style type='text/css'>";
     txtrssoutput.Text += ".Groupstoretable{border:solid 1px black;}";
                txtrssoutput.Text += ".GroupstoreTitle{color:#324a80;text-align:left;font-weight:bold;font-family:Arial Black;text-decoration:none;white-space:inherit;}";
txtrssoutput.Text += ".Groupstoretextbox{width:300px;}";
                txtrssoutput.Text += ".GroustoreDesc{color:Gray;font-size:smaller;text-align:left;text-decoration:none;white-space:inherit;}";
     txtrssoutput.Text += ".GroustoreDate{font-size:small;text-align:left;}";
                txtrssoutput.Text += "img.Groupstoreimg{height:100px;}";
                txtrssoutput.Text += "</style>";
                txtrssoutput.Text += "<span id=groupstoreoutput></span><script type='text/javascript' charset='utf-8' src='http://www.thegroupstore.com/readrss.php?storeid=" + resourcekeytemp + "'></script>";
            }
        }
    }
}
