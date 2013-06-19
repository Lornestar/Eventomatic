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
    public partial class Add_Tab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dstemp = Eventomatic_DB.SPs.ViewStoreSellers(Convert.ToInt64(Add_Tab_fbid.Value)).GetDataSet();

                GridView1.DataSource = dstemp.Tables[0];
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Label lblEvent_Key = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblEvent_Key");
                System.Web.UI.WebControls.Image imgEvent = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgEvent");

                Site sitetemp = new Site();
                imgEvent.ImageUrl = sitetemp.GetEventPic(lblEvent_Key.Text);
            }
        }
    }
}