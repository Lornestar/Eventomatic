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
using System.Globalization;

namespace Eventomatic.Addons
{
    public partial class Tickets2 : System.Web.UI.Page
    {
        Int32 tixid = 0;
        Int32 eventkey = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["tixid"] != null)
            {
                tixid = Convert.ToInt32(Request.QueryString["tixid"]);                
            }
            if (Request.QueryString["eventkey"] != null)
            {
                eventkey = Convert.ToInt32(Request.QueryString["eventkey"]);
            }
            if (!IsPostBack)
            {
                //Webdatechooser date format
                /*  .CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN");
                ci.DateTimeFormat.LongDatePattern = "dddd, MMMM d, yyyy";
                ci.DateTimeFormat.ShortDatePattern = "dddd, MMMM d, yyyy";*/
                Begin_Selling.Culture = new CultureInfo("en-US");
                Selling_Deadline.Culture = new CultureInfo("en-US");
                Begin_Selling.SelectedDate = DateTime.Now;                
                Begin_Selling_Time.SelectedDate = DateTime.Now;
                Selling_Deadline.SelectedDate = DateTime.Now.AddDays(14);
                Selling_Deadline_Time.SelectedDate = DateTime.Parse("1:00AM");
                if (tixid != 0)
                {
                    LoadInfo();
                }
            }            
        }

        protected void LoadInfo()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicketSpecific2(tixid).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Ticket_Description"] != DBNull.Value)
            {
                txtdescription.Text = dstemp.Tables[0].Rows[0]["Ticket_Description"].ToString();
            }
            if (dstemp.Tables[0].Rows[0]["Price"] != DBNull.Value)
            {
                TicketsCost.Value = double.Parse(dstemp.Tables[0].Rows[0]["Price"].ToString());
            }
            if (dstemp.Tables[0].Rows[0]["isdonation"] != DBNull.Value)
            {
                if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["isdonation"]))
                {
                    TicketsCost.Enabled = false;
                    chkdonation.Checked = true;
                }
            }
            if (dstemp.Tables[0].Rows[0]["Capacity"] != DBNull.Value)
            {
                TicketsAvailable.Value = double.Parse(dstemp.Tables[0].Rows[0]["Capacity"].ToString());
            }
            if (dstemp.Tables[0].Rows[0]["Begin_Selling"] != DBNull.Value)
            {
                Begin_Selling.SelectedDate = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Begin_Selling"]);
                Begin_Selling_Time.SelectedDate = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Begin_Selling"]);
            }
            if (dstemp.Tables[0].Rows[0]["Selling_Deadline"] != DBNull.Value)
            {
                Selling_Deadline.SelectedDate = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Selling_Deadline"]);
                Selling_Deadline_Time.SelectedDate = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Selling_Deadline"]);
            }
        }

        protected void btnSaveTicket_Click(object sender, EventArgs e)
        {
            Site sitetemp = new Site();
            if (txtdescription.Text == "")
            {
                txtdescription.Text = txtdescription.EmptyMessage;
            }
            //string eventkey = sitetemp.GetEventKeyTicketKey(tixid);            
            DateTime tempbegin = Begin_Selling.SelectedDate.Value.Date.Add(Begin_Selling_Time.SelectedDate.Value.TimeOfDay);
            DateTime tempfinish = Selling_Deadline.SelectedDate.Value.Date.Add(Selling_Deadline_Time.SelectedDate.Value.TimeOfDay);
            Eventomatic_DB.SPs.UpdateTicket(tixid, eventkey, txtdescription.Text, Convert.ToDecimal(TicketsCost.Value), Convert.ToInt32(TicketsAvailable.Value),
                tempbegin, tempfinish, 0, chkdonation.Checked).Execute();
            //Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('test');</script>");
            RadAjaxPanel1.ResponseScripts.Add(string.Format("CloseWindow();return false;", ""));
        }

        protected void onchkdonation_Clicked(Object sender, EventArgs e)
        {
            if (TicketsCost.Enabled)
            {
                TicketsCost.Enabled = false;
            }
            else
            {
                TicketsCost.Enabled = true;
            }
        }
    }
}
