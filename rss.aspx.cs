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
    public partial class rss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int Resource_Key = 0;
            if (!IsPostBack)
            {
                Resource_Key = Convert.ToInt32(Request.QueryString["Storeid"].ToString());
                DataSet dstemp = Eventomatic_DB.SPs.ViewStore(Resource_Key).GetDataSet();

                GridView1.DataSource = dstemp.Tables[0];
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Site sitetemp = new Site();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Label lblEventKey = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblEvent_Key");
                System.Web.UI.WebControls.Image imgEvent = (System.Web.UI.WebControls.Image)e.Row.FindControl("ImgEvent");
                imgEvent.ImageUrl = sitetemp.GetEventPic(lblEventKey.Text);

                //Put in hyperlink
                int Event_Key = Convert.ToInt32(lblEventKey.Text);
                System.Web.UI.WebControls.HyperLink hypEventName = (System.Web.UI.WebControls.HyperLink)e.Row.FindControl("hypEvent_Name");
                System.Web.UI.WebControls.HyperLink hypEventText = (System.Web.UI.WebControls.HyperLink)e.Row.FindControl("hypEvent_Description");
                if (sitetemp.IsSoldOutEvent(Event_Key))
                {
                    hypEventName.Text += " - Sold Out";
                }
                hypEventName.NavigateUrl += lblEventKey.Text;
                hypEventText.NavigateUrl = hypEventName.NavigateUrl;

                //Put in Calendar info
                System.Web.UI.WebControls.Label lblMonth = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblMonth");
                System.Web.UI.WebControls.Label lblDay = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDay");
                System.Web.UI.WebControls.Label lblDayofWeek = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDayofWeek");
                System.Web.UI.WebControls.Label lblBegins = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblEvent_Begins");

                DateTime dtBegins = Convert.ToDateTime(lblBegins.Text);

                lblMonth.Text = dtBegins.ToString("MMM");
                lblDay.Text = dtBegins.ToString("dd");
                lblDayofWeek.Text = dtBegins.ToString("ddd");

            }
        }
    }
}
