using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Services.Private.Permissions;
using PayPal.Platform.SDK;
using System.Configuration;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Data;
using SubSonic;

namespace Eventomatic
{
    public partial class PayForward2 : System.Web.UI.Page
    {
        string thereturnpage = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Payforward2.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["verification_code"] != "") && (Request.QueryString["verification_code"] != null))
            {
                if ((Request.QueryString["resource_key"] != null) && (Request.QueryString["resource_key"] != ""))
                {
                    hdResource_Key.Value = Request.QueryString["resource_key"].ToString();
                }
                Eventomatic_DB.SPs.UpdateResourcePermVCode(Convert.ToInt32(hdResource_Key.Value), Request.QueryString["verification_code"].ToString()).Execute();
            }
            if ((Request.QueryString["invitekey"] != null) && (Request.QueryString["invitekey"] != ""))
            {
                thereturnpage += "?invitekey=" + Request.QueryString["invitekey"];
            }
            if ((Request.QueryString["resource_key"] != null) && (Request.QueryString["resource_key"] != ""))
            {
                thereturnpage += "?resource_key=" + Request.QueryString["resource_key"];
                if ((Request.QueryString["isnative"] != null) && (Request.QueryString["isnative"] != ""))
                {
                    thereturnpage += "isnative";
                }
            }
            
            if ((Request.QueryString["code"] != null) && (Request.QueryString["code"] != ""))
            {
                string newurl = HttpContext.Current.Request.Url.AbsoluteUri;
                if ((!thereturnpage.ToLower().Contains("localhost")) && (!newurl.ToLower().Contains("https")))
                {
                    newurl = newurl.Replace("http", "https");
                    Response.Redirect(newurl);
                }
                Setfbid();
            }
            else
            {                
                Response.Redirect("http://www.facebook.com/dialog/oauth?client_id=" + ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + thereturnpage + "&display=touch&scope=email");
            }

            if (hddoppauth.Value == "1")
            {
                AuthPP();
            }            
            hdfbAppid.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString();
            hdipaddress.Value = HttpContext.Current.Request.UserHostAddress;
            if (!thereturnpage.ToLower().Contains("localhost"))
            {
                hdStore_URL.Value = ConfigurationSettings.AppSettings.Get("Store_URL").ToString().Replace("http","https");            
            }            
        }

        protected void AuthPP()
        {
            RequestPermissionsRequest permissionsRequest = null;
            Site sitetemp = new Site();


            try
            {
                Boolean isdemo = sitetemp.IsDemo_ResourceKey(Convert.ToInt32(hdResource_Key.Value));

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
                permissionsRequest.callback = ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/PayForward2.aspx?resource_key="+hdResource_Key.Value;

                permissionsRequest.scope = new string[17];
                permissionsRequest.scope[0] = "MOBILE_CHECKOUT";
                permissionsRequest.scope[1] = "DIRECT_PAYMENT";
                permissionsRequest.scope[2] = "MASS_PAY";
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
                    Eventomatic_DB.SPs.UpdateResourcePermToken(Convert.ToInt32(hdResource_Key.Value), PResponse.token).Execute();
                    if (isdemo)
                    {
                        Response.Redirect("https://www.paypal.com/webscr&cmd=_grant-permission&request_token=" + PResponse.token, false);
                    }
                    else
                    {
                        Response.Redirect("https://www.sandbox.paypal.com/webscr&cmd=_grant-permission&request_token=" + PResponse.token, false);
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
        }

        protected void Setfbid()
        {
            string oauth = "";
            oauth = HttpContext.Current.Request.QueryString["code"].ToString();
            //oauth = oauth.Substring(0, oauth.IndexOf("|"));

            //oauth = oauth.Substring(0, oauth.IndexOf("|"));                

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
                hdnfbfirstname.Value = firstname;
                hdnfblastname.Value = lastname;
                bool isnewuser = false;

                Eventomatic_DB.SPs.UpdateResource(Convert.ToInt64(fbid), firstname, lastname, email, HttpContext.Current.Request.UserHostAddress, "PayForward2.aspx", 0, 0, "", accesstoken, 0).Execute();

                Site sitetemp = new Site();

                if (sitetemp.CountPageVisits(Convert.ToInt64(fbid), "PayForward2.aspx") == 0)
                {
                    //Send welcome Email
                    Send_Email se = new Send_Email();
                    string thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/Welcome_Email.txt"));
                    se.Send_Email_Simple("info@theGroupstore.com", email, "Welcome to Groupstore", thebody);
                }
                

                //Eventomatic_DB.SPs.UpdateTicketSellers(Convert.ToInt64(fbid), firstname + " " + lastname, accesstoken, email).Execute();                                
                if ((Request.QueryString["invitekey"] != null) && (Request.QueryString["invitekey"] != ""))
                {
                    DataSet dsinvite = Eventomatic_DB.SPs.PfViewReferral(Convert.ToInt32(Request.QueryString["invitekey"].ToString())).GetDataSet();
                    int rs = 0;
                    bool addadmin = false;
                    string PhoneNumber = "";
                    if (dsinvite.Tables[0].Rows.Count > 0)
                    {
                        if (dsinvite.Tables[0].Rows[0]["smsnumber"] != null)
                        {
                            PhoneNumber = dsinvite.Tables[0].Rows[0]["smsnumber"].ToString();
                        }
                        if (dsinvite.Tables[0].Rows[0]["resource_key"] != null)
                        {
                            rs = Convert.ToInt32(dsinvite.Tables[0].Rows[0]["resource_key"].ToString());
                        }
                        if (dsinvite.Tables[0].Rows[0]["addadmin"] != null)
                        {
                            addadmin = Convert.ToBoolean(dsinvite.Tables[0].Rows[0]["addadmin"].ToString());
                        }
                    }                    

                    if (rs != 0) //was an invite to become seller or Admin
                    {
                        Eventomatic_DB.SPs.PfUpdateFBUsersSellers(Convert.ToInt64(fbid), rs, PhoneNumber, addadmin).Execute();

                        if (addadmin) //welcome popup for admin
                        {
                            hdshowwelcome.Value = "1";
                        }
                        else //welcome popup for seller
                        {
                            hdshowwelcome.Value = "2";
                        }
                    }                    
                }

                //check isnative
                if ((Request.QueryString["isnative"] != null) && (Request.QueryString["isnative"] != ""))
                {
                    hdisnative.Value = "True";
                }
                string mobileos = sitetemp.getMobileOS();
                if (mobileos != "")
                {
                    hdos.Value = mobileos;
                }

                string strresourcekey = "0";
                //check what they are admin or seller of
                DataSet dstemp = Eventomatic_DB.SPs.PfViewListFBUserAdminSellers(Convert.ToInt64(fbid)).GetDataSet();
                hdnumberofstores.Value = dstemp.Tables[0].Rows.Count.ToString();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    //check if requested specific resource_key
                    if ((Request.QueryString["resource_key"] != null) && (Request.QueryString["resource_key"] != ""))
                    {
                        //need to check if seller/admin of that store
                        string rskey = Request.QueryString["resource_key"];

                        if (rskey.Contains("isnative"))
                        {
                            rskey = rskey.Replace("isnative", "");
                            hdisnative.Value = "True";
                        }
                        
                        DataRow[] drtemp;
                        drtemp = dstemp.Tables[0].Select("resource_key = "+ rskey);                        
                        if (drtemp.GetUpperBound(0) > -1) //at least one exists
                        {
                            hdStoreName.Value = drtemp[0]["Resource_Name"].ToString();
                            string strisadmin = drtemp[0]["Isadmin"].ToString();
                            if (strisadmin == "1")
                            {
                                hdisadmin.Value = "True";
                            }
                            else
                            {
                                hdisadmin.Value = "False";
                            }                            
                            hdResource_Key.Value = drtemp[0]["Resource_Key"].ToString();
                            hdppemail.Value = drtemp[0]["Resource_Email"].ToString();

                            strresourcekey = rskey;

                            if (drtemp[0]["DoDirect"] != DBNull.Value)
                            {
                                if (Convert.ToBoolean(drtemp[0]["DoDirect"].ToString()))
                                {
                                    hddodirectpayment.Value = "True";
                                }
                            }
                        }
                        else{
                            Assigndemovalues();
                            strresourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();
                        }
                    }
                    else //didn't request specific resource_key
                    {
                        //they are admin/seller of at least 1 store
                        hdStoreName.Value = dstemp.Tables[0].Rows[0]["Resource_Name"].ToString();
                        hdisadmin.Value = dstemp.Tables[0].Rows[0]["Isadmin"].ToString();
                        hdResource_Key.Value = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
                        hdppemail.Value = dstemp.Tables[0].Rows[0]["Resource_Email"].ToString();
                        strresourcekey = hdResource_Key.Value;
                        if (dstemp.Tables[0].Rows[0]["DoDirect"] != DBNull.Value)
                        {
                            if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["DoDirect"].ToString()))
                            {
                                hddodirectpayment.Value = "True";
                            }
                        }
                    }                                    
                }
                else //not admin/seller of store send them to demo page
                {
                    Assigndemovalues();
                    strresourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();
                }

                Eventomatic_DB.SPs.UpdateResourcePostResourcekey(Convert.ToInt64(fbid), Convert.ToInt32(strresourcekey)).Execute();
                Page.Title = "Snappay POS - " + hdStoreName.Value + " - " + hdnfbfirstname.Value;
                
                if (sitetemp.HavePermission_Rkey(Convert.ToInt32(hdResource_Key.Value)))
                {
                    hdhaveppauth.Value = "True";
                }                

                hddemoresourcekey.Value = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();

                if (hdResource_Key.Value == hddemoresourcekey.Value)
                {
                    Assigndemovalues();
                }

                bool isdemo = sitetemp.IsDemo_ResourceKey(Convert.ToInt32(hdResource_Key.Value));
                if (!isdemo)
                {
                    hdisDemo.Value = "True";
                }

                hdResourceCurrency.Value = sitetemp.GetCurrency(Convert.ToInt32(hdResource_Key.Value));
            }
            catch
            {
            }

        }

        protected void Assigndemovalues()
        {
            hdStoreName.Value = "Demo Store";
            hdisadmin.Value = "True";
            hdResource_Key.Value = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();
            hdppemail.Value = "kingbbj3@hotmail.com";
            hdisDemo.Value = "True";
            hddodirectpayment.Value = "True";
            hdshowwelcome.Value = "3";

            Send_Email se = new Send_Email();
            se.demoemail( Convert.ToInt64(hdnfbid.Value));
        }
    }
}