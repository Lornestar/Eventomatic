using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;

namespace Eventomatic.Addons
{
    public partial class Confirmation_Social : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int eventkey = 0;
            if (Request.QueryString["eventkey"] != null)
            {
                eventkey = Convert.ToInt32(Request.QueryString["eventkey"]);
            }
            DataSet dstemp = Eventomatic_DB.SPs.ViewEventDetails(eventkey).GetDataSet();
            string streventname = "";
            if (dstemp.Tables[0].Rows[0]["Event_Name"] != DBNull.Value)
            {
                Page.Title = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
                streventname = Page.Title;
            }

            string strurl = "http://thegroupstore.com/order_form.aspx?event_key=" + eventkey.ToString();
            char chr34 = Convert.ToChar(34);
            lbltweet.Text = "<a href='http://twitter.com/share' class='twitter-share-button' data-text='" + streventname + "' data-count='none' data-url='" + strurl + "'>Tweet</a>";            
            
            //lblfblike.Text = "<iframe src='http://www.facebook.com/plugins/like.php?href=" + strurl + "&amp;layout=button_count&amp;show_faces=false&amp;width=450&amp;action=like&amp;font&amp;colorscheme=light&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:450px; height:21px;' allowTransparency='true'></iframe>";                        
            lblfblike.Text = "<fb:like href=" + chr34 + strurl + chr34 + " layout=" + chr34 + "button_count" + chr34 + " show_faces=" + chr34 + "false" + chr34 + " font=" + chr34 + "arial" + chr34 + "></fb:like>";
            lbllinkedin.Text = "<script type='in/share' data-url='" + strurl + "' data-counter='right'></script>";

            Site Sitetemp = new Site();
            string strpicurl = Sitetemp.GetEventPic(eventkey.ToString());

            HtmlMeta hm = new HtmlMeta();
            HtmlHead head = (HtmlHead)Page.Header;

            hm.Name = "og:image";
            hm.Content = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + strpicurl.Substring(1);
            head.Controls.Add(hm);

            HtmlHead head2 = (HtmlHead)Page.Header;
            HtmlMeta hm2 = new HtmlMeta();

            hm2.Name = "og:url";
            hm2.Content = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Form.aspx?Event_Key=" + eventkey.ToString();
            head2.Controls.Add(hm2);
            
        }

    }
}