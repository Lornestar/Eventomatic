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
    public partial class Post_Authorize_Remove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strAuthorize = Request["fb_sig_authorize"];
            string strRemove = Request["fb_sig_uninstall"];
            string strfbid = Request["fb_sig_user"];
            if (strAuthorize != null)
            {
                Eventomatic_DB.SPs.UpdateLogPostAuthorizeRemove(Convert.ToInt64(strfbid), true, "0", "0").Execute();
            }
            else
            {                
                Eventomatic_DB.SPs.UpdateLogPostAuthorizeRemove(Convert.ToInt64(strfbid), false, "0", "0").Execute();
            }
        }
    }
}
