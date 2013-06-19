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
    public partial class Tab3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string streventkey = Request.Form["event_key"].ToString();

            // Show/Hide Goals = 0 / Set Goal Amount = 1 / Custom Message = 2
            string strtype = Request.Form["type"].ToString();
            string strCustom = "";
            string strsellfbid = "";
            string strbuyerfbid = "";
            string strgoalamount = "";
            //Custom Message
            if (Request.Form["txtCustom"] != null)
            {
                strCustom = Request.Form["txtCustom"].ToString();
            }
            else if (Request.QueryString["txtCustom"] != null)
            {
                strCustom = Request.QueryString["txtCustom"].ToString();
            }
            //Seller fbid
            if (Request.Form["fb_sig_profile"] != null)
            {
                strsellfbid = Request.Form["fb_sig_profile"].ToString();
            }
            else if (Request.QueryString["fb_sig_profile"] != null)
            {
                strsellfbid = Request.QueryString["fb_sig_profile"].ToString();
            }
            if (Request.Form["goalamount"] != null)
            {
                strgoalamount = Request.Form["goalamount"].ToString();
            }
            else if (Request.QueryString["goalamount"] != null)
            {
                strgoalamount = Request.QueryString["goalamount"].ToString();
            }

            if (strtype == "0")//Show/Hide Goals
            {
                Eventomatic_DB.SPs.UpdateShowGoals(Convert.ToInt32(streventkey), Convert.ToInt64(strsellfbid)).Execute();
                Response.Write("AckShow|");
            }
            else if (strtype == "1")//Save new Goals
            {                
                    Eventomatic_DB.SPs.UpdateGoalAmount(Convert.ToInt32(streventkey), Convert.ToInt64(strsellfbid),Convert.ToDecimal(strgoalamount)).Execute();
                    Response.Write("AckNewGoals|");             
            }
            else if (strtype == "2")//Save new Custom
            {
                Eventomatic_DB.SPs.UpdateCustomMessage(Convert.ToInt32(streventkey), Convert.ToInt64(strsellfbid), strCustom).Execute();
                Response.Write("AckNewCustom|");
            }
        }
    }
}
