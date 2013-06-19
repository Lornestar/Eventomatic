using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

namespace Eventomatic
{
    public partial class viewreceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerror.Visible = false;
            if (Request.QueryString["receiptid"] != null)
            {
                string strreceiptid = Request.QueryString["receiptid"];
                string strtxkey = "";
                int txkey = 0;
                if ((strreceiptid.Length == 9) || (strreceiptid.Length == 8))
                {
                    strtxkey = strreceiptid.Substring(4);
                    txkey = Convert.ToInt32(strtxkey);
                }
                
                DataSet dstemp = Eventomatic_DB.SPs.PfViewTransactionDetailsTxkey(txkey).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)  //it exists
                {
                    if (dstemp.Tables[0].Rows[0]["Receipt_ID"].ToString().ToUpper() == strreceiptid)
                    {
                        Send_Email se = new Send_Email();
                        Hashtable hstemp = se.GetPurchasedPayForwardinfo(txkey);

                        lblthereceipt.Text = hstemp["thebody"].ToString();

                        pnlpartone.Visible = false;
                        pnlparttwo.Visible = true;

                        lblerror.Visible = false;
                 
                    }
                }
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            Site sitetemp = new Site();
            bool receiptexists = false;


            string strtxkey = "";
            if (txtreceiptkey.Text.Length == 8)
            {
                strtxkey = txtreceiptkey.Text.Substring(4);
            }
                
            if (sitetemp.IsNumeric(strtxkey))
            {
                int txkey = Convert.ToInt32(strtxkey);

                DataSet dstemp = Eventomatic_DB.SPs.PfViewTransactionDetailsTxkey(txkey).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)  //it exists
                {
                    if (dstemp.Tables[0].Rows[0]["Receipt_ID"].ToString().ToUpper() == txtreceiptkey.Text.ToUpper())
                    {
                        Send_Email se = new Send_Email();
                        Hashtable hstemp = se.GetPurchasedPayForwardinfo(txkey);

                        lblthereceipt.Text = hstemp["thebody"].ToString();

                        pnlpartone.Visible = false;
                        pnlparttwo.Visible = true;

                        lblerror.Visible = false;

                        receiptexists = true;
                    }                    
                }
                
            }
            
            if (!receiptexists)
            {
                lblerror.Visible = true;
            }
        }
    }
}