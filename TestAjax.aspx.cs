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

namespace Eventomatic
{
    public partial class TestAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
                protected void btnRemove_Click(object sender, System.EventArgs e)
        {
        }


                protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
                {
                }
                protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
                {
                    if (e.Argument == "InitialPageLoad")
                    {
                        //simulate longer page load
                        System.Threading.Thread.Sleep(2000);                        
                    }
                } 
                         
    }
}