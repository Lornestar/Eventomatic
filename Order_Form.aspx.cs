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
using System.Net;
using System.IO;
using Anthem;
using SubSonic;
using Newtonsoft.Json.Linq;

namespace Eventomatic
{
    public partial class Order_Form : System.Web.UI.Page
    {
        int Event_Key = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if ((Request.QueryString["Event_Key"] != null) && (Request.QueryString["Event_Key"] != ""))
            {
                string strtempeventkey = Request.QueryString["Event_Key"].ToString().Replace(".", "");
                Event_Key = Convert.ToInt32(strtempeventkey);
                hdCurrentDate.Value = DateTime.Now.ToString("MMM dd, yyyy HH:mm:ss");                
                if (!IsPostBack)
                {
                    //Update page views
                    Eventomatic_DB.SPs.UpdateEventViews(Event_Key).Execute();

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
                    PopulateExistingEvent(Event_Key);
                    Site sitetemp = new Site();
                    if (sitetemp.IsDemo(Event_Key))
                    {
                        lblDemo.Visible = false;
                    }
                    else
                    {
                        hdTrial.Value = "True";
                        lblDemo.Text = "Trial Version";
                        lblDemo.BackColor = System.Drawing.Color.LightBlue;
                    }
                }
            }
            else { 
                //if accidentally came to page
            }
            
            
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
            DataSet dstemp = Eventomatic_DB.SPs.ViewEventDetails(Event_Key).GetDataSet();

            lblEvent_Name.Text = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
            Page.Title = lblEvent_Name.Text + " - Groupstore.com";
            lblHost.Text = dstemp.Tables[0].Rows[0]["Host"].ToString();
            lblLocation.Text = dstemp.Tables[0].Rows[0]["Location"].ToString();
            lblStreet.Text = dstemp.Tables[0].Rows[0]["Street"].ToString();
            lblCity.Text = dstemp.Tables[0].Rows[0]["City"].ToString();
            lblPhone.Text = dstemp.Tables[0].Rows[0]["Phone"].ToString();
            string streid = dstemp.Tables[0].Rows[0]["eid"].ToString();
            if (dstemp.Tables[0].Rows[0]["Donation"] != DBNull.Value)
            {
                if (dstemp.Tables[0].Rows[0]["Donation"].ToString() == "True")
                {
                    lblGuestlist.Text = "Donator";
                    lblGuestListComment.Text = "Who is making the donation?";
                }
            }
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
                string strComments = dstemp.Tables[0].Rows[0]["Additional_Comments"].ToString();
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
                imgEvent2.ImageUrl = imgEvent.ImageUrl;
                imgEvent3.ImageUrl = imgEvent.ImageUrl;
                if (dstemp.Tables[0].Rows[0]["Ticket_Max"] != DBNull.Value)
                {
                    hdTicketMax.Value = dstemp.Tables[0].Rows[0]["Ticket_Max"].ToString();
                }

                DateTime tempdatetime;

                tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Event_Begins"].ToString());
                lblStart.Text = tempdatetime.ToString("dddd, MMMM d yyyy") + " at " + tempdatetime.ToString("h:mm tt ");

                tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Event_Ends"].ToString());
                lblEnd.Text = tempdatetime.ToString("dddd, MMMM d yyyy") + " at " + tempdatetime.ToString("h:mm tt ");

                //UltraWebGrid1.DataSource = Eventomatic_DB.SPs.ViewTicket(Event_Key).GetDataSet().Tables[0];
                //UltraWebGrid1.DataBind();
                //UltraWebGrid1.Visible = false;

                //check if event is still selling tickets
                /*DateTime Begin_Selling = DateTime.MinValue;
                DateTime Selling_Deadline = DateTime.MaxValue;
                if (dstemp.Tables[0].Rows[0]["Begin_Selling"] != DBNull.Value)
                {
                    Begin_Selling = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Begin_Selling"].ToString());
                }

                if (dstemp.Tables[0].Rows[0]["Selling_Deadline"] != DBNull.Value)
                {
                    Selling_Deadline = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Selling_Deadline"].ToString());
                }*/


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
                    DataRow drtemp = dstemp.Tables[0].NewRow();
                    //drtemp["Quantity"] = "Service Fee";
                    //drtemp["Total"] = "<span id='lblServiceFee' style='display:inline-block;width:100px;'>$ 0.00</span>";
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

