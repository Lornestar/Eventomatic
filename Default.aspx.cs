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
using Eventomatic.Addons;
using System.Net;
using Newtonsoft.Json.Linq;
using Telerik.Web.UI;

namespace Eventomatic
{
    public partial class _Default : System.Web.UI.Page
    {
        fbuser fbuser;
        string storerooturl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            string strtemp = "0";
            if (Request.Form["__EVENTTARGET"] == "DoRemove")
            {
                DoRemove(Request["__EVENTARGUMENT"].ToString());
            }
            else if (Request.Form["__EVENTTARGET"] == "FacebookEvents")
            {
                btnImportfbEvent(Request["__EVENTARGUMENT"].ToString());
            }
            else if (Request.Form["__EVENTTARGET"] == "DoRemovePromoted")
            {
                DoRemovePromoted(Request["__EVENTARGUMENT"].ToString());
            }
            else
            {
                fbuser = Master.getfbuser();
                strtemp = Master.CheckIfInGroup2(fbuser.UID.ToString());
        
                if (strtemp == "0")
                {
                    pnlDefault.Visible = true;
                    pnlfirsttime.Visible = false;
                }
                else if (strtemp == "1") //firsttime
                {
                    //pnlDefault.Visible = false;
                    //pnlfirsttime.Visible = true;
                    //Page.RegisterStartupScript("Myscript", "<script language=javascript>document.getElementById('spnNavbar').style.display = 'none';</script>");                                        
                    /*if (Master.CheckIfIsSeller(fbuser.UID.ToString()))
                    {
                        Response.Redirect("Seller.aspx");
                    }
                    else
                    {
                       
                    } if Sessionstat autodetect not working*/
                    bool iscookieless = HttpContext.Current.Session.IsCookieless;
                    if (iscookieless)
                    {
                        Response.Redirect("IsAppUser.aspx?Firsttime=true");
                    }
                    else
                    {
                        Response.Redirect("FirstTime.aspx");
                    }                   
                    
                }
                                
                if (strtemp == "0")
                {
                    
                    lblUsername.Text = fbuser.Fullname.ToString();
                    /*  FACEBOOK DEPRECATING THIS FEATURE
                    if (!Master.CheckIfIsSeller(fbuser.UID.ToString()))
                    {
                        pnladdtab.Visible = true;
                    } */                   
                    
                    if (Master.CountPageVisits(fbuser.UID, "Settings.aspx") == 0)
                    {
                        pnladdadmin.Visible = true;
                    }
                    int tempdidbuydemo = Master.HasTriedDemoTicket(fbuser.UID,Convert.ToInt32(Master.GetResourceKey()));                    
                    if (tempdidbuydemo != 0)
                    {
                        pnlbuydemo.Visible = true;
                        hypdidbuydemo.NavigateUrl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Form2.aspx?edit=preview&Event_Key=" + tempdidbuydemo.ToString();
                    }      
                    if ((pnladdadmin.Visible == false) && (pnladdtab.Visible == false) && (pnlbuydemo.Visible == false))
                    {
                        pnltodos.Visible = false;
                    }
                    LoadEventsAdmin();

                    if (Request.Form["__EVENTTARGET"] != "Changeview")
                    {
                        //set activities log
                        HiddenField hdnCurrentResource = new HiddenField();
                        hdnCurrentResource = (HiddenField)Activities1.FindControl("hdResource_Key");
                        hdnCurrentResource.Value = Master.GetResourceKey().ToString();

                        Activities1.LoadActivities();
                        Activities1.CloseView();
                    }
                    if (!IsPostBack)
                    {
                        //set activities log                        
                        /*HiddenField hdnfbid = new HiddenField();
                        hdnfbid = (HiddenField)Promote1.FindControl("hdfbid");
                        hdnfbid.Value = fbuser.UID.ToString();

                        Promote1.LoadCombo();*/
                        
                        string strrooturl = Master.GetOpenWinApplocation();
                        lblPromoteEvent.Text = "<a href='#' onclick=openWin(" + fbuser.UID.ToString() +"," + Master.GetResourceKey().ToString() + "," + 0 + ",'" + strrooturl + "'); return false;'>Promote a Facebook Event</a>";
                        lblNoCurrentEvents.Text = "You have no Events Currently Selling. <a href='#' onclick=openWin(" + fbuser.UID.ToString() + "," + Master.GetResourceKey().ToString() + "," + 1 + "); return false;'>Sell an Event</a>";
                        Load_PromotedEvents();
                       
                    }
                }                               
            }
        }

        

        

