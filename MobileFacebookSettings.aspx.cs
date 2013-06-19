using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Eventomatic
{
    public partial class MobileFacebookSettings : System.Web.UI.Page
    {
        string strlblon = "Groupstore will post a News Story on your Facebook Wall each time you sell a ticket.";
        string strlbloff = "Groupstore will NOT post a News Story on your Facebook Wall each time you sell a ticket.";
        string strbtnon = "Turn OFF Wall Post";
        string strbtnoff = "Turn ON Wall Post";
        Int64 fbid = 0;
        int eventkey = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["fbid"] != null) && (Request.QueryString["fbid"] != ""))
            {
                fbid = Convert.ToInt64(Request.QueryString["fbid"].ToString());
            }
            if ((Request.QueryString["event"] != null) && (Request.QueryString["event"] != ""))
            {
                eventkey = Convert.ToInt32(Request.QueryString["event"].ToString());
            }
            if (!IsPostBack)
            {                
                lblfbsettings.Text = strlblon;
                btnAdjustWall.Text = strbtnon;

                DataSet dstemp = Eventomatic_DB.SPs.ViewTicketSellersFbid(fbid).GetDataSet();
                if (dstemp.Tables[0].Rows[0]["Stream_Stories"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Stream_Stories"])== false)
                    {
                        lblfbsettings.Text = strlbloff;
                        btnAdjustWall.Text = strbtnoff;
                    }
                }                               
            }                
            
        }

        protected void btnAdjustWall_Click(object sender, EventArgs e)
        {
            if (btnAdjustWall.Text == strbtnon)
            {
                btnAdjustWall.Text = strbtnoff;
                lblfbsettings.Text = strlbloff;
                Eventomatic_DB.SPs.UpdateTicketSellersStreamStories(fbid, false).Execute();
            }
            else
            {
                btnAdjustWall.Text = strbtnon;
                lblfbsettings.Text = strlblon;
                Eventomatic_DB.SPs.UpdateTicketSellersStreamStories(fbid, true).Execute();
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Page.RegisterClientScriptBlock("thescript", "<script language=javascript> gotourl('Mobile.aspx?event=" + eventkey.ToString()+"');</script>");
            //Response.Redirect("Mobile.aspx?event=" + eventkey.ToString());
        }
    }
}