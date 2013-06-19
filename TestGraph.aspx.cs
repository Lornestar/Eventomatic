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
    public partial class TestGraph : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = Request.QueryString["code"]; //Facebooks sends the user back to our domain with the code parameter
                if (code == null)
                {
                    Response.Redirect("https://graph.facebook.com/oauth/authorize?client_id=5077249571&redirect_uri=http://localhost:57042/testgraph.aspx&scope=publish_stream");
                }                
            }            
        }
    }
}
