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
using com.paypal.soap.api;

namespace Eventomatic
{
    public partial class PayCC : System.Web.UI.Page
    {
        int Tx_Key = 0;
        int Event_Key = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            hdCurrentDate.Value = DateTime.Now.ToString("MMM dd, yyyy HH:mm:ss");
            if (IsPostBack)
            {                
                Event_Key = Convert.ToInt32(hdEvent_Key.Value);
                Tx_Key = Convert.ToInt32(hdTx_Key.Value);
            }
            if (!IsPostBack)
            {
                if ((Request.QueryString["Tx_Key"] != null) && (Request.QueryString["Tx_Key"] != ""))
                {
                    Tx_Key = Convert.ToInt32(Request.QueryString["Tx_Key"].ToString());
                    Site sitetemp = new Site();
                    Event_Key = Convert.ToInt32(sitetemp.GetEventKeyTx(Tx_Key));
                    hdTx_Key.Value = Tx_Key.ToString();
                    hdEvent_Key.Value = Event_Key.ToString();
                }
                else if ((Request.Form["tx_key"] != null) && (Request.Form["tx_key"] != ""))
                {
                    string therighttxkey = Request.Form["Tx_Key"].ToString();
                    string strtemp = Request.Form["Tx_Key"].ToString();
                    string[] strtemp2 = strtemp.Split(',');
                    Site sitetemp = new Site();
                    foreach (string eachone in strtemp2)
                    {
                        if (sitetemp.IsNumeric(eachone))
                        {
                            therighttxkey = eachone;
                        }
                    }
                    Tx_Key = Convert.ToInt32(therighttxkey);
                    Event_Key = Convert.ToInt32(sitetemp.GetEventKeyTx(Tx_Key));
                    hdTx_Key.Value = Tx_Key.ToString();
                    hdEvent_Key.Value = Event_Key.ToString();
                }
                
                DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(Tx_Key).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    //There is a transaction actually happening
                    if (dstemp.Tables[0].Rows[0]["GuestList_First_Name"] != DBNull.Value)
                    {
                        txtPayFirstName.Text = dstemp.Tables[0].Rows[0]["GuestList_First_Name"].ToString();
                    }
                    if (dstemp.Tables[0].Rows[0]["GuestList_Last_Name"] != DBNull.Value)
                    {
                        txtPayLastName.Text = dstemp.Tables[0].Rows[0]["GuestList_Last_Name"].ToString();
                    }
                    if (dstemp.Tables[0].Rows[0]["Amount"] != DBNull.Value)
                    {
                        hdOverallTotal.Value = dstemp.Tables[0].Rows[0]["Amount"].ToString();
                    }
                    if (dstemp.Tables[0].Rows[0]["Service_Fee"] != DBNull.Value)
                    {
                        hdServiceFee.Value = dstemp.Tables[0].Rows[0]["Service_Fee"].ToString();
                    }

                    //Setup Country & Province
                    dstemp = Eventomatic_DB.SPs.ViewInfoCountry(0).GetDataSet();
                    ddlCountry.DataSource = dstemp.Tables[0];
                    ddlCountry.DataTextField = "Country_Text";
                    ddlCountry.DataValueField = "Country_Value";
                    ddlCountry.DataBind();

                    Site Sitetemp = new Site();
                    string strtemp2 = Sitetemp.GetResourceCurrencyTx(Tx_Key);
                    AssignProvince(strtemp2);

                    //Setup Checkout Grid
                    dstemp = Eventomatic_DB.SPs.ViewPayCC(Tx_Key).GetDataSet();
                    DataRow drtemp = dstemp.Tables[0].NewRow();                    
                    dstemp.Tables[0].Rows.Add(drtemp);
                    GridView1.DataSource = dstemp.Tables[0];
                    GridView1.DataBind();

                    PopulateExistingEvent(Event_Key);
                    Site sitetemp = new Site();
                    if (!sitetemp.IsDemo(Event_Key))
                    {
                        lblDemo.Visible = false;
                    }
                    else
                    {
                        lblDemo.Text = "Trial Version";
                        lblDemo.BackColor = System.Drawing.Color.LightBlue;
                    }

                    //Check timing, to see if can still buy tix
                    dstemp = Eventomatic_DB.SPs.ViewTicketAll(Event_Key).GetDataSet();
                    bool IsSelling = false; //if true, that means it is selling now
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
                    hdLastTicketDate.Value = Selling_Deadling_Latest.ToString("MMM dd, yyyy HH:mm:ss");
                    if (!IsSelling)
                    {
                        //Time is up, don't allow to pay
                        pnlbuttons.Visible = false;
                        lblTimeup.Visible = true;
                        lblTimeup.Text = "Tickets for this event are no longer selling online";
                        lblTimeup.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    //Inform them there is no transaction to pay for
                    pnlCC.Visible = false;
                    pnlError.Visible = true;
                }
            }
        }

