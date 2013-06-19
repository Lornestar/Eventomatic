using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.IO;

namespace Eventomatic
{
    /// <summary>
    /// Summary description for dummyWebservice
    /// </summary>
    [WebService(Namespace = "http://localhost:49450")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class dummyWebservice : System.Web.Services.WebService
    {

        [WebMethod()]
        public string HelloToYou(string name)
        {
            return "Hello " + name;
        }

        [WebMethod()]
        public string sayHello()
        {
            return "hello ";
        }  
    }
}
