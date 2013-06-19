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
using Eventomatic.Addons;
using Infragistics.WebUI.UltraWebGrid;

namespace Eventomatic
{
    public partial class Seller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fbuser fbuser = Master.getfbuser();
                lblUsername.Text = fbuser.Fullname.ToString();
                if (!Master.CheckIfIsSeller(fbuser.UID.ToString()))
                {
                    pnladdtab.Visible = true;
                }
                LoadEventsAdmin();
            }
        }

        protected void LoadEventsAdmin()
        {
            //Current Events Grid
            fbuser fbuser = Master.getfbuser();
            DataSet dsCurrentEvents = Eventomatic_DB.SPs.ViewListStoreSellersCurrent(fbuser.UID).GetDataSet();
            string Event_id = "0";

            if (dsCurrentEvents.Tables[0].Rows.Count > 0)
            {
                pnlCurrentEvents.Visible = true;
                    lblNoCurrentEvents.Visible = false;                    

                    UltraWebGrid2.DataSource = dsCurrentEvents.Tables[0];
                    UltraWebGrid2.DataBind();


                    foreach (UltraGridRow row in UltraWebGrid2.Rows)
                    {
                        Event_id = row.Cells[0].Text;
                        row.Cells[5].Text = "<a href='Order_Form.aspx?Event_Key=" + Event_id + "&fbid=" + fbuser.UID.ToString() + "' target='_blank'>Preview</a>&nbsp;&nbsp;&nbsp;<a href='" + System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Seller_Attendee_List.aspx?Event_Key=" + Event_id + "' target='_top'>Guest List</a>";
                    }
            }
            else
            {
                pnlCurrentEvents.Visible = false;
                lblNoCurrentEvents.Visible = true;
            }


            DataSet dsPreviousEvents = Eventomatic_DB.SPs.ViewListStoreSellersPrevious(fbuser.UID).GetDataSet();
            if (dsPreviousEvents.Tables[0].Rows.Count > 0)
            {
                UltraWebGrid3.DataSource = dsPreviousEvents.Tables[0];
                UltraWebGrid3.DataBind();
            }
            else
            {
                pnlPastEvents.Visible = false;
                lblNoPastEvents.Visible = true;
            }
        }

    }
}
