using System;
using System.IO;
using System.Text;
using System.Net;
using System.Web;
using System.Collections;
using System.Data;

namespace Eventomatic
{
    public partial class IPNing : System.Web.UI.Page
    {
        int Tx_Key = 0;
        string Paytype = "CC";

        protected void Page_Load(object sender, EventArgs e)
        {            
            if ((Request.QueryString["Tx_Key"] != null) && (Request.QueryString["Tx_Key"] != ""))
            {
                Tx_Key = Convert.ToInt32(Request.QueryString["Tx_Key"].ToString());
            }            
            if ((Request.QueryString["Paytype"] != null) && (Request.QueryString["Paytype"] != ""))
            {
                Paytype = Request.QueryString["Paytype"].ToString();
            }            
            //Post back to either sandbox or live
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://www.paypal.com/cgi-bin/webscr";
            string strReq = strSandbox;
            Site sitetemp = new Site();
            int resource_key = Convert.ToInt32(sitetemp.GetResourceKeytxKey(Tx_Key));

            if (sitetemp.isPayForward(Tx_Key))
            {
                Paytype = "PF";
            }            

            if (sitetemp.IsDemo_ResourceKey(resource_key))
            {
                strReq = strLive;
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strReq);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            Eventomatic_DB.SPs.UpdateTransactionErrors(strRequest).Execute();            

            //Split up the strRequest & extract variables
            Hashtable Paypal_Vars = new Hashtable();
            string[] strRequestSplit = strRequest.Split('&');
            string[] strtemp;
            string[] strtempAmount;
            decimal dcAmountTotal = 0;
            
            foreach (string word in strRequestSplit)
            {
                strtemp = word.Split('=');
                Paypal_Vars.Add(strtemp[0], strtemp[1]);
                if (((Paytype == "AP")||(Paytype == "PF")) && (strtemp[0].Contains("amount")))
                {
                    strtempAmount = strtemp[1].Split('+');                    
                        dcAmountTotal += decimal.Parse(strtempAmount[1]);
                }
            }
            
            string stritem_name = "";
            string strmc_gross = "0";
            string strfirst_name = "";
            string strlast_name = "";
            string strtxn_id = "";
            string strpayer_id = "";
            string strtax = "0";
            string strpayment_status = "";
            string strpayer_status = "";
            string strreceiver_email = "";
            string strpayer_email = "";
            string strpayment_type = "";
            string strmc_currency = "";
            if (Paytype == "CC")
            {
                
                if (Paypal_Vars.ContainsKey("item_name")) { stritem_name = Paypal_Vars["item_name"].ToString(); }
               
                if (Paypal_Vars.ContainsKey("mc_gross")) { strmc_gross = Paypal_Vars["mc_gross"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("first_name")) { strfirst_name = Paypal_Vars["first_name"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("last_name")) { strlast_name = Paypal_Vars["last_name"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("txn_id")) { strtxn_id = Paypal_Vars["txn_id"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("payer_id")) { strpayer_id = Paypal_Vars["payer_id"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("tax")) { strtax = Paypal_Vars["tax"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("payment_status")) { strpayment_status = Paypal_Vars["payment_status"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("payer_status")) { strpayer_status = Paypal_Vars["payer_status"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("receiver_email")) { strreceiver_email = Paypal_Vars["receiver_email"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("payer_email")) { strpayer_email = Paypal_Vars["payer_email"].ToString(); strpayer_email = strpayer_email.Replace("%40", "@"); }
                
                if (Paypal_Vars.ContainsKey("payment_type")) { strpayment_type = Paypal_Vars["payment_type"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("mc_currency")) { strmc_currency = Paypal_Vars["mc_currency"].ToString(); }              
            }
            else if (Paytype == "AP")
            {
                
                if (Paypal_Vars.ContainsKey("memo")) { stritem_name = Paypal_Vars["memo"].ToString(); }
                strmc_gross = dcAmountTotal.ToString();// "0";
                //if (Paypal_Vars.ContainsKey("mc_gross")) { strmc_gross = Paypal_Vars["mc_gross"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("first_name")) { strfirst_name = Paypal_Vars["first_name"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("last_name")) { strlast_name = Paypal_Vars["last_name"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("transaction%5B0%5D.id_for_sender_txn")) { strtxn_id = Paypal_Vars["transaction%5B0%5D.id_for_sender_txn"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("payer_id")) { strpayer_id = Paypal_Vars["payer_id"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("tax")) { strtax = Paypal_Vars["tax"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("payment_status")) { strpayment_status = Paypal_Vars["payment_status"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("payer_status")) { strpayer_status = Paypal_Vars["payer_status"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("receiver_email")) { strreceiver_email = Paypal_Vars["receiver_email"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("sender_email")) { strpayer_email = Paypal_Vars["sender_email"].ToString(); strpayer_email = strpayer_email.Replace("%40", "@"); }
                
                if (Paypal_Vars.ContainsKey("payment_type")) { strpayment_type = Paypal_Vars["payment_type"].ToString(); }
                
                if (Paypal_Vars.ContainsKey("mc_currency")) { strmc_currency = Paypal_Vars["mc_currency"].ToString(); }              
            }
            else if (Paytype == "PF")
            {

                if (Paypal_Vars.ContainsKey("item_name")) { stritem_name = Paypal_Vars["item_name"].ToString(); }

                if (Paypal_Vars.ContainsKey("mc_gross")) { strmc_gross = Paypal_Vars["mc_gross"].ToString(); }

                if (Paypal_Vars.ContainsKey("first_name")) { strfirst_name = Paypal_Vars["first_name"].ToString(); }

                if (Paypal_Vars.ContainsKey("last_name")) { strlast_name = Paypal_Vars["last_name"].ToString(); }

                if (Paypal_Vars.ContainsKey("transaction%5B0%5D.id_for_sender_txn")) { strtxn_id = Paypal_Vars["transaction%5B0%5D.id_for_sender_txn"].ToString(); }

                if (Paypal_Vars.ContainsKey("payer_id")) { strpayer_id = Paypal_Vars["payer_id"].ToString(); }

                if (Paypal_Vars.ContainsKey("tax")) { strtax = Paypal_Vars["tax"].ToString(); }

                if (Paypal_Vars.ContainsKey("payment_status")) { strpayment_status = Paypal_Vars["payment_status"].ToString(); }

                if (Paypal_Vars.ContainsKey("payer_status")) { strpayer_status = Paypal_Vars["payer_status"].ToString(); }

                if (Paypal_Vars.ContainsKey("transaction%5B0%5D.receiver")) { strreceiver_email = Paypal_Vars["transaction%5B0%5D.receiver"].ToString(); }

                if (Paypal_Vars.ContainsKey("payer_email")) { strpayer_email = Paypal_Vars["payer_email"].ToString(); strpayer_email = strpayer_email.Replace("%40", "@"); }

                if (Paypal_Vars.ContainsKey("payment_type")) { strpayment_type = Paypal_Vars["payment_type"].ToString(); }

                if (Paypal_Vars.ContainsKey("mc_currency")) { strmc_currency = Paypal_Vars["mc_currency"].ToString(); }
            }
                       
            //ABOVE Split up the strRequest & extract variables
            

            if (Paytype != "PF")
            {

                //Check that the tickets purchased aren't soldout
                bool IsSoldOut = false;

                if (Tx_Key > 0)
                {
                    DataSet dstempSoldOut = Eventomatic_DB.SPs.ViewTicketSpecificIPNingSoldout(Tx_Key).GetDataSet();
                    foreach (DataRow r in dstempSoldOut.Tables[0].Rows)
                    {
                        if (r["Sold_Out"].ToString() == "Sold Out")
                        {
                            IsSoldOut = true;
                        }
                    }
                }

                //If tixs are available that were purchased, then validate, if soldout send invalid
                if (IsSoldOut)
                {
                    strRequest += "&cmd=_notify-soldout";
                }
                else
                {
                    strRequest += "&cmd=_notify-validate";
                }

            }
            else
            {
                strRequest += "&cmd=_notify-validate";
            }

            req.ContentLength = strRequest.Length;

            
            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://url:port#"));
            //req.Proxy = proxy;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();
            
            if (strResponse == "VERIFIED")
            {                                

                if (Paytype == "PF")
                {                    

                    Eventomatic_DB.SPs.UpdateTransactionDirectPayments(Tx_Key, strpayer_email, strfirst_name, strlast_name, strtxn_id).Execute();
                    Eventomatic_DB.SPs.UpdateTransactionTicketAmountEmail(Tx_Key, strreceiver_email).Execute();
                }
                else
                {
                    string strtemp2 =
                    stritem_name + "-" +
                    strmc_gross + "-" +
                    strfirst_name + "-" +
                    strlast_name + "-" +
                    strtxn_id + "-" +
                    strpayer_id + "-" +
                    strtax + "-" +
                    strpayment_status + "-" +
                    strpayer_status + "-" +
                    strreceiver_email + "-" +
                    strpayer_email + "-" +
                    strpayment_type + "-" +
                    strmc_currency;
                    string tx_status = "0";
                    DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(Tx_Key).GetDataSet();
                    if (dstemp.Tables[0].Rows[0]["Tx_Status"] != DBNull.Value)
                    {
                        tx_status = dstemp.Tables[0].Rows[0]["Tx_Status"].ToString();
                    }
                    if (Paytype == "AP")
                    {
                        Eventomatic_DB.SPs.UpdateTransaction(Tx_Key, 0, stritem_name, Convert.ToDecimal(strmc_gross)
                        , "", 0, strfirst_name, strlast_name
                        , "", "", 2
                        , strtxn_id, strpayer_id, Convert.ToDecimal(strtax)
                        , strpayment_status, strpayer_status, strreceiver_email
                        , strpayer_email, strpayment_type, strmc_currency
                        , strRequest, "", "", 0, "").Execute();

                        Eventomatic_DB.SPs.UpdateTransactionOutTxkey(Tx_Key).Execute();
                    }

                    //check the payment_status is Completed
                    //check that txn_id has not been previously processed
                    //check that receiver_email is your Primary PayPal email
                    //check that payment_amount/payment_currency are correct
                    //process payment 
                    if (tx_status != "2")
                    {
                        Send_Email SE = new Send_Email();
                        SE.Send_Transaction_Email(Tx_Key, "");
                    }
                }                
            }
            else if (strResponse == "INVALID")
            {
                Eventomatic_DB.SPs.UpdateTransactionErrors(strRequest).Execute();
                //log for manual investigation
            }
            else
            {
                //log response/ipn data for manual investigation
            }

            if (resource_key == 42)
            {
                //windy's stuff
                Eventomatic_DB.SPs.UpdateTransaction(Tx_Key, 0, stritem_name, Convert.ToDecimal(strmc_gross)
                        , "", 0, strfirst_name, strlast_name
                        , "", "", 2
                        , strtxn_id, strpayer_id, Convert.ToDecimal(strtax)
                        , strpayment_status, strpayer_status, strreceiver_email
                        , strpayer_email, strpayment_type, strmc_currency
                        , strRequest, "", "", 0, "").Execute();

                Eventomatic_DB.SPs.UpdateTransactionOutTxkey(Tx_Key).Execute();

                string pdfreceipt = "";
                //Send email                    
                Send_Email SE = new Send_Email();
                SE.Send_Transaction_Email(Tx_Key, pdfreceipt);
            }

        }
        
        protected Boolean IsDemo()
        {
            //False = Trial(Default) & True = Live
            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionIsdemo(Tx_Key).GetDataSet();
            Boolean Demo = false;
            if (dstemp.Tables[0].Rows[0]["Demo"] != DBNull.Value)
            {
                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Demo"].ToString()) == true)
                {
                    Demo = true;
                }
            }
            return Demo;
        }

        protected void updatepaytype()
        {
            if ((Request.QueryString["Paytype"] != null) && (Request.QueryString["Paytype"] != ""))
            {
                Paytype = Request.QueryString["Paytype"].ToString();
            }
        }

    }
}
