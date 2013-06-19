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

namespace Eventomatic.Addons
{
    public partial class FBEventsImport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void LoadImportEvents()
        {
            string fbid = hdnfbid.Value;

            Site sitetemp = new Site();

            
            //******************FINDING FB EVENTS person is admin for
            //Setup dtGroupAdmin
            DataTable dtGroupAdmin = new DataTable("GroupsAdmin");
            DataColumn cltemp;
            DataRow rtemp;

            cltemp = new DataColumn();
            cltemp.DataType = System.Type.GetType("System.String");
            cltemp.ColumnName = "gid";
            cltemp.ReadOnly = false;
            cltemp.Unique = true;
            dtGroupAdmin.Columns.Add(cltemp);
            rtemp = dtGroupAdmin.NewRow();

            DataSet dstemp;
            string GroupAdminQueryAddon = "";
            //First find which groups the person is admin for
            dstemp = sitetemp.ExecuteQuery("SELECT gid, positions FROM group_member WHERE uid=" + fbid);
            if (dstemp != null)
            {
                if (dstemp.Tables.Count >= 2)
                {
                    foreach (DataRow r in dstemp.Tables[2].Rows)
                    {
                        if (r.Table.Columns.Contains("member_type"))
                        {
                            if (r["member_type"].ToString() == "ADMIN")
                            {
                                rtemp["gid"] = dstemp.Tables[1].Rows[dstemp.Tables[2].Rows.IndexOf(r)][0].ToString();
                                dtGroupAdmin.Rows.Add(rtemp);
                                GroupAdminQueryAddon += " OR creator =" + rtemp["gid"].ToString();
                            }
                        }
                    }
                    
                }
                else //not member of any groups
                {

                }                
            }
            else //error in reading groups
            {

            }
            
            
            //Query all events this person is admin of, including events his groups are admin of
            dstemp = sitetemp.ExecuteQuery("SELECT creator,name,start_time, eid  FROM event WHERE eid IN (SELECT eid FROM event_member WHERE uid=" + fbid + ") AND start_time > now() AND (creator =" + fbid + GroupAdminQueryAddon + ")");
            if (dstemp != null)
            {
                if (dstemp.Tables.Count > 1)
                {
                    foreach (DataRow r in dstemp.Tables[1].Rows)
                    {
                        if (r.Table.Columns.Contains("start_time"))
                        {
                            r["start_time"] = sitetemp.fbDateTime(Convert.ToInt32(r["start_time"].ToString()));
                        }
                    }

                    UltraWebGrid1.DataSource = dstemp.Tables[1];
                    UltraWebGrid1.DataBind();
                }
                else
                {
                    pnlImport.Visible = false;
                    lblNoImport.Visible = true;
                }
                //******************FINDING FB EVENTS person is admin for

                //Add Sell your event in FB Import Events
                /*bool IsUnique;
                foreach (UltraGridRow row in UltraWebGrid1.Rows)
                {
                    IsUnique = true;
                    //check if same eid in current 
                    foreach (UltraGridRow row2 in UltraWebGrid2.Rows)
                    {
                        if (row.Cells[4].Text == row2.Cells[4].Text)
                        { IsUnique = false; }
                    }
                    if (IsUnique)
                    {
                        //row.Cells[3].Text = "Sell Event!";
                        //row.Cells[3].TargetURL = System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Edit_Event.aspx?eid=" + row.Cells[4].ToString();
                        row.Cells[3].Text = "<a href='" + System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Edit_Event.aspx?eid=" + row.Cells[4].ToString() + "' target='_top'>Sell Event!</a>";
                    }
                    else { row.Cells[3].Text = "Currently Selling"; }
                }*/
            }
            else //error in running query to see if any events to import
            {
                pnlImport.Visible = false;
                lblNoImport.Visible = true;
            }
            

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            LoadImportEvents();
            Page.RegisterStartupScript("popup3", "<script language=javascript>popup3();</script>");
        }

    }
}