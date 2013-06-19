using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Xml.Linq;
using System.Net;
using System.IO;
using SubSonic;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using Eventomatic.Addons;

namespace Eventomatic
{
    public partial class Order_Form2 : System.Web.UI.Page
    {

        int Event_Key = 0;
        Int64 fbid = 0;
        bool importfb = false;
        bool displaydemoticket = false;

        protected void Page_Load(object sender, EventArgs e)
        {            

            if ((Request.QueryString["Event_Key"] != null) && (Request.QueryString["Event_Key"] != ""))
            {
                Event_Key = Convert.ToInt32(Request.QueryString["Event_Key"].ToString());
            }
            else if (hdeventkey.Value != "0")
            {
                Event_Key = Convert.ToInt32(hdeventkey.Value);
            }

            if ((Event_Key == 0) && (Request.QueryString["edit"] == "true") && (Request.QueryString["storeid"] != "") && (Request.QueryString["storeid"] != null))
            {
                int Resource_Key = Convert.ToInt32(Request.QueryString["storeid"].ToString());
                Event_Key = UpdatedbNewEvent(Resource_Key);
                if ((Request.QueryString["eid"] != null) && (Request.QueryString["eid"] != ""))
                {
                    importfb = true;                    
                }
                else
                {                    
                    Response.Redirect("Order_Form.aspx?Event_Key=" + Event_Key + "&edit=true");
                }
            }

            if (Event_Key != 0)
            {                
                if (Request.Form["__EVENTTARGET"] == "ReloadTickets")
                {
                    ReloadTickets();
                }
                else if (Request.Form["__EVENTTARGET"] == "doRemoveTicket")
                {
                    RemoveItem(0, Convert.ToInt32(Request["__EVENTARGUMENT"].ToString()));
                    ReloadTickets();
                }
                else if (Request.Form["__EVENTTARGET"] == "ReloadQuestions")
                {
                    ReloadQuestions();
                }
                else if (Request.Form["__EVENTTARGET"] == "doRemoveQuestion")
                {
                    RemoveItem(1, Convert.ToInt32(Request["__EVENTARGUMENT"].ToString()));
                    ReloadQuestions();
                }
                else if (Request.Form["__EVENTTARGET"] == "ReloadBackground")
                {
                    Changebackground();
                }
                hdCurrentDate.Value = DateTime.Now.ToString("MMM dd, yyyy HH:mm:ss");
                if (!IsPostBack)
                {
                    hypmobileleader.NavigateUrl = hypmobileleader.NavigateUrl.Replace("0", Event_Key.ToString());
                    hypmobilenews.NavigateUrl = hypmobilenews.NavigateUrl.Replace("0", Event_Key.ToString());

                    if (Request.QueryString["edit"] == null)
                    {
                        //Update page views
                        Eventomatic_DB.SPs.UpdateEventViews(Event_Key).Execute();                    
                    }
                    if (Request.QueryString["gsdemo"] == "true")
                    {
                        hdisdemogeneric.Value = "1";
                    }      
                    DataSet dstemp = Eventomatic_DB.SPs.ViewServiceFee(Event_Key).GetDataSet();
                    hdSFP.Value = dstemp.Tables[0].Rows[0]["Service_Fee_Percentage"].ToString();
                    Boolean booltemp = IsDecimal("0");

                    if (hdSFP.Value == "")
                    { hdSFP.Value = "0"; }
                    else if (booltemp)//hdSFP.Value))
                    { hdSFP.Value = Convert.ToString(Convert.ToDecimal(hdSFP.Value) / 100); }
                    if (hdSFC.Value == "")
                    { hdSFC.Value = "0"; }
                    hdSFC.Value = dstemp.Tables[0].Rows[0]["Service_Fee_Cents"].ToString();
                    if (hdSFM.Value == "")
                    { hdSFM.Value = "0"; }
                    hdSFM.Value = dstemp.Tables[0].Rows[0]["Service_Fee_Max"].ToString();
                    //Check if have paypal account                    
                    Site sitetemp = new Site();
                    if (!sitetemp.IsDemo(Event_Key))
                    {
                        lblDemo.Visible = false;
                    }
                    else
                    {
                        hdTrial.Value = "True";
                        lblDemo.Text = "Trial Version";
                        lblDemo.BackColor = System.Drawing.Color.LightBlue;
                    }

                    //Check if in edit mode
                    if (Request.QueryString["edit"] == "true")
                    {
                        fbloggedin(true);

                        rdpStartDate.SelectedDate = DateTime.Now.AddDays(14).Date;
                        rdpStartTime.SelectedDate = DateTime.Today.AddHours(20);
                        rdpEndDate.SelectedDate = DateTime.Now.AddDays(15).Date;
                        rdpEndTime.SelectedDate = DateTime.Today.AddHours(25);

                        lblAddNew.Attributes.Add("OnClick", "javascript:openWinTickets(0," + Event_Key.ToString() + ");return false;");
                        lblAddnewQuestion.Attributes.Add("OnClick", "javascript:openWinQuestions(0," + Event_Key.ToString() + ");return false;");
                        lblBackground.Attributes.Add("OnClick", "javascript:openWinBackground(" + Event_Key.ToString() + ");return false;");                                                
                        lblgoback.Attributes.Add("OnClick", "javascript:goback('" + sitetemp.GetNavigateurl(fbid) + "default.aspx'); return false;");
                    }
                    else if (Request.QueryString["edit"] == "preview")
                    {
                        fbloggedin(false);                        
                        btnPreview.Text = "Edit Page";                        
                        lblgoback.Attributes.Add("OnClick", "javascript:goback('" + sitetemp.GetNavigateurl(fbid) + "default.aspx'); return false;");
                    }
                    PopulateExistingEvent(Event_Key);
                }
            }

            addfbtags();

        }

        protected void addfbtags()
        {
            HtmlMeta hm = new HtmlMeta();
            HtmlHead head = (HtmlHead)Page.Header;

            hm.Name = "og:image";            
            hm.Content = ConfigurationSettings.AppSettings.Get("Store_URL").ToString()+ imgEvent.ImageUrl.Substring(1);
            head.Controls.Add(hm);

            HtmlHead head2 = (HtmlHead)Page.Header;
            HtmlMeta hm2 = new HtmlMeta();

            hm2.Name = "og:url";
            hm2.Content = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Form.aspx?Event_Key=" + Event_Key;
            head2.Controls.Add(hm2);  
        }

        protected void fbloggedin(bool isedit)
        {
            Site sitetemp = new Site();
            //check if logged in or out of fb
            //Eventomatic.Addons.ConnectService fbconnect = new Eventomatic.Addons.ConnectService();            
            //fbid = 121100861;            
            if (HttpContext.Current.Session["fbuser"] == null) //No authorization or fbuid
            {
                Eventomatic.Addons.ConnectService fbconnect = new Eventomatic.Addons.ConnectService();
                if (fbconnect.IsConnected()) //have cookie & connected
                {
                    fbid = fbconnect.UserId;
                }
            }
            else
            {
                fbuser fbuser;
                fbuser = (Eventomatic.Addons.fbuser)HttpContext.Current.Session["fbuser"];
                fbid = fbuser.UID;
            }
            if (fbid != 0)
            //if (fbid == 121100861)
            {                
                pnlfbloggedin.Visible = true;
                //fbid = fbconnect.UserId;
                hdnfbid.Value = fbid.ToString();

                fbloggedin1.Setuser(fbid);

                int resource_key = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));

