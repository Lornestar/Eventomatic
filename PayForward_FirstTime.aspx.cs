using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using System.Collections;
using System.Configuration;
using System.Data;
using Eventomatic.Addons;

namespace Eventomatic
{
    public partial class PayForward_FirstTime : System.Web.UI.Page
    {        
        string thereturnpage = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "PayForward_FirstTime.aspx";
        fbuser fbuser = new fbuser();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Site sitetemp = new Site();

                if (((Request.Form["signupemail"] != null) && (Request.Form["signupemail"] != "")) || (Request.QueryString["signupemail"] !=null))
                {
                    //New user signing up
                    string stremail = "";
                    string strpwd = "";
                    if (Request.Form["signupemail"] != null)
                    {
                        stremail = Request.Form["signupemail"].ToString();
                    }
                    if (Request.QueryString["signupemail"] != null)
                    {
                        stremail = Request.QueryString["signupemail"].ToString();
                    }
                    if (Request.Form["signuppwd"] != null)
                    {
                        strpwd = Request.Form["signuppwd"].ToString();
                    }
                    if (Request.QueryString["signuppwd"] != null)
                    {
                        strpwd = Request.QueryString["signuppwd"].ToString();
                    }

                    Eventomatic_DB.SPs.UpdateResourcePfFirsttimeEmailpwd(stremail, HttpContext.Current.Request.UserHostAddress, strpwd).Execute();

                    DataSet dstemp = Eventomatic_DB.SPs.ViewFBUserEmail(stremail).GetDataSet();
                    
                    fbuser.Email = stremail;
                    fbuser.UID = Convert.ToInt64(dstemp.Tables[0].Rows[0]["FBid"].ToString());
                    hdfbid.Value = fbuser.UID.ToString();

                    Session["fbuser"] = fbuser;
                }
                else
                {                    

                    if (Session["fbuser"] == null)
                    {
                        //session timed out  **Should give warning**
                        Response.Redirect("http://www.snap-pay.com");
                    }
                    else
                    {
                        fbuser = (fbuser)Session["fbuser"];
                        hdfbid.Value = fbuser.UID.ToString();
                    }
                }

                if ((Request.QueryString["resource_key"] != null) && (Request.QueryString["resource_key"] != ""))
                {
                    hdresource_key.Value = Request.QueryString["resource_key"].ToString();
                    thereturnpage += "?resource_key=" + hdresource_key.Value;
                }

                if ((Request.QueryString["verification_code"] != "") && (Request.QueryString["verification_code"] != null))
                {
                    //Step 2 - Choose plan
                    
                    Eventomatic_DB.SPs.UpdateResourcePermVCode(Convert.ToInt32(hdresource_key.Value), Request.QueryString["verification_code"].ToString()).Execute();
                    
                    sitetemp.SetPermissionHeader(Convert.ToInt32(hdresource_key.Value));

                    //hdresource_key.Value += "beginselling";

                    //Response.Redirect(thereturnpage);

                    //update db with paypal info
                }