        protected void LoadEventsAdmin()
        {            

            RadComboBox ddlgroups = new RadComboBox();
            ddlgroups = (RadComboBox)Master.LoadGroupsList();
                
                //Current Events Grid
                DataSet dsCurrentEvents = Eventomatic_DB.SPs.ViewListCurrentEvents(Convert.ToInt32(ddlgroups.SelectedValue.ToString())).GetDataSet();
                if (dsCurrentEvents.Tables[0].Rows.Count != 0)
                {
                    dsCurrentEvents.Tables[0].TableName = "Current_Events";

                    dsCurrentEvents.Tables.Add(Eventomatic_DB.SPs.ViewTicketByUser(Convert.ToInt32(ddlgroups.SelectedValue.ToString()),0).GetDataSet().Tables[0].Copy());
                    dsCurrentEvents.Tables[1].TableName = "Current_Events_Tickets";
                    try
                    {
                        dsCurrentEvents.Relations.Add("CurrentRelationship", dsCurrentEvents.Tables["Current_Events"].Columns["Event_Key"], dsCurrentEvents.Tables["Current_Events_Tickets"].Columns["Event_Key"]);
                    }
                    catch
                    {
                    }
                }

            

                string Event_id = "0";
                string strRemove = "<a href='javascript:doRemove(EVENTID);'>Remove</a>";

                if (dsCurrentEvents.Tables[0].Rows.Count > 0)
                {
                    

                    pnlCurrentEvents.Visible = true;
                    lblNoCurrentEvents.Visible = false;                                        

                    UltraWebGrid2.DataSource = dsCurrentEvents.Tables[0];
                    UltraWebGrid2.DataBind();

                    Site sitetemp = new Site();
                    foreach (UltraGridRow row in UltraWebGrid2.Rows)
                    {
                        int Sold = 0;
                        if (dsCurrentEvents.Tables[0].Rows[row.Index]["Tickets_Sold"] != DBNull.Value)
                        {
                            Sold = Convert.ToInt32(dsCurrentEvents.Tables[0].Rows[row.Index]["Tickets_Sold"]);
                        }
                        int Capacity = 0;
                        if (dsCurrentEvents.Tables[0].Rows[row.Index]["Tickets_Capacity"] != DBNull.Value)
                        {
                            Capacity = Convert.ToInt32(dsCurrentEvents.Tables[0].Rows[row.Index]["Tickets_Capacity"]);
                        }
                        
                        row.Cells[3].Text = Master.TicketsSoldProgressBarHTML(Sold,Capacity);
                        Event_id = row.Cells[0].Text;
                        row.Cells[6].Text = "<a href='Order_Form.aspx?edit=preview&Event_Key=" + Event_id + "' target='_top'>Preview</a>&nbsp;&nbsp;&nbsp;<a href='" + storerooturl + "order_form2.aspx?event_key=" + Event_id + "&edit=true' target='_top'>Edit</a>&nbsp;&nbsp;&nbsp;<a href='" + sitetemp.GetNavigateurl(fbuser.UID) + "Attendee_List.aspx?Event_Key=" + Event_id + "' target='_top'>Guest List</a>&nbsp;&nbsp;&nbsp;" + strRemove.Replace("EVENTID", Event_id);

                        //string strrevenue = row.Cells[3].Text;
                        
                        Hashtable hsCollectedtemp = sitetemp.GetRevenue_Hashtable(Convert.ToInt32(Event_id), 0);
                        string strrevenue = sitetemp.GetRevenue(hsCollectedtemp);
                        row.Cells[4].Text = "<a href='" + sitetemp.GetNavigateurl(fbuser.UID) + "Attendee_List.aspx?Event_Key=" + Event_id + "&view=transaction' target='_top'>" + strrevenue + "</a>";

                        //Check if product
                        if (sitetemp.Isproduct(Convert.ToInt32(Event_id)))
                        {
                            pnlCalendar.Visible = true;
                        }
                    }
                }
                else {
                    pnlCurrentEvents.Visible = false;
                    lblNoCurrentEvents.Visible = true;
                }
                

                

                //Previous Events Grid
                DataSet dsPreviousEvents = Eventomatic_DB.SPs.ViewListPreviousEvents(Convert.ToInt32(ddlgroups.SelectedValue.ToString())).GetDataSet();
                if (dsPreviousEvents.Tables[0].Rows.Count != 0)
                {
                    dsPreviousEvents.Tables[0].TableName = "Current_Events";

                    dsPreviousEvents.Tables.Add(Eventomatic_DB.SPs.ViewTicketByUser(Convert.ToInt32(ddlgroups.SelectedValue.ToString()),1).GetDataSet().Tables[0].Copy());
                    dsPreviousEvents.Tables[1].TableName = "Current_Events_Tickets";
                    try
                    {
                        dsPreviousEvents.Relations.Add("CurrentRelationship", dsPreviousEvents.Tables["Current_Events"].Columns["Event_Key"], dsPreviousEvents.Tables["Current_Events_Tickets"].Columns["Event_Key"]);
                    }
                    catch
                    {
                    }
                }

                if (dsPreviousEvents.Tables[0].Rows.Count > 0)
                {
                    pnlPastEvents.Visible = true;
                    lblNoPastEvents.Visible = false;

                    UltraWebGrid3.DataSource = dsPreviousEvents.Tables[0];
                    UltraWebGrid3.DataBind();
                    Site sitetemp = new Site();

                    Event_id = "0";
                    foreach (UltraGridRow row in UltraWebGrid3.Rows)
                    {
                        int Sold = 0;
                        if (dsPreviousEvents.Tables[0].Rows[row.Index]["Tickets_Sold"] != DBNull.Value)
                        {
                            Sold = Convert.ToInt32(dsPreviousEvents.Tables[0].Rows[row.Index]["Tickets_Sold"]);
                        }
                        int Capacity = 0;
                        if (dsPreviousEvents.Tables[0].Rows[row.Index]["Tickets_Capacity"] != DBNull.Value)
                        {
                            Capacity = Convert.ToInt32(dsPreviousEvents.Tables[0].Rows[row.Index]["Tickets_Capacity"]);
                        }

                        row.Cells[3].Text = Master.TicketsSoldProgressBarHTML(Sold, Capacity);
                        Event_id = row.Cells[0].Text;
                        row.Cells[6].Text = "<a href='Order_Form.aspx?edit=preview&Event_Key=" + Event_id + "' target='_top'>Preview</a>&nbsp;&nbsp;&nbsp;<a href='" + storerooturl + "order_form2.aspx?event_key=" + Event_id + "&edit=true' target='_top'>Edit</a>&nbsp;&nbsp;&nbsp;<a href='" + sitetemp.GetNavigateurl(fbuser.UID) + "Attendee_List.aspx?Event_Key=" + Event_id + "' target='_top'>Guest List</a>&nbsp;&nbsp;&nbsp;" + strRemove.Replace("EVENTID", Event_id);

                        //string strrevenue = row.Cells[3].Text;
                        
                        Hashtable hsCollectedtemp = sitetemp.GetRevenue_Hashtable(Convert.ToInt32(Event_id), 0);
                        string strrevenue = sitetemp.GetRevenue(hsCollectedtemp);
                        row.Cells[4].Text = "<a href='" + sitetemp.GetNavigateurl(fbuser.UID) + "Attendee_List.aspx?Event_Key=" + Event_id + "&view=transaction' target='_top'>" + strrevenue + "</a>";
                    }
                }
                else {
                    pnlPastEvents.Visible = false;
                    lblNoPastEvents.Visible = true;
                }





                Addons.Addons Addons = new Addons.Addons();
            
                //string fbid = Master.API.uid.ToString();

                //DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Convert.ToInt32(fbid)).GetDataSet();
                string Storeurl = System.Configuration.ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + Addons.EncodeStoreURL(ddlgroups.SelectedItem.Text.ToString());
                string Storeediturl = "&nbsp;&nbsp;&nbsp;<sup><a href='" + System.Configuration.ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Store.aspx?storeid=" + Master.GetResourceKey().ToString() + "&edit=true' target='_top' class='parent'>Edit Store</a></sup>";
                lblStoreURL.Text = "<a href='" + Storeurl + "' target='_blank'>" + Storeurl + "</a>" + Storeediturl;


                if ((!Master.HasPaypalEmail_ResourceKey(Convert.ToInt32(ddlgroups.SelectedValue.ToString()))) && (UltraWebGrid2.Rows.Count > 0))
                {
                    pnlPaypalEmailMissing.Visible = true;
                }
                else
                {
                    pnlPaypalEmailMissing.Visible = false;
                }
        }

