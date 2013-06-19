using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Eventomatic
{
    /// <summary>
    /// Summary description for Order_Confirmation1
    /// </summary>
    [WebService(Namespace = "http://localhost:49450")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Order_Confirmation1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld(string name)
        {
            return "msg=Hello " + name;
        }

        [WebMethod]
        public string SaveFBtx(string fbid,string txkey)
        {
            Eventomatic_DB.SPs.UpdateTransactionFbSeller(Convert.ToInt32(txkey), Convert.ToInt64(fbid)).Execute();
            return "Hello " + fbid;
        }
    }
}
