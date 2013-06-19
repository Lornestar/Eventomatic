    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using Eventomatic.Addons;

namespace Eventomatic.Login
{
    public partial class Settings : System.Web.UI.Page
    {
        Send_Email se = new Send_Email();
        protected void Page_Load(object sender, EventArgs e)
        {
            Site sitetemp = new Site();
            lblSaved.Visible = false;
            if (!IsPostBack)
            {
                int resourcekey = 0;
                if ((Request.QueryString["resource_key"] != null) && (Request.QueryString["resource_key"] != ""))
                {
                    resourcekey = Convert.ToInt32(Request.QueryString["resource_key"].ToString());                    
                }
                if ((Request.QueryString["verification_code"] != "") && (Request.QueryString["verification_code"] != null))
                {
                    Eventomatic_DB.SPs.UpdateResourcePermVCode(resourcekey, Request.QueryString["verification_code"].ToString()).Execute();

                    sitetemp.SetPermissionHeader(resourcekey);

                    Addons.PaypalMethods getbasicdata = new Addons.PaypalMethods();
                    Hashtable hstemp = getbasicdata.GetPersonalData_Parse(resourcekey, true);

                    Eventomatic_DB.SPs.UpdateResourceProfile(resourcekey, hstemp["currency"].ToString(), hstemp["email"].ToString(), true, 0).Execute();

                }

                resourcekey = Convert.ToInt32(Master.getResourceKey());
                Int64 fbid = Master.getfbuser().UID;
                DataSet dstemp = Eventomatic_DB.SPs.ViewResource(resourcekey).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    txtStoreName2.Text = dstemp.Tables[0].Rows[0]["Group_Name"].ToString();
                    txtReceipt.Text = dstemp.Tables[0].Rows[0]["pfConfirmation"].ToString();
                    ddlCurrency.SelectedValue = dstemp.Tables[0].Rows[0]["Desired_Currency"].ToString();
                    lblcurrentaccount.Text = dstemp.Tables[0].Rows[0]["Email_PayPal"].ToString();
                }

                if (!sitetemp.isVerified(resourcekey))
                {
                    lbnotverified.Visible = true;
                }

                ShowWPPWarning();

                chkReferrals(fbid);
            }
        }

