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
    public partial class Transaction_Details : System.Web.UI.Page
    {
        int Tx_Key = 0;  
        protected void Page_Load(object sender, EventArgs e)
        {

            
                if ((Request.QueryString["Tx_Key"] != null) && (Request.QueryString["Tx_Key"] != ""))
                {
                    Tx_Key = Convert.ToInt32(Request.QueryString["Tx_Key"].ToString());
                    if (!IsPostBack)
                    { PopulateExisting(Tx_Key); }

                }            
            
        }

        protected void PopulateExisting(int Tx_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionDetails(Tx_Key).GetDataSet();
            
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                lbltxn_id.Text = dstemp.Tables[0].Rows[0]["txn_id"].ToString();
                lblamount.Text = "$" + dstemp.Tables[0].Rows[0]["Amount"].ToString();
                lblbuyer.Text = dstemp.Tables[0].Rows[0]["first_name"].ToString() + dstemp.Tables[0].Rows[0]["last_name"].ToString();
                lblbuyer_email.Text = dstemp.Tables[0].Rows[0]["payer_email"].ToString();
                lbldate.Text = dstemp.Tables[0].Rows[0]["Confirmation_Date"].ToString();
            }
        }

        protected void btnsendemail_Click(object sender, EventArgs e)
        {
            Send_Email SE = new Send_Email();
            SE.Send_Transaction_Email(Tx_Key, "");
        }

    }
}
