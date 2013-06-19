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
//using com.paypal.soap.api;

namespace Eventomatic.Addons
{
    public partial class Payment_Details : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*Eventomatic.Addons.PaypalMethods PayDet = new Eventomatic.Addons.PaypalMethods();
                GetTransactionDetailsResponseType Detailstemp = PayDet.GetTransactionDetailsCode(Txn_Key.Value);
                if (Detailstemp.Ack.ToString().Contains("Success"))
                {
                    lblTxID.Text = Txn_Key.Value;
                    lblName.Text = Detailstemp.PaymentTransactionDetails.PayerInfo.PayerName.FirstName + " " + Detailstemp.PaymentTransactionDetails.PayerInfo.PayerName.LastName;
                    lblAmount.Text = Detailstemp.PaymentTransactionDetails.PaymentInfo.GrossAmount.Value + " " + Detailstemp.PaymentTransactionDetails.PaymentInfo.GrossAmount.currencyID.ToString();
                    lblDate.Text = Detailstemp.PaymentTransactionDetails.PaymentInfo.PaymentDate.AddHours(-3).ToString("dddd, MMMM d yyyy h:mm tt") + " EST";

                    DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetailsTxnid(Txn_Key.Value).GetDataSet();
                    if (dstemp.Tables[0].Rows[0]["payer_email"] != DBNull.Value)
                    {
                        lblEmail.Text = dstemp.Tables[0].Rows[0]["payer_email"].ToString();
                    }
                    //lblEmail.Text = Detailstemp.PaymentTransactionDetails.PayerInfo.PayerBusiness.ToString();
                }*/
                DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(Convert.ToInt32(Tx_Key.Value)).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {                    
                    lblAmount.Text = "$" + decimal.Round(Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Amount"].ToString()),2);
                    lblDate.Text = dstemp.Tables[0].Rows[0]["Confirmation_Date"].ToString();                    
                    lblEmail.Text = dstemp.Tables[0].Rows[0]["payer_email"].ToString();
                    if ((dstemp.Tables[0].Rows[0]["first_name"].ToString() != "") && (dstemp.Tables[0].Rows[0]["last_name"].ToString()!= ""))
                    {
                        lblName.Text = dstemp.Tables[0].Rows[0]["first_name"].ToString() + " " + dstemp.Tables[0].Rows[0]["last_name"].ToString();
                    }
                    else
                    {
                        lblBuyertext.Visible = false;
                    }
                    
                }
                
                                              
            }
        }
    }
}