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

namespace Eventomatic.ScheduledTask
{
    public partial class UpdateRss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewListResourcesAll(0).GetDataSet();
            foreach (DataRow rowtemp in dstemp.Tables[0].Rows)
            {                
                //modify existing rss xml file
                Eventomatic.Addons.rss_generate rssgenerate = new Eventomatic.Addons.rss_generate();
                rssgenerate.WriteRss(Convert.ToInt32(rowtemp["Resource_Key"]));                                
            }
              
        }
    }
}
