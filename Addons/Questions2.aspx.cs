using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using System.Data;

namespace Eventomatic.Addons
{
    public partial class Questions2 : System.Web.UI.Page
    {
        Int32 questionkey = 0;
        Int32 eventkey = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["tixid"] != null)
            {
                questionkey = Convert.ToInt32(Request.QueryString["tixid"]);
            }
            if (Request.QueryString["eventkey"] != null)
            {
                eventkey = Convert.ToInt32(Request.QueryString["eventkey"]);
            }
            txtAnswersMultipleChoice.EmptyMessage = "Chicken" + Convert.ToChar(13) + "Beef" + Convert.ToChar(13) + "Fish";
            if (!IsPostBack)
            {
                if (questionkey != 0)
                {
                    LoadInfo();
                }
            }            
        }

        protected void LoadInfo()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewQuestionSpecific(questionkey).GetDataSet();
            txtQuestionAsk.Text = dstemp.Tables[0].Rows[0]["The_Question"].ToString();
            dstemp.Tables[0].Rows[0]["The_Question"].ToString();
            if (dstemp.Tables[0].Rows[0]["Question_Type"] != DBNull.Value)
            {
                if (dstemp.Tables[0].Rows[0]["Question_Type"].ToString() == "1")
                {
                    ddlAnswertype.SelectedIndex = 1;
                    RadAjaxPanel1.ResponseScripts.Add("showDropDownAnswers();");
                    DataSet dstemp2 = Eventomatic_DB.SPs.ViewQuestionDropDown(questionkey).GetDataSet();
                    foreach (DataRow r in dstemp2.Tables[0].Rows)
                    {
                        txtAnswersMultipleChoice.Text += r["Question_DD_Value"] + "\r";
                    }                    
                }
            }

            //check mandatory
            if (dstemp.Tables[0].Rows[0]["Mandatory"] != DBNull.Value)
            {
                if (dstemp.Tables[0].Rows[0]["Mandatory"].ToString() == "True")
                {
                    radio2.Checked = true;
                }
            }
        }

        protected void btnSaveTicket_Click(object sender, EventArgs e)
        {            
            if (txtQuestionAsk.Text == "")
            {
                txtQuestionAsk.Text = txtQuestionAsk.EmptyMessage;
            }
            if (ddlAnswertype.SelectedIndex == 0)//it's textbox
            {
                Eventomatic_DB.SPs.UpdateQuestion(questionkey,eventkey, txtQuestionAsk.Text,radio2.Checked, 0, 0).Execute();
            }
            else if (ddlAnswertype.SelectedIndex == 1)//it's dropdownbox
            {
                Eventomatic_DB.SPs.DeleteQuestionsDropDowns(questionkey).Execute();
                //update Transaction & get tx_Key
                StoredProcedure sp_UpdateTransaction = Eventomatic_DB.SPs.UpdateQuestion(questionkey, eventkey, txtQuestionAsk.Text, radio2.Checked, 1, 0);
                sp_UpdateTransaction.Execute();
                if (questionkey <= 0)
                {
                    questionkey = Convert.ToInt32(sp_UpdateTransaction.Command.Parameters[5].ParameterValue.ToString());
                }


                //Parse text into dropdownlist items
                char[] delimiterChars = { '\n' };

                string strtxtAnswersMultipleChoice = txtAnswersMultipleChoice.Text.Replace("\r", "");
                string[] strvalues = strtxtAnswersMultipleChoice.Split(delimiterChars);
                
                foreach (string s in strvalues)
                {
                    if (s != "")
                    {
                        Eventomatic_DB.SPs.UpdateQuestionsDropDowns(questionkey, s).Execute();                        
                    }
                }               

            }
                        
            RadAjaxPanel1.ResponseScripts.Add(string.Format("CloseWindow();return false;", ""));
        }
    }
}
