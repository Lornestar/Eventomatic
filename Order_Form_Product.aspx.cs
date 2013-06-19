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

namespace Eventomatic
{
    public partial class Order_Form_Product : System.Web.UI.Page
    {
        int Event_Key = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["Event_Key"] != null) && (Request.QueryString["Event_Key"] != ""))
            {
                string strtempeventkey = Request.QueryString["Event_Key"].ToString().Replace(".", "");
                Event_Key = Convert.ToInt32(strtempeventkey);
                if (!IsPostBack)
                {
                    Eventomatic_DB.SPs.UpdateEventViews(Event_Key).Execute();

                    //Webdatechooser date format
                    System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN");
                    ci.DateTimeFormat.LongDatePattern = "dddd, MMMM d, yyyy";
                    ci.DateTimeFormat.ShortDatePattern = "dddd, MMMM d, yyyy";
                    Checkin.CalendarLayout.Culture = ci;
                    Checkout.CalendarLayout.Culture = ci;

                    fillpage();
                    Checkhourly();
                }                
            }
            else
            {
                //if accidentally came to page
            }
            
        }

        protected void fillpage()
        {
            Site sitetemp = new Site();

            DataSet dstemp = Eventomatic_DB.SPs.ViewServiceFee(Event_Key).GetDataSet();
            hdSFP.Value = dstemp.Tables[0].Rows[0]["Service_Fee_Percentage"].ToString();
            Boolean booltemp = sitetemp.IsDecimal("0");

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

            Site Sitetemp = new Site();
            dstemp = Eventomatic_DB.SPs.ViewEventDetails(Event_Key).GetDataSet();

            lblEvent_Name.Text = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
            Page.Title = lblEvent_Name.Text + " - Groupstore.com";

            lblGroupName.Text = dstemp.Tables[0].Rows[0]["Group_Name"].ToString() + "'s";

            string Resource_Key = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
            imgGroup.ImageUrl = Sitetemp.GetResourcePic(Resource_Key);
            imgEvent.ImageUrl = Sitetemp.GetEventPic(Event_Key.ToString());

            string strComments = "";
            if (dstemp.Tables[0].Rows[0]["Additional_Comments"] != DBNull.Value)
            {
                strComments = dstemp.Tables[0].Rows[0]["Additional_Comments"].ToString();
            }
            string strComments2 = strComments.Replace(new String((char)13, 1), "<br>");
            strComments2 = strComments2.Replace("\n", "<br/>");
            if (strComments2.ToLower().Contains("http://"))
            {
                Site sitetemp2 = new Site();
                strComments2 = sitetemp2.ReplaceLinks(strComments2);
            }
            lblComments.Text = strComments2;            

            Checkin.Value = DateTime.Now.Date;
            Checkout.Value = DateTime.Now.Date.AddDays(1);

            lblstorelink.Text = "<a href=store.aspx?Storeid=" + Resource_Key.ToString() + ">";

        }
        protected void btnCheckCart_Click(object sender, EventArgs e)
        {
            Site sitetemp = new Site();
            Response.Redirect("View_Cart.aspx?Storeid=" + sitetemp.GetResourceKeyEventKey(Event_Key));
        }

        protected void btnAvailability_Click(object sender, EventArgs e)
        {
            btnAvailability.Text = "Check Again";
            btnAddCart.Visible = true;
            btnCheckCart.Visible = true;
            FillGrid();
        }

        protected void btnAddCart_Click(object sender, EventArgs e)
        {
            bool noerrors = true;
            if (Convert.ToDecimal(hdOverallTotal.Value) <= 0)
            {
                noerrors = false;
                lblerror.Visible = true;
                lblerror.Text = "Please select a product to purchase";
            }
            bool allquantityzero = true;
            foreach (GridViewRow gr in GridView1.Rows)
            {                
                System.Web.UI.WebControls.DropDownList ddlQuantity = (System.Web.UI.WebControls.DropDownList)gr.FindControl("ddlQuantity");   // look for dropdown in the row
                if ((ddlQuantity.Visible) && (ddlQuantity.SelectedIndex > 0))
                {
                    allquantityzero = false;
                }
                if (ddlQuantity.Visible == false)
                {
                    allquantityzero = false;
                }
            }
            if (allquantityzero)
            {
                noerrors = false;
                lblerror.Visible = true;
                lblerror.Text = "Please select a quantity";
            }                

            if (noerrors)
            {
                int tx_key = 0;
                /*if (Session["Cart_tx_key"] != null)
                {
                    tx_key = Convert.ToInt32(Session["Cart_tx_key"].ToString());
                    DataSet dstxinfo = Eventomatic_DB.SPs.ViewTransactionDetails(tx_key).GetDataSet();
                    if (dstxinfo.Tables[0].Rows.Count > 0)
                    {
                        if (dstxinfo.Tables[0].Rows[0]["Tx_Status"] != DBNull.Value)
                        {
                            if (dstxinfo.Tables[0].Rows[0]["Tx_Status"].ToString() == "2")
                            {
                                Session["Cart_tx_key"] = null;
                                tx_key = 0;
                            }
                        }
                    }
                }*/
                DateTime firstdate = Convert.ToDateTime(Checkin.Value);
                DateTime seconddate = Convert.ToDateTime(Checkout.Value);

                //If hotel
                if (hdCalendarType.Value == "0")
                {
                    
                    if (Checkin_Time.Visible)
                    {
                        firstdate = Convert.ToDateTime(Checkin.Value).AddHours(Convert.ToDateTime(Checkin_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Checkin_Time.Value).Minute);
                    }
                    if (Checkout_Time.Visible)
                    {
                        seconddate = Convert.ToDateTime(Checkout.Value).AddHours(Convert.ToDateTime(Checkout_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Checkout_Time.Value).Minute);
                    }
                }
                else if (hdCalendarType.Value == "1")//if kiting lessons
                {
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)r.FindControl("ddlQuantity");   // look for dropdown in the row
                        if (dropDownList.SelectedIndex > 0)
                        {
                            seconddate = firstdate;
                            System.Web.UI.WebControls.Label lblstarttime = (System.Web.UI.WebControls.Label)r.FindControl("lblstarttime");
                            System.Web.UI.WebControls.Label lblendtime = (System.Web.UI.WebControls.Label)r.FindControl("lblendtime");
                            firstdate = firstdate.AddHours(Convert.ToInt32(lblstarttime.Text));
                            seconddate = seconddate.AddHours(Convert.ToInt32(lblendtime.Text));                            
                            break;
                        }
                    }
                }
                
                Site sitetemp = new Site();
                tx_key = sitetemp.SetupTx(GridView1, Convert.ToDecimal(hdServiceFee.Value), Convert.ToDecimal(hdOverallTotal.Value),
                    Event_Key, tx_key, hdFirstName.Value, hdLastName.Value, "", "0", true, firstdate, seconddate);


                
                //Session["Cart_tx_key"] = tx_key;
                //Response.Redirect("View_Cart.aspx?Storeid=" + sitetemp.GetResourceKeyEventKey(Event_Key));
                
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
                    strNotetemp += "Your hotel has received a payment.";
                }
                paytemp.ParallelPayment(!isdemovar, strcurrency, strNotetemp, HostAmount, strHostEmail, ServiceFeeAmount, strServiceFeeEmail, tx_key, Event_Key);            
                
                

                //Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("PayCC_URL").ToString() + "?tx_key=" + tx_key.ToString());
            }                        
        }

        
        
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicketsSoldout(Event_Key).GetDataSet();
            int lastrow = Convert.ToInt32(hdGridRowsCount.Value)-1;
            Site sitetemp = new Site();
            string strcurrency = sitetemp.GetCurrencySymbol(Event_Key);

            bool isdonationalways = false;
            if ((Request.QueryString["donation"] != null) && (Request.QueryString["donation"] != ""))
            {
                if (Request.QueryString["donation"].ToString() == "true")
                {
                    isdonationalways = true;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //dropDownList.AutoPostBack = true;     // added here for clarity
                //dropDownList.SelectedIndexChanged += new EventHandler(ddlQuantity_SelectedIndexChanged);   // declare event

                if (e.Row.RowIndex == lastrow)
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
                if (lastrow -1>= e.Row.RowIndex)
                {
                    int Ticket_Max = Convert.ToInt32(hdTicketMax.Value);
                    int tickets_left = 0;
                    System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity");   // look for dropdown in the row
                    System.Web.UI.WebControls.Label lblPrice = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblPrice");   // look for price in the row                
                    System.Web.UI.WebControls.Label lblremaining = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblremaining");
                    if (hdCalendarType.Value != "1")
                    {
                        if (hdCalendarType.Value == "0")
                        {
                            DateTime firstdate = Convert.ToDateTime(Checkin.Value);
                            DateTime seconddate = Convert.ToDateTime(Checkout.Value);
                            TimeSpan datediff = seconddate - firstdate;
                            int daysstaying = DateTime.Compare(firstdate.Date, seconddate.Date);
                            e.Row.Cells[0].Text = e.Row.Cells[0].Text + " - " + datediff.Days + " days";
                            lblPrice.Text = strcurrency + Convert.ToString(Convert.ToDecimal(lblPrice.Text.Replace("$","")) * datediff.Days);
                        }

                        if (dstemp.Tables[0].Rows[e.Row.RowIndex]["Tickets_Left"] != DBNull.Value)
                        {
                            tickets_left = Convert.ToInt32(lblremaining.Text);
                        }

                        //Set Ticket Max
                        for (int i = Ticket_Max; i < 10; i++)
                        {
                            dropDownList.Items.RemoveAt(dropDownList.Items.Count - 1);
                        }
                        //if ((dstemp.Tables[0].Rows[e.Row.RowIndex]["Sold_Out"].ToString() == "Sold Out") && ((System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity") != null))
                        if ((lblremaining.Text == "0") && ((System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity") != null))
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
                        

                        //Check if there is a free ticket being sold
                        if (lblPrice.Text.Trim() == "$0.00")
                        {
                            hdFreeTicket.Value = "True";
                        }
                        

                        //check if donation box
                        if ((dstemp.Tables[0].Rows[e.Row.RowIndex]["isdonation"] != DBNull.Value) || (isdonationalways))
                        {
                            string strtemp = dstemp.Tables[0].Rows[e.Row.RowIndex]["isdonation"].ToString();
                            if ((dstemp.Tables[0].Rows[e.Row.RowIndex]["isdonation"].ToString().ToLower() == "true") || (isdonationalways))
                            {                                                    
                                lblPrice.Visible = false;
                                System.Web.UI.WebControls.Label lblDollarsign = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDollarSign");   // look for price in the row                
                                System.Web.UI.WebControls.TextBox txtDonate = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtDonate");
                                System.Web.UI.WebControls.Label lblDonate = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDonate");   // look for price in the row                
                                txtDonate.Visible = true;                                
                                lblDollarsign.Visible = true;
                                //if (!isdonationalways)
                                //{
                                    dropDownList.Visible = false;
                                    lblDonate.Visible = true;
                                //}            
                                //txtDonate.Attributes.Add("onKeyUp", "ManipulateGrid(" + Gridview1_index_Global.ToString() + "); return false;");
                            }
                        }
                    }
                    else //kiting lessons
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            dropDownList.Items.RemoveAt(dropDownList.Items.Count - 1);
                        }
                        if (isdonationalways)
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


            }
        }

        protected void FillGrid()
        {
            DateTime startdate = Convert.ToDateTime(Checkin.Value);
            DateTime enddate = Convert.ToDateTime(Checkout.Value);
            if (Checkin_Time.Visible)
            {
                startdate = Convert.ToDateTime(Checkin.Value).AddHours(Convert.ToDateTime(Checkin_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Checkin_Time.Value).Minute);
            }
            if (Checkout_Time.Visible)
            {
                enddate = Convert.ToDateTime(Checkout.Value).AddHours(Convert.ToDateTime(Checkout_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Checkout_Time.Value).Minute);
            }
            
            DataSet dstemp = new DataSet();
            if (hdCalendarType.Value == "0") //hotel rooms
            {
               dstemp = Eventomatic_DB.SPs.ViewTicketProductAvailability(Event_Key,startdate,enddate,1).GetDataSet();
                dstemp.Tables[0].Columns.Add("Start_Time", typeof(int));
                dstemp.Tables[0].Columns.Add("End_Time", typeof(int));
            }
            else if (hdCalendarType.Value == "1") //kiting lessons
            {
                int earliesttime = Convert.ToInt32(hdEarliestTime.Value);
                int latesttime = Convert.ToInt32(hdLatestTime.Value) - Convert.ToInt32(ddlAdults.SelectedValue);
                DataSet dsChecklessons = Eventomatic_DB.SPs.ViewTicketProductAvailabilityLessons(Event_Key, startdate).GetDataSet();
                //Create hashtable with hours instructor is buy
                Hashtable htbusy = new Hashtable();
                foreach (DataRow r in dsChecklessons.Tables[0].Rows)
                {
                    DateTime tempstart = Convert.ToDateTime(r["Start_Date"]);
                    DateTime tempend = Convert.ToDateTime(r["End_Date"]);                    
                    for (int i = tempstart.Hour;i<tempend.Hour;i++)
                    {
                        htbusy.Add(i,i);
                    }
                }

                DateTime tempdt = Convert.ToDateTime(Checkin.Value).Date.AddHours(earliesttime);
                DataTable dttemp = new DataTable();
                dttemp.Columns.Add("Ticket_Description", typeof(string));
                dttemp.Columns.Add("Quantity_Remaining", typeof(int));
                dttemp.Columns.Add("PriceRounded", typeof(decimal));
                dttemp.Columns.Add("Ticket_Key", typeof(int));
                dttemp.Columns.Add("Start_Time", typeof(int));
                dttemp.Columns.Add("End_Time", typeof(int));
                dttemp.Columns.Add("Display_Tickets_Available");
                dttemp.Columns.Add("Sale_Ends");                
                for (int i = earliesttime;i<=latesttime;i++)
                {
                    bool isavailable = true;
                    for (int j = i;j < i+Convert.ToInt32(ddlAdults.SelectedValue);j++)
                    {
                        if (htbusy.ContainsValue(j))
                        {
                            isavailable = false;
                        }
                    }
                    if (isavailable)
                    {
                        int sessionends = i + Convert.ToInt32(ddlAdults.SelectedValue);
                        dttemp.Rows.Add("Session from " + i.ToString() + ":00 to " + sessionends.ToString() + ":00", 1, Convert.ToDecimal(hdTicketPrice.Value), Convert.ToInt32(hdTicket_Key.Value), i, sessionends);
                    }
                }
                dstemp.Tables.Add(dttemp);
            }
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

            hdGridRowsCount.Value = dstemp.Tables[0].Rows.Count.ToString();
            GridView1.DataSource = dstemp.Tables[0];
            GridView1.DataBind();            
        }

        protected void Checkhourly()
        {//For column calendar type
            //0 is hotel rooms / 1 is kiting lessons / 2 is kiting rentals
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicket(Event_Key).GetDataSet();
            bool kitinglessons = false;
            bool kitingrentals = false;
            foreach (DataRow r in dstemp.Tables[0].Rows)
            {
                if (r["Calendar_Type"] != DBNull.Value)
                {
                    if (r["Calendar_Type"].ToString() == "1")
                    {
                        kitinglessons = true;
                    }
                    else if (r["Calendar_Type"].ToString() == "2")
                    {
                        kitingrentals = true;
                    }
                }
            }

            if (kitinglessons)
            {
                hdCalendarType.Value = "1";
                lblcheckin.Text = "Begin lesson at:";
                lblcheckout.Visible = false;
                pnlAdults.Visible = true;                
                Checkout.Visible = false;
                lblAdults.Text = "Desired Lesson length:";
                foreach (DataRow r in dstemp.Tables[0].Rows)
                {
                    if (r["Calendar_Type"] != DBNull.Value)
                    {
                        if (r["Calendar_Type"].ToString() == "1")
                        {
                            ddlAdults.Items.Add(r["Lesson_Length"].ToString());
                            hdEarliestTime.Value = r["Lesson_Earliest_Time"].ToString();
                            hdLatestTime.Value = r["Lesson_Latest_Time"].ToString();                            
                        }                        
                    }
                }
                SetTicketinfo();
                
            }
            else if (kitingrentals)
            {
                hdCalendarType.Value = "2";
                Checkin_Time.Visible = true;
                Checkout_Time.Visible = true;
            }
        }

        protected void SetTicketinfo()
        {
            DataSet dsticketinfo = Eventomatic_DB.SPs.ViewTicketLessonLength(Event_Key, Convert.ToInt32(ddlAdults.SelectedValue)).GetDataSet();
            hdTicket_Key.Value = dsticketinfo.Tables[0].Rows[0]["Ticket_Key"].ToString();
            hdTicketPrice.Value = dsticketinfo.Tables[0].Rows[0]["Price"].ToString();
        }

        protected void ddlAdults_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTicketinfo();
            FillGrid();
        }
    }
}
