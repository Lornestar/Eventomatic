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

namespace Eventomatic.Addons
{
    public partial class SendMoney : System.Web.UI.UserControl
    {
        Site sitetemp = new Site();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "btnSendMoney")
            {
                btnSendMoney();
            }
        }

        public void LoadInfo(string strtemp,int resource_key, string fbid)
        {
            hdamount.Value = strtemp;
            hdresource_key.Value = resource_key.ToString();
            hdfbid.Value = fbid;
            Hashtable hstemp = sitetemp.GetRevenue_Hashtable_Decode(strtemp);

            GridView1.DataSource = dsamount(hstemp);
            GridView1.DataBind();

            //get email
            string strcurrentpage = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            if (strcurrentpage.ToLower().Contains("referral.aspx"))
            {
                DataSet dstemp = Eventomatic_DB.SPs.ViewFbUsers(Convert.ToInt64(fbid)).GetDataSet();
                lbltoemail.Text = dstemp.Tables[0].Rows[0]["Referral_Email"].ToString();
                lblpaypalemail.Text = lbltoemail.Text;
            }
            else
            {
                DataSet dstemp = Eventomatic_DB.SPs.ViewResource(resource_key).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)//have paypal email address
                    {
                        lbltoemail.Text = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
                        lblpaypalemail.Text = lbltoemail.Text;
                    }
                }
            }            

        }

        protected DataTable dsamount(Hashtable hstemp)
        {
            DataTable dttemp = new DataTable();
            
            dttemp.Columns.Add("Currency",typeof(string));
            dttemp.Columns.Add("Owed", typeof(string));
            dttemp.Columns.Add("Fee", typeof(string));
            dttemp.Columns.Add("Amount", typeof(string));

            foreach (DictionaryEntry de in hstemp)
            {
                if (Convert.ToDecimal(de.Value) > 0)
                {
                    decimal Owed = Convert.ToDecimal(de.Value);
                    decimal MassPayFee = sitetemp.MassPaymentFeeCalculate(Owed);
                    decimal MassPayAmount = Owed + MassPayFee;
                    string strsymbol = "$";
                    if (de.Key.ToString() == "EUR")
                    {
                        strsymbol = "€";
                    }
                    else if (de.Key.ToString() == "GBP")
                    {
                        strsymbol = "£";
                    }
                    else if (de.Key.ToString() == "ILS")
                    {
                        strsymbol = "₪";
                    }
                    dttemp.Rows.Add(de.Key, strsymbol + Owed, strsymbol + MassPayFee, strsymbol + MassPayAmount);
                }
            }

            return dttemp;            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btnSendMoney()
        {
            int resource_key = Convert.ToInt32(hdresource_key.Value);
            Int64 fbid = Convert.ToInt64(hdfbid.Value);
            int txouttype = 0;

            Boolean isdemovar = sitetemp.IsDemo_ResourceKey(resource_key);

            string strcurrentpage = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            DataSet dsEventList;
            Hashtable hsOwedSeparate;
            Hashtable hsAmountSent = new Hashtable();
            if (strcurrentpage.ToLower().Contains("referral.aspx")) //referral
            {
                dsEventList = Eventomatic_DB.SPs.ViewListAllEventsReferral(fbid).GetDataSet();
                txouttype = 1;
                hsOwedSeparate = sitetemp.GetAmount_Owed_Referral(fbid);
            }
            else //resource paying
            {
                dsEventList = Eventomatic_DB.SPs.ViewListAllEventsProfile(resource_key).GetDataSet();
                hsOwedSeparate = sitetemp.GetAmount_Owed(resource_key);
            }


            

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                string strCurrency = gvr.Cells[0].Text;
                com.paypal.soap.api.CurrencyCodeType[] tempCurrency = new com.paypal.soap.api.CurrencyCodeType[1];
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

                string strAmount = gvr.Cells[3].Text;
                strAmount = strAmount.Replace("$", "");
                strAmount = strAmount.Replace("€", "");
                strAmount = strAmount.Replace("£", "");
                strAmount = strAmount.Replace("₪", "");

                string[] strValue = new String[1];
                strValue.SetValue(strAmount, 0);
                string[] strUniqueID = new String[1];
                string[] strNote = new String[1];
                strNote.SetValue("Transfer of money owed to you by Groupstore.  The transfer was initiated by an administrator of your Groupstore",0);

                string strFee = gvr.Cells[2].Text;
                strFee = strFee.Replace("$", "");
                strFee = strFee.Replace("€", "");
                strFee = strFee.Replace("£", "");
                strFee = strFee.Replace("₪", "");
                strFee = strFee.Replace("-", "");
                
                string[] strPaypalEmail = new String[1];
                if (lbltoemail.Text != "")
                {
                    strPaypalEmail.SetValue(lbltoemail.Text, 0);
                }
                else
                {
                    if (isdemovar)
                    {
                        strPaypalEmail.SetValue(System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString(), 0);
                    }
                    else
                    {
                        strPaypalEmail.SetValue(System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString(), 0);
                    }
                }
                

                string strSubject = "Money from Groupstore";                                
                Eventomatic.Addons.PaypalMethods paytemp = new Eventomatic.Addons.PaypalMethods();
                string strMassPayAck = "Not yet";

                //double check to make sure money being sent is owed
                decimal checkowed = Convert.ToDecimal(hsOwedSeparate[strCurrency]) - (Convert.ToDecimal(strAmount) + Convert.ToDecimal(strFee));
                if (checkowed >= 0)
                {
                    strMassPayAck = paytemp.MassPayCode(strSubject, com.paypal.soap.api.ReceiverInfoCodeType.EmailAddress, strPaypalEmail, strValue, strUniqueID, strNote, tempCurrency, 1, 0, isdemovar);
                    hsAmountSent.Add(strAmount + strCurrency, "Error");
                }                

                if (strMassPayAck.Contains("Success"))
                {
                    bool FirstTime = true;
                    hsAmountSent[strAmount + strCurrency] = "Sent";
                    decimal Accountedfor = 0;
                    Hashtable hsOwedTotal = sitetemp.GetRevenue_Hashtable_Empty();
                    foreach (DataRow dr in dsEventList.Tables[0].Rows)
                    {
                        int event_key = 0;
                        int resource_key_temp = 0;
                        Hashtable hsCollectedtemp;
                        Hashtable hsPaidtemp;
                        if (strcurrentpage.ToLower().Contains("referral.aspx")) //referral
                        {
                            resource_key_temp = Convert.ToInt32(dr["Resource_Key"]);
                            hsCollectedtemp = sitetemp.GetRevenue_Hashtable(resource_key_temp, 2);
                            hsPaidtemp = sitetemp.GetRevenue_Hashtable(resource_key_temp, 3);
                        }
                        else
                        {
                            event_key = Convert.ToInt32(dr["Event_Key"]);
                            hsCollectedtemp = sitetemp.GetRevenue_Hashtable(event_key, 0);
                            hsPaidtemp = sitetemp.GetRevenue_Hashtable(event_key, 1);
                        }
                        
                        
                        Hashtable hsOwedtemp = sitetemp.GetRevenue_Hashtable_Empty();
                        foreach (DictionaryEntry de in hsCollectedtemp)
                        {
                            hsOwedtemp[de.Key.ToString()] = Convert.ToDecimal(hsCollectedtemp[de.Key.ToString()]) - Convert.ToDecimal(hsPaidtemp[de.Key.ToString()]);
                            hsOwedTotal[de.Key.ToString()] = Convert.ToDecimal(hsOwedtemp[de.Key.ToString()]) + Convert.ToDecimal(hsOwedTotal[de.Key.ToString()]);                            
                        }

                        decimal diffamount2 = Convert.ToDecimal(strFee) + Convert.ToDecimal(strAmount) - Accountedfor;
                        if ((Convert.ToDecimal(hsOwedtemp[strCurrency]) > 0) && (diffamount2 > 0 ))
                        {                            
                            decimal claimpaid = 0;
                            decimal diffamount = Convert.ToDecimal(strFee) + Convert.ToDecimal(strAmount) - Convert.ToDecimal(hsOwedtemp[strCurrency]) - Accountedfor;
                            if (diffamount >= 0)
                            {
                                claimpaid = Convert.ToDecimal(hsOwedtemp[strCurrency]);
                            }
                            else
                            {
                                claimpaid = Convert.ToDecimal(strFee) + Convert.ToDecimal(strAmount) - Accountedfor;
                            }
                            Accountedfor += claimpaid;

                            int tx_out_key = 0;

                            if (FirstTime)
                            {
                                if (strcurrentpage.ToLower().Contains("referral.aspx")) //referral
                                {
                                   StoredProcedure sp_Txout = Eventomatic_DB.SPs.UpdateTransactionOut(claimpaid, strPaypalEmail[0], resource_key_temp, 0, Convert.ToDecimal(strFee), tempCurrency[0].ToString(), Convert.ToInt64(hdfbid.Value), txouttype,0);
                                    sp_Txout.Execute();
                                    tx_out_key = Convert.ToInt32(sp_Txout.Command.Parameters[8].ParameterValue.ToString());
                                }
                                else
                                {
                                    StoredProcedure sp_Txout = Eventomatic_DB.SPs.UpdateTransactionOut(claimpaid, strPaypalEmail[0], resource_key, event_key, Convert.ToDecimal(strFee), tempCurrency[0].ToString(), Convert.ToInt64(hdfbid.Value), txouttype,0);
                                    sp_Txout.Execute();
                                    tx_out_key = Convert.ToInt32(sp_Txout.Command.Parameters[8].ParameterValue.ToString());
                                }
                            }
                            else
                            {
                                if (strcurrentpage.ToLower().Contains("referral.aspx")) //referral
                                {
                                    StoredProcedure sp_Txout = Eventomatic_DB.SPs.UpdateTransactionOut(claimpaid, strPaypalEmail[0], resource_key_temp, 0, Convert.ToDecimal(0), tempCurrency[0].ToString(), Convert.ToInt64(hdfbid.Value), txouttype,0);
                                    sp_Txout.Execute();
                                    tx_out_key = Convert.ToInt32(sp_Txout.Command.Parameters[8].ParameterValue.ToString());
                                }
                                else
                                {
                                    StoredProcedure sp_Txout = Eventomatic_DB.SPs.UpdateTransactionOut(claimpaid, strPaypalEmail[0], resource_key, event_key, Convert.ToDecimal(0), tempCurrency[0].ToString(), Convert.ToInt64(hdfbid.Value), txouttype,0);
                                    sp_Txout.Execute();
                                    tx_out_key = Convert.ToInt32(sp_Txout.Command.Parameters[8].ParameterValue.ToString());
                                }
                            }
                            FirstTime = false;

                            //Record Activity
                            Eventomatic.Addons.Activities activity = new Activities();
                            activity.NewActivity(4, resource_key, fbid, 0, event_key,tx_out_key);
                        }
                    }                    
                }
            }

            //Transaction complete
            TransactionComplete(hsAmountSent);
        }

        protected void TransactionComplete(Hashtable hsAmountSent)
        {
            pnlresult.Visible = true;
            pnlsendmoney.Visible = false;
            DataTable dttemp = new DataTable();
            string strDetails = "The following amount has been sent to" + lblpaypalemail.Text + "</br>";
            strDetails += "<table><tr><td>Amount to send</td><td>Result</td></tr>";
            dttemp.Columns.Add("Amount", typeof(string));
            dttemp.Columns.Add("Result", typeof(string));
            foreach (DictionaryEntry de in hsAmountSent)
            {
                strDetails += "<tr><td>" + de.Key + "</td><td>" + de.Value+ "</td></tr>";
                dttemp.Rows.Add(de.Key, de.Value);
            }
            GridView2.DataSource = dttemp;
            GridView2.DataBind();

            sitetemp.txout_EmailTransfer(strDetails);
        }
        

    }
}