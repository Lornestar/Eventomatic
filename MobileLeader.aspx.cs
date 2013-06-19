using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventomatic
{
    public partial class MobileLeader : System.Web.UI.Page
    {
        int Event_Key = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Request.QueryString["event"] != null) && (Request.QueryString["event"] != ""))
                {
                    Event_Key = Convert.ToInt32(Request.QueryString["event"].ToString());
                }
                //Send New to user control
                HiddenField hdnEvent_Key = new HiddenField();
                hdnEvent_Key = (HiddenField)LeaderBoard1.FindControl("hdEvent_Key");
                hdnEvent_Key.Value = Event_Key.ToString();

            }
        }
    }
}