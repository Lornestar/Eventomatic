using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Eventomatic.Login
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Int64 fbid = Master.getfbuser().UID;
                if ((fbid == 121100861) || (fbid == 501386158) || (fbid == 615701716))
                {
                    Loadtables();
                }
                else
                {
                    //not admin kick out to settings
                    Response.Redirect("settings.aspx");
                }
            }
            else
            {
                Loadtables();
            }
        }

        protected void btnexportexcel_click(object sender, System.EventArgs e)
        {
            RadGrid1.ExportSettings.FileName = "Snappay_Users";
            RadGrid1.MasterTableView.ExportToExcel();            
        }

        protected void Loadtables()
        {
            
                DataSet dstemp = Eventomatic_DB.SPs.ViewAdminUsersSnappay(1).GetDataSet();
                RadListView1.DataSource = dstemp.Tables[0];
                RadListView1.DataBind();

                RadGrid1.DataSource = dstemp.Tables[0];
                RadGrid1.DataBind();
            
                dstemp = Eventomatic_DB.SPs.ViewAdminStoresSnappay(1).GetDataSet();
                RadListView2.DataSource = dstemp.Tables[0];
                RadListView2.DataBind();

                dstemp = Eventomatic_DB.SPs.ViewAdminTransactionsSnappay(1).GetDataSet();
                RadListView3.DataSource = dstemp.Tables[0];
                RadListView3.DataBind();

                dstemp = Eventomatic_DB.SPs.PfViewTxsum(0).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    lbltxcompleted.Text = dstemp.Tables[0].Rows[0][0].ToString();
                }

                dstemp = Eventomatic_DB.SPs.PfViewAdminMerchantcount(0).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    lblusersignup.Text = dstemp.Tables[0].Rows[0][0].ToString();
                }

                dstemp = Eventomatic_DB.SPs.PfViewAdminEngagedmerchantcount(0).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    lblusersdidtx.Text = dstemp.Tables[0].Rows[0][0].ToString();
                }
        }
        
    }
}