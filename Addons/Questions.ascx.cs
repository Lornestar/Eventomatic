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
using SubSonic;


namespace Eventomatic.Addons
{
    public partial class Questions : System.Web.UI.UserControl
    {
        string strFirstDD = "Please Select ...";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Event_Key.Value != "-1") 
            {
                if (!IsPostBack)
                {

                    //LoadPage();
                    
                }
                else
                {
                    if (Request.Form["__EVENTTARGET"] == "DoRemove")
                    {
                        DoRemove(Request["__EVENTARGUMENT"].ToString());
                    }
                }
            }

            

        }

        public void LoadPage(bool Writable) //Read = False , Writeable = true
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewQuestion(Convert.ToInt32(Event_Key.Value)).GetDataSet();
            UltraWebGridQ1.DataSource = dstemp.Tables[0];
            UltraWebGridQ1.DataBind();
            //GridView2.DataSource = Eventomatic_DB.SPs.ViewQuestion(Convert.ToInt32(Event_Key.Value)).GetDataSet().Tables[0];
            //GridView2.DataBind();
            string strType = "0";
            string strQuestionid;
            string strAnswerid;
            DataSet dstemp2;
            if (Writable == false)
            {
                UltraWebGridQ1.Columns[2].Hidden = true;
                lblAddNew.Visible = false;
                lblPleaseNote.Visible = false;
            }
            foreach (UltraGridRow row in UltraWebGridQ1.Rows)
            {
                //strAid =  "txtAnswer" + row.Index.ToString();
                //strAnswer = "<input type='text' style='width: 480px;' id='txtAnswer" + strAid + "' name='" + strAid + "'/>";
                strType = "0";
                strQuestionid = "ctl00_body_UltraWebTab1__ctl2_Questions1_UltraWebGridQ1_ci_0_1_" + row.Index.ToString() + "_Question_Text";
                strAnswerid = "ctl00_body_UltraWebTab1__ctl2_Questions1_UltraWebGridQ1_ci_0_1_" + row.Index.ToString() + "_txtAnswer";
                TemplatedColumn col1 = (TemplatedColumn)row.Cells.FromKey("Col1").Column;
                CellItem cellItemCol1 = (CellItem)col1.CellItems[row.Index];

                if (dstemp.Tables[0].Rows[row.Index]["Question_Type"] != DBNull.Value)
                {
                    
                    TextBox txtAnswer = (TextBox)cellItemCol1.FindControl("txtAnswer");
                    DropDownList ddlAnswer = (DropDownList)cellItemCol1.FindControl("ddlAnswer");

                    if (dstemp.Tables[0].Rows[row.Index]["Question_Type"].ToString() == "1")
                    {
                        //strAid = "ddlAnswer" + row.Index.ToString();
                        //strAnswer = "<select id='" + strAid + "' name='" + strAid + "'>";
                        strAnswerid = "ctl00_body_UltraWebTab1__ctl2_Questions1_UltraWebGridQ1_ci_0_1_" + row.Index.ToString() + "_ddlAnswer";
                        strType = "1";
                        txtAnswer.Visible = false;
                        ddlAnswer.Visible = true;
                        dstemp2 = Eventomatic_DB.SPs.ViewQuestionDropDown(Convert.ToInt32(row.Cells[0].Text)).GetDataSet();
                        
                        ddlAnswer.DataSource = dstemp2.Tables[0];
                        ddlAnswer.DataTextField = "Question_DD_Text";
                        ddlAnswer.DataValueField = "Question_DD_Value";
                        ddlAnswer.DataBind();
                        ddlAnswer.Items.Insert(0, strFirstDD);
                        

                    }
                }

                //Check Mandatory
                string Mandatory = "0";
                if (dstemp.Tables[0].Rows[row.Index]["Mandatory"] != DBNull.Value)
                {                   
                    if (dstemp.Tables[0].Rows[row.Index]["Mandatory"].ToString() == "True")
                    {
                        Label lblMandatory = (Label)cellItemCol1.FindControl("lblMandatory");
                        lblMandatory.Visible = true;
                        Mandatory = "1";
                    }
                }
                row.Cells[2].Text = "<a href='#' onclick=javascript:popup(" + row.Cells[0].Text + "," + strType + ",'" + strQuestionid + "','" + strAnswerid + "'," + Mandatory + ");>Edit</a> | <a href='javascript:doRemove(" + row.Index + ");'>Remove</a>";

            }
        }

        public bool MandatoryAnswered()
        {
            //return true = form is valid
            //return false = form is invalid

            bool QuestionsMandatory = true;

            foreach (UltraGridRow row in UltraWebGridQ1.Rows)
            {
                TemplatedColumn col1 = (TemplatedColumn)row.Cells.FromKey("Col1").Column;
                CellItem cellItemCol1 = (CellItem)col1.CellItems[row.Index];
                Label lblMandatory = (Label)cellItemCol1.FindControl("lblMandatory");
                TextBox txtAnswer = (TextBox)cellItemCol1.FindControl("txtAnswer");
                DropDownList ddlAnswer = (DropDownList)cellItemCol1.FindControl("ddlAnswer");
                if ((lblMandatory.Visible) && (txtAnswer.Visible) && (txtAnswer.Text == ""))
                {
                    QuestionsMandatory = false;
                }
                if ((lblMandatory.Visible) && (ddlAnswer.Visible) && (ddlAnswer.SelectedItem.Text == strFirstDD))
                {
                    QuestionsMandatory = false;
                }
            }

            return QuestionsMandatory;
        }

        public void SaveQuestionsAnswered(string txKey)
        {
            int intQuestion_Key;
            string TheAnswer = "";
            foreach (UltraGridRow row in UltraWebGridQ1.Rows)
            {
                TemplatedColumn col1 = (TemplatedColumn)row.Cells.FromKey("Col1").Column;
                CellItem cellItemCol1 = (CellItem)col1.CellItems[row.Index];
                TextBox txtAnswer = (TextBox)cellItemCol1.FindControl("txtAnswer");
                DropDownList ddlAnswer = (DropDownList)cellItemCol1.FindControl("ddlAnswer");
                if (txtAnswer.Visible == true)//it's textbox
                {
                    TheAnswer = txtAnswer.Text;
                }
                else if (ddlAnswer.Visible == true)//it's dropdownbox
                {
                    TheAnswer = ddlAnswer.SelectedValue;
                }
                intQuestion_Key = Convert.ToInt32(row.Cells[0].Text);
                //Eventomatic_DB.SPs.UpdateQuestionsAnswered(Convert.ToInt32(txKey), intQuestion_Key, Convert.ToInt32(Event_Key.Value), TheAnswer).Execute();
            }
        }

        public void SaveQuestions()
        {
            Site sitetemp = new Site();
            sitetemp.UpdateUltraWebGrid(UltraWebGridQ1, 1, Convert.ToInt32(Event_Key.Value), false);
            //Eventomatic_DB.SPs.DeleteQuestionEventKey(Convert.ToInt32(Event_Key.Value)).Execute();
            int intQuestion_Key;
            bool Mandatory;
            foreach (UltraGridRow row in UltraWebGridQ1.Rows)
            {
                intQuestion_Key = Convert.ToInt32(row.Cells[0].Text);
                if (intQuestion_Key != -1)
                {
                    Eventomatic_DB.SPs.DeleteQuestionsDropDowns(intQuestion_Key).Execute();
                }

                //update question
                TemplatedColumn col1 = (TemplatedColumn)row.Cells.FromKey("Col1").Column;
                CellItem cellItemCol1 = (CellItem)col1.CellItems[row.Index];
                Label txtQuestion = (Label)cellItemCol1.FindControl("Question_Text");
                TextBox txtAnswer = (TextBox)cellItemCol1.FindControl("txtAnswer");
                DropDownList ddlAnswer = (DropDownList)cellItemCol1.FindControl("ddlAnswer");
                Label lblMandatory = (Label)cellItemCol1.FindControl("lblMandatory");
                Mandatory =false;
                if (lblMandatory.Visible)
                {
                    Mandatory = true;
                }

                if (txtAnswer.Visible == true)//it's textbox
                {
                    Eventomatic_DB.SPs.UpdateQuestion(intQuestion_Key, Convert.ToInt32(Event_Key.Value), txtQuestion.Text, Mandatory, 0, 0).Execute();
                }
                else if (ddlAnswer.Visible == true)//it's dropdownbox
                {
                    //update Transaction & get tx_Key
                    StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateQuestion(intQuestion_Key, Convert.ToInt32(Event_Key.Value), txtQuestion.Text, Mandatory, 1, 0);
                    sp_UpdateTransaction.Execute();
                    if (intQuestion_Key <= 0)
                    {
                        intQuestion_Key = Convert.ToInt32(sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString());
                    }
                    

                    for (int i =0;i < ddlAnswer.Items.Count ;i++ )
                    {
                        if (ddlAnswer.Items[i].Text != strFirstDD)
                        {
                            Eventomatic_DB.SPs.UpdateQuestionsDropDowns(intQuestion_Key, ddlAnswer.Items[i].Value).Execute();
                        }                        
                    }
                    
                }               
                
            }            
        }



        protected void ModifyQuestion(UltraGridRow row)
        {            
            string strQuestionid;
            string strAnswerid;
            string Mandatory = "0";
            //Change that specific row
            TemplatedColumn col1 = (TemplatedColumn)row.Cells.FromKey("Col1").Column;
            CellItem cellItemCol1 = (CellItem)col1.CellItems[row.Index];
            Label txtQuestion = (Label)cellItemCol1.FindControl("Question_Text");
            TextBox txtAnswer = (TextBox)cellItemCol1.FindControl("txtAnswer");
            DropDownList ddlAnswer = (DropDownList)cellItemCol1.FindControl("ddlAnswer");
            
            Label lblMandatory = (Label)cellItemCol1.FindControl("lblMandatory");
            if (radio2.Checked)
            {
                Mandatory = "1";
                lblMandatory.Visible = true;
            }
            else
            {
                Mandatory = "0";
                lblMandatory.Visible = false;
            }

            txtQuestion.Text = txtQuestionAsk.Text;
            if (ddlAnswertype.SelectedIndex == 0)
            {
                txtAnswer.Visible = true;
                ddlAnswer.Visible = false;

                strQuestionid = "ctl00_body_UltraWebTab1__ctl2_Questions1_UltraWebGridQ1_ci_0_1_" + row.Index.ToString() + "_Question_Text";
                strAnswerid = "ctl00_body_UltraWebTab1__ctl2_Questions1_UltraWebGridQ1_ci_0_1_" + row.Index.ToString() + "_txtAnswer";
                row.Cells[2].Text = "<a href='#' onclick=javascript:popup(" + row.Cells[0].Text + ",0,'" + strQuestionid + "','" + strAnswerid + "'," + Mandatory + ");>Edit</a> | <a href='javascript:doRemove(" + row.Index + ");'>Remove</a>";
            }
            else if (ddlAnswertype.SelectedIndex == 1)
            {
                txtAnswer.Visible = false;
                ddlAnswer.Visible = true;

                strQuestionid = "ctl00_body_UltraWebTab1__ctl2_Questions1_UltraWebGridQ1_ci_0_1_" + row.Index.ToString() + "_Question_Text";
                strAnswerid = "ctl00_body_UltraWebTab1__ctl2_Questions1_UltraWebGridQ1_ci_0_1_" + row.Index.ToString() + "_ddlAnswer";
                row.Cells[2].Text = "<a href='#' onclick=javascript:popup(" + row.Cells[0].Text + ",1,'" + strQuestionid + "','" + strAnswerid + "'," + Mandatory + ");>Edit</a> | <a href='javascript:doRemove(" + row.Index + ");'>Remove</a>";

                //Parse text into dropdownlist items
                char[] delimiterChars = { '\n' };

                string strtxtAnswersMultipleChoice = txtAnswersMultipleChoice.Text.Replace("\r", "");
                string[] strvalues = strtxtAnswersMultipleChoice.Split(delimiterChars);

                ddlAnswer.Items.Clear();
                ddlAnswer.Items.Add(strFirstDD);
                foreach (string s in strvalues)
                {
                    if (s != "")
                    {
                        ddlAnswer.Items.Add(s);
                    }
                }
                //ddlAnswer.DataSource = 
            }

        }

        protected void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            //Click on save Changes for the editing Question
            int intQuestion_Key = Convert.ToInt32(hdnQuestion_Key.Value);

            if (intQuestion_Key == 0)//A new Question
            {
                UltraWebGridQ1.Rows.Add();
                UltraWebGridQ1.Rows[UltraWebGridQ1.Rows.Count - 1].Cells[0].Text = "-1";
                ModifyQuestion(UltraWebGridQ1.Rows[UltraWebGridQ1.Rows.Count - 1]);
            }
            else
            {
                foreach (UltraGridRow row in UltraWebGridQ1.Rows)
                {
                    if (row.Cells[0].Text == intQuestion_Key.ToString())
                    {
                        ModifyQuestion(row);
                    }
                }
            }            
        }

        protected void DoRemove(string Rowid)
        {            
            /*int rowtoremove = 0;
            foreach (UltraGridRow row in UltraWebGrid1.Rows)
            {
                if (row.Cells[0].Text == Rowid)
                {
                    rowtoremove = row.Index;
                }
            }*/
            UltraWebGridQ1.Rows.RemoveAt(Convert.ToInt32(Rowid));
        }
    }
}