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
using SubSonic;

namespace Eventomatic
{
    public partial class Tab2 : System.Web.UI.Page
    {
        string strReturn = "";
        Hashtable Tickets_Purchased = new Hashtable();

        protected void Page_Load(object sender, EventArgs e)
        {
            int txkey = 0;
            if (Request.QueryString["tx_key"] != null)
            {
                txkey = Convert.ToInt32(Request.QueryString["tx_key"]);
            }
            else if (Request.Form["tx_key"] != null)
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
                txkey = Convert.ToInt32(therighttxkey);
            }
            if (txkey != 0) //it is to forward to paypal
            {                
                
            }
            else //getting the tx_key
            {
                //string streventkey = Request.Form["event_key"].ToString();
                string streventkey = "";
                if (Request.Form["event_key"] != null)
                {
                    streventkey = Request.Form["event_key"].ToString();
                }
                else if (Request.QueryString["event_key"] != null)
                {
                    streventkey = Request.QueryString["event_key"].ToString();
                }

                string strtype = "";
                if (Request.Form["type"] != null)
                {
                    strtype = Request.Form["type"].ToString();
                }
                else if (Request.QueryString["type"] != null)
                {
                    strtype = Request.QueryString["type"].ToString();
                }

                //Free , Paypal or CC // CC = 0 / Paypal = 1 / Free = 2
                //string strtype = Request.Form["type"].ToString();
                
                string strsellfbid = "";
                string strbuyerfbid = "";
                //Seller fbid
                if (Request.Form["fb_sig_profile"] != null)
                {
                    strsellfbid = Request.Form["fb_sig_profile"].ToString();
                }
                else if (Request.QueryString["fb_sig_profile"] != null)
                {
                    strsellfbid = Request.QueryString["fb_sig_profile"].ToString();
                }

                //Buyer fbid
                if (Request.Form["fb_sig_user"] != null)
                {
                    strbuyerfbid = Request.Form["fb_sig_user"].ToString();
                }
                else if (Request.QueryString["fb_sig_user"] != null)
                {
                    strbuyerfbid = Request.QueryString["fb_sig_user"].ToString();
                }

                //Tickets & Quantities to buy  / ticket_key|quantity,ticket_key|quantity
                //string strtix = Request.Form["tix"].ToString();
                string strtix = "";
                if (Request.Form["tix"] != null)
                {
                    strtix = Request.Form["tix"].ToString();
                }
                else if (Request.QueryString["tix"] != null)
                {
                    strtix = Request.QueryString["tix"].ToString();
                } 

                //Guest list first name
                //string strfirstname = Request.Form["guestlist_firstname"].ToString();
                string strfirstname = "";
                if (Request.Form["guestlist_firstname"] != null)
                {
                    strfirstname = Request.Form["guestlist_firstname"].ToString();
                }
                else if (Request.QueryString["guestlist_firstname"] != null)
                {
                    strfirstname = Request.QueryString["guestlist_firstname"].ToString();
                }                 

                //Guest list last name
                //string strlastname = Request.Form["guestlist_lastname"].ToString();
                string strlastname = "";
                if (Request.Form["guestlist_lastname"] != null)
                {
                    strlastname = Request.Form["guestlist_lastname"].ToString();
                }
                else if (Request.QueryString["guestlist_lastname"] != null)
                {
                    strlastname = Request.QueryString["guestlist_lastname"].ToString();
                }

                //Guest list email
                //string stremail = Request.Form["guestlist_email"].ToString();
                string stremail = "";
                if (Request.Form["guestlist_email"] != null)
                {
                    stremail = Request.Form["guestlist_email"].ToString();
                }
                else if (Request.QueryString["guestlist_email"] != null)
                {
                    stremail = Request.QueryString["guestlist_email"].ToString();
                }

                //Total purchase
                //string strtotalamount = Request.Form["totalamount"].ToString();
                string strtotalamount = "";
                if (Request.Form["totalamount"] != null)
                {
                    strtotalamount = Request.Form["totalamount"].ToString();
                }
                else if (Request.QueryString["totalamount"] != null)
                {
                    strtotalamount = Request.QueryString["totalamount"].ToString();
                }                

                //Service Fee amount
                //string strservicefeeamount = Request.Form["servicefeeamount"].ToString();
                string strservicefeeamount = "";
                if (Request.Form["servicefeeamount"] != null)
                {
                    strservicefeeamount = Request.Form["servicefeeamount"].ToString();
                }
                else if (Request.QueryString["servicefeeamount"] != null)
                {
                    strservicefeeamount = Request.QueryString["servicefeeamount"].ToString();
                }

                strtotalamount = parseformstring(strtotalamount);
                strservicefeeamount = parseformstring(strservicefeeamount);
                stremail = parseformstring(stremail);
                streventkey = parseformstring(streventkey);
                strtix = parseformstring(strtix);
                strfirstname = parseformstring(strfirstname);
                strlastname = parseformstring(strlastname);
                strsellfbid = parseformstring(strsellfbid);
                strbuyerfbid = parseformstring(strbuyerfbid);
                strtype = parseformstring(strtype);
                

                try
                {
                    int tx_key = SetupTx(Convert.ToDecimal(strtotalamount), Convert.ToDecimal(strservicefeeamount), Convert.ToInt32(streventkey), strtix, strfirstname, strlastname, stremail, strsellfbid, strbuyerfbid);
                
                /*if (TicketsSoldOut()) //if it's soldout return tx_key=0
                {
                    strReturn += "0";
                }
                else //not sold out
                {*/
                if (strtype == "0") //pay cc
                {
                    Response.Redirect("https://www.thegroupstore.com/paycc.aspx?tx_key=" + tx_key.ToString() + "&fbid=" + strsellfbid);
                    //strReturn += "tx_key=" + tx_key.ToString();
                }
                else if (strtype == "1")//pay paypal
                {
                    gotopaypal(tx_key);
                    //strReturn += "tx_key=" + tx_key.ToString();
                }
                else if (strtype == "2")//free event
                {
                    strReturn += "tx_key=" + tx_key.ToString();
                }
                //}
                }
                catch
                {
                    strReturn = "strtotalamount=" + strtotalamount + " / strservicefeeamount=" + strservicefeeamount;
                }
                //find all post data
                string postdata = "";
                /*for (int i = 0; i < Request.Form.Count; i++)
                {
                    postdata += Request.Form.Keys[i].ToString() + " - " + Request.Form[i].ToString() + " // ";
                    //lblquery.Text += Request.QueryString.Keys[i].ToString() + " - " + Request.QueryString[i].ToString() + " \n ";
                }*/

                Response.Write(strReturn + "|" + postdata);
            }
        }

