using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Data;
using Telerik.Web.UI;

namespace Eventomatic.Addons
{
    public partial class fbGuestList : System.Web.UI.Page
    {
        string eid = "0";
        int viewtype = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["eid"] != null)
            {
                eid = Request.QueryString["eid"];
            }
            if (Request.QueryString["eid"] != null)
            {
                viewtype = Convert.ToInt32(Request.QueryString["viewtype"]);
            }
            if (!IsPostBack)
            {
                LoadListview(viewtype);
                switch (viewtype)
                {
                    case 1: RadCombo1.SelectedIndex = 0;
                        break;
                    case 2: RadCombo1.SelectedIndex = 1;
                        break;
                    case 3: RadCombo1.SelectedIndex = 2;
                        break;
                }
            }
        }

        protected void LoadListview(int type)
        {
            Site Sitetemp = new Site();
            JArray jsonAttending = Sitetemp.GetEventAttending(eid, type);
            if (jsonAttending.Count > 0)
            {
                DataSet dsattending = Sitetemp.GetdsEventAttending(jsonAttending, 1);
                DataTable dttemp = dsattending.Tables[0].Copy();
                dttemp.Merge(dsattending.Tables[1].Copy());
                dttemp.Merge(dsattending.Tables[2].Copy());
                RadListView1.DataSource = dttemp;
                RadListView1.DataBind();
            }                
        }

        protected void RadListView1_PageIndexChanged(object sender, RadListViewPageChangedEventArgs e)
        {
            int type = Convert.ToInt32(RadCombo1.SelectedValue);
            Site Sitetemp = new Site();
            JArray jsonAttending = Sitetemp.GetEventAttending(eid,type);
            DataSet dsattending = Sitetemp.GetdsEventAttending(jsonAttending, 1);
            RadListView1.DataSource = dsattending.Tables[type - 1];
            RadListView1.Rebind();
        }

        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadListview(Convert.ToInt32(RadCombo1.SelectedValue));
        }
    }
}
