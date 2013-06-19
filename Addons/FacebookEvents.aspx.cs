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
using Telerik.Web.UI;

namespace Eventomatic.Addons
{
    public partial class FacebookEvents : System.Web.UI.Page
    {
        Int64 fbid = 0;
        Int32 resourcekey = 0;
        int type = 0; //type = 0 is promote event / type = 1 is sell event
        string rooturl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["fbid"] != null)
                {
                    fbid = Convert.ToInt64(Request.QueryString["fbid"]);                    
                }
                if ((Request.QueryString["resourcekey"] != null) && (Request.QueryString["resourcekey"] != ""))
                {
                    resourcekey = Convert.ToInt32(Request.QueryString["resourcekey"]);
                }
                if ((Request.QueryString["type"] != null) && (Request.QueryString["type"] != ""))
                {
                    type = Convert.ToInt32(Request.QueryString["type"]);
                }
                LoadListview();
            }
        }

        public void LoadListview()
        {
            hypnotfb.NavigateUrl = rooturl + "order_form2.aspx?storeid=" + resourcekey + "&edit=true";
            Site sitetemp = new Site();
            DataSet dstemp = sitetemp.ExecuteQuery_NoSession("SELECT creator,name,start_time, eid  FROM event WHERE eid IN (SELECT eid FROM event_member WHERE uid=" + fbid.ToString() + ") AND start_time > now()",fbid);
            if (dstemp != null)
            {
                if (dstemp.Tables.Count > 1)
                {
                    foreach (DataRow r in dstemp.Tables[1].Rows)
                    {
                        if (r.Table.Columns.Contains("start_time"))
                        {
                            r["start_time"] = sitetemp.fbDateTime(Convert.ToInt32(r["start_time"].ToString()));
                        }
                    }

                    RadListView1.DataSource = dstemp.Tables[1];
                    RadListView1.DataBind();
                }
            }            
            
        }

        protected void btnRemove_Click(object sender, System.EventArgs e)
        {
        }


        protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            Site sitetemp = new Site();
            if (e.Item is RadListViewDataItem)
            {
                RadBinaryImage rdbi = (RadBinaryImage)e.Item.FindControl("radimage");
                //HyperLink hypeventname = (HyperLink)e.Item.FindControl("hypevent");
                Label lbleid = (Label)e.Item.FindControl("lbleventid");
                Label lblaction =(Label)e.Item.FindControl("lblaction");

                rdbi.ImageUrl = "https://graph.facebook.com/" + lbleid.Text + "/picture";
                if (type == 0)//Promote event
                {
                    lblaction.Text = "<a href='#' onclick='returnToParent(" + lbleid.Text + "); return false;' target='_blank'>Promote Event</a>";
                }
                else
                {
                    //string rooturl = ConfigurationSettings.AppSettings.Get("Root_URL").ToString();
                    //if ((Request.QueryString["rooturl"] != null) && (Request.QueryString["rooturl"] != ""))
                    //{                      
                    //}
                    //lblaction.Text = "<a href='" + rooturl + "Edit_Event.aspx?eid=" + lbleid.Text + "' target='_top'><b>Sell Event!</b></a>";                    
                    //lblaction.Text = "<a href='" + rooturl + "order_form2.aspx?eid=" + lbleid.Text + "&edit=true' target='_top'><b>Sell Event!</b></a>";                    
                    char chr34 = Convert.ToChar(34);
                    string strnewurl = rooturl + "order_form2.aspx?eid=" + lbleid.Text + "&storeid=" + resourcekey;
                    string prod = "1";
                    if (rooturl.Contains("localhost"))
                    {
                        prod = "0";
                    }
                    //lblaction.Text = "<a href='" + rooturl + "order_form2.aspx?eid=" + lbleid.Text + "&storeid=" + resourcekey + "&edit=true' target='_blank'><b>Sell Event!</b></a>";                    
                    lblaction.Text = "<a href='#' onclick='returnToParent2(" + lbleid.Text + "," + resourcekey + ","+prod +"); return false;'>Sell Event</a>";
                }
            }            
        }        
    }
}
