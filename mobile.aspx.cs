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
    public partial class mobile : System.Web.UI.Page
    {
        int Event_Key = 0;
        bool displaydemoticket = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["event"] != null) && (Request.QueryString["event"] != ""))
            {
                if (Request.QueryString["event"].ToString().Contains("frompc"))
                {
                    Event_Key = Convert.ToInt32(Request.QueryString["event"].ToString().Replace("frompc",""));
                    btnPayPal.Visible = false;
                    //btnshare.Text = "Send Payment info to buyer";                    
                }
                else
                {
                    Event_Key = Convert.ToInt32(Request.QueryString["event"].ToString());
                }                
            }
            if ((Request.QueryString["code"] != null) && (Request.QueryString["code"] != ""))
            {
                Setfbid();
            }
            if ((Request.QueryString["frompc"] != null) && (Request.QueryString["frompc"] != ""))
            {
                btnPayPal.Visible = false;
                //btnshare.Text = "Send Payment info to buyer";
            }
            if (Event_Key != 0)
            {
                hdeventkey.Value = Event_Key.ToString();
                hdCurrentDate.Value = DateTime.Now.ToString("MMM dd, yyyy HH:mm:ss");
                if (!IsPostBack)
                {
                    hypmobileleader.NavigateUrl = hypmobileleader.NavigateUrl.Replace("0", Event_Key.ToString());
                    hypmobilenews.NavigateUrl = hypmobilenews.NavigateUrl.Replace("0", Event_Key.ToString());
                    //Update page views
                    Eventomatic_DB.SPs.UpdateEventViews(Event_Key).Execute();

                    hypefullsite.NavigateUrl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "order_form.aspx?event_key=" + Event_Key + "&notmobile=true";
                    /*DataSet dstemp = Eventomatic_DB.SPs.ViewServiceFee(Event_Key).GetDataSet();
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
                    hdSFM.Value = dstemp.Tables[0].Rows[0]["Service_Fee_Max"].ToString();*/
                    //Check if have paypal account                    
                    Site sitetemp = new Site();
                    if (!sitetemp.IsDemo(Event_Key))
                    {
                        lblDemo.Visible = false;
                    }
                    else
                    {
                        hdTrial.Value = "True";
                        lblDemo.Text = "Demo Version";
                        lblDemo.BackColor = System.Drawing.Color.LightBlue;
                    }

                    PopulateExistingEvent(Event_Key);                    

                    if (sitetemp.isMobile())
                    {
                        lblismobile.Text = "Mobile";
                    }
                    else
                    {
                        lblismobile.Text = "Not Mobile";
                    }

                    HttpContext curcontext = HttpContext.Current;

                    lblismobile.Text = curcontext.Request.ServerVariables["HTTP_USER_AGENT"];

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
                }
            }
            if (Request.Form["__EVENTTARGET"] == "btnshare")
            {
                Checkout(1);
            }
        }


        protected void PopulateExistingEvent(int Event_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewEventDetails(Event_Key).GetDataSet();

            lblEvent_Name.Text = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();

            if (dstemp.Tables[0].Rows[0]["Leader_Prize"] != DBNull.Value)
            {
                lblLeaderPrize.Text = dstemp.Tables[0].Rows[0]["Leader_Prize"].ToString();
            }

            Page.Title = lblEvent_Name.Text;
            Site Sitetemp = new Site();
            hdcurrencysymbol.Value = Sitetemp.GetCurrencySymbol(Event_Key);

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

                if (GridView1.Rows.Count == 2)
                {
                    System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)GridView1.Rows[0].FindControl("ddlQuantity");   // look for dropdown in the row
                    dropDownList.SelectedIndex = 1;
                }

                //DataSet dstemp2 = Eventomatic_DB.SPs.ViewQuestion(Event_Key).GetDataSet();
                if (hdFreeTicket.Value == "True")
                {
                    pnlemail1.Visible = true;
                    pnlemail2.Visible = true;
                    btnContinue.Visible = true;
                    btnPayPal.Visible = false;
                    btnshare.Visible = false;
                }

                //load questions
                System.Web.UI.WebControls.HiddenField hdnEvent_Key2 = new System.Web.UI.WebControls.HiddenField();
                hdnEvent_Key2 = (System.Web.UI.WebControls.HiddenField)Questions_Order_Form1.FindControl("Event_Key");
                hdnEvent_Key2.Value = Event_Key.ToString();
                Questions_Order_Form1.LoadPage();

                /*
                GridView2.DataSource = dstemp2.Tables[0];
                GridView2.DataBind();
                */
            }
            else
            {
                //Selling hasn't begun yet
                lblSellingDeadline.Visible = true;
                lblSellingDeadline.ForeColor = System.Drawing.Color.Red;
                if (isremoved)
                {
                    lblSellingDeadline.Text = "This event is no longer selling tickets. Contact the event organizer if you would like to know why.";
                    btnContinue.Visible = false;
                    btnshare.Visible = false;                    
                }
                else if (Begin_Selling_Earliest != DateTime.MinValue)
                {
                    lblSellingDeadline.Text = "Tickets for this event will be going on sale beginning " + Begin_Selling_Earliest.ToString("dddd, MMMM d yyyy") + ".";
                }
                else
                {
                    lblSellingDeadline.Text = "Tickets for this event are currently not selling.";
                    btnContinue.Visible = false;
                    btnshare.Visible = false;
                }                

                if (displaydemoticket) //show demo ticket
                {
                    DataSet dsdemo = Eventomatic_DB.SPs.ViewTicketDemo(Event_Key).GetDataSet();
                    if (dsdemo.Tables[0].Rows.Count > 0)
                    {
                        DataRow drtemp = dsdemo.Tables[0].NewRow();
                        dsdemo.Tables[0].Rows.Add(drtemp);

                        GridView1.DataSource = dsdemo.Tables[0];
                        GridView1.DataBind();
                 
                    }
                }
            }
        }


        protected void Checkout(int type)
        {
            bool isoktocheckout = true;
            if ((txtList_Name.Text == ""))
            {
                isoktocheckout = false;
                lblError.Visible = true;
                lblError.Text = "You must enter in a name for the guest list";                
            }
            if (!ConfirmQuantitySelected())//didn't select ticket
            {
                isoktocheckout = false;
                lblError.Visible = true;
                lblError.Text = "You must select a ticket quantity";
            }
            if (!Questions_Order_Form1.MandatoryAnswered())
            {
                isoktocheckout = false;
                lblError.Visible = true;
                lblError.Text = "Please answer all mandatory questions.";
            }
            lblError2.Visible = lblError.Visible;
            lblError2.Text = "<br/>"+lblError.Text;
            if (isoktocheckout)
            {
                if (hdOverallTotal.Value == "0.00")//it is free tix
                {
                    hdFreeTicket.Value = "True";

                    if (txtFreeEmail.Text == "")
                    {
                        isoktocheckout = false;
                        lblError.Visible = true;
                        lblError.Text = "You must enter in an email address for the guest list";
                        lblError2.Visible = lblError.Visible;
                        lblError2.Text = "<br/>" + lblError.Text;
                    }
                    else
                    {
                        int Tx_Key = SetupTx();
                        Send_Email SE = new Send_Email();
                        SE.Send_Transaction_Email(Tx_Key, "");
                        //string strnewurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "mobileconfirm.aspx?Tx_Key=" + Tx_Key;
                        Response.Redirect(ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "mobileconfirm.aspx?Tx_Key=" + Tx_Key);
                        //Page.RegisterClientScriptBlock("gotourl", "<script language=javascript>gotourl('" + strnewurl + "');</script>");
                        //hdgotourl.Value = strnewurl;
                    }
                }
                else //paid event
                {
                    hdFreeTicket.Value = "False";
                    int tx_key = SetupTx();

                    if (type == 1) //sharing
                    {
                        if (hdnfbid.Value == "0")
                        {
                            Response.Redirect("MobileShare.aspx?tx=" + tx_key);
                            //Page.RegisterClientScriptBlock("gotourl", "<script language=javascript>gotourl('MobileShare.aspx?tx=" + tx_key + "');</script>");
                            //hdgotourl.Value = "MobileShare.aspx?tx=" + tx_key;
                        }
                        else
                        {
                            Response.Redirect("MobileShare.aspx?tx=" + tx_key + "&fbid=" + hdnfbid.Value);
                            //Page.RegisterClientScriptBlock("gotourl", "<script language=javascript>gotourl('MobileShare.aspx?tx=" + tx_key + "&fbid=" + hdnfbid.Value + "');</script>");
                            //hdgotourl.Value = "MobileShare.aspx?tx=" + tx_key + "&fbid=" + hdnfbid.Value;
                        }
                    }
                    else
                    {                       
                            string strnewurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "mobilepay.aspx?tx=" + tx_key;
                            //strnewurl = strnewurl.Replace("http://", "https://");
                            Response.Redirect(strnewurl);                     
                            //Page.RegisterClientScriptBlock("gotourl", "<script language=javascript>gotourl('" + strnewurl + "');</script>");
                            //hdgotourl.Value = strnewurl;
                    }                    
                }
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e) //Going from questions to Payment
        {
            Checkout(0);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicketsSoldout(Event_Key).GetDataSet();

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
                //check if it is soldout or not
                if (dstemp.Tables[0].Rows.Count - 1 >= e.Row.RowIndex)
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
                        }
                    }
                }         
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
            
            string tempTx_Key = "0";
            decimal ServiceFee = Convert.ToDecimal(hdServiceFee.Value);
            OverallTotal = Convert.ToDecimal(hdOverallTotal.Value);
            int resource_key = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));
            string currency = sitetemp.GetCurrency(resource_key);

            bool IsFree = false;

            char[] chrspace = {' '};
            int intfirstspace = txtList_Name.Text.IndexOfAny(chrspace);

            string strFirstName = "";
            string strLastName = ""; 
            
            if (intfirstspace > 0)
            {
                strFirstName = txtList_Name.Text.Substring(0, intfirstspace).Trim();
                strLastName = txtList_Name.Text.Substring(intfirstspace).Trim();
            }
            else
            {
                strFirstName = txtList_Name.Text;                
            }

            

            if (OverallTotal > 0) //Money being spent
            {
                //Add Service fee to overall total                        

                //OverallTotal += decimal.Round(ServiceFee, 2);//(OverallTotal * SFP) + SFC;

                //Get IP Address Request.UserHostAddress
                //update Transaction & get tx_Key
                StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "", "", 1, "", "", 0, "", "", "", "", "", "", "", strFirstName, strLastName, ServiceFee, HttpContext.Current.Request.UserHostAddress);
                sp_UpdateTransaction.Execute();
                tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
            }
            else if ((Tickets_Purchased.Count > 0) && (OverallTotal == 0))//Free Events
            {
                IsFree = true;
                //update Transaction & get tx_Key
                StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "", txtFreeEmail.Text, 3, "", "", 0, "", "", "", txtFreeEmail.Text, "", "", "", strFirstName, strLastName, 0, HttpContext.Current.Request.UserHostAddress);
                sp_UpdateTransaction.Execute();
                tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
            }

            int counttix = 0;

            //update Tickets Purchased
            foreach (DictionaryEntry de in Tickets_Purchased)
            {
                if (de.Value.ToString().Contains("d"))
                {
                    StoredProcedure sp_Updatetix = Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), 1, Convert.ToDecimal(de.Value.ToString().Replace("d", "")), 0, strFirstName, strLastName);
                    sp_Updatetix.Execute();

                    int temptixkey = Convert.ToInt32(sp_Updatetix.Command.Parameters[4].ParameterValue.ToString());
                    Questions_Order_Form1.SaveQuestionsAnswered(tempTx_Key, temptixkey);

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

                        StoredProcedure sp_Updatetix = Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), 1, 0, 0, strFirstName, strLastName);
                        sp_Updatetix.Execute();

                        int temptixkey = Convert.ToInt32(sp_Updatetix.Command.Parameters[4].ParameterValue.ToString());

                        Questions_Order_Form1.SaveQuestionsAnswered(tempTx_Key, temptixkey);

                        if (IsFree)
                        {
                            Eventomatic_DB.SPs.UpdateTicketNumFreeEvents(Convert.ToInt32(tempTx_Key)).Execute();
                        }
                        counttix += 1;
                    }
                }
            }


            //update Questions Answered
            //Questions_Order_Form1.SaveQuestionsAnswered( .SaveQuestionsAnswered (tempTx_Key);

            //update ticket seller
            if (hdnfbid.Value != "0")
            {
                Eventomatic_DB.SPs.UpdateTransactionFbids(Convert.ToInt64(hdnfbid.Value.ToString()), 0, Convert.ToInt32(tempTx_Key)).Execute();                
            }

            return Convert.ToInt32(tempTx_Key);
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

        protected bool ConfirmQuantitySelected()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("Questions_Key");
            dttemp.Columns.Add("tix_Name");

            decimal dcmtemp = 0;

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.DropDownList ddlQuantity = (System.Web.UI.WebControls.DropDownList)gvr.FindControl("ddlQuantity");
                    System.Web.UI.WebControls.TextBox txtDonate = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txtDonate");
                    Label lbldescription = (Label)gvr.FindControl("lbldescription");

                    if (ddlQuantity.Visible)
                    {
                        dcmtemp += ddlQuantity.SelectedIndex;

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
                    }

                    if (txtDonate.Visible)
                    {
                        dcmtemp += decimal.Parse(txtDonate.Text);

                        int currentnum = dttemp.Rows.Count + 1;

                        DataRow dr = dttemp.NewRow();
                        dr["Questions_Key"] = "0";
                        dr["tix_Name"] = lbldescription.Text + " #" + currentnum;
                        dttemp.Rows.Add(dr);
                    }
                }
            }


            if (dcmtemp > 0)
            {                

                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnPayPal_Click(object sender, EventArgs e)
        {
            Checkout(0);
        }

        protected void btnLeader_Click(object sender, EventArgs e)
        {
            Response.Redirect("MobileLeader.aspx?event=" + Event_Key);
        }

        protected void btnNews_Click(object sender, EventArgs e)
        {
            Response.Redirect("MobileNews.aspx?event=" + Event_Key);
        }

        protected void btncheckinpaypal_Click(object sender, EventArgs e)
        {
            string thereturnpage = HttpContext.Current.Request.Url.AbsoluteUri;
            if (thereturnpage.Contains("frompc"))
            {
                thereturnpage = thereturnpage.Replace("&frompc=true", "frompc");
            }
            Response.Redirect("http://www.facebook.com/dialog/oauth?client_id=" + ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + thereturnpage + "&display=touch&scope=email,publish_stream,offline_access");
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
            string result = wc.DownloadString("https://graph.facebook.com/oauth/access_token?client_id=" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "&redirect_uri=" + HttpContext.Current.Request.Url.AbsoluteUri + "&client_secret=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Secret").ToString() + "&code=" + oauth);
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
                btncheckinpaypal.Visible = false;
                string email = "";
                string firstname = "";
                string lastname = "";
                if (o["email"]!= null)
                {
                    email = (string)o["email"];
                }
                if (o["first_name"]!= null)
                {
                    firstname = (string)o["first_name"];
                }
                if (o["last_name"]!= null)
                {
                    lastname = (string)o["last_name"];
                }

                Eventomatic_DB.SPs.UpdateTicketSellers(Convert.ToInt64(fbid), firstname+ " " +lastname,accesstoken,email).Execute();
            }
            catch
            {
            }

        }
        
        protected void btnShare_Click(object sender, EventArgs e)
        {
            Checkout(1);
            
        }
        
    }

}