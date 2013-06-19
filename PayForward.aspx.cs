using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;


namespace Eventomatic
{
    public partial class PayForward : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hdipaddress.Value = HttpContext.Current.Request.UserHostAddress;
            if ((Request.QueryString["code"] != null) && (Request.QueryString["code"] != ""))
            {
                Setfbid();
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e) //Going from questions to Payment
        {
        }

        protected void btnPayPal_Click(object sender, EventArgs e)
        {
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
        }

        protected void Setfbid()
        {
            string oauth = "";
            oauth = HttpContext.Current.Request.QueryString["code"].ToString();
            //oauth = oauth.Substring(0, oauth.IndexOf("|"));

            //oauth = oauth.Substring(0, oauth.IndexOf("|"));                

            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string parttoremove = "?code=" + oauth;
            string returnurl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(parttoremove, "");
            //string result = wc.DownloadString("https://graph.facebook.com/oauth/access_token?response_type=token&client_secret=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Secret").ToString() + "&client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&code=" + oauth);
            string result = wc.DownloadString("https://graph.facebook.com/oauth/access_token?client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + returnurl + "&client_secret=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Secret").ToString() + "&code=" + oauth);
            string accesstoken = result.Replace("access_token=", "");


            //Get user id
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string result2 = wc.DownloadString("https://graph.facebook.com/me?access_token=" + accesstoken);

            try
            {
                JObject o = JObject.Parse(result2);
                string fbid = (string)o["id"];
                hdnfbid.Value = fbid;
                //lblfbstatus.Text = fbid +  " will get credit for this ticket sale";                
                string email = "";
                string firstname = "";
                string lastname = "";
                if (o["email"] != null)
                {
                    email = (string)o["email"];
                }
                if (o["first_name"] != null)
                {
                    firstname = (string)o["first_name"];
                }
                if (o["last_name"] != null)
                {
                    lastname = (string)o["last_name"];
                }

                Eventomatic_DB.SPs.UpdateTicketSellers(Convert.ToInt64(fbid), firstname + " " + lastname, accesstoken, email).Execute();
            }
            catch
            {
            }

        }
    }
}