        protected void chkReferrals(Int64 fbid)
        {
            //Check referall status
                DataSet dsuserinfo = Eventomatic_DB.SPs.ViewFbUsers(fbid).GetDataSet();
                DateTime dtsignedup = Convert.ToDateTime(dsuserinfo.Tables[0].Rows[0]["Signed_Up"]);
                DataSet dstemp = Eventomatic_DB.SPs.PfCheckReferrals(fbid).GetDataSet();
                DateTime dtcutoff = Convert.ToDateTime("05/01/2012");
                if ((dstemp.Tables[0].Rows.Count > 2) || (dtsignedup < dtcutoff))
                {
                    //Free for life
                    //lblReferStatus.Text = "Snappay is available to you free for life, thank you for the referrals";                    
                }
                else
                {
                    
                    lblReferStatus.Text = "You signed up on " + dtsignedup.Date + ". Your 1st month free expires " + dtsignedup.AddMonths(1).Date + ".  Please refer friends to get Snappay free for life." ;
                }
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    txtrefer1.Text = dstemp.Tables[0].Rows[0]["email"].ToString();
                    txtrefer1.Enabled = false;
                    btnrefer1.Visible = false;
                    if (dstemp.Tables[0].Rows.Count > 1)
                    {
                        txtrefer2.Text = dstemp.Tables[0].Rows[1]["email"].ToString();
                        txtrefer2.Enabled = false;
                        btnrefer2.Visible = false;
                        if (dstemp.Tables[0].Rows.Count > 2)
                        {
                            txtrefer3.Text = dstemp.Tables[0].Rows[2]["email"].ToString();
                            txtrefer3.Enabled = false;
                            btnrefer3.Visible = false;
                        }
                    }

                }                                                      
        }

        protected void chkTrial_CheckedChanged(object sender, EventArgs e)
        {
            Assign_New_RadioButton();
        }

        protected void chkLive_CheckedChanged(object sender, EventArgs e)
        {
            Assign_New_RadioButton();
        }

        protected void Assign_New_RadioButton()
        {
            /*
            
            if (chkLive.Checked)
            {
                //RequiredFieldValidator1.Enabled = true;
                //RequiredFieldValidator2.Enabled = true;
                //Compare1.Enabled = true;
                pnlLive.Visible = true;
                lblTrialNote.Visible = false;
            }
            else
            {
                //RequiredFieldValidator1.Enabled = false;
                //RequiredFieldValidator2.Enabled = false;
                //Compare1.Enabled = false;
                pnlLive.Visible = false;
                lblTrialNote.Visible = true;
            }
             */
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            int resourcekey = Master.getResourceKey();
            Eventomatic_DB.SPs.UpdateResourcePfFirstTimeSettings(resourcekey, ddlCurrency.SelectedValue, txtStoreName2.Text, txtReceipt.Text).Execute();            
            lblSaved.Visible = true;
        }

        protected void btnChangePayPal_Click(object sender, EventArgs e)
        {
            int resourcekey = Master.getResourceKey();
            string callbackurl = ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/Login/Settings.aspx?resource_key=" + resourcekey.ToString();
            Site sitetemp = new Site();            
            string sendtourl = sitetemp.pf_PPAuth(callbackurl, resourcekey.ToString());
            RadAjaxManager1.Redirect(sendtourl);
        }

        protected void ShowWPPWarning()
        {
            lblWPPLink.Visible = false;
            if (ddlPlan.SelectedValue.ToString() == "1")
            {
                Site sitetemp = new Site();
                int resourcekey = Master.getResourceKey();
                bool live_trial = sitetemp.IsDemo_ResourceKey(resourcekey);
                if (!sitetemp.isWPPEnabled(resourcekey, !live_trial))
                {
                    lblWPPLink.Visible = true;
                    if (ddlCurrency.SelectedValue == "USD")
                    {
                        lblWPPLink.Text = lblWPPLink.Text.Replace("##", "https://registration.paypal.com/welcomePage.do?partner=PayPalCA&bundleCode=C3&country=US");
                    }
                    else
                    {
                        lblWPPLink.Text = lblWPPLink.Text.Replace("##", "https://registration.paypal.com/welcomePage.do?partner=PayPalCA&bundleCode=C3&country=CA");
                    }
                }
            }
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ShowWPPWarning();
        }

        protected void btnpwdchange_Click(object sender, EventArgs e)
        {
            fbuser fbuser = new fbuser();
            fbuser = Master.getfbuser();
            if ((fbuser.UID != 0) && (fbuser.UID != null)){
                Eventomatic_DB.SPs.UpdateFbUsersPassword(fbuser.UID, txtpwdchange.Text).Execute();
                lblpwdchangefinish.Visible = true;


            }
            
        }

        protected void btnrefer1_Click(object sender, EventArgs e)
        {
            doreferral(txtrefer1);
            Int64 fbid = Master.getfbuser().UID;
            chkReferrals(fbid);
        }

        protected void btnrefer2_Click(object sender, EventArgs e)
        {
            doreferral(txtrefer2);
            Int64 fbid = Master.getfbuser().UID;
            chkReferrals(fbid);
        }

        protected void btnrefer3_Click(object sender, EventArgs e)
        {
            doreferral(txtrefer3);
            Int64 fbid = Master.getfbuser().UID;
            chkReferrals(fbid);
        }

        protected void doreferral(Telerik.Web.UI.RadTextBox txttemp){
            int resourcekey = Convert.ToInt32(Master.getResourceKey());
            Int64 fbid = Master.getfbuser().UID;
            Site sitetemp = new Site();
            if (sitetemp.isEmail(txttemp.Text))
            {
                Eventomatic_DB.SPs.PfUpdateReferral2(fbid, resourcekey, txttemp.Text).Execute();

                se.Refer_Friend_Email(txttemp.Text, fbid);
            }
            else
            {
                lblerror.Visible = false;
            }
        }
        
    }
}