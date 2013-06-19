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
using System.ComponentModel;
//using facebook.Schema;
using SubSonic;
using System.IO;
using Eventomatic.Addons;
using Telerik.Web.UI;


namespace Eventomatic
{
    public partial class Edit_Event : System.Web.UI.Page
    {
        int Event_Key = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            fbuser fbcurrentuser = Master.getfbuser();
            Tickets1.fbcurrentuser = fbcurrentuser;

            if ((Request.QueryString["Event_Key"] != null) && (Request.QueryString["Event_Key"] != ""))
            {
                Event_Key = Convert.ToInt32(Request.QueryString["Event_Key"].ToString());
            }
            else if (hdeventkey.Value != "0")
            {
                Event_Key = Convert.ToInt32(hdeventkey.Value);
            }

            if (Request.Form["__EVENTTARGET"] == "ReloadTickets")
            {
                ReloadTickets();
            }
            else if (Request.Form["__EVENTTARGET"] == "doRemoveTicket")
            {
                RemoveItem(0,Convert.ToInt32(Request["__EVENTARGUMENT"].ToString()));
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

            if (!IsPostBack)
            {
                //Send New to user control
                HiddenField hdnEvent_Key = new HiddenField();
                hdnEvent_Key = (HiddenField)Upload1.FindControl("Event_Key");
                hdnEvent_Key.Value = "-1";

                HiddenField hdnEvent_Key2 = new HiddenField();
                hdnEvent_Key2 = (HiddenField)Questions1.FindControl("Event_Key");
                hdnEvent_Key2.Value = Event_Key.ToString();

                HiddenField hdnCurrentResource = new HiddenField();
                hdnCurrentResource = (HiddenField)Upload1.FindControl("Current_Resource");
                hdnCurrentResource.Value = Master.GetResourceKey().ToString();

                HiddenField hdnEvent_Key3 = new HiddenField();
                hdnEvent_Key3 = (HiddenField)Tickets1.FindControl("Event_Key");
                hdnEvent_Key3.Value = Event_Key.ToString();

                HiddenField hdnCurrentResource2 = new HiddenField();
                hdnCurrentResource2 = (HiddenField)Tickets1.FindControl("Resource_Key");
                hdnCurrentResource2.Value = Master.GetResourceKey().ToString();

                
                DataTable dttabletemp = Master.getFriendslist(fbcurrentuser);
                //dttabletemp.Rows.Add(fbcurrentuser.UID, fbcurrentuser.Fullname);                
                dttabletemp.Rows.Add(fbcurrentuser.UID,"Lorne Lantz" + " (You)");
                Tickets1.LoadFriendslist(dttabletemp);
                ReloadTickets();
                ReloadQuestions();

                //Check for Temp Image & Remove if exists
                string strImgTemp = "Temp" + Master.GetResourceKey().ToString() + ".jpg";
                Site Sitetemp = new Site();
                if (Sitetemp.ImgExists(0, strImgTemp))
                {
                    string strPath = Server.MapPath("/Images/Events/" + strImgTemp);                                        
                    File.Delete(strPath);
                }

                //Webdatechooser date format
                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN");
                ci.DateTimeFormat.LongDatePattern = "dddd, MMMM d, yyyy";
                ci.DateTimeFormat.ShortDatePattern = "dddd, MMMM d, yyyy";
                StartDate.CalendarLayout.Culture = ci;
                EndDate.CalendarLayout.Culture = ci;
                //Begin_Selling.CalendarLayout.Culture = ci;
                //Selling_Deadline.CalendarLayout.Culture = ci;
                
                //Insert Timezones
                DataSet dstemp = Eventomatic_DB.SPs.ViewInfoTimezones(0).GetDataSet();
                ddlTimezone.DataSource = dstemp.Tables[0];
                ddlTimezone.DataTextField = "Timezones_Text";
                ddlTimezone.DataValueField = "Timezones_Value";
                ddlTimezone.DataBind();

                //if importing a facebook event
                if ((Request.QueryString["eid"] != null) && (Request.QueryString["eid"] != ""))
                {
                    PopulateFacebook(Request.QueryString["eid"]);
                    btnSave.Text = "Save New Event";                    
                }
                    //if editing existing event
                else if ((Request.QueryString["Event_Key"] != null) && (Request.QueryString["Event_Key"] != ""))
                {
                    btnSave.Text = "Save Changes";                    
                    PopulateExistingEvent(Event_Key);
                }
                    //if creating new one from scratch
                else { 
                //set Event date to 2 weeks in the future.
                    StartDate.Value = DateTime.Now.AddDays(14).Date;
                    EndDate.Value = DateTime.Now.AddDays(15).Date;
                    //Begin_Selling.Value = DateTime.Now.Date;
                    //Selling_Deadline.Value = EndDate.Value;
                    btnSave.Text = "Save New Event";                    
                }

                if (Event_Key == 0)
                {
                    UpdatedbNewEvent();
                    Event_Key = Convert.ToInt32(hdeventkey.Value);
                }
                lblAddNew.Attributes.Add("OnClick", "javascript:openWinTickets(0," + Event_Key.ToString() + ");return false;");
                lblAddnewQuestion.Attributes.Add("OnClick", "javascript:openWinQuestions(0," + Event_Key.ToString() + ");return false;");
            }
        }


