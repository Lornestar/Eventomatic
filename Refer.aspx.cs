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
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using System.IO;
using Eventomatic.Addons;
using Infragistics.WebUI.UltraWebGrid;

namespace Eventomatic
{
    public partial class Refer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string reffbid = Request.QueryString["gsref"];

            Site sitetemp = new Site();

            if (HttpContext.Current.Request.QueryString["code"] == null) //authorize user
            {
                hdredirect.Value = "https://graph.facebook.com/oauth/authorize?client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Refer.aspx?gsref=" + reffbid + "&scope=publish_stream,user_events,friends_events,user_groups,friends_groups,read_friendlists";
            }
            else //set referral in db
            {
                fbuser fbuser = new fbuser();
                string oauth = "";
                if (HttpContext.Current.Request.QueryString["fb_sig_session_key"] != null)
                {
                    oauth = HttpContext.Current.Request.QueryString["fb_sig_session_key"];
                }
                else
                {
                    oauth = HttpContext.Current.Request.QueryString["code"].ToString();
                    oauth = oauth.Substring(0, oauth.IndexOf("|"));
                }

                //oauth = oauth.Substring(0, oauth.IndexOf("|"));                

                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
                string result = wc.DownloadString("https://graph.facebook.com/oauth/access_token?type=client_cred&client_secret=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Secret").ToString() + "&client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&code=" + oauth);
                string accesstoken = result.Replace("access_token=", "");


                //Get user id
                wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
                string result2 = wc.DownloadString("https://api.facebook.com/method/users.getLoggedInUser?access_token=" + accesstoken + "&session_key=" + oauth);

                fbuser.AccessToken = accesstoken;
                fbuser.SessionKey = oauth;

                XmlTextReader textReader = new XmlTextReader(new StringReader(result2));
                textReader.Read();


                while (textReader.Read())
                {
                    if (textReader.NodeType == XmlNodeType.Text)
                    {
                        if (sitetemp.IsNumeric(textReader.Value))
                        {
                            fbuser.UID = Convert.ToInt64(textReader.Value);
                            break;
                        }
                    }
                    textReader.MoveToContent();
                }

                //Check if have user info....if not then store into db
                DataSet dstemp = Eventomatic_DB.SPs.ViewFbUsers(fbuser.UID).GetDataSet();
                if (dstemp.Tables[0].Rows.Count == 0)//new user, so get bunch of info
                {
                    //Get names of user
                    result = wc.DownloadString("https://api.facebook.com/method/users.getInfo?uids=" + fbuser.UID.ToString() + "&fields=name,first_name,last_name&access_token=" + fbuser.AccessToken + "&session_key=" + fbuser.SessionKey);

                    textReader = new XmlTextReader(new StringReader(result));
                    textReader.Read();
                    while (textReader.Read())
                    {
                        if ((textReader.Name == "first_name") && (textReader.NodeType == XmlNodeType.Element))
                        {
                            textReader.Read();
                            fbuser.Firstname = textReader.Value;
                        }
                        else if ((textReader.Name == "last_name") && (textReader.NodeType == XmlNodeType.Element))
                        {
                            textReader.Read();
                            fbuser.Lastname = textReader.Value;
                        }
                        else if ((textReader.Name == "name") && (textReader.NodeType == XmlNodeType.Element))
                        {
                            textReader.Read();
                            fbuser.Fullname = textReader.Value;
                        }
                    }                    
                    Eventomatic_DB.SPs.UpdateResource(fbuser.UID, fbuser.Firstname, fbuser.Lastname, "", HttpContext.Current.Request.UserHostAddress, sitetemp.GetCurrentPageName(), 0, 0, fbuser.SessionKey, fbuser.AccessToken, Convert.ToInt64(reffbid)).Execute();
                }
                else//fill in info from db
                {
                    fbuser.Firstname = dstemp.Tables[0].Rows[0]["First_Name"].ToString();
                    fbuser.Lastname = dstemp.Tables[0].Rows[0]["Last_Name"].ToString();
                    fbuser.Fullname = fbuser.Firstname + " " + fbuser.Lastname;
                }
                HttpContext.Current.Session["fbuser"] = fbuser;
                Response.Redirect("default.aspx");
            }
        }
    }
}
