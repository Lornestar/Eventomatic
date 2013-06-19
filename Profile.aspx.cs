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
    public partial class Profile : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btn_Add.Attributes.Add("onclick", "javascript:AddListbox();");
                Fill_Form();
                if (Master.IsAdmin())
                {
                    pnlModifyTrial.Visible = true;
                }
                                  
            }
        }

        protected void Fill_Form()
        {            

            int Resource_Key = Convert.ToInt32(Master.GetResourceKey());
            DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();

            //False = Trial & True = Live
            chkTrial.Checked = true;
            Assign_New_RadioButton();            
            if (dstemp.Tables[0].Rows[0]["Demo"] != DBNull.Value)
            {
                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Demo"].ToString()) == true)
                {
                    chkTrial.Checked = false;
                    chkLive.Checked = true;
                    Assign_New_RadioButton();
                }
            }
            
            txtPaypal.Text = dstemp.Tables[0].Rows[0]["Email_Paypal"].ToString();
            

            switch(dstemp.Tables[0].Rows[0]["Desired_Currency"].ToString())
            {
                case "CAD": 
                    ddlCurrency.SelectedValue = "CAD";
                    break;
                case "USD": 
                    ddlCurrency.SelectedValue = "USD";
                    break;
                case "EUR":
                    ddlCurrency.SelectedValue = "EUR";
                    break;
                case "GBP":
                    ddlCurrency.SelectedValue = "GBP";
                    break;
                case "ILS":
                    ddlCurrency.SelectedValue = "ILS";
                    break;
            }

            string Paymethod = "0";
            if (dstemp.Tables[0].Rows[0]["Pay_Method"] != DBNull.Value)
            {
                Paymethod = dstemp.Tables[0].Rows[0]["Pay_Method"].ToString();
            }
            if (Paymethod == "0")
            {
                rdendevent.Checked = true;
            }
            if (Paymethod == "1")
            {
                rdattrans.Checked = true;
            }
                 
            //Set Transactions grid
            DataSet dsEventList = Eventomatic_DB.SPs.ViewListAllEventsProfile(Resource_Key).GetDataSet();
            UltraWebGrid2.DataSource = dsEventList.Tables[0];
            UltraWebGrid2.DataBind();
     
            Site sitetemp = new Site();
            Hashtable hsCollectedTotal = sitetemp.GetRevenue_Hashtable_Empty();
            Hashtable hsPaidTotal = sitetemp.GetRevenue_Hashtable_Empty();
            Hashtable hsOwedTotal = sitetemp.GetRevenue_Hashtable_Empty();
            foreach (UltraGridRow row in UltraWebGrid2.Rows)
            {
                int event_key = Convert.ToInt32(row.Cells[0].Text);                

                Hashtable hsCollectedtemp = sitetemp.GetRevenue_Hashtable(event_key, 0);
                Hashtable hsPaidtemp = sitetemp.GetRevenue_Hashtable(event_key, 1);
                Hashtable hsOwedtemp = sitetemp.GetRevenue_Hashtable_Empty();
                foreach (DictionaryEntry de in hsCollectedtemp)
                {
                    hsOwedtemp[de.Key.ToString()] = Convert.ToDecimal(hsCollectedtemp[de.Key.ToString()]) - Convert.ToDecimal(hsPaidtemp[de.Key.ToString()]);
                    hsOwedTotal[de.Key.ToString()] = Convert.ToDecimal(hsOwedtemp[de.Key.ToString()]) + Convert.ToDecimal(hsOwedTotal[de.Key.ToString()]);
                    hsPaidTotal[de.Key.ToString()] = Convert.ToDecimal(hsPaidtemp[de.Key.ToString()]) + Convert.ToDecimal(hsPaidTotal[de.Key.ToString()]);
                    hsCollectedTotal[de.Key.ToString()] = Convert.ToDecimal(hsCollectedtemp[de.Key.ToString()]) + Convert.ToDecimal(hsCollectedTotal[de.Key.ToString()]);
                }

                row.Cells[4].Text = "<a href='" + System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Attendee_List.aspx?Event_Key=" + event_key.ToString() + "&view=transaction' target='_top'>" + sitetemp.GetRevenue(hsCollectedtemp) + "</a>";
                row.Cells[5].Text = "<a href='javascript:viewtxout();'>" + sitetemp.GetRevenue(hsPaidtemp) + "</a>";
                row.Cells[6].Text = sitetemp.GetRevenue(hsOwedtemp);
            }
            UltraWebGrid2.Columns[4].Footer.Caption = "Total= " + sitetemp.GetRevenue(hsCollectedTotal);
            UltraWebGrid2.Columns[5].Footer.Caption = "Total= " + sitetemp.GetRevenue(hsPaidTotal);            
            lblowed.Text = sitetemp.GetRevenue(hsOwedTotal);
            UltraWebGrid2.Columns[6].Footer.Caption = "Total= " + lblowed.Text;

            if (lblowed.Text.Trim() == "$0.00")
            {
                btnpaynow.Enabled = false;
            }

            //Disable Currency change if ever accepted money
            /*
            DataSet dsAcceptedMoney = Eventomatic_DB.SPs.ViewAcceptedMoney(Resource_Key).GetDataSet();
            if (dsAcceptedMoney.Tables[0].Rows.Count > 0)
            {
                if (dsAcceptedMoney.Tables[0].Rows[0]["TotalAmount"] != DBNull.Value)
                {
                    decimal TotalAmount = Convert.ToDecimal(dsAcceptedMoney.Tables[0].Rows[0]["TotalAmount"].ToString());
                    if (TotalAmount > 0)
                    {
                        ddlCurrency.Enabled = false;
                    }
                }
            }*/

            DataSet dstxout = Eventomatic_DB.SPs.ViewTransactionOutDetails(Resource_Key).GetDataSet();
            UltraWebGrid3.DataSource = dstxout.Tables[0];
            UltraWebGrid3.DataBind();

            foreach (UltraGridRow row in UltraWebGrid3.Rows)
            {
                row.Cells[2].Text = row.Cells[2].Text.Trim();
                row.Cells[3].Text = sitetemp.GetCurrencySymbol(row.Cells[7].Text) + row.Cells[3].Text + row.Cells[7].Text;
                row.Cells[4].Text = sitetemp.GetCurrencySymbol(row.Cells[7].Text) + row.Cells[4].Text + row.Cells[7].Text;
                row.Cells[6].Text = "<a href='http://www.facebook.com/profile.php?id=" + row.Cells[8].Text + "' target='_top'>" + row.Cells[6].Text + "</a>";
            }

            //Paypal confirmation
            HiddenField hdnemail = new HiddenField();
            hdnemail = (HiddenField)Paypal_Confirmation1.FindControl("hdemail");
            hdnemail.Value = txtPaypal.Text;
            if (Paypal_Confirmation1.isemailconfirmed(txtPaypal.Text) != 2)
            {
                btnpaynow.Enabled = false;
            }

        }

        protected void btnpaynow_Click(object sender, EventArgs e)
        {
            pnlSettings.Visible = false;
            pnlSendMoney.Visible = true;
            SendMoney1.LoadInfo(lblowed.Text, Convert.ToInt32(Master.GetResourceKey()),Master.getfbid());
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
                Boolean blnLiveorTrial = false;
                if (chkLive.Checked)
                {
                    blnLiveorTrial = true;
                }

                //Paymethod
                int Paymethod = 0;
                if (rdendevent.Checked)
                {
                    Paymethod = 0;
                }
                else if (rdattrans.Checked)
                {
                    Paymethod = 1;
                }

                Eventomatic_DB.SPs.UpdateResourceProfile(Convert.ToInt32(Master.GetResourceKey()), ddlCurrency.SelectedValue, txtPaypal.Text, blnLiveorTrial,Paymethod).Execute();
                Paypal_Confirmation1.enteredemail(txtPaypal.Text, ddlCurrency.SelectedValue, Convert.ToInt32(Master.GetResourceKey()));
                //Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Default.aspx");
                Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('Your Changes have been Saved');location.href = 'Default.aspx';</script>");
            }
            else
            {
                lblError.Text = temperrorstr;
                lblError.Visible = true;
            }
            
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
                lblTrialNote.Visible = false;
            }
            else
            {
                //RequiredFieldValidator1.Enabled = false;
                //RequiredFieldValidator2.Enabled = false;
                //Compare1.Enabled = false;
                pnlLive.Visible = false;
                lblTrialNote.Visible = true;
            }
        }
        
    }
}