        /*protected void btnImport_Click(object sender, EventArgs e)
        {
            LoadImportEvents();
            pnlImport.Visible = true;
            btnImport.Visible = false;
        }*/

        protected void DoRemove(string Eventid)
        {
            //false means invisible
            Eventomatic_DB.SPs.UpdateEventRemove(Convert.ToInt32(Eventid), false).Execute();
            string ugEventid = "";
            foreach (UltraGridRow row in UltraWebGrid2.Rows)
            {
                ugEventid = row.Cells[0].Text;
                if (ugEventid == Eventid)
                {
                    UltraWebGrid2.Rows.RemoveAt(row.Index);
                    break;
                }
            }
            foreach (UltraGridRow row in UltraWebGrid3.Rows)
            {
                ugEventid = row.Cells[0].Text;
                if (ugEventid == Eventid)
                {
                    UltraWebGrid3.Rows.RemoveAt(row.Index);
                    break;
                }
            }
            //Page.RegisterStartupScript("Myscript", "<script language=javascript>location.href = 'Default.aspx';</script>");

            try
            {
                //modify existing rss xml file
                Eventomatic.Addons.rss_generate rssgenerate = new Eventomatic.Addons.rss_generate();
                rssgenerate.WriteRss(Convert.ToInt32(Master.GetResourceKey()));
            }
            catch
            {

            }
        }

