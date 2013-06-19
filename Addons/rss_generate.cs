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
using Rss;
using System.Xml;
using System.Text;
using System.IO;
using RssToolkit;

namespace Eventomatic.Addons
{
    public class rss_generate
    {
        public void WriteRss(int Resource_Key)
        {
            try
            {
                DataSet dsresource = Eventomatic_DB.SPs.ViewResource(Resource_Key).GetDataSet();
                DataSet dstemp = Eventomatic_DB.SPs.ViewStore(Resource_Key).GetDataSet();
                RssChannel channel = new RssChannel();
                string groupname = "Groupstore Rss";
                if (dsresource.Tables[0].Rows.Count > 0)
                {
                    if (dsresource.Tables[0].Rows[0]["Group_Name"] != DBNull.Value)
                    {
                        groupname = dsresource.Tables[0].Rows[0]["Group_Name"].ToString();   
                    }                    
                }                
                channel.Title = groupname;
                channel.Description = groupname;
                channel.PubDate = DateTime.Now;
                Uri siteUri = new Uri("http://www.theGroupstore.com/");
                channel.Link = siteUri;
                foreach (DataRow row in dstemp.Tables[0].Rows)
                {
                    RssItem item = new RssItem();
                    Uri itemUri = new Uri("http://www.theGroupstore.com/Order_Form.aspx?Event_Key=" + row["Event_Key"].ToString());
                    item.Link = itemUri;

                    if (row["Event_Name"] != DBNull.Value)
                    {
                        item.Title = row["Event_Name"].ToString();
                    }
                    else
                    {
                        item.Title = " ";
                    }
                    if (item.Title == "")
                    {
                        item.Title = " ";
                    }

                    if (row["Description"] != DBNull.Value)
                    {
                        item.Description = row["Description"].ToString().Replace("\n\r", " ");
                    }
                    else
                    {
                        item.Description = " ";
                    }
                    if (item.Description == "")
                    {
                        item.Description = " ";
                    }

                    if (row["Event_Begins"] != DBNull.Value)
                    {
                        item.PubDate = Convert.ToDateTime(row["Event_Begins"].ToString());
                    }
                    else
                    {
                        item.PubDate = DateTime.MinValue;
                    }
                    Site sitetemp = new Site();
                    string eventpic = sitetemp.GetEventPic(row["Event_Key"].ToString());
                    item.Comments = "http://www.theGroupstore.com" + eventpic;

                    channel.Items.Add(item);
                }
                if (dstemp.Tables[0].Rows.Count == 0)
                {
                    RssItem item = new RssItem();
                    item.Title = "Currently no Events Selling";
                    item.Description = " ";                    
                    item.Comments = " ";
                    item.PubDate = DateTime.MinValue;
                    Uri itemUri = new Uri("http://www.theGroupstore.com");
                    item.Link = itemUri;
                    channel.Items.Add(item);
                }
                FileStream MyFileStream = null;
                StreamWriter MyWriter = null;
                RssFeed feed = new RssFeed();
                feed.Channels.Add(channel);
                //HttpContext.Current.Response.ContentType = "text/xml";
                feed.Write(HttpContext.Current.Server.MapPath("/rss/") + Resource_Key.ToString() + ".xml");
                //feed.Write(HttpContext.Current.Response.OutputStream);
                //MyFileStream = new FileStream(Server.MapPath("Eventomatic") + "\\testfeed.xml", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                //MyWriter = new StreamWriter(MyFileStream);
                //MyWriter.Write(feed.w
                //HttpContext.Current.Response.OutputStream.Write( utput. .WriteFile("testrss.xml");
                //HttpContext.Current.Response.End();
            }
            catch
            {

            }
        }

        

    }
}
