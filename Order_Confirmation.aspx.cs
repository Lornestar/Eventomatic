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
    public partial class Order_Confirmation : System.Web.UI.Page
    {
        int Event_Key = 0;
        int Tx_Key = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["Tx_Key"] != null) && (Request.QueryString["Tx_Key"] != ""))
            {
                Tx_Key = Convert.ToInt32(Request.QueryString["Tx_Key"].ToString());
                Site sitetemp = new Site();
                Event_Key = Convert.ToInt32(sitetemp.GetEventKeyTx(Tx_Key));
            }
            else if ((Request.Form["Tx_Key"] != null) && (Request.Form["Tx_Key"] != ""))
            {
                Tx_Key = Convert.ToInt32(Request.Form["Tx_Key"].ToString());
                Site sitetemp = new Site();
                Event_Key = Convert.ToInt32(sitetemp.GetEventKeyTx(Tx_Key));
            }

            if ((Request.QueryString["demo"] != null) && (Request.QueryString["demo"] != ""))
            {
                if (Request.QueryString["demo"] == "true")
                {
                    lbldemo.Visible=true;
                    lbldemo2.Visible = true;
                    string strnewurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Attendee_List.aspx?Event_Key=" + Event_Key.ToString();
                    lbldemo.Text = "<br />Since this is a DEMO, <a href='" + strnewurl + "'>click here</a> to view this transaction on the Guest List.";
                    if (Request.QueryString["gsdemo"] == "true")
                    {
                        strnewurl = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Demo_AttendeeList.aspx?tx_key=" + Tx_Key.ToString();
                        lbldemo.Text = "<br />Since this is a DEMO, <a href='" + strnewurl + "'>click here</a> to view this transaction on the Guest List.";                        
                    }
                    lbldemo2.Text = lbldemo.Text;                    
                }
            }
            
            if (!IsPostBack)
            {
                PopulateExistingEvent(Event_Key);
            }
        }

        protected void PopulateExistingEvent(int Event_Key)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewEventDetails(Event_Key).GetDataSet();
            string strComments =dstemp.Tables[0].Rows[0]["Confirmation"].ToString();
            
            Page.Title = dstemp.Tables[0].Rows[0]["Event_Name"].ToString() ;
            //Confirmation_Social.Title = Page.Title;

            //Add images
            string Resource_Key = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
            Site Sitetemp = new Site();

            //imgGroup.ImageUrl = Sitetemp.GetResourcePic(Resource_Key);
            imgEvent.ImageUrl = Sitetemp.GetEventPic(Event_Key.ToString());

            DataSet dstemp2 = Eventomatic_DB.SPs.ViewTransactionDetailsTxnid(Tx_Key).GetDataSet();
            if (dstemp2.Tables[0].Rows[0]["first_name"] != DBNull.Value)
            {
                strComments = strComments.Replace("FIRSTNAME", dstemp2.Tables[0].Rows[0]["first_name"].ToString());
            }
            if (dstemp2.Tables[0].Rows[0]["last_name"] != DBNull.Value)
            {
                strComments = strComments.Replace("LASTNAME", dstemp2.Tables[0].Rows[0]["last_name"].ToString());
            }
            if (dstemp2.Tables[0].Rows[0]["Amount"] != DBNull.Value)
            {
                decimal decamount = decimal.Round(decimal.Parse(dstemp2.Tables[0].Rows[0]["Amount"].ToString()),2);
                strComments = strComments.Replace("AMOUNTPAID", "$" + decamount.ToString() );
            }

            lblConfirmation.Text = strComments.Replace(new String((char)13, 1), "<br>");

            bool IsFree = true;
            if (dstemp2.Tables[0].Rows.Count > 0)
            {
                if (dstemp2.Tables[0].Rows[0]["Amount"] != DBNull.Value)
                {
                    if (Convert.ToInt32(dstemp2.Tables[0].Rows[0]["Amount"]) > 0)
                    {
                        IsFree = false;
                    }
                }
            }
            if (IsFree)
            {
                pnlTxDetails.Visible = false;
            }
            else
            {
                HiddenField hdnTxn_Key = new HiddenField();
                hdnTxn_Key = (HiddenField)Payment_Details1.FindControl("Tx_Key");
                hdnTxn_Key.Value = Tx_Key.ToString();
            }
                

            if (lblConfirmation.Text.Length == 0)
            {
                pnlCustomMessage.Visible = false;
            }

            hdeventkey.Value = Event_Key.ToString();
            hdeventname.Value = Page.Title;
            hdfirstname.Value = dstemp2.Tables[0].Rows[0]["first_name"].ToString();
            hdtxkey.Value = Tx_Key.ToString();


            string strurl = "http://thegroupstore.com/order_form.aspx?event_key=" + Event_Key.ToString();
            char chr34 = Convert.ToChar(34);
            lbltweet.Text = "<a href='http://twitter.com/share' class='twitter-share-button' data-text='" + Page.Title + "' data-count='none' data-url='" + strurl + "'>Tweet</a>";

            //lblfblike.Text = "<iframe src='http://www.facebook.com/plugins/like.php?href=" + strurl + "&amp;layout=button_count&amp;show_faces=false&amp;width=450&amp;action=like&amp;font&amp;colorscheme=light&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:450px; height:21px;' allowTransparency='true'></iframe>";                        
            lblfblike.Text = "<fb:like href=" + chr34 + strurl + chr34 + " layout=" + chr34 + "button_count" + chr34 + " show_faces=" + chr34 + "false" + chr34 + " font=" + chr34 + "arial" + chr34 + "></fb:like>";
            lbllinkedin.Text = "<script type='in/share' data-url='" + strurl + "' data-counter='right'></script>";
                        
            string strpicurl = Sitetemp.GetEventPic(Event_Key.ToString());

            HtmlMeta hm = new HtmlMeta();
            HtmlHead head = (HtmlHead)Page.Header;

            hm.Name = "og:image";
            hm.Content = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + strpicurl.Substring(1);
            head.Controls.Add(hm);

            HtmlHead head2 = (HtmlHead)Page.Header;
            HtmlMeta hm2 = new HtmlMeta();

            hm2.Name = "og:url";
            hm2.Content = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Form.aspx?Event_Key=" + Event_Key.ToString();
            head2.Controls.Add(hm2);
        }

    }
}
