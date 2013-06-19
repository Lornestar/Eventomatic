using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PayPal.Platform.SDK;
using PayPal.Services.Private.AP;
/*using PayPal.Payments.DataObjects;
using PayPal.Payments.Transactions;
using PayPal.Payments.Common.Utility;*/
using com.paypal.sdk.services;
using com.paypal.soap.api;
using com.paypal.sdk.profiles;
using com.paypal.sdk.util;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using PayPal.Services.Private.AA;
using PayPal.Services.Private.Permissions;


namespace Eventomatic.Addons
{
    public class PaypalMethods
    {
        #region Parallel Payments        
        public void ParallelPayment(bool Live_Trial, string strCurrencyCode, string strmemo, decimal dcAmount1, string strEmail1, decimal dcAmount2, string strEmail2, int Tx_Key, int Event_Key)
        {
            PayRequest payRequest = null;
            PayPal.Platform.SDK.BaseAPIProfile profile2 = new PayPal.Platform.SDK.BaseAPIProfile();

            /*bool usedigitalgoods = true;
            if ((Event_Key == 381) || (Event_Key == 481))
            {
                usedigitalgoods = false;
            }*/
            bool usedigitalgoods = false;

            ////Three token 
             
            profile2.APIProfileType = ProfileType.ThreeToken;            
            
            if (Live_Trial)//true = Live , false = trial
            {
                profile2.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Live_Environment").ToString();
                profile2.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString();
                profile2.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                profile2.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                profile2.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
            }
            else
            {
                profile2.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Trial_Environment").ToString();
                profile2.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile2.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                profile2.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                profile2.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }
            /*profile2.RequestDataformat = "SOAP11";
            profile2.ResponseDataformat = "SOAP11";
            */            


            profile2.IsTrustAllCertificates = Convert.ToBoolean(ConfigurationManager.AppSettings["TrustAll"]);

            string url = System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/";
            string returnURL = url + "Order_Confirmation.aspx?Tx_key=" + Tx_Key.ToString();
            string cancelURL = url + "Order_Form.aspx?Event_Key=" + Event_Key.ToString();
            //profile2 = (BaseAPIProfile)HttpContext.Current.Session[Constants.SessionConstants.PROFILE];
            /*profile2.APIUsername = "lorne_1261162854_biz_api1.lornestar.com";
            profile2.APIPassword = "1261162857";
            profile2.APISignature = "Ag4nejYcMq0VFMCdFMpkphAahlwbAXwU43WtfmIl4FC6WJNf0j.0SNj7";
            profile2.APIProfileType = ProfileType.ThreeToken;*/

            payRequest = new PayRequest();
            payRequest.cancelUrl = cancelURL;
            payRequest.returnUrl = returnURL;
            payRequest.ipnNotificationUrl = System.Configuration.ConfigurationSettings.AppSettings.Get("Order_Form_IPNURL").ToString() + Tx_Key+"&Paytype=AP";
            payRequest.reverseAllParallelPaymentsOnError = true;
            

            //payRequest.senderEmail = email.Value;
            //payRequest.clientDetails = new ClientDetailsType();
            //payRequest.clientDetails = ClientInfoUtil.getMyAppDetails();            

            payRequest.feesPayer = "EACHRECEIVER";//feesPayer.Value;
            payRequest.memo = strmemo;// memo.Value;
            payRequest.actionType = "PAY";
            payRequest.currencyCode = strCurrencyCode; //currencyCode.Items[currencyCode.SelectedIndex].Value;
            payRequest.requestEnvelope = new PayPal.Services.Private.AP.RequestEnvelope();
            payRequest.requestEnvelope.errorLanguage = "en_US";//ClientInfoUtil.getMyAppRequestEnvelope();

            payRequest.receiverList = new Receiver[2];
            payRequest.receiverList[0] = new Receiver();
            payRequest.receiverList[0].amount = decimal.Round(dcAmount1,2);//amount_0.Value);
            payRequest.receiverList[0].email = strEmail1;//receiveremail_0.Value;            
            if (usedigitalgoods)
            {
                payRequest.receiverList[0].paymentType = "DIGITALGOODS";
            }
            

            if ((decimal.Round(dcAmount2, 2) > 0) && (strEmail2 != strEmail1))
            {
                payRequest.receiverList[1] = new Receiver();
                payRequest.receiverList[1].amount = decimal.Round(dcAmount2, 2);
                payRequest.receiverList[1].email = strEmail2;// receiveremail_1.Value;
                if (usedigitalgoods)
                {
                    payRequest.receiverList[1].paymentType = "DIGITALGOODS";
                }                
            }
            if (strEmail2 == strEmail1)
            {
                payRequest.receiverList[0].amount += decimal.Round(dcAmount2, 2);                
            }
            Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(Tx_Key, strEmail1).Execute();

            profile2.ResponseDataformat = "SOAP11";
            profile2.RequestDataformat = "SOAP11";
                        

            PayPal.Platform.SDK.AdapativePayments ap = new PayPal.Platform.SDK.AdapativePayments();
            
            ap.APIProfile = profile2;                        

            PayResponse PResponse = ap.pay(payRequest);            

            if (ap.isSuccess.ToUpper() == "FAILURE")
            {
                //HttpContext.Current.Session[Constants.SessionConstants.FAULT] = ap.LastError;                
                for (int i = 0; i <= ap.LastError.ErrorDetails.Length - 1; i++)
                {
                    PayPal.Platform.SDK.FaultDetailFaultMessageError ETtemp = (PayPal.Platform.SDK.FaultDetailFaultMessageError)ap.LastError.ErrorDetails.GetValue(i);
                    decimal OverallTotal = decimal.Round(dcAmount1, 2) + decimal.Round(dcAmount2, 2);
                    Eventomatic_DB.SPs.UpdateCCErrors(ETtemp.message.ToString(), Tx_Key, 2, OverallTotal.ToString()).Execute();
                }
                HttpContext.Current.Response.Redirect("APIError.aspx", false);
            }
            else
            {

                if (PResponse.paymentExecStatus == "COMPLETED")
                {
                    //record any amount of money that got transfered to event host
                    if (strEmail2 != strEmail1)
                    {
                        Site sitetemp = new Site();
                        int resource_key = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));
                        Eventomatic_DB.SPs.UpdateTransactionOut(decimal.Round(dcAmount1, 2), strEmail1, resource_key, Event_Key, 0, strCurrencyCode, 0,0,0).Execute();
                    }
                    HttpContext.Current.Response.Redirect(returnURL);//"PaymentDetails.aspx?paykey=" + PResponse.payKey, false);
                }
                else
                {
                    //HttpContext.Current.Session[Constants.SessionConstants.PAYKEY] = PResponse.payKey;
                    if (Live_Trial)//true = Live , false = trial
                    {
                        if (usedigitalgoods)
                        {
                            HttpContext.Current.Response.Redirect("https://paypal.com/webapps/adaptivepayment/flow/pay?paykey=" + PResponse.payKey, false);
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("https://www.paypal.com/webscr?cmd=" + "_ap-payment&paykey=" + PResponse.payKey, false);
                        }
                        
                    }
                    else
                    {
                        if (usedigitalgoods)
                        {
                            HttpContext.Current.Response.Redirect("https://sandbox.paypal.com/webapps/adaptivepayment/flow/pay?paykey=" + PResponse.payKey, false);
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("https://www.sandbox.paypal.com/webscr?cmd=" + "_ap-payment&paykey=" + PResponse.payKey, false);
                        }                        
                    }

                }

            }
        }
        #endregion

        #region Implicit Direct Payments
        public void ImplicitPayment(bool Live_Trial, string strCurrencyCode, string strmemo, decimal dcAmount1, string strEmail1, int Tx_Key, int Event_Key)
        {
            PayRequest payRequest = null;
            PayPal.Platform.SDK.BaseAPIProfile profile2 = null;

            ////Three token 
            profile2 = new PayPal.Platform.SDK.BaseAPIProfile();
            profile2.APIProfileType = ProfileType.ThreeToken;

            payRequest = new PayRequest();

            if (Live_Trial)//true = Live , false = trial
            {
                profile2.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Live_Environment").ToString();
                profile2.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString();
                profile2.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                profile2.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                profile2.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
                payRequest.senderEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
            }
            else
            {
                profile2.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Trial_Environment").ToString();
                profile2.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile2.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                profile2.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                profile2.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
                payRequest.senderEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString();
            }
            profile2.RequestDataformat = "XML";
            profile2.ResponseDataformat = "XML";

            profile2.IsTrustAllCertificates = Convert.ToBoolean(ConfigurationManager.AppSettings["TrustAll"]);

            string url = System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/";
            string returnURL = url + "Order_Confirmation.aspx?Tx_key=" + Tx_Key.ToString();
            string cancelURL = url + "Order_Form.aspx?Event_Key=" + Event_Key.ToString();
            //profile2 = (BaseAPIProfile)HttpContext.Current.Session[Constants.SessionConstants.PROFILE];
            /*profile2.APIUsername = "lorne_1261162854_biz_api1.lornestar.com";
            profile2.APIPassword = "1261162857";
            profile2.APISignature = "Ag4nejYcMq0VFMCdFMpkphAahlwbAXwU43WtfmIl4FC6WJNf0j.0SNj7";
            profile2.APIProfileType = ProfileType.ThreeToken;*/

            
            payRequest.cancelUrl = cancelURL;
            payRequest.returnUrl = returnURL;
            payRequest.ipnNotificationUrl = System.Configuration.ConfigurationSettings.AppSettings.Get("Order_Form_IPNURL").ToString() + Tx_Key + "&Paytype=AP";
            
            //payRequest.clientDetails = new ClientDetailsType();
            //payRequest.clientDetails = ClientInfoUtil.getMyAppDetails();            

            payRequest.feesPayer = "EACHRECEIVER";//feesPayer.Value;
            payRequest.memo = strmemo;// memo.Value;
            payRequest.actionType = "PAY";
            payRequest.currencyCode = strCurrencyCode; //currencyCode.Items[currencyCode.SelectedIndex].Value;
            payRequest.requestEnvelope = new PayPal.Services.Private.AP.RequestEnvelope();
            payRequest.requestEnvelope.errorLanguage = "en_US";//ClientInfoUtil.getMyAppRequestEnvelope();

            payRequest.receiverList = new Receiver[2];
            payRequest.receiverList[0] = new Receiver();
            payRequest.receiverList[0].amount = decimal.Round(dcAmount1, 2);//amount_0.Value);
            payRequest.receiverList[0].email = strEmail1;//receiveremail_0.Value;
            


            PayPal.Platform.SDK.AdapativePayments ap = new PayPal.Platform.SDK.AdapativePayments();
            ap.APIProfile = profile2;
            PayResponse PResponse = ap.pay(payRequest);

            if (ap.isSuccess.ToUpper() == "FAILURE")
            {
                //HttpContext.Current.Session[Constants.SessionConstants.FAULT] = ap.LastError;                
                for (int i = 0; i <= ap.LastError.ErrorDetails.Length - 1; i++)
                {
                    PayPal.Platform.SDK.FaultDetailFaultMessageError ETtemp = (PayPal.Platform.SDK.FaultDetailFaultMessageError)ap.LastError.ErrorDetails.GetValue(i);
                    decimal OverallTotal = decimal.Round(dcAmount1, 2);
                    Eventomatic_DB.SPs.UpdateCCErrors(ETtemp.message.ToString(), Tx_Key, 2, OverallTotal.ToString()).Execute();
                }
                HttpContext.Current.Response.Redirect("APIError.aspx", false);
            }
            else
            {

                if (PResponse.paymentExecStatus == "COMPLETED")
                {                    
                    //HttpContext.Current.Response.Redirect(returnURL);//"PaymentDetails.aspx?paykey=" + PResponse.payKey, false);
                }
                else
                {
                    //HttpContext.Current.Session[Constants.SessionConstants.PAYKEY] = PResponse.payKey;
                    if (Live_Trial)//true = Live , false = trial
                    {
                        HttpContext.Current.Response.Redirect("https://www.paypal.com/webscr?cmd=" + "_ap-payment&paykey=" + PResponse.payKey, false);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("https://www.sandbox.paypal.com/webscr?cmd=" + "_ap-payment&paykey=" + PResponse.payKey, false);
                    }

                }

            }
        }
        #endregion

        #region Credit Card API Crappy Version
        /*private String mOrderId;

        private void CreditCardPayment()
        {
            mOrderId = PayflowUtility.RequestId;
            // When checkout is clicked, perform a verify enrollment transaction 
            // to check whether the user is enrolled for the buyer authentication 
            // service or not.

            // Before proceeding with transaction processing, you should persist the order details.
            // The details of the order that are generally persisted are:
            // Credit card details : Card Number, Expiry date ( Card CVV2 number cannot be stored).
            // Billing address details, shipping address details.
            // Storing these details helps you to allow a returning customer to do a faster 
            // checkout, having his/her details already populated in the desired fields.
            // Storing the ordering details is generally uses database.
            // Here, however for the demonstration purpose, this web application stores the order 
            // details in a flat file named <order_id>.ord and retrieves it later. 
            // Note that this is not a recommended method to persist order details.
            //PersistOrderDetails(mOrderId);
            if (chkProcessWithBuyerAuth.Checked)
            {
                // After a successful Verify Transaction,
                // if the user is enrolled for the buyer authentication service;
                // user's browser needs to be redirected to his/her banks'/cards' server(Access Control Server [ACS]).
                // Here he/she authenticates him/her self with his login information.
                // During this process, your web application will not have any control over the proceedings unless the 
                // ACS does not redirect back to your server. 
                DoVerifyEnrollmentAndRedirect();
            }
            else
            {
                // Perform an authorization transaction.
                // Populate the transaction from persisted order details.
                AuthorizationTransaction Trans = PopulateTransaction(mOrderId);

                // Submit the transaction.
                Trans.SubmitTransaction();

                if (Trans.Response.TransactionResponse.Result >= 0)
                {
                    // Persist the response paramaters in the order details.
                    // It is a good practice to store AVSADDR, AVSZIP, CVV2MATCH 
                    // along with the unique transaction reference number PNREF.
                    Base64Encoder Encoder = new Base64Encoder();

                    System.Security.Permissions.FileIOPermission IOPerm = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.Write, AppDomain.CurrentDomain.BaseDirectory + "orders");
                    IOPerm.Demand();
                    FileInfo OrderFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"orders\" + mOrderId + ".ord");
                    if (OrderFile.Exists)
                    {
                        StreamWriter OrderWriter = OrderFile.AppendText();
                        OrderWriter.WriteLine("PnRef=" + Trans.Response.TransactionResponse.Pnref);
                        OrderWriter.WriteLine("Result=" + Trans.Response.TransactionResponse.Result.ToString());
                        OrderWriter.WriteLine("RespMsg=" + Trans.Response.TransactionResponse.RespMsg);
                        OrderWriter.WriteLine("AvsAddr=" + Trans.Response.TransactionResponse.AVSAddr);
                        OrderWriter.WriteLine("AvsZip=" + Trans.Response.TransactionResponse.AVSZip);
                        OrderWriter.WriteLine("Cvv2Match=" + Trans.Response.TransactionResponse.CVV2Match);
                        OrderWriter.Flush();
                        OrderWriter.Close();
                    }
                }
                ShowStatusAndRedirect(Trans);
            }
        }

        private void DoVerifyEnrollmentAndRedirect(string CCNumber,string ExpMonth,string ExpYear)
        {

            // Create the Credit card object.
            CreditCard Card = new CreditCard(CCNumber,
                ExpMonth + ExpYear);

            // Create the currency object for amount. 
            // Here the currency code is the 3 digit ISO country code.
            // For US -> 840 or USD.
            Currency Amt = new Currency(new decimal(21.98), "840");

            // Create a Verify Enrollment Transaction.
            BuyerAuthVETransaction Trans = new BuyerAuthVETransaction
                (Constants.PayflowBAUser,
                Card, Constants.Connection,
                Amt,
                PayflowUtility.RequestId);

            // Submit the transaction.
            Response Resp = Trans.SubmitTransaction();

            // Redirect to the ACS ( Access Control Server, eg. users' bank)
            // The URL to ACS and PaReq (Payer Authentication Request -- 
            // digitally signed, encrypted request to authenticate user's enrollment 
            // upon his/her login is returned in the response of verify enrollment.
            // This happens if, the user is enrolled for buyerauth abd the transaction succeeds.
            if (Resp.TransactionResponse.Result == 0)
            {

                // Check if the user is enrolled for the buyer authentication service.
                // If the AUTHENTICATION_STATUS response parameter is E, means user is 
                // enrolled for this service. If so, redirect the user's browser  as follows 
                // to his/her bank's secure URL with PaReq, both obtained in ACSURL response parameter:
                // In this you can use the MD field to set any descriptive field or a key to your persisted 
                // order details (such as order id), which are required to retrive later on.
                // TermUrl is the URL of the page of your web application where the bank's secure server will 
                // redirect the PaRes as the authentication response to for further processing.
                if (String.Equals("E", Resp.BuyerAuthResponse.Authentication_Status))
                {
                    // This means the user has enrolled him/her self for the buyer authentication 
                    // service. Therefore, now the user should be redirected to his/her bank's secure 
                    // server.
                    //	String RedirectUrl = Trans.Response.BuyerAuthResponse.AcsUrl 
                    //	+ "?PaReq=" 
                    //	+ System.Web.HttpUtility.UrlEncode(Trans.Response.BuyerAuthResponse.PaReq.Trim())
                    //	+ "&TermUrl=" 
                    //	+ Constants.LocalHostName +"/CreditCardDetails.aspx&MD="+mOrderId;
                    // Response.Redirect(RedirectUrl,true);
                    RedirectBuyerAuthRequest(Trans.Response.BuyerAuthResponse.PaReq.Trim(), Trans.Response.BuyerAuthResponse.AcsUrl, mOrderId);
                }
                else
                {
                    // If the user is not enrolled for buyer authentication, 
                    // take the decision accornding to your business logic whether to 
                    // allow the transaction to proceed or not.
                    // Here in this ficitous store front, the decision is taken to go 
                    // ahead with an authorization.
                    AuthorizationTransaction TransAuth = PopulateTransaction(mOrderId);
                    Response RespAuth = TransAuth.SubmitTransaction();
                    ShowStatusAndRedirect(TransAuth);
                }
            }
            else
            {
                ShowStatusAndRedirect(Trans);
            }
        }

        public void RedirectBuyerAuthRequest(string PayLoad, string ACSUrl, string mTransactionId)
        {
            string myTermURL = Constants.LocalHostName + "/CreditCardDetails.aspx";
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<html><head>");
            System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", "form1"));
            System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" enctype=\"{3}\">", "form1", "POST", ACSUrl, "application/x-www-form-urlencoded"));
            System.Web.HttpContext.Current.Response.Write(string.Format("<input type='hidden' name='PaReq' value='" + PayLoad + "'>"));
            System.Web.HttpContext.Current.Response.Write(string.Format("<input type='hidden' name='TermUrl' value='" + myTermURL + "'>"));
            System.Web.HttpContext.Current.Response.Write(string.Format("<input type='hidden' name='MD' value='" + mTransactionId + "'>"));
            System.Web.HttpContext.Current.Response.Write("</form>");
            System.Web.HttpContext.Current.Response.Write("</body></html>");
            System.Web.HttpContext.Current.Response.End();
        }

        private AuthorizationTransaction PopulateTransaction(String OrderId)
        {
            Hashtable OrderTable = new Hashtable();
            // Populate the authorization transaction from the persisted order 
            // details. This will generally involve populating the order from 
            // your order database. Here, however for the demonstration purpose, 
            // the order details are stored in a flat file named <order_id>.ord.
            // Note that, this is not a recommneded method to persist order details 
            // and is only implemented for demonstration purpose.
            {
                Base64Encoder Decoder = new Base64Encoder();
                StreamReader OrderReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"orders\" + OrderId + ".ord");
                if (OrderReader != null)
                {
                    while (OrderReader.Peek() >= 0)
                    {
                        String CurrLine = OrderReader.ReadLine();
                        int SepIndex = CurrLine.IndexOf("=", 0);
                        String Name = CurrLine.Substring(0, SepIndex);
                        String Value = String.Empty;
                        if (SepIndex < CurrLine.Length - 1)
                        {
                            if (Name == "CCNumber")
                            {
                                // Example of how you might decrypt the credit card number should you decide to store it.
                                Value = Decoder.Decrypt(CurrLine.Substring(SepIndex + 1));
                            }
                            else
                            {
                                Value = CurrLine.Substring(SepIndex + 1);
                            }
                            OrderTable.Add(Name, Value);
                        }
                    }
                }
                OrderReader.Close();
            }

            // Populate the Billing address details.
            BillTo Bill = new BillTo();
            Bill.FirstName = (String)OrderTable["FirstName"];
            Bill.LastName = (String)OrderTable["LastName"];
            Bill.Street = (String)OrderTable["Street"];
            Bill.City = (String)OrderTable["City"];
            Bill.Zip = (String)OrderTable["Zip"];
            Bill.State = (String)OrderTable["State"];
            
            // Populate the Shipping address details.
            ShipTo Ship = new ShipTo();
            Ship.ShipToFirstName = (String)OrderTable["ShipToFName"];
            Ship.ShipToLastName = (String)OrderTable["ShipToLName"];
            Ship.ShipToStreet = (String)OrderTable["ShipToStreet"];
            Ship.ShipToCity = (String)OrderTable["ShipToCity"];
            Ship.ShipToZip = (String)OrderTable["ShipToZip"];
            Ship.ShipToState = (String)OrderTable["ShipToState"];

            // Populate the invoice
            Invoice Inv = new Invoice();
            Inv.BillTo = Bill;
            Inv.ShipTo = Ship;
            Inv.InvNum = OrderId;
            Inv.Amt = new Currency(decimal.Parse((String)OrderTable["Amount"]));

            // Populate the Credit Card details.
            CreditCard Card = new CreditCard((String)OrderTable["CCNumber"],
                (String)OrderTable["ExpDate"]);

            // Note that CVV2 is not persisted in database.
            // You should never store CVV2 value.
            Card.Cvv2 = txtCVV2.Text;

            // Create the Tender.
            CardTender Tender = new CardTender(Card);

            // Create the transaction.
            AuthorizationTransaction Trans = new AuthorizationTransaction(Constants.PayflowBAUser, Constants.Connection, Inv, Tender, OrderId);

            return Trans;

        }

        private void ShowStatusAndRedirect(BaseTransaction Trans)
        {
            bool mSuccess = false;
            if (Trans.Response.TransactionResponse.Result >= 0)
            {
                mSuccess = true;
            }

            if (Trans.Response.TransactionResponse.Result == 0)
            {
                Response.Redirect("PurchaseComplete.aspx?auth=YES", true);
            }
            else
            {
                String Message = "Your order cannot be completed at this time.";
                String MessageError = "";

                // If result code is greater than 0 (Zero), the transaction is discarded 
                // by the Payflow server. The reason why the transaction is discarded is 
                // evident by the result code value and therefore, you should look at this 
                // result code and decide if 
                // 1. The customer has given some wrong inputs,
                // 2. It's a fraudulent transaction.
                // 3. There's a problem with your merchant account credentials etc.
                // (This is more likely to be caught in your test scenarios.)
                if (mSuccess)
                {
                    // Here you can decide what message needs to be shown to 
                    // the customer based on the result code returned by the Payflow 
                    // Pro service.
                    Message += " Please check your credit card details.";
                    // Normally you wouldn't dislay the result code on your web page. This is just for review.
                    MessageError = "Result code = " + Trans.Response.TransactionResponse.Result.ToString() + ", RespMsg = " + Trans.Response.TransactionResponse.RespMsg.ToString();
                }
                else
                {
                    // A negative result code means there was an error thrown from the 
                    // Payflow SDK for .NET. Pls make sure that your configurations is 
                    // correct.
                    Message += "An internal error occurred.";
                    MessageError = "Result code = " + Trans.Response.TransactionResponse.Result.ToString() + ", RespMsg = " + Trans.Response.TransactionResponse.RespMsg.ToString();
                }

                Response.Redirect("PurchaseComplete.aspx?auth=NO&msg=" + HttpUtility.UrlEncode(Message) + "&msgError=" + HttpUtility.UrlEncode(MessageError), true);
            }
        }*/
        #endregion

        # region DirectPayments
        public Hashtable DoDirectPaymentCode(string paymentAmount, string buyerLastName, string buyerFirstName, string buyerAddress1, string buyerAddress2, string buyerCity, string buyerState, string buyerZipCode, string creditCardType, string creditCardNumber, string CVV2, int expMonth, int expYear, PaymentActionCodeType paymentAction,
            string buyerCountry, CountryCodeType buyerCountryCode, string Tx_Key, CurrencyCodeType tempCurrency, bool Live_Trial)
        {            
            com.paypal.sdk.services.CallerServices caller = new com.paypal.sdk.services.CallerServices();
            
            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();
            
            Site sitetemp = new Site();

            // Set up your API credentials, PayPal end point, and API version.
            if (Live_Trial)//true = Live , false = trial
            {
                profile.Environment = "live";                                
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();                
                string strliveemail = ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();

                if (sitetemp.GetResourceKeytxKey(Convert.ToInt32(Tx_Key)) == "208")
                {
                    profile.APIUsername = "info_api1.jewelenvy.ca";
                    profile.APIPassword = "A22THS64KHCU4VQF";
                    profile.APISignature = "A62K1O2NI-niqyr2CXz.9njuLBAFARQG2OhP1sC04u4bmH.6lvllRk-R";
                    strliveemail = "info@jewelenvy.ca";
                }

                if (sitetemp.GetResourceKeytxKey(Convert.ToInt32(Tx_Key)) == "211")
                {
                    profile.APIUsername = "karen.grant_api1.angelonenetwork.ca";
                    profile.APIPassword = "DRARRDSKL5DCCBLM";
                    profile.APISignature = "AFcWxV21C7fd0v3bYYYRCpSSRl31AzgZdEbToRYw-4BINCHPbFo0D2lw";
                    strliveemail = "karen.grant@angelonenetwork.ca";
                }

                Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(Convert.ToInt32(Tx_Key), strliveemail).Execute();
            }
            else
            {
                profile.Environment = "sandbox";                 
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();                

                Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(Convert.ToInt32(Tx_Key), System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString()).Execute();
            }
            //profile.Environment = "sandbox"; 
            profile.Subject = "SUBJECT=perm@lornestar.com";
            //profile.getFirstPartyEmail[0] = "perm@lornestar.com";
            
            caller.APIProfile = profile;            
            

            // Create the request object.
            DoDirectPaymentRequestType pp_Request = new DoDirectPaymentRequestType();
            pp_Request.Version = "60.0";            

            // Add request-specific fields to the request.
            // Create the request details object.
            pp_Request.DoDirectPaymentRequestDetails = new DoDirectPaymentRequestDetailsType();

            pp_Request.DoDirectPaymentRequestDetails.IPAddress = HttpContext.Current.Request.UserHostAddress;//"10.244.43.106";
            if (Live_Trial)//true = Live , false = trial
            {
                pp_Request.DoDirectPaymentRequestDetails.MerchantSessionId = "6E3F6YL2V86S2";
            }
            else
            {
                pp_Request.DoDirectPaymentRequestDetails.MerchantSessionId = "CBTG55E523YZY";
            }


            pp_Request.DoDirectPaymentRequestDetails.PaymentAction = PaymentActionCodeType.Authorization;

            pp_Request.DoDirectPaymentRequestDetails.CreditCard = new CreditCardDetailsType();

            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardNumber = creditCardNumber;
            switch (creditCardType)
            {
                case "Visa":
                    pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardType = CreditCardTypeType.Visa;
                    break;
                case "MasterCard":
                    pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardType = CreditCardTypeType.MasterCard;
                    break;
                case "Discover":
                    pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardType = CreditCardTypeType.Discover;
                    break;
                case "Amex":
                    pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardType = CreditCardTypeType.Amex;
                    break;
            }
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CVV2 = CVV2.Trim();
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.ExpMonth = expMonth;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.ExpYear = expYear;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.ExpMonthSpecified = true;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.ExpYearSpecified = true;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner = new PayerInfoType();
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Payer = "";
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerID = "";
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerStatus = PayPalUserStatusCodeType.unverified;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerCountry = CountryCodeType.US;

            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address = new com.paypal.soap.api.AddressType();
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.Street1 = buyerAddress1;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.Street2 = buyerAddress2;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.CityName = buyerCity;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.StateOrProvince = buyerState;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.PostalCode = buyerZipCode;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.CountryName = buyerCountry; //"USA";
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.Country = buyerCountryCode; //CountryCodeType.US;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.CountrySpecified = true;

            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerName = new PersonNameType();
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerName.FirstName = buyerFirstName;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerName.LastName = buyerLastName;
            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails = new PaymentDetailsType();
            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails.OrderTotal = new BasicAmountType();
            //pp_Request.DoDirectPaymentRequestDetails.PaymentDetails.NotifyURL = System.Configuration.ConfigurationSettings.AppSettings.Get("Order_Form_IPNURL").ToString() + Tx_Key;
            // NOTE: The only currency supported by the Direct Payment API at this time is US dollars (USD).

            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails.OrderTotal.currencyID = tempCurrency;//CurrencyCodeType.7;
            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails.OrderTotal.Value = paymentAmount;
            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails.InvoiceID = Tx_Key.ToString();           
            

            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails.ButtonSource = "GroupStore_Cart_DP";

            caller.APIProfile.Environment = "live";
            // Execute the API operation and obtain the response.
            DoDirectPaymentResponseType pp_response = new DoDirectPaymentResponseType();
            pp_response = (DoDirectPaymentResponseType)caller.Call("DoDirectPayment", pp_Request);

            DoCaptureResponseType capt_response = new DoCaptureResponseType();

            if (!pp_response.Ack.ToString().Contains("Success")) //doDirectPayment did not go through
            {
                for (int i = 0; i <= pp_response.Errors.Length - 1; i++)
                {
                    ErrorType ETtemp = (ErrorType)pp_response.Errors.GetValue(i);
                    Eventomatic_DB.SPs.UpdateCCErrors2(ETtemp.LongMessage.ToString(), Convert.ToInt32(Tx_Key), 0, paymentAmount, pp_response.CorrelationID, ETtemp.ErrorCode).Execute();
                }
            }
            else //doDirectPayment went through
            {
                string strAuthid = pp_response.TransactionID;

                // Create the request object.
                DoCaptureRequestType capt_Request = new DoCaptureRequestType();
                capt_Request.Version = "60.0";

                capt_Request.AuthorizationID = strAuthid;
                capt_Request.Amount = new BasicAmountType();
                capt_Request.Amount.currencyID = tempCurrency;
                capt_Request.Amount.Value = paymentAmount;

                
                //MAX 11 chars
                capt_Request.Descriptor = sitetemp.GetResourceDescriptorTx(Convert.ToInt32(Tx_Key)).ToUpper();

                
                capt_response = (DoCaptureResponseType)caller.Call("DoCapture", capt_Request);
                
            }

            Hashtable httemp = new Hashtable();
            httemp.Add("pp_response", pp_response);
            httemp.Add("capt_response", capt_response);

            return httemp;
        }
        #endregion 

        /*public DoDirectPaymentResponseType DoDirectPaymentCode_NVP(string paymentAmount, string buyerLastName, string buyerFirstName, string buyerAddress1, string buyerAddress2, string buyerCity, string buyerState, string buyerZipCode, string creditCardType, string creditCardNumber, string CVV2, int expMonth, int expYear, PaymentActionCodeType paymentAction,
            string buyerCountry, CountryCodeType buyerCountryCode, string Tx_Key, CurrencyCodeType tempCurrency, bool Live_Trial)
        {
            
            NVPCallerServices caller = new NVPCallerServices();
            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();
            

            // Set up your API credentials, PayPal end point, API operation and version.
            if (Live_Trial)//true = Live , false = trial
            {
                profile.Environment = "live";
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();

                Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(Convert.ToInt32(Tx_Key), ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString()).Execute();
            }
            else
            {
                profile.Environment = "sandbox";
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();

                Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(Convert.ToInt32(Tx_Key), System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString()).Execute();
            }
            //profile.Environment = "sandbox"; 
            profile.Subject = "perm@lornestar.com";


            caller.APIProfile = profile;

            NVPCodec encoder = new NVPCodec();
            encoder["VERSION"] = "51.0";
            encoder["METHOD"] = "DoDirectPayment";
            
            // Add request-specific fields to the request.
            encoder["PAYMENTACTION"] = paymentAction;
            encoder["AMT"] = paymentAmount;
            encoder["CREDITCARDTYPE"] = creditCardType;
            encoder["ACCT"] = creditCardNumber;
            encoder["EXPDATE"] = expMonth.ToString();
            encoder["CVV2"] = CVV2;
            encoder["FIRSTNAME"] = buyerFirstName;
            encoder["LASTNAME"] = buyerLastName;
            encoder["STREET"] = buyerAddress1;
            encoder["CITY"] = buyerCity;
            encoder["STATE"] = buyerState;
            encoder["ZIP"] = buyerZipCode;
            encoder["COUNTRYCODE"] = buyerCountryCode;
            encoder["CURRENCYCODE"] = tempCurrency;

            // Execute the API operation and obtain the response.
            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = caller.Call(pStrrequestforNvp);

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);
            return decoder["ACK"];
        }*/

        MassPayRequestType pp_request = new MassPayRequestType();
		MassPayRequestItemType MassItemReq=new MassPayRequestItemType();


        #region MassPay
        public string MassPayCode(string EmailSubject, ReceiverInfoCodeType receiverType, string[] ReceiverEmail, string[] value, string[] UniqueId, string[] note, CurrencyCodeType[] currencyId, int Count, int Tx_Key, bool Live_Trial)
		{
            com.paypal.sdk.services.CallerServices caller = new com.paypal.sdk.services.CallerServices();			

			IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();
			/*
			 WARNING: Do not embed plaintext credentials in your application code.
			 Doing so is insecure and against best practices.
			 Your API credentials must be handled securely. Please consider
			 encrypting them for use in any production environment, and ensure
			 that only authorized individuals may view or modify them.
			 */

            // Set up your API credentials, PayPal end point, and API version.
            if (Live_Trial)//true = Live , false = trial
            {
                profile.Environment = "live";//System.Configuration.ConfigurationSettings.AppSettings.Get("Live_Environment").ToString();
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
            }
            else
            {
                profile.Environment = "sandbox";
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }
			caller.APIProfile = profile;
			
			// Create the request object.
			pp_request.MassPayItem= new MassPayRequestItemType[Count];
			pp_request.Version="51.0";

            // Add request-specific fields to the request.
			MassPayResponseType pp_response=new MassPayResponseType();

			for (int i=0;i<Count;i++)
			{

				pp_request.MassPayItem[i]=new MassPayRequestItemType();

				pp_request.MassPayItem[i].ReceiverEmail=ReceiverEmail[i];
				pp_request.MassPayItem[i].Amount = new BasicAmountType();
				pp_request.MassPayItem[i].Amount.Value = value[i];
				pp_request.MassPayItem[i].Amount.currencyID=currencyId[i];
				pp_request.MassPayItem[i].UniqueId=UniqueId[i];
				pp_request.MassPayItem[i].Note=note[i];

			}
			
			pp_request.EmailSubject=EmailSubject;
			pp_request.ReceiverType=receiverType;//Enum for ReceiverType is ReceiverInfoCodeType.EmailAddress

            // Execute the API operation and obtain the response.
			pp_response= (MassPayResponseType) caller.Call("MassPay", pp_request);
            if (!pp_response.Ack.ToString().Contains("Success"))
            {
                for (int i = 0; i <= pp_response.Errors.Length - 1; i++)
                {
                    ErrorType ETtemp = (ErrorType)pp_response.Errors.GetValue(i);
                    Eventomatic_DB.SPs.UpdateCCErrors(ETtemp.LongMessage.ToString(), Tx_Key, 1,value[i]).Execute();
                }
            }
			return pp_response.Ack.ToString();

		}

        #endregion

        #region MassPay3
        public string MassPay3(int txkey)
        {            
            string strreturn = "";
            Site sitetemp = new Site();

            bool Live_Trial = !sitetemp.IsDemo_Payforward(txkey);

            string parameter;
            StringBuilder requestString = new StringBuilder();


            string strsubject = "&SUBJECT=";
            string strhostemail = sitetemp.HavePermission(txkey);
            strsubject += strhostemail;
            Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(txkey, strhostemail).Execute();

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";
            string ApplicationID = "";

            string strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
            if (!Live_Trial)
            {
                strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString();
            }

            com.paypal.sdk.services.CallerServices caller = new com.paypal.sdk.services.CallerServices();
            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();

            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString(); //"APP-3NS87937TF907760F";
                profile.Environment = "live";
            }
            else
            {
                APIurl = "https://api-3t.sandbox.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile.Environment = "sandbox";
            }

            profile.APIUsername = APIusername;
            profile.APIPassword = APIPassword;
            profile.APISignature = APISignature;
            caller.APIProfile = profile;

            // Build the method and credential portion of the SetExpressCheckout request string
            parameter = "METHOD=" + HttpUtility.UrlEncode("MassPay");
            requestString.Append(parameter);
            requestString.Append("&USER=" + APIusername);
            requestString.Append("&PWD=" + APIPassword);
            requestString.Append("&SIGNATURE=" + APISignature);
            requestString.Append("&VERSION=2.3");
            requestString.Append("&CURRENCYCODE=" + sitetemp.GetResourceCurrencyTx(txkey));
            
            string paymentamount = Math.Round(sitetemp.getServiceFee(txkey),2).ToString();

            requestString.Append("&RECEIVERTYPE=EmailAddress");
            requestString.Append("&L_EMAIL0=" + strServiceFeeEmail);//strServiceFeeEmail);
            requestString.Append("&L_AMT0=" + paymentamount);            
            requestString.Append("&L_NOTE0=SnappayServiceFee");

            if ((!strsubject.Contains("kingbbj3@hotmail.com")) && ((!strsubject.Contains("lorne@lornestar.com"))))
            {
                requestString.Append(strsubject);
            }            

            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {
                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";


                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                string txnid = "";
                for (int i = 0; i < pairs.Length; i++)
                {

                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    if (pair[0].ToLower() == "ack" && HttpUtility.UrlDecode(pair[1]).ToLower() != "failure")
                    {
                        success = true;
                    }
                    if (pair[0].ToLower() == "token")
                    {
                        token = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "payerid")
                    {
                        payerID = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "transactionid")
                    {
                        txnid = HttpUtility.UrlDecode(pair[1]);
                    }
                }
                if (success)
                {
                    strreturn = txnid;
                }
                else
                {
                    Eventomatic_DB.SPs.UpdateCCErrors2(pairs[7].ToString(), txkey, 0, paymentamount, pairs[1].ToString(), pairs[5].ToString()).Execute();
                }
            }
            catch (Exception ex)
            {
            }

            return strreturn;
        }
        #endregion

        #region PaymentDetails

        public GetTransactionDetailsResponseType GetTransactionDetailsCode(string trxID, bool Live_Trial)
        {
            com.paypal.sdk.services.CallerServices caller = new com.paypal.sdk.services.CallerServices();

            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();
            /*
             WARNING: Do not embed plaintext credentials in your application code.
             Doing so is insecure and against best practices.
             Your API credentials must be handled securely. Please consider
             encrypting them for use in any production environment, and ensure
             that only authorized individuals may view or modify them.
             */

            // Set up your API credentials, PayPal end point, and API version.
            if (Live_Trial)//true = Live , false = trial
            {
                profile.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Live_Environment").ToString();
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
            }
            else
            {
                profile.Environment = "sandbox";
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }
            caller.APIProfile = profile;

            // Create the request object.
            GetTransactionDetailsRequestType concreteRequest = new GetTransactionDetailsRequestType();
            concreteRequest.Version = "51.0";

            // Add request-specific fields to the request.
            concreteRequest.TransactionID = trxID.Trim();

            // Execute the API operation and obtain the response.
            GetTransactionDetailsResponseType pp_response = new GetTransactionDetailsResponseType();
            pp_response = (GetTransactionDetailsResponseType)caller.Call("GetTransactionDetails", concreteRequest);
            return pp_response;

        }

        #endregion

        #region Express Checkout
        //used for mobile
        public void DoExpressCheckout(int txkey, bool Live_Trial, decimal dcamount, int Event_Key, bool LandingPageBilling)
        {
            

            string parameter;
            StringBuilder requestString = new StringBuilder();

            Site sitetemp = new Site();
            string strsubject = "&SUBJECT=";
            if (Live_Trial)//true = Live , false = trial
            {
                strsubject += sitetemp.HavePermission(txkey);
            }
            else
            {
                strsubject += ConfigurationSettings.AppSettings.Get("3rdParty_Email_Trial").ToString();
            }
            if (Event_Key == 98)
            {
                strsubject = "&SUBJECT=kiteboarding@windyaddiction.com";
            }
            Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(txkey, strsubject.Replace("&SUBJECT=","")).Execute();

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";

            
            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp"; 
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
                
            }
            else
            {
                APIurl = "https://api.sandbox.paypal.com/nvp";//"https://api-3t.sandbox.paypal.com/nvp";//
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }

            APIurl = APIurl.Replace("svcs.", "");

            // Build the method and credential portion of the SetExpressCheckout request string
            parameter = "METHOD=" + HttpUtility.UrlEncode("SetExpressCheckout");
            requestString.Append(parameter);
            requestString.Append(SetupExpressCheckoutstring(Live_Trial));
            
            // Append the required parameters            
            parameter = "&PAYMENTREQUEST_0_AMT=" + Math.Round(dcamount,2).ToString();
            requestString.Append(parameter);

            parameter = "&PAYMENTREQUEST_0_CURRENCYCODE=" + sitetemp.GetResourceCurrencyTx(txkey);
            requestString.Append(parameter);            
            parameter = "&RETURNURL=" + ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/" + "mobileconfirm.aspx?Tx_Key=" + txkey.ToString();
            requestString.Append(parameter);
            parameter = "&CANCELURL=" + ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/" + "mobile.aspx?event=" + Event_Key.ToString();
            requestString.Append(parameter);            
            
            
            if (strsubject.Contains("@"))
            {
                requestString.Append(strsubject);
            }            

            //Other info
            parameter = "&SOLUTIONTYPE=Sole";
            requestString.Append(parameter);

            if (LandingPageBilling)
            {
                parameter = "&LANDINGPAGE=Billing";
                requestString.Append(parameter);
            }            
            parameter = "&NOSHIPPING=1";
            requestString.Append(parameter);

            parameter = "&BUTTONSOURCE=GroupStore_Cart_EC";
            requestString.Append(parameter);

            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {

                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = request.Length;

                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                for (int i = 0; i < pairs.Length; i++)
                {

                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    if (pair[0].ToLower() == "ack" && HttpUtility.UrlDecode(pair[1]).ToLower() != "failure")
                    {
                        success = true;
                    }
                    if (pair[0].ToLower() == "token")
                    {
                        token = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "payerid")
                    {
                        payerID = HttpUtility.UrlDecode(pair[1]);
                    }
                } 
                if (success)
                {
                    Eventomatic_DB.SPs.UpdateTransactionToken(txkey, token).Execute();

                    //HttpContext.Current.Session["OrderTotal"] = amountTextBox.Text;
                    if (Live_Trial)//true = Live , false = trial
                    {
                        HttpContext.Current.Response.Redirect("https://www.paypal.com/webscr?cmd=_express-checkout-mobile&useraction=commit&token=" + token);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("https://sandbox.paypal.com/webscr?cmd=_express-checkout-mobile&useraction=commit&token=" + token);
                    }                    
                }
                else
                {
                    //messageLabel.Text = "SetExpressCheckout Failed";
                }
            }
            catch (Exception ex)
            {
            }
        }

        # endregion

        #region Express Checkout Part 2

        public void DoExpressCheckout2(int txkey, bool Live_Trial)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetailsTxnid(txkey).GetDataSet();
            string Payerid = "";
            string amt = "";

            string token = "";
            if (dstemp.Tables[0].Rows[0]["token"] != DBNull.Value)
            {
                token = dstemp.Tables[0].Rows[0]["token"].ToString();
            }

            string parameter;
            StringBuilder requestString = new StringBuilder();

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";

            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
            }
            else
            {
                APIurl = "https://api.sandbox.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }

            APIurl = APIurl.Replace("svcs.", "");


            //GetExpress info first            
            requestString.Append("METHOD=" + HttpUtility.UrlEncode("GetExpressCheckoutDetails"));
            requestString.Append(SetupExpressCheckoutstring(Live_Trial));
            requestString.Append("&TOKEN=" + token);

            Site sitetemp = new Site();

            string strsubject = "&SUBJECT=";
            if (Live_Trial)//true = Live , false = trial
            {
                strsubject += sitetemp.HavePermission(txkey);
            }
            else
            {
                strsubject += ConfigurationSettings.AppSettings.Get("3rdParty_Email_Trial").ToString();
            }
            if (strsubject.Contains("@"))
            {
                requestString.Append(strsubject);
            }            

            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {

                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = request.Length;

                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                NVPCodec decoder = new NVPCodec();
                decoder.Decode(responseString);

                string strAck = decoder["ACK"].ToLower();
                if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
                {
                    Payerid = decoder["PAYERID"];
                    string stremail = decoder["EMAIL"];
                    string stramt = decoder["AMT"];

                    stremail = stremail.Replace("%40","@");
                    stremail = stremail.Replace("%2e",".");

                    stramt = stramt.Replace("%2e", ".");

                    string strfirstname = decoder["FIRSTNAME"];
                    string strlastname = decoder["LASTNAME"];

                    StringBuilder requestString2 = new StringBuilder();
                    

                    //Then confirm payment
                    requestString2.Append("METHOD=" + HttpUtility.UrlEncode("DoExpressCheckoutPayment"));
                    requestString2.Append(SetupExpressCheckoutstring(Live_Trial));
                    requestString2.Append("&PAYMENTACTION=Sale");
                    requestString2.Append("&PAYERID=" + Payerid);
                    requestString2.Append("&TOKEN=" + token);
                    requestString2.Append("&PAYMENTREQUEST_0_AMT=" + stramt);
                    requestString2.Append("&PAYMENTREQUEST_0_CURRENCYCODE=" + sitetemp.GetResourceCurrencyTx(txkey));
                    requestString2.Append("&OrderTotal=" + stramt);
                    requestString2.Append(strsubject);

                    if (strsubject.Contains("@"))
                    {
                        requestString.Append(strsubject);
                    }                     
                    
                    request = requestString2.ToString();                    
                    try
                    {

                        // Create request object
                        webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                        webRequest.Method = "POST";
                        webRequest.ContentType = "application/x-www-form-urlencoded";
                        webRequest.ContentLength = request.Length;

                        // Write the request string to the request object
                        writer = new StreamWriter(webRequest.GetRequestStream());
                        writer.Write(request);
                        writer.Close();

                        // Get the response from the request object and verify the status
                        webResponse = webRequest.GetResponse() as HttpWebResponse;
                        if (!webRequest.HaveResponse)
                        {
                            throw new Exception();
                        }
                        if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                        {
                            throw new Exception();
                        }

                        // Read the response string
                        reader = new StreamReader(webResponse.GetResponseStream());
                        responseString = reader.ReadToEnd();
                        reader.Close();
                    }
                    catch
                    {
                    }
                    
                    
                    decoder.Decode(responseString);

                    strAck = decoder["ACK"].ToLower();                                       
                    if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
                    {
                        Eventomatic_DB.SPs.UpdateTransactionExpressCheckoutEmail(txkey, stremail, strfirstname, strlastname).Execute();

                        //add in transaction id
                        try
                        {                                                        

                            string txnid = "";
                            if (decoder["PAYMENTREQUEST_0_TRANSACTIONID"] != null)
                            {
                                txnid = decoder["PAYMENTREQUEST_0_TRANSACTIONID"];
                            }
                            if (txnid != "")
                            {
                                Eventomatic_DB.SPs.UpdateTransactionTxnid(txkey, txnid).Execute();
                            }
                        }
                        catch
                        {
                        }
                        
                        
                        Send_Email SE = new Send_Email();
                        SE.Send_Transaction_Email(txkey, "");
                        //return true;                        
                    }
                    else
                    {
                        Eventomatic_DB.SPs.UpdateCCErrors(decoder["L_SHORTMESSAGE0"].ToString(), txkey, 3, stramt).Execute();
                    /*    retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                            "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                            "Desc2=" + decoder["L_LONGMESSAGE0"];*/                      
                    }
                }
                else
                    {
                        Eventomatic_DB.SPs.UpdateCCErrors(decoder["L_SHORTMESSAGE0"].ToString() + " requeststring = " + request, txkey, 3, "0.00").Execute();
                }
            }
            catch //error in getdetails
            {
            }                       
        }

        protected string SetupExpressCheckoutstring(bool Live_Trial)
        {
            StringBuilder requestString = new StringBuilder();
            string parameter;

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";            

            if (Live_Trial)//true = Live , false = trial
            {                
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
            }
            else
            {             
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }

            
            parameter = "&USER=" + HttpUtility.UrlEncode(APIusername);
            requestString.Append(parameter);
            parameter = "&PWD=" + HttpUtility.UrlEncode(APIPassword);
            requestString.Append(parameter);
            parameter = "&SIGNATURE=" + HttpUtility.UrlEncode(APISignature);
            requestString.Append(parameter);
            parameter = "&VERSION=" + HttpUtility.UrlEncode("63.0");
            requestString.Append(parameter);

            return requestString.ToString();
            
        }


        # endregion

        #region Express Checkout Native
        //used for mobile
        public string DoExpressCheckout_Native(int txkey, bool Live_Trial, decimal dcamount)
        {
            string strreturnurl = "";

            string parameter;
            StringBuilder requestString = new StringBuilder();

            Site sitetemp = new Site();
            string strsubject = "&SUBJECT=";
            if (Live_Trial)//true = Live , false = trial
            {
                strsubject += sitetemp.HavePermission(txkey);
            }
            else
            {
                strsubject += ConfigurationSettings.AppSettings.Get("3rdParty_Email_Trial").ToString();
            }
            
            Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(txkey, strsubject.Replace("&SUBJECT=", "")).Execute();

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";


            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();

            }
            else
            {
                APIurl = "https://api-3t.sandbox.paypal.com/nvp";//"https://api-3t.sandbox.paypal.com/nvp";//
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }

            APIurl = APIurl.Replace("svcs.", "");

            // Build the method and credential portion of the SetExpressCheckout request string
            parameter = "METHOD=" + HttpUtility.UrlEncode("SetExpressCheckout");
            requestString.Append(parameter);
            requestString.Append(SetupExpressCheckoutstring(Live_Trial));

            // Append the required parameters            
            parameter = "&PAYMENTREQUEST_0_AMT=" + Math.Round(dcamount, 2).ToString();
            requestString.Append(parameter);
            string strCurrency = sitetemp.GetResourceCurrencyTx(txkey);
            parameter = "&PAYMENTREQUEST_0_CURRENCYCODE=" + strCurrency;

            requestString.Append(parameter);
            parameter = "&RETURNURL=" + ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/" + "mobileconfirm_Native.aspx?Tx_Key=" + txkey.ToString();
            requestString.Append(parameter);
            parameter = "&CANCELURL=" + ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/" + "mobileconfirm_Native.aspx?cancel=true";
            requestString.Append(parameter);


            if (strsubject.Contains("@"))
            {
                if ((!strsubject.Contains("kingbbj3@hotmail.com")) && ((!strsubject.Contains("lorne@lornestar.com"))))
                {
                    requestString.Append(strsubject);
                }                
            }

            //Other info
            parameter = "&SOLUTIONTYPE=Sole";
            requestString.Append(parameter);

            parameter = "&LANDINGPAGE=Billing";
            requestString.Append(parameter);
            
            parameter = "&NOSHIPPING=1";
            requestString.Append(parameter);

            parameter = "&BUTTONSOURCE=GroupStore_Cart_EC";
            requestString.Append(parameter);

            if (strCurrency != "USD")
            {
                string strlocal = "CA";
                if ((strCurrency == "EUR") || (strCurrency == "GBP"))
                {
                    strlocal = "GB";
                }
                else if (strCurrency == "ILS")
                {
                    strlocal = "he_IL";
                }

                requestString.Append("&LOCALECODE=" + strlocal);
            }
            

            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {

                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = request.Length;

                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                for (int i = 0; i < pairs.Length; i++)
                {

                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    if (pair[0].ToLower() == "ack" && HttpUtility.UrlDecode(pair[1]).ToLower() != "failure")
                    {
                        success = true;
                    }
                    if (pair[0].ToLower() == "token")
                    {
                        token = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "payerid")
                    {
                        payerID = HttpUtility.UrlDecode(pair[1]);
                    }
                }
                if (success)
                {
                    Eventomatic_DB.SPs.UpdateTransactionToken(txkey, token).Execute();

                    //HttpContext.Current.Session["OrderTotal"] = amountTextBox.Text;
                    if (Live_Trial)//true = Live , false = trial
                    {
                        strreturnurl = "https://www.paypal.com/webscr?cmd=_express-checkout-mobile&useraction=commit&token=" + token;
                        if ((sitetemp.HavePermission(txkey) == "") || (strsubject.ToLower().Contains("lorne@lornestar.com")))
                        {
                            strreturnurl = "https://www.paypal.com/webscr?cmd=_express-checkout-mobile&useraction=commit&token=";
                        }
                    }
                    else
                    {
                        strreturnurl = "https://sandbox.paypal.com/webscr?cmd=_express-checkout-mobile&useraction=commit&token=" + token;
                    }
                }
                else
                {
                    Eventomatic_DB.SPs.UpdateCCErrors2(pairs[7].ToString(), txkey, 0, dcamount.ToString(), pairs[1].ToString(), pairs[5].ToString()).Execute();
                    //messageLabel.Text = "SetExpressCheckout Failed";
                }
            }
            catch (Exception ex)
            {
            }

            return strreturnurl;
        }

        # endregion

        #region Signup Reference Tx Billing Agreement
        //used for mobile
        public void Billing_Agreement(int Resource_Key, bool Live_Trial)
        {


            string parameter;
            StringBuilder requestString = new StringBuilder();

            Site sitetemp = new Site();
            
            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";


            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();

            }
            else
            {
                APIurl = "https://api.sandbox.paypal.com/nvp";//"https://api-3t.sandbox.paypal.com/nvp";//
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }

            APIurl = APIurl.Replace("svcs.", "");

            // Build the method and credential portion of the SetExpressCheckout request string
            parameter = "METHOD=" + HttpUtility.UrlEncode("SetExpressCheckout");
            requestString.Append(parameter);
            requestString.Append(SetupExpressCheckoutstring(Live_Trial));

            // Append the required parameters            
            //parameter = "&PAYMENTREQUEST_0_AMT=" + Math.Round(dcamount, 2).ToString();
            //requestString.Append(parameter);

            //parameter = "&PAYMENTREQUEST_0_CURRENCYCODE=" + sitetemp.GetResourceCurrencyTx(txkey);
            //requestString.Append(parameter);
            requestString.Append("&VERSION=64.0"); 
            requestString.Append("&PAYMENTACTION=Authorization");
            requestString.Append("&AMT=0");
            requestString.Append("&DESC=Snappay Service Fees");
            requestString.Append("&L_BILLINGTYPE0=MerchantInitiatedBilling");
            parameter = "&RETURNURL=" + ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/" + "payforward_firsttime.aspx?resource_key=" + Resource_Key.ToString() + "beginselling";
            requestString.Append(parameter);
            parameter = "&CANCELURL=" + ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/" + "payforward_firsttime.aspx?resource_key=" + Resource_Key.ToString() + "billingagreement";
            requestString.Append(parameter);

            
            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {

                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = request.Length;

                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                for (int i = 0; i < pairs.Length; i++)
                {

                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    if (pair[0].ToLower() == "ack" && HttpUtility.UrlDecode(pair[1]).ToLower() != "failure")
                    {
                        success = true;
                    }
                    if (pair[0].ToLower() == "token")
                    {
                        token = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "payerid")
                    {
                        payerID = HttpUtility.UrlDecode(pair[1]);
                    }
                }
                if (success)
                {                    
                    Eventomatic_DB.SPs.UpdateBillingAgreement(Resource_Key, token, false, "", 0,"").Execute();

                    //HttpContext.Current.Session["OrderTotal"] = amountTextBox.Text;
                    if (Live_Trial)//true = Live , false = trial
                    {
                        HttpContext.Current.Response.Redirect("https://www.paypal.com/webscr?cmd=_express-checkout-mobile&useraction=commit&token=" + token);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("https://sandbox.paypal.com/webscr?cmd=_express-checkout-mobile&useraction=commit&token=" + token);
                    }
                }
                else
                {
                    //messageLabel.Text = "SetExpressCheckout Failed";
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Billing_Agreement2(string token, bool Live_Trial)
        {
            string Payerid = "";
            StringBuilder requestString = new StringBuilder();

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";

            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
            }
            else
            {
                APIurl = "https://api.sandbox.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }

            APIurl = APIurl.Replace("svcs.", "");


            //GetExpress info first            
            requestString.Append("METHOD=" + HttpUtility.UrlEncode("GetExpressCheckoutDetails"));
            requestString.Append(SetupExpressCheckoutstring(Live_Trial));
            requestString.Append("&TOKEN=" + token);

            Site sitetemp = new Site();

            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {

                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = request.Length;

                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                NVPCodec decoder = new NVPCodec();
                decoder.Decode(responseString);

                string strAck = decoder["ACK"].ToLower();
                if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
                {
                    Payerid = HttpUtility.UrlDecode(decoder["PAYERID"]);
                    string stremail = HttpUtility.UrlDecode(decoder["EMAIL"]);
                    string stramt = HttpUtility.UrlDecode(decoder["AMT"]);

                    /*stremail = stremail.Replace("%40", "@");
                    stremail = stremail.Replace("%2e", ".");

                    stramt = stramt.Replace("%2e", ".");
                    */
                    string strfirstname = HttpUtility.UrlDecode(decoder["FIRSTNAME"]);
                    string strlastname = HttpUtility.UrlDecode(decoder["LASTNAME"]);

                    StringBuilder requestString2 = new StringBuilder();


                    //Then confirm payment
                    requestString2.Append("METHOD=" + HttpUtility.UrlEncode("CreateBillingAgreement"));
                    requestString2.Append(SetupExpressCheckoutstring(Live_Trial));
                    //requestString.Append("&AMT=0");
                    //requestString.Append("&L_BILLINGAGREEMENTDESCRIPTION0=Snappay Service Fees");
                    //requestString.Append("&L_BILLINGTYPE0=RecurringPayments");
                    //requestString2.Append("&PAYERID=" + Payerid);
                    requestString2.Append("&TOKEN=" + token);

                    request = requestString2.ToString();
                    try
                    {

                        // Create request object
                        webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                        webRequest.Method = "POST";
                        webRequest.ContentType = "application/x-www-form-urlencoded";
                        webRequest.ContentLength = request.Length;

                        // Write the request string to the request object
                        writer = new StreamWriter(webRequest.GetRequestStream());
                        writer.Write(request);
                        writer.Close();

                        // Get the response from the request object and verify the status
                        webResponse = webRequest.GetResponse() as HttpWebResponse;
                        if (!webRequest.HaveResponse)
                        {
                            throw new Exception();
                        }
                        if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                        {
                            throw new Exception();
                        }

                        // Read the response string
                        reader = new StreamReader(webResponse.GetResponseStream());
                        responseString = reader.ReadToEnd();
                        reader.Close();
                    }
                    catch
                    {
                    }


                    decoder.Decode(responseString);

                    strAck = decoder["ACK"].ToLower();
                    if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
                    {
                        Eventomatic_DB.SPs.UpdateBillingAgreement(0, token, true, decoder["BILLINGAGREEMENTID"].ToString().Replace("%2d", "-"), 1,stremail).Execute();
                        //return true;

                    }
                    else
                    {
                        Eventomatic_DB.SPs.UpdateCCErrors(decoder["L_SHORTMESSAGE0"].ToString(), 0, 4, token).Execute();
                           string retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                                "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                                "Desc2=" + decoder["L_LONGMESSAGE0"];
                    }
                }
                else
                {
                    Eventomatic_DB.SPs.UpdateCCErrors(decoder["L_SHORTMESSAGE0"].ToString() + " requeststring = " + request, 0, 4, token).Execute();
                }
            }
            catch //error in getdetails
            {
            }              
        }

        # endregion

        #region DoDirectPayment2

        public string DoDirectPayment2(string paymentAmount, string buyerLastName, string buyerFirstName, string buyerAddress1, string buyerAddress2, string buyerCity, string buyerState, string buyerZipCode, string creditCardType, string creditCardNumber, string CVV2, int expMonth, int expYear, PaymentActionCodeType paymentAction,
            string buyerCountry, CountryCodeType buyerCountryCode, int txkey, CurrencyCodeType tempCurrency, bool Live_Trial)
        {

            string strreturn = "";
            Site sitetemp = new Site();
            string parameter;
            StringBuilder requestString = new StringBuilder();

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";
            string ApplicationID = "";
            string HostEmail = sitetemp.HavePermission(txkey);

            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString(); //"APP-3NS87937TF907760F";

                Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(txkey, HostEmail).Execute();
            }
            else
            {
                APIurl = "https://api.sandbox.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();

                Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(txkey, ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString()).Execute();
            }            

            // Build the method and credential portion of the SetExpressCheckout request string
            parameter = "METHOD=" + HttpUtility.UrlEncode("DoDirectPayment");
            requestString.Append(parameter);            

            requestString.Append("&VERSION=51.0");
            requestString.Append("&AMT=" + paymentAmount);
            requestString.Append("&CREDITCARDTYPE=" + creditCardType);
            requestString.Append("&ACCT=" + creditCardNumber);
            requestString.Append("&EXPDATE=" + expMonth.ToString() + expYear.ToString());
            requestString.Append("&CVV2=" + CVV2);
            requestString.Append("&FIRSTNAME=" + buyerFirstName);
            requestString.Append("&LASTNAME=" + buyerLastName);
            requestString.Append("&STREET=" + buyerAddress1);
            requestString.Append("&CITY=" + buyerCity);
            requestString.Append("&STATE=" + buyerState);
            requestString.Append("&ZIP=" + buyerZipCode);
            requestString.Append("&COUNTRYCODE=" + buyerCountryCode.ToString());
            requestString.Append("&CURRENCYCODE=" + tempCurrency.ToString());
            requestString.Append("&BUTTONSOURCE=GroupStore_Cart_DP");
            requestString.Append("&PAYMENTACTION=Sale");
            requestString.Append("&InvoiceID=" + txkey.ToString());
            

            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;            
            try
            {

                
                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                string strtempheader = sitetemp.GetPermissionHeader(Convert.ToInt32(sitetemp.GetResourceKeytxKey(txkey)), Live_Trial, APIurl);
                webRequest.Headers.Add("X-PAYPAL-AUTHORIZATION", strtempheader);                
                webRequest.Headers.Add("X-PAYPAL-REQUEST-DATA-FORMAT", "NV");
                webRequest.Headers.Add("X-PAYPAL-RESPONSE-DATA-FORMAT", "NV");
                //webRequest.Headers.Add("X-PAYPAL-SECURITY-USERID",APIusername);
                //webRequest.Headers.Add("X-PAYPAL-SECURITY-PASSWORD", APIPassword);
                //webRequest.Headers.Add("X-PAYPAL-SECURITY-SIGNATURE", APISignature);
                webRequest.Headers.Add("X-PAYPAL-APPLICATION-ID", "APP-3NS87937TF907760F");// ApplicationID);
                

                
                webRequest.ContentLength = request.Length;                

                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                
                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                string txnid = "";
                for (int i = 0; i < pairs.Length; i++)
                {

                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    if (pair[0].ToLower() == "ack" && HttpUtility.UrlDecode(pair[1]).ToLower() != "failure")
                    {
                        success = true;
                    }
                    if (pair[0].ToLower() == "token")
                    {
                        token = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "payerid")
                    {
                        payerID = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "transactionid")
                    {
                        txnid = HttpUtility.UrlDecode(pair[1]);
                    }
                }
                if (success)
                {
                    strreturn = txnid;
                    /*Eventomatic_DB.SPs.UpdateTransactionToken(txkey, token).Execute();

                    //HttpContext.Current.Session["OrderTotal"] = amountTextBox.Text;
                    if (Live_Trial)//true = Live , false = trial
                    {
                        HttpContext.Current.Response.Redirect("https://www.paypal.com/webscr?cmd=_express-checkout-mobile&useraction=commit&token=" + token);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("https://sandbox.paypal.com/webscr?cmd=_express-checkout-mobile&useraction=commit&token=" + token);
                    }*/
                }
                else
                {
                    //messageLabel.Text = "SetExpressCheckout Failed";
                    //Eventomatic_DB.SPs.UpdateCCErrors(pairs[7].ToString(), txkey, 3, paymentAmount).Execute();


                    Eventomatic_DB.SPs.UpdateCCErrors2(pairs[7].ToString(), txkey, 0, paymentAmount, pairs[1].ToString(), pairs[5].ToString()).Execute();
                }
            }
            catch (Exception ex)
            {
            }

            //DoDirectPaymentRequestType pp_Request = new DoDirectPaymentRequestType();

            return strreturn;
        }

        # endregion

        #region DoDirectPayment3
        public string DoDirectPayment3(string paymentAmount, string buyerLastName, string buyerFirstName, string buyerAddress1, string buyerAddress2, string buyerCity, string buyerState, string buyerZipCode, string creditCardType, string creditCardNumber, string CVV2, int expMonth, int expYear, PaymentActionCodeType paymentAction,
            string buyerCountry, CountryCodeType buyerCountryCode, int txkey, CurrencyCodeType tempCurrency, bool Live_Trial)
        {
            string strreturn = "";
            Site sitetemp = new Site();

            string parameter;
            StringBuilder requestString = new StringBuilder();

            
            string strsubject = "&SUBJECT=";
            string strhostemail = sitetemp.HavePermission(txkey);
            strsubject += strhostemail;            
            Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(txkey, strhostemail).Execute();

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";
            string ApplicationID = "";

            com.paypal.sdk.services.CallerServices caller = new com.paypal.sdk.services.CallerServices();
            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();

            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString(); //"APP-3NS87937TF907760F";
                profile.Environment = "live";
            }
            else
            {
                APIurl = "https://api.sandbox.paypal.com/nvp";//"https://api-3t.sandbox.paypal.com/nvp";//
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile.Environment = "sandbox";
            }

            profile.APIUsername = APIusername;
            profile.APIPassword = APIPassword;
            profile.APISignature = APISignature;            
            caller.APIProfile = profile;

            // Build the method and credential portion of the SetExpressCheckout request string
            parameter = "METHOD=" + HttpUtility.UrlEncode("DoDirectPayment");
            requestString.Append(parameter);

            if (expYear.ToString().Length == 2)
            {
                expYear += 2000;
            }

            requestString.Append("&VERSION=51.0");
            requestString.Append("&AMT=" + paymentAmount);
            requestString.Append("&CREDITCARDTYPE=" + creditCardType);
            requestString.Append("&ACCT=" + creditCardNumber);
            requestString.Append("&EXPDATE=" + expMonth.ToString() + expYear.ToString());
            requestString.Append("&CVV2=" + CVV2);
            requestString.Append("&FIRSTNAME=" + buyerFirstName);
            requestString.Append("&LASTNAME=" + buyerLastName);
            requestString.Append("&STREET=" + buyerAddress1);
            requestString.Append("&CITY=" + buyerCity);
            requestString.Append("&STATE=" + buyerState);
            requestString.Append("&ZIP=" + buyerZipCode);
            requestString.Append("&COUNTRYCODE=" + buyerCountryCode.ToString());
            requestString.Append("&CURRENCYCODE=" + tempCurrency.ToString());
            requestString.Append("&BUTTONSOURCE=GroupStore_Cart_DP");
            requestString.Append("&PAYMENTACTION=Sale");
            requestString.Append("&InvoiceID=" + txkey.ToString());
            requestString.Append(strsubject);
            requestString.Append("&USER=" + APIusername);
            requestString.Append("&PWD=" + APIPassword);
            requestString.Append("&SIGNATURE=" + APISignature);
            if (HttpContext.Current.Request.UserHostAddress.Length < 5)
            {
                //local, so make up ip address
                requestString.Append("&IPADDRESS=192.168.0.0");
            }
            else
            {
                requestString.Append("&IPADDRESS=" + HttpContext.Current.Request.UserHostAddress);
            }
            



            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {
                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";                


                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                string txnid = "";
                string strerror = "";
                for (int i = 0; i < pairs.Length; i++)
                {

                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    if (pair[0].ToLower() == "ack" && HttpUtility.UrlDecode(pair[1]).ToLower() != "failure")
                    {
                        success = true;
                    }
                    if (pair[0].ToLower() == "token")
                    {
                        token = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "payerid")
                    {
                        payerID = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "transactionid")
                    {
                        txnid = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower().Contains("l_longmessage"))
                    {
                        strerror = HttpUtility.UrlDecode(pair[1]);
                    }
                }
                if (success)
                {
                    strreturn = txnid;                    
                }
                else
                {
                    strreturn = strerror + "snerror";
                    Eventomatic_DB.SPs.UpdateCCErrors2(pairs[7].ToString(), txkey, 0, paymentAmount, pairs[1].ToString(), pairs[5].ToString()).Execute();
                }
            }
            catch (Exception ex)
            {
            }

            return strreturn;
        }
        #endregion

        #region AdaptiveAccounts

        public string CreateBizAccount(bool Live_Trial, string accounttype, string salutation, string firstname, string middlename, string lastname,
            DateTime dateofbirth, string address1, string address2, string city, string state, string postalcode, string countrycode, string citizenship,
            string phonenumber, string email, string currencycode, string returnURL,string bizname, string bizaddress1, string bizaddress2, 
            string bizcity, string bizstate, string bizpostalcode, string bizcountrycode, string bizphone, string bizcategory, string bizsubcategory, string bizserviceemail, 
            string bizservicephone, string bizwebsite, DateTime bizdateestablishment, string biztype, decimal bizavgprice, decimal bizavgmonthlyvolume,
            string bizsalesvenue, string percentonlinesales, string salesvenuedesc)
        {
            string strreturn = "";

            PayPal.Platform.SDK.BaseAPIProfile profile = new PayPal.Platform.SDK.BaseAPIProfile();            
            
            Site sitetemp = new Site();

            profile.APIProfileType = ProfileType.ThreeToken;

            if (Live_Trial)//true = Live , false = trial
            {
                profile.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Live_Environment").ToString();
                profile.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString();
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
            }
            else
            {
                profile.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Trial_Environment").ToString();
                profile.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
                profile.SandboxMailAddress = "lorne@lornestar.com";
            }

            profile.IsTrustAllCertificates = Convert.ToBoolean(ConfigurationManager.AppSettings["TrustAll"]);
            profile.RequestDataformat = "SOAP11";
            profile.ResponseDataformat = "SOAP11";
            profile.DeviceIpAddress = HttpContext.Current.Request.UserHostAddress;            
            
            CreateAccountRequest createAccountRequest = null;
            PayPal.Platform.SDK.AdaptiveAccounts aa = new PayPal.Platform.SDK.AdaptiveAccounts();
            aa.APIProfile = profile;
            
            try{
                createAccountRequest = new CreateAccountRequest();
                createAccountRequest.accountType = accounttype;
                createAccountRequest.name = new NameType();
                createAccountRequest.name.salutation = salutation;
                createAccountRequest.name.firstName = firstname;
                createAccountRequest.name.middleName = middlename;
                createAccountRequest.name.lastName = lastname;
                createAccountRequest.dateOfBirth = dateofbirth;
                createAccountRequest.address = new PayPal.Services.Private.AA.AddressType();
                createAccountRequest.address.line1 = address1;
                if (address2 != "")
                {
                    createAccountRequest.address.line2 = address2;
                }                
                createAccountRequest.address.city = city;
                createAccountRequest.address.state = state;
                createAccountRequest.address.postalCode = postalcode;
                createAccountRequest.address.countryCode = countrycode;
                createAccountRequest.citizenshipCountryCode = citizenship;
                createAccountRequest.currencyCode = currencycode;
                createAccountRequest.contactPhoneNumber = phonenumber;
                createAccountRequest.preferredLanguageCode = "en_US";
                
                //cretaeAccountRequest.sandboxEmailAddress = sandboxDeveloperEmail.Value;                
                createAccountRequest.emailAddress = email;
                                
                createAccountRequest.createAccountWebOptions = new CreateAccountWebOptionsType();
                createAccountRequest.createAccountWebOptions.returnUrl = returnURL;
                createAccountRequest.registrationType = "WEB";
                
                 

                ////Business Info
                createAccountRequest.businessInfo = new PayPal.Services.Private.AA.BusinessInfoType();
                createAccountRequest.businessInfo.businessName = bizname;
                createAccountRequest.businessInfo.businessAddress = new PayPal.Services.Private.AA.AddressType();
                createAccountRequest.businessInfo.businessAddress.line1 = bizaddress1;
                if (bizaddress2 != "")
                {
                    createAccountRequest.businessInfo.businessAddress.line2 = bizaddress2;
                }                
                createAccountRequest.businessInfo.businessAddress.city = bizcity;
                createAccountRequest.businessInfo.businessAddress.state = bizstate;
                createAccountRequest.businessInfo.businessAddress.postalCode = bizpostalcode;
                createAccountRequest.businessInfo.businessAddress.countryCode = bizcountrycode;
                createAccountRequest.businessInfo.workPhone = bizphone;
                createAccountRequest.businessInfo.category = bizcategory;
                createAccountRequest.businessInfo.subCategory = bizsubcategory;
                if (bizservicephone != "")
                {
                    createAccountRequest.businessInfo.customerServicePhone = bizservicephone;
                }
                createAccountRequest.businessInfo.customerServiceEmail = bizserviceemail;                
                if (bizwebsite != "")
                {
                    createAccountRequest.businessInfo.webSite = bizwebsite;
                }                
                createAccountRequest.businessInfo.dateOfEstablishment = bizdateestablishment;
                createAccountRequest.businessInfo.dateOfEstablishmentSpecified = true;

                createAccountRequest.businessInfo.businessType = (BusinessType)Enum.Parse(typeof(BusinessType), biztype , true);
                createAccountRequest.businessInfo.businessTypeSpecified = true;
                createAccountRequest.businessInfo.averagePrice = bizavgprice;
                createAccountRequest.businessInfo.averagePriceSpecified = true;
                createAccountRequest.businessInfo.averageMonthlyVolume = bizavgmonthlyvolume;
                createAccountRequest.businessInfo.averageMonthlyVolumeSpecified = true;
                createAccountRequest.businessInfo.percentageRevenueFromOnline = percentonlinesales;
                createAccountRequest.businessInfo.salesVenue = new PayPal.Services.Private.AA.SalesVenueType[1];
                createAccountRequest.businessInfo.salesVenue[0] = new PayPal.Services.Private.AA.SalesVenueType();
                createAccountRequest.businessInfo.salesVenue[0] = (PayPal.Services.Private.AA.SalesVenueType)Enum.Parse(typeof(PayPal.Services.Private.AA.SalesVenueType), bizsalesvenue, true);
                if (salesvenuedesc != "")
                {
                    createAccountRequest.businessInfo.salesVenueDesc = salesvenuedesc;
                }                
                ////
                
                CreateAccountResponse CAResponse = aa.CreateAccount(createAccountRequest);
				
				if (aa.isSuccess == "FAILURE")
                {
                    //HttpContext.Current.Session[Constants.SessionConstants.FAULT] = aa.LastError;
                    //HttpContext.Current.Response.Redirect("APIError.aspx", false);
                }
                
				else
				{
                    //Session[Constants.SessionConstants.CREATEACCOUNTRESPONSE] = CAResponse;
                    HttpContext.Current.Response.Redirect(CAResponse.redirectURL, false);
					
				}


			}
			catch(FATALException FATALEx)
			{
				
			}
			catch(Exception ex)
			{
				FATALException FATALEx = new FATALException("Error occurred in CreateAccount Page.", ex); 
				
			}

            return strreturn;
        }

        #endregion

        #region GetPersonalData

        public Hashtable GetPersonalData(int Resource_Key, bool Live_Trial)
        {
            Hashtable hsreturn = new Hashtable();

            Site sitetemp = new Site();

            string parameter;
            StringBuilder requestString = new StringBuilder();


            string strsubject = "&SUBJECT=mbaafinancedirector@mcmaster.ca";
            string strhostemail = sitetemp.GetResourceEmail(Resource_Key);
            strsubject += strhostemail;            

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";
            string ApplicationID = "";

            com.paypal.sdk.services.CallerServices caller = new com.paypal.sdk.services.CallerServices();
            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();

            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://svcs.paypal.com/Permissions/GetBasicPersonalData";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString(); //"APP-3NS87937TF907760F";
                profile.Environment = "live";
            }
            else
            {
                APIurl = "https://svcs.sandbox.paypal.com/Permissions/GetBasicPersonalData";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile.Environment = "sandbox";
            }

            
            profile.APIUsername = APIusername;
            profile.APIPassword = APIPassword;
            profile.APISignature = APISignature;
            profile.Subject = strsubject;
            caller.APIProfile = profile;            

            // Build the method and credential portion of the SetExpressCheckout request string                        
            requestString.Append("attributeList.attribute(0)=http://axschema.org/namePerson/first");
            requestString.Append("&attributeList.attribute(1)=http://axschema.org/namePerson/last");
            requestString.Append("&attributeList.attribute(2)=http://axschema.org/contact/email");
            requestString.Append("&attributeList.attribute(3)=http://schema.openid.net/contact/fullname");
            requestString.Append("&attributeList.attribute(4)=http://openid.net/schema/company/name");
            requestString.Append("&attributeList.attribute(5)=http://axschema.org/contact/country/home");
            requestString.Append("&attributeList.attribute(6)=https://www.paypal.com/webapps/auth/schema/payerID");
            /*requestString.Append("&attributeList.attribute(7)=http://axschema.org/birthDate");
            requestString.Append("&attributeList.attribute(8)=http://axschema.org/contact/postalCode/home");
            requestString.Append("&attributeList.attribute(9)=http://schema.openid.net/contact/street1");
            requestString.Append("&attributeList.attribute(10)=http://schema.openid.net/contact/street2");
            requestString.Append("&attributeList.attribute(11)=http://axschema.org/contact/city/home");
            requestString.Append("&attributeList.attribute(12)=http://axschema.org/contact/phone/default");
            */
            //requestString.Append(strsubject);
            requestString.Append("&requestEnvelope.errorLanguage=en_US");            
            
            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {
                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                string strtempheader = sitetemp.GetPermissionHeader(Resource_Key, Live_Trial,APIurl);
                //strtempheader = "timestamp=1323738462,signature=5xG6bpNwaz2p45gIIk62Cts5y5o=,token=q4XHJ65MnCZO5qVcxkH29I53c37At784SPa3heimmj94Mo893ppNpg";
                webRequest.Headers.Add("X-PAYPAL-AUTHORIZATION", strtempheader);                
                webRequest.Headers.Add("X-PAYPAL-REQUEST-DATA-FORMAT", "NV");
                webRequest.Headers.Add("X-PAYPAL-RESPONSE-DATA-FORMAT", "NV");
                //webRequest.Headers.Add("X-PAYPAL-SECURITY-USERID",APIusername);
               // webRequest.Headers.Add("X-PAYPAL-SECURITY-PASSWORD", APIPassword);
               // webRequest.Headers.Add("X-PAYPAL-SECURITY-SIGNATURE", APISignature);
                webRequest.Headers.Add("X-PAYPAL-APPLICATION-ID", ApplicationID);
                


                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                string txnid = "";
                for (int i = 0; i < pairs.Length; i++)
                {                    
                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    hsreturn.Add(pair[0].ToString(), pair[1].ToString());
                    if (pair[0].ToLower() == "responseEnvelope.ack")
                    {
                        success = true;
                        hsreturn.Add("Success",HttpUtility.UrlDecode(pair[1]));
                    }
                    if (pair[0].ToLower() == "token")
                    {
                        token = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "payerid")
                    {
                        payerID = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "transactionid")
                    {
                        txnid = HttpUtility.UrlDecode(pair[1]);
                    }
                }
                if (success)
                {
                    
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
            }


            return hsreturn;
        }

        #endregion

        #region GetPersonalData_Parse

        public Hashtable GetPersonalData_Parse(int Resource_Key, bool Live_Trial)
        {
            Hashtable hsreturn = new Hashtable();

            Site sitetemp = new Site();

            string strnumtemp = "";
            

            Hashtable hstemp = GetPersonalData(Resource_Key,Live_Trial);
            if (hstemp.Count < 15)
            {
                System.Threading.Thread.Sleep(8000);
                hstemp = GetPersonalData(Resource_Key, Live_Trial);
            }
            if (hstemp.Count < 15)
            {
                System.Threading.Thread.Sleep(8000);
                hstemp = GetPersonalData(Resource_Key, Live_Trial);
            }
            if (hstemp.Count < 15)
            {
                System.Threading.Thread.Sleep(4000);
                hstemp = GetPersonalData(Resource_Key, Live_Trial);
            }
            if (hstemp.Count < 15)
            {
                System.Threading.Thread.Sleep(4000);
                hstemp = GetPersonalData(Resource_Key, Live_Trial);
            }
            if (hstemp.Count < 15)
            {
                System.Threading.Thread.Sleep(4000);
                hstemp = GetPersonalData(Resource_Key, Live_Trial);
            }
            if (hstemp.Count < 15)
            {
                System.Threading.Thread.Sleep(4000);
                hstemp = GetPersonalData(Resource_Key, Live_Trial);
            }
            if (hstemp.Count < 15)
            {
                System.Threading.Thread.Sleep(4000);
                hstemp = GetPersonalData(Resource_Key, Live_Trial);
            }
            foreach (DictionaryEntry Item in hstemp)
            {
                //lblresult.Text += Item.Key.ToString() + ":" + Item.Value.ToString() + " <br/> ";
                if (Item.Value.ToString().Contains("email"))
                {
                    hsreturn.Add("email", helpparse(Item, hstemp, "email"));
                }
                if (Item.Value.ToString().Contains("first"))
                {
                    hsreturn.Add("first", helpparse(Item, hstemp, "first"));
                }
                if (Item.Value.ToString().Contains("last"))
                {
                    hsreturn.Add("last", helpparse(Item, hstemp, "last"));
                }
                if (Item.Value.ToString().ToLower().Contains("payerid"))
                {
                    hsreturn.Add("payerid", helpparse(Item, hstemp, "payerid"));
                }

                if (Item.Value.ToString().Contains("country"))
                {
                    strnumtemp = Item.Key.ToString().Replace("response.personalData(", "").Replace(").personalDataKey", "");
                    foreach (DictionaryEntry Item2 in hstemp)
                    {
                        if (Item2.Key.ToString().Contains(strnumtemp + ").personalDataValue"))
                        {
                            string strcountry = cleandata(Item2.Value.ToString());
                            hsreturn.Add("country",strcountry);
                            hsreturn.Add("currency",sitetemp.GetCurrencyFromCountry(strcountry));
                            break;
                        }
                    }
                }
            }

            Eventomatic_DB.SPs.UpdatePayPalInfo(Resource_Key, hsreturn["email"].ToString(), true, hsreturn["first"].ToString(), hsreturn["last"].ToString(), hsreturn["country"].ToString(), hsreturn["payerid"].ToString()).Execute();
            
            Eventomatic_DB.SPs.UpdatePayPalInfoVerified(Resource_Key,GetVerified(Resource_Key, Live_Trial)).Execute();

            return hsreturn;
        }

        private string helpparse(DictionaryEntry Item, Hashtable hstemp, string strtype)
        {
            string strreturn = "";
            string strnumtemp = "";

            
                strnumtemp = Item.Key.ToString().Replace("response.personalData(", "").Replace(").personalDataKey", "");
                foreach (DictionaryEntry Item2 in hstemp)
                {
                    if (Item2.Key.ToString().Contains(strnumtemp + ").personalDataValue"))
                    {
                        strreturn = cleandata(Item2.Value.ToString());
                        break;
                    }
                }
            

            return strreturn;
        }

        private string cleandata(string strtemp)
        {
            string strreturn = strtemp.Replace("%5F", "_");
            strreturn = strreturn.Replace("%40", "@");
            strreturn = strreturn.Replace("%2D", "-");


            return strreturn;
        }

        #endregion

        #region GetVerified

        public bool GetVerified(int resource_key, bool Live_Trial)
        {
            bool boolreturn = false;            

            PayPal.Platform.SDK.BaseAPIProfile profile = new PayPal.Platform.SDK.BaseAPIProfile();            
                        
            profile.APIProfileType = ProfileType.ThreeToken;

            if (Live_Trial)//true = Live , false = trial
            {
                profile.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Live_Environment").ToString();
                profile.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString();
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
            }
            else
            {
                profile.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Trial_Environment").ToString();
                profile.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                profile.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                profile.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();                
            }

            profile.IsTrustAllCertificates = Convert.ToBoolean(ConfigurationManager.AppSettings["TrustAll"]);
            profile.RequestDataformat = "SOAP11";
            profile.ResponseDataformat = "SOAP11";
            profile.DeviceIpAddress = HttpContext.Current.Request.UserHostAddress;            
                        
            PayPal.Platform.SDK.AdaptiveAccounts aa = new PayPal.Platform.SDK.AdaptiveAccounts();
            aa.APIProfile = profile;
            GetVerifiedStatusRequest getVerifiedStatusRequest = null;

            try
            {
                DataSet dstemp = Eventomatic_DB.SPs.ViewPayPalInfo(resource_key).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    //there is a value
                    getVerifiedStatusRequest = new GetVerifiedStatusRequest();

                    getVerifiedStatusRequest.emailAddress = dstemp.Tables[0].Rows[0]["Email"].ToString();
                    getVerifiedStatusRequest.firstName = dstemp.Tables[0].Rows[0]["First_Name"].ToString();
                    getVerifiedStatusRequest.lastName = dstemp.Tables[0].Rows[0]["Last_Name"].ToString();
                    getVerifiedStatusRequest.matchCriteria = "NAME";
                    getVerifiedStatusRequest.requestEnvelope = new PayPal.Services.Private.AA.RequestEnvelope();
                    getVerifiedStatusRequest.requestEnvelope.errorLanguage = "en_US";
                    GetVerifiedStatusResponse getVerifiedStatusResponse = aa.GetVerifiedStatus(getVerifiedStatusRequest);

                    if (aa.isSuccess == "FAILURE")
                    {
                        //HttpContext.Current.Session[Constants.SessionConstants.FAULT] = aa.LastError;
                        //HttpContext.Current.Response.Redirect("APIError.aspx", false);
                    }

                    else
                    {
                        //Session[Constants.SessionConstants.GETVERIFIEDSTATUSRESPONSE] = getVerifiedStatusResponse;
                        //Response.Redirect("GetVerifiedStatusReceipt.aspx", false);
                        if (getVerifiedStatusResponse.accountStatus.ToUpper() == "VERIFIED")
                        {
                            boolreturn = true;
                        }
                    }
                }
            }
            catch
            {
            }


            return boolreturn;
        }

        #endregion

        #region Check Account EC
        //used for mobile
        public void CheckAccount_ExpressCheckout(bool Live_Trial, string ppemail)
        {


            string parameter;
            StringBuilder requestString = new StringBuilder();

            Site sitetemp = new Site();
            
            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";


            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();

            }
            else
            {
                APIurl = "https://api.sandbox.paypal.com/nvp";//"https://api-3t.sandbox.paypal.com/nvp";//
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }

            APIurl = APIurl.Replace("svcs.", "");

            // Build the method and credential portion of the SetExpressCheckout request string
            parameter = "METHOD=" + HttpUtility.UrlEncode("SetExpressCheckout");
            requestString.Append(parameter);
            requestString.Append(SetupExpressCheckoutstring(Live_Trial));

            // Append the required parameters            
            parameter = "&PAYMENTREQUEST_0_AMT=5";
            requestString.Append(parameter);

            requestString.Append("&PAYMENTREQUEST_0_SELLERPAYPALACCOUNTID=" + ppemail);
            requestString.Append("&PAYMENTREQUEST_0_PAYMENTREQUESTID=0");
            

            parameter = "&PAYMENTREQUEST_0_CURRENCYCODE=USD";

            requestString.Append("&PAYMENTREQUEST_1_SELLERPAYPALACCOUNTID=kingbbj3@hotmail.com");
            requestString.Append("&&PAYMENTREQUEST_1_AMT=5");
            requestString.Append("&PAYMENTREQUEST_1_CURRENCYCODE=USD");
            requestString.Append("&PAYMENTREQUEST_1_PAYMENTREQUESTID=1");
            requestString.Append(parameter);
            parameter = "&RETURNURL=" + ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/" + "mobileconfirm.aspx?Tx_Key=";
            requestString.Append(parameter);
            parameter = "&CANCELURL=" + ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/" + "mobile.aspx?event=";
            requestString.Append(parameter);

            //Other info
            parameter = "&SOLUTIONTYPE=Sole";
            requestString.Append(parameter);

            parameter = "&NOSHIPPING=1";
            requestString.Append(parameter);


            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {

                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = request.Length;

                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                for (int i = 0; i < pairs.Length; i++)
                {

                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    if (pair[0].ToLower() == "ack" && HttpUtility.UrlDecode(pair[1]).ToLower() != "failure")
                    {
                        success = true;
                    }
                    if (pair[0].ToLower() == "token")
                    {
                        token = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "payerid")
                    {
                        payerID = HttpUtility.UrlDecode(pair[1]);
                    }
                }
                if (success)
                {                    

                    //HttpContext.Current.Session["OrderTotal"] = amountTextBox.Text;
                    if (Live_Trial)//true = Live , false = trial
                    {
                        
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    //messageLabel.Text = "SetExpressCheckout Failed";
                }
            }
            catch (Exception ex)
            {
            }
        }

        # endregion

        #region Check Account EC
        public void CheckAccount_ParallelPayment(bool Live_Trial, string stremail)
        {
            PayRequest payRequest = null;
            PayPal.Platform.SDK.BaseAPIProfile profile2 = new PayPal.Platform.SDK.BaseAPIProfile();

            ////Three token 

            profile2.APIProfileType = ProfileType.ThreeToken;

            if (Live_Trial)//true = Live , false = trial
            {
                profile2.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Live_Environment").ToString();
                profile2.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString();
                profile2.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                profile2.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                profile2.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
            }
            else
            {
                profile2.Environment = System.Configuration.ConfigurationSettings.AppSettings.Get("Trial_Environment").ToString();
                profile2.ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile2.APIUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                profile2.APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                profile2.APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
            }
            

            profile2.IsTrustAllCertificates = Convert.ToBoolean(ConfigurationManager.AppSettings["TrustAll"]);

            string url = System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/";
            string returnURL = url + "Order_Confirmation.aspx?Tx_key=";
            string cancelURL = url + "Order_Form.aspx?Event_Key=";
            //profile2 = (BaseAPIProfile)HttpContext.Current.Session[Constants.SessionConstants.PROFILE];
            /*profile2.APIUsername = "lorne_1261162854_biz_api1.lornestar.com";
            profile2.APIPassword = "1261162857";
            profile2.APISignature = "Ag4nejYcMq0VFMCdFMpkphAahlwbAXwU43WtfmIl4FC6WJNf0j.0SNj7";
            profile2.APIProfileType = ProfileType.ThreeToken;*/

            payRequest = new PayRequest();
            payRequest.cancelUrl = cancelURL;
            payRequest.returnUrl = returnURL;
            payRequest.ipnNotificationUrl = System.Configuration.ConfigurationSettings.AppSettings.Get("Order_Form_IPNURL").ToString();
            payRequest.reverseAllParallelPaymentsOnError = true;


            //payRequest.senderEmail = email.Value;
            //payRequest.clientDetails = new ClientDetailsType();
            //payRequest.clientDetails = ClientInfoUtil.getMyAppDetails();            

            payRequest.feesPayer = "EACHRECEIVER";//feesPayer.Value;
            payRequest.memo = "testing";// memo.Value;
            payRequest.actionType = "PAY";
            payRequest.currencyCode =  "USD"; //currencyCode.Items[currencyCode.SelectedIndex].Value;
            payRequest.requestEnvelope = new PayPal.Services.Private.AP.RequestEnvelope();
            payRequest.requestEnvelope.errorLanguage = "en_US";//ClientInfoUtil.getMyAppRequestEnvelope();

            payRequest.receiverList = new Receiver[2];
            payRequest.receiverList[0] = new Receiver();
            payRequest.receiverList[0].amount = decimal.Round(5, 2);//amount_0.Value);
            payRequest.receiverList[0].email = stremail;//receiveremail_0.Value;            

            payRequest.receiverList[1] = new Receiver();
            payRequest.receiverList[1].amount = decimal.Round(5, 2);//amount_0.Value);
            payRequest.receiverList[1].email = "Lorne@Lornestar.com";//receiveremail_0.Value;            

            
            profile2.ResponseDataformat = "SOAP11";
            profile2.RequestDataformat = "SOAP11";


            PayPal.Platform.SDK.AdapativePayments ap = new PayPal.Platform.SDK.AdapativePayments();

            ap.APIProfile = profile2;

            PayResponse PResponse = ap.pay(payRequest);

            if (ap.isSuccess.ToUpper() == "FAILURE")
            {
                //HttpContext.Current.Session[Constants.SessionConstants.FAULT] = ap.LastError;                
                for (int i = 0; i <= ap.LastError.ErrorDetails.Length - 1; i++)
                {
                    PayPal.Platform.SDK.FaultDetailFaultMessageError ETtemp = (PayPal.Platform.SDK.FaultDetailFaultMessageError)ap.LastError.ErrorDetails.GetValue(i);
                    string strtemp = ETtemp.message.ToString();                    
                }
                HttpContext.Current.Response.Redirect("APIError.aspx", false);
            }
            else
            {

                if (PResponse.paymentExecStatus == "COMPLETED")
                {
                    //record any amount of money that got transfered to event host
                    
                }
                else
                {
                    //HttpContext.Current.Session[Constants.SessionConstants.PAYKEY] = PResponse.payKey;
                    /*
                    if (Live_Trial)//true = Live , false = trial
                    {
                        if (usedigitalgoods)
                        {
                            HttpContext.Current.Response.Redirect("https://paypal.com/webapps/adaptivepayment/flow/pay?paykey=" + PResponse.payKey, false);
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("https://www.paypal.com/webscr?cmd=" + "_ap-payment&paykey=" + PResponse.payKey, false);
                        }

                    }
                    else
                    {
                        if (usedigitalgoods)
                        {
                            HttpContext.Current.Response.Redirect("https://sandbox.paypal.com/webapps/adaptivepayment/flow/pay?paykey=" + PResponse.payKey, false);
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("https://www.sandbox.paypal.com/webscr?cmd=" + "_ap-payment&paykey=" + PResponse.payKey, false);
                        }
                    }*/

                }

            }
        }
        #endregion


        #region Check Account DirectPayment
        public string CheckAccount_DirectPayment(bool Live_Trial, string stremail)
        {
            //will return --> "true", "false" or "not verified"

            string strreturn = "true";
            Site sitetemp = new Site();

            string parameter;
            StringBuilder requestString = new StringBuilder();


            string strsubject = "&SUBJECT=" + stremail;            

            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";
            string ApplicationID = "";

            com.paypal.sdk.services.CallerServices caller = new com.paypal.sdk.services.CallerServices();
            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();

            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString(); //"APP-3NS87937TF907760F";
                profile.Environment = "live";
            }
            else
            {
                APIurl = "https://api.sandbox.paypal.com/nvp";//"https://api-3t.sandbox.paypal.com/nvp";//
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile.Environment = "sandbox";
            }

            profile.APIUsername = APIusername;
            profile.APIPassword = APIPassword;
            profile.APISignature = APISignature;
            caller.APIProfile = profile;

            // Build the method and credential portion of the SetExpressCheckout request string
            parameter = "METHOD=" + HttpUtility.UrlEncode("DoDirectPayment");
            requestString.Append(parameter);

            requestString.Append("&VERSION=51.0");
            requestString.Append("&AMT=0.01");
            requestString.Append("&CREDITCARDTYPE=VISA");
            requestString.Append("&ACCT=4520050022690117");
            requestString.Append("&EXPDATE=102012");
            requestString.Append("&CVV2=555");
            requestString.Append("&FIRSTNAME=Mike");
            requestString.Append("&LASTNAME=Sil");
            requestString.Append("&STREET=4 Rodeo Drive");
            requestString.Append("&CITY=Beverly Hills");
            requestString.Append("&STATE=California");
            requestString.Append("&ZIP=90210");
            requestString.Append("&COUNTRYCODE=CA");
            requestString.Append("&CURRENCYCODE=USD");            
            requestString.Append("&PAYMENTACTION=Sale");            
            requestString.Append(strsubject);
            requestString.Append("&USER=" + APIusername);
            requestString.Append("&PWD=" + APIPassword);
            requestString.Append("&SIGNATURE=" + APISignature);
            requestString.Append("&IPADDRESS=10.10.10.10");



            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {
                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";


                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                string txnid = "";
                string errorcode = "";
                for (int i = 0; i < pairs.Length; i++)
                {

                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    if (pair[0].ToLower() == "ack" && HttpUtility.UrlDecode(pair[1]).ToLower() != "failure")
                    {
                        success = true;
                    }
                    if (pair[0].ToUpper() == "L_LONGMESSAGE0")
                    {
                        errorcode = HttpUtility.UrlDecode(pair[1]);
                    }                    
                }
                if (success)
                {
                    strreturn = "true";
                }
                else
                {
                    if (errorcode.Contains("Account is not verified"))
                    {
                        strreturn = "not verified";
                    }
                    else if (errorcode.Contains("You do not have permissions to make this API call"))
                    {
                        strreturn = "false";
                    }
                    else if (errorcode.Contains("This transaction cannot be processed. The merchant's account is not able to process transactions"))
                    {
                        strreturn = "false";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return strreturn;
        }
        #endregion

        #region DoReference Tx

        public string DoReferencePayasyougo(int txkey)
        {
            string strreturn = "";
            Site sitetemp = new Site();
            string stramount = Math.Round(sitetemp.getServiceFee(txkey), 2).ToString();
            bool Live_Trial = !sitetemp.IsDemo_Payforward(txkey);
            string strCurrency = sitetemp.GetResourceCurrencyTx(txkey);
            int resourcekey = Convert.ToInt32(sitetemp.GetResourceKeytxKey(txkey));

            strreturn = DoReferencetx(strCurrency, stramount, "Snappay Transaction fee", Live_Trial,0,resourcekey,txkey);

            return strreturn;
        }

        public string DoReferenceHighVolume(int resourcekey)
        {
            string strreturn = "";
            Site sitetemp = new Site();
            string stramount = ConfigurationSettings.AppSettings.Get("Monthly_Service_Fee").ToString();
            bool Live_Trial = !sitetemp.IsDemo_ResourceKey(resourcekey);
            string strCurrency = sitetemp.GetResourceCurrencyResourceKey(resourcekey);

            //double check to make sure not double charging merchant


            strreturn = DoReferencetx(strCurrency, stramount, "Snappay Monthly fee", Live_Trial,1,resourcekey,0);

            return strreturn;
        }

        public string DoReferencetx(string strCurrency, string stramount, string strdesc, bool Live_Trial, int Billingtype, int resource_key, int txkey)
        {
            string strreturn = "";
            Site sitetemp = new Site();

            

            string parameter;
            StringBuilder requestString = new StringBuilder();
            
            string APIusername = "";
            string APIPassword = "";
            string APISignature = "";
            string APIurl = "";
            string ApplicationID = "";

            string strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
            if (!Live_Trial)
            {
                strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString();
            }

            com.paypal.sdk.services.CallerServices caller = new com.paypal.sdk.services.CallerServices();
            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();

            if (Live_Trial)//true = Live , false = trial
            {
                APIurl = "https://api-3t.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername_Live").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword_Live").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature_Live").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID_Live").ToString(); //"APP-3NS87937TF907760F";
                profile.Environment = "live";
            }
            else
            {
                APIurl = "https://api-3t.sandbox.paypal.com/nvp";
                APIusername = System.Configuration.ConfigurationSettings.AppSettings.Get("APIUsername").ToString();
                APIPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("APIPassword").ToString();
                APISignature = System.Configuration.ConfigurationSettings.AppSettings.Get("APISignature").ToString();
                ApplicationID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID").ToString();
                profile.Environment = "sandbox";
            }

            profile.APIUsername = APIusername;
            profile.APIPassword = APIPassword;
            profile.APISignature = APISignature;
            caller.APIProfile = profile;

            string referenceid = "";
            string billingagreementkey = "";            
            DataSet dstemp = Eventomatic_DB.SPs.ViewBillingAgreementActiveResourceKey(resource_key).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                referenceid = dstemp.Tables[0].Rows[0]["Reference_Transaction"].ToString();
                billingagreementkey = dstemp.Tables[0].Rows[0]["Billing_Agreement_Key"].ToString();
            }

            // Build the method and credential portion of the SetExpressCheckout request string
            parameter = "METHOD=" + HttpUtility.UrlEncode("DoReferenceTransaction");
            requestString.Append(parameter);
            requestString.Append("&USER=" + APIusername);
            requestString.Append("&PWD=" + APIPassword);
            requestString.Append("&SIGNATURE=" + APISignature);
            requestString.Append("&REFERENCEID=" + referenceid);
            requestString.Append("&VERSION=69.0");            
            requestString.Append("&PAYMENTACTION=Sale");
           // requestString.Append("&L_ITEMCATEGORY0=Digital");            
            requestString.Append("&CURRENCYCODE=" + strCurrency);

            string paymentamount = stramount;

            requestString.Append("&AMT=" + paymentamount);
            requestString.Append("&DESC=" + strdesc);

            
            // Post the request to the API and redirect the buyer to PayPal
            string token = string.Empty;
            string payerID = string.Empty;
            string request = requestString.ToString();
            HttpWebResponse webResponse;
            try
            {
                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";


                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;
                if (!webRequest.HaveResponse)
                {
                    throw new Exception();
                }
                if (webResponse.StatusCode != HttpStatusCode.OK && webResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception();
                }

                // Read the response string
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();

                // Parse the response string
                bool success = false;
                char[] ampersand = { '&' };
                string[] pairs = responseString.Split(ampersand);
                char[] equalsign = { '=' };
                string txnid = "";
                string correlationid = "";
                for (int i = 0; i < pairs.Length; i++)
                {

                    // Find the acknowledgement and other parameters required for subsequent API calls
                    string[] pair = pairs[i].Split(equalsign);
                    if (pair[0].ToLower() == "ack" && HttpUtility.UrlDecode(pair[1]).ToLower() != "failure")
                    {
                        success = true;
                    }
                    if (pair[0].ToLower() == "token")
                    {
                        token = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "payerid")
                    {
                        payerID = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "transactionid")
                    {
                        txnid = HttpUtility.UrlDecode(pair[1]);
                    }
                    if (pair[0].ToLower() == "correlationid")
                    {
                        correlationid = HttpUtility.UrlDecode(pair[1]);
                    }
                }
                if (success)
                {
                    //string strmerchantemail = sitetemp.GetResourceEmail(resource_key);
                    Eventomatic_DB.SPs.UpdateBillingPayment(txkey, Convert.ToDecimal(paymentamount), 0, correlationid, txnid,Convert.ToInt32(billingagreementkey)).Execute();
                    strreturn = txnid;
                }
                else
                {
                    Eventomatic_DB.SPs.UpdateCCErrors2(pairs[7].ToString(), txkey, 5, paymentamount, correlationid, pairs[5].ToString()).Execute();

                    //Send Warning Email
                    string strbody = "A Billing error occured.<br/> txkey = " + txkey + ", resourcekey = " + resource_key + ", Billing Agreement Key = " + billingagreementkey;
                    string Toemail = System.Configuration.ConfigurationSettings.AppSettings.Get("ErrorToEmail").ToString();
                    Send_Email SE = new Send_Email();
                    SE.Send_Email_Function("BillingError@theGroupstore.com", Toemail, "An Billing Error has occured", strbody, 0);
                }
            }
            catch (Exception ex)
            {
            }

            return strreturn;
        }
        #endregion
    }
}
