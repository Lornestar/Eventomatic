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
    public partial class testcomments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblfbcomments.Text = "<fb:comments xid='131957560178399_51' canpost='true' returnurl='http://www.facebook.com/profile.php?id=121100861?v=app_131957560178399' candelete='false' simple='false' reverse='true' showform='true'></fb:comments>";
        }
    }
}
