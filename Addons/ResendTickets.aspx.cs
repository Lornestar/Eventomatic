using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace Eventomatic.Addons
{
    public partial class ResendTickets : System.Web.UI.Page
    {
        int txkey = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["txkey"] != null)
            {
                txkey = Convert.ToInt32(Request.QueryString["txkey"]);
            }
            if (!IsPostBack)
            {
                string strpayer_email = "";
                DataSet dstemp = Eventomatic_DB.SPs.ViewEmailReceipt(txkey).GetDataSet();
                if (dstemp.Tables[0].Rows[0]["payer_email"] != DBNull.Value)
                { strpayer_email = dstemp.Tables[0].Rows[0]["payer_email"].ToString(); }
                lblresendemail.Text = strpayer_email;
            }
        }

        protected void btnSaveTicket_Click(object sender, EventArgs e)
        {
            Send_Email SE = new Send_Email();
            Hashtable hstemp = SE.Getpurchasedtixinfo(txkey, "");
            string[] strcc = new string[1];
            strcc.SetValue(txtccemail.Text,0);
            string[] strbcc = new string[1];
            SE.Send_Email_Function2("", hstemp["strpayer_email"].ToString(), "Ticket Purchase Confirmation", hstemp["thebody"].ToString(),strcc,strbcc,txkey);


            RadAjaxPanel1.ResponseScripts.Add(string.Format("alert('The eticket has been resent');CloseWindow();return false;", ""));
        }
        
    }
}