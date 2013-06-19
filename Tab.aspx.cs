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
using System.Drawing;

namespace Eventomatic
{
    public partial class Tab : System.Web.UI.Page
    {

        int Event_Key_Global = 0;
        int Ticket_Max_Global = 0;
        int Gridview1_index_Global = 0;
        string strfbid_Global = "391377955486";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                /*HttpContext.Current.Request.QueryString["Percent"] = "30";
                HttpContext.Current.Request.QueryString["Max"] = "50";
                HttpContext.Current.Request.QueryString["Dollars"] = "1";
                HttpContext.Current.Request.Headers["If-None-Match"] = "";
                Eventomatic.Addons.Thermometer thermometer = new Eventomatic.Addons.Thermometer();
                thermometer.ProcessRequest(HttpContext.Current);*/

                string strfbid = "391377955486";
                if (Request.Form["fb_sig_profile_id"] != null)
                {
                    strfbid = Request.Form["fb_sig_profile_id"].ToString();
                }
                else if (Request.QueryString["fb_sig_profile_id"] != null)
                {
                    strfbid = Request.QueryString["fb_sig_profile_id"].ToString();
                }
                txtfbid.Text = strfbid;


                lblFirstName.Text = "<fb:name uid='" + strfbid + "' capitalize='true' firstnameonly='true' possessive='true' />";
                lblreferal.Text = "<a href=" + System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "refer.aspx?gsref=" + strfbid + ">Open Your Groupstore</a> ";

                //gridview stuff
                int Resource_Key = 3;
                DataSet dstemp = Eventomatic_DB.SPs.ViewStoreSellers(Convert.ToInt64(strfbid)).GetDataSet();

                
                GridView1.DataSource = dstemp.Tables[0];
                GridView1.DataBind();

                if (dstemp.Tables[0].Rows.Count == 1)
                {
                    System.Web.UI.WebControls.Panel pnlEvent_Descriptionfull = (System.Web.UI.WebControls.Panel)GridView1.Rows[0].FindControl("pnlEvent_Descriptionfull");
                    pnlEvent_Descriptionfull.Attributes.Remove("style");
                    //Page.RegisterStartupScript("Myscript", "<script language=javascript>opendescription();</script>");
                }

                

                if ((GridView1.Rows.Count == 0) || (dstemp.Tables[0].Rows.Count == 0))
                {
                    pnlGroupName.Visible = false;                    
                    lblheader.Visible = true;
                    lblheader.Text = "There are currently no events on sale.";
                }
            }
            
