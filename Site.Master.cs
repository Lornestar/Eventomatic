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
//using facebook;
//using facebook.web;
//using facebook.Schema;
using System.Web.Mail;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using Eventomatic.Addons;
using Infragistics.WebUI.UltraWebGrid;
using SubSonic;
using Newtonsoft.Json.Linq;
using Facebook;
using Facebook.Web;
using Telerik.Web.UI;
using PayPal.Services.Private.Permissions;
using PayPal.Platform.SDK;
using com.paypal.sdk.core;
using System.Text;

namespace Eventomatic
{
    public partial class Site : System.Web.UI.MasterPage
    {

        string[] requiredAppPermissions = {"user_events","user_groups","email","offline_access"};
        string strrequiredAppPermissions = "user_events,user_groups,email,offline_access";
        /*
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.QueryString["fb_sig_session_key"] != null)
                {
                    if (HttpContext.Current.Session["fbuser"] != null)
                    {
                        fbuser fbuser = (fbuser)HttpContext.Current.Session["fbuser"];
                        if (fbuser.SessionKey != HttpContext.Current.Request.QueryString["fb_sig_session_key"])
                        {
                            Populatefbuser();
                        }
                    }
                    else
                    {                        
                        Populatefbuser();
                    }                    
                }
                else if (HttpContext.Current.Session["fbuser"] == null) //No authorization or fbuid
                {
                    Populatefbuser();
                    /*
                    if (HttpContext.Current.Request.QueryString["code"] == null)
                    {
                        AuthRedirect(); 
                    }
                    else
                    {
                        Populatefbuser();
                    }
                }                
            }
        }*/

        protected void AuthRedirect() //redirect to authorize
        {
            //Response.Redirect("https://graph.facebook.com/oauth/authorize?client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/default.aspx&scope=publish_stream,user_events,friends_events,user_groups,friends_groups,read_friendlists");            
            string strcurrentpage = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string streventkey = "";
            if (Request.QueryString["event_key"] != null)
            {
                streventkey = Request.QueryString["event_key"];
            }
            if (strcurrentpage.ToLower().Contains("firsttime.aspx"))
            {
                Response.Redirect("IsAppUser.aspx?url=firsttime.aspx");
            }
            else if (strcurrentpage.ToLower().Contains("seller_attendee_list.aspx"))
            {
                Response.Redirect("IsAppUser.aspx?url=seller_attendee_list.aspx?Event_Key=" + streventkey);
            }
            else
            {
                Response.Redirect("IsAppUser.aspx");
            }            
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //base.ApiKey = System.Configuration.ConfigurationSettings.AppSettings.Get("APIKey").ToString();
            //base.Secret = System.Configuration.ConfigurationSettings.AppSettings.Get("Secret").ToString();               
            if (Request.Form["__EVENTTARGET"] == "ImportfbEvent")
            {
                LoadImportEvents();
            }
            if (!IsPostBack)
            {                
                string strtempResource = GetResourceKey();
                string strcurrentpage = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                int tempResource = 0;
                if (IsNumeric(strtempResource))
                {
                    tempResource = Convert.ToInt32(strtempResource);
                }                
                fbuser fbuser = getfbuser();
                Eventomatic_DB.SPs.UpdateResource(fbuser.UID, fbuser.Firstname, fbuser.Lastname, fbuser.Email, HttpContext.Current.Request.UserHostAddress, GetCurrentPageName(),tempResource,GetCurrentPageEventKey(),fbuser.SessionKey,fbuser.AccessToken,0).Execute();
                //if (System.Web.HttpContext.Current.Request.Url.AbsolutePath != "/FirstTime.aspx")                

                if (strcurrentpage.ToLower().Contains("default.aspx"))
                {
                    //false = apps.facebook.com // true = thegroupstore.com                    
                    Eventomatic_DB.SPs.UpdateResourceNavigateURL(fbuser.UID, Applocation()).Execute();
                }

                IsSpy(fbuser.UID);

                //lblurl.Text = strcurrentpage;
                if (CheckIfInGroup2(getfbid()) == "0") //it is not firsttime
                {                    
                        //Assigning it regular menu
                        Menu1.DataSource = SiteMapDataSource1;
                        if (IsAdmin())
                        {
                            //Assigning it admin menu
                            Menu1.DataSource = SiteMapDataSource2;
                            pnlAdmin.Visible = true;
                        }
                        Menu1.DataBind();
                        ddlgroups = LoadGroupsList();                    
                 
                }
                else {//not Groupstore admin
                    if ((strcurrentpage.ToLower().Contains("seller.aspx")) || (strcurrentpage.ToLower().Contains("referral.aspx")) || (strcurrentpage.ToLower().Contains("seller_attendee_list.aspx")))
                    {//in seller mode 
                        pnlNavbar.Visible = false;
                        pnlNavbarseller.Visible = true;
                    }
                    else if (strcurrentpage.ToLower().Contains("firsttime.aspx")) //On the firsttime.aspx
                    {
                        Menu1.Visible = false;
                        lblGroups.Visible = false;
                        lblSeparator.Visible = false;
                        ddlgroups.Visible = false;
                    }
                    else
                    {
                        Page.RegisterStartupScript("Myscript", "<script language=javascript>top.location.href = '" + ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "firsttime.aspx';</script>");
                    }
                }

                string strrooturl = GetOpenWinApplocation();
                string stropenwin = "'openWin(" + fbuser.UID.ToString() + "," + GetResourceKey().ToString() + "," + 1 + ",'" + strrooturl + "'); return false;'";
                lblSellEvent.Text = "<a href='#' onclick=" + stropenwin + ">Sell Event</a>";
                hdfbid.Value = fbuser.UID.ToString();
                hdresourcekey.Value = GetResourceKey().ToString();

                RadMenu1.OnClientItemClicked = "onMenuClicked";
                foreach (RadMenuItem rdi in RadMenu1.Items)
                {
                    if (rdi.Text != "Actions")
                    {
                        rdi.NavigateUrl = GetNavigateurl(fbuser.UID) + rdi.NavigateUrl;
                    }                    
                    if ((rdi.Text == "Admin") && (IsAdmin()))
                    {
                        rdi.Visible = true;
                    }
                    if ((rdi.ImageUrl.Contains("fblogout")) && (!Applocation()))
                    {
                        rdi.Visible = true;                        
                    }                    
                    foreach (RadMenuItem rdi2 in rdi.Items)
                    {
                        if (rdi2.Text == "Sell Event")
                        {
                            //rdi2.Attributes.Add("onclient", stropenwin.Replace("'", ""));                            
                            //rdi2.NavigateUrl = GetNavigateurl() + "#";                                                        
                        }
                        else if (rdi2.Text == "Sell Tickets on Fan Page")
                        {
                            rdi2.NavigateUrl = "http://www.facebook.com/add.php?api_key=" + ConfigurationSettings.AppSettings.Get("APIKey").ToString() + "&pages=1";
                        }
                        else
                        {
                            rdi2.NavigateUrl = GetNavigateurl(fbuser.UID) + rdi2.NavigateUrl;
                        }
                    }
                }
                                                
            }
            Session["CurrentResource"] = ddlgroups.SelectedValue;
          
                CheckDemo();
         
            Site Sitetemp = new Site();
            if (Sitetemp.ImgExists(1, ddlgroups.SelectedValue.ToString() + ".jpg"))
            {
                imgLogo.Visible = true;
                imgLogo.ImageUrl = "/Images/Groups/" + ddlgroups.SelectedValue.ToString() + ".jpg";
            }
            else
            {
                imgLogo.Visible = false;
            }
        }

        

        protected void Populatefbuser()
        {
            fbuser fbuser = new fbuser();
            if ((HttpContext.Current.Request.QueryString["code"] == null) && (HttpContext.Current.Request.QueryString["fb_sig_session_key"] == null))
            {
                AuthRedirect();                
            }
            else
            {
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
                        if (IsNumeric(textReader.Value))
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
                    string strreferral = "0";                    
                    Eventomatic_DB.SPs.UpdateResource(fbuser.UID, fbuser.Firstname, fbuser.Lastname, "", HttpContext.Current.Request.UserHostAddress, GetCurrentPageName(), 0, 0, fbuser.SessionKey, fbuser.AccessToken,Convert.ToInt64(strreferral)).Execute();

                }
                else//fill in info from db
                {
                    fbuser.Firstname = dstemp.Tables[0].Rows[0]["First_Name"].ToString();
                    fbuser.Lastname = dstemp.Tables[0].Rows[0]["Last_Name"].ToString();
                    fbuser.Fullname = fbuser.Firstname + " " + fbuser.Lastname;
                }
                HttpContext.Current.Session["fbuser"] = fbuser;
                string strtemp = CheckIfInGroup2(fbuser.UID.ToString());
                string strcurrentpage = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                bool iscookieless = HttpContext.Current.Session.IsCookieless;

                //Check if user is admin of fb pages
                try
                {
                    getfbPages(fbuser.UID);
                }
                catch
                {
                }                

                if ((strtemp == "1") && (!strcurrentpage.ToLower().Contains("firsttime.aspx")) && (!strcurrentpage.ToLower().Contains("seller_attendee_list.aspx")))
                {
                    Response.Redirect("IsAppUser.aspx?url=firsttime.aspx");
                }                
            }  
        }

        

        protected void PopulatefbuserGraph()
        {            
            CanvasAuthorizer authorizer;
            fbuser fbuser = new fbuser();
            FacebookApp fbApp = new FacebookApp();
            
            authorizer = new CanvasAuthorizer();
            authorizer.Permissions = requiredAppPermissions;            
            if (authorizer.Authorize())
            {
                //ShowFacebookContent();                        
                JsonObject myInfo = (JsonObject)fbApp.Get("me");

                fbuser.UID = Convert.ToInt64(myInfo["id"].ToString());
                fbuser.AccessToken = fbApp.AccessToken;
                fbuser.SessionKey = fbApp.Session.Signature;
                fbuser.Firstname = myInfo["first_name"].ToString();
                fbuser.Lastname = myInfo["last_name"].ToString();
                fbuser.Fullname = fbuser.Firstname + " " + fbuser.Lastname;
                fbuser.Email = getfbappemail(myInfo);

                HttpContext.Current.Session["fbuser"] = fbuser;
                Eventomatic_DB.SPs.UpdateResource(fbuser.UID, fbuser.Firstname, fbuser.Lastname, "", HttpContext.Current.Request.UserHostAddress, GetCurrentPageName(), 0, 0, fbuser.SessionKey, fbuser.AccessToken, 0).Execute();
                try
                {
                    getfbPages(fbuser.UID);
                }
                catch
                {
                }                
            }
            else
            {
                //var url = authorizer.ge auth.GetLoginUrl(new HttpRequestWrapper(Request));
                var url = "https://graph.facebook.com/oauth/authorize?client_id=" + ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "default.aspx&scope=" + strrequiredAppPermissions;
                Uri newuri = new Uri(url);                
                var content = CanvasUrlBuilder.GetCanvasRedirectHtml(newuri);
                Response.ContentType = "text/html";
                Response.Write(content);
                Response.End();
                //return;
            }                
            
        }

