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
using PayPal.Platform.SDK;
using PayPal.Services.Private.AP;

namespace Eventomatic
{
    public partial class testpaypal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        

        protected void btntest_Click(object sender, EventArgs e)
        {
            Addons.PaypalMethods ppcheck = new Addons.PaypalMethods();
            ppcheck.CheckAccount_DirectPayment(false, "demo@thegroupstore.com");
        }
    }
}
