using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Eventomatic.Addons
{
    public class Addons
    {
        public string EncodeStoreURL(string Name)
        {
            string strreturn = Name;
            strreturn = strreturn.Replace(" ", "");
            strreturn = strreturn.Replace("!", "");
            strreturn = strreturn.Replace("'", "");
            strreturn = strreturn.Replace("#", "");
            strreturn = strreturn.Replace("$", "");
            strreturn = strreturn.Replace("%", "");
            strreturn = strreturn.Replace("^", "");
            strreturn = strreturn.Replace("&", "");
            strreturn = strreturn.Replace("*", "");
            strreturn = strreturn.Replace("(", "");
            strreturn = strreturn.Replace(")", "");
            strreturn = strreturn.Replace("<", "");
            strreturn = strreturn.Replace(">", "");
            strreturn = strreturn.Replace("?", "");
            strreturn = strreturn.Replace("/", "");
            strreturn = strreturn.Replace(":", "");
            strreturn = strreturn.Replace(";", "");
            strreturn = strreturn.Replace("+", "");
            strreturn = strreturn.Replace("=", "");
            return strreturn;
        }
    }
}
