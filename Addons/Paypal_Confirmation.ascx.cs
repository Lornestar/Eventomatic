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

namespace Eventomatic.Addons
{
    public partial class Paypal_Confirmation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadEmail();            
        }

        public void LoadEmail()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewPaypalConfirmation(hdemail.Value).GetDataSet();

            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Date_Sent"] != DBNull.Value)
                {
                    lbldatesent.Text = dstemp.Tables[0].Rows[0]["Date_Sent"].ToString();
                }
            }

            if (isemailconfirmed(hdemail.Value) == 2)
            {
                pnlisconfirmed.Visible = true;
                pnlnotconfirmed.Visible = false;
            }
            if (hdemail.Value == "")
            {
                pnlisconfirmed.Visible = false;
                pnlnotconfirmed.Visible = false;
            }
        }

        public int isemailconfirmed(string emailaddress)
        {// 0 = not confirmed & not in db / 1 not confirmed & in db / 2 = is confirmed & in db
            int thereturn = 0;
            DataSet dstemp = Eventomatic_DB.SPs.ViewPaypalConfirmation(emailaddress.ToLower()).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                thereturn = 1;
                if (dstemp.Tables[0].Rows[0]["Confirmed"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Confirmed"]))
                    {
                        thereturn = 2;
                    }
                }
            }            
            return thereturn;
        }

        public void enteredemail(string emailaddress, string strCurrency, int resource_key)
        {
            if (isemailconfirmed(emailaddress) == 0) //not in db & not confirmed
            {
                sendconfirmation(emailaddress, strCurrency, resource_key);
            }
        }

        protected void btnenteramount_Click(object sender, EventArgs e)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewPaypalConfirmation(hdemail.Value).GetDataSet();

            decimal correctamount = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Amount_Sent"]);
            if (correctamount == Convert.ToDecimal(Amount_Sent.Value)) //correct amount
            {
                Eventomatic_DB.SPs.UpdatePaypalConfirmation(hdemail.Value.ToLower(),1,0).Execute();
                lblincorrect.Visible = false;
                pnlisconfirmed.Visible = true;
                pnlnotconfirmed.Visible = false;
            }
            else //incorrect amount
            {
                lblincorrect.Visible = true;
            }
        }

        public void sendconfirmation(string emailaddress, string strCurrency, int resource_key)
        {
            decimal amounttosend = 0;
            Random random = new Random();
            amounttosend = Math.Round(Convert.ToDecimal(0.01) + Convert.ToDecimal(0.02 * random.NextDouble()),2);            


            string strSubject = "Groupstore Confirming your PayPal email address";

            string[] strPaypalEmail = new String[1];
            strPaypalEmail.SetValue(emailaddress, 0);
            
            string[] strValue = new String[1];
            strValue.SetValue(amounttosend.ToString(), 0);

            string[] strUniqueID = new String[1];
            string[] strNote = new String[1];
            strNote.SetValue("Groupstore Confirming your PayPal email address", 0);

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

            Site sitetemp = new Site();
            Boolean isdemovar = sitetemp.IsDemo_ResourceKey(resource_key);

            Eventomatic.Addons.PaypalMethods paytemp = new Eventomatic.Addons.PaypalMethods();
            string strMassPayAck = "Not yet";
            strMassPayAck = paytemp.MassPayCode(strSubject, com.paypal.soap.api.ReceiverInfoCodeType.EmailAddress, strPaypalEmail, strValue, strUniqueID, strNote, tempCurrency, 1, 0, isdemovar);
            
            if (strMassPayAck.Contains("Success"))
            {
                Eventomatic_DB.SPs.UpdatePaypalConfirmation(emailaddress.ToLower(), 0, amounttosend).Execute();
            }
        }
    }
}