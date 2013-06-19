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
using Infragistics.WebUI.UltraWebTab;
using SubSonic;

namespace Eventomatic.Addons
{
    public partial class Tickets : System.Web.UI.UserControl
    {
        fbuser fbcurrentuser_global = new fbuser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Webdatechooser date format
                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN");
                ci.DateTimeFormat.LongDatePattern = "dddd, MMMM d, yyyy";
                ci.DateTimeFormat.ShortDatePattern = "dddd, MMMM d, yyyy";                  
                Begin_Selling.CalendarLayout.Culture = ci;
                Selling_Deadline.CalendarLayout.Culture = ci;
                
            }
            else
            {
                if (Request.Form["__EVENTTARGET"] == "DoRemove2")
                {
                    DoRemove(Request["__EVENTARGUMENT"].ToString());
                }
            }
        }

        public void LoadPage() 
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicketAll(Convert.ToInt32(Event_Key.Value)).GetDataSet();
            UltraWebGridT1.DataSource = dstemp.Tables[0];
            UltraWebGridT1.DataBind();
            
            foreach (UltraGridRow row in UltraWebGridT1.Rows)
            {
                row.Cells[6].Text = "<a href='#' onclick=javascript:popup3(" + Convert.ToString(row.Index + 1) + ");>Edit</a> | <a href='javascript:doRemove2(" + row.Index + ");'>Remove</a>";
                DataSet dstempsellers = Eventomatic_DB.SPs.ViewTicketSellers(Convert.ToInt32(Event_Key.Value), Convert.ToInt32(row.Cells[0].Value)).GetDataSet();
                string strsellers = "";
                string strsellers_name = "";
                Site sitetemp = new Site();
                fbuser searchingfbuser = new Eventomatic.Addons.fbuser();
                foreach (DataRow r in dstempsellers.Tables[0].Rows)
                {
                    if (r["FBid"] != DBNull.Value)
                    {
                        strsellers += "|" + r["FBid"].ToString();
                        searchingfbuser = sitetemp.getfbuserinfo(Convert.ToInt64(r["FBid"].ToString()), fbcurrentuser_global);
                        strsellers_name += "|" + searchingfbuser.Fullname;
                    }                    
                }
                row.Cells[7].Text = strsellers;
                row.Cells[8].Text = strsellers_name;
            }            
        }

        public void LoadFriendslist(DataTable dtFriends)
        {                
            DataTable dtFriendsList = dtFriends;            
            /*lbFriendsList_Temp.DataSource = dtFriendsList;
            lbFriendsList_Temp.DataTextField = "Name";
            lbFriendsList_Temp.DataValueField = "fbid";
            lbFriendsList_Temp.DataBind();*/

            lbFriendsList.DataSource = dtFriendsList;
            lbFriendsList.DataTextField = "Name";
            lbFriendsList.DataValueField = "fbid";
            lbFriendsList.DataBind();
        }

        protected void btnSaveTicket_Click(object sender, EventArgs e)
        {
            //Click on save Changes for the editing Question
            int intTicket_Key = Convert.ToInt32(hdnTicket_Key.Value);
            int thecurrentrow = 0;

            if (intTicket_Key == 0)//A new Question
            {
                UltraWebGridT1.Rows.Add();
                UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].Cells[0].Text = "-1";
                ModifyTicket(UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1]);
                UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].DataChanged = DataChanged.Added;
                UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].Cells[6].Text = "<a href='#' onclick=javascript:popup3(" + Convert.ToString(UltraWebGridT1.Rows.Count) + ");>Edit</a> | <a href='javascript:doRemove2(" + Convert.ToString(UltraWebGridT1.Rows.Count) + ");'>Remove</a>";
                thecurrentrow = UltraWebGridT1.Rows.Count - 1;
            }
            else
            {
                foreach (UltraGridRow row in UltraWebGridT1.Rows)
                {
                    if (row.Cells[0].Text == intTicket_Key.ToString())
                    {
                        row.DataChanged = DataChanged.Modified;
                        ModifyTicket(row);
                        thecurrentrow = row.Index;
                    }
                }
            }

            bool isrefreshpage = false;

            if (Event_Key.Value == "0")
            {
                Infragistics.WebUI.UltraWebTab.ContentPane tempcontent = (Infragistics.WebUI.UltraWebTab.ContentPane)Parent;
                Edit_Event editeventpage = (Edit_Event)tempcontent.Page;                
                

                Hashtable hstemp = editeventpage.getEventinfo();
                DateTime tempStartdate = Convert.ToDateTime(hstemp["tempStartdate"]);
                DateTime tempEnddate = Convert.ToDateTime(hstemp["tempEnddate"]);                
                bool tempbool = Convert.ToBoolean(hstemp["chkDisplayAvailable"]);

                StoredProcedure sp_UpdateEvent = Eventomatic_DB.SPs.UpdateEvent(0, hstemp["txtEventName"].ToString(), hstemp["txtHost"].ToString(),tempStartdate ,
            tempEnddate, hstemp["txtLocation"].ToString(), hstemp["txtStreet"].ToString(), hstemp["txtCity"].ToString(), hstemp["txtPhone"].ToString(), hstemp["txtEmail"].ToString(), hstemp["txtComments"].ToString(),
            tempbool,hstemp["txtConfirmation"].ToString(),Convert.ToInt32(hstemp["Resource_Key"]),hstemp["eid"].ToString(), 0,Convert.ToInt32(hstemp["ddlMaxTickets"]),Convert.ToDecimal(hstemp["ddlTimezone"]),Convert.ToInt32(hstemp["tempticketnum"]),Convert.ToBoolean(hstemp["chkfundraiser"]),0);
                sp_UpdateEvent.Execute();
                Event_Key.Value = sp_UpdateEvent.Command.Parameters[15].ParameterValue.ToString();
                isrefreshpage = true;
                Eventomatic_DB.SPs.UpdateEventForTicket(Convert.ToInt32(Event_Key.Value)).Execute();
            }

            //update the specific ticket            
            Site sitetemp = new Site();
            sitetemp.Update_DB(UltraWebGridT1.Rows[thecurrentrow],0, Convert.ToInt32(Event_Key.Value));
            LoadPage();

            if (isrefreshpage)
            {
                //Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() +"Edit_Event.aspx?Event_Key=" + Event_Key.Value);
                Page.RegisterStartupScript("Myscript", "<script language=javascript>location.href = 'Edit_Event.aspx?Event_Key=" + Event_Key.Value + "';</script>");
            }
        }

        public void Addfbticket(groupstore_ticket gsticketemp)
        {
            UltraWebGridT1.Rows.Add();
            UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].Cells[0].Text = "-1";
            UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].Cells[1].Value = gsticketemp.Description;
            UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].Cells[2].Value = gsticketemp.BeginSelling;
            UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].Cells[3].Value = gsticketemp.SellingDeadline;
            UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].Cells[4].Value = gsticketemp.Price;
            UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].Cells[5].Value = gsticketemp.Capacity;
            
            UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].DataChanged = DataChanged.Added;
            UltraWebGridT1.Rows[UltraWebGridT1.Rows.Count - 1].Cells[6].Text = "<a href='#' onclick=javascript:popup3(" + Convert.ToString(UltraWebGridT1.Rows.Count) + ");>Edit</a> | <a href='javascript:doRemove2(" + Convert.ToString(UltraWebGridT1.Rows.Count) + ");'>Remove</a>";
        }

        protected void ModifyTicket(UltraGridRow row)
        {
            
            row.Cells[0].Value = hdnTicket_Key.Value;
            row.Cells[1].Value = txtdescription.Text;
            DateTime tempStartdateSelling = Convert.ToDateTime(Begin_Selling.Value).AddHours(Convert.ToDateTime(Begin_Selling_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Begin_Selling_Time.Value).Minute);
            DateTime tempEnddateSelling = Convert.ToDateTime(Selling_Deadline.Value).AddHours(Convert.ToDateTime(Selling_Deadline_Time.Value).Hour).AddMinutes(Convert.ToDateTime(Selling_Deadline_Time.Value).Minute);
            row.Cells[2].Value = tempStartdateSelling;
            row.Cells[3].Value = tempEnddateSelling;
            if (hdnPrice.Value == "1")
            {
                row.Cells[4].Value = WebCurrencyEdit1.Value;
            }            
            row.Cells[5].Value = WebMaskEdit1.Value;
            //Change that specific row
            /*TemplatedColumn col1 = (TemplatedColumn)row.Cells.FromKey("Col1").Column;
            CellItem cellItemCol1 = (CellItem)col1.CellItems[row.Index];
            Label lblDescription = (Label)cellItemCol1.FindControl("lblDescription");
            Label lblStarts = (Label)cellItemCol1.FindControl("lblStarts");
            Label lblEnds = (Label)cellItemCol1.FindControl("lblEnds");
            Label lblPrice = (Label)cellItemCol1.FindControl("lblPrice");
            Label lblCapacity = (Label)cellItemCol1.FindControl("lblCapacity");
            */
            //update ticket sellers            
            row.Cells[7].Value = hdsellers.Value;
            row.Cells[8].Value = hdsellersnames.Value;
            //update donation box
            row.Cells[9].Value = chkdonation.Checked.ToString();
        }

        protected void DoRemove(string Rowid)
        {
            /*int rowtoremove = 0;
            foreach (UltraGridRow row in UltraWebGridT1.Rows)
            {
                if (row.Cells[0].Text == Rowid)
                {
                    rowtoremove = row.Index;
                }
            }*/
            //UltraWebGridT1.Rows[rowtoremove].DataChanged = DataChanged.Deleted;
            int txkey = Convert.ToInt32(UltraWebGridT1.Rows[Convert.ToInt32(Rowid)].Cells[0].ToString());
            Site sitetemp = new Site();
            sitetemp.DeleteRow(txkey, 0);
            UltraWebGridT1.Rows.RemoveAt(Convert.ToInt32(Rowid));
        }

        public void SaveTickets(int intEvent_Key)
        {
            //Eventomatic_DB.SPs.DeleteTicketEventKey(Convert.ToInt32(Event_Key.Value)).Execute();            
            /*foreach (UltraGridRow row in UltraWebGridT1.Rows)
            {
                DateTime tempStartdateSelling = Convert.ToDateTime(row.Cells[2].Value);
                DateTime tempEnddateSelling = Convert.ToDateTime(row.Cells[3].Value);
                Eventomatic_DB.SPs.UpdateTicket(Convert.ToInt32(row.Cells[0].Value), Convert.ToInt32(Event_Key.Value), row.Cells[1].Value.ToString(), Convert.ToDecimal(row.Cells[4].Value),Convert.ToInt32(row.Cells[5].Value), tempStartdateSelling, tempEnddateSelling).Execute();
            }*/
            Event_Key.Value = intEvent_Key.ToString();
            Site sitetemp = new Site();
            sitetemp.UpdateUltraWebGrid(UltraWebGridT1, 0, Convert.ToInt32(Event_Key.Value),true);
            //LoadPage(Currentfbuser.
        }

        public fbuser fbcurrentuser
        {
            get { return fbcurrentuser_global; }
            set { fbcurrentuser_global = value; }
        }

        public void SaveSellers(int intEvent_Key)
        {
            Event_Key.Value = intEvent_Key.ToString();
            DataSet dstemp = Eventomatic_DB.SPs.ViewTicket(intEvent_Key).GetDataSet();
            foreach (DataRow r in dstemp.Tables[0].Rows)
            {

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Move data from dropdown to listbox
            ListItem litemp = new ListItem();
            litemp.Value = lbFriendsList.SelectedItem.Value;
            litemp.Text = lbFriendsList.SelectedItem.Text;
            lbAdmins.Items.Add(litemp);            

            lbFriendsList.Items.RemoveAt(lbFriendsList.SelectedIndex);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            //Move data from listbox to dropdown            
            ListItem litemp = new ListItem();
            litemp.Value = lbAdmins.SelectedItem.Value;
            litemp.Text = lbAdmins.SelectedItem.Text;
            lbFriendsList.Items.Add(litemp);
            lbAdmins.Items.RemoveAt(lbAdmins.SelectedIndex);
        }
    }
}