        protected void PopulatefbuserGraphConnect()
        {
            Eventomatic.Addons.ConnectService fbconnect = new Eventomatic.Addons.ConnectService();            
            if (fbconnect.IsConnected()) //have cookie & connected
            {                               
                JObject myinfo = doGraphcall(fbconnect.UserId + "?access_token=" + fbconnect.AccessToken);

                fbuser fbuser = new fbuser();
                fbuser.UID = fbconnect.UserId;
                fbuser.Firstname = (string)myinfo["first_name"];
                fbuser.Lastname = (string)myinfo["last_name"];
                fbuser.Fullname = fbuser.Firstname + " " + fbuser.Lastname;
                fbuser.Email = (string)myinfo["email"];
                fbuser.AccessToken = fbconnect.AccessToken;

                HttpContext.Current.Session["fbuser"] = fbuser;
                Eventomatic_DB.SPs.UpdateResource(fbuser.UID, fbuser.Firstname, fbuser.Lastname, "", HttpContext.Current.Request.UserHostAddress, GetCurrentPageName(), 0, 0, fbuser.SessionKey, fbuser.AccessToken, 0).Execute();
                try
                {
                    getfbPages(fbuser.UID);
                }
                catch
                {
                }                                
            }
            else
            {
                //go to logged out page
               Response.Redirect(ConfigurationSettings.AppSettings.Get("LoggedOut_URL").ToString());
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

        public string getfbappemail(JsonObject myInfo)
        {            
            string stremail = "";
            if (myInfo.ContainsKey("email"))
            {
                stremail = myInfo["email"].ToString();
            }
            return stremail;
        }


        public string GetCurrentPageName()
         {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            
            return sRet; 
        }

        public int GetCurrentPageEventKey()
        {
            int tempint = 0;
            if (System.Web.HttpContext.Current.Request.Params.Keys[0].Contains("Event_Key"))
            {
                if (IsNumeric(System.Web.HttpContext.Current.Request.Params[0].ToString()))
                {
                    tempint = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params[0]);
                }                
            }
            return tempint;
        }

        public Eventomatic.Addons.fbuser getfbuser()
        {
            if (HttpContext.Current.Session["fbuser"] == null) //No authorization or fbuid
            {                
                if ((Applocation()) && (!IsPostBack))
                { PopulatefbuserGraph(); }
                else
                {
                    PopulatefbuserGraphConnect();
                }

            }
            Eventomatic.Addons.fbuser fbuser = new Eventomatic.Addons.fbuser();
            fbuser = (Eventomatic.Addons.fbuser)HttpContext.Current.Session["fbuser"];
            return fbuser;
            /*try
            {
                // object strtemp = Api.Users.GetLoggedInUser();                
                fbuser = API.users.getInfo();
                Session["username"] = fbuser.name;
                Session["firstname"] = fbuser.first_name;
                Session["lastname"] = fbuser.last_name;
                Session["userid"] = fbuser.uid;
                
            }
            catch
            {
                //object tempstr = Session["username"];
                if ((Session["userid"] != "0") && (Session["username"] != null) && (Session["firstname"] != null) && (Session["lastname"] != null))
                {
                    fbuser.name = Session["username"].ToString();
                    fbuser.first_name = Session["firstname"].ToString();
                    fbuser.last_name = Session["lastname"].ToString();
                    fbuser.uid = Convert.ToInt64(Session["userid"].ToString());
                }
                else
                {
                    Response.Redirect("CustomError.aspx");
                }
            }*/
        }

        public fbuser getfbuserinfo(Int64 fbid)
        {
            fbuser currentfbuser = getfbuser();            
            fbuser searchingfbuser = new Eventomatic.Addons.fbuser();
            searchingfbuser.UID = fbid;
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string result = wc.DownloadString("https://api.facebook.com/method/users.getInfo?uids=" + searchingfbuser.UID.ToString() + "&fields=name,first_name,last_name&access_token=" + currentfbuser.AccessToken + "&session_key=" + currentfbuser.SessionKey);

            XmlTextReader textReader = new XmlTextReader(new StringReader(result));
            textReader.Read();
            while (textReader.Read())
            {
                if ((textReader.Name == "first_name") && (textReader.NodeType == XmlNodeType.Element))
                {
                    textReader.Read();
                    searchingfbuser.Firstname = textReader.Value;
                }
                else if ((textReader.Name == "last_name") && (textReader.NodeType == XmlNodeType.Element))
                {
                    textReader.Read();
                    searchingfbuser.Lastname = textReader.Value;
                }
                else if ((textReader.Name == "name") && (textReader.NodeType == XmlNodeType.Element))
                {
                    textReader.Read();
                    searchingfbuser.Fullname = textReader.Value;
                }
            }
            return searchingfbuser;
        }

        public fbuser getfbuserinfo(Int64 fbid,fbuser currentfbuser)
        {            
            fbuser searchingfbuser = new Eventomatic.Addons.fbuser();
            searchingfbuser.UID = fbid;
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string result = wc.DownloadString("https://api.facebook.com/method/users.getInfo?uids=" + searchingfbuser.UID.ToString() + "&fields=name,first_name,last_name&access_token=" + currentfbuser.AccessToken + "&session_key=" + currentfbuser.SessionKey);

            XmlTextReader textReader = new XmlTextReader(new StringReader(result));
            textReader.Read();
            while (textReader.Read())
            {
                if ((textReader.Name == "first_name") && (textReader.NodeType == XmlNodeType.Element))
                {
                    textReader.Read();
                    searchingfbuser.Firstname = textReader.Value;
                }
                else if ((textReader.Name == "last_name") && (textReader.NodeType == XmlNodeType.Element))
                {
                    textReader.Read();
                    searchingfbuser.Lastname = textReader.Value;
                }
                else if ((textReader.Name == "name") && (textReader.NodeType == XmlNodeType.Element))
                {
                    textReader.Read();
                    searchingfbuser.Fullname = textReader.Value;
                }
            } 
            return searchingfbuser;
        } 

        public string getfbid()
        {
            if (HttpContext.Current.Session["fbuser"] == null) //No authorization or fbuid
            
            if (Session["fbuser"] == null) //No authorization or fbuid
            {
                //Populatefbuser();
                if (Applocation())
                { PopulatefbuserGraph(); }
                else
                {
                    PopulatefbuserGraphConnect();
                }
            } 
            Eventomatic.Addons.fbuser fbuser = new Eventomatic.Addons.fbuser();
            fbuser = (Eventomatic.Addons.fbuser)HttpContext.Current.Session["fbuser"];
            return fbuser.UID.ToString();
            /*string fbstruserid = "0";
            Int64 fbintuserid = 0;
            try
            {
                user fbuser = API.users.getInfo();
                fbstruserid = fbuser.uid.ToString();
                //fbintuserid = Convert.ToInt64(fbuser.uid);
                return fbstruserid;
            }
            catch
            {
                if (Session["userid"] != "0")
                {
                    fbstruserid = Session["userid"].ToString();
                   // fbintuserid = Convert.ToInt64(Session["userid"].ToString());
                    return fbstruserid; 
                }
                else
                {                   
                    Response.Redirect("CustomError.aspx");                    
                }
            }*/
            return "0";
        }

        

        public bool IsAdmin()
            //must be logged into fb
        {
            fbuser fbuser = getfbuser();            
            DataSet dstemp = Eventomatic_DB.SPs.ViewIfAdmin(Convert.ToInt64(fbuser.UID)).GetDataSet();
            bool strreturn = false;
            if (dstemp.Tables[0].Rows[0]["Admin"] != DBNull.Value)
            {
                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Admin"]) == true)
                {
                    strreturn = true;
                }
            }
            return strreturn;
        }

        public bool IsAdmin(Int64 fbid)
        //must be logged into fb
        {            
            DataSet dstemp = Eventomatic_DB.SPs.ViewIfAdmin(Convert.ToInt64(fbid)).GetDataSet();
            bool strreturn = false;
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Admin"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Admin"]) == true)
                    {
                        strreturn = true;
                    }
                }
            }            
            return strreturn;
        }

        protected void CheckDemo()
        {
            string tempresourcekey = GetResourceKey();
            if (IsNumeric(tempresourcekey))
            {
                DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Convert.ToInt32(tempresourcekey)).GetDataSet();
                //False = Trial & True = Live
                lblDemo.Text = "Trial Version";
                lblDemo.BackColor = System.Drawing.Color.LightBlue;
                if (dstemp.Tables[0].Rows[0]["Demo"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Demo"].ToString()) == true)
                    {//Live
                        //lblDemo.Text = "Live Version";
                        //lblDemo.BackColor = System.Drawing.Color.LightGreen;
                        lblDemo.Visible = false;
                    }
                }
            }            
        }

        protected void CheckIfInGroup(string fbid)
        {
            //if (System.Web.HttpContext.Current.Request.Url.AbsolutePath != "/FirstTime.aspx")
            if (hdnFirstTime.Value == "0")
            {
                DataSet dstemp = Eventomatic_DB.SPs.ViewIfInGroup(Convert.ToInt64(fbid)).GetDataSet();
                if (dstemp.Tables[0].Rows[0]["NumGroups"].ToString() == "0")
                {
                    //Not in a group, go to firsttime page
                    //Response.Redirect("FirstTime.aspx");
                    hdnFirstTime.Value = "1";
                    //Response.Redirect("default.aspx");
                } 
            }
            
    }

        public bool CheckIfIsStoreAdmin(Int64 fbid,int Resource_Key)
        {
            bool result = false;
            //Check if is admin of a specific store
            DataSet dstemp = Eventomatic_DB.SPs.ViewIsStoreAdmin(fbid, Resource_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0][0] != DBNull.Value)
            {
                if (dstemp.Tables[0].Rows[0][0].ToString() == "1")
                {
                    result = true;
                }
            }
            if (IsAdmin(fbid))
            {
                result = true;
            }
            return result;
        }

        public bool CheckIfIsSeller(string fbid)
        {
            DataSet dstemp = ExecuteQuery("SELECT tab_added FROM permissions WHERE uid='" + fbid + "'");
            string strtemp = "0";
            if (dstemp.Tables[1].Rows[0]["tab_added"] != null)
            {
                strtemp = dstemp.Tables[1].Rows[0]["tab_added"].ToString();
            } 
            // 0 = means no tab added
            // 1 = means there is a tab
            if (strtemp == "1")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        public int CountPageVisits(Int64 fbid, string url)
        {
            int numvisits = 0;
            DataSet dstemp = Eventomatic_DB.SPs.ViewFbUsersLog(fbid, url).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["visits"] != DBNull.Value)
            {
                numvisits = Convert.ToInt32(dstemp.Tables[0].Rows[0]["visits"]);
            }
            return numvisits;
        }

        public int HasTriedDemoTicket(Int64 fbid,int Resource_Key)
        {
            int inttemp = 0;            
            DataSet dstemp = Eventomatic_DB.SPs.ViewIfTriedDemo(fbid, Resource_Key).GetDataSet();
            if (dstemp.Tables.Count > 0)
            {
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    if (dstemp.Tables[0].Rows[0]["Event_Key"] != DBNull.Value)
                    {
                        inttemp = Convert.ToInt32(dstemp.Tables[0].Rows[0]["Event_Key"]);
                    }
                }
            }            
            return inttemp;
        }

        public bool CheckIfIsSeller_NoSession(string fbid)
        {
            DataSet dstemp = ExecuteQuery_NoSession("SELECT tab_added FROM permissions WHERE uid='" + fbid + "'",Convert.ToInt64(fbid));
            string strtemp = "0";
            if (dstemp != null)
            {
                if (dstemp.Tables.Count > 1)
                {
                    if (dstemp.Tables[1].Columns.Contains("tab_added"))
                    {
                        if (dstemp.Tables[1].Rows[0]["tab_added"] != null)
                        {
                            strtemp = dstemp.Tables[1].Rows[0]["tab_added"].ToString();
                        }
                    }
                }               

            }
            
            // 0 = means no tab added
            // 1 = means there is a tab
            if (strtemp == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string CheckIfInGroup2(string fbid)
        {
            //if (System.Web.HttpContext.Current.Request.Url.AbsolutePath != "/FirstTime.aspx")
            string strreturn = "0";
            try
            {
                if (hdnFirstTime.Value == "0")
                {
                    DataSet dstemp = Eventomatic_DB.SPs.ViewIfInGroup(Convert.ToInt64(fbid)).GetDataSet();
                    if ((dstemp.Tables[0].Rows[0]["NumGroups"].ToString() == "0"))
                    {
                        //Not in a group, go to firsttime page
                        //Response.Redirect("FirstTime.aspx");
                        strreturn = "1";
                        //Response.Redirect("default.aspx");
                    }
                    else
                    {
                        strreturn = "0";
                    }
                }
                else
                {
                    strreturn = "0";
                }
            }
            catch
            {
                strreturn = "0";
            }

            return strreturn;
        }
        
        public bool ImgExists(int ImgType, string ImgName)
        { 
        //ImgType = 0 --> Event
        //ImgType = 1 --> Group
            if (ImgType == 0)
            {
                return File.Exists(System.Web.HttpContext.Current.Server.MapPath("/Images/Events/" + ImgName));
            }
            else
            {
                return File.Exists(System.Web.HttpContext.Current.Server.MapPath("/Images/Groups/" + ImgName));
            }            
        }

        public bool ImgResourceThumbExists(string ImgName)
        {
            
            return File.Exists(System.Web.HttpContext.Current.Server.MapPath("/Images/Groups/Thumbs/" + ImgName));
            
        }

        public string GetResourceThumbPic(string Resource_Key)
        {
            return System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/Images/Groups/Thumbs/" + Resource_Key + ".jpg";   
        }

        public string TicketsSoldProgressBarHTML(int Sold, int Capacity)
        {
            double Percent = Math.Round(100 * (float)Sold / (float)Capacity);
            if ((Sold == 0) || (Capacity == 0))
            {
                Percent = 0;
            }
            if (Sold > Capacity)
            {
                Percent = 100;
            }
            //return "<table width=250px height=30px border=1 class=ProgressBar_Table><tr><td width='" + Percent.ToString() + "%' style='background-color:#18243d;'>&nbsp;</td><td>&nbsp;</td></tr></table>";
            return "<div class='ProgressBar1'><div class='ProgressBar2' style='background-color: #18243d; width: " + Percent.ToString() + "%;'>" +
        "<div class='ProgressBar3'>" + Sold + "/" + Capacity + "</div></div></div>";

        }

        public string GetEventPic(string Event_Key)
        {
            if (ImgExists(0,Event_Key + ".jpg"))
            {
                return "/Images/Events/" + Event_Key + ".jpg";
            }
            else
            {
                return "/Images/Events/No_Image.jpg";
            }
        }

        public string GetResourcePic(string Resource_Key)
        {
            if (ImgExists(1,Resource_Key + ".jpg"))
            {
                return "/Images/Groups/" + Resource_Key + ".jpg";
            } 
            else
            {
                return "/Images/Groups/No_Image.jpg";
            }
        }

        public string GetResourceKey()
        {            
            if (ddlgroups.Items.Count == 0)
            {
                RadComboBox ddltemp = new RadComboBox();
                ddltemp = (RadComboBox)LoadGroupsList();
                return ddltemp.SelectedValue.ToString();
            }
            else { return ddlgroups.SelectedValue.ToString(); }
        }

        public string GetResourceName()
        {
            if (ddlgroups.Items.Count == 0)
            {
                RadComboBox ddltemp = new RadComboBox();
                ddltemp = (RadComboBox)LoadGroupsList();
                return ddltemp.SelectedItem.Text.ToString();
            }
            else { return ddlgroups.SelectedItem.Text.ToString(); }
        }

        public string GetEventKeyTx(int Tx_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(Tx_Key).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Event_Key"] != DBNull.Value)
                {
                    return dstemp.Tables[0].Rows[0]["Event_Key"].ToString();
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }            
        }
        
        public string GetEventKeyTicketKey(int Ticket_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicketSpecific2(Ticket_Key).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Event_Key"] != DBNull.Value)
                {
                    return dstemp.Tables[0].Rows[0]["Event_Key"].ToString();
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        public string GetResourceCurrencyTx(int Tx_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetailsTxnid(Tx_Key).GetDataSet(); //Eventomatic_DB.SPs.ViewResourceFromTxKey(Tx_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Currency"] != DBNull.Value)
            {
                return dstemp.Tables[0].Rows[0]["Currency"].ToString();
            }
            else
            {
                return "CAD";
            }
        }

        public string GetResourceEmail(int Resource_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)
            {
                return dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
            }
            else
            {
                return "Lorne@Lornestar.com";
            }
        }
        
        public string GetResourceEmailTx(int Tx_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromTxKey(Tx_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)
            {
                return dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
            }
            else
            {
                return "Lorne@Lornestar.com";
            }
        }



        public bool GetResourceThirdPartyPayPalTx(int Tx_Key)
        {
            //if is ThirdPartyPayPal means money deposited into their account
            //if is not ThirdPartyPayPal mean money deposited into Groupstore account

            //default is false
            bool isThirdPartyPayPal = false;
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromTxKey(Tx_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["ThirdPartyPayPal"] != DBNull.Value)
            {
               isThirdPartyPayPal = Convert.ToBoolean( dstemp.Tables[0].Rows[0]["ThirdPartyPayPal"].ToString());
            }
            return isThirdPartyPayPal;
        }
        

        public string GetResourceDescriptorTx(int Tx_Key)
        {
            //Gets descriptor for Credit Card Statement

            string strdescriptor = "Snappay";
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromTxKey(Tx_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Descriptor"] != DBNull.Value)
            {
                strdescriptor = dstemp.Tables[0].Rows[0]["Descriptor"].ToString();
            }
            return strdescriptor;
        }

        public string GetResourceNameTx(int Tx_Key)
        {
            //Gets descriptor for Credit Card Statement

            string strdescriptor = "Snappay";
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromTxKey(Tx_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Group_Name"] != DBNull.Value)
            {
                strdescriptor = dstemp.Tables[0].Rows[0]["Group_Name"].ToString();
            }
            return strdescriptor;
        }

        public string GetResourceCurrency(int Event_Key)
        {
            string strreturn = "CAD";
            DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Convert.ToInt32(GetResourceKeyEventKey(Event_Key))).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Desired_Currency"] != DBNull.Value)
                {
                    strreturn = dstemp.Tables[0].Rows[0]["Desired_Currency"].ToString();
                }
            }            
            return strreturn;
        }

        public string GetResourceCurrencyResourceKey(int Resource_Key)
        {
            string strreturn = "CAD";
            DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Desired_Currency"] != DBNull.Value)
                {
                    strreturn = dstemp.Tables[0].Rows[0]["Desired_Currency"].ToString();
                }
            }
            return strreturn;
        }

        public string GetResourceKeyEventKey(int Event_Key)
        {
            string strreturn = "0";
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromEventKey(Event_Key).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Resource_Key"] != DBNull.Value)
                {
                    strreturn = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
                }
            }                        
            return strreturn;
        }

        public string GetResourceKeytxKey(int tx_key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromTxKey(tx_key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Resource_Key"] != DBNull.Value)
            {
                return dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
            }
            else
            {
                return "0";
            }
        }
        

        public RadComboBox LoadGroupsList()
        {
            if (!IsPostBack)
            {

                
                //CheckIfInGroup(fbstruserid);


                long fbid = Convert.ToInt64(getfbid());//fbintuserid;
                DataSet dstemp = Eventomatic_DB.SPs.ViewListFBUserResources(fbid).GetDataSet();
                if (dstemp.Tables[0].Rows.Count == 1)
                {
                    string strGroupname = dstemp.Tables[0].Rows[0]["Group_Name"].ToString();
                    if (strGroupname.Length > 10)
                    {
                        strGroupname = strGroupname.Substring(0,10);
                    }
                    lblGroups.Text = "Current Group - " + strGroupname;
                    ddlgroups.Visible = false;
                }
                else
                {
                    ddlgroups.Visible = true;

                }
                ddlgroups.DataSource = dstemp.Tables[0];
                ddlgroups.DataTextField = "Group_Name";
                ddlgroups.DataValueField = "Resource_Key";
                ddlgroups.DataBind();            

                if ((Session["CurrentResource"] != null) && (ddlgroups.Items.FindItemByValue(Session["CurrentResource"].ToString()) != null))
                {ddlgroups.SelectedValue = Session["CurrentResource"].ToString();}
                
            }
            

            return ddlgroups;
        }
        

        public DateTime fbDateTime(int unixtimestamp)
        {                
                return new DateTime(1970,1,1,0,0,0).AddSeconds(unixtimestamp).AddHours(-3);
        }


        public void UpdateUltraWebGrid(Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgridtemp, int GridID, int Event_Key,bool AlwaysUpdate)
        {
            DataSet dstemp = UltraGridView(GridID,Event_Key);
            int deleterowcount = 0;
            bool FoundMatch;
            foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow r in Ultrawebgridtemp.Rows)
            {
                if (r.DataChanged == Infragistics.WebUI.UltraWebGrid.DataChanged.Modified)
                {
                    Update_DB(r, GridID,Event_Key);
                }
                else if (r.DataChanged == Infragistics.WebUI.UltraWebGrid.DataChanged.Added)
                {
                    Update_DB(r, GridID, Event_Key);
                   // Load_Existing();
                }
                else if (AlwaysUpdate)
                {
                    Update_DB(r, GridID, Event_Key);
                }
                FoundMatch = false;
                while ((!FoundMatch) && (deleterowcount < dstemp.Tables[0].Rows.Count))
                {
                    //Delete Stuff
                    if ((r.Cells[0].ToString() != dstemp.Tables[0].Rows[deleterowcount][0].ToString()) && (r.Cells[0].ToString() != "-1"))
                    {
                        //delete this row
                        DeleteRow(Convert.ToInt32(dstemp.Tables[0].Rows[deleterowcount][0]), GridID);
                    }
                    else
                    {
                        FoundMatch = true;
                    }
                    deleterowcount = deleterowcount + 1;
                }
            }
            while (deleterowcount < dstemp.Tables[0].Rows.Count)//delete rest of the 
            {
                if (dstemp.Tables[0].Rows[deleterowcount][0].ToString() != "-1")
                {
                    DeleteRow(Convert.ToInt32(dstemp.Tables[0].Rows[deleterowcount][0]), GridID);
                }                
                deleterowcount = deleterowcount + 1;
            }
        }

        /*GridIDs
         * ------
         * 0 - Ticket Information
         * 1 - Questions To ask
         */

        protected DataSet UltraGridView(int GridID,int Event_Key)
        {
            DataSet dstemp=null;
            switch (GridID)
            {
                case 0:
                    dstemp = Eventomatic_DB.SPs.ViewTicketAll(Event_Key).GetDataSet();
                    break;
                case 1:
                    dstemp = Eventomatic_DB.SPs.ViewQuestion(Event_Key).GetDataSet();
                    break;
                
            }
            return dstemp;
        }

        public void DeleteRow(int Key, int GridID)
        {
            switch (GridID)
            {
                case 0:
                    Eventomatic_DB.SPs.DeleteTicket(Key).Execute();
                    break;
                case 1:
                    Eventomatic_DB.SPs.DeleteQuestion(Key).Execute();
                    break;
                
            }
        }
        
        public void Update_DB(Infragistics.WebUI.UltraWebGrid.UltraGridRow r, int GridID, int Event_Key)
        {
                switch (GridID)
                {
                    case 0:
                        DateTime tempStartdateSelling = Convert.ToDateTime(r.Cells[2].Value);
                        DateTime tempEnddateSelling = Convert.ToDateTime(r.Cells[3].Value);
                        bool isdonation = false;
                        if (r.Cells[9].Value != null)
                        {
                            if (r.Cells[9].Value.ToString().ToLower().Contains("true"))
                            {
                                isdonation = true;
                            }
                        }
                        
                        StoredProcedure sp_UpdateTicket = Eventomatic_DB.SPs.UpdateTicket(Convert.ToInt32(r.Cells[0].Value), Event_Key, r.Cells[1].Value.ToString(), Convert.ToDecimal(r.Cells[4].Value), Convert.ToInt32(r.Cells[5].Value), tempStartdateSelling, tempEnddateSelling,0,isdonation);
                        sp_UpdateTicket.Execute();
                        int ticket_key = 0;
                        if (Convert.ToInt32(r.Cells[0].Value) == 0)
                        {
                            ticket_key = Convert.ToInt32(sp_UpdateTicket.Command.Parameters[7].ParameterValue.ToString());
                        }
                        else
                        {
                            ticket_key = Convert.ToInt32(r.Cells[0].Value);
                        }
                        Eventomatic_DB.SPs.DeleteTicketSellersTicketKey(ticket_key).Execute();
                        string strsellers = r.Cells[7].ToString();
                        string[] words = r.Cells[7].ToString().Split('|');
                        foreach (string word in words)
                        {
                            if (IsNumeric(word))
                            {
                               // Eventomatic_DB.SPs.UpdateTicketSellers(ticket_key, Event_Key, Convert.ToInt64(word)).Execute();
                            }
                        }
                        //Eventomatic_DB.SPs.UpdateTicket(Convert.ToInt32(r.Cells[0].Text), Event_Key, r.Cells[3].Text, Convert.ToDecimal(r.Cells[2].Text), Convert.ToInt32(r.Cells[4].Text), DateTime.Now, DateTime.Now).Execute();
                        break;
                    case 1:
                        Eventomatic_DB.SPs.UpdateQuestion(Convert.ToInt32(r.Cells[0].Text), Event_Key, r.Cells[1].Text,Convert.ToBoolean(r.Cells[2].Value),0,0).Execute();
                        break;
                }
        }

        public DataSet GetGroups()
        {
            fbuser fbuser = getfbuser();
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            //string result = wc.DownloadString("http://api.facebook.com/restserver.php?method=facebook.fql.query&query=SELECT%20fan_count%20FROM%20page%20WHERE%20page_id=36922302396");
            string result = wc.DownloadString("http://api.facebook.com/restserver.php?method=friends.get&uid=" + fbuser.UID.ToString() + "&token=" + fbuser.SessionKey);

            DataSet queryResults = new DataSet();
            System.IO.StringReader xmlReader = new System.IO.StringReader(result);
            queryResults.ReadXml(xmlReader);

            return queryResults;
        }

        public string GetCurrency(int Resource_Key)
        {
            string rtcurrency = "CAD";
            DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
            switch (dstemp.Tables[0].Rows[0]["Desired_Currency"].ToString())
            {
                case "CAD":
                    rtcurrency = "CAD";
                    break;
                case "USD":
                    rtcurrency = "USD";
                    break;
                case "EUR":
                    rtcurrency = "EUR";
                    break;
                case "GBP":
                    rtcurrency = "GBP";
                    break;
                case "ILS":
                    rtcurrency = "ILS";
                    break;
            }
            return rtcurrency;
        }

        public string GetCurrencyFromCountry(string strCountryCode)
        {
            string rtcurrency = "USD";
            switch (strCountryCode.ToUpper())
            {
                case "CA":
                    rtcurrency = "CAD";
                    break;
                case "US":
                    rtcurrency = "USD";
                    break;
                case "DE":
                    rtcurrency = "EUR";
                    break;
                case "GB":
                    rtcurrency = "GBP";
                    break;
                case "IL":
                    rtcurrency = "ILS";
                    break;
            }
            return rtcurrency;
        }

        public decimal MassPaymentFeeCalculate(decimal Owed)
        {
            decimal MassPayFee = Owed * Convert.ToDecimal(0.02);
            if (MassPayFee > 1)
            {
                MassPayFee = 1;
            }
            return (-1 * decimal.Round(MassPayFee,2));
        }

        public string GetRevenue(Hashtable hstemp)
        {
            
            string strreturn = "";            
            int count = 0;
            foreach (DictionaryEntry de in hstemp)
            {                
                if ((de.Key.ToString() == "CAD") && (Convert.ToDecimal(de.Value)>0))
                {
                    strreturn += "$" + de.Value + "CAD";
                    count++;
                }
                else if ((de.Key.ToString() == "USD") && (Convert.ToDecimal(de.Value)>0))
                {
                    if (count > 0)
                    {
                        strreturn += ", ";
                    }                
                    strreturn += "$" + de.Value + "USD";
                    count++;
                }
                else if ((de.Key.ToString() == "EUR") && (Convert.ToDecimal(de.Value)>0))
                {
                    if (count > 0)
                    {
                        strreturn += ", ";
                    }                
                    strreturn += "€" + de.Value + "EUR";
                    count++;
                }
                else if ((de.Key.ToString() == "GBP") && (Convert.ToDecimal(de.Value)>0))
                {
                    if (count > 0)
                    {
                        strreturn += ", ";
                    }                
                    strreturn += "£" + de.Value + "GBP";
                    count++;
                }
                else if ((de.Key.ToString() == "ILS") && (Convert.ToDecimal(de.Value)>0))
                {
                    if (count > 0)
                    {
                        strreturn += ", ";
                    }                
                    strreturn += "₪" + de.Value + "ILS";
                    count++;
                }
            }
            if (strreturn == "")
            {
                strreturn = "$0.00";
            }
            return strreturn;
        }

        public Hashtable GetRevenue_Hashtable_Empty()
        {
            Hashtable hstemp = new Hashtable();
            hstemp.Add("CAD", 0);
            hstemp.Add("USD", 0);
            hstemp.Add("EUR", 0);
            hstemp.Add("GBP", 0);
            hstemp.Add("ILS", 0);
            return hstemp;
        }

        public Hashtable GetRevenue_Hashtable_Decode(string strtemp)
        {
            Hashtable hstemp = GetRevenue_Hashtable_Empty();
            string[] words = strtemp.Split(',');
            foreach (string word in words)
            {
                string wordtemp = "";
                if (word.Contains("CAD"))
                {
                    wordtemp = word.Replace("CAD", "");
                    wordtemp = wordtemp.Replace("$", "");
                    hstemp["CAD"] = wordtemp;
                }
                else if (word.Contains("USD"))
                {
                    wordtemp = word.Replace("USD", "");
                    wordtemp = wordtemp.Replace("$", "");
                    hstemp["USD"] = wordtemp;
                }
                else if (word.Contains("EUR"))
                {
                    wordtemp = word.Replace("EUR", "");
                    wordtemp = wordtemp.Replace("€", "");
                    hstemp["EUR"] = wordtemp;
                }
                else if (word.Contains("GBP"))
                {
                    wordtemp = word.Replace("GBP", "");
                    wordtemp = wordtemp.Replace("£", "");
                    hstemp["GBP"] = wordtemp;
                }
                else if (word.Contains("ILS"))
                {
                    wordtemp = word.Replace("ILS", "");
                    wordtemp = wordtemp.Replace("₪", "");
                    hstemp["ILS"] = wordtemp;
                }
            }
            return hstemp;
        }

        public Hashtable GetAmount_Owed(int resource_key)
        {
            Hashtable hstemp = GetRevenue_Hashtable_Empty();
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceAmountOwed(resource_key).GetDataSet();

            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_CAD"].ToString()) > 0)
                {
                    hstemp["CAD"] = dstemp.Tables[0].Rows[0]["Owed_CAD"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_USD"].ToString()) > 0)
                {
                    hstemp["USD"] = dstemp.Tables[0].Rows[0]["Owed_USD"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_EUR"].ToString()) > 0)
                {
                    hstemp["EUR"] = dstemp.Tables[0].Rows[0]["Owed_EUR"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_GBP"].ToString()) > 0)
                {
                    hstemp["GBP"] = dstemp.Tables[0].Rows[0]["Owed_GBP"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_ILS"].ToString()) > 0)
                {
                    hstemp["ILS"] = dstemp.Tables[0].Rows[0]["Owed_ILS"].ToString();
                }
            }

            return hstemp;
        }

        public Hashtable GetAmount_Owed_Referral(Int64 fbid)
        {
            Hashtable hstemp = GetRevenue_Hashtable_Empty();
            DataSet dstemp = Eventomatic_DB.SPs.ViewReferralAmountOwed(fbid).GetDataSet();

            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_CAD"].ToString()) > 0)
                {
                    hstemp["CAD"] = dstemp.Tables[0].Rows[0]["Owed_CAD"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_USD"].ToString()) > 0)
                {
                    hstemp["USD"] = dstemp.Tables[0].Rows[0]["Owed_USD"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_EUR"].ToString()) > 0)
                {
                    hstemp["EUR"] = dstemp.Tables[0].Rows[0]["Owed_EUR"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_GBP"].ToString()) > 0)
                {
                    hstemp["GBP"] = dstemp.Tables[0].Rows[0]["Owed_GBP"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Owed_ILS"].ToString()) > 0)
                {
                    hstemp["ILS"] = dstemp.Tables[0].Rows[0]["Owed_ILS"].ToString();
                }
            }

            return hstemp;
        }

        public Hashtable GetRevenue_Hashtable(int Event_Key, int type)
        {
            //type = 0 Revenue / type = 1 Paid out / type = 2 Net Revenue (Resource) / type = 3 Net Revenue Paid out (fbid)
            DataSet dstemp;
            if (type == 1)
            {
                dstemp = Eventomatic_DB.SPs.ViewEventPaidOutCurrency(Event_Key).GetDataSet();
            }
            else if (type == 2)
            {
                dstemp = Eventomatic_DB.SPs.ViewResourceNetRevenueCurrency(Event_Key).GetDataSet();
            }
            else if (type == 3)
            {
                dstemp = Eventomatic_DB.SPs.ViewResourceNetRevenuePaidoutCurrency(Event_Key).GetDataSet();
            }
            else
            {
                dstemp = Eventomatic_DB.SPs.ViewEventRevenueCurrency(Event_Key).GetDataSet();
            }

            Hashtable hstemp = GetRevenue_Hashtable_Empty();
            if (dstemp.Tables[0].Rows.Count > 0)
            {                
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Revenue_CAD"].ToString()) > 0)
                {
                    hstemp["CAD"] = dstemp.Tables[0].Rows[0]["Revenue_CAD"].ToString();                    
                }            
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Revenue_USD"].ToString()) > 0)
                {
                    hstemp["USD"] = dstemp.Tables[0].Rows[0]["Revenue_USD"].ToString();                    
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Revenue_EUR"].ToString()) > 0)
                {
                    hstemp["EUR"] = dstemp.Tables[0].Rows[0]["Revenue_EUR"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Revenue_GBP"].ToString()) > 0)
                {
                    hstemp["GBP"] = dstemp.Tables[0].Rows[0]["Revenue_GBP"].ToString();
                }
                if (Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Revenue_ILS"].ToString()) > 0)
                {
                    hstemp["ILS"] = dstemp.Tables[0].Rows[0]["Revenue_ILS"].ToString();
                }
            }
            return hstemp;
        }

        public Hashtable Divide_Hashtable(Hashtable hstemp, decimal divamount)
        {
            Hashtable hstemp2 = GetRevenue_Hashtable_Empty();
            foreach (DictionaryEntry de in hstemp)
            {
                hstemp2[de.Key] = decimal.Round((Convert.ToDecimal(de.Value) * divamount),2);
            }
            return hstemp2;
        }

        public void txout_EmailTransfer(string strDetails)
        {
            string thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/txout_Email.txt"));
            thebody = thebody.Replace("TIMETX", DateTime.Now.ToString());
            thebody = thebody.Replace("PAGEMESSAGE", strDetails);
            string Toemail = System.Configuration.ConfigurationSettings.AppSettings.Get("ErrorToEmail").ToString();
            Send_Email SE = new Send_Email();
            SE.Send_Email_Function("Txout@theGroupstore.com", Toemail, "Money has been Tx out", thebody, 0);
        }

        public DataSet ExecuteQuery(string queryCommand)
        {
            string errorInfo = String.Empty;
            DataSet queryResults = null;

            try
            {
                fbuser fbuser = getfbuser();

                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
                //string result = wc.DownloadString("http://api.facebook.com/restserver.php?method=facebook.fql.query&query=SELECT%20fan_count%20FROM%20page%20WHERE%20page_id=36922302396");
                string result = wc.DownloadString("https://api.facebook.com/method/fql.query?query=" + queryCommand + "&access_token=" + fbuser.AccessToken);

                /*string xmlDataReturned = API.fql.query(queryCommand);*/

                queryResults = new DataSet();
                System.IO.StringReader xmlReader = new System.IO.StringReader(result);
                queryResults.ReadXml(xmlReader);

                return queryResults;
            }

            catch (Exception ex_exec_query)
            {
                errorInfo = "Failed to exec [" + queryCommand + "]:" + ex_exec_query.Message;
                return queryResults;
            }
        }

        public DataSet ExecuteQuery_NoSession(string queryCommand,Int64 fbid)
        {
            string errorInfo = String.Empty;
            DataSet queryResults = null;

            try
            {
                DataSet dstempfbuser = Eventomatic_DB.SPs.ViewFbUsers(fbid).GetDataSet();

                fbuser fbuser = new fbuser();
                fbuser.AccessToken = dstempfbuser.Tables[0].Rows[0]["Access_Token"].ToString();
                fbuser.SessionKey = dstempfbuser.Tables[0].Rows[0]["Session_Key"].ToString();

                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
                //string result = wc.DownloadString("http://api.facebook.com/restserver.php?method=facebook.fql.query&query=SELECT%20fan_count%20FROM%20page%20WHERE%20page_id=36922302396");
                string result = wc.DownloadString("https://api.facebook.com/method/fql.query?query=" + queryCommand + "&access_token=" + fbuser.AccessToken);

                /*string xmlDataReturned = API.fql.query(queryCommand);*/

                queryResults = new DataSet();
                System.IO.StringReader xmlReader = new System.IO.StringReader(result);
                queryResults.ReadXml(xmlReader);

                return queryResults;
            }

            catch (Exception ex_exec_query)
            {
                errorInfo = "Failed to exec [" + queryCommand + "]:" + ex_exec_query.Message;
                return queryResults;
            }
        }

        public string ReplaceLinks(string arg)
        //Replaces web and email addresses in text with hyperlinks
        {
            Regex urlregex = new Regex(@"(^|[\n ])((www|ftp)\.[^ ,""\s<]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            arg = urlregex.Replace(arg,
              m => String.Format(" <a href=\"http://{0}\" target=\"_blank\">{1}</a> ",
                m.Groups[2].Value,
                m.Groups[2].Value.Length > 27 ? m.Groups[2].Value.Substring(0, 27) + "..." : m.Groups[2].Value));


            Regex httpurlregex = new Regex(@"(^|[\n ])((http://www\.|http://|https://)[^ ,""\s<]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            arg = httpurlregex.Replace(arg,
              m => String.Format(" <a href=\"{0}\" target=\"_blank\">{1}</a> ",
                m.Groups[2].Value,
                m.Groups[2].Value.Length > 27 ? m.Groups[2].Value.Substring(0, 27) + "..." : m.Groups[2].Value));

            return arg;
        }

        public Boolean IsSoldOut(int Ticket_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicketSpecificSoldout(Ticket_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Sold_Out"] != DBNull.Value)
            {
                if (dstemp.Tables[0].Rows[0]["Sold_Out"].ToString() == "Available")
                {
                    return false;
                }
                else
                { return true; }
            }
            else
            {
                return false;
            }
        }

        public Boolean HasPaypalEmail(int Event_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewHasPaypalEmail(Event_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean HasPaypalEmail_ResourceKey(int Resource_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewHasPaypalEmailResourceKey(Resource_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean IsSoldOutEvent(int Event_Key)
        {
            bool IsSoldout = false;
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicketsSoldout(Event_Key).GetDataSet();
            foreach (DataRow r in dstemp.Tables[0].Rows)
            {
                string strtemp = r["Sold_Out"].ToString();
                if (r["Sold_Out"].ToString() == "Sold Out")
                {
                    IsSoldout = true;
                }
            }
            return IsSoldout;
        }

        public Boolean IsNumeric(string s)
        {
            try
            {
                Int64.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Boolean IsDemo(int Event_Key)
        {
            //False = Trial(Default) & True = Live
            DataSet dstemp = Eventomatic_DB.SPs.ViewPaypalEmail(Event_Key).GetDataSet();
            Boolean Demo = true;
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Demo"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Demo"].ToString()) == true)
                    {
                        Demo = false;
                    }
                }
            }            
            return Demo;
        }

        public Boolean IsDemo_Payforward(int Tx_Key)
        {
            //False = Trial(Default) & True = Live
            DataSet dstemp = Eventomatic_DB.SPs.ViewPayForwardDetails(Tx_Key).GetDataSet();
            Boolean Demo = true;
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Demo"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Demo"].ToString()) == true)
                    {
                        Demo = false;
                    }
                }
            }
            return Demo;
        }

        public Boolean IsDemo_ResourceKey(int Resource_Key)
        {
            //False = Trial(Default) & True = Live
            DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
            Boolean Demo = false;
            if (dstemp.Tables[0].Rows[0]["Demo"] != DBNull.Value)
            {
                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Demo"].ToString()) == true)
                {
                    Demo = true;
                }
            }
            return Demo;
        }
        
        public bool isEmail(string inputEmail)
        {
            //inputEmail = NulltoString(inputEmail);
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public bool isAlphaNumeric(string inputstr)
        {
            /*Regex pattern = new Regex(@"^([a-zA-Z0-9])$");
            if (pattern.IsMatch(inputstr))
            {
                return true;
            }
            else
            {
                return false;
            }*/
            return Regex.IsMatch(inputstr, @"^[a-zA-Z0-9 '-]*$");

        }

        public bool IsUniqueGroupStore(string theName)
        {
            string strtemp = "";
            bool IsUnique = true;
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceAll(0).GetDataSet();
            foreach (DataRow r in dstemp.Tables[0].Rows)
            {
                strtemp = "";
                if (r["Group_Name"] != DBNull.Value)
                {
                    strtemp = r["Group_Name"].ToString();
                }
                if (theName.Replace(" ", "").ToUpper() == strtemp.Replace(" ", "").ToUpper())
                {
                    IsUnique = false;
                }
            }
            return IsUnique;
        }   

        public void savepicurl(string picurl,string resourcekey)
        {
            string strtempfbimg = picurl; 

            string strImgTemp = "Temp" + resourcekey + ".jpg";
            string strPath = Server.MapPath("/Images/Events/" + strImgTemp);

            //save the img locally

            byte[] b;
            System.Net.HttpWebRequest myReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strtempfbimg);
            System.Net.WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            //int i;
            using (BinaryReader br = new BinaryReader(stream))
            {
                //i = (int)(stream.Length);
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();

            FileStream fs = new FileStream(strPath, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                w.Write(b);
            }
            finally
            {
                fs.Close();
                w.Close();
            }
        }

        public void savepicurlEventKey(string picurl, string eventkey)
        {
            string strtempfbimg = picurl;

            string strImgTemp = eventkey.ToString() + ".jpg";
            string strPath = HttpContext.Current.Server.MapPath("/Images/Events/" + strImgTemp);

            //save the img locally

            byte[] b;
            System.Net.HttpWebRequest myReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strtempfbimg);
            System.Net.WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            //int i;
            using (BinaryReader br = new BinaryReader(stream))
            {
                //i = (int)(stream.Length);
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();

            FileStream fs = new FileStream(strPath, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                w.Write(b);
            }
            finally
            {
                fs.Close();
                w.Close();
            }
        }

        public void savepicurl2(string picurl, string strPath)
        {
            //save the img locally

            byte[] b;
            System.Net.HttpWebRequest myReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(picurl);
            System.Net.WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            //int i;
            using (BinaryReader br = new BinaryReader(stream))
            {
                //i = (int)(stream.Length);
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();

            FileStream fs = new FileStream(strPath, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                w.Write(b);
            }
            finally
            {
                fs.Close();
                w.Close();
            }
        }

        public Eventomatic.Addons.groupstore_event Importfbevent(string eid, Int64 fbid)
        {
            Eventomatic.Addons.groupstore_event gsevent = new Eventomatic.Addons.groupstore_event();
            gsevent.Eid = eid;
            DataSet dstemp = ExecuteQuery_NoSession("SELECT creator,name,start_time, end_time, pic_big, host, location, description, update_time FROM event WHERE eid='" + eid + "'",fbid);

            gsevent.Eventname = dstemp.Tables[1].Rows[0]["name"].ToString();
            gsevent.Host = dstemp.Tables[1].Rows[0]["host"].ToString();
            gsevent.EventBegins = fbDateTime(Convert.ToInt32(dstemp.Tables[1].Rows[0]["start_time"].ToString())).ToLocalTime();
            gsevent.EventEnds = fbDateTime(Convert.ToInt32(dstemp.Tables[1].Rows[0]["end_time"].ToString())).ToLocalTime();
            gsevent.Location = dstemp.Tables[1].Rows[0]["location"].ToString();
            gsevent.Additionalcomments = dstemp.Tables[1].Rows[0]["description"].ToString();
            gsevent.Imageurl = dstemp.Tables[1].Rows[0]["pic_big"].ToString();

            //figure out tickets
            groupstore_ticket temptickets = new groupstore_ticket();
            temptickets.Description = "General Admission";
            temptickets.BeginSelling = DateTime.Now;
            temptickets.SellingDeadline = gsevent.EventBegins.AddHours(-3);
            temptickets.Capacity = 100;

            int tempticketprice = 15;
            //if ((gsevent.Additionalcomments.ToLower().Contains("ticket")) || (gsevent.Additionalcomments.ToLower().Contains("tix")) || (gsevent.Additionalcomments.ToLower().Contains("early bird")) || (gsevent.Additionalcomments.Contains("$")) || (gsevent.Additionalcomments.Contains("buck")) || (gsevent.Additionalcomments.Contains("dollar")))
            if (gsevent.Additionalcomments.ToLower().Contains("$"))
            //means some sort of chance can figure out how much tix are
            {
                string[] descriptionrows = gsevent.Additionalcomments.Split('\n');
                Hashtable htdollarsignrows = new Hashtable();
                int counter = 0;
                foreach (string descrow in descriptionrows)
                {
                    if (descrow.Contains("$"))
                    {
                        htdollarsignrows.Add(counter, descrow);
                        counter += 1;
                    }
                }

                int chosenrow = 0;
                if (htdollarsignrows.Count > 1) //more than 1 line with $ sign, gotta decide which row to keep
                {
                    Hashtable htrankrows = new Hashtable();
                    for (int i = 0; i < counter; i++)
                    {
                        htrankrows.Add(i, 0);
                        if ((htdollarsignrows[i].ToString().ToLower().Contains("ticket")) || (htdollarsignrows[i].ToString().ToLower().Contains("tix")))
                        {
                            htrankrows[i] = Convert.ToInt32(htrankrows[i]) + 30;
                        }
                        if (htdollarsignrows[i].ToString().ToLower().Contains("admission"))
                        {
                            htrankrows[i] = Convert.ToInt32(htrankrows[i]) + 15;
                        }
                        if (htdollarsignrows[i].ToString().ToLower().Contains("early bird"))
                        {
                            htrankrows[i] = Convert.ToInt32(htrankrows[i]) + 30;
                        }

                        if (Convert.ToInt32(htrankrows[i]) > Convert.ToInt32(htrankrows[chosenrow]))
                        {
                            chosenrow = i;
                        }
                    }
                }

                //By now only have 1 row
                string tempdollarsignword = "";
                //if (htdollarsignrows[chosenrow].ToString().IndexOf("$") == htdollarsignrows[chosenrow].ToString().LastIndexOf("$"))
                //{//There is only 1 $ in the row
                int charposition = htdollarsignrows[chosenrow].ToString().IndexOf("$") + 1;
                //while (htdollarsignrows[chosenrow].ToString()[charposition] != ' ')//keep going till find a space
                try
                {
                    while (IsNumeric(htdollarsignrows[chosenrow].ToString()[charposition].ToString()))
                    {
                        //if (Master.IsNumeric(htdollarsignrows[chosenrow].ToString()[charposition].ToString()))
                        //{
                        tempdollarsignword += htdollarsignrows[chosenrow].ToString()[charposition];
                        charposition += 1;
                        //}                    
                    }
                }
                catch
                {
                    tempdollarsignword = "";
                }
                
                /*}
                else //There's more than 1 $ sign
                {
                }*/
                if (tempdollarsignword != "")
                {
                    tempticketprice = Convert.ToInt32(tempdollarsignword);
                }
                else
                {
                    tempticketprice = 15;
                }
            }
            else //don't know how much tix are so guess on general admission tix
            {
                tempticketprice = 15;
            }
            temptickets.Price = tempticketprice;

            gsevent.Tickets = new groupstore_ticket[1];
            gsevent.Tickets.SetValue(temptickets, 0);

            //ticket num
            gsevent.Ticketnum = 0;
            Random random = new Random();
            gsevent.Ticketnum = random.Next(1500, 7000);

            return gsevent;
        }

        public DataSet RankEvents(string strGroupname)
        {
            string strPageAdminFQL = "";
            string fbid = getfbid();
            //Check if admin of any pages, if so add it to Events FQL call
            //Get Pages is admin of
            DataSet dstemp = ExecuteQuery("SELECT page_id FROM page_admin WHERE uid =" + fbid);
            if (dstemp != null)
            {
                if (dstemp.Tables.Count >= 2)
                {
                    foreach (DataRow r in dstemp.Tables[1].Rows)
                    {
                        //Is a page admin
                        strPageAdminFQL += " OR uid=" + r["page_id"].ToString();                        
                    }
                }
            }
            
            
            //Check if has any events            
            dstemp = ExecuteQuery("SELECT creator,name,start_time,pic_small, eid, host, description, update_time  FROM event WHERE eid IN (SELECT eid FROM event_member WHERE uid='" + fbid + "'" + strPageAdminFQL + ") AND start_time > now()");

            if (dstemp != null)
            {
                if (dstemp.Tables.Count > 1)//At least 2 events to sell
                {
                    dstemp.Tables[1].Columns.Add("rank", System.Type.GetType("System.Int32"));
                    foreach (DataRow r in dstemp.Tables[1].Rows) //Go through events
                    {
                        if (r.Table.Columns.Contains("start_time"))
                        {
                            r["start_time"] = fbDateTime(Convert.ToInt32(r["start_time"].ToString())).ToString("MMMM dd, yyyy");
                        }
                        if (r.Table.Columns.Contains("name"))
                        {
                            if (r["name"].ToString().Length > 100)
                            {
                                r["name"] = r["name"].ToString().Substring(0, 100);
                            }
                        }
                        //Give each event a ranking
                        r["rank"] = 0;

                        DateTime tempupdatetime = fbDateTime(Convert.ToInt32(r["update_time"].ToString()));
                        if ((tempupdatetime.AddDays(-1) < DateTime.Now) && (tempupdatetime > DateTime.Now))
                        //Was modified in the past day
                        {
                            r["rank"] = Convert.ToInt32(r["rank"].ToString()) + 30;
                        }
                        if ((tempupdatetime.AddHours(-1) < DateTime.Now) && (tempupdatetime > DateTime.Now))
                        //Was modified in the past hour
                        {
                            r["rank"] = Convert.ToInt32(r["rank"]) + 50;
                        }
                        if ((tempupdatetime.AddMinutes(-15) < DateTime.Now) && (tempupdatetime > DateTime.Now))
                        //Was modified in the past 15 minutes
                        {
                            r["rank"] = Convert.ToInt32(r["rank"]) + 70;
                        }
                        if (r["host"].ToString().ToLower().Contains(strGroupname.ToLower()))
                        //Has the group name in host
                        {
                            r["rank"] = Convert.ToInt32(r["rank"]) + 70;
                        }
                        if (r["description"].ToString().ToLower().Contains(strGroupname.ToLower()))
                        //Has the group name in description
                        {
                            r["rank"] = Convert.ToInt32(r["rank"]) + 20;
                        }
                        if (r["description"].ToString().ToLower().Contains("ticket"))
                        //Has the word 'ticket' in description
                        {
                            r["rank"] = Convert.ToInt32(r["rank"]) + 35;
                        }
                        if ((strPageAdminFQL.Contains(r["creator"].ToString())) || (fbid == r["creator"].ToString()))
                        //They were a creator of the event
                        {
                            r["rank"] = Convert.ToInt32(r["rank"]) + 40;
                        }
                        //Give each event a ranking
                    }
                }
            }
            return dstemp;
        }

        protected void LoadImportEvents()
        {
            //Use same analytics used in firsttime page

            Site sitetemp = new Site();
            DataSet dstemp = sitetemp.RankEvents(GetResourceName());

            if (dstemp != null)
            {                
                if (dstemp.Tables.Count > 1)//At least 2 events to sell
                {
                    DataTable dttemp = dstemp.Tables[1].Copy();
                    //Order rankings                    
                    dttemp.DefaultView.Sort = "[rank] desc";
                    UltraWebGrid1.DataSource = dttemp;
                    UltraWebGrid1.DataBind();
                }
                else //No events so go directly to edit_event.aspx
                {
                    Response.Redirect("Edit_Event.aspx");
                }                
            }
            else //error in running query to see if any events to import
            {
                //pnlImport.Visible = false;
                //lblNoImport.Visible = true;
                Response.Redirect("Edit_Event.aspx");
            }

            //Add Sell your event in FB Import Events
            bool IsUnique;
            foreach (UltraGridRow row in UltraWebGrid1.Rows)
            {
                if (row.Cells[1].Text.ToString().Length > 30)
                {
                    row.Cells[1].Text = row.Cells[1].Text.Substring(0, 30);
                }

                row.Cells[3].Text = "<a href='" + System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Edit_Event.aspx?eid=" + row.Cells[4].ToString() + "' target='_top'><b>Sell Event!</b></a>";
                
            }
            Page.RegisterStartupScript("popup4", "<script language=javascript>popup4();</script>");
        }

        public void getfbPages(Int64 fbid)
        {
            
            DataSet dstemp;            
            //First find which pages the person is admin for
            dstemp = ExecuteQuery("SELECT page_id, type FROM page_admin WHERE uid=" + fbid);
            if (dstemp != null)
            {
                if (dstemp.Tables.Count >= 2)
                {
                    foreach (DataRow r in dstemp.Tables[1].Rows)
                    {
                        Eventomatic_DB.SPs.UpdateFbPages(fbid, Convert.ToInt64(r["page_id"])).Execute();
                    }
                }
            }
        }

        public DataTable getFriendslist(fbuser fbuser)
        {
            DataTable dtFriendsList = new DataTable("FBFriends");
            DataColumn cltemp;
            DataColumn cltemp2;
            DataRow rtemp;

            cltemp = new DataColumn();
            cltemp.DataType = System.Type.GetType("System.String");
            cltemp.ColumnName = "fbid";
            cltemp.ReadOnly = false;
            cltemp.Unique = true;
            dtFriendsList.Columns.Add(cltemp);
            cltemp2 = new DataColumn();
            cltemp2.DataType = System.Type.GetType("System.String");
            cltemp2.ColumnName = "Name";
            cltemp2.ReadOnly = false;
            cltemp2.Unique = true;
            dtFriendsList.Columns.Add(cltemp2);
            Hashtable fbht = new Hashtable();

            ListItem litemp = new ListItem();

            //System.Collections.Generic.IList<facebook.Schema.user> friends = Master.API.friends.getUserObjects();


            string fbid = fbuser.UID.ToString();
            DataSet dstemp = ExecuteQuery("SELECT uid, name FROM user WHERE  uid IN (SELECT uid2 FROM friend WHERE uid1 ='" + fbid + "')");
            //foreach (facebook.Schema.user friend in friends)
            foreach (DataRow r in dstemp.Tables[1].Rows)
            {
                //ListItem li = new ListItem(friend.name, friend.uid.ToString());
                //lstFacebookFriends.Items.Add(li);
                litemp.Value = r["uid"].ToString(); //friend.uid.ToString();
                litemp.Text = r["name"].ToString();//friend.name.ToString();
                if ((!fbht.ContainsValue(r["name"].ToString().ToUpper())))
                {
                    rtemp = dtFriendsList.NewRow();
                    rtemp[0] = r["uid"].ToString();
                    rtemp[1] = r["name"].ToString();
                    dtFriendsList.Rows.Add(rtemp);
                    fbht.Add(r["uid"].ToString(), r["name"].ToString().ToUpper());
                }

            }
            dtFriendsList.DefaultView.Sort = "Name asc";
            return dtFriendsList;
        }

        public double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public string GetCurrencySymbol(int Event_Key)
        {
            string strtemp = GetResourceCurrency(Event_Key);
            string strsymbol = "$";
            
            if (strtemp.Contains("EUR"))
            {
                strsymbol = "€";                
            }
            else if (strtemp.Contains("GBP"))
            {
                strsymbol = "£";                
            }
            else if (strtemp.Contains("ILS"))
            {
                strsymbol = "₪";                
            }
            return strsymbol;
        }

        public string GetCurrencySymbol(string strtemp)
        {            
            string strsymbol = "$";

            if (strtemp.Contains("EUR"))
            {
                strsymbol = "€";
            }
            else if (strtemp.Contains("GBP"))
            {
                strsymbol = "£";
            }
            else if (strtemp.Contains("ILS"))
            {
                strsymbol = "₪";
            }
            return strsymbol;
        }

        public string RemoveCurrencySymbol(string strtemp)
        {
            string strreturn = strtemp.Replace("$", "");
            strreturn = strreturn.Replace("€", "");
            strreturn = strreturn.Replace("£", "");
            strreturn = strreturn.Replace("₪", "");
            return strreturn;

        }
         public bool IsDecimal(string theValue)
        {
            try
            {
                Convert.ToDouble(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        } //IsDecimal


         public void RemoveTicketFromCart(int tx_key, int ticket_key)
         {
             Eventomatic_DB.SPs.UpdateTransactionCartRemoveTicket(tx_key, ticket_key).Execute();
         }

         public int SetupTx(GridView GridView1, decimal ServiceFee, decimal OverallTotal, int Event_Key, int tx_key_existing, string FirstName, string LastName, string FreeEmail, string specificuser, bool IsCalendar, DateTime Start_Date,DateTime End_Date)
         {             
             decimal TotalSum;
             System.Web.UI.WebControls.Label lblTotalSum;
             System.Web.UI.WebControls.DropDownList ddlQuantity;
             string strPurchaseDescription;
             string PurchaseDescription = "";
             Hashtable Tickets_Purchased = new Hashtable();             


             //Figure out Purchase Description & Setup Hashtable
             foreach (GridViewRow gvrow in GridView1.Rows)
             {
                 if (gvrow.RowIndex != GridView1.Rows.Count - 1)
                 {
                     TotalSum = 0;
                     if (gvrow.RowType == DataControlRowType.DataRow)
                     {
                         bool isdonation = false;
                         lblTotalSum = (System.Web.UI.WebControls.Label)gvrow.FindControl("lblPrice");
                         System.Web.UI.WebControls.TextBox txtDonate = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("txtDonate");
                         ddlQuantity = (System.Web.UI.WebControls.DropDownList)gvrow.FindControl("ddlQuantity");
                         System.Web.UI.WebControls.Label lblTicketKey = (System.Web.UI.WebControls.Label)gvrow.FindControl("lblTicketKey");

                         if (lblTotalSum.Visible == false)
                         {
                             isdonation = true;
                         }

                         if (isdonation)
                         {
                             TotalSum = Convert.ToDecimal(txtDonate.Text);
                             //OverallTotal += TotalSum;
                         }
                         else
                         {
                             TotalSum = Convert.ToDecimal(lblTotalSum.Text.Replace("$", ""));
                             //OverallTotal += TotalSum * ddlQuantity.SelectedIndex;
                         }

                         if (((ddlQuantity.SelectedIndex > 0) && (ddlQuantity.Visible)) || ((Convert.ToDecimal(txtDonate.Text) > 0) && (isdonation)))
                         {
                             strPurchaseDescription = gvrow.Cells[0].Text; //.FindControl("Ticket_Description");
                             if (PurchaseDescription.Length > 0)
                             {
                                 PurchaseDescription += ", ";
                             }
                             PurchaseDescription += strPurchaseDescription;
                             if (isdonation)
                             {
                                 Tickets_Purchased.Add(lblTicketKey.Text, TotalSum + "d");
                             }
                             else
                             {

                                 Tickets_Purchased.Add(lblTicketKey.Text, ddlQuantity.SelectedIndex);
                                 //Tickets_Purchased.Add(Eventomatic_DB.SPs.ViewTicketSpecific(gvrow.Cells[0].Text, Event_Key, TotalSum).GetDataSet().Tables[0].Rows[0]["Ticket_Key"].ToString(), ddlQuantity.SelectedIndex);
                             }

                         }
                     }
                 }

             }
             string tempTx_Key = "0";             
             Site sitetemp = new Site();
             int resource_key = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));
             string currency = sitetemp.GetCurrency(resource_key);

             bool IsFree = false;

             if (tx_key_existing == 0)
             {
                 if (OverallTotal > 0) //Money being spent
                 {
                     //Add Service fee to overall total                        

                     //OverallTotal += decimal.Round(ServiceFee, 2);//(OverallTotal * SFP) + SFC;

                     //Get IP Address Request.UserHostAddress
                     //update Transaction & get tx_Key
                     StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "", "", 1, "", "", 0, "", "", "", "", "", "", "", FirstName, LastName, ServiceFee, HttpContext.Current.Request.UserHostAddress);
                     sp_UpdateTransaction.Execute();
                     tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
                 }
                 else if ((Tickets_Purchased.Count > 0) && (OverallTotal == 0))//Free Events
                 {
                     IsFree = true;
                     //update Transaction & get tx_Key
                     StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "", FreeEmail, 3, "", "", 0, "", "", "", FreeEmail, "", "", "", FirstName, LastName, 0, HttpContext.Current.Request.UserHostAddress);
                     sp_UpdateTransaction.Execute();
                     tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
                 }
             }
             else
             {
                 tempTx_Key = tx_key_existing.ToString();
                 Eventomatic_DB.SPs.UpdateTransactionAlreadyCart(Convert.ToInt32(tempTx_Key), decimal.Round(OverallTotal, 2), ServiceFee).Execute();
             }

             //update Tickets Purchased
             foreach (DictionaryEntry de in Tickets_Purchased)
             {
                 if (de.Value.ToString().Contains("d"))
                 {
                     if (IsCalendar)
                     {
                         Eventomatic_DB.SPs.UpdateTicketsPurchasedCalendar(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), 1, Convert.ToDecimal(de.Value.ToString().Replace("d", "")),Start_Date,End_Date).Execute();
                     }
                     else
                     {
                         Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), 1, Convert.ToDecimal(de.Value.ToString().Replace("d", "")), 0, "", "").Execute();
                     }                     
                 }
                 else
                 {
                     if (IsCalendar)
                     {
                         Eventomatic_DB.SPs.UpdateTicketsPurchasedCalendar(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), Convert.ToInt32(de.Value), 0,Start_Date,End_Date).Execute();
                     }
                     else
                     {
                         Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), Convert.ToInt32(de.Value), 0, 0, "", "").Execute();
                     }                     
                 }
             }
             if (IsFree)
             {
                 Eventomatic_DB.SPs.UpdateTicketNumFreeEvents(Convert.ToInt32(tempTx_Key)).Execute();
             }
             

             //update ticket seller
             if (specificuser != "0")
             {
                 Eventomatic_DB.SPs.UpdateTransactionFbids(Convert.ToInt64(specificuser), 0, Convert.ToInt32(tempTx_Key)).Execute();
             }

             return Convert.ToInt32(tempTx_Key);
         }

         public bool Isproduct(int Event_Key)
         {
             bool IsProducttemp = false;
             DataSet dsisproduct = Eventomatic_DB.SPs.ViewIsProductEvent(Event_Key).GetDataSet();
             foreach (DataRow r in dsisproduct.Tables[0].Rows)
             {
                 if (r["Type"] != DBNull.Value)
                 {
                     if (r["Type"].ToString() == "1")
                     {
                         IsProducttemp = true;
                     }
                 }
             }
             return IsProducttemp;
         }

         public string graph_getfbname(Int64 fbid)
         {
             WebClient wc = new WebClient();
             wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
             try
             {
                 string result = wc.DownloadString("https://graph.facebook.com/" + fbid.ToString() + "?fields=name");
                 if (!result.Contains("error"))
                 {
                     string[] strresults = result.Split('"');                     
                     return strresults[3];
                 }
                 else return "";
                 
             }
             catch
             {
                 return "";
             }
         }

         public string getgraphimg(string id)
         {
             string thereturn = "https://graph.facebook.com/" + id + "/picture";  //person pic             
             return thereturn;
         }

         public JArray GetEventAttending(string eid,int Type)
         {
             //type = 0 Invited / type = 1 attending / type = 2 maybe / type = 3 noreply
             WebClient wc = new WebClient();
             wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters             
             JArray jarray = new JArray();
             string graphcall = "https://graph.facebook.com/" + eid + "/invited?access_token=";
             if (Type == 1)
             {
                graphcall = graphcall.Replace("invited", "attending");
             }
             else if (Type == 2)
             {
                 graphcall = graphcall.Replace("invited", "maybe");
             }
             if (Type == 3)
             {
                 graphcall = graphcall.Replace("invited", "declined");
             }
             try
             {
                 DataSet dstemp = Eventomatic_DB.SPs.ViewFbUsers(121100861).GetDataSet();
                 string result = wc.DownloadString(graphcall + dstemp.Tables[0].Rows[0]["Access_Token"].ToString());
                 JObject data = JObject.Parse(result);                 
                 jarray = (JArray)data["data"];
             }
             catch
             {
                 string Toemail = System.Configuration.ConfigurationSettings.AppSettings.Get("ErrorToEmail").ToString();
                 Send_Email SE = new Send_Email();
                // SE.Send_Email_Function("Error@theGroupstore.com", Toemail, "An Error has occured", "Facebook Get Event Attending", 0);
             }
             return jarray;
         }

         public DataSet GetdsEventAttending(JArray jsonAttending,int Type)
         {
             DataSet dstemp = new DataSet();
             DataTable dtAttending = new DataTable();
             dtAttending.Columns.Add("Pic_Url");
             dtAttending.Columns.Add("Name");
             dtAttending.Columns.Add("fblink");
             DataTable dtMaybe = dtAttending.Copy();
             DataTable dtNot = dtAttending.Copy();
             string picurl = "https://graph.facebook.com/#/picture";             
             string fblink = "<a href='http://www.facebook.com/profile.php?id=#'>";
             if (Type == 1) //fbGuestlist page requesting
             {
                 fblink = "<a href='#' onclick='returnToParent(#); return false;'>";
             }

             foreach (JObject jo in jsonAttending)
             {
                 string rsvp = (string)jo["rsvp_status"];
                 switch (rsvp)
                 {
                     case "attending":
                         DataRow rtemp = dtAttending.NewRow();
                         rtemp["Pic_Url"] = picurl.Replace("#", (string)jo["id"]);
                         rtemp["Name"] = (string)jo["name"];
                         rtemp["fblink"] = fblink.Replace("#", (string)jo["id"]);
                         dtAttending.Rows.Add(rtemp);
                         break;
                     case "declined":
                         DataRow rtemp2 = dtNot.NewRow();
                         rtemp2["Pic_Url"] = picurl.Replace("#", (string)jo["id"]);
                         rtemp2["Name"] = (string)jo["name"];
                         rtemp2["fblink"] = fblink.Replace("#", (string)jo["id"]);
                         dtNot.Rows.Add(rtemp2);
                         break;
                     case "unsure":
                         DataRow rtemp3 = dtMaybe.NewRow();
                         rtemp3["Pic_Url"] = picurl.Replace("#", (string)jo["id"]);
                         rtemp3["Name"] = (string)jo["name"];
                         rtemp3["fblink"] = fblink.Replace("#", (string)jo["id"]);
                         dtMaybe.Rows.Add(rtemp3);
                         break;
                 }
             }
             dstemp.Tables.Add(dtAttending);
             dstemp.Tables.Add(dtMaybe);
             dstemp.Tables.Add(dtNot);
             return dstemp;
         }

         public string GetNavigateurl()
         {
             string strtemp = ConfigurationSettings.AppSettings.Get("Root_URL").ToString();
             if (!Applocation())
                {
                    strtemp = ConfigurationSettings.AppSettings.Get("Store_URL").ToString();
                }
             return strtemp;
         }

         public string GetNavigateurl(Int64 fbid)
         {
             string strtemp = ConfigurationSettings.AppSettings.Get("Root_URL").ToString();
             DataSet dstemp = Eventomatic_DB.SPs.ViewFbUsers(fbid).GetDataSet();
             if (dstemp.Tables[0].Rows.Count > 0)
             {
                 if (dstemp.Tables[0].Rows[0]["GetNavigate"] != DBNull.Value)
                 {
                     if (!Convert.ToBoolean(dstemp.Tables[0].Rows[0]["GetNavigate"]))
                     {
                         strtemp = ConfigurationSettings.AppSettings.Get("Store_URL").ToString();
                     }
                 }
             }             
             return strtemp;
         }

         public bool Applocation()
         {
             bool booltemp = true;                          
                if (HttpContext.Current.Request.UrlReferrer == null)
                {
                    booltemp = false;
                }
                else if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.ToString().Contains("apps.facebook.com")) && (!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.ToString().Contains("signed_request")))
                {
                    booltemp = false;
                }
                if (hdAppLocation != null)
                {
                    if (hdAppLocation.Value == "x")
                    {
                        hdAppLocation.Value = booltemp.ToString();
                    }
                    else if (hdAppLocation.Value == "True")
                    {
                        booltemp = true;
                    }
                }                
             return booltemp;
         }


         protected void ddlgroups_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
         {
             int inttemp = ddlgroups.SelectedIndex;
         }

         public string GetOpenWinApplocation()
         {
             string strrooturl = "";
             if (Applocation())
             {
                 strrooturl = "True";
             }
             return strrooturl;
         }

         protected void IsSpy(Int64 fbid)
         {
             DataSet dstemp = Eventomatic_DB.SPs.ViewFbUsers(fbid).GetDataSet();
             if (dstemp.Tables[0].Rows[0]["IsSpy"] != DBNull.Value)
             {
                 if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["IsSpy"]))
                 {
                     Response.Redirect("ErrorPaging.aspx");
                 }
             }

         }

         public string Hasdone_DemoPay(string txtPayPal)
         {             
             string strreturn = "";
             DataSet dstemp = Eventomatic_DB.SPs.ViewPayPalDemoPay(txtPayPal).GetDataSet();

             if (dstemp.Tables[0].Rows.Count > 0)
             {
                 //has done demopay before          
                 string strdatesent = dstemp.Tables[0].Rows[0]["Date_Sent"].ToString();
                 decimal amountsent = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Amount_Sent"]);
                 strreturn = "Please note: We have already sent this PayPal account $" + decimal.Round(amountsent, 2).ToString() + " on " + strdatesent + "EST.";
             }
             else
             {
                 //first time                 
             }

             return strreturn;
         }

         public string Getqrurl(int Event_Key)
         {
             Eventomatic.Addons.qrcodes qr = new Eventomatic.Addons.qrcodes();
             qr.GenerateEventqrimg(Convert.ToInt32(Event_Key));

             string strtemp = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "images/qr/" + Event_Key.ToString() + ".png";
             return strtemp;
         }

         public string Getqrurl(int Event_Key, Int64 fbid)
         {
             Eventomatic.Addons.qrcodes qr = new Eventomatic.Addons.qrcodes();
             qr.GenerateEventqrimg(Convert.ToInt32(Event_Key),fbid);

             string strtemp = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "images/qr/" + Event_Key.ToString() + "&fbid=" + fbid + ".png";
             return strtemp;
         }

         public string Getqrurltx(int tx_key)
         {
             Eventomatic.Addons.qrcodes qr = new Eventomatic.Addons.qrcodes();
             qr.Generatetxqrimg(tx_key);

             string strtemp = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "images/qr/tx/" + tx_key.ToString() + ".png";
             return strtemp;
         }

         public Boolean isMobile()
         {             

             HttpContext curcontext = HttpContext.Current;

             string user_agent = curcontext.Request.ServerVariables["HTTP_USER_AGENT"];
             user_agent = user_agent.ToLower();


             // Checks the user-agent  
             if (user_agent != null)
             {
                 // Checks if its a Windows browser but not a Windows Mobile browser  
                 if (user_agent.Contains("windows") && !user_agent.Contains("windows ce"))
                 {
                     return false;
                 }

                 // Checks if it is a mobile browser  
                 string pattern = "up.browser|up.link|windows ce|iphone|iemobile|mini|mmp|symbian|midp|wap|phone|pocket|mobile|pda|psp";
                 MatchCollection mc = Regex.Matches(user_agent, pattern, RegexOptions.IgnoreCase);
                 if (mc.Count > 0)
                     return true;

                 // Checks if the 4 first chars of the user-agent match any of the most popular user-agents  
                 string popUA = "|acs-|alav|alca|amoi|audi|aste|avan|benq|bird|blac|blaz|brew|cell|cldc|cmd-|dang|doco|eric|hipt|inno|ipaq|java|jigs|kddi|keji|leno|lg-c|lg-d|lg-g|lge-|maui|maxo|midp|mits|mmef|mobi|mot-|moto|mwbp|nec-|newt|noki|opwv|palm|pana|pant|pdxg|phil|play|pluc|port|prox|qtek|qwap|sage|sams|sany|sch-|sec-|send|seri|sgh-|shar|sie-|siem|smal|smar|sony|sph-|symb|t-mo|teli|tim-|tosh|tsm-|upg1|upsi|vk-v|voda|w3c |wap-|wapa|wapi|wapp|wapr|webc|winw|winw|xda|xda-|";
                 if (popUA.Contains("|" + user_agent.Substring(0, 4) + "|"))
                     return true;
             }

             // Checks the accept header for wap.wml or wap.xhtml support  
             string accept = curcontext.Request.ServerVariables["HTTP_ACCEPT"];
             if (accept != null)
             {
                 if (accept.Contains("text/vnd.wap.wml") || accept.Contains("application/vnd.wap.xhtml+xml"))
                 {
                     return true;
                 }
             }

             // Checks if it has any mobile HTTP headers  

             string x_wap_profile = curcontext.Request.ServerVariables["HTTP_X_WAP_PROFILE"];
             string profile = curcontext.Request.ServerVariables["HTTP_PROFILE"];
             string opera = curcontext.Request.Headers["HTTP_X_OPERAMINI_PHONE_UA"];

             if (x_wap_profile != null || profile != null || opera != null)
             {
                 return true;
             }

             return false;
         }

         public string getMobileOS()
         {
             string strOS = "";
             HttpContext curcontext = HttpContext.Current;

             string user_agent = curcontext.Request.ServerVariables["HTTP_USER_AGENT"];
             user_agent = user_agent.ToLower();

             if (user_agent.ToLower().Contains("blackberry"))
             {
                 strOS = "blackberry";
             }
             else if ((user_agent.ToLower().Contains("iphone") || (user_agent.ToLower().Contains("ipad")) || (user_agent.ToLower().Contains("ipod"))))
             {
                 strOS = "ios";
             }
             else if (user_agent.ToLower().Contains("android"))
             {
                 strOS = "android";
             }

             return strOS;
         }

         public Boolean iscompatiblePayPalMobile()
         {
             Boolean iscompatible = false;

             HttpContext curcontext = HttpContext.Current;

             string user_agent = curcontext.Request.ServerVariables["HTTP_USER_AGENT"];
             user_agent = user_agent.ToLower();
             //user_agent = "BlackBerry9700/5.0.0.680 Profile/MIDP-2.1 Cconfiguration/CLDC-1.1 vENDORid/370";
             //user_agent = "Mozilla/5.0 (BlackBerry; U; Blackberry 9700; en-US) AppleWebKit/534.8+ (KHTML,like Gecko) Version/6.0.0.448 Mobile Safari/534.8+";

             //Blackberry version 5.0.0.93
             //BlackBerry9000/5.0.0.93 Profile/MIDP-2.0 Configuration/CLDC-1.1 VendorID/179
             
             if (user_agent.ToLower().Contains("blackberry"))
             {
                 try
                 {
                 
                 //need to get version
                 string[] words = user_agent.Split(' ');
                 
                     string[] words2 = words[0].Split('/');
                     string version = words2[1].Substring(0, 1);
                     if (IsDecimal(version))
                     {
                         decimal dectemp = Convert.ToDecimal(version);
                         if (dectemp >= 6)
                         {
                             iscompatible = true;
                         }
                     }
                     foreach (string eachword in words)
                     {
                         if (eachword.ToLower().Contains("version"))
                         {
                             string[] strtemp = eachword.Split('/');
                             version = strtemp[1].Substring(0, 1);
                             if (IsDecimal(version))
                             {
                                 decimal dectemp = Convert.ToDecimal(version);
                                 if (dectemp >= 6)
                                 {
                                     iscompatible = true;
                                 }
                             }
                         }
                     }
                 }
                 catch
                 {
                 }
                 
             }
             
             //iphone
             //version 4.1
             //mozilla/5.0 (iphone; u; cpu iphone os 4_1 like mac os x; en-us) applewebkit/532.9 (khtml, like gecko) version/4.0.5 mobile/8b117 

             //version 4.0.2
             //mozilla/5.0 (iphone; u; cpu iphone os 4_0_2 like mac os x; en-us) applewebkit/532.9 (khtml, like gecko) version/4.0.5 mobile/8a400 
             if ((user_agent.ToLower().Contains("iphone")) || user_agent.ToLower().Contains("ipad"))
             {
                 iscompatible = true;
             }

             //android
             //Mozilla/5.0 (Linux; U; Android 2.1-update1; de-de; HTC Desire 1.19.161.5 Build/ERE27) AppleWebKit/530.17 (KHTML, like Gecko) Version/4.0 Mobile Safari/530.17
             //http://technobuz.com/2011/03/android-user-agent-string/
             else if (user_agent.ToLower().Contains("android"))
             {
                 iscompatible = true;
             }

             return iscompatible;
         }

         public string getfbid_Seller_Name(int tx_key)
         {
             string strname = "";

             DataSet dstemp = Eventomatic_DB.SPs.ViewTicketSellersTxkey(tx_key).GetDataSet();
             if (dstemp.Tables[0].Rows.Count > 0)
             {
                 if (dstemp.Tables[0].Rows[0]["Full_Name"] != DBNull.Value)
                 {
                     strname = dstemp.Tables[0].Rows[0]["Full_Name"].ToString();
                 }
             }
             
             return strname;
         }

         public string getfbid_Seller_Name(Int64 fbid)
         {
             string strname = "";

             DataSet dstemp = Eventomatic_DB.SPs.ViewFbUsers(fbid).GetDataSet();
             if (dstemp.Tables[0].Rows.Count > 0)
             {
                 if ((dstemp.Tables[0].Rows[0]["First_Name"] != DBNull.Value) && (dstemp.Tables[0].Rows[0]["Last_Name"] != DBNull.Value))
                 {
                     strname = dstemp.Tables[0].Rows[0]["First_Name"].ToString() + ' ' + dstemp.Tables[0].Rows[0]["Last_Name"].ToString();
                 }
             }

             return strname;
         }

         public bool ShowMobileSales(int Event_Key)
         {
             int Resource_Key = Convert.ToInt32(GetResourceKeyEventKey(Event_Key));
             bool Showmobile = false;
             DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
             if (dstemp.Tables[0].Rows[0]["Mobile_Sales"] != DBNull.Value)
             {
                 if (dstemp.Tables[0].Rows[0]["Mobile_Sales"].ToString().ToLower() == "true")
                 {
                     Showmobile = true;
                 }
             }

             /*dstemp = Eventomatic_DB.SPs.ViewQuestion(Event_Key).GetDataSet();
             if (dstemp.Tables[0].Rows.Count > 0)
             {
                 Showmobile = false;
             }*/

             return Showmobile;
             //return false;
         }

         public bool ShowMobileSales_Rkey(int Resource_Key)
         {             
             bool Showmobile = false;
             DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
             if (dstemp.Tables[0].Rows[0]["Mobile_Sales"] != DBNull.Value)
             {
                 if (dstemp.Tables[0].Rows[0]["Mobile_Sales"].ToString().ToLower() == "true")
                 {
                     Showmobile = true;
                 }
             }             
             return Showmobile;
         }

         public string HavePermission(int tx_key)
         {
             string HavePermission = "";

             int resourcetemp = Convert.ToInt32(GetResourceKeytxKey(tx_key));

             if (resourcetemp!=0)
             {
                 DataSet dstemp = Eventomatic_DB.SPs.ViewResource(resourcetemp).GetDataSet();
                 //if (dstemp.Tables[0].Rows[0]["Perm_Verification_Code"] != DBNull.Value)
                 //{
                     if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)
                     {
                         HavePermission = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
                     }
                 //}
             }             
             
             return HavePermission;
         }

         public bool HavePermission_Rkey(int Resource_Key)
         {
             bool HavePermission = false;
            
             if (Resource_Key != 0)
             {
                 DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
                 if (dstemp.Tables[0].Rows[0]["Perm_Verification_Code"] != DBNull.Value)
                 {
                     HavePermission = true;
                 }
             }
             return HavePermission;
         }

         public bool HavePermission_Eventkey(int Event_Key)
         {
             bool HavePermission = false;
             int Resource_Key = Convert.ToInt32(GetResourceKeyEventKey(Event_Key));

             if (Event_Key != 0)
             {
                 DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
                 if (dstemp.Tables[0].Rows[0]["Perm_Verification_Code"] != DBNull.Value)
                 {
                     HavePermission = true;
                 }
             }
             return HavePermission;
         }

         public void SetPermissionHeader(int Resource_Key)
         {
             string PermissionHeader = "";
             
             GetAccessTokenRequest getAccessTokenRequest = null;
            BaseAPIProfile profile2 = null;
             
             bool Live_Demo = false;
             Live_Demo = IsDemo_ResourceKey(Resource_Key);
             
            try
            {
                profile2 = GetPermissionsCallProfile(Live_Demo);
                getAccessTokenRequest = new GetAccessTokenRequest();
                RequestEnvelope en = new RequestEnvelope();
                en.errorLanguage = "en_US";
                getAccessTokenRequest.requestEnvelope = en;

                DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
                getAccessTokenRequest.token = dstemp.Tables[0].Rows[0]["Perm_Request_Token"].ToString();
                getAccessTokenRequest.verifier = dstemp.Tables[0].Rows[0]["Perm_Verification_Code"].ToString();

                PayPal.Platform.SDK.Permissions per = new PayPal.Platform.SDK.Permissions();
                per.APIProfile = profile2;                

                GetAccessTokenResponse getAccessTokenResponse = per.getAccessToken(getAccessTokenRequest);

                if (per.isSuccess.ToUpper() == "FAILURE")
                {
                    /*HttpContext.Current.Session[Constants.SessionConstants.FAULT] = per.LastError;
                    HttpContext.Current.Response.Redirect("APIError.aspx", false);*/
                }
                else
                {
                    Eventomatic_DB.SPs.UpdateResourcePermAccessToken(Resource_Key, getAccessTokenResponse.token, getAccessTokenResponse.tokenSecret).Execute();

                    /*Session[Constants.SessionConstants.GetAccessTokenResponse] = getAccessTokenResponse;
                    this.Response.Redirect("GetAccessTokenResponse.aspx", false);
                    Hashtable map = new Hashtable();
                    
                    if (Live_Demo) //live
                    {
                    }
                    else //demo
                    {
                        map = OauthSignature.getAuthHeader(ConfigurationSettings.AppSettings.Get("APIUsername").ToString(), ConfigurationSettings.AppSettings.Get("APIPassword").ToString(), getAccessTokenResponse.token, getAccessTokenResponse.tokenSecret, OauthSignature.HTTPMethod.POST, "https://api.sandbox.paypal.com/nvp", null);
                    }

                    if ((map.ContainsKey("TimeStamp")) && (map.ContainsKey("Signature")))
                    {
                        PermissionHeader = "timestamp=" + map["TimeStamp"].ToString() + ",token=" + getAccessTokenResponse.token + ",signature=" + map["Signature"].ToString();
                    }                    */
                }
            }
            catch
            {
            }            
         }


         public string GetPermissionHeader(int Resource_Key, bool Live_Demo, string callurl)
         {
             string strreturn = "";
             Hashtable map = new Hashtable();
             
             
             string APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
             string APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();

             if (!Live_Demo) //demo
             {                 
                 APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                 APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
             }

             

             string straccesstoken = "";
             string straccesstokensecret = "";
             DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
             if (dstemp.Tables[0].Rows[0]["Perm_Access_Token"] != DBNull.Value)
             {
                 straccesstoken = dstemp.Tables[0].Rows[0]["Perm_Access_Token"].ToString();
             }
             if (dstemp.Tables[0].Rows[0]["Perm_Access_Token_Secret"] != DBNull.Value)
             {
                 straccesstokensecret = dstemp.Tables[0].Rows[0]["Perm_Access_Token_Secret"].ToString();
             }

             
             map = OauthSignature.getAuthHeader(APIusername, APIPassword, straccesstoken, straccesstokensecret, OauthSignature.HTTPMethod.POST, callurl, null);

             if ((map.ContainsKey("TimeStamp")) && (map.ContainsKey("Signature")))
             {
                 strreturn = "timestamp=" + map["TimeStamp"].ToString() + ",signature=" + map["Signature"].ToString() + ",token=" + straccesstoken;
             }

             return strreturn;
         }

        
         public PayPal.Platform.SDK.BaseAPIProfile GetPermissionsCallProfile(bool Live_Demo)
         {
             ////Three token 
             BaseAPIProfile profile2 = null;
             profile2 = new PayPal.Platform.SDK.BaseAPIProfile();
             profile2.APIProfileType = ProfileType.ThreeToken;
             profile2.RequestDataformat = "SOAP11";
             profile2.ResponseDataformat = "SOAP11";
             profile2.IsTrustAllCertificates = true;
             profile2.EndPointAppend = "Permissions/GetAccessToken";


             if (Live_Demo)//true = Live , false = trial
             {
                 profile2.Environment = "https://svcs.paypal.com/";
                 profile2.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString();
                 profile2.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                 profile2.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                 profile2.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
             }
             else
             {
                 profile2.Environment = "https://svcs.sandbox.paypal.com/";
                 profile2.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                 profile2.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                 profile2.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                 profile2.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
             }
             return profile2;
         }

        

         public void Facebook_PostLink_OnWall(string fbid, string linkurl, string message, string picurl, string name,string Access_Token, string caption, string description)
         {                          

             try
             {                 
                 //doGraphcall(thecall);
                 StringBuilder requestString = new StringBuilder();
                 requestString.Append("access_token=" + Access_Token + "&message=" + message + "&link=" + linkurl + "&picture=" + picurl + "&name=" + name + "&description="+description);                 
                 HttpWebResponse webResponse;
                 HttpWebRequest webRequest = WebRequest.Create("https://graph.facebook.com/"+ fbid +"/feed") as HttpWebRequest;
                 webRequest.Method = "POST";
                 webRequest.ContentType = "application/x-www-form-urlencoded";
                 string request = requestString.ToString();
                 webRequest.ContentLength = request.Length;

                 StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                 writer.Write(request);
                 writer.Close();

                 webResponse = webRequest.GetResponse() as HttpWebResponse;
             }
             catch
             {
             }            

         }

            //link, message, picture, name, caption, description

         public bool isPayForward(int txkey)
         {
             DataSet dsispayforward = Eventomatic_DB.SPs.ViewIsPayForward(txkey).GetDataSet();
             bool ispayforward = false;
             if (dsispayforward.Tables[0].Rows[0]["txtype"] != DBNull.Value)
             {
                 if (dsispayforward.Tables[0].Rows[0]["txtype"].ToString() == "1")
                 {
                     ispayforward = true;
                 }
             }
             return ispayforward;
         }

         public bool isDoDirectPayment(int txkey)
         {
             bool isdodirect = false;
             DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromTxKey(txkey).GetDataSet();
             if (dstemp.Tables[0].Rows[0]["dodirectpayment"] != DBNull.Value)
             {
                 if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["dodirectpayment"].ToString()))
                 {
                     isdodirect = true;
                 }
             }
             return isdodirect;
         }

         public bool isDoDirectPaymentresourcekey(int resourcekey)
         {
             bool isdodirect = false;
             DataSet dstemp = Eventomatic_DB.SPs.ViewResource(resourcekey).GetDataSet();
             if (dstemp.Tables[0].Rows[0]["dodirectpayment"] != DBNull.Value)
             {
                 if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["dodirectpayment"].ToString()))
                 {
                     isdodirect = true;
                 }
             }
             return isdodirect;
         }

         public bool isWPPEnabled(int resourcekey, bool Live_Trial)
         {
             bool boolreturn = false;
             string ppemail = GetResourceEmail(resourcekey);
             Addons.PaypalMethods ppmeth = new Addons.PaypalMethods();
             if (ppmeth.CheckAccount_DirectPayment(Live_Trial,ppemail.ToLower()) == "true"){
                 boolreturn = true;
             }
             return boolreturn;
         }

         public bool isVerified(int resourcekey)
         {
             bool boolreturn = true;
             DataSet dstemp2 = Eventomatic_DB.SPs.ViewPayPalInfo(resourcekey).GetDataSet();
             if (dstemp2.Tables[0].Rows.Count > 0)
             {
                 if (Convert.ToBoolean(dstemp2.Tables[0].Rows[0]["Verified"].ToString()) == false)
                 {
                     //show warning
                     boolreturn = false;
                 }
             }
             return boolreturn;
         }

         public bool CheckiftxComplete(int txkey)
         {
             bool txComplete = false;

             DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(txkey).GetDataSet();
             if (dstemp.Tables[0].Rows[0]["Tx_Status"] != DBNull.Value)
             {
                 if (dstemp.Tables[0].Rows[0]["Tx_Status"].ToString() == "2")
                 {
                     txComplete = true;
                 }
             }

             return txComplete;
         }

         public Hashtable pf_getfbinfo(string oauth,string thereturnpage)
         {
             Hashtable hstemp = new Hashtable();
            //oauth = oauth.Substring(0, oauth.IndexOf("|"));

            //oauth = oauth.Substring(0, oauth.IndexOf("|"));                

            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            //string result = wc.DownloadString("https://graph.facebook.com/oauth/access_token?response_type=token&client_secret=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Secret").ToString() + "&client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&code=" + oauth);
            string strsend = "https://graph.facebook.com/oauth/access_token?client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + thereturnpage + "&client_secret=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Secret").ToString() + "&code=" + oauth;
            string result = wc.DownloadString(strsend);
            string accesstoken = result.Replace("access_token=", "");

            hstemp.Add("accesstoken", accesstoken);

            //Get user id
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string result2 = wc.DownloadString("https://graph.facebook.com/me?access_token=" + accesstoken);

            try
            {
                JObject o = JObject.Parse(result2);
                string fbid = (string)o["id"];
                hstemp.Add("fbid", fbid);
                //lblfbstatus.Text = fbid +  " will get credit for this ticket sale";                                
                if (o["email"] != null)
                {
                    hstemp.Add("email", (string)o["email"]);
                }
                if (o["first_name"] != null)
                {
                    hstemp.Add("firstname", (string)o["first_name"]);
                }
                if (o["last_name"] != null)
                {
                    hstemp.Add("lastname", (string)o["last_name"]);
                }
            }
            catch
            {
            }
            return hstemp;
         }

         public string pf_PPAuth(string callbackurl, string resource_key)
         {
            string strreturn = "";
              
            RequestPermissionsRequest permissionsRequest = null;
            Site sitetemp = new Site();


            try
            {
                Boolean isdemo = sitetemp.IsDemo_ResourceKey(Convert.ToInt32(resource_key));

                BaseAPIProfile profile2;
                if (isdemo)
                {
                    profile2 = sitetemp.GetPermissionsCallProfile(true);
                }
                else
                {
                    profile2 = sitetemp.GetPermissionsCallProfile(false);
                }


                permissionsRequest = new RequestPermissionsRequest();

                RequestEnvelope en = new RequestEnvelope();
                en.errorLanguage = "en_US";
                permissionsRequest.requestEnvelope = en;
                permissionsRequest.callback = callbackurl;

                permissionsRequest.scope = new string[17];
                permissionsRequest.scope[0] = "MOBILE_CHECKOUT";
                permissionsRequest.scope[1] = "DIRECT_PAYMENT";               
                permissionsRequest.scope[2] = "ACCESS_BASIC_PERSONAL_DATA";
                permissionsRequest.scope[3] = "ACCESS_ADVANCED_PERSONAL_DATA";
                /*string[] strScope =  {"MOBILE_CHECKOUT","DIRECT_PAYMENT"};
                permissionsRequest.scope = strScope;*/

                PayPal.Platform.SDK.Permissions per = new PayPal.Platform.SDK.Permissions();
                per.APIProfile = profile2;
                RequestPermissionsResponse PResponse = per.requestPermissions(permissionsRequest);

                
                if (per.isSuccess.ToUpper() == "FAILURE")
                {
                    /*HttpContext.Current.Session[Constants.SessionConstants.FAULT] = per.LastError;
                    HttpContext.Current.Response.Redirect("APIError.aspx", false);*/
                }
                else
                {
                    //HttpContext.Current.Session[Constants.SessionConstants.TOKENKEY] = PResponse.token;
                    //HttpContext.Current.Session[Constants.SessionConstants.REQUESTPERMISSIONSRESPONSE] = PResponse;
                    Eventomatic_DB.SPs.UpdateResourcePermToken(Convert.ToInt32(resource_key), PResponse.token).Execute();
                    if (isdemo)
                    {
                        strreturn = "https://www.paypal.com/webscr&cmd=_grant-permission&request_token=" + PResponse.token;
                    }
                    else
                    {
                        strreturn = "https://www.sandbox.paypal.com/webscr&cmd=_grant-permission&request_token=" + PResponse.token;
                    }
                }
            }
            catch (FATALException FATALEx)
            {
                /*Session[Constants.SessionConstants.FATALEXCEPTION] = FATALEx;
                this.Response.Redirect(Constants.ASPXPages.APIERROR + "?" + Constants.QueryStringConstants.TYPE + "=FATAL", false);*/
            }
            catch (Exception ex)
            {
                /*FATALException FATALEx = new FATALException("Error occurred in PayCreate Page.", ex);
                Session[Constants.SessionConstants.FATALEXCEPTION] = FATALEx;
                this.Response.Redirect("APIError.aspx?type=FATAL", false);*/
            }

            return strreturn;
         }

        
         public decimal getServiceFee(int txkey)
         {
             decimal dcreturn = 0;             
             decimal dcamount = 0;
             DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromTxKey(txkey).GetDataSet();

             decimal SFP = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Service_Fee_Percentage"].ToString());
             decimal SFC = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Service_Fee_Cents"].ToString());
             decimal SFM = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Service_Fee_Max"].ToString());

             dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(txkey).GetDataSet();
             if (dstemp.Tables[0].Rows[0]["Amount"] != DBNull.Value)
             {
                 dcamount = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Amount"].ToString());
             }

             //get service fee amount
             decimal dctemp = (dcamount * (SFP / 100));

             decimal dcppfee = (dctemp * Convert.ToDecimal(0.02));
             if (dcppfee > 1){
                 dcppfee = 1;
             }
             //adjust for paypal fee of 2% max $1
             dcreturn = dctemp - dcppfee;


             return dcreturn;
         }

         public string getgooglemapsaddress(string Latitude, string Longitude)
         {
             string strreturn = "";

             WebClient wc = new WebClient();
             wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
             string result = wc.DownloadString("http://maps.googleapis.com/maps/geo?q=" + Latitude + "," + Longitude);
             JObject o = JObject.Parse(result);
             JArray level1 = (JArray)o["Placemark"];
             
             JObject level2 = (JObject)level1[0];

            
             

             //o = JObject.Parse(level1);
             strreturn = (string)level2["address"];

             return strreturn;
         }
        
    }
}
