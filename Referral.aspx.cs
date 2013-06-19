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
//using facebook.Schema;
using Infragistics.WebUI.UltraWebGrid;

namespace Eventomatic
{
    public partial class Referral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btn_Add.Attributes.Add("onclick", "javascript:AddListbox();");
                Fill_Form();                
            }
        }

        protected void Fill_Form()
        {            

            Int64 fbid = Convert.ToInt64(Master.getfbid());
            DataSet dstemp = Eventomatic_DB.SPs.ViewFbUsers(fbid).GetDataSet();

            txtPaypal.Text = dstemp.Tables[0].Rows[0]["Referral_Email"].ToString();

            decimal Referral_Rate = Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("ReferralRate").ToString());
            if (dstemp.Tables[0].Rows[0]["Referral_Rate"] != DBNull.Value)
            {
                Referral_Rate = Convert.ToDecimal(dstemp.Tables[0].Rows[0]["Referral_Rate"].ToString());
            }

            //Set Transactions grid
            DataSet dsEventList = Eventomatic_DB.SPs.ViewListAllEventsReferral(fbid).GetDataSet();
            UltraWebGrid2.DataSource = dsEventList.Tables[0];
            UltraWebGrid2.DataBind();

            Site sitetemp = new Site();
            Hashtable hsNetRevenueTotal = sitetemp.GetRevenue_Hashtable_Empty();
            Hashtable hsNetRevenue_ShareTotal = sitetemp.GetRevenue_Hashtable_Empty();
            Hashtable hsPaidTotal = sitetemp.GetRevenue_Hashtable_Empty();
            Hashtable hsOwedTotal = sitetemp.GetRevenue_Hashtable_Empty();
            foreach (UltraGridRow row in UltraWebGrid2.Rows)
            {
                int resource_key = Convert.ToInt32(row.Cells[0].Text);

                Hashtable hsNetRevenuetemp = sitetemp.GetRevenue_Hashtable(resource_key, 2);
                Hashtable hsNetRevenue_Sharetemp = sitetemp.Divide_Hashtable(hsNetRevenuetemp,Referral_Rate);
                Hashtable hsPaidtemp = sitetemp.GetRevenue_Hashtable(resource_key, 3);
                Hashtable hsOwedtemp = sitetemp.GetRevenue_Hashtable_Empty();
                foreach (DictionaryEntry de in hsNetRevenuetemp)
                {
                    hsOwedtemp[de.Key.ToString()] = Convert.ToDecimal(hsNetRevenue_Sharetemp[de.Key.ToString()]) - Convert.ToDecimal(hsPaidtemp[de.Key.ToString()]);
                    hsOwedTotal[de.Key.ToString()] = Convert.ToDecimal(hsOwedtemp[de.Key.ToString()]) + Convert.ToDecimal(hsOwedTotal[de.Key.ToString()]);
                    hsPaidTotal[de.Key.ToString()] = Convert.ToDecimal(hsPaidtemp[de.Key.ToString()]) + Convert.ToDecimal(hsPaidTotal[de.Key.ToString()]);
                    hsNetRevenue_ShareTotal[de.Key.ToString()] = Convert.ToDecimal(hsNetRevenue_Sharetemp[de.Key.ToString()]) + Convert.ToDecimal(hsNetRevenue_ShareTotal[de.Key.ToString()]);
                }

                row.Cells[3].Text = sitetemp.GetRevenue(hsNetRevenuetemp);
                row.Cells[4].Text = sitetemp.GetRevenue(hsNetRevenue_Sharetemp);
                row.Cells[5].Text = "<a href='javascript:viewtxout();'>" + sitetemp.GetRevenue(hsPaidtemp) + "</a>";
                row.Cells[6].Text = sitetemp.GetRevenue(hsOwedtemp);

                row.Cells[1].Text = "<a href='http://www.facebook.com/profile.php?id=" + row.Cells[1].Text + "' target='_top'>" + row.Cells[7].Text + "</a>";
            }
            UltraWebGrid2.Columns[4].Footer.Caption = "Total= " + sitetemp.GetRevenue(hsNetRevenue_ShareTotal);
            UltraWebGrid2.Columns[5].Footer.Caption = "Total= " + sitetemp.GetRevenue(hsPaidTotal);
            lblowed.Text = sitetemp.GetRevenue(hsOwedTotal);
            UltraWebGrid2.Columns[6].Footer.Caption = "Total= " + lblowed.Text;

            if (lblowed.Text.Trim() == "$0.00")
            {
                btnpaynow.Enabled = false;
            }

            DataSet dstxout = Eventomatic_DB.SPs.ViewTransactionOutDetailsReferral(fbid).GetDataSet();
            UltraWebGrid3.DataSource = dstxout.Tables[0];
            UltraWebGrid3.DataBind();

            foreach (UltraGridRow row in UltraWebGrid3.Rows)
            {
                row.Cells[2].Text = row.Cells[2].Text.Trim();
                row.Cells[3].Text = sitetemp.GetCurrencySymbol(row.Cells[7].Text) + row.Cells[3].Text + row.Cells[7].Text;
                row.Cells[4].Text = sitetemp.GetCurrencySymbol(row.Cells[7].Text) + row.Cells[4].Text + row.Cells[7].Text;
                row.Cells[6].Text = "<a href='http://www.facebook.com/profile.php?id=" + row.Cells[8].Text + "' target='_top'>" + row.Cells[6].Text + "</a>";
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool tempPageValid = true;
            string temperrorstr = "";
            if (chkLive.Checked)
            {
                Site sitetemp = new Site();
                if (!sitetemp.isEmail(txtPaypal.Text) || (!sitetemp.isEmail(txtPaypalConfirm.Text)))
                {
                    tempPageValid = false;
                    temperrorstr = "Please enter a valid Email Address.";
                }
                if (txtPaypal.Text != txtPaypalConfirm.Text)
                {
                    tempPageValid = false;
                    temperrorstr = "Paypal Emails do not match.";
                }
            }
            if (tempPageValid)
            {
                lblError.Visible = false;                
                Int64 fbid = Convert.ToInt64(Master.getfbid());

                Eventomatic_DB.SPs.UpdateReferralEmail(fbid, txtPaypal.Text).Execute();
                //Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Default.aspx");
                Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('Your Changes have been Saved');</script>");
            }
            else
            {
                lblError.Text = temperrorstr;
                lblError.Visible = true;
            }
        }

        protected void btnreferemail_Click(object sender, EventArgs e)
        {
            Site sitetemp = new Site();
            if (sitetemp.isEmail(txtreferemail.Text) && (txtreferemail.Text != ""))
            {
                Send_Email sendemail = new Send_Email();
                sendemail.Refer_Friend_Email(txtreferemail.Text, Convert.ToInt64(Master.getfbid()));

                lblreferemail.ForeColor = System.Drawing.Color.Blue;
                lblreferemail.Text = "Your referral email has been sent.";
                lblreferemail.Visible = true;
            }
            else
            {
                lblreferemail.ForeColor = System.Drawing.Color.Red;
                lblreferemail.Text = "Please enter a valid email address";
                lblreferemail.Visible = true;
            }
        }

        protected void btnpaynow_Click(object sender, EventArgs e)
        {
            pnlSettings.Visible = false;
            pnlSendMoney.Visible = true;
            SendMoney1.LoadInfo(lblowed.Text, Convert.ToInt32(Master.GetResourceKey()), Master.getfbid());
        }
        
        protected void chkTrial_CheckedChanged(object sender, EventArgs e)
        {
            Assign_New_RadioButton();
        }

        protected void chkLive_CheckedChanged(object sender, EventArgs e)
        {
            Assign_New_RadioButton();
        }

        protected void Assign_New_RadioButton()
        {
            if (chkLive.Checked)
            {
                //RequiredFieldValidator1.Enabled = true;
                //RequiredFieldValidator2.Enabled = true;
                //Compare1.Enabled = true;
                pnlLive.Visible = true;                
            }
            else
            {
                //RequiredFieldValidator1.Enabled = false;
                //RequiredFieldValidator2.Enabled = false;
                //Compare1.Enabled = false;
                pnlLive.Visible = false;                
            }
        }
    }
}
