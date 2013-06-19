using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using SubSonic;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using com.paypal.soap.api;
using System.Collections;

namespace Eventomatic
{
    public partial class PayForward_Ajax : System.Web.UI.Page
    {
        string strReturn = "";
        char chr34 = Convert.ToChar(34);
        char chr92 = Convert.ToChar(92);
        string cb = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["type"] != null) || (Request.Form["type"] != null))
            {
                int Resource_Key = 0;
                cb = "";                
                if (Request.QueryString["callback"] != null)
                {
                    cb = Request.QueryString["callback"].ToString();
                }
                else if (Request.Form["callback"] != null)
                {
                    cb = Request.Form["callback"].ToString();
                }
                strReturn = cb;
                string strtype = "";
                if (Request.QueryString["type"] != null)
                {
                    strtype = Request.QueryString["type"].ToString();
                }
                else if (Request.Form["type"] != null)
                {
                    strtype = Request.Form["type"].ToString();
                }
                else
                {
                    strtype = "GetSig";
                }                
                switch (strtype)
                {
                    case "SetupTx":
                        //stuff
                        strReturn += "({ " + chr34 + "txkey" + chr34 + " : " + chr34 + SetupTx() + chr34 + " })";
                        break;
                    case "SetupForwardTx":
                        int strnewtx = SetupTx();

                        smstx(strnewtx);
                        
                        strReturn += "({" +  chr34 + "txkey" + chr34 + ":" + chr34 + strnewtx + chr34 + "})";
                        break;
                    case "ActivePayments":
                        Int64 fbid = 0;
                        if (Request.QueryString["fbid"] != null)
                        {
                            fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
                        }
                        strReturn += "({" + chr34 + "activepayments" + chr34 + ": " + ActivePayments(fbid) + "})";
                        break;
                    case "ReportList":
                        
                        if (Request.QueryString["Resource_Key"] != null)
                        {
                            Resource_Key = Convert.ToInt32(Request.QueryString["Resource_Key"].ToString());
                        }
                        else if (Request.Form["Resource_Key"] != null)
                        {
                            Resource_Key = Convert.ToInt32(Request.Form["Resource_Key"].ToString());
                        }
                        strReturn += ReportList(Resource_Key).Replace(@"\", " ");
                        break;
                    case "ReportList_Iphone":
                        if (Request.QueryString["Resource_Key"] != null)
                        {
                            Resource_Key = Convert.ToInt32(Request.QueryString["Resource_Key"].ToString());
                        }
                        else if (Request.Form["Resource_Key"] != null)
                        {
                            Resource_Key = Convert.ToInt32(Request.Form["Resource_Key"].ToString());
                        }
                        strReturn += ReportList_Iphone(Resource_Key).Replace(@"\", " ");
                        break;
                    case "ResourceName":
                        strReturn += "({" + chr34 + "resourcekey" + chr34 + ": " + UpdateResource() + "})";

                        break;

                    case "AdminList":
                        strReturn += GetAdminList().Replace(@"\", " ");
                        break;
                    case "SellerList":
                        strReturn += GetSellerList().Replace(@"\", " ");
                        break;
                    case "InviteUser":
                        
                        strReturn += "({invitekey:"+ inviteuser() +" })";
                        break;
                    case "RemoveUser":
                        removeuser();
                        strReturn += "({ })";
                        break;
                    case "ResourceList":
                        strReturn += getresourceslist().Replace(@"\", " ");
                        break;
                    case "OpenStore":                        
                        strReturn += "({ resourcekey:" +OpenStore()+ " })";
                        break;
                    case "Menu_Merchant":
                        strReturn += getMenuMerchant().Replace(@"\", " ");
                        break;
                    case "updateProductsPurchased":
                        updateProductsPurchased();
                        strReturn += "({ })";
                        break;
                    case "PaymentCC":
                        strReturn += "({ " + chr34 + "PaymentComplete" + chr34 + " : " + chr34 + Paymentcc() + chr34 + " })";                        
                        break;
                    case "GetSig":
                        strReturn += "({ " + chr34 + "SigComplete" + chr34 + " : " + chr34 + GetSignature() + chr34 + " })";
                        break;
                    case "successtxmpl":
                        strReturn += "({ " + chr34 + "PaymentComplete" + chr34 + " : " + chr34 + successtxmpl() + chr34 + " })";
                        break;
                    case "ECurl":
                        strReturn += "({ " + chr34 + "ECurl" + chr34 + " : " + chr34 + ECurl() + chr34 + " })";
                        break;
                    case "Checktxstatus":
                        strReturn += txstatus();                        
                        break;
                    case "MassPay3":
                        domasspay();
                        break;
                    case "initpage":
                        strReturn += initpage();
                        break;
                    case "initpage_username":
                        strReturn += initpage_username();
                        break;
                    case "sendreceipt":
                        strReturn += sendreceipt();
                        break;
                }
            }            
            
            Response.Write( strReturn);
            //lblreturn.Text = strReturn;
        }

        protected int SetupTx()
        {
            string tempTx_Key = "0";

            string Note = "";
            decimal amount = 0;
            string currency = "USD";
            int Resource_Key = 0;
            Int64 fbid = 0;
            string ipaddress = "";
            Site sitetemp = new Site();

            if (Request.QueryString["note"] != null)
            {
                Note = Request.QueryString["note"].ToString();
            }
            if (Request.QueryString["amount"] != null)
            {
                amount = Convert.ToDecimal(Request.QueryString["amount"].ToString());                
            }            
            if (Request.QueryString["resource_key"] != null)
            {
                Resource_Key = Convert.ToInt32(Request.QueryString["resource_key"].ToString());
            }
            if (Request.QueryString["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
            }
            if (Request.QueryString["ipaddress"] != null)
            {
                ipaddress = Request.QueryString["ipaddress"].ToString();
            }
            currency = sitetemp.GetCurrency(Resource_Key);
            ipaddress = HttpContext.Current.Request.UserHostAddress;

            StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdatePayForwardTxSetup(Note, amount, currency, Resource_Key, fbid, 0, ipaddress);
            sp_UpdateTransaction.Execute();
            tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
            
            return Convert.ToInt32(tempTx_Key);
        }

        protected void smstx(int txkey)
        {
            Addons.Twilio sms = new Addons.Twilio();
            string thebody = "";
            Site sitetemp = new Site();
            string mobilepayurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "mobilepay.aspx?tx=" + txkey.ToString();
            string PhoneNumber = "";
            if (Request.QueryString["phonenumber"] != null)
            {
                PhoneNumber = Request.QueryString["phonenumber"].ToString();
            }

            thebody = sitetemp.getfbid_Seller_Name(txkey) + " initiated a Groupstore Sale. Complete this sale at " + mobilepayurl;
            
            sms.SendSMS(PhoneNumber, thebody);

            Eventomatic_DB.SPs.UpdateTransactionSMSNumber(txkey, PhoneNumber).Execute();
            Eventomatic_DB.SPs.PfUpdateDidforward(txkey).Execute();
        }

        protected string ActivePayments(Int64 fbid)
        {
            string strReturn = "";
            DataSet dstemp = Eventomatic_DB.SPs.PfViewTransactionDetailsFbidPasthour(fbid).GetDataSet();
            strReturn = JsonConvert.SerializeObject(dstemp.Tables[0]);
            return strReturn;
        }

        protected string ReportList(int Resource_Key)
        {
            string strReturn = "";
            int intreporttype = 0;
            Int64 fbid = 0;
            Site sitetemp = new Site();
            if (Request.QueryString["reporttype"] != null)
            {
                if (sitetemp.IsNumeric(Request.QueryString["reporttype"].ToString()))
                {
                    intreporttype = Convert.ToInt32(Request.QueryString["reporttype"].ToString());
                }                
            }
            else if (Request.Form["reporttype"] != null)
            {
                if (sitetemp.IsNumeric(Request.Form["reporttype"].ToString()))
                {
                    intreporttype = Convert.ToInt32(Request.Form["reporttype"].ToString());
                }
            }

            if (Request.QueryString["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
            }
            else if (Request.Form["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.Form["fbid"].ToString());
            }
            DataSet dstemp = Eventomatic_DB.SPs.PfViewTransactionDetailsResourcekey(Resource_Key, intreporttype,fbid).GetDataSet();
            strReturn = JsonConvert.SerializeObject(dstemp.Tables[0]);
            return strReturn;
        }

        protected string ReportList_Iphone(int Resource_Key)
        {
            string strReturn = "";
            int intreporttype = 0;
            Int64 fbid = 0;
            Site sitetemp = new Site();
            if (Request.QueryString["reporttype"] != null)
            {
                if (sitetemp.IsNumeric(Request.QueryString["reporttype"].ToString()))
                {
                    intreporttype = Convert.ToInt32(Request.QueryString["reporttype"].ToString());
                }
            }
            else if (Request.Form["reporttype"] != null)
            {
                if (sitetemp.IsNumeric(Request.Form["reporttype"].ToString()))
                {
                    intreporttype = Convert.ToInt32(Request.Form["reporttype"].ToString());
                }
            }

            if (Request.QueryString["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
            }
            else if (Request.Form["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.Form["fbid"].ToString());
            }
            DataSet dstemp = Eventomatic_DB.SPs.PfViewTransactionDetailsResourcekeyIphone(Resource_Key, intreporttype, fbid).GetDataSet();
            strReturn = JsonConvert.SerializeObject(dstemp.Tables[0]);
            return strReturn;
        }

        protected int UpdateResource()
        {
            int Resource_Key = 0;
            string Resource_Name = "";
            Int64 fbid = 0;
            string Currency = "USD";
            string PayPalEmail = "";
            if (Request.QueryString["Resource_Key"] != null)
            {
                Resource_Key = Convert.ToInt32(Request.QueryString["Resource_Key"].ToString());
            }
            if (Request.QueryString["Resource_Name"] != null)
            {
                Resource_Name = Request.QueryString["Resource_Name"].ToString();
            }
            if (Request.QueryString["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
            }
            if (Request.QueryString["currency"] != null)
            {
                Currency = Request.QueryString["currency"].ToString();
            }
            if (Request.QueryString["ppemail"] != null)
            {
               PayPalEmail = Request.QueryString["ppemail"].ToString();
            }
            if ((Resource_Key == 0) || (Resource_Key == 3))
            {
                StoredProcedure sp_AddGroup = Eventomatic_DB.SPs.UpdateGroups(fbid, 0, Resource_Name, 0, 0, 0, 0);
                sp_AddGroup.Execute();
                Resource_Key = Convert.ToInt32(sp_AddGroup.Command.Parameters[6].ParameterValue.ToString());            
            }
            Eventomatic_DB.SPs.UpdateResourceProfile(Resource_Key, Currency,  PayPalEmail, true, 0).Execute();

            return Resource_Key;
        }

        protected string GetAdminList()
        {
            string strreturn;
            int Resource_Key = 0;
            if (Request.QueryString["Resource_Key"] != null)
            {
                Resource_Key = Convert.ToInt32(Request.QueryString["Resource_Key"].ToString());
            }
            DataSet dstemp = Eventomatic_DB.SPs.ViewListGroupMembers(Resource_Key).GetDataSet();
            strreturn = JsonConvert.SerializeObject(dstemp.Tables[0]);
            return strreturn;
        }

        protected string GetSellerList()
        {
            string strreturn;
            int Resource_Key = 0;
            if (Request.QueryString["Resource_Key"] != null)
            {
                Resource_Key = Convert.ToInt32(Request.QueryString["Resource_Key"].ToString());
            }
            DataSet dstemp = Eventomatic_DB.SPs.PfViewListStoreSellers(Resource_Key).GetDataSet();
            strreturn = JsonConvert.SerializeObject(dstemp.Tables[0]);
            return strreturn;
        }

        protected string inviteuser()
        {
            int Resource_Key = 0;
            Int64 fbid = 0;
            string PhoneNumber = "";
            bool addadmin = false;
            int invitekey = 0;
            if (Request.QueryString["phonenumber"] != null)
            {
                PhoneNumber = Request.QueryString["phonenumber"].ToString();
            }

            if (Request.QueryString["Resource_Key"] != null)
            {
                Resource_Key = Convert.ToInt32(Request.QueryString["Resource_Key"].ToString());
            }            
            if (Request.QueryString["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
            }
            if (Request.QueryString["addadmin"] != null)
            {
                addadmin = Convert.ToBoolean(Request.QueryString["addadmin"].ToString());
            }
            
            StoredProcedure sp_AddInvite = Eventomatic_DB.SPs.PfUpdateReferral(fbid, addadmin, PhoneNumber, Resource_Key ,0);
            sp_AddInvite.Execute();
            invitekey = Convert.ToInt32(sp_AddInvite.Command.Parameters[4].ParameterValue.ToString());            
                      

            Addons.Twilio sms = new Addons.Twilio();
            string thebody = "";
            Site sitetemp = new Site();
            string directurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Demo.aspx?invitekey=" + invitekey.ToString();

            thebody = sitetemp.getfbid_Seller_Name(fbid) + " has invited you to their Groupstore. Click the link to accept this invite: " + directurl;

            if (!directurl.Contains("localhost"))
            {
                sms.SendSMS(PhoneNumber, thebody);
            }

            return invitekey.ToString();

            /*
             * 
             */
        }

        protected void removeuser()
        {
            int Resource_Key = 0;
            Int64 fbid = 0;            
            bool isadmin = false;            

            if (Request.QueryString["Resource_Key"] != null)
            {
                Resource_Key = Convert.ToInt32(Request.QueryString["Resource_Key"].ToString());
            }
            if (Request.QueryString["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
            }
            if (Request.QueryString["isadmin"] != null)
            {
                isadmin = Convert.ToBoolean(Request.QueryString["isadmin"].ToString());
            }

            Eventomatic_DB.SPs.PfRemoveFBUsersSellers(fbid, Resource_Key, isadmin).Execute();
        }

        protected string getresourceslist()
        {
            Int64 fbid = 0;
            if (Request.QueryString["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
            }
            DataSet dstemp = Eventomatic_DB.SPs.PfViewListFBUserAdminSellers(fbid).GetDataSet();
            string strreturn = JsonConvert.SerializeObject(dstemp.Tables[0]);
            return strreturn;
        }

        protected string OpenStore()
        {
            Int64 fbid = 0;
            string storename = "";
            if (Request.QueryString["fbid"] != null)
            {
                fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
            }
            if (Request.QueryString["storename"] != null)
            {
                storename = Request.QueryString["storename"].ToString();
            }            
            StoredProcedure sp_AddGroup = Eventomatic_DB.SPs.UpdateGroups(fbid, 0, storename,
                Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFP").ToString()),
                Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFC").ToString()),
                Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFM").ToString()),
                0);
            sp_AddGroup.Execute();
            return sp_AddGroup.Command.Parameters[6].ParameterValue.ToString();            
        }

        protected string getMenuMerchant()
        {
            int Resource_Key = 0;            

            if (Request.QueryString["Resource_Key"] != null)
            {
                Resource_Key = Convert.ToInt32(Request.QueryString["Resource_Key"].ToString());
            }

            DataSet dstemp = Eventomatic_DB.SPs.PfViewListMenuMerchant(Resource_Key).GetDataSet();
            string strreturn = JsonConvert.SerializeObject(dstemp.Tables[0]);
            return strreturn;
        }

        protected void updateProductsPurchased()
        {
            string jsonTicketKeys = "";
            int txkey = 0;
            if (Request.QueryString["Ticket_Keys"] != null)
            {
                jsonTicketKeys = Request.QueryString["Ticket_Keys"].ToString().Substring(1);
            }
            if (Request.QueryString["txkey"] != null)
            {
                txkey = Convert.ToInt32(Request.QueryString["txkey"].ToString());
            }

            string[] words = jsonTicketKeys.Split('~');
            foreach (string word in words)
            {
                string[] words2 = word.Split('-');
                Int32 ticketkey = Convert.ToInt32(words2[0]);
                Int32 quantity = Convert.ToInt32(words2[1]);
                Eventomatic_DB.SPs.UpdateTicketsPurchased(txkey, ticketkey, quantity, 0, 0, "", "").Execute();
            }
        }

        protected string ECurl()
        {
            string strECurl = "";

            int txkey = 0;            
            if (Request.QueryString["txkey"] != null)
            {
                txkey = Convert.ToInt32(Request.QueryString["txkey"].ToString());
            }

            Site sitetemp = new Site();
            Boolean isdemovar = sitetemp.IsDemo_Payforward(txkey);

            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(txkey).GetDataSet();
            decimal OverallTotal = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Amount"].ToString());

            Eventomatic.Addons.PaypalMethods dopayment = new Eventomatic.Addons.PaypalMethods();
            strECurl = dopayment.DoExpressCheckout_Native(txkey, !isdemovar,OverallTotal);

            return strECurl;
        }

        protected string Paymentcc()
        {
            Site sitetemp = new Site();
            string strAckresponse = "false";
            string strjson = "type=PaymentCC";
            if (Request.QueryString["txkey"] != null)
            {
                strjson += "&txkey=" + Request.QueryString["txkey"];
            }
            else if (Request.Form["txkey"] != null)
            {
                strjson += "&txkey=" + Request.Form["txkey"];
            }
            try
            {
                int txkey = 0;
                string ccnumber = "";
                string ccexpmonth = "";
                string ccexpyear = "";
                string ccname = "";
                string ccemail = "";
                string ccCSC = "000";
                if (Request.QueryString["txkey"] != null)
                {
                    txkey = Convert.ToInt32(Request.QueryString["txkey"].ToString());
                    strjson += "&txkey=" + txkey;
                }
                else if (Request.Form["txkey"] != null)
                {
                    txkey = Convert.ToInt32(Request.Form["txkey"].ToString());
                    strjson += "&txkey=" + txkey;
                }
                
                if (Request.QueryString["ccnumber"] != null)
                {
                    ccnumber = Request.QueryString["ccnumber"].ToString();
                    strjson += "&ccnumber=" + ccnumber;
                }
                else if (Request.Form["ccnumber"] != null)
                {
                    ccnumber = Request.Form["ccnumber"].ToString();
                    strjson += "&ccnumber=" + ccnumber;
                }

                if (Request.QueryString["ccexpmonth"] != null)
                {
                    ccexpmonth = Request.QueryString["ccexpmonth"].ToString();
                    strjson += "&ccexpmonth" + ccexpmonth;
                }
                else if (Request.Form["ccexpmonth"] != null)
                {
                    ccexpmonth = Request.Form["ccexpmonth"].ToString();
                    strjson += "&ccexpmonth" + ccexpmonth;
                }

                if (Request.QueryString["ccexpyear"] != null)
                {
                    ccexpyear = Request.QueryString["ccexpyear"].ToString();
                    strjson += "&ccexpyear=" + ccexpyear;
                }
                else if (Request.Form["ccexpyear"] != null)
                {
                    ccexpyear = Request.Form["ccexpyear"].ToString();
                    strjson += "&ccexpyear=" + ccexpyear;
                }

                if (Request.QueryString["ccname"] != null)
                {
                    ccname = Request.QueryString["ccname"].ToString();
                    strjson += "&ccname="+ccname;
                }
                else if (Request.Form["ccname"] != null)
                {
                    ccname = Request.Form["ccname"].ToString();
                    strjson += "&ccname=" + ccname;
                }

                if (Request.QueryString["ccemail"] != null)
                {
                    ccemail = Request.QueryString["ccemail"].ToString();
                    strjson += "&ccemail="+ccemail;
                }
                else if (Request.Form["ccemail"] != null)
                {
                    ccemail = Request.Form["ccemail"].ToString();
                    strjson += "&ccemail=" + ccemail;
                }

                if (Request.QueryString["ccCSC"] != null)
                {
                    ccCSC = Request.QueryString["ccCSC"].ToString();
                    strjson += "&ccCSC"+ccCSC;
                }
                else if (Request.Form["ccCSC"] != null)
                {
                    ccCSC = Request.Form["ccCSC"].ToString();
                    strjson += "&ccCSC" + ccCSC;
                }

                string cctype = "Visa";
                if (ccnumber.StartsWith("5"))
                {
                    cctype = "MasterCard";
                }
                else if (ccnumber.StartsWith("3"))
                {
                    cctype = "Amex";
                }
                else if (ccnumber.StartsWith("6"))
                {
                    cctype = "Discover";
                }

                ccnumber = ccnumber.Replace("-", "");

                ccexpyear = ccexpyear.Replace("20", "");

                string[] strnames = getnames(ccname);                
                com.paypal.soap.api.CountryCodeType CountryCode = SelectedCountry();
                Boolean isdemovar = sitetemp.IsDemo_Payforward(txkey);

                com.paypal.soap.api.CurrencyCodeType[] tempCurrency = new com.paypal.soap.api.CurrencyCodeType[1];
                string strCurrency = "CAD";
                strCurrency = sitetemp.GetResourceCurrencyTx(txkey);
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

                DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(txkey).GetDataSet();
                decimal OverallTotal = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Amount"].ToString());
                Eventomatic.Addons.PaypalMethods paytemp = new Eventomatic.Addons.PaypalMethods();

                string strReceiptID = dstemp.Tables[0].Rows[0]["Receipt_ID"].ToString();

                //checks if setting is to collect payment into groupstore account or not
                bool thirdpartypaypal = sitetemp.GetResourceThirdPartyPayPalTx(txkey);
                
                string strHostEmail = "";
                strHostEmail = sitetemp.GetResourceEmailTx(txkey);
                
                
                
                //check if paypal email is groupstores email
                if (!isdemovar)
                {                  
                    if (strHostEmail == ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString())
                    {
                        thirdpartypaypal = false;
                    }                    
                }
                else
                {                    
                    if (strHostEmail == ConfigurationSettings.AppSettings.Get("Host_Email_Trial").ToString())
                    {
                        thirdpartypaypal = false;
                    }                    
                }

                Hashtable httemp;
                DoCaptureResponseType strAck = new DoCaptureResponseType();
                DoDirectPaymentResponseType DirectPaymentObj = new DoDirectPaymentResponseType();
                bool booltxcomplete = false;
                string strtxnid = "";

                bool iscomplete = sitetemp.CheckiftxComplete(txkey);

                if (!iscomplete) //tx hasn't been completed yet
                {
                    /*
                    if (thirdpartypaypal) //using merchant's account to process
                    {
                        strtxnid = paytemp.DoDirectPayment3(decimal.Round(OverallTotal, 2).ToString(), strnames[1].ToString(), strnames[0].ToString(), "", "", "", "", "", cctype, ccnumber,
                       ccCSC, Convert.ToInt32(ccexpmonth), Convert.ToInt32("20" + ccexpyear), com.paypal.soap.api.PaymentActionCodeType.Sale, "Canada", CountryCode, txkey, (com.paypal.soap.api.CurrencyCodeType)tempCurrency.GetValue(0), !isdemovar);

                        if (strtxnid != "") //Payment went through
                        {
                            booltxcomplete = true;
                        }
                    }
                    else //using groupstore's account to process
                    {
                        httemp = paytemp.DoDirectPaymentCode(decimal.Round(OverallTotal, 2).ToString(), strnames[1].ToString(), strnames[0].ToString(), "2 Carling", "", "Hamilton", "On", "L8S1M8", cctype, ccnumber,
                      ccCSC, Convert.ToInt32(ccexpmonth), Convert.ToInt32("20" + ccexpyear), com.paypal.soap.api.PaymentActionCodeType.Sale, "Canada", CountryCode, txkey.ToString(), (com.paypal.soap.api.CurrencyCodeType)tempCurrency.GetValue(0), !isdemovar);


                        strAck = (DoCaptureResponseType)httemp["capt_response"];
                        DirectPaymentObj = (DoDirectPaymentResponseType)httemp["pp_response"];
                        if ((strAck.Ack.ToString().ToLower().Contains("success")) && (DirectPaymentObj.Ack.ToString().ToLower().Contains("success"))) //Payment went through
                        {
                            booltxcomplete = true;
                            strtxnid = DirectPaymentObj.TransactionID;
                        }
                    }
                    */

                    strtxnid = paytemp.DoDirectPayment3(decimal.Round(OverallTotal, 2).ToString(), strnames[1].ToString(), strnames[0].ToString(), "", "", "", "", "", cctype, ccnumber,
                       ccCSC, Convert.ToInt32(ccexpmonth), Convert.ToInt32("20" + ccexpyear), com.paypal.soap.api.PaymentActionCodeType.Sale, "Canada", CountryCode, txkey, (com.paypal.soap.api.CurrencyCodeType)tempCurrency.GetValue(0), !isdemovar);

                    if (!strtxnid.Contains("snerror")) //Payment went through
                    {
                        Eventomatic_DB.SPs.UpdateTransactionDirectPayments(txkey, ccemail, strnames[0].ToString(), strnames[1].ToString(), strtxnid).Execute();

                        string Bizname = sitetemp.GetResourceNameTx(txkey);

                        //Send_Email SE = new Send_Email();
                        //SE.Send_Transaction_Email(txkey, "");
                        //Send text with instructions
                        Addons.Twilio sms = new Addons.Twilio();
                        string thebody = "Your purchase from " + Bizname + " of $" + decimal.Round(OverallTotal, 2).ToString() + " is complete. Visit snap-pay.com to view your Receipt. Your receipt number is " + strReceiptID + ".";

                        try
                        {
                            sms.SendSMS(ccemail, thebody);
                        }
                        catch
                        {
                        }
                    }                    
                }
                

                //Check if tx was completed
                iscomplete = sitetemp.CheckiftxComplete(txkey);
                if (!iscomplete) //not completed
                {
                    //strAckresponse = "false";
                    strAckresponse = strtxnid.Replace("snerror", "");
                }
                else
                {
                    strAckresponse = "true";
                }
            }
            catch
            {
                Eventomatic_DB.SPs.UpdateCCErrors2(strjson, 0, 5, "0", "", "PaymentCC Error").Execute();
            }

            

            return strAckresponse;
        }

        protected string[] getnames(string name)
        {
            string[] strreturn = new string[2];
            char[] chrspace = { ' ' };
            int intfirstspace = name.IndexOfAny(chrspace);

            
            if (intfirstspace > 0)
            {
                strreturn.SetValue(name.Substring(0, intfirstspace).Trim(),0);
                strreturn.SetValue(name.Substring(intfirstspace).Trim(),1);
            }
            else
            {
                strreturn.SetValue(name,0);
                strreturn.SetValue("",1);
            }
            return strreturn;
        }

        protected com.paypal.soap.api.CountryCodeType SelectedCountry()
        {
            return (CountryCodeType)Enum.Parse(typeof(CountryCodeType), "CA", true);//rtnCode;
        }

        protected com.paypal.soap.api.CurrencyCodeType getcurrency(int txkey){
            com.paypal.soap.api.CurrencyCodeType tempCurrency = com.paypal.soap.api.CurrencyCodeType.CAD;
            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceFromTxKey(txkey).GetDataSet();
            string strCurrency = "CAD";
            if (dstemp.Tables[0].Rows[0]["Desired_Currency"] != DBNull.Value)
            {
                strCurrency = dstemp.Tables[0].Rows[0]["Desired_Currency"].ToString();
            }
            switch (strCurrency)
            {
                case "USD": tempCurrency = com.paypal.soap.api.CurrencyCodeType.USD;
                    break;
                case "CAD": tempCurrency = com.paypal.soap.api.CurrencyCodeType.CAD;
                    break;
                case "EUR": tempCurrency = com.paypal.soap.api.CurrencyCodeType.EUR;
                    break;
                case "GBP": tempCurrency = com.paypal.soap.api.CurrencyCodeType.GBP;
                    break;
                case "ILS": tempCurrency = com.paypal.soap.api.CurrencyCodeType.ILS;
                    break;
            }
            return tempCurrency;
        }

        protected string GetSignature()
        {
            int txkey = 0;
            string strreturn = "false";
            byte[] imageData;
            if (Request.Form["txkey"] != null)
            {
                txkey = Convert.ToInt32(Request.Form["txkey"].ToString());
            }
            if (Request.Form["img"] != null)
            {
                string strimg = Request.Form["img"].ToString().Replace("data:image/jpeg;base64,", "");
                strimg = strimg.Replace("data:image/png;base64,", "");
                /*//byte tmpbyte = Convert.ToByte(strimg.Substring(0, 7));
                //imageData = new byte[e];
                //imageData= System.Text.Encoding encoding.GetBytes(yourString)
                
                //System.Text.Encoding enc = System.Text.Encoding. .UTF8;
                imageData = System.Text.Encoding.UTF8.GetBytes(strimg);
                //string myString = enc.GetString(myByteArray );                

                string file = Server.MapPath("/Images/Signature/") + "\\" + txkey.ToString() + ".png";                
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
                }*/

                string file = Server.MapPath("/Images/Signature/") + "\\" + txkey.ToString() + ".jpg";
                FileStream fs = new FileStream(file, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);

                try
                {
                    byte[] data = Convert.FromBase64String(strimg);
                    bw.Write(data);
                }
                catch
                {
                    Eventomatic_DB.SPs.UpdateCCErrors(strimg, txkey, 0,"").Execute();                    
                }

                
                bw.Close();

                strreturn = "true";
            }           
            
            /*
            //save image
            if (imageData.Length > 0)
            {
                

                //string fullurl = Request.Url.AbsoluteUri;
                //string saveimgurl = fullurl.Substring(0, fullurl.ToLower().IndexOf("order_form2.aspx")) + Thumbnail.ImageUrl.Replace("~/", "");

                //Site sitetemp = new Site();
                //sitetemp.savepicurl2(saveimgurl, file);

                
            }*/
            //strreturn = Request.Form["img"].Substring(0,20);
            return strreturn;
        }

        protected string successtxmpl()
        {
            Site sitetemp = new Site();
            string strAckresponse = "false";

            int txkey = 0;
            string txnid = "";            
            string payemail = "";            
            if (Request.QueryString["txkey"] != null)
            {
                txkey = Convert.ToInt32(Request.QueryString["txkey"].ToString());
            }
            if (Request.QueryString["txnid"] != null)
            {
                txnid = Request.QueryString["txnid"].ToString();
            }
            if (Request.QueryString["payemail"] != null)
            {
                payemail = Request.QueryString["payemail"].ToString();
            }

            Eventomatic_DB.SPs.UpdateTransactionDirectPayments(txkey, payemail, "", "", txnid).Execute();

            return strAckresponse;
        }

        protected string txstatus()
        {
            Site sitetemp = new Site();
            string strresponse = "false";

            int txkey = 0;
            string txnid = "";
            string payemail = "";
            if (Request.QueryString["txkey"] != null)
            {
                txkey = Convert.ToInt32(Request.QueryString["txkey"].ToString());
            }

            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(txkey).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Tx_Status"] != DBNull.Value)
            {
                if (dstemp.Tables[0].Rows[0]["Tx_Status"].ToString() == "2")
                {
                    strresponse = "true";
                }
            }

            return strresponse;
        }

        protected void domasspay()
        {
            /*
            int txkey = 0;            
            if (Request.QueryString["txkey"] != null)
            {
                txkey = Convert.ToInt32(Request.QueryString["txkey"].ToString());
            }

            Site sitetemp = new Site();
            //check which plan they are on
            if (!sitetemp.isDoDirectPayment(txkey)) //pay as you go plan
            {                
                Addons.PaypalMethods pp = new Addons.PaypalMethods();
                pp.DoReferencePayasyougo(txkey);
            }
             */
        }

        protected string initpage()
        {
            string strreturn = "{ ";

            string storename = "";
            string resourcekey = "0";
            string ppemail = "0";
            string isadmin = "False";
            string dodirectpayment = "False";
            string currency = "CAD";
            string isdemo = "False";
            string showwelcome = "0";
            string fbid = "0";
            string fbfirstname = "";
            string fblastname = "";
            string fbemail = "";
            string fbaccesstoken = "";
            string numberofstores = "0";

            Site sitetemp = new Site();

            if (Request.QueryString["fbid"] != null)
            {
                fbid = Request.QueryString["fbid"].ToString();
            }
            if (Request.QueryString["fbfirstname"] != null)
            {
                fbfirstname = Request.QueryString["fbfirstname"].ToString();
            }
            if (Request.QueryString["fblastname"] != null)
            {
                fblastname = Request.QueryString["fblastname"].ToString();
            }
            if (Request.QueryString["fbemail"] != null)
            {
                fbemail = Request.QueryString["fbemail"].ToString();
            }
            if (Request.QueryString["fbaccesstoken"] != null)
            {
                fbaccesstoken = Request.QueryString["fbaccesstoken"].ToString();
            }

            Eventomatic_DB.SPs.UpdateResource(Convert.ToInt64(fbid), fbfirstname, fblastname, fbemail, HttpContext.Current.Request.UserHostAddress, "PayForward2.aspx", 0, 0, "", fbaccesstoken, 0).Execute();            

            //check what they are admin or seller of
            DataSet dstemp = Eventomatic_DB.SPs.PfViewListFBUserAdminSellers(Convert.ToInt64(fbid)).GetDataSet();            
            numberofstores = dstemp.Tables[0].Rows.Count.ToString();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                //check if requested specific resource_key
                if ((Request.QueryString["resource_key"] != null) && (Request.QueryString["resource_key"] != ""))
                {
                    //need to check if seller/admin of that store
                    string rskey = Request.QueryString["resource_key"];

                    if (rskey.Contains("isnative"))
                    {
                        rskey = rskey.Replace("isnative", "");                        
                    }

                    DataRow[] drtemp;
                    drtemp = dstemp.Tables[0].Select("resource_key = " + rskey);
                    if (drtemp.GetUpperBound(0) > -1) //at least one exists
                    {
                        storename = drtemp[0]["Resource_Name"].ToString();
                        string strisadmin = drtemp[0]["Isadmin"].ToString();
                        if (strisadmin == "1")
                        {
                            isadmin = "True";
                        }
                        else
                        {
                            isadmin = "False";
                        }
                        resourcekey = drtemp[0]["Resource_Key"].ToString();
                        ppemail = drtemp[0]["Resource_Email"].ToString();

                        resourcekey = rskey;

                        if (drtemp[0]["DoDirect"] != DBNull.Value)
                        {
                            if (Convert.ToBoolean(drtemp[0]["DoDirect"].ToString()))
                            {
                                dodirectpayment = "True";
                            }
                        }
                    }
                    else
                    {
                        storename = "Demo Store";
                        isadmin = "True";
                        resourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();
                        ppemail = "kingbbj3@hotmail.com";
                        isdemo = "True";
                        dodirectpayment = "True";
                        showwelcome = "3";
                        try
                        {
                            Send_Email se = new Send_Email();
                            se.demoemail(Convert.ToInt64(fbid));                        
                        }
                        catch
                        {
                        }                        
                    }
                }
                else //didn't request specific resource_key
                {
                    //they are admin/seller of at least 1 store
                    storename = dstemp.Tables[0].Rows[0]["Resource_Name"].ToString();
                    isadmin = dstemp.Tables[0].Rows[0]["Isadmin"].ToString();
                    resourcekey = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
                    ppemail = dstemp.Tables[0].Rows[0]["Resource_Email"].ToString();

                    if (dstemp.Tables[0].Rows[0]["DoDirect"] != DBNull.Value)
                    {
                        if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["DoDirect"].ToString()))
                        {
                            dodirectpayment = "True";
                        }
                    }
                }
            }
            else //not admin/seller of store send them to demo page
            {
                storename = "Demo Store";
                isadmin = "True";
                resourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();
                ppemail = "kingbbj3@hotmail.com";
                isdemo = "True";
                dodirectpayment = "True";
                showwelcome = "3";
                try
                {
                    Send_Email se = new Send_Email();
                    se.demoemail(Convert.ToInt64(fbid));
                }
                catch
                {
                }
            }

            Eventomatic_DB.SPs.UpdateResourcePostResourcekey(Convert.ToInt64(fbid), Convert.ToInt32(resourcekey)).Execute();            

            /*
            if (sitetemp.HavePermission_Rkey(Convert.ToInt32(resourcekey)))
            {
                 hdhaveppauth.Value = "True";
            }
             */

            string hddemoresourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();

            if (resourcekey == hddemoresourcekey)
            {
                storename = "Demo Store";
                isadmin = "True";
                resourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();
                ppemail = "kingbbj3@hotmail.com";
                isdemo = "True";
                dodirectpayment = "True";
                showwelcome = "3";
                try
                {
                    Send_Email se = new Send_Email();
                    se.demoemail(Convert.ToInt64(fbid));
                }
                catch
                {
                }
            }

            bool boolisdemo = sitetemp.IsDemo_ResourceKey(Convert.ToInt32(resourcekey));
            if (!boolisdemo)
            {
                isdemo = "True";
            }

            currency = sitetemp.GetCurrency(Convert.ToInt32(resourcekey));

            strreturn += chr34 + "storename" + chr34 + ":" + chr34 + storename + chr34 + ",";
            strreturn += chr34 + "resourcekey" + chr34 + ":" + chr34 + resourcekey + chr34 + ",";
            strreturn += chr34 + "ppemail" + chr34 + ":" + chr34 + ppemail + chr34 + ",";
            strreturn += chr34 + "isadmin" + chr34 + ":" + chr34 + isadmin + chr34 + ",";
            strreturn += chr34 + "dodirectpayment" + chr34 + ":" + chr34 + dodirectpayment + chr34 + ",";
            strreturn += chr34 + "currency" + chr34 + ":" + chr34 + currency + chr34 + ",";
            strreturn += chr34 + "isdemo" + chr34 + ":" + chr34 + isdemo + chr34 + ",";
            strreturn += chr34 + "numberofstores" + chr34 + ":" + chr34 + numberofstores + chr34 + ",";
            strreturn += chr34 + "showwelcome" + chr34 + ":" + chr34 + showwelcome + chr34;
            strreturn += chr34 + "ipaddress" + chr34 + ":" + chr34 + HttpContext.Current.Request.UserHostAddress + chr34;

            strreturn += "}";
            return strreturn;
        }

        protected string initpage_username()
        {
            string strreturn = "{ ";

            string storename = "";
            string resourcekey = "0";
            string ppemail = "0";
            string isadmin = "False";
            string dodirectpayment = "False";
            string currency = "CAD";
            string isdemo = "False";
            string showwelcome = "0";
            string fbid = "0";
            /*string fbfirstname = "";
            string fblastname = "";
            string fbemail = "";
            string fbaccesstoken = "";*/
            string numberofstores = "0";
            string useremail = "";
            string userpassword = "";
            string userfirstname = "";
            string userlastname = "";

            Site sitetemp = new Site();

            if (Request.QueryString["email"] != null)
            {
                useremail = Request.QueryString["email"].ToString();
            }
            if (Request.QueryString["password"] != null)
            {
                userpassword = Request.QueryString["password"].ToString();
            }

            DataSet dstemp = Eventomatic_DB.SPs.ViewFBUserEmail(useremail).GetDataSet();
            if ((dstemp.Tables[0].Rows.Count > 0) && (useremail != ""))
            {
                //Email exists
                if (dstemp.Tables[0].Rows[0]["Password"].ToString() == userpassword)
                {
                    //password correct
                    //get fbid
                    fbid = dstemp.Tables[0].Rows[0]["FBid"].ToString();
                    userfirstname = dstemp.Tables[0].Rows[0]["First_Name"].ToString();
                    userlastname = dstemp.Tables[0].Rows[0]["Last_Name"].ToString();

                    //check what they are admin or seller of
                    dstemp = Eventomatic_DB.SPs.PfViewListFBUserAdminSellers(Convert.ToInt64(fbid)).GetDataSet();
                    numberofstores = dstemp.Tables[0].Rows.Count.ToString();
                    if (dstemp.Tables[0].Rows.Count > 0)
                    {
                        //check if requested specific resource_key
                        if ((Request.QueryString["resource_key"] != null) && (Request.QueryString["resource_key"] != ""))
                        {
                            //need to check if seller/admin of that store
                            string rskey = Request.QueryString["resource_key"];

                            if (rskey.Contains("isnative"))
                            {
                                rskey = rskey.Replace("isnative", "");
                            }

                            DataRow[] drtemp;
                            drtemp = dstemp.Tables[0].Select("resource_key = " + rskey);
                            if (drtemp.GetUpperBound(0) > -1) //at least one exists
                            {
                                storename = drtemp[0]["Resource_Name"].ToString();
                                string strisadmin = drtemp[0]["Isadmin"].ToString();
                                if (strisadmin == "1")
                                {
                                    isadmin = "True";
                                }
                                else
                                {
                                    isadmin = "False";
                                }
                                resourcekey = drtemp[0]["Resource_Key"].ToString();
                                ppemail = drtemp[0]["Resource_Email"].ToString();

                                resourcekey = rskey;

                                if (drtemp[0]["DoDirect"] != DBNull.Value)
                                {
                                    if (Convert.ToBoolean(drtemp[0]["DoDirect"].ToString()))
                                    {
                                        dodirectpayment = "True";
                                    }
                                }
                            }
                            else
                            {
                                storename = "Demo Store";
                                isadmin = "True";
                                resourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();
                                ppemail = "kingbbj3@hotmail.com";
                                isdemo = "True";
                                dodirectpayment = "True";
                                showwelcome = "3";
                                try
                                {
                                    Send_Email se = new Send_Email();
                                    se.demoemail(Convert.ToInt64(fbid));
                                }
                                catch
                                {
                                }
                            }
                        }
                        else //didn't request specific resource_key
                        {
                            //they are admin/seller of at least 1 store
                            storename = dstemp.Tables[0].Rows[0]["Resource_Name"].ToString();
                            isadmin = dstemp.Tables[0].Rows[0]["Isadmin"].ToString();
                            resourcekey = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
                            ppemail = dstemp.Tables[0].Rows[0]["Resource_Email"].ToString();

                            if (dstemp.Tables[0].Rows[0]["DoDirect"] != DBNull.Value)
                            {
                                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["DoDirect"].ToString()))
                                {
                                    dodirectpayment = "True";
                                }
                            }
                        }
                    }
                    else //not admin/seller of store send them to demo page
                    {
                        storename = "Demo Store";
                        isadmin = "True";
                        resourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();
                        ppemail = "kingbbj3@hotmail.com";
                        isdemo = "True";
                        dodirectpayment = "True";
                        showwelcome = "3";
                        try
                        {
                            Send_Email se = new Send_Email();
                            se.demoemail(Convert.ToInt64(fbid));
                        }
                        catch
                        {
                        }
                    }

                    Eventomatic_DB.SPs.UpdateResourcePostResourcekey(Convert.ToInt64(fbid), Convert.ToInt32(resourcekey)).Execute();

                    /*
                    if (sitetemp.HavePermission_Rkey(Convert.ToInt32(resourcekey)))
                    {
                         hdhaveppauth.Value = "True";
                    }
                     */

                    string hddemoresourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();

                    if (resourcekey == hddemoresourcekey)
                    {
                        storename = "Demo Store";
                        isadmin = "True";
                        resourcekey = ConfigurationSettings.AppSettings.Get("pf_demo_resourcekey").ToString();
                        ppemail = "kingbbj3@hotmail.com";
                        isdemo = "True";
                        dodirectpayment = "True";
                        showwelcome = "3";
                        try
                        {
                            Send_Email se = new Send_Email();
                            se.demoemail(Convert.ToInt64(fbid));
                        }
                        catch
                        {
                        }
                    }

                    bool boolisdemo = sitetemp.IsDemo_ResourceKey(Convert.ToInt32(resourcekey));
                    if (!boolisdemo)
                    {
                        isdemo = "True";
                    }

                    currency = sitetemp.GetCurrency(Convert.ToInt32(resourcekey));

                    strreturn += chr34 + "valid" + chr34 + ":" + chr34 + "true" + chr34 + ",";
                    strreturn += chr34 + "storename" + chr34 + ":" + chr34 + storename + chr34 + ",";
                    strreturn += chr34 + "resourcekey" + chr34 + ":" + chr34 + resourcekey + chr34 + ",";
                    strreturn += chr34 + "ppemail" + chr34 + ":" + chr34 + ppemail + chr34 + ",";
                    strreturn += chr34 + "isadmin" + chr34 + ":" + chr34 + isadmin + chr34 + ",";
                    strreturn += chr34 + "dodirectpayment" + chr34 + ":" + chr34 + dodirectpayment + chr34 + ",";
                    strreturn += chr34 + "currency" + chr34 + ":" + chr34 + currency + chr34 + ",";
                    strreturn += chr34 + "isdemo" + chr34 + ":" + chr34 + isdemo + chr34 + ",";
                    strreturn += chr34 + "numberofstores" + chr34 + ":" + chr34 + numberofstores + chr34 + ",";
                    strreturn += chr34 + "showwelcome" + chr34 + ":" + chr34 + showwelcome + chr34 + ",";
                    strreturn += chr34 + "fbid" + chr34 + ":" + chr34 + fbid + chr34 + ",";
                    strreturn += chr34 + "userfirstname" + chr34 + ":" + chr34 + userfirstname + chr34 + ",";
                    strreturn += chr34 + "userlastname" + chr34 + ":" + chr34 + userlastname + chr34 + ",";
                    strreturn += chr34 + "ipaddress" + chr34 + ":" + chr34 + HttpContext.Current.Request.UserHostAddress + chr34;                    

                    strreturn += "}";
                }
                else
                {
                    //incorrect password
                    strreturn += chr34 + "valid" + chr34 + ":" + chr34 + "false" + chr34 + "}";
                }
            }
            else
            {
                //Email does not exists
                strreturn += chr34 + "valid" + chr34 + ":" + chr34 + "false" + chr34 + "}";
            }


            
            return strreturn;
        }        

        protected string sendreceipt()
        {
            string strreturn = "";
            int txkey = 0;
            string ccemail = "";
            string longitude = "";
            string latitude = "";
            if (Request.QueryString["txkey"] != null)
            {
                txkey = Convert.ToInt32(Request.QueryString["txkey"].ToString());                
            }
            else if (Request.Form["txkey"] != null)
            {
                txkey = Convert.ToInt32(Request.Form["txkey"].ToString());             
            }
            
            if (Request.QueryString["ccemail"] != null)
            {
                ccemail = Request.QueryString["ccemail"].ToString();             
            }
            else if (Request.Form["ccemail"] != null)
            {
                ccemail = Request.Form["ccemail"].ToString();                
            }

            if (Request.QueryString["lat"] != null)
            {
                latitude = Request.QueryString["lat"].ToString();
            }
            else if (Request.Form["lat"] != null)
            {
                latitude = Request.Form["lat"].ToString();
            }

            if (Request.QueryString["long"] != null)
            {
                longitude = Request.QueryString["long"].ToString();
            }
            else if (Request.Form["long"] != null)
            {
                longitude = Request.Form["long"].ToString();
            }

            //update gps location
            Eventomatic_DB.SPs.UpdateGps(txkey, latitude, longitude).Execute();

            //Get amount & receipt id
            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(txkey).GetDataSet();
            decimal OverallTotal = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Amount"].ToString());
            string strReceiptID = dstemp.Tables[0].Rows[0]["Receipt_ID"].ToString();

            Site sitetemp = new Site();
            string Bizname = sitetemp.GetResourceNameTx(txkey);
            string bitlyurl = "";

            //get bitly url
            string thereceiptemail = HttpUtility.UrlEncode("http://www.thegroupstore.com/viewreceipt.aspx?receiptid=" + strReceiptID);

            string apikey = "R_690959f19386c7aa94fcf243ea002aee";
            string strlogin = "lornestar";
            string urlcall = "https://api-ssl.bitly.com/v3/shorten?login=" + strlogin + "&apiKey=" + apikey + "&longUrl=" + thereceiptemail + "&format=json";


            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string result = wc.DownloadString(urlcall);
            JObject o = JObject.Parse(result);
            JObject level1 = (JObject)o["data"];
            //JArray level1 = (JArray)o["data"];            

            string strnewurl = (string)level1["url"];
            bitlyurl =  " or click here " + strnewurl + ".";
            if (!strnewurl.Contains("bit.ly"))
            {
                bitlyurl = "";
            }

            if (Bizname.Length > 60)
            {
                Bizname = Bizname.Substring(0, 60);
            }

            Addons.Twilio sms = new Addons.Twilio();
            string thebody = "Your purchase from " + Bizname + " of $" + decimal.Round(OverallTotal, 2).ToString() + " is complete. Click here to view your Receipt " + strnewurl;
            sms.SendSMS(ccemail, thebody);

            try
            {
                
            }
            catch
            {
            }

            return strreturn;
        }
    }
}