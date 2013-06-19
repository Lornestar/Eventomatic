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

namespace Eventomatic.Addons
{
    public partial class Promote_Event : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Request.Form["__EVENTTARGET"] == "LoadCombo")
            {
                RadComboBox1.Visible = true;
                lblpromoteevent.Visible = false;
                LoadCombo();
            }*/
        }

        public void LoadCombo()
        {
            Site sitetemp = new Site();
            DataSet dstemp = sitetemp.ExecuteQuery("SELECT creator,name,start_time, eid  FROM event WHERE eid IN (SELECT eid FROM event_member WHERE uid=" + hdfbid.Value  + ") AND start_time > now()");
            if (dstemp != null)
            {
                if (dstemp.Tables.Count > 1)
                {
                    foreach (DataRow r in dstemp.Tables[1].Rows)
                    {
                        if (r.Table.Columns.Contains("start_time"))
                        {
                            r["start_time"] = sitetemp.fbDateTime(Convert.ToInt32(r["start_time"].ToString()));
                        }
                    }

                    RadComboBox1.DataSource = dstemp.Tables[1];
                    RadComboBox1.DataTextField = "name";
                    RadComboBox1.DataValueField = "eid";
                    RadComboBox1.DataBind();
                }
            }
        }

        protected void btnRemove_Click(object sender, System.EventArgs e)
        {
        }

        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
        }

        protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
        }
    }
}