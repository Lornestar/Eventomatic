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
    public partial class Seller_Attendee_List : System.Web.UI.Page
    {
        int Event_Key = 0;  
        protected void Page_Load(object sender, EventArgs e)
        {            
            if ((Request.QueryString["Event_Key"] != null) && (Request.QueryString["Event_Key"] != ""))
            {
                Event_Key = Convert.ToInt32(Request.QueryString["Event_Key"].ToString());
                if (!IsPostBack)
                {
                    fbuser fbuser = Master.getfbuser();
                    DataSet dstemp = Eventomatic_DB.SPs.ViewAttendeeListSummarySellers(Event_Key,fbuser.UID).GetDataSet();
                    if (dstemp.Tables[0].Rows.Count > 0)
                    {
                        lblEventName.Text = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
                        lbltotalsold.Text = dstemp.Tables[0].Rows[0]["Tickets_Sold"].ToString();
                        lblyouget.Text = dstemp.Tables[0].Rows[0]["Revenue"].ToString();
                    }
                    if (Request.QueryString["view"] == "transaction")
                    {
                        UltraWebTab1.SelectedTab = 1;
                    }
                    PopulateExistingEvent(Event_Key);
                    PopulateExistingEvent_Transaction(Event_Key);
                }
            }            
        }

        protected void PopulateExistingEvent_Transaction(int Event_Key)
        {
            fbuser fbuser = Master.getfbuser();
            DataSet dstemp = Eventomatic_DB.SPs.ViewAttendeeListTransactionsSellers(Event_Key,fbuser.UID).GetDataSet();
            UltraWebGrid3.DataSource = dstemp.Tables[0];
            UltraWebGrid3.DataBind();

            UltraWebGrid4.DataSource = dstemp.Tables[0];
            UltraWebGrid4.DataBind();
        }

        protected void PopulateExistingEvent(int Event_Key)
        {
            fbuser fbuser = Master.getfbuser();
            DataSet dstemp = Eventomatic_DB.SPs.ViewAttendeeListSellers(Event_Key,fbuser.UID).GetDataSet();
            DataSet dstempWebGrid2 = dstemp.Copy();
            if (dstemp.Tables[0].Rows.Count != 0)
            {
                dstemp.Tables[0].TableName = "Attendees";

                dstemp.Tables.Add(Eventomatic_DB.SPs.ViewQuestionsAnsweredSellers(Event_Key,fbuser.UID).GetDataSet().Tables[0].Copy());
                dstemp.Tables[1].TableName = "Answers";
                dstemp.Relations.Add("CurrentRelationship", dstemp.Tables["Attendees"].Columns["Ticket_Transaction_Key"], dstemp.Tables["Answers"].Columns["Ticket_Transaction_Key"]);
            }


            UltraWebGrid1.DataSource = dstemp.Tables[0];
            UltraWebGrid1.DataBind();

            Site sitetemp = new Site();
            string Tx_Key = "0";
            string strGotTicket = "<a href='javascript:doGotTicket(TXKEY);'>Got Ticket</a>";
            string strAlreadyHasTicketCurrentFBuser = "<a href='javascript:doGotTicketRemove(TXKEY);'>FBNAME</a>";
            string strtemp = "";
            //fbuser fbuser = Master.getfbuser(); //Master.API.users.getInfo();
            string strfbid = fbuser.UID.ToString();
            foreach (UltraGridRow row in UltraWebGrid1.Rows)
            {
                Tx_Key = row.Cells[9].Value.ToString();
                row.Cells[8].Text = "<a href='Order_Confirmation.aspx?Tx_key=" + Tx_Key + "' target='_blank'>Details</a>";
                if (row.Cells[11].Text == null)
                {
                    row.Cells[7].Text = strGotTicket.Replace("TXKEY", Tx_Key);
                }
                else if (row.Cells[10].Text == strfbid)
                {
                    strtemp = strAlreadyHasTicketCurrentFBuser.Replace("TXKEY", Tx_Key);
                    row.Cells[7].Text = strtemp.Replace("FBNAME", row.Cells[11].ToString());
                }
                else
                {
                    row.Cells[7].Text = row.Cells[11].Text;
                }

                /*if ((row.Cells[13].Text != "0") && (row.Cells[13].Text != null))
                {
                    row.Cells[0].Text = "<table><tr><td><a href='http://www.facebook.com/profile.php?id=" + row.Cells[13].Text + "' target='_top'><Fb:profile-pic uid='" + row.Cells[13].Text + "' linked='false' height='25'/></a></td></tr><tr><td>" + row.Cells[0].Text + "</td></tr></table>";
                }*/
                if ((row.Cells[12].Text != "0") && (row.Cells[12].Text != null))
                {
                    row.Cells[1].Text = "<a href='http://www.facebook.com/profile.php?id=" + row.Cells[12].Text + "' target='_top'><fb:name uid='" + row.Cells[12].Text + "' linked='false'></fb:name></a>";
                }
            }



            DataSet dstempQuestions = Eventomatic_DB.SPs.ViewQuestion(Event_Key).GetDataSet();

            //Create new Question columns
            string tempQuestionKey = "";
            foreach (DataRow row in dstempQuestions.Tables[0].Rows)
            {
                if (row["Question_Key"] != DBNull.Value)
                { tempQuestionKey = row["Question_Key"].ToString(); }
                //UltraWebGrid2.Columns.Add(tempQuestionKey);                
                //UltraWebGrid2.Columns[UltraWebGrid2.Columns.Count-1].Header.Caption = tempQuestionKey;

                dstempWebGrid2.Tables[0].Columns.Add(new DataColumn(tempQuestionKey, typeof(string)));
            }


            DataSet dstempAnswers = Eventomatic_DB.SPs.ViewQuestionsAnsweredSellers(Event_Key,fbuser.UID).GetDataSet();
            string tempstr1 = "";
            string tempstr2 = "";
            int rowindex = 0;
            //Insert Answers into table
            foreach (DataRow row in dstempAnswers.Tables[0].Rows)
            {
                DataRow[] drtemp = dstempWebGrid2.Tables[0].Select("Tx_Key = " + row["Tx_Key"].ToString());
                if (drtemp[0] != null)
                {
                    rowindex = dstempWebGrid2.Tables[0].Rows.IndexOf(drtemp[0]);
                    dstempWebGrid2.Tables[0].Rows[rowindex][row["Question_Key"].ToString()] = row["The_Answer"].ToString();
                    //drtemp[0][row["Question_Key"].ToString()] = "Test"; //row["The_Answer"];
                }
            }

            int CounterQs = 0;
            string strQ = "";
            string strQ_Shortened = "";
            foreach (DataRow row in dstempQuestions.Tables[0].Rows)
            {
                if (row["Question_Key"] != DBNull.Value)
                {
                    CounterQs++;
                    if (row["The_Question"] != DBNull.Value)
                    {
                        strQ = row["The_Question"].ToString();
                    }
                    tempQuestionKey = row["Question_Key"].ToString();
                    if (strQ.Length > 20)
                    {
                        strQ_Shortened = strQ.Substring(0, 20) + "...";
                    }
                    else
                    {
                        strQ_Shortened = strQ;
                    }
                    dstempWebGrid2.Tables[0].Columns[tempQuestionKey].ColumnName = CounterQs.ToString() + "-" + strQ_Shortened;
                }


            }

            dstempWebGrid2.Tables[0].Columns.Remove("Ticket_Transaction_Key");
            dstempWebGrid2.Tables[0].Columns.Remove("Tx_Key");

            UltraWebGrid2.DataSource = dstempWebGrid2.Tables[0];
            UltraWebGrid2.DataBind();


        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            UltraWebGridExcelExporter1.DownloadName = "Groupstore_" + lblEventName.Text.Replace(" ", "") + ".xls";
            UltraWebGridExcelExporter1.ExportMode = Infragistics.WebUI.UltraWebGrid.ExcelExport.ExportMode.Download;
            Infragistics.Excel.Workbook workbook = new Infragistics.Excel.Workbook();
            Infragistics.Excel.Worksheet worksheet = workbook.Worksheets.Add("Guest List");

            UltraWebGrid2.Columns[0].Header.Caption = "First Name";
            UltraWebGrid2.Columns[1].Header.Caption = "Last Name";
            UltraWebGrid2.Columns[5].Header.Caption = "Amount Paid";
            UltraWebGrid2.Columns[7].Header.Caption = "Date Paid";
            //UltraWebGrid2.Columns[8].Header.Caption = "Physical Tix";
            UltraWebGrid2.Columns.RemoveAt(12);
            UltraWebGrid2.Columns.RemoveAt(11);
            UltraWebGrid2.Columns.RemoveAt(9);
            UltraWebGrid2.Columns.RemoveAt(8);
            UltraWebGrid2.Columns.RemoveAt(6);
            UltraWebGrid2.Columns.RemoveAt(5);
            UltraWebGrid2.Columns.RemoveAt(2);

            this.UltraWebGridExcelExporter1.Export(this.UltraWebGrid2, worksheet);

            Infragistics.Excel.Worksheet worksheet2 = workbook.Worksheets.Add("Transactions");

            UltraWebGrid4.Columns[0].Header.Caption = "First Name";
            UltraWebGrid4.Columns[1].Header.Caption = "Last Name";
            UltraWebGrid4.Columns[4].Header.Caption = "Date Paid";
            UltraWebGrid4.Columns[8].Header.Caption = "Paypal Fee";
            UltraWebGrid4.Columns.RemoveAt(6);
            UltraWebGrid4.Columns.RemoveAt(5);
            UltraWebGrid4.Columns.RemoveAt(3);
            UltraWebGrid4.Columns.RemoveAt(2);

            this.UltraWebGridExcelExporter1.Export(this.UltraWebGrid4, worksheet2);

            workbook.Save(System.Web.HttpContext.Current.Server.MapPath("/Images/Excel/") + "/Groupstore_" + lblEventName.Text.Replace(" ", "") + ".xls");
        }
        

        protected void UltraWebTab1_TabClick(object sender, Infragistics.WebUI.UltraWebTab.WebTabEvent e)
        {

        }
    }
}
