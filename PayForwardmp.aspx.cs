using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventomatic
{
    public partial class PayForwardmp : System.Web.UI.Page
    {
        int Tx_Key = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["tx"] != null) && (Request.QueryString["tx"] != ""))
            {
                Tx_Key = Convert.ToInt32(Request.QueryString["tx"].ToString());
                hdtxkey.Value = Tx_Key.ToString();

                Site sitetemp = new Site();
                if (sitetemp.IsDemo_Payforward(Tx_Key))
                {
                    hdisDemo.Value = "True";
                }

                if (sitetemp.CheckiftxComplete(Tx_Key))
                {
                    hdAlreadyPaid.Value = "True";
                }
             }
        }
    }
}