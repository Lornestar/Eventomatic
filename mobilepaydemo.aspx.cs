using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Eventomatic
{
    public partial class mobilepaydemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tx = Request.QueryString["tx"].ToString();
            string strpaywithpp = "";
            if ((Request.QueryString["paywithpp"] != null) && (Request.QueryString["paywithpp"] != ""))
            {
                strpaywithpp = "&paywithpp=true";
            }
            hdredirecturl.Value =ConfigurationSettings.AppSettings.Get("Store_URL").ToString()+ "mobilepay.aspx?demowarning=true&tx=" + tx + strpaywithpp; 
        }
    }
}