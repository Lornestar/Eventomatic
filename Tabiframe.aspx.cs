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
using System.Drawing;
using Newtonsoft.Json.Linq;
using Telerik.Web.UI;

namespace Eventomatic
{
    public partial class Tabiframe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var signedRequest = Facebook.Web.FacebookWebContext.Current.SignedRequest;

            if (signedRequest == null)
            {                
                if ((Request.QueryString["Signed_Request"] != null) && (Request.QueryString["Signed_Request"] != ""))
                {
                    signedRequest = Facebook.FacebookSignedRequest.Parse(ConfigurationSettings.AppSettings.Get("Secret").ToString(), Request.QueryString["Signed_Request"].ToString());
                }                
            }

            string strfbid = "391377955486";
            
            try
            {
                JObject data = JObject.Parse(signedRequest.Data.ToString());
                JObject data2 = JObject.Parse(data["page"].ToString());
                string strpageid = (string)data2["id"];
                strfbid = strpageid;
            }
            catch
            {
            }            
            DataSet dstemp = Eventomatic_DB.SPs.ViewStoreSellers(Convert.ToInt64(strfbid)).GetDataSet();
            hdpageid.Value = strfbid;

            RadListView1.DataSource = dstemp.Tables[0];
            RadListView1.DataBind();
        }

        protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            Site sitetemp = new Site(); 
            //if (e.Row.RowType == DataControlRowType.DataRow)
            if (e.Item is RadListViewDataItem)            
            {
                System.Web.UI.WebControls.Label lblEventKey = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblEvent_Key");
                RadBinaryImage imgEvent = (RadBinaryImage)e.Item.FindControl("ImgEvent");
                

                //Put in hyperlink
                int Event_Key = Convert.ToInt32(lblEventKey.Text);
                System.Web.UI.WebControls.HyperLink hypEventName = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("hypEvent_Name");
                System.Web.UI.WebControls.HyperLink hypEventText = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("hypEvent_Description");
                System.Web.UI.WebControls.Panel pnldate = (System.Web.UI.WebControls.Panel)e.Item.FindControl("pnldate");
                Label lbleventtype = (Label)e.Item.FindControl("lbleventtype");
                Label lbleid = (Label)e.Item.FindControl("lbleid");
                HyperLink hypbuynow = (HyperLink)e.Item.FindControl("hypbuynow");
                Label lblimagelink = (Label)e.Item.FindControl("lblimagelink");
                

                imgEvent.ImageUrl = sitetemp.GetEventPic(Event_Key.ToString());                
                
                bool IsProduct= sitetemp.Isproduct(Event_Key);
                if (IsProduct)
                {
                    pnldate.Visible = false;
                    hypEventName.NavigateUrl = hypEventName.NavigateUrl.Replace("Order_Form.aspx", "Order_Form_product.aspx");
                }                
                                
                if ((sitetemp.IsSoldOutEvent(Event_Key)) && (IsProduct == false))
                {                    
                    hypEventName.Text += " - Sold Out";
                }

                hypEventName.NavigateUrl += Event_Key.ToString(); //lblEventKey.Text;
                //check if is a promoted event
                if (lbleventtype.Text == "1") //it's a promoted event
                {
                    hypEventName.NavigateUrl = "http://www.facebook.com/event.php?eid=" + lbleid.Text;
                    imgEvent.ImageUrl = sitetemp.getgraphimg(lbleid.Text);
                    hypbuynow.Visible = false;
                }
                hypEventText.NavigateUrl = hypEventName.NavigateUrl;
                hypbuynow.NavigateUrl = hypEventName.NavigateUrl;
                lblimagelink.Text = "<a href='"+ hypEventName.NavigateUrl.Replace("~/","") + "' target='_blank'>";                

                //Put in Calendar info
                System.Web.UI.WebControls.Label lblMonth = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblMonth");
                System.Web.UI.WebControls.Label lblDay = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblDay");
                System.Web.UI.WebControls.Label lblDayofWeek = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblDayofWeek");
                System.Web.UI.WebControls.Label lblBegins = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblEvent_Begins");

                DateTime dtBegins = Convert.ToDateTime(lblBegins.Text);

                lblMonth.Text = dtBegins.ToString("MMM");
                lblDay.Text = dtBegins.ToString("dd");
                lblDayofWeek.Text = dtBegins.ToString("ddd");
                
            }
        }
    }
}