                if ((Request.QueryString["resource_key"] != null) && (Request.QueryString["resource_key"] != ""))
                {
                    //Update db with their PayPal info
                    Addons.PaypalMethods getbasicdata = new Addons.PaypalMethods();
                    Hashtable hstemp = new Hashtable();

                    if (islive())
                    {
                        hstemp = getbasicdata.GetPersonalData_Parse(Convert.ToInt32(hdresource_key.Value), true);
                    }
                    else
                    {
                        hstemp.Add("email", "perm@lornestar.com");
                        hstemp.Add("first", "Mike");
                        hstemp.Add("last", "Johnson");
                        hstemp.Add("payerid", "S7RZL78L3DZ4S");
                        hstemp.Add("country", "US");
                        hstemp.Add("currency", "USD");
                    }


                    fbuser.Firstname = hstemp["first"].ToString();
                    fbuser.Lastname = hstemp["last"].ToString();
                    Eventomatic_DB.SPs.UpdateFbUsersNames(fbuser.UID, fbuser.Firstname, fbuser.Lastname).Execute();
                    Session["fbuser"] = fbuser;

                    Eventomatic_DB.SPs.UpdateResourceProfile(Convert.ToInt32(hdresource_key.Value), hstemp["currency"].ToString(), hstemp["email"].ToString(), islive(), 0).Execute();

                    DataSet dstemp = Eventomatic_DB.SPs.ViewPayPalInfo(Convert.ToInt32(hdresource_key.Value)).GetDataSet();
                    if (dstemp.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Verified"].ToString()) == false)
                        {
                            //show warning
                            lblnotverified.Visible = true;
                        }
                    }

                    if (getbasicdata.CheckAccount_DirectPayment(islive(), hstemp["email"].ToString().ToLower()) != "true")
                    {
                        lbldodirectpayment.Visible = true;
                    }


                    //Step 4 & 5 - Choose Settings & Begin Selling
                    if ((Request.QueryString["token"] != null) && (Request.QueryString["token"] != ""))
                    {
                        Addons.PaypalMethods pp = new Addons.PaypalMethods();
                        pp.Billing_Agreement2(Request.QueryString["token"].ToString(), islive());
                    }

                    hdresource_key.Value = hdresource_key.Value.Replace("beginselling", "");

                    RadTabStrip1.Tabs[0].Enabled = false;
                    RadTabStrip1.Tabs[1].Enabled = false;
                    RadTabStrip1.Tabs[2].Enabled = false;
                    RadTabStrip1.Tabs[3].Enabled = true;
                    RadTabStrip1.SelectedIndex = 3;
                    RadMultiPage1.SelectedIndex = 3;

                    //if chose high volume charge the monthly fee
                    if (sitetemp.isDoDirectPaymentresourcekey(Convert.ToInt32(hdresource_key.Value)))
                    {
                        //Charge monthly
                        Addons.PaypalMethods pp = new Addons.PaypalMethods();
                        pp.DoReferenceHighVolume(Convert.ToInt32(hdresource_key.Value));
                    }

                    /*
                    //at least at Step 2
                    RadTabStrip1.Tabs[0].Enabled = false;
                    RadTabStrip1.Tabs[1].Enabled = true;
                    RadTabStrip1.SelectedIndex = 1;
                    RadMultiPage1.SelectedIndex = 1;

                    if (hdresource_key.Value.Contains("beginselling"))
                    {
                       
                    }
                    else if (hdresource_key.Value.Contains("billingagreement"))
                    {
                        //Step 3 - Billing Agreement                        

                        hdresource_key.Value = hdresource_key.Value.Replace("billingagreement", "");

                        RadTabStrip1.Tabs[2].Enabled = true;
                        RadTabStrip1.SelectedIndex = 2;
                        RadMultiPage1.SelectedIndex = 2;
                    }
                    else //Step 2 - Choose Plan
                    {

                        
                    }
                     */
                }

                
               // if ((Request.QueryString["code"] != null) && (Request.QueryString["code"] != ""))
               // {                    
              //      hsfbinfo = sitetemp.pf_getfbinfo(HttpContext.Current.Request.QueryString["code"].ToString(), thereturnpage);
              //      hdfbid.Value = hsfbinfo["fbid"].ToString();

                    //Eventomatic_DB.SPs.UpdateResource(Convert.ToInt64(hsfbinfo["fbid"].ToString()), hsfbinfo["firstname"].ToString(), hsfbinfo["lastname"].ToString(), hsfbinfo["email"].ToString(), HttpContext.Current.Request.UserHostAddress, "PayForward_FirstTime.aspx", 0, 0, "", hsfbinfo["accesstoken"].ToString(), 0).Execute();

                    if (hdresource_key.Value == "aa")
                    {                        
                        Setupppauth();
                    }
                    
              /*  }
                else
                {                    
                    Response.Redirect("http://www.facebook.com/dialog/oauth?client_id=" + ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + thereturnpage + "&scope=email");
                }
                */
                
