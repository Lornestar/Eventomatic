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
    public partial class Error404 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string strtemp = Request.Url.ToString();
            lblpath.Text = strtemp;
            string strtemp2 = strtemp.Replace("&AspxAutoDetectCookieSupport=1", "");            
            string strtemp3 = strtemp2.Replace("http://www.thegroupstore.com/error404.aspx?404;http://www.thegroupstore.com:80/", "");
            string strtemp4 = strtemp3.Replace("http://thegroupstore.com/error404.aspx?404;http://thegroupstore.com:80/", "");            
            lblpath2.Text = strtemp4;
            Eventomatic.Addons.Addons addons = new Eventomatic.Addons.Addons();
            strtemp4 = addons.EncodeStoreURL(strtemp4);

            DataSet dstemp = Eventomatic_DB.SPs.ViewResourceError404(strtemp4).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Resource_Key"] != DBNull.Value)
                {
                    string strtemp5 = "";
                    strtemp5 = "store.aspx?storeid=" + dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
                    lblpath.Text = strtemp5;
                    //Response.Redirect(strtemp3);
                    Server.Transfer(strtemp5);
                }
            }
            
        }
    }
}