        protected void PopulateFacebook(string eid)
        {
            groupstore_event gsevent = new groupstore_event();
            Site Sitetemp = new Site();
            gsevent = Sitetemp.Importfbevent(eid,Convert.ToInt64(Master.getfbid()));

            
            //DataSet dstemp = Master.ExecuteQuery("SELECT name,host,start_time,end_time,location,venue,description,pic_big FROM event WHERE eid=" + eid);
            txtEventName.Text = gsevent.Eventname;//dstemp.Tables[1].Rows[0]["name"].ToString();
            txtHost.Text = gsevent.Host; //dstemp.Tables[1].Rows[0]["host"].ToString();
            txtLocation.Text = gsevent.Location; //dstemp.Tables[1].Rows[0]["location"].ToString();

            
            //DateTime fbTime = Master.fbDateTime(Convert.ToInt32(dstemp.Tables[1].Rows[0]["start_time"].ToString()));

            StartDate.Value = gsevent.EventBegins.Date; //fbTime.ToLocalTime().Date;
            StartTime.Value = gsevent.EventBegins; //fbTime.ToLocalTime();

            //fbTime = Master.fbDateTime(Convert.ToInt32(dstemp.Tables[1].Rows[0]["end_time"].ToString()));
            EndDate.Value = gsevent.EventEnds.Date; //fbTime.ToLocalTime().Date;
            EndTime.Value = gsevent.EventEnds; //fbTime.ToLocalTime();

            txtComments.Text = gsevent.Additionalcomments; //dstemp.Tables[1].Rows[0]["description"].ToString();
            //Begin_Selling.Value = DateTime.Now.Date;
            //Begin_Selling_Time.Value = DateTime.Now.Date;
            //Selling_Deadline.Value = EndDate.Value;
            //Selling_Deadline_Time.Value = EndTime.Value;          

            txtStreet.Text = gsevent.Street; //dstemp.Tables[2].Rows[0]["street"].ToString();
            txtCity.Text = gsevent.City; //dstemp.Tables[2].Rows[0]["city"].ToString();

            //if (dstemp.Tables[1].Rows[0]["pic_big"] != DBNull.Value)
            if (gsevent.Imageurl != "")
            {
                //Master.savepicurl(dstemp.Tables[1].Rows[0]["pic_big"].ToString(),Master.GetResourceKey().ToString());
                Master.savepicurl(gsevent.Imageurl,Master.GetResourceKey().ToString());
                //UserControl uploadtemp = new Eventomatic.Addons.Upload();


                Upload1.LoadfbEvent();
            }
            //save in db new ticket
            groupstore_ticket gsticketemp = (groupstore_ticket)gsevent.Tickets.GetValue(0);
            Tickets1.Addfbticket(gsticketemp);

            /*string strvalue = "";
            string strcolumn = "";
            foreach (DataTable dt in dstemp.Tables)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    strcolumn = dc.Caption;
                    strvalue = dt.Rows[0][dc.Caption].ToString();
                }
            }*/
        }

