using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
//using com.paypal.soap.api;
using com.paypal.sdk.services;
using com.paypal.sdk.profiles;
using com.paypal.sdk.util;
using System.Data;
using System.Configuration;

namespace Eventomatic
{
    public partial class MobileShare : System.Web.UI.Page
    {
        int Tx_Key = 0;
        string mobilepayurl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
           
            int Event_Key = 0;
            Int64 fbid = 0;
            if ((Request.QueryString["tx"] != null) && (Request.QueryString["tx"] != ""))
            {
                if (Request.QueryString["tx"].ToString() == "app")
                {
                    Tx_Key = 0;
                    PopulateShareapp();
                }
                else
                {
                    Tx_Key = Convert.ToInt32(Request.QueryString["tx"].ToString());                
                }                
            }
            if (Tx_Key != 0)
            {
                Site sitetemp = new Site();

                Event_Key = Convert.ToInt32(sitetemp.GetEventKeyTx(Tx_Key));
                if ((Request.QueryString["fbid"] != null) && (Request.QueryString["fbid"] != ""))
                {
                    fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
                }

                mobilepayurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "mobilepay.aspx?tx=" + Tx_Key.ToString();
                
                imgqr.ImageUrl = sitetemp.Getqrurltx(Tx_Key);
                
                PopulateExistingEvent(Event_Key);
            }
        }

        protected void PopulateShareapp()
        {
            imgqr.ImageUrl = "http://promo.thegroupstore.com/images/bb.png";
            imgqr.Width = 320;
            imgqr.Height = 320;
            mobilepayurl = "http://www.thegroupstore.com/mobiledownload.aspx";
            lblEvent_Name.Text = "Share App";
            lblqrcheckout.Text = "Share App by qr code";
            btnEmail.Text = "Share App by email";
            btnphone.Text = "Share App by sms";
        }

        protected void PopulateExistingEvent(int Event_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewEventDetails(Event_Key).GetDataSet();

            lblEvent_Name.Text = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
            Page.Title = lblEvent_Name.Text;

            Site sitetemp = new Site();
            if (!sitetemp.HavePermission_Eventkey(Event_Key))
            {
                btnphone.Enabled = false;
                btnphone.Text = "Disabled - Mobile Sales turned off";
                lblqrcheckout.Text = "Mobile Sales is turned off - If you are the event host, log into Groupstore, go to Settings and click on Enable Mobile Sales. This will accelerating ticket sales by allowing everyone to sell ticket on your behalf with their smartphones.";
                imgqr.Visible = false;
            }
        }

        protected void btnphone_Click(object sender, EventArgs e)
        {
            Addons.Twilio sms = new Addons.Twilio();
            string thebody = "";
            Site sitetemp = new Site();
            if (Request.QueryString["tx"].ToString() == "app")
            {
                thebody = "Someone has shared with you the Groupstore mobile app.  You can download it at " + mobilepayurl;
            }
            else
            {
                thebody = sitetemp.getfbid_Seller_Name(Tx_Key) + " initiated a Groupstore Sale. Complete this sale at " + mobilepayurl;
            }
            
            sms.SendSMS(txtPhoneNumber.Text, thebody);
            Eventomatic_DB.SPs.UpdateTransactionSMSNumber(Tx_Key, txtPhoneNumber.Text).Execute();
            Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('The sms has been sent.');</script>");
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