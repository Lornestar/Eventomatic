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
using Infragistics.WebUI.UltraWebGrid;

namespace Eventomatic.Admin
{
    public partial class Service_Fees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "DoRestore")
            {
                DoRestore(Request["__EVENTARGUMENT"].ToString());
            }
            else
            {
                if (!IsPostBack)
                { Load_Existing(); }
            }
        }

        protected void Load_Existing()
        {
            int inttmp = UltraWebTab1.SelectedTabIndex;
            switch (inttmp)
            {
                case 0: UltraWebGrid1.DataSource = Eventomatic_DB.SPs.ViewAdminServiceFee(0).GetDataSet().Tables[0];
                        UltraWebGrid1.DataBind();
                        break;
                case 1: UltraWebGrid2.DataSource = Eventomatic_DB.SPs.ViewAdminEvents(0).GetDataSet().Tables[0];
                        UltraWebGrid2.DataBind();
                        break;
                case 2: UltraWebGrid3.DataSource = Eventomatic_DB.SPs.ViewAdminUsers(0).GetDataSet().Tables[0];
                        UltraWebGrid3.DataBind();
                        break;
                case 3: UltraWebGrid4.DataSource = Eventomatic_DB.SPs.ViewErrors(0).GetDataSet().Tables[0];
                        UltraWebGrid4.DataBind();
                        break;
                case 4: UltraWebGrid5.DataSource = Eventomatic_DB.SPs.ViewAdminEventsRemoved(0).GetDataSet().Tables[0];
                        UltraWebGrid5.DataBind();
                        string Event_id = "0";
                        string strRemove = "<a href='javascript:doRestore(EVENTID);'>Restore</a>";
                        foreach (UltraGridRow row in UltraWebGrid5.Rows)
                        {
                            Event_id = row.Cells[0].Text;
                            row.Cells[3].Text = strRemove.Replace("EVENTID", Event_id);
                        }
                        break;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            UltraWebGridExcelExporter1.DownloadName = "ErrorList.xls";
            UltraWebGridExcelExporter1.ExportMode = Infragistics.WebUI.UltraWebGrid.ExcelExport.ExportMode.Download;
            Infragistics.Excel.Workbook workbook = new Infragistics.Excel.Workbook();
            workbook.Worksheets.Add("Errors");

            this.UltraWebGridExcelExporter1.Export(this.UltraWebGrid4, workbook);
        }

       
           
       

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DataSet dstemp = null;
            Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgridtemp = null;            
            switch (UltraWebTab1.SelectedTabIndex)
            {
                case 0:
                    dstemp = Eventomatic_DB.SPs.ViewAdminServiceFee(0).GetDataSet();
                    Ultrawebgridtemp = UltraWebGrid1;
                    break;
                case 1:
                    dstemp = Eventomatic_DB.SPs.ViewAdminEvents(0).GetDataSet();
                    Ultrawebgridtemp = UltraWebGrid2;
                    break;
                /*case 2:
                    dstemp = Pharmastats_DB.SPs.GetPageSuggestedActivitiesAdmin().GetDataSet();
                    Ultrawebgridtemp = UltraWebGrid3;
                    break;
                case 3:
                    dstemp = Pharmastats_DB.SPs.GetPageSubscriptionAdmin().GetDataSet();
                    Ultrawebgridtemp = UltraWebGrid4;
                    break;*/
            }
            int deleterowcount = 0;
            bool FoundMatch;
            foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow r in Ultrawebgridtemp.Rows)
            {
                if (r.DataChanged == Infragistics.WebUI.UltraWebGrid.DataChanged.Modified)
                {
                    Update_DB(r);
                }
                else if (r.DataChanged == Infragistics.WebUI.UltraWebGrid.DataChanged.Added)
                {
                    Update_DB(r);
                    // Load_Existing();
                }
                FoundMatch = false;
                while ((!FoundMatch) && (deleterowcount < dstemp.Tables[0].Rows.Count))
                {
                    //Delete Stuff
                    if (r.Cells[0].ToString() != dstemp.Tables[0].Rows[deleterowcount][0].ToString())
                    {
                        //delete this row
                        DeleteRow(Convert.ToInt32(dstemp.Tables[0].Rows[deleterowcount][0]));
                    }
                    else
                    {
                        FoundMatch = true;
                    }
                    deleterowcount = deleterowcount + 1;
                }
            }
            while (deleterowcount < dstemp.Tables[0].Rows.Count)//delete rest of the 
            {
                DeleteRow(Convert.ToInt32(dstemp.Tables[0].Rows[deleterowcount][0]));
                deleterowcount = deleterowcount + 1;
            }
            lblresult.Text = "Your changes have been saved.";
            lblresult.ForeColor = System.Drawing.Color.Blue;
            lblresult.Visible = true;
        }

        protected void DeleteRow(int Key)
        {
            /*switch (UltraWebTab1.SelectedTabIndex)
            {
                case 0:
                    Pharmastats_DB.SPs.DeleteEmail(Key).Execute();//Delete Email row
                    break;
                case 1:
                    Pharmastats_DB.SPs.DeleteDisease(Key).Execute();//Delete Email row
                    break;
                case 2:
                    Pharmastats_DB.SPs.DeleteSuggestedActivitiesAdmin(Key).Execute();//Delete Email row
                    break;
                case 3:
                    Pharmastats_DB.SPs.DeleteSubscriptionAdmin(Key).Execute();//Delete Email row
                    break;
            }*/

        }

        
        protected void Update_DB(Infragistics.WebUI.UltraWebGrid.UltraGridRow r)
        {
            switch (UltraWebTab1.SelectedTabIndex)
            {
                case 0:
                    Eventomatic_DB.SPs.UpdateAdminServiceFee(Convert.ToDecimal(r.Cells[4].Value), Convert.ToDecimal(r.Cells[5].Value), Convert.ToDecimal(r.Cells[6].Value), Convert.ToInt32(r.Cells[0].Value)).Execute();
                    break;
                case 1:
                    Eventomatic_DB.SPs.UpdateAdminEvents(Convert.ToDecimal(r.Cells[4].Value), Convert.ToDecimal(r.Cells[5].Value), Convert.ToDecimal(r.Cells[6].Value), Convert.ToInt32(r.Cells[0].Value)).Execute();
                    //Pharmastats_DB.SPs.UpdateDisease(Convert.ToInt32(r.Cells[0].Value), Convert.ToString(r.Cells[1].Value)).Execute();
                    break;
               /* case 2:
                    Pharmastats_DB.SPs.UpdateSuggestedActivitiesAdmin(Convert.ToInt32(r.Cells[0].Value), Convert.ToString(r.Cells[1].Value), Convert.ToString(r.Cells[2].Value), 0, Convert.ToInt32(r.Cells[3].Value)).Execute();
                    break;
                case 3:
                    Pharmastats_DB.SPs.UpdateSubscriptionAdmin(Convert.ToInt32(r.Cells[0].Value), Convert.ToString(r.Cells[1].Value), Convert.ToDateTime(r.Cells[2].Value), Convert.ToDateTime(r.Cells[3].Value), Convert.ToDecimal(r.Cells[4].Value)).Execute();
                    break;*/
            }
            
        }

        protected void WebTab_Click(object sender, EventArgs e)
        {
            Load_Existing();
        }

        protected void DoRestore(string Eventid)
        {
            //false means invisible
            Eventomatic_DB.SPs.UpdateEventRemove(Convert.ToInt32(Eventid), true).Execute();
            string ugEventid = "";
            foreach (UltraGridRow row in UltraWebGrid5.Rows)
            {
                ugEventid = row.Cells[0].Text;
                if (ugEventid == Eventid)
                {
                    UltraWebGrid5.Rows.RemoveAt(row.Index);
                    break;
                }
            }            
        }
    }
}
