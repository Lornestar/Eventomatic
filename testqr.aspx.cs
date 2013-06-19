using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Text;

namespace Eventomatic
{
    public partial class testqr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btntest_Click(object sender, EventArgs e)
        {
            /*Eventomatic.Addons.qrcodes qr = new Eventomatic.Addons.qrcodes();
            qr.GenerateEventqrimg(Convert.ToInt32(txttest.Text));
            
            string bitlyurl = "";

            //get bitly url

            string apikey = "R_690959f19386c7aa94fcf243ea002aee";
            string strlogin = "lornestar";
            string urlcall = "https://api-ssl.bitly.com/v3/shorten?login=" + strlogin + "&apiKey=" + apikey + "&longUrl=http%3A%2F%2Fbetaworks.com%2F&format=json";

            
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8; //This is if you have non english characters
            string result = wc.DownloadString(urlcall);
            JObject o = JObject.Parse(result);
            JObject level1 = (JObject)o["data"];
            //JArray level1 = (JArray)o["data"];            

            string strnewurl = (string)level1["url"];
            bitlyurl = " or click here " + strnewurl + ".";
             * */
            HttpWebResponse webResponse;
            string APIurl = "https://api.anoni.com/sessions";
            StringBuilder requestString = new StringBuilder();
            requestString.Append("username=lornestar2");
            requestString.Append("password=x339211x");
            string request = requestString.ToString();

                // Create request object
                HttpWebRequest webRequest = WebRequest.Create(APIurl) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = request.Length;

                // Write the request string to the request object
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.Write(request);
                writer.Close();

                // Get the response from the request object and verify the status
                webResponse = webRequest.GetResponse() as HttpWebResponse;

        }
    }
}