        protected void PopulateExistingEvent(int Event_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewEventDetails(Event_Key).GetDataSet();

            txtEventName.Text = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
            txtHost.Text = dstemp.Tables[0].Rows[0]["Host"].ToString();
            txtLocation.Text = dstemp.Tables[0].Rows[0]["Location"].ToString();
            txtStreet.Text = dstemp.Tables[0].Rows[0]["Street"].ToString();
            txtCity.Text = dstemp.Tables[0].Rows[0]["City"].ToString();
            txtPhone.Text = dstemp.Tables[0].Rows[0]["Phone"].ToString();
            txtEmail.Text = dstemp.Tables[0].Rows[0]["Email"].ToString();
            txtComments.Text = dstemp.Tables[0].Rows[0]["Additional_Comments"].ToString();
            txtConfirmation.Text = dstemp.Tables[0].Rows[0]["Confirmation"].ToString();

            if (dstemp.Tables[0].Rows[0]["Timezone"] != DBNull.Value)
            {
                int Timezone = Convert.ToInt32(dstemp.Tables[0].Rows[0]["Timezone"]);
                if (ddlTimezone.Items.FindByValue(Timezone.ToString()) != null)
                {
                    ddlTimezone.SelectedValue = Timezone.ToString();
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

            DateTime tempdatetime;

            if (dstemp.Tables[0].Rows[0]["Event_Begins"] != DBNull.Value)
            {
                tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Event_Begins"].ToString());
                StartDate.Value = tempdatetime.Date;
                StartTime.Value = tempdatetime;
            }
            
            if (dstemp.Tables[0].Rows[0]["Event_Ends"] != DBNull.Value)
            {
                tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Event_Ends"].ToString());
                EndDate.Value = tempdatetime.Date;
                EndTime.Value = tempdatetime;
            }
            

            /*if (dstemp.Tables[0].Rows[0]["Begin_Selling"] != DBNull.Value)
            {
                tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Begin_Selling"].ToString());
                Begin_Selling.Value = tempdatetime.Date;
                Begin_Selling_Time.Value = tempdatetime;
            }

            if (dstemp.Tables[0].Rows[0]["Selling_Deadline"] != DBNull.Value)
            {
                tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Selling_Deadline"].ToString());
                Selling_Deadline.Value = tempdatetime.Date;
                Selling_Deadline_Time.Value = tempdatetime;
            }*/

            if (dstemp.Tables[0].Rows[0]["Display_Tickets_Available"] != DBNull.Value)
            {
                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Display_Tickets_Available"]) == true)
                {
                    chkDisplayAvailable.Checked = true;
                }

            }

            if (dstemp.Tables[0].Rows[0]["Donation"] != DBNull.Value)
            {
                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Donation"]) == true)
                {
                    chkfundraiser.Checked = true;
                }

            }

            /*UltraWebGrid1.DataSource = Eventomatic_DB.SPs.ViewTicket(Event_Key).GetDataSet().Tables[0];
            UltraWebGrid1.DataBind();*/

            //UltraWebGrid2.DataSource = Eventomatic_DB.SPs.ViewQuestion(Event_Key).GetDataSet().Tables[0];
            //UltraWebGrid2.DataBind();
            //Load Questions
            Questions1.LoadPage(true);
            Tickets1.LoadPage();
            

            //Send Event_Key to user control
            HiddenField hdnEvent_Key = new HiddenField();
            hdnEvent_Key = (HiddenField)Upload1.FindControl("Event_Key");
            hdnEvent_Key.Value = Event_Key.ToString();

            HiddenField hdnEvent_Key2 = new HiddenField();
            hdnEvent_Key2 = (HiddenField)Questions1.FindControl("Event_Key");
            hdnEvent_Key2.Value = Event_Key.ToString();

            HiddenField hdnEvent_Key3 = new HiddenField();
            hdnEvent_Key3 = (HiddenField)BackgroundImage1.FindControl("Event_Key");
            hdnEvent_Key3.Value = Event_Key.ToString();
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            fbuser fbuser = Master.getfbuser();// Master.API.users.getInfo();

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
            
            DateTime tempStartdate = Convert.ToDateTime(StartDate.Value).AddHours(Convert.ToDateTime(StartTime.Value).Hour).AddMinutes(Convert.ToDateTime(StartTime.Value).Minute);
            DateTime tempEnddate = Convert.ToDateTime(EndDate.Value).AddHours(Convert.ToDateTime(EndTime.Value).Hour).AddMinutes(Convert.ToDateTime(EndTime.Value).Minute);
            /*DateTime tempStartdateSelling = Convert.ToDateTime(Begin_Selling.Value).AddHours(Convert.ToDateTime(Begin_Selling_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Begin_Selling_Time.Value).Minute);
            DateTime tempEnddateSelling = Convert.ToDateTime(Selling_Deadline.Value).AddHours(Convert.ToDateTime(Selling_Deadline_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Selling_Deadline_Time.Value).Minute);*/
            //tempStartdate.AddHours(3);//Convert.ToDouble(Convert.ToDateTime(StartTime.Value).Hour.ToString()));

            StoredProcedure sp_UpdateEvent = Eventomatic_DB.SPs.UpdateEvent(Event_Key, txtEventName.Text, txtHost.Text, tempStartdate,
            tempEnddate, txtLocation.Text, txtStreet.Text, txtCity.Text, txtPhone.Text, txtEmail.Text, txtComments.Text,
            chkDisplayAvailable.Checked, txtConfirmation.Text, Convert.ToInt32(Master.GetResourceKey()), eid, 0, Convert.ToInt32(ddlMaxTickets.SelectedValue), Convert.ToDecimal(ddlTimezone.SelectedValue), tempticketnum, chkfundraiser.Checked,0);
                        
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
                /*  Not going to recording editing events right now
                //Record Activity
                Eventomatic.Addons.Activities activity = new Activities();
                activity.NewActivity(3, Convert.ToInt32(Master.GetResourceKey()), Convert.ToInt64(Master.getfbid()), 0, Event_Key, 0);
                 */
            }

            //update Tickets
            //Master.UpdateUltraWebGrid(UltraWebGrid1, 0, Event_Key);

            //update questions
            //Master.UpdateUltraWebGrid(UltraWebGrid2, 1, Event_Key);
            /*HiddenField hdnEvent_Key4 = new HiddenField();
            hdnEvent_Key4 = (HiddenField)Questions1.FindControl("Event_Key");
            hdnEvent_Key4.Value = Event_Key.ToString();

            Questions1.SaveQuestions();

            HiddenField hdnEvent_Key5 = new HiddenField();
            hdnEvent_Key5 = (HiddenField)Questions1.FindControl("Event_Key");
            hdnEvent_Key5.Value = Event_Key.ToString();

            Tickets1.SaveTickets(Event_Key);*/

            //update bkimgurl
            BackgroundImage1.SaveBkImgUrl();

            string strImgTemp = "Temp"+Master.GetResourceKey().ToString()+".jpg";
            Site Sitetemp = new Site();
            if (Sitetemp.ImgExists(0,strImgTemp)){
                string strPath = Server.MapPath("/Images/Events/" + strImgTemp);
                string strNewPath = strPath.Replace(strImgTemp, Event_Key.ToString()+".jpg");
                //string strNewPath2 = strNewPath.Replace(Master.GetResourceKey().ToString(),Event_Key.ToString());
                File.Move(strPath,strNewPath);
            }
            
            try
            {
                //modify existing rss xml file
                Eventomatic.Addons.rss_generate rssgenerate = new Eventomatic.Addons.rss_generate();
                rssgenerate.WriteRss(Convert.ToInt32(Master.GetResourceKey()));
            }
            catch
            {
                
            }
            
            

            //Server.Transfer(System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString());
            //Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Default.aspx");

            //Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('Your Changes have been Saved');location.href = 'Default.aspx';</script>");
            RadAjaxPanel1.ResponseScripts.Add(string.Format("PromptSaved()",""));
            
        }

        public Hashtable getEventinfo()
        {
            string eid = "0";
            if ((Request.QueryString["eid"] != null) && (Request.QueryString["eid"] != ""))
            {
                eid = Request.QueryString["eid"].ToString();
            }

            DateTime tempStartdate = Convert.ToDateTime(StartDate.Value).AddHours(Convert.ToDateTime(StartTime.Value).Hour).AddMinutes(Convert.ToDateTime(StartTime.Value).Minute);
            DateTime tempEnddate = Convert.ToDateTime(EndDate.Value).AddHours(Convert.ToDateTime(EndTime.Value).Hour).AddMinutes(Convert.ToDateTime(EndTime.Value).Minute);
            /*DateTime tempStartdateSelling = Convert.ToDateTime(Begin_Selling.Value).AddHours(Convert.ToDateTime(Begin_Selling_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Begin_Selling_Time.Value).Minute);
            DateTime tempEnddateSelling = Convert.ToDateTime(Selling_Deadline.Value).AddHours(Convert.ToDateTime(Selling_Deadline_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Selling_Deadline_Time.Value).Minute);*/
            
            Random random = new Random();
            int tempticketnum = random.Next(1500, 7000);

            Hashtable hstemp = new Hashtable();
            hstemp.Add("txtEventName", txtEventName.Text);
            hstemp.Add("txtHost", txtHost.Text);
            hstemp.Add("tempStartdate", tempStartdate);
            hstemp.Add("tempEnddate",tempEnddate);
            hstemp.Add("txtLocation",txtLocation.Text);
            hstemp.Add("txtStreet",txtStreet.Text);
            hstemp.Add("txtCity",txtCity.Text);
            hstemp.Add("txtPhone",txtPhone.Text);
            hstemp.Add("txtEmail",txtEmail.Text);
            hstemp.Add("txtComments",txtComments.Text);
            hstemp.Add("chkDisplayAvailable",chkDisplayAvailable.Checked);
            hstemp.Add("txtConfirmation",txtConfirmation.Text);
            hstemp.Add("Resource_Key",Convert.ToInt32(Master.GetResourceKey()));
            hstemp.Add("eid",eid);
            hstemp.Add("ddlMaxTickets",Convert.ToInt32(ddlMaxTickets.SelectedValue));
            hstemp.Add("ddlTimezone",Convert.ToDecimal(ddlTimezone.SelectedValue));
            hstemp.Add("tempticketnum", tempticketnum);
            hstemp.Add("chkfundraiser",chkfundraiser.Checked);            
            return hstemp;

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

        protected void UpdatedbNewEvent()
        {
            DateTime tempStartdate = Convert.ToDateTime(StartDate.Value).AddHours(Convert.ToDateTime(StartTime.Value).Hour).AddMinutes(Convert.ToDateTime(StartTime.Value).Minute);
            DateTime tempEnddate = Convert.ToDateTime(EndDate.Value).AddHours(Convert.ToDateTime(EndTime.Value).Hour).AddMinutes(Convert.ToDateTime(EndTime.Value).Minute);

            string eid = "0";
            if ((Request.QueryString["eid"] != null) && (Request.QueryString["eid"] != ""))
            {
                eid = Request.QueryString["eid"].ToString();
            }
            Random random = new Random();
            int tempticketnum = random.Next(1500, 7000);

            int resource_key = Convert.ToInt32(Master.GetResourceKey());

            StoredProcedure sp_UpdateEvent = Eventomatic_DB.SPs.UpdateEvent(0, txtEventName.Text, txtHost.Text, tempStartdate,
            tempEnddate, txtLocation.Text, txtStreet.Text, txtCity.Text, txtPhone.Text, txtEmail.Text, txtComments.Text,
            chkDisplayAvailable.Checked, txtConfirmation.Text, resource_key, eid, 0, Convert.ToInt32(ddlMaxTickets.SelectedValue), Convert.ToDecimal(ddlTimezone.SelectedValue), Convert.ToInt32(tempticketnum), false, 0);
            sp_UpdateEvent.Execute();

            string eventkeynew = sp_UpdateEvent.Command.Parameters[15].ParameterValue.ToString();            
            Eventomatic_DB.SPs.UpdateEventForTicket(Convert.ToInt32(eventkeynew)).Execute();

            hdeventkey.Value = eventkeynew;
            //string transferurl = "Edit_Event.aspx?Event_Key=" + eventkeynew;
            //Server.Transfer(transferurl);            
        }
    }
}