        protected string parseformstring(string strinput)
        {
            string strtemp = strinput;
            if (strtemp.Contains(","))
            {
                string[] words = strtemp.Split(',');
                strtemp = words[words.Length - 1];                
            }
            return strtemp;
        }

        protected bool TicketsSoldOut()//true = Sold out / false = Available
        {
            bool ticketssoldout = false;
            Site sitetemp = new Site();
            foreach (DictionaryEntry de in Tickets_Purchased)
            {
                if (sitetemp.IsSoldOut(Convert.ToInt32(de.Key)))
                {
                    ticketssoldout=true;
                }
            }
            return ticketssoldout;
        }

        protected int SetupTx(decimal OverallTotal, decimal ServiceFee, int Event_Key, string tixinfo, string guestlistfirstname, string guestlistlastname, string guestlistemail,string fbidseller, string fbidbuyer)
        {                    
            string PurchaseDescription = "";            

            //Figure out Purchase Description & Setup Hashtable
            string[] eachtix = tixinfo.Split(',');
            foreach (string eachone in eachtix)
            {
                string[] spliteachone = eachone.Split('|');
                int ticket_key = Convert.ToInt32(spliteachone.GetValue(0));
                int quantity = 0;
                //check if donate button or dropdown
                DataSet dstemp2 = Eventomatic_DB.SPs.ViewTicketSpecific2(ticket_key).GetDataSet();
                bool isdonation = false;
                if (dstemp2.Tables[0].Rows[0]["isdonation"] != DBNull.Value)
                {
                    if (dstemp2.Tables[0].Rows[0]["isdonation"].ToString().ToLower() == "true")
                    {
                        isdonation = true;
                    }
                }
                if (isdonation)
                {
                    //quantity = Convert.ToDecimal(spliteachone.GetValue(1));
                    Tickets_Purchased.Add(ticket_key.ToString(), Convert.ToDecimal(spliteachone.GetValue(1)).ToString() + "d");
                }
                else
                {
                    quantity = Convert.ToInt32(spliteachone.GetValue(1));
                    Tickets_Purchased.Add(ticket_key.ToString(), quantity.ToString());
                }
                
                
                if (PurchaseDescription.Length > 0)
                {
                    PurchaseDescription += ", ";
                }
                DataSet dstemp = Eventomatic_DB.SPs.ViewTicketSpecific2(ticket_key).GetDataSet();
                if (dstemp.Tables[0].Rows[0]["Ticket_Description"] != DBNull.Value)
                {
                    PurchaseDescription += dstemp.Tables[0].Rows[0]["Ticket_Description"].ToString();
                }                
            }
            string tempTx_Key = "0";
            bool IsFree = false;
            Site sitetemp = new Site();
            int resource_key = Convert.ToInt32(sitetemp.GetResourceKeyEventKey(Event_Key));
            string currency = sitetemp.GetCurrency(resource_key);

            if (OverallTotal > 0) //Money being spent
            {             
                StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "", "", 1, "", "", 0, "", "", "", "", "", "", "", guestlistfirstname, guestlistlastname, ServiceFee, HttpContext.Current.Request.UserHostAddress);
                sp_UpdateTransaction.Execute();
                tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
            }
            else if ((Tickets_Purchased.Count > 0) && (OverallTotal == 0))//Free Events
            {
                IsFree = true;
                //update Transaction & get tx_Key
                StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateTransaction(0, Event_Key, PurchaseDescription, decimal.Round(OverallTotal, 2), currency, 0, "", "", "",guestlistemail , 3, "", "", 0, "", "", "", guestlistemail, "", "", "", guestlistfirstname, guestlistlastname, 0, HttpContext.Current.Request.UserHostAddress);
                sp_UpdateTransaction.Execute();
                tempTx_Key = sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString();
            }
            //update fbids of buyer & seller            
            Int64 intfbidseller = 0;
            Int64 intfbidbuyer = 0;
            if (sitetemp.IsNumeric(fbidseller))
            {
                intfbidseller = Convert.ToInt64(fbidseller);
            }
            if (sitetemp.IsNumeric(fbidbuyer))
            {
                intfbidbuyer = Convert.ToInt64(fbidbuyer);
            }
            Eventomatic_DB.SPs.UpdateTransactionFbids(intfbidseller, intfbidbuyer, Convert.ToInt32(tempTx_Key)).Execute();


