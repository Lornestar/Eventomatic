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
    public partial class View_Cart : System.Web.UI.Page
    {
        int Resource_Key = 0;
        int tx_key = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["Storeid"] != null) && (Request.QueryString["Storeid"] != ""))
            {
                Resource_Key = Convert.ToInt32(Request.QueryString["Storeid"].ToString());
                if (!IsPostBack)
                {                    
                    FillForm();
                }
                else
                {
                    if (Request.Form["__EVENTTARGET"] == "DoRemove2")
                    {
                        DoRemove(Request["__EVENTARGUMENT"].ToString());
                    }
                }
            }

        }

        protected void DoRemove(string Rowid)
        {
            int ticket_key = Convert.ToInt32(Rowid);
            tx_key = Convert.ToInt32(hdtx_key.Value);
            Site sitetemp = new Site();
            sitetemp.RemoveTicketFromCart(tx_key, ticket_key);
            DataSet dstemp = Eventomatic_DB.SPs.ViewCartTickets(tx_key).GetDataSet();
            dstemp.Tables[0].Rows.Add(dstemp.Tables[0].NewRow());
            GridView1.DataSource = dstemp.Tables[0];
            GridView1.DataBind();
        }

        protected void FillForm()
        {
            lblstorelink.Text = "<a href=store.aspx?Storeid=" + Resource_Key.ToString() + ">";
            Site sitetemp = new Site();

            DataSet dstemp = Eventomatic_DB.SPs.ViewServiceFeeResourceKey(Resource_Key).GetDataSet();
            
            hdSFP.Value = dstemp.Tables[0].Rows[0]["Service_Fee_Percentage"].ToString();
            Boolean booltemp = sitetemp.IsDecimal("0");

            if (hdSFP.Value == "")
            { hdSFP.Value = "0"; }
            else if (booltemp)//hdSFP.Value))
            { hdSFP.Value = Convert.ToString(Convert.ToDecimal(hdSFP.Value) / 100); }
            if (hdSFC.Value == "")
            { hdSFC.Value = "0"; }
            hdSFC.Value = dstemp.Tables[0].Rows[0]["Service_Fee_Cents"].ToString();
            if (hdSFM.Value == "")
            { hdSFM.Value = "0"; }
            hdSFM.Value = dstemp.Tables[0].Rows[0]["Service_Fee_Max"].ToString();

            /*
            if (sitetemp.IsDemo(Event_Key))
            {
                lblDemo.Visible = false;
            }
            else
            {
                hdTrial.Value = "True";
                lblDemo.Text = "Trial Version";
                lblDemo.BackColor = System.Drawing.Color.LightBlue;
            }*/

            Site Sitetemp = new Site();
            dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
            
            lblGroupName.Text = dstemp.Tables[0].Rows[0]["Group_Name"].ToString() + "'s";
            
            imgGroup.ImageUrl = Sitetemp.GetResourcePic(Resource_Key.ToString());

            
            if (Session["Cart_tx_key"] != null)
            {
                tx_key = Convert.ToInt32(Session["Cart_tx_key"].ToString());
                DataSet dstxinfo = Eventomatic_DB.SPs.ViewTransactionDetails(tx_key).GetDataSet();
                if (dstxinfo.Tables[0].Rows.Count > 0)
                {
                    if (dstxinfo.Tables[0].Rows[0]["Tx_Status"] != DBNull.Value)
                    {
                        if (dstxinfo.Tables[0].Rows[0]["Tx_Status"].ToString() == "2")
                        {
                            Session["Cart_tx_key"] = null;
                            tx_key = 0;
                        }
                    }
                }
                hdtx_key.Value = tx_key.ToString();
            }
            if (tx_key != 0)
            {
                dstemp = Eventomatic_DB.SPs.ViewCartTickets(tx_key).GetDataSet();
                dstemp.Tables[0].Rows.Add(dstemp.Tables[0].NewRow());
                GridView1.DataSource = dstemp.Tables[0];
                GridView1.DataBind();
            }
            else //nothing in cart
            {
                nocart();
            }
            lbltxkey.Text = tx_key.ToString();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {       
            DataSet dstemp = Eventomatic_DB.SPs.ViewCartTickets(tx_key).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    if (e.Row.RowIndex == dstemp.Tables[0].Rows.Count)
                    {
                        System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity");   // look for dropdown in the row            
                        if (dropDownList != null)
                        { dropDownList.Visible = false; }

                        System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblServiceFee");
                        if (label != null)
                        {
                            label.Visible = true;
                        }
                        System.Web.UI.WebControls.Label lblTotal = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotal");
                        lblTotal.Text = "$ " + dstemp.Tables[0].Rows[0]["Service_Fee_Total"].ToString();
                        hdServiceFee.Value = dstemp.Tables[0].Rows[0]["Service_Fee_Total"].ToString();
                    }
                    else
                    {
                        System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity");   // look for dropdown in the row 
                        System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblQuantity");
                        dropDownList.SelectedIndex = Convert.ToInt32(label.Text);

                        System.Web.UI.WebControls.Label lblTicketKey = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTicketKey");
                        System.Web.UI.WebControls.Label lblRemove = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblRemove");
                        System.Web.UI.WebControls.Label lblTicketPurchasedKey = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTicketPurchasedKey");
                        lblRemove.Text = "<a href='javascript:doRemove2(" + lblTicketPurchasedKey.Text + ");'>Remove</a>";
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    System.Web.UI.WebControls.Label lblTotal = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotalOverall");
                    lblTotal.Text = "$ " + dstemp.Tables[0].Rows[0]["Amount_Total"].ToString();
                    hdOverallTotal.Value = dstemp.Tables[0].Rows[0]["Amount_Total"].ToString();
                }
            }
            else
            {
                nocart();
            }
        }

        protected void nocart()
        {
            pnlnopay.Visible = true;
            pnlpay.Visible = false;
            GridView1.Visible = false;
        }

        protected bool CheckTerms()
        {
            bool tempbool = false;
            if (chkTerms.Checked)
            {
                tempbool = true;
            }
            else
            {
                lblError.Text = "Please agree to terms of service before you complete your transaction.";
                lblError.Visible = true;    
            }
            return tempbool;
        }

        protected void btnCC_Click(object sender, EventArgs e)
        {
            if (CheckTerms())
            {
                Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("PayCC_URL").ToString() + "?tx_key=" + hdtx_key.Value);
            }            
        }

        protected void btnPaypal_Click(object sender, EventArgs e)
        {
            if (CheckTerms())
            {
                tx_key = Convert.ToInt32(hdtx_key.Value);
                DataSet dseventkey = Eventomatic_DB.SPs.ViewTransactionDetails(tx_key).GetDataSet();
                int Event_Key = Convert.ToInt32(dseventkey.Tables[0].Rows[0]["Event_Key"].ToString());
                Site Sitetemp = new Site();
                string strcurrency = Sitetemp.GetResourceCurrencyTx(tx_key);
                decimal ServiceFeeAmount = decimal.Parse(hdServiceFee.Value);
                decimal HostAmount = decimal.Parse(hdOverallTotal.Value) - ServiceFeeAmount;
                Boolean isdemovar = Sitetemp.IsDemo_ResourceKey(Resource_Key);
                string strHostEmail = strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("Trial_Email").ToString();
                string strServiceFeeEmail = "";

                string PayMethod = "0";
                DataSet dsresourceinfo = Eventomatic_DB.SPs.ViewResourceFromTxKey(tx_key).GetDataSet();
                if (dsresourceinfo.Tables[0].Rows[0]["Pay_Method"] != DBNull.Value)
                {
                    PayMethod = dsresourceinfo.Tables[0].Rows[0]["Pay_Method"].ToString();
                }
                //0 or null mean end of event / 1 means each transaction

                if (isdemovar)
                {
                    strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
                }
                else
                {
                    strServiceFeeEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString();
                }

                DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
                if ((dstemp.Tables[0].Rows[0]["Email_Paypal"] != DBNull.Value) && (PayMethod == "1"))
                {
                    strHostEmail = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
                }
                else
                {
                    if (isdemovar)
                    {
                        strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Live").ToString();
                    }
                    else
                    {
                        strHostEmail = System.Configuration.ConfigurationSettings.AppSettings.Get("My_Email_Trial").ToString();
                    }
                }

                Eventomatic.Addons.PaypalMethods paytemp = new Eventomatic.Addons.PaypalMethods();
                string strNotetemp = "";
                if (Sitetemp.isAlphaNumeric(lblGroupName.Text))
                {
                    strNotetemp += lblGroupName.Text + " has received payment."; ;
                }
                else
                {
                    strNotetemp += "Your event has received a payment.";
                }
                paytemp.ParallelPayment(isdemovar, strcurrency, strNotetemp, HostAmount, strHostEmail, ServiceFeeAmount, strServiceFeeEmail, tx_key, Event_Key);
            }            
        }
    }
}
