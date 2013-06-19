using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Eventomatic.Addons
{
    public partial class EventNewsFeed : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsevents = Eventomatic_DB.SPs.ViewEventNewsfeed(Convert.ToInt32(hdEvent_Key.Value)).GetDataSet();

            if (dsevents.Tables[0].Rows.Count > 0)
            {
                if (hdonlytop.Value == "0") //show all rows
                {
                    GridView1.DataSource = dsevents.Tables[0];
                    GridView1.DataBind();
                }
                else //show only top row
                {
                    DataTable dttemp = new DataTable();
                    dttemp = dsevents.Tables[0].Clone();
                    dttemp.ImportRow(dsevents.Tables[0].Rows[0]);

                    GridView1.ShowHeader = false;

                    GridView1.DataSource = dttemp;
                    GridView1.DataBind();
                }
            }
            else
            {
                GridView1.Visible = false;
                lblnoevents.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Image imgseller = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgSeller");
                HyperLink hypseller = (HyperLink)e.Row.FindControl("hypSeller");
                System.Web.UI.WebControls.Label lblfbid = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblfbid");
                
                hypseller.NavigateUrl = hypseller.NavigateUrl.Replace("0", lblfbid.Text);
                imgseller.ImageUrl = "http://graph.facebook.com/" + lblfbid.Text + "/picture?type=square";

                if (hypseller.Text.ToLower() == "someone")
                {
                    hypseller.NavigateUrl = "";
                }
            }
        }
    }
}