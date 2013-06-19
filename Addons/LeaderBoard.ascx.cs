using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Eventomatic.Addons
{
    public partial class LeaderBoard : System.Web.UI.UserControl
    {
        decimal TotalSold = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsallsellers = Eventomatic_DB.SPs.ViewLeaderBoard(Convert.ToInt32(hdEvent_Key.Value)).GetDataSet();

            if (dsallsellers.Tables[0].Rows.Count > 0)
            {

                DataSet dstemp = Eventomatic_DB.SPs.ViewEventDetails(Convert.ToInt32(hdEvent_Key.Value)).GetDataSet();
                if (dstemp.Tables[0].Rows[0]["Leader_Prize"] != DBNull.Value)
                {
                    lblWinnergets.Text = dstemp.Tables[0].Rows[0]["Leader_Prize"].ToString();
                }

                TotalSold = Convert.ToDecimal(dsallsellers.Tables[0].Compute("Sum(Amount_Sold)", ""));

                if (hdonlytop.Value == "0") //show all rows
                {
                    GridView1.DataSource = dsallsellers.Tables[0];
                    GridView1.DataBind();
                }
                else //show only top row
                {
                    DataTable dttemp = new DataTable();
                    dttemp = dsallsellers.Tables[0].Clone();
                    dttemp.ImportRow(dsallsellers.Tables[0].Rows[0]);

                    GridView1.ShowHeader = false;

                    GridView1.DataSource = dttemp;
                    GridView1.DataBind();

                    if (lblWinnergets.Text.Length > 50)
                    {
                        lblWinnergets.Text = lblWinnergets.Text.Substring(0, 47) + "...";
                    }                    
         
                }
                                
            }
            else
            {
                lblnoleaders.Visible = true;
            }

            
         
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Image imgseller = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgSeller");
                HyperLink hypseller = (HyperLink)e.Row.FindControl("hypSeller");
                System.Web.UI.WebControls.Label lblfbid = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblfbid");
                System.Web.UI.WebControls.Label lbltotalamount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbltotalamount");
                System.Web.UI.WebControls.Label lblTotalPercent = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTotalPercent");
                System.Web.UI.WebControls.Label lblranking = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblranking");

                lblranking.Text = Convert.ToString(e.Row.RowIndex + 1) + ". ";

                hypseller.NavigateUrl = hypseller.NavigateUrl.Replace("0", lblfbid.Text);
                imgseller.ImageUrl = "http://graph.facebook.com/" + lblfbid.Text + "/picture?type=square";
                
                if (lbltotalamount.Text != "")
                {                        
                    decimal selleramountsold = 0;
                    int selleramountsoldpercent = 0;                                                
                    selleramountsold = Convert.ToDecimal(lbltotalamount.Text);                        
                    if (selleramountsold > 0)
                    {
                        selleramountsoldpercent = Convert.ToInt32((selleramountsold / TotalSold) * 100);
                    }
                    lblTotalPercent.Text = selleramountsoldpercent.ToString() + "%";
                }                
            }
        }

    }
}