                    GridView3.DataSource = dstemp.Tables[0];
                    GridView3.DataBind();
                    
                    //Load Questions
                    System.Web.UI.WebControls.HiddenField hdnEvent_Key2 = new System.Web.UI.WebControls.HiddenField();
                    hdnEvent_Key2 = (System.Web.UI.WebControls.HiddenField)Questions_Order_Form1.FindControl("Event_Key");
                    hdnEvent_Key2.Value = Event_Key.ToString();

                    Questions_Order_Form1.LoadPage();

                    //Loads Sellers & Comments
                    string appid = System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString();
                    string xid = appid + "_" + Event_Key.ToString();
                    char chr34 = Convert.ToChar(34);
                    lblfbcomments.Text = "<fb:comments xid=" + chr34 + xid + chr34 + " canpost=" + chr34 + "true" + chr34 + " candelete=" + chr34 + "false" + chr34 + " width=" + chr34 + "200px" + chr34 + "></fb:comments>";                    
                    //lblfbcomments.Text = "<fb:comments xid='titans_comments' canpost='true' candelete='false' returnurl='http://apps.facebook.com/myapp/titans/'><fb:title>Talk about the Titans</fb:title></fb:comments>";
                    lblfblike.Text = "<fb:like href=" + chr34 + System.Configuration.ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Form.aspx?Event_Key=" + Event_Key.ToString() + chr34 + " layout=" + chr34 + "button_count" + chr34 + " show_faces=" + chr34 + "false" + chr34 + " font=" + chr34 + "arial" + chr34 + "></fb:like>";
                    
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