            //update Tickets Purchased
            foreach (DictionaryEntry de in Tickets_Purchased)
            {
                //it was donation box
                if (de.Value.ToString().Contains("d"))
                {
                    Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key),1, Convert.ToDecimal(de.Value.ToString().Replace("d","")),0,"","").Execute();
                }
                else
                {
                    Eventomatic_DB.SPs.UpdateTicketsPurchased(Convert.ToInt32(tempTx_Key), Convert.ToInt32(de.Key), Convert.ToInt32(de.Value), 0, 0, "", "").Execute();
                }
                
            }
            if (IsFree)
            {
                Eventomatic_DB.SPs.UpdateTicketNumFreeEvents(Convert.ToInt32(tempTx_Key)).Execute();
            }

            return Convert.ToInt32(tempTx_Key);
        }

        protected void gotopaypal(int txkey)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(txkey).GetDataSet();
            int Event_Key = Convert.ToInt32(dstemp.Tables[0].Rows[0]["Event_Key"].ToString());
            decimal servicefee = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Service_Fee"].ToString());
            decimal overalltotal = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Amount"].ToString());
            Site Sitetemp = new Site();
            string strcurrency = Sitetemp.GetResourceCurrency(Event_Key);
            decimal ServiceFeeAmount = servicefee;
            decimal HostAmount = overalltotal - ServiceFeeAmount;
            Boolean isdemovar = Sitetemp.IsDemo(Event_Key);
            string strHostEmail = strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("Trial_Email").ToString();
            string strServiceFeeEmail = "";
            if (!isdemovar)
            {
                strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
            }
            else
            {
                strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString();
            }

            dstemp = Eventomatic_DB.SPs.ViewPaypalEmail(Event_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)
            {
                strHostEmail = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
            }

            Eventomatic.Addons.PaypalMethods paytemp = new Eventomatic.Addons.PaypalMethods();
            string strNotetemp = "";
            dstemp = Eventomatic_DB.SPs.ViewEventDetails(Event_Key).GetDataSet();
            string streventname = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
            if (Sitetemp.isAlphaNumeric(streventname))
            {
                strNotetemp += streventname + " has received payment."; ;
            }
            else
            {
                strNotetemp += "Your event has received a payment.";
            }
            paytemp.ParallelPayment(!isdemovar, strcurrency, strNotetemp, HostAmount, strHostEmail, ServiceFeeAmount, strServiceFeeEmail, txkey, Event_Key);
        }
    }
}
