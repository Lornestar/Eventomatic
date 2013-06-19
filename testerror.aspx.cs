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

namespace Eventomatic
{
    public partial class testerror : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int inttest = Convert.ToInt32("test");
        }

        protected void btntest_Click(object sender, EventArgs e)
        {
            //create error
            int inttest = Convert.ToInt32("test");
           /* Hashtable Paypal_Vars = new Hashtable();
            string strRequest = "transaction%5B1%5D.status_for_sender_txn=Completed&payment_request_date=Wed+Dec+23+14%3A29%3A29+PST+2009&return_url=http%3A//dev.sellitallstore.com/PaymentDetails.aspx&fees_payer=EACHRECEIVER&ipn_notification_url=http%3A//dev.sellitallstore.com/IPNing.aspx%3FTx_Key%3D8%26Paytype%3DAP&transaction%5B1%5D.status=Completed&transaction%5B1%5D.id_for_sender_txn=1CB87348DJ699363C&sender_email=lorneb_1242411941_per%40lornestar.com&verify_sign=AePo0NLqhqHe1CSf8-TXxpNuuJ3aAmf4Mk44DKG1oMPX7AKCMsgEEvJf&transaction%5B1%5D.amount=CAD+1.08&test_ipn=1&transaction%5B0%5D.id_for_sender_txn=9LW58486WR9882046&transaction%5B0%5D.receiver=evento_1252204669_biz%40lornestar.com&cancel_url=http%3A//dev.sellitallstore.com/SetPayParallel.aspx&transaction%5B1%5D.is_primary_receiver=false&transaction%5B0%5D.is_primary_receiver=false&pay_key=AP-14N39990E11620246&action_type=PAY&transaction%5B0%5D.id=7F360341AN015901G&memo=EventTicket&transaction%5B0%5D.status=Completed&transaction%5B1%5D.receiver=lorne_1261162854_biz%40lornestar.com&transaction%5B0%5D.status_for_sender_txn=Completed&transaction_type=Adaptive+Payment+PAY&transaction%5B0%5D.amount=CAD+20.00&status=COMPLETED&transaction%5B1%5D.id=6WX208913P565164B&log_default_shipping_address_in_transaction=false&charset=windows-1252&notify_version=UNVERSIONED&reverse_all_parallel_payments_on_error=false";
            string[] strRequestSplit = strRequest.Split('&');
            string[] strtemp;
            string[] strtempAmount;
            string Paytype = "AP";
            decimal dcAmountTotal = 0;
            Site sitetemp = new Site();
            foreach (string word in strRequestSplit)
            {
                strtemp = word.Split('=');
                Paypal_Vars.Add(strtemp[0], strtemp[1]);
                if ((Paytype == "AP") && (strtemp[0].Contains("amount")))
                {
                    strtempAmount = strtemp[1].Split('+');
                    if (sitetemp.IsNumeric(strtempAmount[1]))
                    {
                        dcAmountTotal += decimal.Parse(strtempAmount[1]);
                    }
                }
            }*/
        }
    }
}
