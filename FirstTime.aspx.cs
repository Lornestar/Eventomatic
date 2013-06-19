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
//using facebook;
//using facebook.web;
//using facebook.Schema;
using Infragistics.WebUI.UltraWebGrid;
using Eventomatic.Addons;
using SubSonic;
using System.IO;

namespace Eventomatic
{
    public partial class FirstTime : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "SaveAll")
            {
                try
                {
                    SaveAll();
                }
                catch
                {
                }
                Response.Redirect("default.aspx");
            }
            if (!IsPostBack)
            {
                CheckAdminof();
                init_cheque();

                string strfbid = Master.getfbid();

                HiddenField hdnfbid = new HiddenField();
                hdnfbid = (HiddenField)Add_Tab1.FindControl("Add_Tab_fbid");
                hdnfbid.Value = strfbid;

                DataSet dstemp = Eventomatic_DB.SPs.ViewStoreSellers(Convert.ToInt64(strfbid)).GetDataSet();

                if ((!Master.CheckIfIsSeller(strfbid)) && (dstemp.Tables[0].Rows.Count > 0))
                {
                    Page.RegisterStartupScript("Myscript", "<script language=javascript>popup_Add_Tab();</script>");
                }                
            }
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
                    lblErrorNoName.Visible = false;
                    //Response.Redirect("Default.aspx");
                    LoadStep2();
                    hdstep.Value = "2";
                    UltraWebTab1.SelectedTab = 1;
                    UltraWebTab1.Tabs[0].Text = "Step 1 (" + txtNewGroup.Text + ") <img src='Images/arrow_forward.gif' />";               
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

        protected void CheckAdminof()
        {
            //Setup dtGroupAdmin
            DataTable dtAdmins = new DataTable("Admins");
            DataColumn cltemp;
            DataRow rtemp;

            cltemp = new DataColumn();
            cltemp.DataType = System.Type.GetType("System.String");
            cltemp.ColumnName = "id";
            cltemp.ReadOnly = false;
            cltemp.Unique = false;
            dtAdmins.Columns.Add(cltemp);

            cltemp = new DataColumn();
            cltemp.DataType = System.Type.GetType("System.Boolean");
            cltemp.ColumnName = "IsGroup";
            cltemp.ReadOnly = false;
            cltemp.Unique = false;
            dtAdmins.Columns.Add(cltemp);

            cltemp = new DataColumn();
            cltemp.DataType = System.Type.GetType("System.String");
            cltemp.ColumnName = "Name";
            cltemp.ReadOnly = false;
            cltemp.Unique = false;
            dtAdmins.Columns.Add(cltemp);

            cltemp = new DataColumn();
            cltemp.DataType = System.Type.GetType("System.Int32");
            cltemp.ColumnName = "Rank";
            cltemp.ReadOnly = false;
            cltemp.Unique = false;
            dtAdmins.Columns.Add(cltemp);

            cltemp = new DataColumn();
            cltemp.DataType = System.Type.GetType("System.String");
            cltemp.ColumnName = "Pic";
            cltemp.ReadOnly = false;
            cltemp.Unique = false;
            dtAdmins.Columns.Add(cltemp);
            //Rank is out of 10, the higher the better
            //Group admin = 5
            //Group officer = 3
            //Page admin = 8

            

            //Checks if they are admin of Group or Page
            fbuser fbuser = Master.getfbuser();
            string fbid = fbuser.UID.ToString();

            DataSet dstemp;            

            //First find which groups the person is admin for
            dstemp = Master.ExecuteQuery("SELECT gid, positions FROM group_member WHERE uid=" + fbid);
            //dstemp = Master.GetGroups();
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
                                rtemp = dtAdmins.NewRow();
                                rtemp["id"] = dstemp.Tables[1].Rows[dstemp.Tables[2].Rows.IndexOf(r)][0].ToString();
                                rtemp["IsGroup"] = true;
                                rtemp["Rank"] = 5;
                                dtAdmins.Rows.Add(rtemp);
                            }
                            else if (r["member_type"].ToString() == "OFFICER")
                            {
                                rtemp = dtAdmins.NewRow();
                                rtemp["id"] = dstemp.Tables[1].Rows[dstemp.Tables[2].Rows.IndexOf(r)][0].ToString();
                                rtemp["IsGroup"] = true;
                                rtemp["Rank"] = 3;
                                dtAdmins.Rows.Add(rtemp);
                            }
                        }
                        
                    }
                }
            }

            //Get Pages is admin of
            dstemp = Master.ExecuteQuery("SELECT page_id, name, pic From page WHERE page_id IN (SELECT page_id FROM page_admin WHERE uid =" + fbid + ")");
            if (dstemp != null)
            {
                if (dstemp.Tables.Count >= 2)
                {
                    foreach (DataRow r in dstemp.Tables[1].Rows)
                    {
                        rtemp = dtAdmins.NewRow();
                        rtemp["id"] = r["page_id"].ToString();
                        rtemp["IsGroup"] = false;
                        rtemp["Name"] = r["name"].ToString();
                        rtemp["Rank"] = 8;
                        rtemp["Pic"] = r["pic"].ToString();
                        dtAdmins.Rows.Add(rtemp);
                    }
                }
            }

            Hashtable htGroupsuggest = new Hashtable();
            htGroupsuggest.Add("Name", "");
            htGroupsuggest.Add("id", "");
            htGroupsuggest.Add("Rank", 0);
            htGroupsuggest.Add("IsGroup", false);            
            //They are admin of something so decide what Group/Page should be put in the Box
            if (dtAdmins.Rows.Count > 0)
            {
                foreach (DataRow r in dtAdmins.Rows)
                {
                    if (Convert.ToInt32(r["Rank"]) > Convert.ToInt32(htGroupsuggest["Rank"]))
                    {
                        htGroupsuggest["Rank"] = Convert.ToInt32(r["Rank"]);                        
                        htGroupsuggest["Name"] = r["Name"].ToString();
                        htGroupsuggest["id"] = r["id"].ToString();
                        htGroupsuggest["IsGroup"] = Convert.ToBoolean(r["IsGroup"]);
                    }
                }
            }
            if (htGroupsuggest["id"].ToString() != "")
            {
                if (Convert.ToBoolean(htGroupsuggest["IsGroup"]))
                {
                    dstemp = Master.ExecuteQuery("SELECT name FROM group WHERE gid=" + htGroupsuggest["id"].ToString());
                    htGroupsuggest["Name"] = dstemp.Tables[1].Rows[0]["name"].ToString();
                }
            }
            txtNewGroup.Text = htGroupsuggest["Name"].ToString();
            Session["htGroupsuggest"] = htGroupsuggest;
            UltraWebGrid2.DataSource = dtAdmins;
            UltraWebGrid2.DataBind();
        }

        protected void LoadStep2()
        {            
            Site sitetemp = new Site();
            DataSet dstemp = new DataSet();
            try
            {
                dstemp =  sitetemp.RankEvents(txtNewGroup.Text);
            }
            catch
            {
            }
            if (dstemp != null)
            {
                if (dstemp.Tables.Count > 1)//At least 1 event to sell
                {
                    if (dstemp.Tables[1].Rows.Count > 1)//At least 2 events to sell
                    {
                        //Order rankings
                        DataTable dttemp = dstemp.Tables[1].Copy();
                        dttemp.DefaultView.Sort = "[rank] desc";

                        //if top event has ranking greater than 29 than will suggest it
                        if (Convert.ToInt32(dttemp.DefaultView[0]["rank"]) > 29)
                        {

                            if (dttemp.DefaultView[0]["pic_small"].ToString() != "")
                            {
                                imgSuggestedEvent.ImageUrl = dttemp.DefaultView[0]["pic_small"].ToString();
                            }
                            lblSuggestedEvent.Text = dttemp.DefaultView[0]["name"].ToString();
                            lblSuggestedEventtime.Text = dttemp.DefaultView[0]["start_time"].ToString();
                            hdeid.Value = dttemp.DefaultView[0]["eid"].ToString();
                            dttemp.Rows.Remove(dttemp.DefaultView[0].Row);
                            hdsuggest.Value = "1";
                        }
                        else
                        {
                            hdsuggest.Value = "0";
                        }
                        pnlHasEvent.Visible = true;
                        hdcustomevent.Value = "0";
                        UltraWebGrid1.DataSource = dttemp;
                        UltraWebGrid1.DataBind();
                    }
                    else if (dstemp.Tables[1].Rows.Count == 1)//only 1 event
                    {
                        pnlHasEvent.Visible = false;
                        pnlOnlyOneEvent.Visible = true;
                        pnlisinlist.Visible = false;
                        hdcustomevent.Value = "0";
                        hdsuggest.Value = "1";
                        if (dstemp.Tables[1].Rows[0]["pic_small"].ToString() != "")
                        {
                            imgSuggestOnlyEvent.ImageUrl = dstemp.Tables[1].Rows[0]["pic_small"].ToString();
                        }
                        lblSuggestOnlyEvent.Text = dstemp.Tables[1].Rows[0]["name"].ToString();
                        lblSuggestOnlyEventtime.Text = dstemp.Tables[1].Rows[0]["start_time"].ToString();
                        hdeid.Value = dstemp.Tables[1].Rows[0]["eid"].ToString();
                    }

                    //Add Sell your event in FB Import Events
                    bool IsUnique;
                    string strtempchecked = "CHECKED";
                    foreach (UltraGridRow row in UltraWebGrid1.Rows)
                    {
                        //row.Cells[3].Text = "<a href='javascript:Choose_eid(" + row.Cells[4].ToString() + ");'>Sell Event!</a>";                    
                        row.Cells[1].Text = "<input type='radio' name='eid' value='" + row.Cells[4].ToString() + "' onClick='Choose_eid(" + row.Cells[4].ToString() + ")'" + strtempchecked + " />";
                        strtempchecked = "";
                    }
                }
                else//means there are no events
                {
                    pnlHasEvent.Visible = false;
                    hdcustomevent.Value = "1";
                    hdsuggest.Value = "0";
                    hdEventstagechosen.Value = "2";
                    pnlisinlist.Visible = false;
                }
                
                
                //Criteria to go over --> Is word ticket or sell mentioned, Are they admin of event,
            }
            else//means some sort of error occured in reading events
            {
                pnlHasEvent.Visible = false;
                hdcustomevent.Value = "1";
                hdsuggest.Value = "0";
                hdEventstagechosen.Value = "2";
                pnlisinlist.Visible = false;
            }
            //Initialize dates
            //Webdatechooser date format
            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN");
            ci.DateTimeFormat.LongDatePattern = "dddd, MMMM d, yyyy";
            ci.DateTimeFormat.ShortDatePattern = "dddd, MMMM d, yyyy";
            StartDate.CalendarLayout.Culture = ci;
            EndDate.CalendarLayout.Culture = ci;
            StartDate.Value = DateTime.Now.AddDays(14).Date;
            EndDate.Value = DateTime.Now.AddDays(15).Date;
            txtEventName.Text = txtNewGroup.Text + " Party";
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Changed Country Selection         
            DataSet dstemp;
            if (ddlCountry.SelectedItem.Text == "United States")
            {
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(2).GetDataSet();
                ddlStateProvince.DataSource = dstemp.Tables[0];
                lblAreaZipCode.Text = "Zip Code";
                lblStateProvince.Text = "State";
                ddlStateProvince.Visible = true;
                txtPayStateProvince.Visible = false;
                RequiredFieldValidator10.Enabled = false;
            }
            else if (ddlCountry.SelectedItem.Text == "Canada")
            {
                dstemp = Eventomatic_DB.SPs.ViewInfoRegion(1).GetDataSet();
                ddlStateProvince.DataSource = dstemp.Tables[0];
                lblAreaZipCode.Text = "Postal Code";
                lblStateProvince.Text = "Province";
                ddlStateProvince.Visible = true;
                txtPayStateProvince.Visible = false;
                RequiredFieldValidator10.Enabled = false;
            }            
            else
            {
                ddlStateProvince.Visible = false;
                lblAreaZipCode.Text = "Postal/Zip Code";
                lblStateProvince.Text = "State / Province / Region";
                txtPayStateProvince.Visible = true;
                RequiredFieldValidator10.Enabled = true;
            }
            ddlStateProvince.DataTextField = "Region_Text";
            ddlStateProvince.DataValueField = "Region_Value";
            ddlStateProvince.DataBind();
        }

        protected void init_cheque()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewInfoRegion(2).GetDataSet();
            ddlStateProvince.DataSource = dstemp.Tables[0];
            lblAreaZipCode.Text = "Zip Code";
            lblStateProvince.Text = "State";
            ddlStateProvince.Visible = true;
            txtPayStateProvince.Visible = false;
            RequiredFieldValidator10.Enabled = false;
        }

        protected void SaveAll()
        {
            //Save Tab 1 - Groupname
            Int64 fbid = Convert.ToInt64(Master.getfbid());
            StoredProcedure sp_AddGroup = Eventomatic_DB.SPs.UpdateGroups(fbid, 0, txtNewGroup.Text,
                Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFP").ToString()),
                Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFC").ToString()),
                Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFM").ToString()),
                0);
            sp_AddGroup.Execute();
            int resourcekey = Convert.ToInt32(sp_AddGroup.Command.Parameters[6].ParameterValue.ToString());            

            //Save Tab 2 - First Event
            string strtempeid = "0";
            groupstore_event gsevent = new groupstore_event();
            Site Sitetemp = new Site();
            switch (hdEventstagechosen.Value)
            {
                case "0"://Suggestion
                    strtempeid = hdeid.Value;
                    gsevent = Sitetemp.Importfbevent(strtempeid, fbid);
                    break;
                case "1"://ultragrid
                    strtempeid = hdeid.Value;
                    gsevent = Sitetemp.Importfbevent(strtempeid,fbid);
                    break;
                case "2"://Custom
                    gsevent.Eventname = txtEventName.Text;
                    gsevent.EventBegins = Convert.ToDateTime(StartDate.Value);
                    gsevent.EventEnds = Convert.ToDateTime(EndDate.Value);

                    groupstore_ticket temptickets = new groupstore_ticket();
                    temptickets.Description = "General Admission";
                    temptickets.BeginSelling = DateTime.Now;
                    temptickets.SellingDeadline = gsevent.EventBegins.AddHours(-3);
                    temptickets.Capacity = 100;
                    temptickets.Price = 15;

                    gsevent.Tickets = new groupstore_ticket[1];
                    gsevent.Tickets.SetValue(temptickets, 0);

                    //ticket num
                    gsevent.Ticketnum = 0;
                    Random random = new Random();
                    gsevent.Ticketnum = random.Next(1500, 7000);
                    break;
            }
            
            //save in db new event
            StoredProcedure sp_UpdateEvent = Eventomatic_DB.SPs.UpdateEvent(0, gsevent.Eventname, gsevent.Host, gsevent.EventBegins,
            gsevent.EventEnds, gsevent.Location, "", "", "", "", gsevent.Additionalcomments,
            false, "Thank you for your purchase.", resourcekey, strtempeid, 0, 10, 0, gsevent.Ticketnum, false,0);

            sp_UpdateEvent.Execute();
            int Event_Key = Convert.ToInt32(sp_UpdateEvent.Command.Parameters[15].ParameterValue.ToString());
            
            //save in db new ticket
            groupstore_ticket gsticketemp = (groupstore_ticket)gsevent.Tickets.GetValue(0);
            StoredProcedure sp_UpdateTicket = Eventomatic_DB.SPs.UpdateTicket(0, Event_Key, gsticketemp.Description, gsticketemp.Price, gsticketemp.Capacity, gsticketemp.BeginSelling,
                gsticketemp.SellingDeadline,0,false);
            sp_UpdateTicket.Execute();
            int ticket_key = Convert.ToInt32(sp_UpdateTicket.Command.Parameters[7].ParameterValue.ToString());
            //Eventomatic_DB.SPs.UpdateTicketSellers(ticket_key, Event_Key, fbid).Execute();

            string strImgTemp = "";            
            //img for Group
            // 0 is gid or pid / 1 is IsGroup / 2 is Name / 3 is Rank / 4 is Pic
            foreach (UltraGridRow r in UltraWebGrid2.Rows)
            {
                if ((txtNewGroup.Text == r.Cells[2].ToString()) && (r.Cells[4].ToString() != "") && (r.Cells[4].ToString().ToLower() != "cell"))
                {
                    Master.savepicurl(r.Cells[4].ToString(), resourcekey.ToString());
                    strImgTemp = "Temp" + resourcekey.ToString() + ".jpg";                    
                    if (Sitetemp.ImgExists(0, strImgTemp))
                    {
                        string strPath = Server.MapPath("/Images/Events/" + strImgTemp);
                        string strPath_Group = Server.MapPath("/Images/Groups/" + strImgTemp);
                        string strNewPath = strPath_Group.Replace(strImgTemp, Event_Key.ToString() + ".jpg");
                        File.Move(strPath, strNewPath);
                    }
                }
            }

            if ((gsevent.Imageurl != "") && (gsevent.Imageurl != null))
            {
                //Save group img            
                Master.savepicurl(gsevent.Imageurl, resourcekey.ToString());
                strImgTemp = "Temp" + resourcekey.ToString() + ".jpg";
                if (Sitetemp.ImgExists(0, strImgTemp))
                {
                    string strPath = Server.MapPath("/Images/Events/" + strImgTemp);
                    string strNewPath = strPath.Replace(strImgTemp, Event_Key.ToString() + ".jpg");
                    File.Move(strPath, strNewPath);
                }

            }
            
            //try to save group/page image


            //Save Tab 3 - Payment Options
            switch (hdpayment.Value)
            {
                case "1"://Paypal
                    Eventomatic_DB.SPs.UpdateResourceProfile(resourcekey, ddlCurrency.SelectedValue, txtPaypal.Text,true,0).Execute();
                    Addons.Paypal_Confirmation Paypal_Confirmation1 = new Addons.Paypal_Confirmation();
                    Paypal_Confirmation1.enteredemail(txtPaypal.Text, ddlCurrency.SelectedValue, resourcekey);
                    break;
                case "2"://By cheque                    
                    break;
                case "3"://Skip                    
                    break;
            }

            //Record Activity
            Eventomatic.Addons.Activities activity = new Activities();
            activity.NewActivity(5,resourcekey , fbid,0,0,0);

            

        }

        
    }
}

