using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Eventomatic.Addons
{
    public partial class BackgroundImage2 : System.Web.UI.Page
    {
        Int32 eventkey = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["eventkey"] != null)
            {
                eventkey = Convert.ToInt32(Request.QueryString["eventkey"]);
            }
            string username = "../Images/BackgroundImages";
            Dictionary<string, string> t = System.IO.Directory.GetFiles(MapPath(username)).ToDictionary(p => username + "/" + System.IO.Path.GetFileName(p));
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Backgroundurl");

            foreach (KeyValuePair<string, string> kvp in t)
            {
                DataRow dr = dt.NewRow();
                dr["Backgroundurl"] = kvp.Key;
                if (kvp.Key.Contains("jpg"))
                {
                    dt.Rows.Add(dr);
                }                
            }

            RadListView1.DataSource = dt;
            RadListView1.DataBind();                          
        }

        public void SaveBkImgUrl()
        {
            string[] strvalues = hdnSelectedImagesrc.Value.Split('/');
            string strtemp = "../Images/BackgroundImages/" + strvalues[strvalues.Length - 1];
            Eventomatic_DB.SPs.UpdateEventBkImgUrl(eventkey, strtemp).Execute();
        }

        protected void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            SaveBkImgUrl();
            RadAjaxPanel1.ResponseScripts.Add(string.Format("CloseWindow();return false;", ""));
        }
    }
}