                //setup mixpanel analytics
                string strmixpanelname = "";
                if (fbuser.Firstname != null)
                {
                    strmixpanelname = "mpq.name_tag('" + fbuser.Firstname + " " + fbuser.Lastname + " " + fbuser.UID + "');";
                }                
                switch (RadTabStrip1.SelectedIndex)
                {
                    case 0://Step 1
                            Page.RegisterStartupScript("Mixpanel", "<script language=javascript>mpq.track('Signup Step1 PayPal');"+ strmixpanelname + "</script>");
                            break;
                    case 1://Step 2
                            Page.RegisterStartupScript("Mixpanel", "<script language=javascript>mpq.track('Signup Step2 Plan');" + strmixpanelname + "</script>");
                            break;
                    case 2://Step 3
                            Page.RegisterStartupScript("Mixpanel", "<script language=javascript>mpq.track('Signup Step3 Billing');" + strmixpanelname + "</script>");
                            break;
                    case 3://Step 4
                            Page.RegisterStartupScript("Mixpanel", "<script language=javascript>mpq.track('Signup Step4 Settings');" + strmixpanelname + "</script>");
                            break;
                    case 4://Step 5
                            Page.RegisterStartupScript("Mixpanel", "<script language=javascript>mpq.track('Signup Step5 Begin');" + strmixpanelname + "</script>");
                            break;
                }
            }                       
        }

        protected void btnNext1_Click(object sender, EventArgs e)
        {
            //step 2 button Go to Billing Agreement
            if (ddlPlan.SelectedValue == "1")
            {
                Eventomatic_DB.SPs.UpdateResourceDoDirectPayment(Convert.ToInt32(hdresource_key.Value), true).Execute();
            }
            else
            {
                Eventomatic_DB.SPs.UpdateResourceDoDirectPayment(Convert.ToInt32(hdresource_key.Value), false).Execute();
            }

            RadTabStrip1.Tabs[1].Enabled = false;
            RadTabStrip1.Tabs[2].Enabled = true;
            RadTabStrip1.SelectedIndex = 2;
            RadMultiPage1.SelectedIndex = 2;

            chkTOS.Checked = false;

            Page.RegisterStartupScript("Mixpanel", "<script language=javascript>mpq.track('Signup Step3 Billing');</script>");
        }

        protected void btnNext2_Click(object sender, EventArgs e)
        {
            Setupppauth();
        }

        protected void btnNext3_Click(object sender, EventArgs e)
        {
            //update settings info
            if (txtStoreName2.Text == "")
            {
                lblstorenameReq.ForeColor = System.Drawing.Color.Red;
                lblerror.Visible = true;
            }
            else
            {
                lblerror.Visible = false;
                int resourcekey = Convert.ToInt32(hdresource_key.Value);
                //update db & begin selling
                Eventomatic_DB.SPs.UpdateResourcePfFirstTimeSettings(resourcekey, ddlCurrency.SelectedValue, txtStoreName2.Text, txtReceipt.Text).Execute();
                RadTabStrip1.Tabs[3].Enabled = false;
                RadTabStrip1.Tabs[4].Enabled = true;
                RadTabStrip1.SelectedIndex = 4;
                RadMultiPage1.SelectedIndex = 4;

                Site sitetemp = new Site();
                
                //check if need WPP
                if ((!sitetemp.isWPPEnabled(resourcekey,islive())) && (sitetemp.isDoDirectPaymentresourcekey(resourcekey))){
                    lblWPPLink.Visible = true;
                    if (ddlCurrency.SelectedValue == "USD")
                    {
                       lblWPPLink.Text =  lblWPPLink.Text.Replace("##", "https://registration.paypal.com/welcomePage.do?partner=PayPalCA&bundleCode=C3&country=US");
                    }
                    else
                    {
                        lblWPPLink.Text = lblWPPLink.Text.Replace("##", "https://registration.paypal.com/welcomePage.do?partner=PayPalCA&bundleCode=C3&country=CA");
                    }
                }

                if (!sitetemp.isVerified(resourcekey))
                {
                    lblVerifyLink.Visible = true;
                }

                Page.RegisterStartupScript("Mixpanel", "<script language=javascript>mpq.track('Signup Step5 Begin');</script>");
            }            
        }

        protected void btnNext4_Click(object sender, EventArgs e)
        {
            if (txtsmartphonenum.Text == "")
            {
                lblsmartphonereq.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Addons.Twilio sms = new Addons.Twilio();
                string thebody = "Welcome to Snappay.  You can begin accepting payment by downloading the Snappay app at http://www.getsnappay.com";
                sms.SendSMS(txtsmartphonenum.Text, thebody);
            }            
        }

        protected void Setupppauth()
        {
            Int64 fbid = Convert.ToInt64(hdfbid.Value);

            //create store
            StoredProcedure sp_AddGroup = Eventomatic_DB.SPs.UpdateGroups(fbid, 0, "",
                Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFP").ToString()),
                Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFC").ToString()),
                Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFM").ToString()),
                0);
            sp_AddGroup.Execute();
            hdresource_key.Value = sp_AddGroup.Command.Parameters[6].ParameterValue.ToString();

            int Resource_Key = Convert.ToInt32(hdresource_key.Value);


            //save paypal info            
            if (!islive())
            {
                Eventomatic_DB.SPs.UpdateResourceProfile(Resource_Key, ddlCurrency.SelectedValue, "", false, 0).Execute();
            }
            else
            {
                Eventomatic_DB.SPs.UpdateResourceProfile(Resource_Key, ddlCurrency.SelectedValue, "", true, 0).Execute();
            }

            /*
            if (ddlPPAccount.SelectedValue == "False") //premiere
            {
                Eventomatic_DB.SPs.PfUpdateResourceProfile(Resource_Key, false).Execute();
            }
            else
            {
                Eventomatic_DB.SPs.PfUpdateResourceProfile(Resource_Key, true).Execute();
            }
            */

            string callbackurl = ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/PayForward_FirstTime.aspx?resource_key=" + hdresource_key.Value;
            Site sitetemp = new Site();
            string sendtourl = sitetemp.pf_PPAuth(callbackurl, hdresource_key.Value);
            RadAjaxManager1.Redirect(sendtourl);
        }

        protected void btnCreatePP_Click(object sender, EventArgs e)
        {
            pnlcreatePayPal.Visible = true;

            //initialize ddl in create paypal form
            UpdateProvince();
            UpdateProvinceBiz();
            UpdateddlCategory();

            RequiredFieldValidator1.Enabled = true;
            RequiredFieldValidator2.Enabled = true;
            RequiredFieldValidator3.Enabled = true;
            RequiredFieldValidator4.Enabled = true;
            RequiredFieldValidator5.Enabled = true;
            RequiredFieldValidator6.Enabled = true;
            RequiredFieldValidator7.Enabled = true;
            RequiredFieldValidator8.Enabled = true;
            RequiredFieldValidator9.Enabled = true;
            RequiredFieldValidator10.Enabled = true;
            RequiredFieldValidator11.Enabled = true;
            RequiredFieldValidator12.Enabled = true;
            rqPayPal.Enabled = true;

            DOB.MinDate = DateTime.Now.AddYears(-100);
            DOB.SelectedDate = DateTime.Now.AddYears(-40);
            txtBizDate.MinDate = DateTime.Now.AddYears(-50);
            txtBizDate.SelectedDate = DateTime.Now.AddYears(-3);
        }

        protected void btnCreatePayPal_Click(object sender, EventArgs e)
        {
            Addons.PaypalMethods createppbiz = new Addons.PaypalMethods();
            bool Live_Demo = true; //default is Live
            if (ConfigurationSettings.AppSettings.Get("Store_URL").ToString().Contains("localhost"))
            {
                Live_Demo = false;
            }

            string returnURL = thereturnpage + "?resource_key=aa";

            string strresult = createppbiz.CreateBizAccount(Live_Demo, "Business", ddlSalutation.Text, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text,
                DOB.SelectedDate.Value, txtAddress1.Text, txtAddress2.Text, txtCity.Text, ddlProvince.SelectedValue, txtAreaZipCode.Text, ddlCountry.SelectedValue,
                ddlCountryCitizenship.SelectedValue, txtPhone1.Text, txtEmail1.Text, ddlCurrencyCode.SelectedValue, returnURL, txtBizName.Text,
                txtBizAddress1.Text, txtBizAddress2.Text, txtCity.Text, ddlBizProvince.SelectedValue, txtBizAreaZipCode.Text, ddlBizCountry.SelectedValue,
                txtBizPhone.Text, ddlBizCategory.SelectedValue, ddlBizSubCategory.SelectedValue, txtBizCustomerEmail.Text, txtBizCustomerPhone.Text, txtBizWebsite.Text,
                txtBizDate.SelectedDate.Value, ddlBusinessType.SelectedValue, Convert.ToDecimal(txtBizAvgTx.Text), Convert.ToDecimal(txtBizAvgMonthly.Text),ddlsalesvenue.SelectedValue,
                txtpercentonline.Text,txtsalesvenuedesc.Text);
        }

        protected void btnBillingBack_Click(object sender, EventArgs e)
        {
            RadTabStrip1.Tabs[1].Enabled = true;
            RadTabStrip1.Tabs[2].Enabled = false;
            RadTabStrip1.SelectedIndex = 1;
            RadMultiPage1.SelectedIndex = 1;
        }

        protected void btnBillingNext_Click(object sender, EventArgs e)
        {
            if (chkTOS.Checked)
            {
                lbltoserror.Visible = false;
                Addons.PaypalMethods pp = new Addons.PaypalMethods();
                pp.Billing_Agreement(Convert.ToInt32(hdresource_key.Value), islive());                
            }
            else
            {
                lbltoserror.Visible = true;
            }
        }        

        protected void UpdateProvince()
        {
            DataSet dstemp;
            if (ddlCountry.SelectedValue == "US")
            {                
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(2).GetDataSet();
                lblAreaZipCode.Text = "Zip Code";
                lblStateProvince.Text = "State";
                txtAreaZipCode.EmptyMessage = "90210";
            }
            else
            {
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(1).GetDataSet();
                lblAreaZipCode.Text = "Postal Code";
                lblStateProvince.Text = "Province";
                txtAreaZipCode.EmptyMessage = "M5R 2X1";
            }
            ddlProvince.DataSource = dstemp.Tables[0];
            ddlProvince.DataTextField = "Region_Text";
            ddlProvince.DataValueField = "Region_Value";
            ddlProvince.DataBind();            
        }

        protected void UpdateProvinceBiz()
        {
            DataSet dstemp;

            if (ddlBizCountry.SelectedValue == "US")
            {             
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(2).GetDataSet();
                lblBizAreaZipCode.Text = "Zip Code";
                lblStateProvince.Text = "State";
                txtBizAreaZipCode.EmptyMessage = "90210";            
            }
            else
            {
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(1).GetDataSet();
                lblBizAreaZipCode.Text = "Postal Code";
                lblStateProvince.Text = "Province";
                txtBizAreaZipCode.EmptyMessage = "M5R 2X1";
            }
            ddlBizProvince.DataSource = dstemp.Tables[0];
            ddlBizProvince.DataTextField = "Region_Text";
            ddlBizProvince.DataValueField = "Region_Value";
            ddlBizProvince.DataBind();

            
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            UpdateProvince();
        }

        protected void ddlBizCountry_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            UpdateProvinceBiz();
        }

        protected void ddlsalesvenue_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ddlsalesvenue.SelectedValue == "OTHER")
            {
                RequiredFieldValidator12.Enabled = true;
            }
            else
            {
                RequiredFieldValidator12.Enabled = false;
            }
        }

        protected void ddlBizCategory_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            UpdateddlCategory();
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (pnldodirectpayment.Visible)
            {
                pnldodirectpayment.Visible = false;

                lblScheduleHighVolume.Visible = false;
                lblSchedulePayasyougo.Visible = true;
            }
            else
            {
                pnldodirectpayment.Visible = true;
                lblScheduleHighVolume.Visible = true;
                lblSchedulePayasyougo.Visible = false;

                lblScheduleHighVolume.Text = lblScheduleHighVolume.Text.Replace("##", "$ " + ConfigurationSettings.AppSettings.Get("Monthly_Service_Fee").ToString());
            }
        }

        public void UpdateddlCategory()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewInfoMerchants(Convert.ToInt32(ddlBizCategory.SelectedValue)).GetDataSet();


            ddlBizSubCategory.DataSource = dstemp.Tables[0];
            ddlBizSubCategory.DataTextField = "Description";
            ddlBizSubCategory.DataValueField = "SubCategory";
            ddlBizSubCategory.DataBind();            
        }

        private bool islive(){
            bool islive = true;
            if (ConfigurationSettings.AppSettings.Get("Store_URL").ToString().Contains("localhost"))
            {
                islive = false;
            }
            return islive;
            //return true;
        }
        
    }
}