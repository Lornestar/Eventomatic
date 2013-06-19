using System;
using System.Configuration;

namespace Eventomatic
{
    public partial class Promo_Demo : System.Web.UI.Page
    {
        string demourl = "http://www.thegroupstore.com/demo.aspx";
        string thebody = "You can try the Groupstore Demo at http://www.thegroupstore.com/demo.aspx";
        int resource_key = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgqr.ImageUrl = "http://promo.thegroupstore.com/images/demo_qr.jpg";

                if ((Request.QueryString["resource_key"] != null) && (Request.QueryString["resource_key"] != ""))
                {
                    resource_key = Convert.ToInt32(Request.QueryString["resource_key"]);

                    demourl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "PayForward2.aspx?resource_key=" + resource_key.ToString();
                    thebody = "You can sell on Groupstore at " + demourl;

                    Addons.qrcodes qrcode = new Addons.qrcodes();
                    qrcode.Generatepfqrimg(resource_key);
                    imgqr.ImageUrl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "images/qr/pf/" + resource_key + ".png";

                    lblheader.Text = "";
                    lblaction.Text = "That's it! You're all set to start selling now.  Use the tool below to send your exclusive Groupstore link to your phone.";
                }
            }            
        }

        protected void btnphone_Click(object sender, EventArgs e)
        {
            Addons.Twilio sms = new Addons.Twilio();
           

            sms.SendSMS(txtPhoneNumber.Text, thebody);            
            Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('The sms has been sent.');</script>");
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            Eventomatic.Send_Email sendemail = new Send_Email();
            
            sendemail.Send_Email_Simple("Mobile@thegroupstore.com", txtEmail.Text, "Complete the Sale", thebody);
            Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('The Email has been sent.');</script>");
        }
    }
}