        protected void LoadImportEvents()
        {
            //Use same analytics used in firsttime page
            /*
            Site sitetemp = new Site();
            DataSet dstemp = sitetemp.RankEvents(Master.GetResourceName());

            if (dstemp != null)
            {
                DataTable dttemp = dstemp.Tables[1].Copy();
                if (dstemp.Tables.Count > 1)//At least 2 events to sell
                {
                    //Order rankings                    
                    dttemp.DefaultView.Sort = "[rank] desc";
                }                
                UltraWebGrid1.DataSource = dttemp;
                UltraWebGrid1.DataBind();
            }
            else //error in running query to see if any events to import
            {
                pnlImport.Visible = false;
                lblNoImport.Visible = true;
            }

            //Add Sell your event in FB Import Events
            bool IsUnique;
            foreach (UltraGridRow row in UltraWebGrid1.Rows)
            {
                if (row.Cells[1].Text.ToString().Length > 30)
                {
                    row.Cells[1].Text = row.Cells[1].Text.Substring(0, 30);
                }
                
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
                    row.Cells[3].Text = "<a href='" + System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Edit_Event.aspx?eid=" + row.Cells[4].ToString() + "' target='_top'><b>Sell Event!</b></a>";
                }
                else { row.Cells[3].Text = "Currently Selling"; }
            }
            /*fbuser fbuser = Master.getfbuser();
            string fbid = fbuser.UID.ToString();
            
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
            dstemp = Master.ExecuteQuery("SELECT gid, positions FROM group_member WHERE uid=" + fbid);
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
            //dstemp = Master.ExecuteQuery("SELECT creator,name,start_time, eid  FROM event WHERE eid IN (SELECT eid FROM event_member WHERE uid=" + fbid + ") AND start_time > now() AND (creator =" + fbid + GroupAdminQueryAddon + ")");
            dstemp = Master.ExecuteQuery("SELECT creator,name,start_time, eid  FROM event WHERE eid IN (SELECT eid FROM event_member WHERE uid='" + fbid + "') AND start_time > now()");
            if (dstemp != null)
            {
                if (dstemp.Tables.Count > 1)
                {
                    foreach (DataRow r in dstemp.Tables[1].Rows)
                    {
                        if (r.Table.Columns.Contains("start_time"))
                        {
                            r["start_time"] = Master.fbDateTime(Convert.ToInt32(r["start_time"].ToString()));
                        }
                        if (r.Table.Columns.Contains("name"))
                        {
                            if (r["name"].ToString().Length > 30)
                            {
                                r["name"] = r["name"].ToString().Substring(0, 30);
                            }
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
                bool IsUnique;
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
                }
            }
            */
        }