            for (int i = 0; i < Request.Form.Count; i++)
            {
                lblquery.Text += Request.Form.Keys[i].ToString() + " - " + Request.Form[i].ToString() + " // ";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblquery.Text = "";
            for (int i = 0; i < Request.QueryString.Count; i++)
            {
                lblquery.Text += Request.Form.Keys[i].ToString() + " - " + Request.Form[i].ToString() + " // "; 
                //lblquery.Text += Request.QueryString.Keys[i].ToString() + " - " + Request.QueryString[i].ToString() + " \n ";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string strfbid = "100000990150671";
            if (Request.Form["fb_sig_profile_id"] != null)
            {
                strfbid = Request.Form["fb_sig_profile_id"].ToString();
            }
            
            Site sitetemp = new Site();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Label lblEventKey = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblEvent_Key");
                System.Web.UI.WebControls.Image imgEvent = (System.Web.UI.WebControls.Image)e.Row.FindControl("ImgEvent");
                imgEvent.ImageUrl = System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + sitetemp.GetEventPic(lblEventKey.Text);

                //Put in hyperlink
                Event_Key_Global = Convert.ToInt32(lblEventKey.Text);
                int Event_Key = Event_Key_Global;
                Eventomatic_DB.SPs.UpdateEventViews(Event_Key).Execute();
                System.Web.UI.WebControls.Label lblEventName = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblEvent_Name");
                System.Web.UI.WebControls.Label lblEventText = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblEvent_Description");
                string strEventName = lblEventName.Text;
                string strEventDesc = lblEventText.Text;
                if (sitetemp.IsSoldOutEvent(Event_Key))
                {
                    lblEventName.Text += " - Sold Out";
                }
                System.Web.UI.WebControls.HiddenField eid = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("hdeid");
                if (eid.Value != "")
                {
                    lblEventName.Text = "<a href='http://www.facebook.com/event.php?eid=" + eid.Value + "'>" + lblEventName.Text + "</a>";
                }

                //fix up descriptionfull with hyperlinks & spaces
                System.Web.UI.WebControls.Label lblEventTextfull = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblEvent_Descriptionfull");
                string strComments = lblEventTextfull.Text;
                string strComments2 = strComments.Replace(new String((char)13, 1), "<br>");
                if (strComments2.ToLower().Contains("http://"))
                {
                    Site sitetemp2 = new Site();
                    strComments2 = sitetemp2.ReplaceLinks(strComments2);
                }
                lblEventTextfull.Text = strComments2;

                //add see more to short event description if needed
                if (strComments.Length > 75)
                {
                    lblEventText.Text += "<br/><a href=# onclick='btnbuyfrom(" + e.Row.DataItemIndex.ToString() + "); return false;'>See More</a>";
                }

                System.Web.UI.WebControls.Label lblCustom_Message = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblCustom_Message");                
                lblCustom_Message.Text = " | <a href=# onclick='btncustommessage(" + e.Row.DataItemIndex.ToString() + "); return false;'>Custom Message</a>";
                System.Web.UI.WebControls.Label lblCustom_Message_Save = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblCustom_Message_Save");
                lblCustom_Message_Save.Text = "<a href=# onclick='btncustommessagesave(" + e.Row.DataItemIndex.ToString() + "); return false;'>Save Message</a>";

            //http://www.facebook.com/event.php?eid=109088222443617
                //hypEventName.NavigateUrl += lblEventKey.Text;
                //hypEventText.NavigateUrl = hypEventName.NavigateUrl;

                //Put in Calendar info
                //System.Web.UI.WebControls.Label lblMonth = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblMonth");
                //System.Web.UI.WebControls.Label lblDay = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDay");
                //update service fee amounts
                //System.Web.UI.WebControls.Label lblDate = (System.Web.UI.WebControls.Label)e.Row.FindControl("hdSFP");
                //System.Web.UI.WebControls.Label lblDate = (System.Web.UI.WebControls.Label)e.Row.FindControl("hdSFC");
                //System.Web.UI.WebControls.Label lblDate = (System.Web.UI.WebControls.Label)e.Row.FindControl("hdSFM");                                

                System.Web.UI.WebControls.Label lblAdminlinks = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblAdminlinks");
                lblAdminlinks.Text = "<a href=# onclick='ViewAdminsExternalLinks(" + e.Row.DataItemIndex.ToString() + "); return false;'>View External Link</a> | <a href='http://apps.facebook.com/groupstore/Seller_Attendee_List.aspx?event_key=" + Event_Key + "'>Your Sales</a> | <a href=# onclick='ViewShowGoals(" + e.Row.DataItemIndex.ToString() + "); return false;'>Show/Hide Goals</a>";
                
                System.Web.UI.WebControls.Label lblsharebtn = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblsharebtn");
                lblsharebtn.Text = "<fb:share-button class='meta'><meta name='medium' content='mult'/><meta name='title' content='" + strEventName +"'/>";
                if (sitetemp.isAlphaNumeric(strEventDesc))
                {
                    lblsharebtn.Text += "<meta name='description' content='" + strEventDesc + "'/>";
                }                
                lblsharebtn.Text += "<link rel='image_src' href='" + imgEvent.ImageUrl + "'/>";
                lblsharebtn.Text += "<link rel='target_url' href='http://www.facebook.com/profile.php?id=" + strfbid + "&v=app_" + System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString() + "'/>";

                System.Web.UI.WebControls.Label lblsharebtn2 = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblsharebtn2");
                lblsharebtn2.Text = lblsharebtn.Text;

                //Show/Hide Thermometer
                DataSet dsthermometer = Eventomatic_DB.SPs.ViewShowGoals(Event_Key, Convert.ToInt64(strfbid)).GetDataSet();
                string strshowgoals = "none;";
                if (dsthermometer.Tables[0].Rows.Count > 0)
                {
                    if (dsthermometer.Tables[0].Rows[0]["ShowGoals"] != DBNull.Value)
                    {
                        if (dsthermometer.Tables[0].Rows[0]["ShowGoals"].ToString() == "True")
                        {
                            strshowgoals = "block;";
                        }
                    }
                }
                
                System.Web.UI.WebControls.Panel pnlthermometer = (System.Web.UI.WebControls.Panel)e.Row.FindControl("pnlthermometer");
                pnlthermometer.Attributes.Add("style", "display:" + strshowgoals);

                //Set Goals
                string strGoalAmount = "0.00";
                if (dsthermometer.Tables[0].Rows.Count > 0)
                {
                    if (dsthermometer.Tables[0].Rows[0]["GoalAmount"] != DBNull.Value)
                    {
                        strGoalAmount = dsthermometer.Tables[0].Rows[0]["GoalAmount"].ToString();
                    }
                }
                
                System.Web.UI.WebControls.Label lblFundraisingGoal = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblFundraisingGoal");
                System.Web.UI.WebControls.TextBox txtFundraisingGoal = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtFundraisingGoal");
                                
                lblFundraisingGoal.Text = "$ " + strGoalAmount;
                txtFundraisingGoal.Text = strGoalAmount;
                txtFundraisingGoal.Attributes.Add("onKeyUp", "ChecktxtFundraisingGoal(" + Gridview1_index_Global.ToString() + "); return false;");

                string strRaised = "0.00";
                DataSet dsRaised = Eventomatic_DB.SPs.ViewAttendeeListSummarySellers(Event_Key, Convert.ToInt64(strfbid)).GetDataSet();
                if (dsRaised.Tables[0].Rows.Count > 0)
                {
                    strRaised = dsRaised.Tables[0].Rows[0]["FullAmount"].ToString();
                }
                System.Web.UI.WebControls.Label lblFundraised = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblFundraised");
                lblFundraised.Text = "$ " + strRaised;
                System.Web.UI.WebControls.TextBox txtFundraised = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtFundraised");
                txtFundraised.Text = strRaised;

                //Set Img Thermometer
                System.Web.UI.WebControls.Image imgThermometer = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgthermometer");
                decimal PercentageAmount = 0;
                if (strGoalAmount != "0.00")
                {
                    PercentageAmount = decimal.Round((decimal.Parse(strRaised) / decimal.Parse(strGoalAmount)) * 100);
                }                
                if (PercentageAmount > 100)
                {
                    PercentageAmount = 100;
                }
                string strRoot = System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString();
                imgThermometer.ImageUrl = strRoot + "/addons/thermometer.aspx?Percent=" + PercentageAmount.ToString() + "&max=" + decimal.Round(Convert.ToDecimal(strGoalAmount),0).ToString() + "&dollars=1";

                //Edit Fundraising Goal
                System.Web.UI.WebControls.Label lbleditFundraisingGoal = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbleditFundraisingGoal");
                lbleditFundraisingGoal.Text = "<a href=# onclick='editFundraisingGoal(" + e.Row.DataItemIndex.ToString() + "); return false;'>edit</a>";
                System.Web.UI.WebControls.Label lblsaveFundraisingGoal = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblsaveFundraisingGoal");
                lblsaveFundraisingGoal.Text = "<a href=# onclick='saveFundraisingGoal(" + e.Row.DataItemIndex.ToString() + "); return false;'>save</a>";

                System.Web.UI.WebControls.Label lblDate = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDate");
                System.Web.UI.WebControls.Label lblBegins = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblEvent_Begins");

                DateTime dtBegins = Convert.ToDateTime(lblBegins.Text);
                lblDate.Text = "<fb:date t='" + sitetemp.ConvertToUnixTimestamp(dtBegins).ToString() + "' format='verbose' />";                

                System.Web.UI.WebControls.GridView gridview2 = (System.Web.UI.WebControls.GridView)e.Row.FindControl("GridView2");


                //Set Next btn
                System.Web.UI.WebControls.Label lblNextbutton = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblNextbutton");
                DataSet dsisfundraiser = Eventomatic_DB.SPs.ViewIsFundraiser(Event_Key).GetDataSet();
                bool isfundraiser = false;
                if (dsisfundraiser.Tables[0].Rows.Count > 0)
                {
                    if (dsisfundraiser.Tables[0].Rows[0]["IsFundraiser"] != DBNull.Value)
                    {
                        if (dsisfundraiser.Tables[0].Rows[0]["IsFundraiser"].ToString() == "True")
                        {
                            isfundraiser = true;
                        }
                    }
                }

                System.Web.UI.WebControls.Label lblexternalsite = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblexternalsite");
                System.Web.UI.WebControls.Label lblBuy = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblBuy");
                

                if (isfundraiser)
                {
                    lblNextbutton.Text = "<a href=# onclick='btnnext_Fundraiser(" + e.Row.DataItemIndex.ToString() + "); return false;'>Next</a>";
                    lblexternalsite.Text = lblexternalsite.Text.Replace("THEURL", System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/order_form_fundraiser.aspx?event_key=" + Event_Key + "&fbid=" + strfbid);
                    lblBuy.Text = "<a href=# onclick='btnbuyfrom(" + e.Row.DataItemIndex.ToString() + "); return false;'>Donate to <fb:name uid='" + strfbid + "' capitalize='true' firstnameonly='true' reflexive='true' /></a>";
                }
                else
                {
                    lblNextbutton.Text = "<a href=# onclick='btnnext(" + e.Row.DataItemIndex.ToString() + "); return false;'>Next</a>";
                    lblexternalsite.Text = lblexternalsite.Text.Replace("THEURL", System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString() + "/order_form.aspx?event_key=" + Event_Key + "&fbid=" + strfbid);
                    lblBuy.Text = "<a href=# onclick='btnbuyfrom(" + e.Row.DataItemIndex.ToString() + "); return false;'>Buy from <fb:name uid='" + strfbid + "' capitalize='true' firstnameonly='true' reflexive='true' /></a>";
                }
                
                System.Web.UI.WebControls.Label lblBackbutton = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblBackbutton");
                lblBackbutton.Text = "<table><tr><td><img src=http://www.theGroupstore.com/Images/arrow2_back.jpg style=border-style:none;height:10px; /></td><td><a href=# onclick='btnback(" + e.Row.DataItemIndex.ToString() + "); return false;'>Back</a></td></tr></table>";
                Gridview1_index_Global = e.Row.DataItemIndex;

                //DataSet dstemp = Eventomatic_DB.SPs.ViewStoreTicketSellers(Event_Key,Convert.ToInt64(strfbid)).GetDataSet();
                DataSet dstemp = Eventomatic_DB.SPs.ViewTicket(Event_Key).GetDataSet();
                DataRow drtemp = dstemp.Tables[0].NewRow();
                //drtemp["Quantity"] = "Service Fee";
                //drtemp["Total"] = "<span id='lblServiceFee' style='display:inline-block;width:100px;'>$ 0.00</span>";
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    if (dstemp.Tables[0].Rows[0]["Display_Tickets_Available"] != DBNull.Value)
                    {                        
                        if (dstemp.Tables[0].Rows[0]["Display_Tickets_Available"].ToString() == "True")
                        {
                            GridView1.Columns[1].Visible = true;
                        }
                    }
                    if (dstemp.Tables[0].Rows[0]["Ticket_Max"] != DBNull.Value)
                    {
                        Ticket_Max_Global = Convert.ToInt32(dstemp.Tables[0].Rows[0]["Ticket_Max"].ToString());
                    }
                    else
                    {
                        Ticket_Max_Global = 10;
                    }
                }
                

                //fb comments
                System.Web.UI.WebControls.Label lblfbcomments = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblfbcomments");
                string appid = System.Configuration.ConfigurationSettings.AppSettings.Get("fbAppID").ToString();
                string xid = appid + "_" + Event_Key.ToString();
                lblfbcomments.Text = "<fb:comments xid='" + xid + "' canpost='true' returnurl='http://www.facebook.com/profile.php?id="+strfbid+ "?v=app_" + appid +"' candelete='false' simple='false' reverse='true' showform='true'></fb:comments>";                

                /*
                //see all
                DataSet dsallsellers = Eventomatic_DB.SPs.ViewSellersEventKey(Event_Key).GetDataSet();
                string strgroupsellers = "";
                char chr34 = Convert.ToChar(34);
                int counter = 0;
                int placing = 0;
                int placingoffset = 0;
                decimal TotalSold = Convert.ToDecimal(dsallsellers.Tables[0].Compute("Sum(Amount_Sold)", ""));
                string currentsellerplace = "";
                string currentsellerperc = "";
                int previousselleramount = 0;
                
                foreach (DataRow r in dsallsellers.Tables[0].Rows)
                {
                    if (r["FBid"] != DBNull.Value)
                    {
                        string sellerfbid = r["FBid"].ToString();                        
                        decimal selleramountsold = 0;
                        int selleramountsoldpercent = 0;
                        if (r["Amount_Sold"] != DBNull.Value)
                        {
                            selleramountsold = Convert.ToDecimal(r["Amount_Sold"]);
                        }
                        if (selleramountsold > 0)
                        {
                            selleramountsoldpercent = Convert.ToInt32((selleramountsold/TotalSold) * 100);
                        }
                        if (previousselleramount == selleramountsoldpercent)
                        {
                            placing -= 1;
                            placingoffset += 1;
                        }
                        else if(placingoffset > 0)
                        {
                            placing += placingoffset;
                            placingoffset = 0;
                        }
                        previousselleramount = selleramountsoldpercent;

                        placing += 1;
                        if ((sellerfbid != strfbid) && (sellerfbid != appid))
                        {
                            counter += 1;                            
                            if (counter == 4)
                            {
                                strgroupsellers += "<span id='GridView1_ctl0" + e.Row.DataItemIndex.ToString() + "_additionalsellers' style='display:none;text-align:center;'>";
                            }
                            string currentplacing;
                            string currentsoldperc;
                            if (TotalSold > 0)
                            {
                                currentplacing = Placing(placing);
                                currentsoldperc = selleramountsoldpercent.ToString();
                            }
                            else
                            {
                                currentplacing = "1st";
                                currentsoldperc = Convert.ToInt32(100 / dsallsellers.Tables[0].Rows.Count).ToString();
                            }
                            //strgroupsellers += "<table><tr><td><fb:profile-pic uid=" + chr34 + sellerfbid + chr34 + " linked=" + chr34 + "true" + chr34 + " height=" + chr34 + "40" + chr34 + "width=" + chr34 + "43" + chr34 + "/></td></tr><tr><td><fb:name uid='" + sellerfbid + "' firstnameonly='true' /></td></tr></table>&nbsp;";                        
                            strgroupsellers += "<table width=100%><tr valign='top'><td width='45px'>";
                            strgroupsellers += "<fb:profile-pic uid=" + chr34 + sellerfbid + chr34 + " linked=" + chr34 + "true" + chr34 + " height=" + chr34 + "33" + chr34 + "width=" + chr34 + "35" + chr34 + "/>";
                            strgroupsellers += "</td><td style='text-align:left;width:35px;'>";
                            strgroupsellers += "<b>" + currentplacing + "</b></td><td style='text-align:left;'>";
                            strgroupsellers += "<fb:name uid=" + chr34 + sellerfbid + chr34 + " firstnameonly=" + chr34 + "true" + chr34 + "></fb:name>";
                            strgroupsellers += "<br/>" + currentsoldperc +"%";
                            strgroupsellers += "</td></tr></table>";
                        }                      
                        else if (sellerfbid == strfbid)
                        {
                            if (TotalSold > 0)
                            {
                                currentsellerplace = Placing(placing);
                                currentsellerperc = selleramountsoldpercent.ToString();
                            }
                            else
                            {
                                currentsellerplace = "1st";
                                currentsellerperc = Convert.ToInt32(100 / dsallsellers.Tables[0].Rows.Count).ToString();
                            }                            
                        }
                    }                    
                }
                if (counter > 3)
                {
                    strgroupsellers += "</span>";
                }
                System.Web.UI.WebControls.Panel pnlGroupsellers = (System.Web.UI.WebControls.Panel)e.Row.FindControl("pnlGroupsellers");
                System.Web.UI.WebControls.Panel pnlencourageseller = (System.Web.UI.WebControls.Panel)e.Row.FindControl("pnlencourageseller");
                if (counter == 0)//No Sales or No Sellers
                {
                    pnlGroupsellers.Visible = false;
                    //pnlencourageseller.Visible = true;
                }

                System.Web.UI.WebControls.Label lblGroupSellers = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblGroupSellers");
                lblGroupSellers.Text = strgroupsellers;
                
                System.Web.UI.WebControls.Label lblSellerStats = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblSellerStats");
                lblSellerStats.Text = lblFirstName.Text = "<fb:name uid='" + strfbid + "' capitalize='true' firstnameonly='true' possessive='false' />";
                lblSellerStats.Text += ": <b>" + currentsellerplace + "</b><br/>Sold " + currentsellerperc + "% of tickets";


                System.Web.UI.WebControls.Label lblSellersSeeAll = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblSellersSeeAll");
                if (counter > 3)
                {
                    lblSellersSeeAll.Text = "<a href=# onclick='seeallbutton(" + e.Row.DataItemIndex.ToString() + "); return false;'>see all</a>";
                }
                else
                {
                    lblSellersSeeAll.Visible = false;
                }
                */
                
                dstemp.Tables[0].Rows.Add(drtemp);
                Event_Key_Global = Event_Key;
                strfbid_Global = strfbid;
                //Ticket_Max_Global = Convert.ToInt32(dstemp.Tables[0].Rows[0]["Ticket_Max"].ToString());                 
                gridview2.DataSource = dstemp.Tables[0];
                gridview2.DataBind();

            }
        }

