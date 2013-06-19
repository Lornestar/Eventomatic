using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Eventomatic
{
    public partial class mobileconfirm : System.Web.UI.Page
    {
        int Event_Key = 0;
        int Tx_Key = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Site sitetemp = new Site();
            if ((Request.QueryString["Tx_Key"] != null) && (Request.QueryString["Tx_Key"] != ""))
            {
                Tx_Key = Convert.ToInt32(Request.QueryString["Tx_Key"].ToString());

                Event_Key = Convert.ToInt32(sitetemp.GetEventKeyTx(Tx_Key));
            }
            else if ((Request.Form["Tx_Key"] != null) && (Request.Form["Tx_Key"] != ""))
            {
                Tx_Key = Convert.ToInt32(Request.Form["Tx_Key"].ToString());
                Event_Key = Convert.ToInt32(sitetemp.GetEventKeyTx(Tx_Key));
            }

            Addons.PaypalMethods paypalmethods = new Addons.PaypalMethods();

            if (Event_Key != 0) //it's an event
            {
                if (sitetemp.IsDemo(Event_Key))
                {
                    paypalmethods.DoExpressCheckout2(Tx_Key, false);
                }
                else
                {
                    paypalmethods.DoExpressCheckout2(Tx_Key,  true);
                }
            }
            else{ //it's payforward

                if (sitetemp.IsDemo_Payforward(Tx_Key))
                {                    
                    paypalmethods.DoExpressCheckout2(Tx_Key, false);
                }
                else
                {
                    paypalmethods.DoExpressCheckout2(Tx_Key, true);
                }

                bool didforward = false;
                DataSet dstemp = Eventomatic_DB.SPs.PfViewDidforward(Tx_Key).GetDataSet();
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    if (dstemp.Tables[0].Rows[0]["didforward"] != DBNull.Value) 
                    {
                        if (Convert.ToBoolean(dstemp.Tables[0].Rows[0]["didforward"].ToString()) == true)
                        {
                            didforward = true;
                        }
                    }
                }
                if (!didforward)
                {
                    Response.Redirect("Payforward2.aspx");
                }
            }
        }
    }
}