        /*protected void btnExcel_Click(object sender, EventArgs e)
        {
            LoadImportEvents();
            Page.RegisterStartupScript("popup3", "<script language=javascript>popup3();</script>");
        }*/

        protected void btnImportfbEvent(string eid)
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string result = wc.DownloadString("https://graph.facebook.com/"+eid);
            try
            {
                JObject o = JObject.Parse(result);
                string eventname = (string)o["name"];
                string description = (string)o["description"];
                DateTime starttime = DateTime.Parse((string)o["start_time"]);
                DateTime endtime = DateTime.Parse((string)o["end_time"]);
                Eventomatic_DB.SPs.UpdateEvent(0, eventname, "", starttime, endtime, "", "", "", "", "", description, true, "",
                    Convert.ToInt32(Master.GetResourceKey()), eid, 0, 10, 0, 0, false, 1).Execute();
                Load_PromotedEvents();
            }
            catch
            {
            }            
        }

        protected void Load_PromotedEvents()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewListCurrentEventsPromoting(Convert.ToInt32(Master.GetResourceKey())).GetDataSet();
            RadListView1.DataSource = dstemp.Tables[0];
            RadListView1.DataBind();
        }

        protected void btnaddgroup_Click(object sender, EventArgs e)
        {
            //Adding New Group
            if (txtNewGroup.Text != "")
            {
                Site sitetemp = new Site();
                bool IsUnique = sitetemp.IsUniqueGroupStore(txtNewGroup.Text);

                if (IsUnique)
                {
                    ListItem litemp = new ListItem();
                    litemp.Value = "";
                    litemp.Text = txtNewGroup.Text;

                    Eventomatic_DB.SPs.UpdateGroups(Convert.ToInt64(Master.getfbid()), 0, txtNewGroup.Text,
                        Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFP").ToString()),
                        Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFC").ToString()),
                        Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFM").ToString()),
                        0).Execute();
                    HiddenField hdnFirstTime = new HiddenField();
                    hdnFirstTime = (HiddenField)Master.FindControl("hdnFirstTime");      
                    hdnFirstTime.Value = "0";
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblErrorNoName.Visible = true;
                    lblErrorNoName.Text = "Unfortunately the Group Name already Exists. Please select another name.";
                    lblErrorNoName.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblErrorNoName.Visible = true;
                lblErrorNoName.Text = "*Please enter your new Groupstore name.";
                lblErrorNoName.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item is RadListViewDataItem)
            {
                RadBinaryImage rdbi = (RadBinaryImage)e.Item.FindControl("radimage");
                //HyperLink hypeventname = (HyperLink)e.Item.FindControl("hypevent");
                Label lbleid = (Label)e.Item.FindControl("lbleventid");
                Label lblremove = (Label)e.Item.FindControl("lblremove");
                Label lbleventkey = (Label)e.Item.FindControl("lbleventkey");

                Site sitetemp = new Site();                
                rdbi.ImageUrl = sitetemp.getgraphimg(lbleid.Text);
                lblremove.Text = "<a href='#' onclick='DoRemovePromoted(" + lbleventkey.Text + "); return false;'>Remove</a>";
            }
        }

        protected void DoRemovePromoted(string Eventid)
        {
            Eventomatic_DB.SPs.UpdateEventRemove(Convert.ToInt32(Eventid), false).Execute();
            Load_PromotedEvents();
        }
    }
}