                //check if fbid is store admin
                if (sitetemp.CheckIfIsStoreAdmin(fbid, resource_key))
                {
                    //is store admin
                    fbloggedin1.Setmsg("You are an admin of this store");
                    if (isedit)
                    {
                        SettoEdit();
                        if (importfb)
                        {
                            PopulateFacebook(Request.QueryString["eid"], fbid);
                        }
                    }
                    else //preview mode
                    {
                        displaydemoticket = true;
                    }
                    //allow user to edit
                    btnPreview.Visible = true;
                    lblgoback.Visible = true;
                    pnladmintools.Visible = true;
                }
                else
                {
                    //not store admin
                    fbloggedin1.Setmsg("You are not an admin of this store");
                }
            }
        }

        protected void SettoEdit()
        {
            pnlfbloggedin.Visible = true;
            txtCity.Visible = true;
            txtComments.Visible = true;
            txtCouponCode.Visible = true;
            txtEmailInput.Visible = true;
            pnltxtEventName.Visible = true;
            pnlddlTimezone.Visible = true;
            txtHost.Visible = true;
            txtLocation.Visible = true;
            txtPhone.Visible = true;
            txtStreet.Visible = true;
            rdpEndDate.Visible = true;
            rdpEndTime.Visible = true;
            rdpStartDate.Visible = true;
            rdpStartTime.Visible = true;            

            pnlTickets_Edit.Visible = true;
            pnlQuestions_Edit.Visible = true;
            pnlConfirmation.Visible = true;
            pnltixdetails.Visible = true;

            btnSaveDetails.Visible = true;
            btnPreview.Visible = true;
            lblgoback.Visible = true;
            lblgoback.Visible = true;
            lblBackground.Visible = true;

            pnlTickets.Visible = false;

            pnlButtons.Visible = false;

            lblCity.Visible = false;
            lblComments.Visible = false;
            lblDemo.Visible = false;
            lblEmail.Visible = false;
            lblEnd.Visible = false;
            lblEvent_Name.Visible = false;
            lblHost.Visible = false;
            lblLocation.Visible = false;
            lblPhone.Visible = false;
            lblStart.Visible = false;
            lblStreet.Visible = false;
            hypMapit.Visible = false;

            RadTabStrip1.Visible = false;

            ReloadTickets();
            ReloadQuestions();
            
            //Insert Timezones
            DataSet dstemp = Eventomatic_DB.SPs.ViewInfoTimezones(0).GetDataSet();
            ddlTimezone.DataSource = dstemp.Tables[0];
            ddlTimezone.DataTextField = "Timezones_Text";
            ddlTimezone.DataValueField = "Timezones_Value";
            ddlTimezone.DataBind();

            AsyncUpload1.Visible = true;
            Thumbnail.Visible = true;
            imgEvent.Visible = false;

            imgGroup.Visible = false;
            Thumbnail2.Visible = true;
            AsyncUpload2.Visible = true;
        }

        protected bool IsDecimal(string theValue)
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

        protected void PopulateExistingEvent(int Event_Key)
        {
            lbleventurl.Text = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "order_form.aspx?event_key=" + Event_Key.ToString(); //"event/" + Event_Key.ToString();

            DataSet dstemp = Eventomatic_DB.SPs.ViewEventDetails(Event_Key).GetDataSet();

            lblEvent_Name.Text = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
            txtEventName.Text = lblEvent_Name.Text;
            Page.Title = lblEvent_Name.Text + " - theGroupstore.com";
            lblHost.Text = dstemp.Tables[0].Rows[0]["Host"].ToString();
            txtHost.Text = lblHost.Text;
            lblLocation.Text = dstemp.Tables[0].Rows[0]["Location"].ToString();
            txtLocation.Text = lblLocation.Text;
            lblStreet.Text = dstemp.Tables[0].Rows[0]["Street"].ToString();
            txtStreet.Text = lblStreet.Text;
            lblCity.Text = dstemp.Tables[0].Rows[0]["City"].ToString();
            txtCity.Text = lblCity.Text;
            lblPhone.Text = dstemp.Tables[0].Rows[0]["Phone"].ToString();
            txtPhone.Text = lblPhone.Text;

            if (dstemp.Tables[0].Rows[0]["Timezone"] != DBNull.Value)
            {
                int Timezone = Convert.ToInt32(dstemp.Tables[0].Rows[0]["Timezone"]);
                if (ddlTimezone.Items.FindByValue(Timezone.ToString()) != null)
                {
                    ddlTimezone.SelectedValue = Timezone.ToString();
                }
            }

            if (dstemp.Tables[0].Rows[0]["Confirmation"] != DBNull.Value)
            {
                txtConfirmation.Text = dstemp.Tables[0].Rows[0]["Confirmation"].ToString() ;
            }
            if (dstemp.Tables[0].Rows[0]["Display_Tickets_Available"] != DBNull.Value)
            {
                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Display_Tickets_Available"]) == true)
                {
                    chkDisplayAvailable.Checked = true;
                }
            }
            if (dstemp.Tables[0].Rows[0]["Ticket_Max"] != DBNull.Value)
            {
                int TicketMax = Convert.ToInt32(dstemp.Tables[0].Rows[0]["Ticket_Max"]);
                if (ddlMaxTickets.Items.FindByValue(TicketMax.ToString()) != null)
                {
                    ddlMaxTickets.SelectedValue = TicketMax.ToString();
                }
            }
            string streid = dstemp.Tables[0].Rows[0]["eid"].ToString();
            Loadfbmash(streid);
            /*
            if (dstemp.Tables[0].Rows[0]["Donation"] != DBNull.Value)
            {
                if (dstemp.Tables[0].Rows[0]["Donation"].ToString() == "True")
                {
                    lblGuestlist.Text = "Donator";
                    lblfGuestListComment.Text = "Who is making the donation?";
                }
            }*/
            Site Sitetemp = new Site();
            hdcurrencysymbol.Value = Sitetemp.GetCurrencySymbol(Event_Key);
            string strMapit = lblStreet.Text.Replace(" ", "+") + "," + lblCity.Text.Replace(" ", "+");
            hypMapit.NavigateUrl = "http://maps.google.com/maps?f=q&hl=en&q=" + strMapit;
            if ((lblStreet.Text == "") && (lblCity.Text == ""))
            {
                hypMapit.Visible = false;
            }
            if (lblPhone.Text.Length > 0)
            {
                lblPhone.Text = lblPhone.Text + "&nbsp;&nbsp;&nbsp;";
            }
            lblEmail.Text = dstemp.Tables[0].Rows[0]["Email"].ToString();
            txtPhone.Text = lblPhone.Text;
            txtEmailInput.Text = lblEmail.Text;
            string strComments = dstemp.Tables[0].Rows[0]["Additional_Comments"].ToString();
            txtComments.Text = strComments;
            if (Request.QueryString["fbid"] != null)
            {
                if (Sitetemp.IsNumeric(Request.QueryString["fbid"]))
                {
                    DataSet dseventfb = Eventomatic_DB.SPs.ViewStoreSellersEventKey(Convert.ToInt64(Request.QueryString["fbid"]), Event_Key).GetDataSet();
                    if (dseventfb.Tables[0].Rows.Count > 0)
                    {
                        if (dseventfb.Tables[0].Rows[0]["Descriptionfull"] != DBNull.Value)
                        {
                            strComments = dseventfb.Tables[0].Rows[0]["Descriptionfull"].ToString();
                        }
                    }
                }
            }

            string strComments2 = strComments.Replace(new String((char)13, 1), "<br>");
            strComments2 = strComments2.Replace("\n", "<br/>");
            if (strComments2.ToLower().Contains("http://"))
            {
                Site sitetemp2 = new Site();
                strComments2 = sitetemp2.ReplaceLinks(strComments2);
            }
            lblComments.Text = strComments2;

            //Replace(strValue, Chr(13) & Chr(10), "<br>")
            lblGroupName.Text = dstemp.Tables[0].Rows[0]["Group_Name"].ToString() + "'s";

            //Add images
            string Resource_Key = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();


            imgGroup.ImageUrl = Sitetemp.GetResourcePic(Resource_Key);
            Thumbnail2.ImageUrl = imgGroup.ImageUrl;
            //check if is person's groupstore
            if (Request.QueryString["fbid"] != null)
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
                try
                {
                    string result = wc.DownloadString("http://graph.facebook.com/" + Request.QueryString["fbid"].ToString() + "/picture");
                    imgGroup.ImageUrl = "http://graph.facebook.com/" + Request.QueryString["fbid"].ToString() + "/picture";

                    result = wc.DownloadString("http://graph.facebook.com/" + Request.QueryString["fbid"].ToString() + "?fields=name");
                    if (!result.Contains("error"))
                    {
                        string[] strresults = result.Split('"');
                        lblGroupName.Text = strresults[3] + "'s";
                    }
                    hdspecificuser.Value = "True";
                }
                catch
                {
                }
            }

            imgEvent.ImageUrl = Sitetemp.GetEventPic(Event_Key.ToString());
            Thumbnail.ImageUrl = imgEvent.ImageUrl;
            if (dstemp.Tables[0].Rows[0]["Ticket_Max"] != DBNull.Value)
            {
                hdTicketMax.Value = dstemp.Tables[0].Rows[0]["Ticket_Max"].ToString();
            }

            DateTime tempdatetime;

            if (dstemp.Tables[0].Rows[0]["Event_Begins"] != DBNull.Value)
            {
                tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Event_Begins"].ToString());
                lblStart.Text = tempdatetime.ToString("dddd, MMMM d yyyy") + " at " + tempdatetime.ToString("h:mm tt ");
                rdpStartDate.SelectedDate = tempdatetime;
                rdpStartTime.SelectedDate = tempdatetime;
            }

            if (dstemp.Tables[0].Rows[0]["Event_Ends"] != DBNull.Value)
            {
                tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Event_Ends"].ToString());
                lblEnd.Text = tempdatetime.ToString("dddd, MMMM d yyyy") + " at " + tempdatetime.ToString("h:mm tt ");
                rdpEndDate.SelectedDate = tempdatetime;
                rdpEndTime.SelectedDate = tempdatetime;
            }            

            char chr34 = Convert.ToChar(34);
            lblfblike.Text = "<fb:like href=" + chr34 + ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Form.aspx?Event_Key=" + Event_Key.ToString() + chr34 + " layout=" + chr34 + "button_count" + chr34 + " show_faces=" + chr34 + "false" + chr34 + " font=" + chr34 + "arial" + chr34 + "></fb:like>";                
            //lblfblike.Text = "<iframe src='http://www.facebook.com/plugins/like.php?href=" + ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Form.aspx?Event_Key=" + Event_Key.ToString() + "&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:hidden; height:21px;' allowTransparency='true'></iframe>";

            //check if the event has been removed
            bool IsSelling = false; //if true, that means it is selling now
            bool isremoved = false;
            if (dstemp.Tables[0].Rows[0]["Visible"] != DBNull.Value)
            {
                if (dstemp.Tables[0].Rows[0]["Visible"].ToString() == "False")
                {
                    isremoved = true;
                }
            }

            dstemp = Eventomatic_DB.SPs.ViewTicketAll(Event_Key).GetDataSet();

            DateTime Begin_Selling = DateTime.MinValue;
            DateTime Selling_Deadline = DateTime.MaxValue;
            DateTime Begin_Selling_Earliest = DateTime.MinValue;
            DateTime Selling_Deadling_Latest = DateTime.MinValue;
            foreach (DataRow row in dstemp.Tables[0].Rows)
            {
                if (row["Sale_Begins"] != DBNull.Value)
                {
                    Begin_Selling = Convert.ToDateTime(row["Sale_Begins"].ToString());
                }

                if (row["Sale_Ends"] != DBNull.Value)
                {
                    Selling_Deadline = Convert.ToDateTime(row["Sale_Ends"].ToString());
                }
                if ((Begin_Selling != DateTime.MinValue) && (Selling_Deadline != DateTime.MaxValue) && (Begin_Selling <= DateTime.Now) && (Selling_Deadline >= DateTime.Now))
                {
                    IsSelling = true;
                }
                else if ((Begin_Selling > DateTime.Now) && (Begin_Selling_Earliest < Begin_Selling))
                {
                    Begin_Selling_Earliest = Begin_Selling;
                }
                if (Selling_Deadline > Selling_Deadling_Latest)
                {
                    Selling_Deadling_Latest = Selling_Deadline;
                }
            }


            if (isremoved)
            {
                IsSelling = false;
            }

            if (IsSelling)
            {
                //Input Current time & Latest Selling Time into hidden variable for the coutndown clock
                hdLastTicketDate.Value = Selling_Deadling_Latest.ToString("MMM dd, yyyy HH:mm:ss");// ("dd/MM/yyyy hh:mm:ss");                       
                
                dstemp = Eventomatic_DB.SPs.ViewTicket(Event_Key).GetDataSet();
                                
                if (displaydemoticket) //show demo ticket
                {
                    dstemp = Eventomatic_DB.SPs.ViewTicketDemo(Event_Key).GetDataSet();                    
                }

                DataRow drtemp = dstemp.Tables[0].NewRow();
                dstemp.Tables[0].Rows.Add(drtemp);

                if (dstemp.Tables[0].Rows[0]["Display_Tickets_Available"] != DBNull.Value)
                {
                    if (dstemp.Tables[0].Rows[0]["Display_Tickets_Available"].ToString() == "True")
                    {
                        GridView1.Columns[1].Visible = true;
                    }
                }
                                

                GridView1.DataSource = dstemp.Tables[0];
                GridView1.DataBind();                

                
                DataSet dstemp2 = Eventomatic_DB.SPs.ViewQuestion(Event_Key).GetDataSet();
                if (hdFreeTicket.Value == "True")
                {
                }

                /*
                GridView2.DataSource = dstemp2.Tables[0];
                GridView2.DataBind();
                */

                if (Sitetemp.ShowMobileSales(Event_Key))
                {
                    pnlMobile.Visible = true;

                    //Send New to user control
                    HiddenField hdnEvent_Key = new HiddenField();
                    hdnEvent_Key = (HiddenField)LeaderBoard1.FindControl("hdEvent_Key");
                    hdnEvent_Key.Value = Event_Key.ToString();

                    HiddenField hdonlytop = new HiddenField();
                    hdonlytop = (HiddenField)LeaderBoard1.FindControl("hdonlytop");
                    hdonlytop.Value = "1";

                    //Send New to user control
                    HiddenField hdnEvent_Key2 = new HiddenField();
                    hdnEvent_Key = (HiddenField)EventNewsFeed1.FindControl("hdEvent_Key");
                    hdnEvent_Key.Value = Event_Key.ToString();

                    HiddenField hdonlytop2 = new HiddenField();
                    hdonlytop = (HiddenField)EventNewsFeed1.FindControl("hdonlytop");
                    hdonlytop.Value = "1";

                    imgMobileSell.ImageUrl = Sitetemp.Getqrurl(Event_Key);

                    btnshare.Attributes.Add("onclick", "javascript:openWinSelltix(" + Event_Key + ");");                    

                    lblgskey.Text = "(For BB App, use Groupstore Key " + Resource_Key.ToString() + ")";

                    if (Sitetemp.HavePermission_Eventkey(Event_Key))
                    {
                        if ((Sitetemp.isMobile()) && (Request.QueryString["notmobile"] == null))
                        {
                            Response.Redirect(ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Mobile.aspx?event=" + Event_Key.ToString());
                        }                        
                    }
                    else //haven't given paypal permission yet
                    {
                        pnlgivemobilepermission.Visible = true;
                        hypbbapp.Visible = false;
                        lblgskey.Visible = false;
                        imgMobileSell.Visible = false;
                    }
                }
            }
            else
            {
                //Selling hasn't begun yet
                lblSellingDeadline.Visible = true;
                lblSellingDeadline.ForeColor = System.Drawing.Color.Red;
                if (isremoved)
                {
                    lblSellingDeadline.Text = "This event is no longer selling tickets. Contact the event organizer if you would like to know why.";
                }
                else if (Begin_Selling_Earliest != DateTime.MinValue)
                {
                    lblSellingDeadline.Text = "Tickets for this event will be going on sale beginning " + Begin_Selling_Earliest.ToString("dddd, MMMM d yyyy") + ".";
                }
                else
                {
                    lblSellingDeadline.Text = "Tickets for this event are currently not selling.";
                }
                chkTerms.Enabled = false;
                pnlButtons.Visible = false;

                if (displaydemoticket) //show demo ticket
                {
                    DataSet dsdemo = Eventomatic_DB.SPs.ViewTicketDemo(Event_Key).GetDataSet();
                    if (dsdemo.Tables[0].Rows.Count > 0)
                    {
                        DataRow drtemp = dsdemo.Tables[0].NewRow();
                        dsdemo.Tables[0].Rows.Add(drtemp);
                       
                        GridView1.DataSource = dsdemo.Tables[0];
                        GridView1.DataBind();

                        chkTerms.Enabled = true;
                        pnlButtons.Visible = true;
                    }
                }
            }

            Changebackground();
        }

        protected void Loadfbmash(string streid)
        {
            Site Sitetemp = new Site();
            //Loads Sellers & Comments
            string appid = System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString();
            string xid = appid + "_" + Event_Key.ToString();
            //lblfbcomments.Text = "<fb:comments xid=" + chr34 + xid + chr34 + " canpost=" + chr34 + "true" + chr34 + " candelete=" + chr34 + "false" + chr34 + " width=" + chr34 + "200px" + chr34 + "></fb:comments>";
            //lblfbcomments.Text = "<fb:comments xid='titans_comments' canpost='true' candelete='false' returnurl='http://apps.facebook.com/myapp/titans/'><fb:title>Talk about the Titans</fb:title></fb:comments>";                

            lblsharebtn.Text = "<fb:share-button class='meta'><meta name='medium' content='mult'/><meta name='title' content='" + lblEvent_Name.Text + "'/>";
            if (Sitetemp.isAlphaNumeric(lblComments.Text))
            {
                lblsharebtn.Text += "<meta name='description' content='" + lblComments.Text + "'/>";
            }
            lblsharebtn.Text += "<link rel='image_src' href='" + imgEvent.ImageUrl + "'/>";
            lblsharebtn.Text += "<link rel='target_url' href='" + System.Configuration.ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "order_form.aspx?event_key=" + Event_Key.ToString() + "'/>";

            if (streid != "0")
            {
                JArray jsonAttending = Sitetemp.GetEventAttending(streid, 0);
                if (jsonAttending.Count > 0)
                {
                    DataSet dsattending = Sitetemp.GetdsEventAttending(jsonAttending, 0);

                    FaceMash1.Load_Pics(dsattending.Tables[0]);
                    FaceMash2.Load_Pics(dsattending.Tables[1]);
                    FaceMash3.Load_Pics(dsattending.Tables[2]);

                    lblAttending.Text = lblAttending.Text.Replace("#", dsattending.Tables[0].Rows.Count.ToString());
                    lblAttending2.Text = lblAttending2.Text.Replace("#", dsattending.Tables[1].Rows.Count.ToString());
                    lblAttending3.Text = lblAttending3.Text.Replace("#", dsattending.Tables[2].Rows.Count.ToString());

                    lbviewattending.OnClientClick = lbviewattending.OnClientClick.Replace("#", streid);
                    lbviewattending2.OnClientClick = lbviewattending2.OnClientClick.Replace("#", streid);
                    lbviewattending3.OnClientClick = lbviewattending3.OnClientClick.Replace("#", streid);

                    if (dsattending.Tables[0].Rows.Count > 0)
                    {
                        pnlfbattending.Visible = true;
                    }
                    if (dsattending.Tables[1].Rows.Count > 0)
                    {
                        pnlfbattending2.Visible = true;
                    }
                    if (dsattending.Tables[2].Rows.Count > 0)
                    {
                        //pnlfbattending3.Visible = true;
                    }
                    pnlAttending.Visible = true;
                }
            }
        }

        protected void Changebackground()
        {
            //Check Background Image
            DataSet dstemp = Eventomatic_DB.SPs.ViewEventBkImgUrl(Event_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["BkImgUrl"] != DBNull.Value)
            {
                string strtemp = dstemp.Tables[0].Rows[0]["BkImgUrl"].ToString().Trim();
                string[] strvalues = strtemp.Split('/');

                hdBackgroundImage.Value = "../Images/BackgroundImages/" + strvalues[strvalues.Length - 1];
            }
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicketsSoldout(Event_Key).GetDataSet();

            string strfirsttix = dstemp.Tables[0].Rows[0]["Ticket_Description"].ToString();
            string tempstring = sender.ToString();
            tempstring = e.ToString();
            Site sitetemp = new Site();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowscount = Eventomatic_DB.SPs.ViewTicket(Event_Key).GetDataSet().Tables[0].Rows.Count;
                if (displaydemoticket)
                {
                    rowscount = Eventomatic_DB.SPs.ViewTicketDemo(Event_Key).GetDataSet().Tables[0].Rows.Count;
                }
                else if (strfirsttix == "Try Groupstore Demo Ticket")
                {                    
                    dstemp.Tables[0].Rows.RemoveAt(0);
                }
                if (e.Row.RowIndex == rowscount)
                {
                    System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity");   // look for dropdown in the row            
                    if (dropDownList != null)
                    { dropDownList.Visible = false; }

                    System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblServiceFee");
                    if (label != null)
                    {
                        label.Visible = true;
                    }


                }
                DataSet dstempquick = Eventomatic_DB.SPs.ViewTicket(Event_Key).GetDataSet();
                //check if it is soldout or not                
                if (dstempquick.Tables[0].Rows.Count - 1 >= e.Row.RowIndex)
                {
                    int Ticket_Max = Convert.ToInt32(hdTicketMax.Value);
                    int tickets_left = 0;
                    System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity");   // look for dropdown in the row
                    System.Web.UI.WebControls.Label lblPrice = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblPrice");   // look for price in the row                
                    System.Web.UI.WebControls.Label lblEnds = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblEnds");   // look for price in the row                                
                    System.Web.UI.WebControls.Label Timezoneshort = (System.Web.UI.WebControls.Label)e.Row.FindControl("Timezoneshort");

                    //Check if there is a free ticket being sold
                    if (lblPrice.Text.Trim() == "0.00")
                    {
                        hdFreeTicket.Value = "True";
                    }

                    lblPrice.Text = sitetemp.GetCurrencySymbol(Event_Key) + lblPrice.Text;
                    lblEnds.Text += " " + Timezoneshort.Text;

                    if (dstemp.Tables[0].Rows[e.Row.RowIndex]["Tickets_Left"] != DBNull.Value)
                    {
                        tickets_left = Convert.ToInt32(dstemp.Tables[0].Rows[e.Row.RowIndex]["Tickets_Left"]);
                    }
                    //Set Ticket Max
                    for (int i = Ticket_Max; i < 10; i++)
                    {
                        dropDownList.Items.RemoveAt(dropDownList.Items.Count - 1);
                    }
                    if ((dstemp.Tables[0].Rows[e.Row.RowIndex]["Sold_Out"].ToString() == "Sold Out") && ((System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity") != null))
                    {
                        dropDownList.Visible = false;
                        System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblServiceFee");
                        label.Visible = true;
                        label.Text = "Sold Out";
                    }
                    else if (tickets_left < Ticket_Max)
                    {
                        for (int i = tickets_left; i < Ticket_Max; i++)
                        {
                            dropDownList.Items.RemoveAt(dropDownList.Items.Count - 1);
                        }
                    }


                    //check if donation box
                    if (dstempquick.Tables[0].Rows[e.Row.RowIndex]["isdonation"] != DBNull.Value)
                    {
                        string strtemp = dstempquick.Tables[0].Rows[e.Row.RowIndex]["isdonation"].ToString();
                        if (dstempquick.Tables[0].Rows[e.Row.RowIndex]["isdonation"].ToString().ToLower() == "true")
                        {
                            dropDownList.Visible = false;
                            lblPrice.Visible = false;
                            System.Web.UI.WebControls.Label lblDollarsign = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDollarSign");   // look for price in the row                
                            System.Web.UI.WebControls.TextBox txtDonate = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtDonate");
                            System.Web.UI.WebControls.Label lblDonate = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDonate");   // look for price in the row                
                            txtDonate.Visible = true;
                            lblDonate.Visible = true;
                            lblDollarsign.Visible = true;
                        }
                    }
                }

                //check if row is a coupon code
                System.Web.UI.WebControls.Label lblCouponCode = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblCouponCode");
                if (lblCouponCode.Text != "")
                {
                    pnlCouponCode.Visible = true;
                    e.Row.Visible = false;
                    hdgvoffset.Value = Convert.ToString(Convert.ToInt32(hdgvoffset.Value) + 1);
                }
            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.DropDownList ddltemp = (System.Web.UI.WebControls.DropDownList)GridView1.Rows[0].FindControl("ddlQuantity");
                System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity");
                for (int i = ddltemp.Items.Count; i <= 10; i++)
                {
                    dropDownList.Items.RemoveAt(dropDownList.Items.Count - 1);
                }
            }

            if (e.Row.RowIndex == Eventomatic_DB.SPs.ViewTicket(Event_Key).GetDataSet().Tables[0].Rows.Count)
            {
                System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity");   // look for dropdown in the row            
                if (dropDownList != null)
                { dropDownList.Visible = false; }

                System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblServiceFee");
                if (label != null)
                {
                    label.Visible = true;
                }


            }
        }


        // Handle the event
        public void ddlQuantity_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void GotoPaypal()
        {
            /*decimal OverallTotal = 0;
            decimal TotalSum;
            System.Web.UI.WebControls.Label lblTotalSum;
            System.Web.UI.WebControls.DropDownList ddlQuantity;
            string strPurchaseDescription;
            string PurchaseDescription = "";
            Hashtable Tickets_Purchased = new Hashtable();
            int Itemnum = 1;
            Site sitetemp = new Site();


            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                if (gvrow.RowIndex != GridView1.Rows.Count - 1)
                {
                    TotalSum = 0;
                    if (gvrow.RowType == DataControlRowType.DataRow)
                    {
                        lblTotalSum = (System.Web.UI.WebControls.Label)gvrow.FindControl("lblPrice");
                        TotalSum = Convert.ToDecimal(sitetemp.RemoveCurrencySymbol(lblTotalSum.Text));
                        ddlQuantity = (System.Web.UI.WebControls.DropDownList)gvrow.FindControl("ddlQuantity");
                        OverallTotal += TotalSum * ddlQuantity.SelectedIndex;
                        if (ddlQuantity.SelectedIndex > 0)
                        {
                            strPurchaseDescription = gvrow.Cells[0].Text; //.FindControl("Ticket_Description");
                            if (PurchaseDescription.Length > 0)
                            {
                                PurchaseDescription += ", ";
                            }
                            PurchaseDescription += strPurchaseDescription;
                            Tickets_Purchased.Add(Eventomatic_DB.SPs.ViewTicketSpecific(gvrow.Cells[0].Text, Event_Key, TotalSum).GetDataSet().Tables[0].Rows[0]["Ticket_Key"].ToString(), ddlQuantity.SelectedIndex);

                            System.Web.UI.WebControls.HiddenField hdtempAmount = new System.Web.UI.WebControls.HiddenField();
                            hdtempAmount.Value = decimal.Round(TotalSum, 2).ToString();
                            hdtempAmount.ID = "amount_" + Itemnum.ToString();
                            form1.Controls.Add(hdtempAmount);

                            System.Web.UI.WebControls.HiddenField hdtempDesc = new System.Web.UI.WebControls.HiddenField();
                            hdtempDesc.Value = strPurchaseDescription;
                            hdtempDesc.ID = "item_name_" + Itemnum.ToString();
                            form1.Controls.Add(hdtempDesc);

                            System.Web.UI.WebControls.HiddenField hdtempQuantity = new System.Web.UI.WebControls.HiddenField();
                            hdtempQuantity.Value = ddlQuantity.SelectedIndex.ToString();
                            hdtempQuantity.ID = "quantity_" + Itemnum.ToString();
                            form1.Controls.Add(hdtempQuantity);

                            Itemnum += 1;
                        }
                    }
                }

            }

            string strQ1 = System.Configuration.ConfigurationSettings.AppSettings.Get("Free_Q1").ToString();
            string strQ2 = System.Configuration.ConfigurationSettings.AppSettings.Get("Free_Q2").ToString();
            string strQ3 = System.Configuration.ConfigurationSettings.AppSettings.Get("Free_Q3").ToString();

            if (OverallTotal > 0)
            {
                //Add Service fee to overall total             

                decimal ServiceFee = Convert.ToDecimal(hdServiceFee.Value);

                OverallTotal += decimal.Round(ServiceFee, 2);//(OverallTotal * SFP) + SFC;


                //Get IP Address Request.UserHostAddress
                //update Transaction & get tx_Key
                StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), "CAD", 0, "", "", "", "", 1, "", "", 0, "", "", "", "", "", "", "", txtList_FirstName.Text, txtList_LastName.Text, ServiceFee, HttpContext.Current.Request.UserHostAddress);
                sp_UpdateTransaction.Execute();
                string tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();

                //update Tickets Purchased
                foreach (DictionaryEntry de in Tickets_Purchased)
                {
                    Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), Convert.ToInt32(de.Value), 0).Execute();
                }

                //update Questions Answered
                Questions_Order_Form1.SaveQuestionsAnswered(tempTx_Key);

                System.Web.UI.WebControls.HiddenField hdtemp = new System.Web.UI.WebControls.HiddenField();
                DataSet dstemp = Eventomatic_DB.SPs.ViewPaypalEmail(Event_Key).GetDataSet();
                Boolean isdemovar = sitetemp.IsDemo(Event_Key);

                if (isdemovar)
                {
                    //Live
                    hdtemp.Value = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();

                }
                else
                {
                    //Trial
                    hdtemp.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("Trial_Email").ToString();
                }

                hdtemp.ID = "business";
                form1.Controls.Add(hdtemp);

                System.Web.UI.WebControls.HiddenField hdtemp2 = new System.Web.UI.WebControls.HiddenField();
                //hdtemp3.Value = "http://localhost:57042/Order_Confirmation.aspx?Event_Key=" + Event_Key + "&Tx_Key=" + tempTx_Key.ToString();
                hdtemp2.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("Order_Form_ReturnURL").ToString() + Event_Key + "&Tx_Key=" + tempTx_Key.ToString();
                hdtemp2.ID = "return";
                form1.Controls.Add(hdtemp2);


                System.Web.UI.WebControls.HiddenField hdtemp3 = new System.Web.UI.WebControls.HiddenField();
                hdtemp3.Value = "_cart";
                hdtemp3.ID = "cmd";
                form1.Controls.Add(hdtemp3);

                System.Web.UI.WebControls.HiddenField hdtemp4 = new System.Web.UI.WebControls.HiddenField();
                hdtemp4.Value = "CAD";
                hdtemp4.ID = "currency_code";
                form1.Controls.Add(hdtemp4);

                System.Web.UI.WebControls.HiddenField hdtemp5 = new System.Web.UI.WebControls.HiddenField();
                //hdtemp7.Value = "http://localhost:57042/IPNing.aspx?Event_Key=" + Event_Key;
                hdtemp5.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("Order_Form_IPNURL").ToString() + Event_Key;
                hdtemp5.ID = "notify_url";
                form1.Controls.Add(hdtemp5);

                System.Web.UI.WebControls.HiddenField hdtemp6 = new System.Web.UI.WebControls.HiddenField();
                hdtemp6.Value = tempTx_Key;
                hdtemp6.ID = "custom";
                form1.Controls.Add(hdtemp6);


                if (sitetemp.ImgResourceThumbExists(sitetemp.GetResourceKeyEventKey(Event_Key) + ".jpg"))
                {
                    System.Web.UI.WebControls.HiddenField hdtemp7 = new System.Web.UI.WebControls.HiddenField();
                    hdtemp7.Value = sitetemp.GetResourceThumbPic(sitetemp.GetResourceKeyEventKey(Event_Key));
                    hdtemp7.ID = "image_url";
                    form1.Controls.Add(hdtemp7);
                }

                System.Web.UI.WebControls.HiddenField hdtemp8 = new System.Web.UI.WebControls.HiddenField();
                hdtemp8.Value = "1";
                hdtemp8.ID = "no_shipping";
                form1.Controls.Add(hdtemp8);

                System.Web.UI.WebControls.HiddenField hdtemp9 = new System.Web.UI.WebControls.HiddenField();
                hdtemp9.Value = "1";
                hdtemp9.ID = "upload";
                form1.Controls.Add(hdtemp9);


                if (isdemovar)
                {
                    form1.Action = System.Configuration.ConfigurationSettings.AppSettings.Get("Order_Form_ActionURLLive").ToString();
                }
                else
                {
                    form1.Action = System.Configuration.ConfigurationSettings.AppSettings.Get("Order_Form_ActionURLTrial").ToString();
                }

                form1.Method = "post";
                //Anthem.Manager.AddScriptForClientSideEval("document.form1.submit();");
                //form1.Target = "paypal";

                Page.RegisterStartupScript("Myscript", "<script language=javascript>submitit();</script>");
                /*
                 * buyer - lorneb_1242411941_per@lornestar.com - pwd-261378132
                 * seller - lorne_1242411549_biz@lornestar.com - pwd-261378194
                 * 
                 * seller - lorne_1261162854_biz@lornestar.com - pwd-261378294
                 *
                 * 
                 * buyer - evento_1252204669_biz@lornestar.com   pwd-258588241
         
                          <form action="https://www.paypal.com/cgi-bin/webscr" method="post">
                <input type="hidden" name="cmd" value="_s-xclick">
                <input type="hidden" name="hosted_button_id" value="5267147">
                <input type="image" src="https://www.paypal.com/en_US/i/btn/btn_buynowCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!">
                <img alt="" border="0" src="https://www.paypal.com/en_US/i/scr/pixel.gif" width="1" height="1">
                </form>
                */
              /*    
            }
            //Free Event
            else if ((Tickets_Purchased.Count > 0) && (OverallTotal == 0))
            {
                string strFirstName = "";
                string strLastName = "";
                string strEmail = "";
                System.Web.UI.WebControls.Label Question_Key;
                System.Web.UI.WebControls.Label Question_Text;
                System.Web.UI.WebControls.TextBox QuestionsAnsweredTextbox;
                //Store first name, last name & email to database
              
                //update Transaction & get tx_Key
                StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), "CAD", 0, strFirstName, strLastName, strEmail, "", 3, "", "", 0, "", "", "", strEmail, "", "", "", txtList_FirstName.Text, txtList_LastName.Text, 0, HttpContext.Current.Request.UserHostAddress);
                sp_UpdateTransaction.Execute();
                string tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();

                //update Tickets Purchased
                foreach (DictionaryEntry de in Tickets_Purchased)
                {
                    Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), Convert.ToInt32(de.Value), 0).Execute();
                }

              
                Send_Email SE = new Send_Email();
                SE.Send_Transaction_Email(Convert.ToInt32(tempTx_Key), "");
                Response.Redirect("Order_Confirmation.aspx?Event_Key=" + Event_Key);
            }*/

        }



        protected void btnTest_Click(object sender, EventArgs e)
        {
            
            lblTest.Text = DateTime.Now.ToString();
        }

        protected void btnPurchase_Click(object sender, EventArgs e)
        {

        }

     
    

        protected int SetupTx()
        {
            decimal OverallTotal = 0;
            decimal TotalSum;
            System.Web.UI.WebControls.Label lblTotalSum;
            System.Web.UI.WebControls.DropDownList ddlQuantity;
            string strPurchaseDescription;
            string PurchaseDescription = "";
            Hashtable Tickets_Purchased = new Hashtable();
            Site sitetemp = new Site();

            //Figure out Purchase Description & Setup Hashtable            
            pnlTickets.Visible = true;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                if (gvrow.RowType == DataControlRowType.DataRow)
                {
                    TotalSum = 0;
                    
                        bool isdonation = false;
                        lblTotalSum = (System.Web.UI.WebControls.Label)gvrow.FindControl("lblPrice");
                        System.Web.UI.WebControls.TextBox txtDonate = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("txtDonate");
                        ddlQuantity = (System.Web.UI.WebControls.DropDownList)gvrow.FindControl("ddlQuantity");
                        System.Web.UI.WebControls.Label lblTicketKey = (System.Web.UI.WebControls.Label)gvrow.FindControl("lblTicketKey");

                        if ((txtDonate.Visible) || (ddlQuantity.Visible))
                        {
                            if (txtDonate.Visible)
                            {
                                isdonation = true;
                            }

                            if (isdonation)
                            {
                                TotalSum = Convert.ToDecimal(txtDonate.Text);
                                if (TotalSum > 0) //they are actually donating an amount
                                {
                                    OverallTotal += TotalSum;
                                    Tickets_Purchased.Add(lblTicketKey.Text, TotalSum + "d");
                                }
                            }
                            else
                            {
                                TotalSum = Convert.ToDecimal(sitetemp.RemoveCurrencySymbol(lblTotalSum.Text));
                                OverallTotal += TotalSum * ddlQuantity.SelectedIndex;
                            }

                            if ((ddlQuantity.SelectedIndex > 0) && (ddlQuantity.Visible))
                            {
                                strPurchaseDescription = gvrow.Cells[0].Text; //.FindControl("Ticket_Description");
                                if (PurchaseDescription.Length > 0)
                                {
                                    PurchaseDescription += ", ";
                                }
                                PurchaseDescription += strPurchaseDescription;
                                Tickets_Purchased.Add(lblTicketKey.Text, ddlQuantity.SelectedIndex);

                            }
                        }                        
                    
                }

            }
            pnlTickets.Visible = false;
            string tempTx_Key = "0";
            decimal ServiceFee = Convert.ToDecimal(hdServiceFee.Value);
            OverallTotal = Convert.ToDecimal(hdOverallTotal.Value);
            int resource_key = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));
            string currency = sitetemp.GetCurrency(resource_key);

            bool IsFree = false;

            TextBox txtList_FirstName = (TextBox)RadListView3.Items[0].FindControl("txtList_FirstName");
            TextBox txtList_LastName = (TextBox)RadListView3.Items[0].FindControl("txtList_LastName");
            //TextBox txtFreeEmail = (TextBox)RadListView3.Items[0].FindControl("txtFreeEmail");

            if (OverallTotal > 0) //Money being spent
            {
                //Add Service fee to overall total                        

                //OverallTotal += decimal.Round(ServiceFee, 2);//(OverallTotal * SFP) + SFC;

                //Get IP Address Request.UserHostAddress
                //update Transaction & get tx_Key
                StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "", "", 1, "", "", 0, "", "", "", "", "", "", "", txtList_FirstName.Text, txtList_LastName.Text, ServiceFee, HttpContext.Current.Request.UserHostAddress);
                sp_UpdateTransaction.Execute();
                tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
            }
            else if ((Tickets_Purchased.Count > 0) && (OverallTotal == 0))//Free Events
            {
                IsFree = true;
                //update Transaction & get tx_Key
                StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "", txtFreeEmail.Text, 3, "", "", 0, "", "", "", txtFreeEmail.Text, "", "", "", txtList_FirstName.Text,txtList_LastName.Text, 0, HttpContext.Current.Request.UserHostAddress);
                sp_UpdateTransaction.Execute();
                tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
            }

            int counttix = 0;

            //update Tickets Purchased
            foreach (DictionaryEntry de in Tickets_Purchased)
            {
                if (de.Value.ToString().Contains("d"))
                {                    
                    TextBox txtList_FirstNameNum = (TextBox)RadListView3.Items[counttix].FindControl("txtList_FirstName");
                    TextBox txtList_LastNameNum = (TextBox)RadListView3.Items[counttix].FindControl("txtList_LastName");
                    StoredProcedure sp_Updatetix = Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), 1, Convert.ToDecimal(de.Value.ToString().Replace("d", "")), 0, txtList_FirstNameNum.Text, txtList_LastNameNum.Text);
                    sp_Updatetix.Execute();

                    int temptixkey = Convert.ToInt32(sp_Updatetix.Command.Parameters[4].ParameterValue.ToString());
                    Questions_Order_Form questions = (Questions_Order_Form)RadListView3.Items[counttix].FindControl("Questions_Order_Form1");
                    questions.SaveQuestionsAnswered(tempTx_Key, temptixkey);
                    
                    if (IsFree)
                    {
                        Eventomatic_DB.SPs.UpdateTicketNumFreeEvents(Convert.ToInt32(tempTx_Key)).Execute();
                    }
                    counttix += 1;
                }
                else
                {
                    for (int i = 1; i <= Convert.ToInt32(de.Value); i++)
                    {
                        TextBox txtList_FirstNameNum = (TextBox)RadListView3.Items[counttix].FindControl("txtList_FirstName");
                        TextBox txtList_LastNameNum = (TextBox)RadListView3.Items[counttix].FindControl("txtList_LastName");

                        StoredProcedure sp_Updatetix = Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key),1, 0,0,txtList_FirstNameNum.Text,txtList_LastNameNum.Text);
                        sp_Updatetix.Execute();

                        int temptixkey = Convert.ToInt32(sp_Updatetix.Command.Parameters[4].ParameterValue.ToString());
                        Questions_Order_Form questions = (Questions_Order_Form)RadListView3.Items[counttix].FindControl("Questions_Order_Form1");
                        questions.SaveQuestionsAnswered(tempTx_Key, temptixkey);
                        
                        if (IsFree)
                        {
                            Eventomatic_DB.SPs.UpdateTicketNumFreeEvents(Convert.ToInt32(tempTx_Key)).Execute();
                        }
                        counttix += 1;
                    }                    
                }
            }
            

            //update Questions Answered
            //Questions_Order_Form1.SaveQuestionsAnswered(tempTx_Key);

            //update ticket seller
            if (hdspecificuser.Value == "True")
            {
                Eventomatic_DB.SPs.UpdateTransactionFbids(Convert.ToInt64(Request.QueryString["fbid"].ToString()), 0, Convert.ToInt32(tempTx_Key)).Execute();
            }
            
            return Convert.ToInt32(tempTx_Key);
        }
        

        protected bool TicketsSoldOut()//true = Sold out / false = Available
        {
            bool TicketsSoldOut = false;
            System.Web.UI.WebControls.Label lblTicket_Key;
            System.Web.UI.WebControls.DropDownList dropDownList;
            System.Web.UI.WebControls.Label lblServiceFee;
            Site sitetemp = new Site();
            foreach (GridViewRow gvrow in GridView1.Rows)
            {

                if (gvrow.RowType == DataControlRowType.DataRow)
                {
                    lblTicket_Key = (System.Web.UI.WebControls.Label)gvrow.FindControl("lblTicketKey");
                    dropDownList = (System.Web.UI.WebControls.DropDownList)gvrow.FindControl("ddlQuantity");   // look for dropdown in the row
                    lblServiceFee = (System.Web.UI.WebControls.Label)gvrow.FindControl("lblServiceFee");
                    if (sitetemp.IsNumeric(lblTicket_Key.Text))
                    {
                        if ((sitetemp.IsSoldOut(Convert.ToInt32(lblTicket_Key.Text))) && (dropDownList.SelectedIndex > 0))
                        {
                            TicketsSoldOut = true;
                            dropDownList.Visible = false;
                            lblServiceFee.Visible = true;
                            lblServiceFee.Text = "Sold Out";
                        }
                    }
                }
            }
            if (TicketsSoldOut)//sold out
            {
                lblError.Visible = true;
                lblError.Text = "Unfortunately the tickets you selected are now Sold Out.";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            return TicketsSoldOut;
        }


        protected void PayPayPal()
        {            
            bool boolTicketsSoldOut = TicketsSoldOut();
            if (!boolTicketsSoldOut)//not sold out
            {
                int Tx_Key = SetupTx();
                Site Sitetemp = new Site();
                string strcurrency = Sitetemp.GetResourceCurrency(Event_Key);
                decimal ServiceFeeAmount = decimal.Parse(hdServiceFee.Value);
                decimal HostAmount = decimal.Parse(hdOverallTotal.Value) - ServiceFeeAmount;
                Boolean isdemovar = Sitetemp.IsDemo(Event_Key);
                string strHostEmail = strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("Trial_Email").ToString();
                string strServiceFeeEmail = "";
                
                if (!isdemovar)
                {
                    strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
                    strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
                }
                else
                {
                    strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString();
                    strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("Host_Email_Trial").ToString();
                }

                DataSet dstemp = Eventomatic_DB.SPs.ViewPaypalEmail(Event_Key).GetDataSet();
                
                if (!isdemovar)
                {
                    //strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
                    if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)
                    {
                        strHostEmail = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
                    }
                }
                
                

                Eventomatic.Addons.PaypalMethods paytemp = new Eventomatic.Addons.PaypalMethods();
                string strNotetemp = "";
                if (Sitetemp.isAlphaNumeric(lblEvent_Name.Text))
                {
                    strNotetemp += lblEvent_Name.Text + " has received payment."; ;
                }
                else
                {
                    strNotetemp += "Your event has received a payment.";
                }
                if (Event_Key == 330)
                {
                    Response.Redirect(ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "MobilePay.aspx?tx=" + Tx_Key);
                }
                paytemp.ParallelPayment(!isdemovar, strcurrency, strNotetemp, HostAmount, strHostEmail, ServiceFeeAmount, strServiceFeeEmail, Tx_Key, Event_Key);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e) //Going back
        {
            RadTabStrip1.SelectedIndex = 0;
            btnBack.Visible = false;
            lblbuyguest.Visible = false;
            RadTabStrip1.Tabs[0].Enabled = true;
            RadTabStrip1.Tabs[1].Enabled = false;
            pnlTerms.Visible = false;
            pnlTickets.Visible = true;
            pnlDetails.Visible = true;
            pnlQuestions.Visible = false;
            lblError.Visible = false;
            btnContinue.Text = "Continue";

            RadAjaxManager1.ResponseScripts.Add("ManipulateGrid(0)");
        }


        protected void btnContinue_Click(object sender, EventArgs e) //Going from questions to Payment
        {
            if (RadTabStrip1.SelectedIndex == 0) //Check tix
            {

                if (ConfirmQuantitySelected()) //selected tix
                {
                    lblError.Visible = false;

                    RadTabStrip1.SelectedIndex = 1;                    
                    RadTabStrip1.Tabs[0].Enabled = false;
                    RadTabStrip1.Tabs[1].Enabled = true;
                    btnBack.Visible = true;
                    pnlTerms.Visible = true;
                    lblbuyguest.Visible = true;

                    pnlTickets.Visible = false;
                    pnlDetails.Visible = false;
                    pnlQuestions.Visible = true;

                    btnContinue.Text = "Checkout";

                    if (hdOverallTotal.Value == "0.00")//it is free tix
                    {
                        pnlemail.Visible = true;
                    }
                    
                }
                else //didn't select tix
                {
                    lblError.Visible = true;
                    lblError.Text = "Please select the Ticket Quantity you wish to purchase.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (RadTabStrip1.SelectedIndex == 1) //go to checkout
            {
                

                bool QuestionsMandatory = true;

                foreach (RadListViewItem rdlsv in RadListView3.Items)
                {
                    Questions_Order_Form questions = (Questions_Order_Form)rdlsv.FindControl("Questions_Order_Form1");
                    TextBox txtList_FirstName = (TextBox)rdlsv.FindControl("txtList_FirstName");
                    TextBox txtList_LastName = (TextBox)rdlsv.FindControl("txtList_LastName");
                    //TextBox txtFreeEmail = (TextBox)rdlsv.FindControl("txtFreeEmail");

                    if (!questions.MandatoryAnswered())
                    {
                        QuestionsMandatory = false;
                    }
                    if ((txtList_FirstName.Text == "") || (txtList_LastName.Text == "") || ((txtFreeEmail.Text == "") && (pnlemail.Visible)))
                    {
                        QuestionsMandatory = false;
                    }
                }
                    
                bool boolTicketsSoldOut = TicketsSoldOut();

                if (!QuestionsMandatory)
                {
                    lblError.Visible = true;
                    lblError.Text = "Please answer all mandatory questions.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                else if (boolTicketsSoldOut)
                {
                    lblError.Visible = true;
                    lblError.Text = "Unfortunately the tickets you selected are now Sold Out.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                else if (!chkTerms.Checked)
                {
                    lblError.Visible = true;
                    lblError.Text = "Please read and accept the Terms of Service.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (hdOverallTotal.Value == "0.00")//it is free tix
                    {
                        hdFreeTicket.Value = "True";                        
                        
                            int Tx_Key = SetupTx();
                            Send_Email SE = new Send_Email();
                            SE.Send_Transaction_Email(Tx_Key, "");
                            Response.Redirect(ConfigurationSettings.AppSettings.Get("Store_URL").ToString()+ "Order_Confirmation.aspx?tx_key=" + Tx_Key);                        
                    }
                    else if ((hdisdemogeneric.Value == "1") && (lblDemo.Visible))
                    {
                        //go to demopay
                        int Tx_Key = SetupTx();
                        Site sitetemp = new Site();
                        int resourcekey = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));
                        Eventomatic_DB.SPs.UpdateFbUsersDemoTicket(Convert.ToInt64(hdnfbid.Value), resourcekey).Execute();
                        Response.Redirect(ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Demopay.aspx?gsdemo=true&tx_key=" + Tx_Key);
                    }
                    else if (hdisdemopay.Value == "1")
                    {
                        //go to demopay
                        int Tx_Key = SetupTx();
                        Site sitetemp = new Site();
                        int resourcekey = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));
                        Eventomatic_DB.SPs.UpdateFbUsersDemoTicket(Convert.ToInt64(hdnfbid.Value), resourcekey).Execute();
                        Response.Redirect(ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Demopay.aspx?tx_key=" + Tx_Key);
                    }
                    else
                    {
                        hdFreeTicket.Value = "False";
                        //go to paypal
                        PayPayPal();
                    }

                    lblError.Visible = false;
                    hdGoToPayment.Value = "1";
                    
                   
                }

            }
        }

        protected bool ConfirmQuantitySelected()
        {
            Site sitetemp = new Site();
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("Questions_Key");
            dttemp.Columns.Add("tix_Name");            

            decimal dcmtemp = 0;

            bool ddlselected = false;

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.DropDownList ddlQuantity = (System.Web.UI.WebControls.DropDownList)gvr.FindControl("ddlQuantity");
                    System.Web.UI.WebControls.TextBox txtDonate = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txtDonate");
                    System.Web.UI.WebControls.Label lblPrice = (System.Web.UI.WebControls.Label)gvr.FindControl("lblPrice");   // look for price in the row
                    Label lbldescription = (Label)gvr.FindControl("lbldescription");                    

                    if (ddlQuantity.Visible)
                    {
                        string pricetemp = sitetemp.RemoveCurrencySymbol(lblPrice.Text);
                        dcmtemp += decimal.Parse(pricetemp) * ddlQuantity.SelectedIndex;

                        for (int i = 1; i <= ddlQuantity.SelectedIndex; i++)
                        {
                            int currentnum = dttemp.Rows.Count + 1;

                            DataRow dr = dttemp.NewRow();
                            dr["Questions_Key"] = "0";
                            dr["tix_Name"] = lbldescription.Text + " #" + currentnum;
                            dttemp.Rows.Add(dr);
                        }                        

                        //check if it's a demo
                        Label lblIsDemo = (Label)gvr.FindControl("lblIsDemo");
                        if (lblIsDemo.Text == "True" && ddlQuantity.SelectedIndex > 0)
                        {
                            hdisdemopay.Value = "1";
                        }

                        if (ddlQuantity.SelectedIndex > 0)
                        {
                            ddlselected = true;
                        }
                    }

                    if (txtDonate.Visible)
                    {
                        dcmtemp += decimal.Parse(txtDonate.Text);

                        if (decimal.Parse(txtDonate.Text) > 0)
                        {
                            int currentnum = dttemp.Rows.Count + 1;
                            DataRow dr = dttemp.NewRow();
                            dr["Questions_Key"] = "0";
                            dr["tix_Name"] = lbldescription.Text + " #" + currentnum;
                            dttemp.Rows.Add(dr);                       
                        }                        
                    }
                }
            }


            if ((dcmtemp > 0) || (ddlselected))
            {
                RadListView3.DataSource = dttemp;
                RadListView3.DataBind();

                return true;
            }
            else
            {
                return false;
            }            
        }
        

        protected void btnSaveDetails_Click(object sender, EventArgs e) //Save Details
        {
            SaveEvent();
        }

        protected void SaveEvent()
        {
            Site Sitetemp = new Site();
            string eid = "0";
            if ((Request.QueryString["eid"] != null) && (Request.QueryString["eid"] != ""))
            {
                eid = Request.QueryString["eid"].ToString();
            }
            int tempticketnum = 0;
            if (Event_Key == 0)
            {
                Random random = new Random();
                tempticketnum = random.Next(1500, 7000);
            }

            DateTime tempStartdate = rdpStartDate.SelectedDate.Value.AddHours(rdpStartTime.SelectedDate.Value.Hour).AddMinutes(rdpStartTime.SelectedDate.Value.Minute);
            DateTime tempEnddate = rdpEndDate.SelectedDate.Value.AddHours(rdpEndTime.SelectedDate.Value.Hour).AddMinutes(rdpEndTime.SelectedDate.Value.Minute);
            int resource_key = Convert.ToInt32(Sitetemp.GetResourceKeyEventKey(Event_Key));

            StoredProcedure sp_UpdateEvent = Eventomatic_DB.SPs.UpdateEvent(Event_Key, txtEventName.Text, txtHost.Text, tempStartdate,
            tempEnddate, txtLocation.Text, txtStreet.Text, txtCity.Text, txtPhone.Text, txtEmailInput.Text, txtComments.Text,
            chkDisplayAvailable.Checked, txtConfirmation.Text, resource_key, eid, 0, Convert.ToInt32(ddlMaxTickets.SelectedValue), Convert.ToDecimal(ddlTimezone.SelectedValue), tempticketnum, false, 0);

            sp_UpdateEvent.Execute();
            if (Event_Key == 0)
            {
                Event_Key = Convert.ToInt32(sp_UpdateEvent.Command.Parameters[15].ParameterValue.ToString());

                //Record Activity
                Eventomatic.Addons.Activities activity = new Activities();
                activity.NewActivity(2, Convert.ToInt32(Master.GetResourceKey()), Convert.ToInt64(Master.getfbid()), 0, Event_Key, 0);
            }
            else
            {
            }


            //update bkimgurl
            //BackgroundImage1.SaveBkImgUrl();            

            try
            {
                //modify existing rss xml file
                Eventomatic.Addons.rss_generate rssgenerate = new Eventomatic.Addons.rss_generate();
                rssgenerate.WriteRss(Convert.ToInt32(Sitetemp.GetResourceKeyEventKey(Event_Key)));
            }
            catch
            {

            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Changed Country Selection
        }

        protected void btnCouponCode_Click(object sender, EventArgs e)
        {
            bool gotone = false;
            foreach (GridViewRow r in GridView1.Rows)
            {
                System.Web.UI.WebControls.Label lblCouponCode = (System.Web.UI.WebControls.Label)r.FindControl("lblCouponCode");
                if (lblCouponCode.Text.Trim() == txtCouponCode.Text)
                {
                    r.Visible = true;
                    pnlCouponCode.Visible = false;
                    gotone = true;
                    hdgvoffset.Value = Convert.ToString(Convert.ToInt32(hdgvoffset.Value) - 1);
                }
            }
            if (gotone)
            {
                lblnomatch.Visible = false;
            }
            else
            {
                lblnomatch.Visible = true;
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (e.Argument == "InitialPageLoad")
            {
                //simulate longer page load                
            }
        }

        protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            Site sitetemp = new Site();
            if (e.Item is RadListViewDataItem)
            {
                
                Label lblticketkey = (Label)e.Item.FindControl("lblticketkey");
                Label lblAction = (Label)e.Item.FindControl("lblAction");
                Label lblPrice = (Label)e.Item.FindControl("lblPrice");

                lblPrice.Text = sitetemp.GetCurrencySymbol(Event_Key) + " " + lblPrice.Text;
                lblAction.Text = "<a href='#' onclick=javascript:openWinTickets(" + lblticketkey.Text + "," + Event_Key.ToString() + ");return false;>Edit</a> | <a href='#' onclick='javascript:doRemoveTicket(" + lblticketkey.Text + ");'>Remove</a>";
            }
        }

        protected void RadListView2_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            Site sitetemp = new Site();
            if (e.Item is RadListViewDataItem)
            {
                Label lblQuestionsMandatory = (Label)e.Item.FindControl("lblQuestionsMandatory");
                Label lblQuestion_Text = (Label)e.Item.FindControl("lblQuestion_Text");
                TextBox txtQuestionsAnswer = (TextBox)e.Item.FindControl("txtQuestionsAnswer");
                DropDownList ddlQuestionsAnswer = (DropDownList)e.Item.FindControl("ddlQuestionsAnswer");
                Label lblQuestion_Type = (Label)e.Item.FindControl("lblQuestion_Type");
                Label lblMandatory = (Label)e.Item.FindControl("lblMandatory");
                Label lblquestionkey = (Label)e.Item.FindControl("lblquestionkey");
                Label lblQuestionAction = (Label)e.Item.FindControl("lblQuestionAction");

                if (lblQuestion_Type.Text == "1")//dropdown
                {
                    ddlQuestionsAnswer.Visible = true;
                    txtQuestionsAnswer.Visible = false;
                    DataSet dstemp2 = Eventomatic_DB.SPs.ViewQuestionDropDown(Convert.ToInt32(lblquestionkey.Text)).GetDataSet();

                    ddlQuestionsAnswer.DataSource = dstemp2.Tables[0];
                    ddlQuestionsAnswer.DataTextField = "Question_DD_Text";
                    ddlQuestionsAnswer.DataValueField = "Question_DD_Value";
                    ddlQuestionsAnswer.DataBind();
                    ddlQuestionsAnswer.Items.Insert(0, "Please Select...");
                }
                if (lblMandatory.Text == "1")
                {
                    lblQuestionsMandatory.Visible = true;
                }

                lblQuestionAction.Text = "<a href='#' onclick=javascript:openWinQuestions(" + lblquestionkey.Text + "," + Event_Key.ToString() + ");return false;>Edit</a> | <a href='#' onclick='javascript:doRemoveQuestion(" + lblquestionkey.Text + ");'>Remove</a>";
            }
        }

        protected void RadListView3_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item is RadListViewDataItem)
            {
                Questions_Order_Form questions = (Questions_Order_Form)e.Item.FindControl("Questions_Order_Form1");
                //Load Questions
                System.Web.UI.WebControls.HiddenField hdnEvent_Key2 = new System.Web.UI.WebControls.HiddenField();
                hdnEvent_Key2 = (System.Web.UI.WebControls.HiddenField)questions.FindControl("Event_Key");
                hdnEvent_Key2.Value = Event_Key.ToString();

                questions.LoadPage();
            }            
        }        

        protected void ReloadTickets()//Reload tickets
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicketAll(Event_Key).GetDataSet();
            RadListView1.DataSource = dstemp.Tables[0];
            RadListView1.DataBind();
        }

        protected void ReloadQuestions()//Reload questions
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewQuestion(Event_Key).GetDataSet();
            RadListView2.DataSource = dstemp.Tables[0];
            RadListView2.DataBind();
        }

        protected void RemoveItem(int Itemtype, int Key)//Remove tickets
        {
            switch (Itemtype)
            {
                case 0:
                    Eventomatic_DB.SPs.DeleteTicket(Key).Execute();
                    break;
                case 1:
                    Eventomatic_DB.SPs.DeleteQuestion(Key).Execute();
                    break;

            }
        }

        protected void AsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {            
            byte[] imageData;
            using (Stream stream = e.File.InputStream)
            {
                imageData = new byte[stream.Length];
                stream.Read(imageData, 0, (int)stream.Length);
                Thumbnail.DataValue = imageData;
            }
                                    

            //save image
            if (!Thumbnail.ImageUrl.Contains("Images/Events"))
            {
                string file = Server.MapPath("/Images/Events/") + "\\" + Event_Key.ToString() + ".jpg";
                //AsyncUpload1.UploadedFiles[0].SaveAs(file);

                string fullurl = Request.Url.AbsoluteUri;
                string saveimgurl = fullurl.Substring(0, fullurl.ToLower().IndexOf("order_form2.aspx")) + Thumbnail.ImageUrl.Replace("~/", "");

                Site sitetemp = new Site();
                //sitetemp.savepicurl2(saveimgurl, file);

                FileStream fs = new FileStream(file, FileMode.Create);
                BinaryWriter w = new BinaryWriter(fs);
                try
                {
                    w.Write(imageData);
                }
                finally
                {
                    fs.Close();
                    w.Close();
                }
            }
            Thumbnail.Width = Unit.Pixel(200);
        }

        protected void AsyncUpload2_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            byte[] imageData;
            using (Stream stream = e.File.InputStream)
            {
                imageData = new byte[stream.Length];
                stream.Read(imageData, 0, (int)stream.Length);
                Thumbnail2.DataValue = imageData;
            }

            Site sitetemp = new Site();                

            //save image
            if (!Thumbnail2.ImageUrl.Contains("Images/Groups"))
            {
                string file = Server.MapPath("/Images/Groups/") + "\\" + sitetemp.GetResourceKeyEventKey(Event_Key) + ".jpg";                

                string fullurl = Request.Url.AbsoluteUri;
                string saveimgurl = fullurl.Substring(0, fullurl.ToLower().IndexOf("order_form2.aspx")) + Thumbnail2.ImageUrl.Replace("~/", "");
                

                FileStream fs = new FileStream(file, FileMode.Create);
                BinaryWriter w = new BinaryWriter(fs);
                try
                {
                    w.Write(imageData);
                }
                finally
                {
                    fs.Close();
                    w.Close();
                }
            }
            Thumbnail2.Height = Unit.Pixel(30);
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string strcurrentpage = System.Web.HttpContext.Current.Request.Url.ToString();
            strcurrentpage = strcurrentpage.Replace("event/", "");
            if (strcurrentpage.Contains("true"))
            {
                Response.Redirect(strcurrentpage.Replace("true", "preview"));
            }
            else
            {
                Response.Redirect(strcurrentpage.Replace("preview", "true"));
            }            
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Site sitetemp = new Site();
            Response.Redirect(sitetemp.GetNavigateurl(fbid) + "default.aspx");
        }

        protected int UpdatedbNewEvent(int resource_key)
        {

            rdpStartDate.SelectedDate = DateTime.Now.AddDays(14).Date;
            rdpStartTime.SelectedDate = DateTime.Today.AddHours(20);
            rdpEndDate.SelectedDate = DateTime.Now.AddDays(15).Date;
            rdpEndTime.SelectedDate = DateTime.Today.AddHours(25);

            Site Sitetemp = new Site();

            DateTime tempStartdate = DateTime.Today.AddDays(14).AddHours(20);
            DateTime tempEnddate = DateTime.Today.AddDays(15).AddHours(25);

            string eid = "0";
            if ((Request.QueryString["eid"] != null) && (Request.QueryString["eid"] != ""))
            {
                eid = Request.QueryString["eid"].ToString();
            }
            Random random = new Random();
            int tempticketnum = random.Next(1500, 7000);
            

            StoredProcedure sp_UpdateEvent = Eventomatic_DB.SPs.UpdateEvent(0, txtEventName.Text, txtHost.Text, tempStartdate,
            tempEnddate, txtLocation.Text, txtStreet.Text, txtCity.Text, txtPhone.Text, txtEmailInput.Text, txtComments.Text,
            chkDisplayAvailable.Checked, txtConfirmation.Text, resource_key, eid, 0, Convert.ToInt32(ddlMaxTickets.SelectedValue), 0, Convert.ToInt32(tempticketnum), false, 0);
            sp_UpdateEvent.Execute();            

            string eventkeynew = sp_UpdateEvent.Command.Parameters[15].ParameterValue.ToString();
            //Eventomatic_DB.SPs.UpdateEventForTicket(Convert.ToInt32(eventkeynew)).Execute();

            Eventomatic.Addons.qrcodes qr = new Eventomatic.Addons.qrcodes();
            qr.GenerateEventqrimg(Convert.ToInt32(eventkeynew));

            hdeventkey.Value = eventkeynew;

            string strtemp = "../Images/BackgroundImages/00.jpg";
            Eventomatic_DB.SPs.UpdateEventBkImgUrl(Convert.ToInt32(eventkeynew), strtemp).Execute();

            return Convert.ToInt32(eventkeynew);
            //string transferurl = "Edit_Event.aspx?Event_Key=" + eventkeynew;
            //Server.Transfer(transferurl);            
        }

        protected void PopulateFacebook(string eid, Int64 fbid)
        {            

            groupstore_event gsevent = new groupstore_event();
            Site Sitetemp = new Site();
            gsevent = Sitetemp.Importfbevent(eid,fbid);            

            //DataSet dstemp = Master.ExecuteQuery("SELECT name,host,start_time,end_time,location,venue,description,pic_big FROM event WHERE eid=" + eid);
            txtEventName.Text = gsevent.Eventname;//dstemp.Tables[1].Rows[0]["name"].ToString();
            txtHost.Text = gsevent.Host; //dstemp.Tables[1].Rows[0]["host"].ToString();
            txtLocation.Text = gsevent.Location; //dstemp.Tables[1].Rows[0]["location"].ToString();


            rdpStartDate.SelectedDate = gsevent.EventBegins.Date; //fbTime.ToLocalTime().Date;
            rdpStartTime.SelectedDate = gsevent.EventBegins; //fbTime.ToLocalTime();
            
            rdpEndDate.SelectedDate = gsevent.EventEnds.Date; //fbTime.ToLocalTime().Date;
            rdpEndTime.SelectedDate = gsevent.EventEnds; //fbTime.ToLocalTime();

            txtComments.Text = gsevent.Additionalcomments; //dstemp.Tables[1].Rows[0]["description"].ToString();
            
            txtStreet.Text = gsevent.Street; //dstemp.Tables[2].Rows[0]["street"].ToString();
            txtCity.Text = gsevent.City; //dstemp.Tables[2].Rows[0]["city"].ToString();

            if (gsevent.Imageurl != "")
            {
                Sitetemp.savepicurlEventKey(gsevent.Imageurl,Event_Key.ToString());
                imgEvent.ImageUrl = "/Images/Events/"+ Event_Key.ToString() + ".jpg";
                Thumbnail.ImageUrl = imgEvent.ImageUrl;
            }
            //save in db new ticket
            groupstore_ticket gsticketemp = (groupstore_ticket)gsevent.Tickets.GetValue(0);
            //Tickets1.Addfbticket(gsticketemp);

            DateTime tempbegin = DateTime.Now;
            DateTime tempfinish = gsevent.EventEnds;
            Eventomatic_DB.SPs.UpdateTicket(0,Event_Key, "General Admission", gsticketemp.Price, 100,
                tempbegin, tempfinish, 0, false).Execute();

            SaveEvent();
            //Eventomatic_DB.SPs.UpdateEventForTicket(Event_Key).Execute();
            Response.Redirect("Order_Form.aspx?Event_Key=" + Event_Key + "&edit=true");
        }
    
    }
}