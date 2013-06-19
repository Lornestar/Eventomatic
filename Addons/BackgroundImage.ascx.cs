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
using System.Collections.Generic;

namespace Eventomatic.Addons
{
    public partial class BackgroundImage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = "../Images/BackgroundImages";
                Dictionary<string, string> t = System.IO.Directory.GetFiles(MapPath(username)).ToDictionary(p => username + "/" + System.IO.Path.GetFileName(p));
                Repeater1.DataSource = t;
                Repeater1.DataBind();

                DataSet dstemp = Eventomatic_DB.SPs.ViewEventBkImgUrl(Convert.ToInt32(Event_Key.Value)).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    if (dstemp.Tables[0].Rows[0]["BkImgUrl"] != DBNull.Value)
                    {
                        string strtemp = dstemp.Tables[0].Rows[0]["BkImgUrl"].ToString().Trim();
                        string[] strvalues = strtemp.Split('/');

                        ImgBackground.ImageUrl = "../Images/BackgroundImages/" + strvalues[strvalues.Length - 1];
                        hdnSelectedImageid.Value = "ctl00_body_UltraWebTab1__ctl0_BackgroundImage1_Repeater1_ctl" + strvalues[strvalues.Length - 1].Replace(".jpg", "") + "_img1";
                    }                    
                }                                
            }
        }

        public void SaveBkImgUrl()
        {
            Eventomatic_DB.SPs.UpdateEventBkImgUrl(Convert.ToInt32(Event_Key.Value), ImgBackground.ImageUrl.ToString()).Execute();
        }

        protected void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            //After click update images
            ImgBackground.ImageUrl = hdnSelectedImagesrc.Value;
        }
    }
}