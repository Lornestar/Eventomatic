using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace Eventomatic.Addons
{
    public partial class Thermometer1 : System.Web.UI.Page
    {
        


        #region IHttpHandler Members
        int thepicwidth = 140;
        int thepicheight = 285;
        int x1 = 70;
        int x2 = 110;
        int xmid = 90;
        public bool IsReusable
        {
            get { return false; }
        }
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["Percent"] != null)
            {
                int Percent = int.Parse(context.Request.QueryString["Percent"]);
                int Max = int.Parse(context.Request.QueryString["Max"]);
                bool Dollar = int.Parse(context.Request.QueryString["Dollars"]) == 1;
                float Pixels = (float)Percent * 2.15f;
                Bitmap TempImage = new Bitmap(thepicwidth, thepicheight);
                Graphics TempGraphics = Graphics.FromImage(TempImage);
                TempGraphics.FillRectangle(Brushes.White, new Rectangle(0, 0, thepicwidth, thepicheight));
                Pen TempPen = new Pen(Color.Black);
                Pen RedPen = new Pen(Color.Red);
                TempGraphics.DrawArc(TempPen, x1-10, 224, 59, 59, 320, 260);
                TempGraphics.DrawLine(TempPen, x1-1, 235, x1-1, 20);
                TempGraphics.DrawLine(TempPen, x2+1, 235, x2+1, 20);
                TempGraphics.DrawArc(TempPen, x1-1, 5, 42, 40, 180, 180);
                GreyFloodFill(TempImage);
                FloodFill(TempImage, Percent);
                bool DrawText = true;
                decimal Amount = Max;
                int counter = 0;
                decimal AmountSection = Max / 20m;
                for (float y = 20.0f; y < 235.0f; y += 10.75f)
                {
                    counter +=1;
                    TempGraphics.DrawLine(TempPen, x1, y, x1+12, y);
                    if (DrawText)
                    {
                        Font TempFont = new Font("Arial", 14.0f, FontStyle.Bold);
                        if (Dollar)
                        {
                            TempGraphics.DrawString("$" + decimal.Round(Amount,2).ToString(), TempFont, Brushes.Black, 5, y);
                        }
                        else
                        {
                            TempGraphics.DrawString(decimal.Round(Amount, 2).ToString(), TempFont, Brushes.Black, 5, y);
                        }
                        DrawText = false;
                    }
                    else if ((counter == 10) || (counter == 19))
                    {
                        DrawText = true;
                    }

                    //Amount -= ((Max / 100) * 5.0f);
                    Amount -= AmountSection;
                }
                string etag = "\"" + Percent.GetHashCode() + "\"";
                string incomingEtag = context.Request.Headers["If-None-Match"];
                context.Response.Cache.SetExpires(DateTime.Now.ToUniversalTime().AddDays(1));
                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                context.Response.Cache.SetMaxAge(new TimeSpan(7, 0, 0, 0));
                context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                context.Response.Cache.SetETag(etag);
                if (String.Compare(incomingEtag, etag) == 0)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                    context.Response.End();
                }
                else
                {
                    context.Response.ContentType = "image/Gif";
                    TempImage.Save(context.Response.OutputStream, ImageFormat.Gif);
                }
            }
        }

        private void GreyFloodFill(Bitmap TempImage)
        {
            int RedColor = 255;
            for (int y = 20; y <= 235; ++y)
            {
                for (int x = x1; x <= x2; ++x)
                {
                    float Distance = Math.Abs(xmid - x);
                    RedColor = (int)(255.0f * (1.0f - (Distance / 30.0f)));
                    TempImage.SetPixel(x, y, Color.FromArgb(RedColor, RedColor, RedColor));
                }
            }
        }
        private void FloodFill(Bitmap TempImage, int Percent)
        {
            if (Percent == 100)
            {
                FillCircle(TempImage, xmid, 255, 29);
                FillCircle(TempImage, xmid, 25, 20);
                FillRectangle(TempImage, xmid, 20, 235);
            }
            else
            {
                FillCircle(TempImage, xmid, 255, 29);
                FillRectangle(TempImage, xmid, (int)(235.0f - ((float)Percent * 2.15f)), 235);
            }
        }
        private void FillRectangle(Bitmap TempImage, int x, int y1, int y2)
        {
            int MaxDistance = 20;
            for (int i = x - MaxDistance; i < x + MaxDistance; ++i)
            {
                for (int j = y1; j < y2; ++j)
                {
                    float Distance = (float)Math.Abs((i - x));
                    int RedColor = (int)(255.0f * (1.0f - (Distance / (2.0f * MaxDistance))));
                    TempImage.SetPixel(i, j, Color.FromArgb(RedColor, 0, 0));
                }
            }
        }
        private void FillCircle(Bitmap TempImage, int x, int y, int MaxDistance)
        {
            for (int i = x - MaxDistance; i < x + MaxDistance; ++i)
            {
                for (int j = y - MaxDistance; j < y + MaxDistance; ++j)
                {
                    float Distance = (float)Math.Sqrt(((j - y) * (j - y)) + ((i - x) * (i - x)));
                    if (Distance < MaxDistance)
                    {
                        int RedColor = (int)(255.0f * (1.0f - (Distance / (2.0f * MaxDistance))));
                        TempImage.SetPixel(i, j, Color.FromArgb(RedColor, 0, 0));
                    }
                }
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcessRequest(HttpContext.Current);
        }
    }
}
