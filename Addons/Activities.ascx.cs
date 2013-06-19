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
    public partial class Activities : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Request.Form["__EVENTTARGET"] == "Changeview")
            {
                ChangeView();
            }            
        }

        public void ChangeView()
        {
            if (GridView1.Rows.Count > 2)
            {            
                
                if (GridView1.Rows[2].Visible == false)
                {
                    for (int i = 2; i < GridView1.Rows.Count; i++)
                    {
                        GridView1.Rows[i].Visible = true;
                    }
                }
                else
                {
                    CloseView();
                }
            }
        }

        public void CloseView()
        {
            for (int i = 2; i < GridView1.Rows.Count; i++)
            {
                GridView1.Rows[i].Visible = false;
            }
        }

        public void LoadActivities()
        {            
            DataSet dstemp = Eventomatic_DB.SPs.ViewActivities(Convert.ToInt32(hdResource_Key.Value)).GetDataSet();

            GridView1.DataSource = dstemp.Tables[0];
            GridView1.DataBind();

            lblamount.Text = lblamount.Text.Replace("AMOUNT", dstemp.Tables[0].Rows.Count.ToString());
            hdLogAmount.Value = dstemp.Tables[0].Rows.Count.ToString();

            if (dstemp.Tables[0].Rows.Count <= 2)
            {
                lblamount.Visible = false;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            Site sitetemp = new Site();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Label lblfbid = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblfbid");
                System.Web.UI.WebControls.Label lblProfilepic = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblProfilepic");
                lblProfilepic.Text = "<a href='http://www.facebook.com/profile.php?id=" + lblfbid.Text + "' target='_top'><fb:profile-pic uid='" + lblfbid.Text + "' linked='false' width='30' height='30'></fb:profile-pic></a>";

                System.Web.UI.WebControls.Label lblfbname = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblfbname");
                lblfbname.Text = "<a href='http://www.facebook.com/profile.php?id=" + lblfbid.Text + "' target='_top'><fb:name uid=" + lblfbid.Text + " capitalize='true' linked='false'></fb:name></a>";
                
                System.Web.UI.WebControls.Label lblActivitytext = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblActivitytext");
                System.Web.UI.WebControls.Label lblfbid_added = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblfbid_added");                    
                System.Web.UI.WebControls.Label lblevent = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblevent");                    
                System.Web.UI.WebControls.Label lbltx_out_key = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbltx_out_key");                    
                System.Web.UI.WebControls.Label lblPaypal_Email = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblPaypal_Email");
                System.Web.UI.WebControls.Label lblAmount_Sent = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblAmount_Sent");
                System.Web.UI.WebControls.Label lblCurrency_Sent = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblCurrency_Sent");

                string strtempfbid_added = "<a href='http://www.facebook.com/profile.php?id=" + lblfbid_added.Text + "' target='_top'><fb:name reflexive='true' uid=" + lblfbid_added.Text + " capitalize='true' linked='false'></fb:name></a>";
                string amountsent = sitetemp.GetCurrencySymbol(lblCurrency_Sent.Text) + lblAmount_Sent.Text + lblCurrency_Sent.Text;
                lblActivitytext.Text = lblActivitytext.Text.Replace("<NEWADMIN>", strtempfbid_added);
                lblActivitytext.Text = lblActivitytext.Text.Replace("<EVENTNAME>", lblevent.Text);
                lblActivitytext.Text = lblActivitytext.Text.Replace("<PAYPAL_EMAIL>", lblPaypal_Email.Text);
                lblActivitytext.Text = lblActivitytext.Text.Replace("<AMOUNT>", amountsent);
                /*
                if (e.Row.RowIndex >= 2)
                {
                    System.Web.UI.WebControls.Label lblTopHidden = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblTopHidden");
                    lblTopHidden.Visible =true;
                    lblTopHidden.Text = "<span id='toploghidden' style='display:none;'>";
                }
                if (e.Row.RowIndex >= 2)
                {
                    System.Web.UI.WebControls.Label lblBottomHidden = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblBottomHidden");
                    lblBottomHidden.Visible =true;
                    lblBottomHidden.Text = "</span>";
                }*/
            }            

        }        

        public void NewActivity(int Type, int Resource_Key,Int64 fbid,Int64 fbid_added, int Event_Key, int tx_out_key)
        {
            /*
             *1 new admin
             *2 Added event
             *3 Edited event
             *4 took out money
             */
            Eventomatic_DB.SPs.UpdateActivity(Resource_Key, fbid, Type,fbid_added,Event_Key,tx_out_key).Execute();
        }
    }
}