using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Eventomatic.Addons
{
    public partial class FaceMash : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Load_Pics( DataTable dttemp)
        {
            DataTable outputTable = dttemp.Clone();
            int amountpics = 5;
            if (dttemp.Rows.Count < amountpics)
            {
                amountpics = dttemp.Rows.Count;
            }
            for (int i = 0; i < amountpics; i++)
            {
                outputTable.ImportRow(dttemp.Rows[i]);
            }
            RadListView1.DataSource = outputTable;
            RadListView1.DataBind();            
        }
    }
}