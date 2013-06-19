using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace Eventomatic
{
    public partial class demopay : System.Web.UI.Page
    {
        int Event_Key = 0;
        int Tx_Key = 0;
        Int64 fbid = 0;
        Site sitetemp = new Site();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["Tx_Key"] != null) && (Request.QueryString["Tx_Key"] != ""))
            {
                Tx_Key = Convert.ToInt32(Request.QueryString["Tx_Key"].ToString());
                Event_Key = Convert.ToInt32(sitetemp.GetEventKeyTx(Tx_Key));
            }
            else if ((Request.Form["Tx_Key"] != null) && (Request.Form["Tx_Key"] != ""))
            {
                Tx_Key = Convert.ToInt32(Request.Form["Tx_Key"].ToString());
                Event_Key = Convert.ToInt32(sitetemp.GetEventKeyTx(Tx_Key));
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["gsdemo"] == "true")
                {
                    hdisdemogeneric.Value = "1";
                }      
                fbloggedin();
                DataSet dstemp = Eventomatic_DB.SPs.ViewPaypalEmail(Event_Key).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    if (dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value)
                    {
                        txtPayPal.Text = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
                    }
                }                
            }            
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Eventomatic_DB.SPs.UpdateTransactionDemoPay(Tx_Key).Execute();            
            string strnewurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Confirmation.aspx?demo=true&tx_key=" + Tx_Key;
            if (hdisdemogeneric.Value == "1")
            {
                strnewurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Confirmation.aspx?demo=true&gsdemo=true&tx_key=" + Tx_Key;
            }
            if (txteticket.Text != "")
            {
                Eventomatic_DB.SPs.UpdateTransactionDemoPayPayeremail(Tx_Key, txteticket.Text).Execute();
                Send_Email SE = new Send_Email();
                SE.Send_Transaction_Email(Tx_Key,"");                
            }
            if (txtPayPal.Text != "")
            {
                string strhasdonebefore = sitetemp.Hasdone_DemoPay(txtPayPal.Text);
                if (strhasdonebefore == "")
                {
                    //first time
                    string strcurrency = sitetemp.GetResourceCurrency(Event_Key);
                    decimal amountsend = Convert.ToDecimal(0.02);
                    Eventomatic.Addons.PaypalMethods paytemp = new Eventomatic.Addons.PaypalMethods();
                    bool islive = false;
                    if (strnewurl.Contains("thegroupstore.com"))
                    {
                        islive = true;
                    }
                    else
                    {
                        islive = false;
                    }
                    //paytemp.ImplicitPayment(true, strcurrency, "Groupstore Demo Ticket", amountsend, txtPayPal.Text, Tx_Key, Event_Key);

                    com.paypal.soap.api.CurrencyCodeType[] tempCurrency = new com.paypal.soap.api.CurrencyCodeType[1];
                    tempCurrency.SetValue(com.paypal.soap.api.CurrencyCodeType.USD, 0);

                    string[] strUniqueID = new String[1];
                    string[] strNote = new String[1];
                    strNote.SetValue("Groupstore Demo", 0);

                    string[] strValue = new String[1];
                    strValue.SetValue(amountsend.ToString(), 0);
                    
                    string[] strPaypalEmail = new String[1];
                    strPaypalEmail.SetValue(txtPayPal.Text, 0);
                    

                    string strMassPayAck = "Not yet";
                    strMassPayAck = paytemp.MassPayCode("Groupstore Demo", com.paypal.soap.api.ReceiverInfoCodeType.EmailAddress, strPaypalEmail, strValue, strUniqueID, strNote, tempCurrency, 1, 0, true);
            


                    Eventomatic_DB.SPs.UpdatePayPalDemoPay(txtPayPal.Text, amountsend).Execute();
                    RadAjaxPanel1.ResponseScripts.Add(string.Format("hasdonedemopaynomsg('" + strnewurl + "')", ""));
                }
                else
                {
                    //has done before
                    RadAjaxPanel1.ResponseScripts.Add(string.Format("hasdonedemopay('" + strhasdonebefore + "','" + strnewurl + "')", ""));
                }
            }
            else
            {
                RadAjaxPanel1.ResponseScripts.Add(string.Format("hasdonedemopaynomsg('" + strnewurl + "')", ""));
            }            
        }

        
        protected void fbloggedin()
        {
            if (HttpContext.Current.Session["fbuser"] == null) //No authorization or fbuid
            {
                Eventomatic.Addons.ConnectService fbconnect = new Eventomatic.Addons.ConnectService();
                if (fbconnect.IsConnected()) //have cookie & connected
                {
                    fbid = fbconnect.UserId;
                }
            }
            else
            {
                Eventomatic.Addons.fbuser fbuser;
                fbuser = (Eventomatic.Addons.fbuser)HttpContext.Current.Session["fbuser"];
                fbid = fbuser.UID;
            }
            if (fbid != 0)
            {                
                fbloggedin1.Visible = true;                                
                fbloggedin1.Setuser(fbid);                
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (e.Argument == "InitialPageLoad")
            {
                //simulate longer page load
                System.Threading.Thread.Sleep(2000);
            }
        } 

        protected void btntest_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }
    }
}