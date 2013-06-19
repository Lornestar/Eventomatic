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


namespace Eventomatic
{
    public partial class Reporting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //Webdatechooser date format
                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN");
                ci.DateTimeFormat.LongDatePattern = "dddd, MMMM d, yyyy";
                ci.DateTimeFormat.ShortDatePattern = "dddd, MMMM d, yyyy";
                StartDate.CalendarLayout.Culture = ci;
                EndDate.CalendarLayout.Culture = ci;
                StartDate.Value = DateTime.Now.AddDays(-7).Date;
                EndDate.Value = DateTime.Now.Date;

                if (Master.IsAdmin())
                {
                    pnlHost.Visible = false;
                    //UltraWebGrid1.Visible = false;
                    LoadAdmin();
                }
                else
                {
                    UltraWebGrid2.Visible = false;
                    LoadHost();
                }
            }
            else
            {
                UpdatePage();
            }
            //LoadChartHost();
        }

        public DataSet BindDataGridView( Hashtable hash)
        {
            DataSet set = new DataSet();
            if (hash.Count > 0)
            {

                DataTable table = set.Tables.Add("TestValues");

                //now build our table
                table.Columns.Add("key", typeof(string));
                table.Columns.Add("date", typeof(DateTime));
                                
                foreach (DictionaryEntry de in hash)
                {
                    if (!table.Columns.Contains(de.Key.ToString().Substring(22)))
                    {
                        table.Columns.Add(de.Key.ToString().Substring(22), typeof(int));
                    }
                }

                //table.Columns.Add("value", typeof(int));
                //table.Columns.Add("Ticket_Type", typeof(string));

                //use iDictionaryEnumerator to get an enumerator for the Hashtable
                IDictionaryEnumerator enumerator = hash.GetEnumerator();

                //DataRow for holding the data from the Hashtable
                DataRow row = null;

                //loop using the IDictionaryEnumerator we created
                while (enumerator.MoveNext())
                {
                    //create new DataRow in our DataTable
                    row = table.NewRow();

                    //get the values from the Hashtable
                    string index = (string)enumerator.Key;
                    int value = (int)enumerator.Value;
                    
                    //add values to our new DataRow                    
                    DateTime dtindex = Convert.ToDateTime(index.Substring(0, 10));
                    row["key"] = dtindex.ToString("MMM dd");
                    row[index.Substring(22)] = value;
                    row["date"] = dtindex;

                    //add the new DataRow to our DataTable
                    table.Rows.Add(row);
                }

            }
            return set;
        }

        protected void LoadChartHost()
        {
            DataSet dstemp;
            if (UltraWebGrid1.Visible)
            {
                dstemp = Eventomatic_DB.SPs.ViewReportingHostChart(Convert.ToInt32(Master.GetResourceKey()), Convert.ToDateTime(StartDate.Value), Convert.ToDateTime(EndDate.Value)).GetDataSet();
            }
            else //admin
            {
                dstemp = Eventomatic_DB.SPs.ViewReportingAdminChart( Convert.ToDateTime(StartDate.Value), Convert.ToDateTime(EndDate.Value)).GetDataSet();
            }
            Hashtable Tickets_Purchased = new Hashtable();
            string strEventName = "No Name";
            foreach (DataRow row in dstemp.Tables[0].Rows)
            {

                if ((row["Event_Name"] != DBNull.Value) && (row["Event_Name"] != ""))
                {
                    strEventName = row["Tix_Sold_Date"].ToString() + row["Event_Name"].ToString();
                }
                if (!Tickets_Purchased.Contains(strEventName))
                {
                    Tickets_Purchased.Add(strEventName, 1);
                }
                else
                {
                    Tickets_Purchased[strEventName] = Convert.ToInt32(Tickets_Purchased[strEventName]) + 1;
                }
            }

            DataSet dstemp2 = BindDataGridView(Tickets_Purchased);
            if (dstemp2.Tables.Count > 0)
            {
                dstemp2.Tables[0].DefaultView.Sort = "date";
                UltraChart1.DataSource = dstemp2.Tables[0].DefaultView;
                UltraChart1.DataBind();
            }

            //UltraChart1.ChartImagesPath = "/Images/ChartImages/";
            //UltraChart1.DeploymentScenario.ImageURL = "/Images/ChartImages";

            

        }

        protected void LoadHost()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewReportingHostStats(Convert.ToInt32(Master.GetResourceKey())).GetDataSet();            
            lblTotalEvents.Text = dstemp.Tables[0].Rows.Count.ToString();
            lblCurrentEvents.Text = "0";
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                lblCurrentEvents.Text = dstemp.Tables[0].Rows[0]["Current_Amount"].ToString();
            }
            

            dstemp = Eventomatic_DB.SPs.ViewReportingHostEvents(Convert.ToInt32(Master.GetResourceKey()), Convert.ToDateTime(StartDate.Value), Convert.ToDateTime(EndDate.Value)).GetDataSet();
            if (dstemp.Tables[0].Rows.Count != 0)
            {
                dstemp.Tables[0].TableName = "Current_Events";

                dstemp.Tables.Add(Eventomatic_DB.SPs.ViewReportingHostTicketByUser(Convert.ToInt32(Master.GetResourceKey()), Convert.ToDateTime(StartDate.Value), Convert.ToDateTime(EndDate.Value)).GetDataSet().Tables[0].Copy());
                dstemp.Tables[1].TableName = "Current_Events_Tickets";
                dstemp.Relations.Add("CurrentRelationship", dstemp.Tables["Current_Events"].Columns["Event_Key"], dstemp.Tables["Current_Events_Tickets"].Columns["Event_Key"]);
            }

            dstemp.Tables[0].Columns.Add("Tickets_Progress", System.Type.GetType("System.String"));
            foreach (DataRow row in dstemp.Tables[0].Rows)
            {
                //Tickets_Progress
                int Sold = 0;
                if (row["Tickets_Sold"] != DBNull.Value)
                {
                    Sold = Convert.ToInt32(row["Tickets_Sold"]);
                }
                int Capacity = 0;
                if (row["Tickets_Capacity"] != DBNull.Value)
                {
                    Capacity = Convert.ToInt32(row["Tickets_Capacity"]);
                }
                row["Tickets_Progress"] = Master.TicketsSoldProgressBarHTML(Sold, Capacity);
            }

            UltraWebGrid1.DataSource = dstemp.Tables[0];
            UltraWebGrid1.DataBind();

            //Tickets Sold columns
            int inttixsolddate = 0;
            int inttixcapacitydate = 0;
            foreach (UltraGridRow row in UltraWebGrid1.Rows)
            {
                string tixsolddate = "0";
                string tixcapacitydate = "0";
                if (dstemp.Tables[0].Rows[row.Index]["Tickets_Sold"] != DBNull.Value)
                {
                    tixsolddate = dstemp.Tables[0].Rows[row.Index]["Tickets_Sold"].ToString();
                }
                if (dstemp.Tables[0].Rows[row.Index]["Tickets_Capacity"] != DBNull.Value)
                {
                    tixcapacitydate = dstemp.Tables[0].Rows[row.Index]["Tickets_Capacity"].ToString();
                }
                
                inttixsolddate += Convert.ToInt32(tixsolddate);
                inttixcapacitydate += Convert.ToInt32(tixcapacitydate);                
                
            }
            UltraWebGrid1.Columns[2].Footer.Caption = Master.TicketsSoldProgressBarHTML(inttixsolddate, inttixcapacitydate); 


        }

        protected void LoadAdmin()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewReportingAdminOverall(Convert.ToDateTime(StartDate.Value), Convert.ToDateTime(EndDate.Value)).GetDataSet();

            if (dstemp.Tables[0].Rows.Count != 0)
            {
                dstemp.Tables[0].TableName = "Current_Events";

                dstemp.Tables.Add(Eventomatic_DB.SPs.ViewReportingAdminEvents(Convert.ToDateTime(StartDate.Value), Convert.ToDateTime(EndDate.Value)).GetDataSet().Tables[0].Copy());
                dstemp.Tables[1].TableName = "Events";
                dstemp.Relations.Add("CurrentRelationship", dstemp.Tables[0].Columns["Resource_Key"], dstemp.Tables["Events"].Columns["Resource_Key"]);
            }

            dstemp.Tables[1].Columns.Add("Tickets_Progress", System.Type.GetType("System.String"));
            foreach (DataRow row in dstemp.Tables[1].Rows)
            {
                //Tickets_Progress
                int Sold = 0;
                if (row["Tickets_Sold"] != DBNull.Value)
                {
                    Sold = Convert.ToInt32(row["Tickets_Sold"]);
                }
                int Capacity = 0;
                if (row["Tickets_Capacity"] != DBNull.Value)
                {
                    Capacity = Convert.ToInt32(row["Tickets_Capacity"]);
                }
                row["Tickets_Progress"] = Master.TicketsSoldProgressBarHTML(Sold, Capacity);
            }
                
            UltraWebGrid2.DataSource = dstemp.Tables[0];
            UltraWebGrid2.DataBind();


            //Tickets Sold columns
            int inttixsolddate = 0;
            int inttixcapacitydate = 0;
            int inttixsoldtotal = 0;
            int inttixcapacitytotal = 0;
            foreach (UltraGridRow row in UltraWebGrid2.Rows)
            {
                string tixsolddate = "0";
                string tixcapacitydate = "0";
                string tixsoldtotal = "0";
                string tixcapacitytotal = "0";
                if (dstemp.Tables[0].Rows[row.Index]["TicketsSold_Date"] != DBNull.Value)
                {
                    tixsolddate = dstemp.Tables[0].Rows[row.Index]["TicketsSold_Date"].ToString();
                }
                if (dstemp.Tables[0].Rows[row.Index]["TicketsCapacity_Date"] != DBNull.Value)
                {
                    tixcapacitydate = dstemp.Tables[0].Rows[row.Index]["TicketsCapacity_Date"].ToString();
                }
              

                if (dstemp.Tables[0].Rows[row.Index]["TicketsSold_Total"] != DBNull.Value)
                {
                    tixsoldtotal = dstemp.Tables[0].Rows[row.Index]["TicketsSold_Total"].ToString();
                }
                if (dstemp.Tables[0].Rows[row.Index]["TicketsCapacity_Total"] != DBNull.Value)
                {
                    tixcapacitytotal = dstemp.Tables[0].Rows[row.Index]["TicketsCapacity_Total"].ToString();
                }

                row.Cells[5].Text = tixsolddate + "/" + tixcapacitydate;
                row.Cells[6].Text = tixsoldtotal + "/" + tixcapacitytotal;
                inttixsolddate += Convert.ToInt32(tixsolddate);
                inttixcapacitydate += Convert.ToInt32(tixcapacitydate);
                inttixsoldtotal += Convert.ToInt32(tixsoldtotal);
                inttixcapacitytotal += Convert.ToInt32(tixcapacitytotal);

                // Rev/Event
                row.Cells[9].Text = "0";
                if (row.Cells[3].Text != "0")
                {
                    row.Cells[9].Text = Convert.ToString(Convert.ToDecimal(row.Cells[7].Text) / Convert.ToDecimal(row.Cells[3].Text));
                }
                
                // Rev/Tix
                row.Cells[10].Text = "0";
                if (tixsolddate != "0")
                {
                    row.Cells[10].Text = Convert.ToString(Convert.ToDecimal(row.Cells[7].Text) / Convert.ToDecimal(tixsolddate));
                }                
            }
            UltraWebGrid2.Columns[5].Footer.Caption= inttixsolddate.ToString() + "/" + inttixcapacitydate.ToString();
            UltraWebGrid2.Columns[6].Footer.Caption = inttixsoldtotal.ToString() + "/" + inttixcapacitytotal.ToString();

           
        }

        protected void UpdatePage()
        {
            if (UltraWebTab1.TabIndex == 0)
            {
                if (UltraWebGrid1.Visible)
                {                    
                    if (UltraWebTab1.SelectedTab == 0)
                    {
                        LoadHost();
                    }
                    else if (UltraWebTab1.SelectedTab == 1)
                    {
                        LoadChartHost();
                    }
                }
                else
                {
                    if (UltraWebTab1.SelectedTab == 0)
                    {
                        LoadAdmin();
                    }
                    else if (UltraWebTab1.SelectedTab == 1)
                    {
                        LoadChartHost();
                    }
                }
            }
        }

        protected void btndate_Click(object sender, EventArgs e)
        {
            //dddddddddddd
            
        }

        protected void UltraWebGrid2_InitializeLayout(object sender, LayoutEventArgs e)
        {
            // Rev/Event footer
            /*e.Layout.Bands[0].Columns[9].Footer.Caption = "0";
            if (e.Layout.Bands[0].Columns[3].Footer.Caption != "0")
            {
                e.Layout.Bands[0].Columns[9].Footer.Caption = Convert.ToString(Convert.ToDecimal(e.Layout.Bands[0].Columns[7].Footer.Caption) / Convert.ToDecimal(e.Layout.Bands[0].Columns[3].Footer.Caption));
            }*/
        }

        
    }
}
