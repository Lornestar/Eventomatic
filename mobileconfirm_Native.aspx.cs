using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventomatic
{
    public partial class mobileconfirm_Native : System.Web.UI.Page
    {
        int Event_Key = 0;
        int Tx_Key = 0;
        string mobileos = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Site sitetemp = new Site();

            mobileos = sitetemp.getMobileOS();
            string strscript = "";

            if (Request.QueryString["cancel"] == "true"){
                if (mobileos == "android")
                {
                    strscript = "<script language=JavaScript>window.demo.clickOnSaleCancelled();</script>";
                }
                else
                {
                    strscript = "<script language=JavaScript>NativeBridge.call('clickOnSaleCancelled', [], function() {});</script>";
                }
            }
            else
            {
                
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

                if (sitetemp.IsDemo_Payforward(Tx_Key))
                {
                    paypalmethods.DoExpressCheckout2(Tx_Key, false);
                }
                else
                {
                    paypalmethods.DoExpressCheckout2(Tx_Key, true);
                }

                if (mobileos == "android")
                {
                    strscript = "<script language=JavaScript>window.demo.clickOnSaleComplete();</script>";
                }
                else
                {
                    strscript = "<script language=JavaScript>NativeBridge.call('clickOnSaleComplete', [], function() {});</script>";
                }
                
            }

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", strscript);
        }
    }
}