using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Eventomatic
{
    public partial class MobileTurnedoff : System.Web.UI.Page
    {
        int Tx_Key = 0;
        string mobilepayurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["tx"] != null) && (Request.QueryString["tx"] != ""))
            {
                Tx_Key = Convert.ToInt32(Request.QueryString["tx"].ToString());
                mobilepayurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "mobilepay.aspx?tx=" + Tx_Key.ToString();
            }
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            Eventomatic.Send_Email sendemail = new Send_Email();

            string thebody = "";

            if (Request.QueryString["tx"].ToString() == "app")
            {
                thebody = "Someone has shared with you the Groupstore mobile app.  You can download it at " + mobilepayurl;
            }
            else
            {
                thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/Mobile_Share.txt"));
            }

            Site sitetemp = new Site();
            thebody = thebody.Replace("CHECKOUTURL", mobilepayurl);
            thebody = thebody.Replace("SELLERNAME", sitetemp.getfbid_Seller_Name(Tx_Key));

            sendemail.Send_Email_Simple("Mobile@thegroupstore.com", txtEmail.Text, "Complete the Sale", thebody);
            Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('The Email has been sent.');</script>");
        }
    }
}