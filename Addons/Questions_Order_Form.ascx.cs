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

namespace Eventomatic.Addons
{
    public partial class Questions_Order_Form : System.Web.UI.UserControl
    {
        string strFirstDD = "Please Select ...";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadPage()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewQuestion(Convert.ToInt32(Event_Key.Value)).GetDataSet();
            GridView1.DataSource = dstemp.Tables[0];
            GridView1.DataBind();            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewQuestion(Convert.ToInt32(Event_Key.Value)).GetDataSet();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int count = e.Row.RowIndex;
                string strType = "0";
                DataSet dstemp2;
                //strAid =  "txtAnswer" + row.Index.ToString();
                //strAnswer = "<input type='text' style='width: 480px;' id='txtAnswer" + strAid + "' name='" + strAid + "'/>";
                strType = "0";
                /*TemplatedColumn col1 = (TemplatedColumn)row.Cells.FromKey("Col1").Column;
                CellItem cellItemCol1 = (CellItem)col1.CellItems[row.Index];*/

                if (dstemp.Tables[0].Rows[count]["Question_Type"] != DBNull.Value)
                {

                    TextBox txtAnswer = (TextBox)e.Row.FindControl("txtAnswer");
                    DropDownList ddlAnswer = (DropDownList)e.Row.FindControl("ddlAnswer");
                    Label lblQuestionKey = (Label)e.Row.FindControl("lblQuestionKey");

                    if (dstemp.Tables[0].Rows[count]["Question_Type"].ToString() == "1")
                    {
                        //strAid = "ddlAnswer" + row.Index.ToString();
                        //strAnswer = "<select id='" + strAid + "' name='" + strAid + "'>";                        
                        strType = "1";
                        txtAnswer.Visible = false;
                        ddlAnswer.Visible = true;
                        dstemp2 = Eventomatic_DB.SPs.ViewQuestionDropDown(Convert.ToInt32(lblQuestionKey.Text)).GetDataSet();

                        ddlAnswer.DataSource = dstemp2.Tables[0];
                        ddlAnswer.DataTextField = "Question_DD_Text";
                        ddlAnswer.DataValueField = "Question_DD_Value";
                        ddlAnswer.DataBind();
                        ddlAnswer.Items.Insert(0, strFirstDD);


                    }
                }

                //Check Mandatory
                string Mandatory = "0";
                if (dstemp.Tables[0].Rows[count]["Mandatory"] != DBNull.Value)
                {
                    if (dstemp.Tables[0].Rows[count]["Mandatory"].ToString() == "True")
                    {
                        Label lblMandatory = (Label)e.Row.FindControl("lblMandatory");
                        lblMandatory.Visible = true;
                        Mandatory = "1";
                    }
                }
            }
            
            }

        public void SaveQuestionsAnswered(string txKey,int tixpurchasedkey)
        {
            int intQuestion_Key;
            string TheAnswer = "";
            foreach (GridViewRow row in GridView1.Rows)
            {                
                TextBox txtAnswer = (TextBox)row.FindControl("txtAnswer");
                DropDownList ddlAnswer = (DropDownList)row.FindControl("ddlAnswer");
                Label lblQuestionKey = (Label)row.FindControl("lblQuestionKey");
                if (txtAnswer.Visible == true)//it's textbox
                {
                    TheAnswer = txtAnswer.Text;
                }
                else if (ddlAnswer.Visible == true)//it's dropdownbox
                {
                    TheAnswer = ddlAnswer.SelectedValue;
                }
                intQuestion_Key = Convert.ToInt32(lblQuestionKey.Text);
                Eventomatic_DB.SPs.UpdateQuestionsAnswered(Convert.ToInt32(txKey), intQuestion_Key, Convert.ToInt32(Event_Key.Value), TheAnswer,tixpurchasedkey).Execute();
            }
        }

        public bool MandatoryAnswered()
        {
            //return true = form is valid
            //return false = form is invalid

            bool QuestionsMandatory = true;

            foreach (GridViewRow row in GridView1.Rows)
            {                
                Label lblMandatory = (Label)row.FindControl("lblMandatory");
                TextBox txtAnswer = (TextBox)row.FindControl("txtAnswer");
                DropDownList ddlAnswer = (DropDownList)row.FindControl("ddlAnswer");
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

    }
}