        protected void PopulateExistingEvent(int Event_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewEventDetails(Event_Key).GetDataSet();

            lblEvent_Name.Text = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
            Page.Title = lblEvent_Name.Text;
            //Replace(strValue, Chr(13) & Chr(10), "<br>")
            lblGroupName.Text = dstemp.Tables[0].Rows[0]["Group_Name"].ToString() + "'s";

            //Add images
            string Resource_Key = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
            Site Sitetemp = new Site();

            imgGroup.ImageUrl = Sitetemp.GetResourcePic(Resource_Key);
            //check if is person's groupstore
            string fbid = "";
            if (Request.Form["fbid"] != null)
            {
                fbid = Request.Form["fbid"].ToString();
            }
            else if (Request.QueryString["fbid"] != null)
            {
                fbid = Request.QueryString["fbid"].ToString();
            }
            if (fbid != "")
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
                try
                {
                    string result = wc.DownloadString("http://graph.facebook.com/" + fbid + "/picture");
                    imgGroup.ImageUrl = "http://graph.facebook.com/" + fbid + "/picture";

                    result = wc.DownloadString("http://graph.facebook.com/" + fbid + "?fields=name");
                    if (!result.Contains("error"))
                    {
                        string[] strresults = result.Split('"');
                        lblGroupName.Text = strresults[3] + "'s";
                    }                    
                }
                catch
                {
                }
            }                
            if (imgGroup.ImageUrl == "")
            {
                imgGroup.Visible = false;
            }