        protected string Placing(int placing)
        {
            string thereturn = "";
            if (placing == 1)
            {
                thereturn = "1st";
            }
            else if (placing == 2)
            {
                thereturn = "2nd";
            }
            else if (placing == 3)
            {
                thereturn = "3rd";
            }
            else
            {
                thereturn = placing.ToString() + "th";
            }
            return thereturn;
        }
        

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            int Event_Key = Event_Key_Global;
            string strfbid = strfbid_Global;
                        
             if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                //DataSet dstemp = Eventomatic_DB.SPs.ViewStoreTicketsSoldoutSellers(Event_Key,Convert.ToInt64(strfbid)).GetDataSet();
                DataSet dstemp = Eventomatic_DB.SPs.ViewTicketsSoldout(Event_Key).GetDataSet();
                string tempstring = sender.ToString();
                tempstring = e.ToString();

                //dropDownList.AutoPostBack = true;     // added here for clarity
                //dropDownList.SelectedIndexChanged += new EventHandler(ddlQuantity_SelectedIndexChanged);   // declare event
                //DataSet dstemp2 = Eventomatic_DB.SPs.ViewStoreTicketSellers(Event_Key,Convert.ToInt64(strfbid)).GetDataSet();
                DataSet dstemp2 = Eventomatic_DB.SPs.ViewTicket(Event_Key).GetDataSet();
                if (e.Row.RowIndex == dstemp2.Tables[0].Rows.Count)
                {
                    System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity");   // look for dropdown in the row            
                    if (dropDownList != null)
                    { dropDownList.Visible = false; }

                    System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblServiceFee");
                    if (label != null)
                    {
                        label.Visible = true;
                    }


                }
                //check if it is soldout or not
                if (dstemp.Tables[0].Rows.Count - 1 >= e.Row.RowIndex)
                {
                    System.Web.UI.WebControls.Label lblTicketMax = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTicketMax");   // look for ticket max in row
                    int Ticket_Max = Ticket_Max_Global;
                    int tickets_left = 0;
                    System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity");   // look for dropdown in the row
                    System.Web.UI.WebControls.Label lblPrice = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblPrice");   // look for price in the row                
                    if (dstemp.Tables[0].Rows[e.Row.RowIndex]["Tickets_Left"] != DBNull.Value)
                    {
                        tickets_left = Convert.ToInt32(dstemp.Tables[0].Rows[e.Row.RowIndex]["Tickets_Left"]);
                    }
                    //Set Ticket Max
                    for (int i = Ticket_Max; i < 10; i++)
                    {
                        dropDownList.Items.RemoveAt(dropDownList.Items.Count - 1);
                    }
                    dropDownList.Attributes.Add("onchange", "ManipulateGrid(" + Gridview1_index_Global.ToString() + "); return false;");
                    if ((dstemp.Tables[0].Rows[e.Row.RowIndex]["Sold_Out"].ToString() == "Sold Out") && ((System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlQuantity") != null))
                    {
                        dropDownList.Visible = false;
                        System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblServiceFee");
                        label.Visible = true;
                        label.Text = "Sold Out";
                    }
                    else if (tickets_left < Ticket_Max)
                    {
                        for (int i = tickets_left; i < Ticket_Max; i++)
                        {
                            dropDownList.Items.RemoveAt(dropDownList.Items.Count - 1);
                        }
                    }                    

                    //check if it is a donation box
                    if (dstemp2.Tables[0].Rows.Count - 1 >= e.Row.RowIndex)
                    {
                        if (dstemp2.Tables[0].Rows[e.Row.RowIndex]["isdonation"] != DBNull.Value)
                        {
                            string strtemp = dstemp2.Tables[0].Rows[e.Row.RowIndex]["isdonation"].ToString();
                            if (dstemp2.Tables[0].Rows[e.Row.RowIndex]["isdonation"].ToString().ToLower() == "true")
                            {
                                dropDownList.Visible = false;
                                lblPrice.Visible = false;
                                System.Web.UI.WebControls.Label lblDollarsign = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDollarSign");   // look for price in the row                
                                System.Web.UI.WebControls.TextBox txtDonate = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtDonate");
                                System.Web.UI.WebControls.Label lblDonate = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDonate");   // look for price in the row                
                                txtDonate.Visible = true;
                                lblDonate.Visible = true;
                                lblDollarsign.Visible = true;
                                txtDonate.Attributes.Add("onKeyUp", "ManipulateGrid(" + Gridview1_index_Global.ToString() + "); return false;");
                            }
                        }
                    }                    
                }


            }
        }
    }
}
