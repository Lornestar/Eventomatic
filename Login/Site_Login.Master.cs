using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using Facebook.Web;
using Eventomatic.Addons;
using System.Configuration;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Eventomatic.Login
{
    public partial class Site_Login : System.Web.UI.MasterPage
    {
        string[] requiredAppPermissions = { "user_events", "user_groups", "email", "offline_access" };
        string strrequiredAppPermissions = "user_events,user_groups,email,offline_access";
        Site sitetemp = new Site();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("signin.aspx"))
                {
                    int tempResource = 0;
                    if (sitetemp.IsNumeric(hdresourcekey.Value))
                    {
                        tempResource = Convert.ToInt32(hdresourcekey.Value);
                    }
                    fbuser fbuser = getfbuser();
                    Eventomatic_DB.SPs.UpdateResource(fbuser.UID, fbuser.Firstname, fbuser.Lastname, fbuser.Email, HttpContext.Current.Request.UserHostAddress, GetCurrentPageName(), tempResource, 0, fbuser.SessionKey, fbuser.AccessToken, 0).Execute();

                    getResourceKey();

                    imgfbprofile.ImageUrl = imgfbprofile.ImageUrl.Replace("#", fbuser.UID.ToString());
                    lblloggedin.Text = lblloggedin.Text.Replace("#", fbuser.Fullname);
                }
                else
                {
                    lblloggedin.Visible = false;
                    btnlogout.Visible = false;
                }
            }
        }

        public int getResourceKey()
        {

            if (hdresourcekey.Value == "0")
            {
                fbuser fbuser = getfbuser();
                //check how many stores admin of
                Hashtable adminstoreinfo = AdminforNumberofStores(fbuser.UID);
                int adminfornumberofstores = Convert.ToInt32(adminstoreinfo["amount"]);
                if (adminfornumberofstores > 0)
                {
                    //admin for at least one store
                    hdresourcekey.Value = adminstoreinfo["Resource_Key"].ToString();
                }
                else
                {
                    //not admin, send to sign up
                    Response.Redirect(ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "PayForward_FirstTime.aspx");
                }
            }            
            return Convert.ToInt32(hdresourcekey.Value);
        }


        protected void Populatefbuser()
        {
            string thereturnpage = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + GetCurrentPageName();
            string oauth = "";
            oauth = HttpContext.Current.Request.QueryString["code"].ToString();

            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            //string result = wc.DownloadString("https://graph.facebook.com/oauth/access_token?response_type=token&client_secret=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Secret").ToString() + "&client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&code=" + oauth);
            string strsend = "https://graph.facebook.com/oauth/access_token?client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + thereturnpage + "&client_secret=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Secret").ToString() + "&code=" + oauth;
            string result = wc.DownloadString(strsend);
            string accesstoken = result.Replace("access_token=", "");


            //Get user id
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string result2 = wc.DownloadString("https://graph.facebook.com/me?access_token=" + accesstoken);

            try
            {
                JObject o = JObject.Parse(result2);
                string fbid = (string)o["id"];                
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
                fbuser fbuser = new fbuser();
                fbuser.UID = Convert.ToInt64(fbid);
                fbuser.AccessToken = accesstoken;
                //fbuser.SessionKey = fbApp.Session.Signature;
                fbuser.Firstname = firstname;
                fbuser.Lastname = lastname;
                fbuser.Fullname = fbuser.Firstname + " " + fbuser.Lastname;
                fbuser.Email = email;

                HttpContext.Current.Session["fbuser"] = fbuser;

                Eventomatic_DB.SPs.UpdateResource(Convert.ToInt64(fbid), firstname, lastname, email, HttpContext.Current.Request.UserHostAddress, GetCurrentPageName(), 0, 0, "", accesstoken, 0).Execute();

            }
            catch
            {
            }
        }

        

        protected JObject doGraphcall(string thecall)
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string result = wc.DownloadString("https://graph.facebook.com/" + thecall);

            JObject o = JObject.Parse(result);
            return o;
        }

        public string GetCurrentPageName()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;

            return "Login/"+ sRet;
        }

        public Eventomatic.Addons.fbuser getfbuser()
        {
            if (HttpContext.Current.Session["fbuser"] == null) //No authorization or fbuid
            {
                //No session currently, redirect to sign in page


                /*
                string thereturnpage = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + GetCurrentPageName();
                if ((Request.QueryString["code"] != null) && (Request.QueryString["code"] != ""))
                {                    
                    Populatefbuser();
                }
                else
                {
                    Response.Redirect("http://www.facebook.com/dialog/oauth?client_id=" + ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + thereturnpage + "&scope=email");
                }*/
                Response.Redirect("SignIn.aspx");
            }
            Eventomatic.Addons.fbuser fbuser = new Eventomatic.Addons.fbuser();
            fbuser = (Eventomatic.Addons.fbuser)HttpContext.Current.Session["fbuser"];
            hdfbid.Value = fbuser.UID.ToString();
            return fbuser;
        }

        public Hashtable AdminforNumberofStores(Int64 fbid)
        {            
            Hashtable hsreturn = new Hashtable();
            //check what they are admin or seller of
            //DataSet dstemp = Eventomatic_DB.SPs.PfViewListFBUserAdmin(fbid).GetDataSet();
            DataSet dstemp = Eventomatic_DB.SPs.PfViewListFBUserAdminSellers(Convert.ToInt64(fbid)).GetDataSet();
            hsreturn.Add("amount",Convert.ToInt32(dstemp.Tables[0].Rows.Count.ToString()));
            if (Convert.ToInt32(hsreturn["amount"]) > 0)
            {
                hsreturn.Add("StoreName", dstemp.Tables[0].Rows[0]["Resource_Name"].ToString());
                hsreturn.Add("Resource_Key", dstemp.Tables[0].Rows[0]["Resource_Key"].ToString());
                hsreturn.Add("Email", dstemp.Tables[0].Rows[0]["Resource_Email"].ToString());
            }            
            
            return hsreturn;
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["fbuser"] = null;
            Response.Redirect("http://www.snap-pay.com");
        }
    }
}