            imgEvent.ImageUrl = Sitetemp.GetEventPic(Event_Key.ToString());
            
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
            Site sitetemp = new Site();
            string symbol = sitetemp.GetCurrencySymbol(Event_Key);
            string currency = sitetemp.GetResourceCurrency(Event_Key);
            //DataSet dstemp = Eventomatic_DB.SPs.ViewTicketsSoldout(Event_Key).GetDataSet();
            string tempstring = sender.ToString();
            tempstring = e.ToString();                        
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                System.Web.UI.WebControls.Label lblDonationAmount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDonationAmount");
                System.Web.UI.WebControls.Label lblTotal = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
                if (lblDonationAmount.Text != "")
                {
                    if (Convert.ToDecimal(lblDonationAmount.Text) != 0)//is donation box
                    {
                        
                        lblTotal.Text = symbol + " " + decimal.Round(Convert.ToDecimal(lblDonationAmount.Text), 2);
                    }
                    else
                    {
                        lblTotal.Text = symbol + " " + decimal.Round(Convert.ToDecimal(lblTotal.Text), 2);
                    }
                }
                
                
                //dropDownList.AutoPostBack = true;     // added here for clarity
                //dropDownList.SelectedIndexChanged += new EventHandler(ddlQuantity_SelectedIndexChanged);   // declare event
                if (e.Row.RowIndex == Eventomatic_DB.SPs.ViewPayCC(Tx_Key).GetDataSet().Tables[0].Rows.Count)
                {
                    System.Web.UI.WebControls.Label lblQuantity = (System.Web.UI.WebControls.Label)e.Row.FindControl("ddlQuantity");   // look for dropdown in the row
                    if (lblQuantity != null)
                    { lblQuantity.Visible = false; }

                    System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblServiceFee");
                    if (label != null)
                    {
                        label.Visible = true;
                    }                                        
                    if (lblTotal != null)
                    {
                        lblTotal.Text = symbol + " " + decimal.Round(decimal.Parse(hdServiceFee.Value.ToString()), 2) ;
                    }
                    
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                System.Web.UI.WebControls.Label lblTotal = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotalOverall");
                if (lblTotal != null)
                {
                    lblTotal.Text = symbol + " " + decimal.Round(decimal.Parse(hdOverallTotal.Value.ToString()), 2) + " " + currency;
                }
            }
        }


        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            
        }

        protected void AssignProvince(string Currency)
        {
            DataSet dstemp;
            if (Currency == "USD")
            {
                ddlCountry.SelectedIndex = 1;
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(2).GetDataSet();
                lblAreaZipCode.Text = "Zip Code";
                lblStateProvince.Text = "State";
            }
            else
            {
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(1).GetDataSet();
                lblAreaZipCode.Text = "Postal Code";
                lblStateProvince.Text = "Province";
            }
            ddlStateProvince.DataSource = dstemp.Tables[0];
            ddlStateProvince.DataTextField = "Region_Text";
            ddlStateProvince.DataValueField = "Region_Value";
            ddlStateProvince.DataBind();
        }

        protected com.paypal.soap.api.CountryCodeType SelectedCountry()
        {            
            return (CountryCodeType)Enum.Parse(typeof(CountryCodeType), ddlCountry.SelectedValue.ToString(), true);//rtnCode;
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Changed Country Selection         
            DataSet dstemp;
            if (ddlCountry.SelectedItem.Text == "United States")
            {
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(2).GetDataSet();
                ddlStateProvince.DataSource = dstemp.Tables[0];
                lblAreaZipCode.Text = "Zip Code";
                lblStateProvince.Text = "State";
                ddlStateProvince.Visible = true;
                txtPayStateProvince.Visible = false;
                RequiredFieldValidator10.Enabled = false;
            }
            else if (ddlCountry.SelectedItem.Text == "Canada")
            {
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(1).GetDataSet();
                ddlStateProvince.DataSource = dstemp.Tables[0];
                lblAreaZipCode.Text = "Postal Code";
                lblStateProvince.Text = "Province";
                ddlStateProvince.Visible = true;
                txtPayStateProvince.Visible = false;
                RequiredFieldValidator10.Enabled = false;
            }
            else if (ddlCountry.SelectedItem.Text == "Australia")
            {
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(12).GetDataSet();
                ddlStateProvince.DataSource = dstemp.Tables[0];
                lblAreaZipCode.Text = "Postcode";
                lblStateProvince.Text = "State/Territory";
                ddlStateProvince.Visible = true;
                txtPayStateProvince.Visible = false;
                RequiredFieldValidator10.Enabled = false;
            }
            else
            {
                ddlStateProvince.Visible = false;
                lblAreaZipCode.Text = "Postal/Zip Code";
                lblStateProvince.Text = "State / Province / Region";
                txtPayStateProvince.Visible = true;
                RequiredFieldValidator10.Enabled = true;
            }
            
            ddlStateProvince.DataTextField = "Region_Text";
            ddlStateProvince.DataValueField = "Region_Value";
            ddlStateProvince.DataBind();
        }

        protected void btnCC_Click(object sender, EventArgs e)
        {
            //Check if is Email
            Site sitetemp = new Site();
            Boolean isdemovar = sitetemp.IsDemo(Event_Key);
            if ((sitetemp.isEmail(txtPayEmail.Text)) || (Page.IsValid))
            {
                //Setup Currency, Group Name, Host email
                string strGroupName = "";
                string[] strPaypalEmail = new String[1];
                string strCurrency = "CAD";
                string PayMethod = "0";
                com.paypal.soap.api.CurrencyCodeType[] tempCurrency = new com.paypal.soap.api.CurrencyCodeType[1];
                DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromTxKey(Tx_Key).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    if (dstemp.Tables[0].Rows[0]["Group_Name"] != DBNull.Value)
                    {
                        strGroupName = dstemp.Tables[0].Rows[0]["Group_Name"].ToString();
                    }
                    if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)//have paypal email address
                    {
                        strPaypalEmail.SetValue(dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString(), 0);
                    }
                    else
                    {
                        
                        if (!isdemovar)
                         {
                             strPaypalEmail.SetValue(System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString(),0);
                         }
                         else
                         {
                             strPaypalEmail.SetValue(System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString(),0);
                         }
                        
                    }
                    if (dstemp.Tables[0].Rows[0]["Desired_Currency"] != DBNull.Value)
                    {
                        strCurrency = dstemp.Tables[0].Rows[0]["Desired_Currency"].ToString();
                    }
                    switch (strCurrency)
                    {
                        case "USD": tempCurrency.SetValue(com.paypal.soap.api.CurrencyCodeType.USD, 0);
                            break;
                        case "CAD": tempCurrency.SetValue(com.paypal.soap.api.CurrencyCodeType.CAD, 0);
                            break;
                        case "EUR": tempCurrency.SetValue(com.paypal.soap.api.CurrencyCodeType.EUR, 0);
                            break;
                        case "GBP": tempCurrency.SetValue(com.paypal.soap.api.CurrencyCodeType.GBP, 0);
                            break;
                        case "ILS": tempCurrency.SetValue(com.paypal.soap.api.CurrencyCodeType.ILS, 0);
                            break;
                    }
                    Eventomatic_DB.SPs.UpdateTransactionCurrency(Tx_Key, tempCurrency.GetValue(0).ToString()).Execute();
                    if (dstemp.Tables[0].Rows[0]["Pay_Method"] != DBNull.Value)
                    {
                        PayMethod = dstemp.Tables[0].Rows[0]["Pay_Method"].ToString();
                    }
                }

                string strSubject = ""; 
                string strNotetemp = "";
                if (sitetemp.isAlphaNumeric(lblEvent_Name.Text))
                {
                    strSubject += lblEvent_Name.Text + " has received payment.";
                    strNotetemp += lblEvent_Name.Text;
                }
                else
                {
                    strSubject += "Your event has received payment.";
                    strNotetemp += "Your event has";
                }
                strNotetemp += " received payment.";
                if ((sitetemp.isAlphaNumeric(txtPayFirstName.Text)) && (sitetemp.isAlphaNumeric(txtPayLastName.Text)))
                {
                    strNotetemp += "  Payment was made by Credit Card by " + txtPayFirstName.Text + " " + txtPayLastName.Text; 
                }                
                
                decimal OverallTotal = Convert.ToDecimal(hdOverallTotal.Value);
                decimal ServiceFee = Convert.ToDecimal(hdServiceFee.Value);
                com.paypal.soap.api.CountryCodeType CountryCode = SelectedCountry();
                Site Sitetemp = new Site();                
                Eventomatic.Addons.PaypalMethods paytemp = new Eventomatic.Addons.PaypalMethods();
                DoCaptureResponseType strAck = new DoCaptureResponseType();
                Hashtable httemp = new Hashtable();
                DoDirectPaymentResponseType DirectPaymentObj = new DoDirectPaymentResponseType();
                httemp = paytemp.DoDirectPaymentCode(decimal.Round(OverallTotal, 2).ToString(), txtPayLastName.Text, txtPayFirstName.Text, txtPayBillingAddress1.Text, txtPayBillingAddress2.Text, txtPayCity.Text, ddlStateProvince.SelectedValue, txtPayAreaZipCode.Text, ddlType.Text, txtPayCCNum.Text,
                    txtPayCSC.Text, Convert.ToInt32(txtPaymonth.Value.ToString()), Convert.ToInt32("20" + txtPayyear.Value.ToString()), com.paypal.soap.api.PaymentActionCodeType.Sale, ddlCountry.SelectedValue.ToString(), CountryCode, Tx_Key.ToString(), (com.paypal.soap.api.CurrencyCodeType)tempCurrency.GetValue(0), !isdemovar);
                
                strAck = (DoCaptureResponseType)httemp["capt_response"];
                DirectPaymentObj = (DoDirectPaymentResponseType)httemp["pp_response"];
                if (strAck.Ack.ToString().Contains("Success")) //Payment went through
                {
                    decimal MassPayAmount = 0;
                    decimal MassPayFee = 0;
                    decimal PaypalAmount = ((OverallTotal - ServiceFee) * Convert.ToDecimal(0.029)) + Convert.ToDecimal(0.3);
                    string[] strValue = new String[1]; ;
                    string[] strUniqueID = new String[1]; ;
                    string[] strNote = new String[1];
                    strNote.SetValue(strNotetemp, 0);
                    
                    //Calculate Mass Payment                    
                    MassPayAmount = decimal.Round((OverallTotal - ServiceFee - PaypalAmount), 2);
                    MassPayFee = MassPayAmount * Convert.ToDecimal(0.02);
                    if (MassPayFee > 1)
                    {
                        MassPayFee = 1;
                    }

                    //Choose largest value  MasspayFee or MasspayAmount
                    if (MassPayFee > ServiceFee)
                    {
                        MassPayAmount = decimal.Round((OverallTotal - MassPayFee - PaypalAmount), 2);                                      
                    }
                    strValue.SetValue(MassPayAmount.ToString(), 0);

                    Eventomatic_DB.SPs.UpdateTransaction(Tx_Key, 0, "", OverallTotal, strCurrency, 0, txtPayFirstName.Text, txtPayLastName.Text, "", "", 4, DirectPaymentObj.TransactionID, "", 0, "", "", "", txtPayEmail.Text, "", "", "", "", "", 0, "").Execute();

                    //Create PDF Receipt
                    DataSet dsisfundraiser = Eventomatic_DB.SPs.ViewIsFundraiser(Event_Key).GetDataSet();
                    bool isfundraiser = false;
                    if (dsisfundraiser.Tables[0].Rows[0]["IsFundraiser"] != DBNull.Value)
                    {
                        if (dsisfundraiser.Tables[0].Rows[0]["IsFundraiser"].ToString() == "True")
                        {
                            isfundraiser = true;
                        }
                    }
                    string pdfreceipt = "";
                    string donarinfo = "";
                    string locationissued = "";
                    if (isfundraiser)
                    {
                        donarinfo = txtPayFirstName.Text + " " + txtPayLastName.Text + "\n" + txtPayBillingAddress1.Text + "\n" + txtPayBillingAddress2.Text + "\n" + txtPayCity.Text + ", " + ddlStateProvince.Text + "\n" + txtPayAreaZipCode.Text;
                        locationissued = txtPayCity.Text + ", " + ddlStateProvince.Text;
                        Eventomatic.Addons.PDFReceipt PDF = new Eventomatic.Addons.PDFReceipt();
                        pdfreceipt = PDF.CreatePDFReceipt(Tx_Key, donarinfo,locationissued);
                    }

                    //Send email                    
                    Send_Email SE = new Send_Email();
                    SE.Send_Transaction_Email(Tx_Key,pdfreceipt);
                    
                    bool Avoid = false;
                    string strtemp9 = strPaypalEmail.GetValue(0).ToString().ToUpper();
                    string strtemp10 = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString().ToUpper();
                    if (((strPaypalEmail.GetValue(0).ToString().ToUpper() == System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString().ToUpper()) && (!isdemovar)) || ((strPaypalEmail.GetValue(0).ToString().ToUpper() == System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString().ToUpper()) && (isdemovar)))
                    {
                        Avoid = true;
                    }
                    if (PayMethod != "1") //0 or null mean end of event / 1 means each transaction
                    {
                        Avoid = true;
                    }
                    if (!Avoid)
                    {
                        string strMassPayAck = paytemp.MassPayCode(strSubject, com.paypal.soap.api.ReceiverInfoCodeType.EmailAddress, strPaypalEmail, strValue, strUniqueID, strNote, tempCurrency, 1, Tx_Key, !isdemovar);
                        if (strMassPayAck.Contains("Success"))
                        {
                            int resource_key = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));
                            Eventomatic_DB.SPs.UpdateTransactionOut(Convert.ToDecimal(strValue[0]), strPaypalEmail[0], resource_key, Event_Key, MassPayFee, tempCurrency[0].ToString(), 0,0,0).Execute();
                        }
                    }                    
                    // do a Response.Redirect
                    /*Eventomatic.Addons.RemotePost myremotepost = new Eventomatic.Addons.RemotePost();
                    myremotepost.Url = System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/"+"Order_Confirmation.aspx";
                    myremotepost.Add("Tx_key",Tx_Key.ToString());                    
                    myremotepost.Post() ;*/
                    
                    //System.Web.HttpContext.Current.Response.Write(string.Format("","Tx_Key",Tx_Key.ToString()));
                    

                    //Response.Write("Tx_key=" + );
                    Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/"+"Order_Confirmation.aspx?Tx_Key="+Tx_Key.ToString());
                    //Response.Redirect("Order_Confirmation.aspx?Tx_key=" + Tx_Key.ToString());
                }
                else //Payment did not go through
                {
                    lblError.Text = "";
                    for (int i = 0; i <= strAck.Errors.Length-1; i++)
                    {
                        ErrorType ETtemp = (ErrorType)strAck.Errors.GetValue(i);
                        Eventomatic_DB.SPs.UpdateCCErrors(ETtemp.LongMessage.ToString(), Tx_Key, 0, OverallTotal.ToString()).Execute();
                        lblError.Text += ETtemp.LongMessage.ToString();
                    }
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Red;

                }
            }
            else //Not email
            {
                lblError.Visible = true;
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "";
                if (sitetemp.isEmail(txtPayEmail.Text))
                {
                    lblError.Text += "Please enter a valid Email Address.<br/>";                    
                }
                if (txtPayFirstName.Text == "")
                {
                    lblError.Text += "Please enter a First Name.<br/>";
                }
                if (txtPayLastName.Text == "")
                {
                    lblError.Text += "Please enter a Last Name.<br/>";
                }
                if (txtPayCCNum.Text == "                ")
                {
                    lblError.Text += "Please enter a Credit Card #.<br/>";
                }
                if (txtPaymonth.Text == "  ")
                {
                    lblError.Text += "Please enter a valid Expiry Date Month.<br/>";
                }
                if (txtPayyear.Text == "  ")
                {
                    lblError.Text += "Please enter a valid Expiry Date Year.<br/>";
                }
                if (txtPayCSC.Text == "   ")
                {
                    lblError.Text += "Please enter a valid CSC #.<br/>";
                }
                if (txtPayBillingAddress1.Text == "")
                {
                    lblError.Text += "Please enter an address.<br/>";
                }
                if (txtPayCity.Text == "")
                {
                    lblError.Text += "Please enter a City.<br/>";
                }
                if (txtPayAreaZipCode.Text == "")
                {
                    lblError.Text += "Please enter an Area Code/Zip Code.";
                }
            }
            /* CC - 4795179738066222
             * CC - 4551210059365825
             * CC - 4795179738066284
             * CC - Test User 4757 6541 1543 8898 exp - 01/11 000             
             */
        }

    }
}
