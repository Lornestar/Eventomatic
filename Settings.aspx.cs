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
//using facebook.Schema;
using Eventomatic.Addons;
using Telerik.Web.UI;
using PayPal.Services.Private.Permissions;
using PayPal.Platform.SDK;

namespace Eventomatic
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int Resource_Key = Convert.ToInt32(Master.GetResourceKey());                
                //btn_Add.Attributes.Add("onclick", "javascript:AddListbox();");
                Fill_Form();
                if (Master.IsAdmin())
                {
                    pnlModifyTrial.Visible = true;
                }
                Site sitetemp = new Site();
                if ((Request.QueryString["verification_code"] != "") && (Request.QueryString["verification_code"] != null))
                {
                    Eventomatic_DB.SPs.UpdateResourcePermVCode(Resource_Key, Request.QueryString["verification_code"].ToString()).Execute();
                }

                if (sitetemp.ShowMobileSales_Rkey(Resource_Key))
                {                    
                    pnlRequestPermission.Visible = true;
                    if (sitetemp.HavePermission_Rkey(Resource_Key))
                    {
                        pnlRequestPermission_AlreadyHave.Visible = true;
                        pnlRequestPermission_Thepitch.Visible = false;
                    }
                }
            }
        }

        protected void Fill_Form()
        {
            //populate combobox
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceNetwork(0).GetDataSet();
            RadComboBox1.DataSource = dstemp.Tables[0];
            RadComboBox1.DataTextField = "Network_Name";
            RadComboBox1.DataValueField = "Network_Key";
            RadComboBox1.DataBind();
            
            int Resource_Key = Convert.ToInt32(Master.GetResourceKey());
            dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();

            if (dstemp.Tables[0].Rows[0]["Network_Key"] != DBNull.Value)
            {
                string strtemp = dstemp.Tables[0].Rows[0]["Network_Key"].ToString();
                RadComboBox1.SelectedValue = strtemp;
            }

            

            //False = Trial & True = Live
            chkTrial.Checked = true;
            Assign_New_RadioButton();
            if (dstemp.Tables[0].Rows[0]["Demo"] != DBNull.Value)
            {
                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Demo"].ToString()) == true)
                {
                    chkTrial.Checked = false;
                    chkLive.Checked = true;
                    Assign_New_RadioButton();
                }
            }

            txtPaypal.Text = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();


            switch (dstemp.Tables[0].Rows[0]["Desired_Currency"].ToString())
            {
                case "CAD":
                    ddlCurrency.SelectedValue = "CAD";
                    break;
                case "USD":
                    ddlCurrency.SelectedValue = "USD";
                    break;
                case "EUR":
                    ddlCurrency.SelectedValue = "EUR";
                    break;
                case "GBP":
                    ddlCurrency.SelectedValue = "GBP";
                    break;
                case "ILS":
                    ddlCurrency.SelectedValue = "ILS";
                    break;
            }
            //Paypal confirmation
            HiddenField hdnemail = new HiddenField();
            hdnemail = (HiddenField)Paypal_Confirmation1.FindControl("hdemail");
            hdnemail.Value = txtPaypal.Text;            

            //Load Administrators
            RadComboBox ddlgroups = new RadComboBox();
            ddlgroups = (RadComboBox)Master.LoadGroupsList();
            DataSet dsCurrentEvents = Eventomatic_DB.SPs.ViewListGroupMembers(Convert.ToInt32(ddlgroups.SelectedValue.ToString())).GetDataSet();            
            lbAdmins.DataSource = dsCurrentEvents.Tables[0];
            lbAdmins.DataTextField = "Full_Name";
            lbAdmins.DataValueField = "FBid";
            lbAdmins.DataBind();

            try
            {
                DataTable dtFriendsList = Master.getFriendslist(Master.getfbuser());
                ListItem litemp = new ListItem();
                for (int i = 0; i < dtFriendsList.Rows.Count; i++)
                {
                    litemp.Value = dtFriendsList.Rows[i]["fbid"].ToString();
                    litemp.Text = dtFriendsList.Rows[i]["Name"].ToString();
                    if (lbAdmins.Items.Contains(litemp))
                    {
                        dtFriendsList.Rows.Remove(dtFriendsList.Rows[i]);
                        i += 1;
                    }
                }
                lbFriendsList.DataSource = dtFriendsList;
                lbFriendsList.DataTextField = "Name";
                lbFriendsList.DataValueField = "fbid";
                lbFriendsList.DataBind();

                RadComboBox ddlGroupList = Master.LoadGroupsList();
                lblGroupName.Text = ddlGroupList.SelectedItem.Text + " Administrators";
            }
            catch
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool tempPageValid = true;
            string temperrorstr = "";
            Site sitetemp = new Site();
            if (chkLive.Checked)
            {                
                if (!sitetemp.isEmail(txtPaypal.Text) || (!sitetemp.isEmail(txtPaypalConfirm.Text)))
                {
                    tempPageValid = false;
                    temperrorstr = "Please enter a valid Email Address.";
                }
                if (txtPaypal.Text != txtPaypalConfirm.Text)
                {
                    tempPageValid = false;
                    temperrorstr = "Paypal Emails do not match.";
                }
            }
            if (tempPageValid)
            {
                lblError.Visible = false;
                Boolean blnLiveorTrial = false;
                if (chkLive.Checked)
                {
                    blnLiveorTrial = true;
                }
                

                Eventomatic_DB.SPs.UpdateResourceProfile(Convert.ToInt32(Master.GetResourceKey()), ddlCurrency.SelectedValue, txtPaypal.Text, blnLiveorTrial, 0).Execute();
                Paypal_Confirmation1.enteredemail(txtPaypal.Text, ddlCurrency.SelectedValue, Convert.ToInt32(Master.GetResourceKey()));
                //Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Default.aspx");
                string strurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString();
                bool booltemp = sitetemp.Applocation();
                if (!booltemp)
                {
                    strurl = ConfigurationSettings.AppSettings.Get("Root_URL").ToString();
                }
                strurl += "Default.aspx";
                Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('Your Changes have been Saved');location.href = '" + strurl + "';</script>");
            }
            else
            {
                lblError.Text = temperrorstr;
                lblError.Visible = true;
            }

        }

        protected void Assign_New_RadioButton()
        {
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
        }

        protected void chkTrial_CheckedChanged(object sender, EventArgs e)
        {
            Assign_New_RadioButton();
        }

        protected void chkLive_CheckedChanged(object sender, EventArgs e)
        {
            Assign_New_RadioButton();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Move data from dropdown to listbox
            ListItem litemp = new ListItem();
            litemp.Value = lbFriendsList.SelectedItem.Value;
            litemp.Text = lbFriendsList.SelectedItem.Text;
            lbAdmins.Items.Add(litemp);
            fbuser friends = Master.getfbuserinfo(Convert.ToInt64(lbFriendsList.SelectedValue));
            RadComboBox ddlGroupList = Master.LoadGroupsList();

            //Eventomatic_DB.SPs.UpdateResource(Convert.ToInt32(friends[0].uid), friends[0].first_name, friends[0].last_name, "").Execute();
            Eventomatic_DB.SPs.UpdateFbUsersResource(friends.UID, Convert.ToInt32(ddlGroupList.SelectedValue), friends.Firstname, friends.Lastname).Execute();

            //Record Activity
            Eventomatic.Addons.Activities activity = new Activities();
            activity.NewActivity(1, Convert.ToInt32(ddlGroupList.SelectedValue), Convert.ToInt64(Master.getfbid()), friends.UID, 0, 0);

            lbFriendsList.Items.RemoveAt(lbFriendsList.SelectedIndex);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            //Move data from listbox to dropdown
            fbuser friends = Master.getfbuserinfo(Convert.ToInt64(lbAdmins.SelectedValue));
            RadComboBox ddlGroupList = Master.LoadGroupsList();
            if (friends.UID.ToString() != Master.getfbid())
            {
                ListItem litemp = new ListItem();
                litemp.Value = lbAdmins.SelectedItem.Value;
                litemp.Text = lbAdmins.SelectedItem.Text;
                lbFriendsList.Items.Add(litemp);

                Eventomatic_DB.SPs.DeleteFbUsersResource(friends.UID, Convert.ToInt32(ddlGroupList.SelectedValue)).Execute();
                lbAdmins.Items.RemoveAt(lbAdmins.SelectedIndex);

                //Record Activity
                Eventomatic.Addons.Activities activity = new Activities();
                activity.NewActivity(6, Convert.ToInt32(ddlGroupList.SelectedValue), Convert.ToInt64(Master.getfbid()), friends.UID, 0, 0);

            }
            else
            {
                lblError.Visible = true;
            }

        }

        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadComboBox1.SelectedValue != null)
            {
                RadComboBox ddlGroupList = Master.LoadGroupsList();
                Eventomatic_DB.SPs.UpdateResourceNetwork(Convert.ToInt32(ddlGroupList.SelectedValue), Convert.ToInt32(RadComboBox1.SelectedValue)).Execute();
            }            
        }

        protected void btnRequestPermission_Click(object sender, EventArgs e)
        {

            RequestPermissionsRequest permissionsRequest = null;
            Site sitetemp = new Site();


            try
            {

                BaseAPIProfile profile2 = sitetemp.GetPermissionsCallProfile(chkLive.Checked);


                permissionsRequest = new RequestPermissionsRequest();

                RequestEnvelope en = new RequestEnvelope();
                en.errorLanguage = "en_US";
                permissionsRequest.requestEnvelope = en;
                permissionsRequest.callback = ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/Settings.aspx";

                permissionsRequest.scope = new string[17];
                permissionsRequest.scope[0] = "MOBILE_CHECKOUT";
                //permissionsRequest.scope[1] = "DIRECT_PAYMENT";
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
                    Eventomatic_DB.SPs.UpdateResourcePermToken(Convert.ToInt32(Master.GetResourceKey()), PResponse.token).Execute();
                    if (chkLive.Checked)//true = Live , false = trial
                    {
                        Server.Transfer("https://www.paypal.com/webscr&cmd=_grant-permission&request_token=" + PResponse.token);
                    }
                    else
                    {
                        Server.Transfer("https://www.sandbox.paypal.com/webscr&cmd=_grant-permission&request_token=" + PResponse.token, false);                        
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



        

        protected void btnCancelPerm_Click(object sender, EventArgs e)
        {
            GetAccessTokenRequest getAccessTokenRequest = null;
            BaseAPIProfile profile2 = null;
            Site sitetemp = new Site();
            
            try
            {
                profile2 = sitetemp.GetPermissionsCallProfile(chkLive.Checked);
                getAccessTokenRequest = new GetAccessTokenRequest();
                RequestEnvelope en = new RequestEnvelope();
                en.errorLanguage = "en_US";
                getAccessTokenRequest.requestEnvelope = en;

                DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Convert.ToInt32(Master.GetResourceKey())).GetDataSet();
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
                    /*Session[Constants.SessionConstants.GetAccessTokenResponse] = getAccessTokenResponse;
                    this.Response.Redirect("GetAccessTokenResponse.aspx", false);*/
                    CancelPerm(getAccessTokenResponse);

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

        protected void CancelPerm(GetAccessTokenResponse getAccessTokenResponse)
        {
            CancelPermissionsRequest cancelPermissionsRequest = null;
            BaseAPIProfile profile2 = null;
            Site sitetemp = new Site();

            try
            {
                profile2 = sitetemp.GetPermissionsCallProfile(chkLive.Checked);
                cancelPermissionsRequest = new CancelPermissionsRequest();
                RequestEnvelope en = new RequestEnvelope();
                en.errorLanguage = "en_US";
                cancelPermissionsRequest.requestEnvelope = en;


                cancelPermissionsRequest.token = getAccessTokenResponse.token;


                PayPal.Platform.SDK.Permissions per = new PayPal.Platform.SDK.Permissions();
                per.APIProfile = profile2;
                CancelPermissionsResponse getpermissionResponse = per.cancelPermissions(cancelPermissionsRequest);

                if (per.isSuccess.ToUpper() == "FAILURE")
                {
                    /*HttpContext.Current.Session[Constants.SessionConstants.FAULT] = per.LastError;
                    HttpContext.Current.Response.Redirect("APIError.aspx", false);*/
                }
                else
                {

                    /*Session[Constants.SessionConstants.cancelPermissionsResponse] = getpermissionResponse;
                    this.Response.Redirect("CancelPermissionsResponse.aspx", false);*/
                    int Resource_Key = Convert.ToInt32(Master.GetResourceKey());                
                    Eventomatic_DB.SPs.UpdateResourcePermVCode(Resource_Key, null).Execute();
                    Eventomatic_DB.SPs.UpdateResourcePermToken(Resource_Key, null).Execute();
                    pnlRequestPermission_AlreadyHave.Visible = false;
                    pnlRequestPermission_Thepitch.Visible = true;
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

    }
}
