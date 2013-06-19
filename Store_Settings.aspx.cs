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

namespace Eventomatic
{
    public partial class Store_Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
                if (!IsPostBack)
                {
                    PopulateExistingStore();
                } 
        }

        protected void PopulateExistingStore()
        {
            Site sitetemp = new Site();

            
            DataSet dstemp = Eventomatic_DB.SPs.ViewResource(Convert.ToInt32(Master.GetResourceKey())).GetDataSet();

            txtTitle.Text = dstemp.Tables[0].Rows[0]["Store_Title"].ToString();
            txtDescription.Text = dstemp.Tables[0].Rows[0]["Store_Description"].ToString();
            txtContact.Text = dstemp.Tables[0].Rows[0]["Store_Contact"].ToString();

            //Send Event_Key to user control
            HiddenField hdnEvent_Key = new HiddenField();
            hdnEvent_Key = (HiddenField)Upload1.FindControl("Resource_Key");
            hdnEvent_Key.Value = Master.GetResourceKey().ToString();         
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Eventomatic_DB.SPs.UpdateStore(Convert.ToInt32(Convert.ToInt32(Master.GetResourceKey())),txtTitle.Text, txtDescription.Text, txtContact.Text).Execute();

            Page.RegisterStartupScript("Myscript", "<script language=javascript>alert('Your Changes have been Saved');location.href = 'Default.aspx';</script>");
        }
    }
}