                            pnlAttending.Visible = true;
                        }                        
                    }
                    
                    
                    
                    DataSet dstemp2 = Eventomatic_DB.SPs.ViewQuestion(Event_Key).GetDataSet();
                    if (hdFreeTicket.Value == "True")
                    {
                        //pnlChoosePayment.Visible = false;
                        //pnlFreeEventEmail.Visible = true;
                        //UltraWebTab1.Tabs[2].Text = "Confirmation";
                        /*
                        int CurrentQKey = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings.Get("Free_Q1").ToString());
                        DataSet dstempFreeQs = Eventomatic_DB.SPs.ViewQuestionFreeTickets(CurrentQKey).GetDataSet();

                        DataRow rtemp = dstemp2.Tables[0].NewRow();
                        rtemp["Question_Text"] = dstempFreeQs.Tables[0].Rows[0]["Question_Text"].ToString();
                        rtemp["Mandatory"] = "True";
                        rtemp["Question_Key"] = CurrentQKey.ToString();
                        dstemp2.Tables[0].Rows.Add(rtemp);

                        CurrentQKey = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings.Get("Free_Q2").ToString());
                        dstempFreeQs = Eventomatic_DB.SPs.ViewQuestionFreeTickets(CurrentQKey).GetDataSet();

                        rtemp = dstemp2.Tables[0].NewRow();
                        rtemp["Question_Text"] = dstempFreeQs.Tables[0].Rows[0]["Question_Text"].ToString();
                        rtemp["Mandatory"] = "True";
                        rtemp["Question_Key"] = CurrentQKey.ToString();
                        dstemp2.Tables[0].Rows.Add(rtemp);

                        CurrentQKey = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings.Get("Free_Q3").ToString());
                        dstempFreeQs = Eventomatic_DB.SPs.ViewQuestionFreeTickets(CurrentQKey).GetDataSet();

                        rtemp = dstemp2.Tables[0].NewRow();
                        rtemp["Question_Text"] = dstempFreeQs.Tables[0].Rows[0]["Question_Text"].ToString();
                        rtemp["Mandatory"] = "True";
                        rtemp["Question_Key"] = CurrentQKey.ToString();
                        dstemp2.Tables[0].Rows.Add(rtemp);*/
                    }
                    /*else if (!Sitetemp.HasPaypalEmail(Event_Key))
                    {
                        //Cannot sell because no Paypal Email
                        lblSellingDeadline.Visible = true;
                        lblSellingDeadline.Text = "Tickets cannot be sold until the Event Host enters a Paypal email address to accept payments.";
                        lblSellingDeadline.ForeColor = System.Drawing.Color.Red;
                        chkTerms.Enabled = false;
                        pnlButtons.Visible = false;
                        GridView1.Visible = false;
                    }*/

                    GridView2.DataSource = dstemp2.Tables[0];
                    GridView2.DataBind();

                    

                    //lblSellingDeadline.Visible = true;
                    //lblSellingDeadline.Text = "Tickets for this event are available for purchase until " + Selling_Deadline.ToString("dddd, MMMM d yyyy") + ".";
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
                    //btnPurchase.Enabled = false;
                    //txtEmail.Enabled = false;
                    chkTerms.Enabled = false;
                    pnlButtons.Visible = false;
                }
                /*else if (Selling_Deadline < DateTime.Now)
                {
                    //Selling deadline has past
                    lblSellingDeadline.Visible = true;
                    lblSellingDeadline.Text = "Tickets for this event are no longer on sale as of " + Selling_Deadline.ToString("dddd, MMMM d yyyy") + ".";
                    //btnPurchase.Enabled = false;
                    //txtEmail.Enabled = false;
                    chkTerms.Enabled = false;
                }*/


                //UltraWebGrid2.DataSource = Eventomatic_DB.SPs.ViewQuestion(Event_Key).GetDataSet().Tables[0];
                //UltraWebGrid2.DataBind();
                
                //Check Background Image
                dstemp = Eventomatic_DB.SPs.ViewEventBkImgUrl(Event_Key).GetDataSet();
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

        string tempstring = sender.ToString();
        tempstring = e.ToString();
        Site sitetemp = new Site();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            //dropDownList.AutoPostBack = true;     // added here for clarity
            //dropDownList.SelectedIndexChanged += new EventHandler(ddlQuantity_SelectedIndexChanged);   // declare event
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
            //check if it is soldout or not
            if (dstemp.Tables[0].Rows.Count-1 >= e.Row.RowIndex)
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

                if (dstemp.Tables[0].Rows[e.Row.RowIndex]["Tickets_Left"] != DBNull.Value){
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
                        dropDownList.Items.RemoveAt(dropDownList.Items.Count-1);
                    }
                }
                                                

                //check if donation box
                if (dstemp.Tables[0].Rows[e.Row.RowIndex]["isdonation"] != DBNull.Value)
                {
                    string strtemp = dstemp.Tables[0].Rows[e.Row.RowIndex]["isdonation"].ToString();
                    if (dstemp.Tables[0].Rows[e.Row.RowIndex]["isdonation"].ToString().ToLower() == "true")
                    {
                        dropDownList.Visible = false;
                        lblPrice.Visible = false;
                        System.Web.UI.WebControls.Label lblDollarsign = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDollarSign");   // look for price in the row                
                        System.Web.UI.WebControls.TextBox txtDonate = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtDonate");
                        System.Web.UI.WebControls.Label lblDonate = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDonate");   // look for price in the row                
                        txtDonate.Visible = true;
                        lblDonate.Visible = true;
                        lblDollarsign.Visible = true;
                        //txtDonate.Attributes.Add("onKeyUp", "ManipulateGrid(" + Gridview1_index_Global.ToString() + "); return false;");
                    }
                }
            }
            
            //check if row is a coupon code
            System.Web.UI.WebControls.Label lblCouponCode = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblCouponCode");
            if (lblCouponCode.Text != "")
            {
                pnlCouponCode.Visible = true;
                e.Row.Visible = false;
                hdgvoffset.Value  = Convert.ToString(Convert.ToInt32(hdgvoffset.Value) + 1);
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
        /*GridViewRow gridViewRow = (GridViewRow)(((Control)sender).NamingContainer);    // get reference to the row

        DropDownList ddlQuantity2 = (DropDownList)gridViewRow.FindControl("ddlQuantity");
        // you can now access other controls within this row something like ...
        Label lblPrice2 = (Label)gridViewRow.FindControl("lblPrice");
        Label lblTotal2 = (Label)gridViewRow.FindControl("lblTotal");
        decimal totalval = Convert.ToInt32(ddlQuantity2.Text) * Convert.ToDecimal(lblPrice2.Text.Replace("$", ""));
        lblTotal2.Text = "$ " + Math.Round(totalval,2).ToString();

        decimal OverallTotal=0;
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            lblTotal2 = (Label)gvrow.FindControl("lblTotal");
            OverallTotal += Convert.ToDecimal(lblTotal2.Text.Replace("$", ""));
        }
        
        //Label lblTotalOverall = (Label)GridView1.Columns[3].
        //lblTotalOverall.Text = "$ " + OverallTotal.ToString();
        //GridView1.Columns[1].FooterText = "$ " + OverallTotal.ToString();
         */
    }

     protected void GotoPaypal()
     {
         decimal OverallTotal = 0;
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
                         Tickets_Purchased.Add(Eventomatic_DB.SPs.ViewTicketSpecific(gvrow.Cells[0].Text,Event_Key,TotalSum).GetDataSet().Tables[0].Rows[0]["Ticket_Key"].ToString(), ddlQuantity.SelectedIndex);

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

             OverallTotal += decimal.Round(ServiceFee,2);//(OverallTotal * SFP) + SFC;
             
             
             //Get IP Address Request.UserHostAddress
             //update Transaction & get tx_Key
             StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), "CAD", 0, "", "", "", "", 1, "", "", 0, "", "", "", "", "", "", "", txtFirstName.Text, txtLastName.Text, ServiceFee, HttpContext.Current.Request.UserHostAddress);
             sp_UpdateTransaction.Execute();
             string tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
             
             //update Tickets Purchased
             foreach (DictionaryEntry de in Tickets_Purchased)
             {
                 //Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), Convert.ToInt32(de.Value),0).Execute();
             }

             //update Questions Answered
             //Questions_Order_Form1.SaveQuestionsAnswered(tempTx_Key);

             System.Web.UI.WebControls.HiddenField hdtemp = new System.Web.UI.WebControls.HiddenField();             
             DataSet dstemp = Eventomatic_DB.SPs.ViewPaypalEmail(Event_Key).GetDataSet();
             Boolean isdemovar =  sitetemp.IsDemo(Event_Key);

             if (isdemovar)
             {
                 //Live
                 hdtemp.Value = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
                 
             }
             else {
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
             else {
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

                       * WebPostRequest myPost = new WebPostRequest("http://www.nicecleanexample.com/PublicTest/autosubmit.php?cmd=login");
                      myPost.Add("txtName", "test");
                      myPost.Add("txtPassword", "test2");
                      string Response = myPost.GetResponse();
                       */
         }
             //Free Event
         else if ((Tickets_Purchased.Count > 0) && (OverallTotal == 0))
         {             
             string strFirstName = "";
             string strLastName= "";
             string strEmail = "";
             System.Web.UI.WebControls.Label Question_Key;
             System.Web.UI.WebControls.Label Question_Text;
             System.Web.UI.WebControls.TextBox QuestionsAnsweredTextbox;
             //Store first name, last name & email to database
             foreach (GridViewRow gvrow in GridView2.Rows)
             {

                 if (gvrow.RowType == DataControlRowType.DataRow)
                 {
                     Question_Key = (System.Web.UI.WebControls.Label)gvrow.FindControl("Question_Key");
                     if ((Question_Key.Text == strQ1) || (Question_Key.Text == strQ2) || (Question_Key.Text == strQ3))
                     {
                         Question_Text = (System.Web.UI.WebControls.Label)gvrow.FindControl("Question_Text");
                         QuestionsAnsweredTextbox = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("txtAnswer");
                         if (Question_Key.Text == strQ1)
                            {strFirstName = QuestionsAnsweredTextbox.Text;}
                         else if (Question_Key.Text == strQ2)
                         {strLastName = QuestionsAnsweredTextbox.Text;}
                         else if (Question_Key.Text == strQ3)
                         {strEmail = QuestionsAnsweredTextbox.Text;}
                     }
                 }
             }

             //update Transaction & get tx_Key
             StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), "CAD", 0, strFirstName, strLastName, strEmail, "", 3, "", "", 0, "", "", "", strEmail, "", "", "", txtFirstName.Text, txtLastName.Text, 0, HttpContext.Current.Request.UserHostAddress);
             sp_UpdateTransaction.Execute();
             string tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();

             //update Tickets Purchased
             foreach (DictionaryEntry de in Tickets_Purchased)
             {
                 //Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), Convert.ToInt32(de.Value),0).Execute();
             }

             //update Questions Answered
             
             foreach (GridViewRow gvrow in GridView2.Rows)
             {

                 if (gvrow.RowType == DataControlRowType.DataRow)
                 {                     
                     Question_Key = (System.Web.UI.WebControls.Label)gvrow.FindControl("Question_Key");
                     QuestionsAnsweredTextbox = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("txtAnswer");
                     if ((Question_Key.Text != strQ1) && (Question_Key.Text != strQ2) && (Question_Key.Text != strQ3))
                     {
                        // Eventomatic_DB.SPs.UpdateQuestionsAnswered(Convert.ToInt32(tempTx_Key), Convert.ToInt32(Question_Key.Text), Event_Key, QuestionsAnsweredTextbox.Text).Execute();
                     }                     
                 }

             }
             Send_Email SE = new Send_Email();
             SE.Send_Transaction_Email(Convert.ToInt32(tempTx_Key),"");
             Response.Redirect("Order_Confirmation.aspx?Event_Key=" + Event_Key);
         }
         
     }

     

     protected void btnTest_Click(object sender, EventArgs e)
     {

         //DataSet dstemp = new DataSet();
         //int s = Convert.ToInt32(dstemp.Tables[0].Rows[0]["test"]);
         //string s = System.IO.File.ReadAllText(Server.MapPath("/Emails/Ticket_Reciept.txt"));
         //Send_Email SE = new Send_Email();
         //SE.Send_Email_Function("Lornestore Noone@Test.com", "Lorne@Vencorps.com", "Ticket Purchase Confirmation", s);
         lblTest.Text = DateTime.Now.ToString();
     }

     protected void btnPurchase_Click(object sender, EventArgs e)
     {
         
     }

     protected void btnFree_Click(object sender, EventArgs e)
     {         
         Site sitetemp = new Site();
         if (sitetemp.isEmail(txtFreeEmail.Text))
         {
             //Free Event Confirmation
             bool boolTicketsSoldOut = TicketsSoldOut();
             if (!boolTicketsSoldOut)//not sold out
             {
                 int Tx_Key = SetupTx();
                 Send_Email SE = new Send_Email();
                 SE.Send_Transaction_Email(Tx_Key,"");
                 Response.Redirect("Order_Confirmation.aspx?tx_key=" + Tx_Key);
             }
         }
         else
         {
             lblError.Visible = true;
             lblError.Text = "Please enter a valid email address.";
             lblError.ForeColor = System.Drawing.Color.Red;
         }
     }

     protected void btnCC_Click(object sender, EventArgs e)
     {
         bool boolTicketsSoldOut = TicketsSoldOut();
         if (!boolTicketsSoldOut)//not sold out
         {
             int Tx_Key = SetupTx();
             if (hdspecificuser.Value == "True")
             {
                 Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("PayCC_URL").ToString() + "?tx_key=" + Tx_Key + "&fbid=" + Request.QueryString["fbid"].ToString());
             }
             else
             {
                 Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("PayCC_URL").ToString() + "?tx_key=" + Tx_Key);
             }             
         }
     }

     protected void btnPaypal_Click(object sender, EventArgs e)
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

             string PayMethod = "0";
             DataSet dsresourceinfo = Eventomatic_DB.SPs.ViewResourceFromTxKey(Tx_Key).GetDataSet();
             if (dsresourceinfo.Tables[0].Rows[0]["Pay_Method"] != DBNull.Value)
             {
                 PayMethod = dsresourceinfo.Tables[0].Rows[0]["Pay_Method"].ToString();
             }
             //0 or null mean end of event / 1 means each transaction

             if (isdemovar)
             {
                 strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
             }
             else
             {
                 strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString();
             }
             
             DataSet dstemp = Eventomatic_DB.SPs.ViewPaypalEmail(Event_Key).GetDataSet();
             if ((dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value) && (PayMethod == "1"))
             {
                 strHostEmail = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
             }
             else
             {
                 if (isdemovar)
                 {
                     strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
                 }
                 else
                 {
                     strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString();
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
             paytemp.ParallelPayment(isdemovar, strcurrency, strNotetemp, HostAmount, strHostEmail, ServiceFeeAmount, strServiceFeeEmail, Tx_Key,Event_Key);
         }         
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
         foreach (GridViewRow gvrow in GridView1.Rows)
         {
             if ((gvrow.RowIndex != GridView1.Rows.Count - 1) && (gvrow.Visible))
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
         string tempTx_Key="0";
         decimal ServiceFee = Convert.ToDecimal(hdServiceFee.Value);
         OverallTotal = Convert.ToDecimal(hdOverallTotal.Value);         
         int resource_key = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));
         string currency = sitetemp.GetCurrency(resource_key);

         bool IsFree = false;

         if (OverallTotal > 0) //Money being spent
         {
             //Add Service fee to overall total                        

             //OverallTotal += decimal.Round(ServiceFee, 2);//(OverallTotal * SFP) + SFC;

             //Get IP Address Request.UserHostAddress
             //update Transaction & get tx_Key
             StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "", "", 1, "", "", 0, "", "", "", "", "", "", "", hdFirstName.Value, hdLastName.Value, ServiceFee, HttpContext.Current.Request.UserHostAddress);
             sp_UpdateTransaction.Execute();
             tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
         }
         else if ((Tickets_Purchased.Count > 0) && (OverallTotal == 0))//Free Events
         {
             IsFree = true;
             //update Transaction & get tx_Key
             StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "", txtFreeEmail.Text, 3, "", "", 0, "", "", "", txtFreeEmail.Text, "", "", "", hdFirstName.Value, hdLastName.Value, 0, HttpContext.Current.Request.UserHostAddress);
             sp_UpdateTransaction.Execute();
             tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
         }

         //update Tickets Purchased
         foreach (DictionaryEntry de in Tickets_Purchased)
         {
             if (de.Value.ToString().Contains("d"))
             {
                 Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), 1, Convert.ToDecimal(de.Value.ToString().Replace("d", "")),0,"","").Execute();
             }
             else
             {
                 Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), Convert.ToInt32(de.Value), 0,0,"","").Execute();
             }             
         }
         if (IsFree)
         {
             Eventomatic_DB.SPs.UpdateTicketNumFreeEvents(Convert.ToInt32(tempTx_Key)).Execute();
         }

         //update Questions Answered
         Questions_Order_Form1.SaveQuestionsAnswered(tempTx_Key,0);

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
    

     protected void btnContinue2_Click(object sender, EventArgs e) //Going from questions to Payment
     {
         bool QuestionsMandatory = Questions_Order_Form1.MandatoryAnswered();

         bool boolTicketsSoldOut = TicketsSoldOut();
         

         /*bool IsNoPurchase = true;
         System.Web.UI.WebControls.DropDownList ddlQuantity;
         foreach (GridViewRow gvrow in GridView1.Rows)
         {
             if (gvrow.RowIndex != GridView1.Rows.Count - 1)
             {
                 if (gvrow.RowType == DataControlRowType.DataRow)
                 {
                     ddlQuantity = (System.Web.UI.WebControls.DropDownList)gvrow.FindControl("ddlQuantity");
                     if (ddlQuantity.SelectedIndex > 0)
                     {
                         IsNoPurchase = false;
                     }
                 }
             }
         }


         if (IsNoPurchase)
         {
             //Still have 0 as checkout value
             lblError.Visible = true;
             lblError.Text = "Please select the Ticket Quantity you wish to purchase.";
             lblError.ForeColor = System.Drawing.Color.Red;
         }*/
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
         else
         {
             if (Page.IsValid)
             {
                 if (hdOverallTotal.Value == "0.00")//it is free tix
                 {
                     pnlChoosePayment.Visible = false;
                     pnlFreeEventEmail.Visible = true;
                     UltraWebTab1.Tabs[2].Text = "Confirmation";
                     hdFreeTicket.Value = "True";
                 }
                 else
                 {
                     pnlChoosePayment.Visible = true;
                     pnlFreeEventEmail.Visible = false;
                     UltraWebTab1.Tabs[2].Text = "Checkout";
                     hdFreeTicket.Value = "False";
                 }

                 lblError.Visible = false;
                 UltraWebTab1.SelectedTab = 2;
                 hdGoToPayment.Value = "1";
             }
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
          


        
    }
}
