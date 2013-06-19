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
    public class ConnectService
    {

        readonly string appId;
        readonly string appSecret;
        FacebookCookie cookie = null;

        public ConnectService()
        {
            appId = ConfigurationManager.AppSettings["APIKey"];
            appSecret = ConfigurationManager.AppSettings["Secret"];
        }

        #region IFacebookConnectService Members

        public bool IsConnected()
        {
            if (cookie == null)
            {
                cookie = FacebookCookie.GetCookie(appId, appSecret);
            }
            return
                cookie != null &&
                cookie.UserId != 0 &&
                !string.IsNullOrEmpty(cookie.SessionKey);
        }

        public string SessionKey
        {
            get
            {
                if (cookie != null)
                {
                    return cookie.SessionKey;
                }
                else
                {
                    return null;
                }
            }
        }

        public long UserId
        {
            get
            {
                if (cookie != null)
                {
                    return cookie.UserId;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string AccessToken
        {
            get
            {
                if (cookie != null)
                {
                    return cookie.AccessToken;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

    }
}
