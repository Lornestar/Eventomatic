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
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;


namespace Eventomatic.Addons
{
    public class Thermometer
    {
        #region IHttpHandler Members
        public bool IsReusable
        {
            get { return false; }
        }
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["Percent"] == null)
            {
                int Percent = int.Parse(context.Request.QueryString["Percent"]);
                int Max = int.Parse(context.Request.QueryString["Max"]);
                bool Dollar = int.Parse(context.Request.QueryString["Dollars"]) == 1;
                float Pixels = (float)Percent * 2.15f;
                Bitmap TempImage = new Bitmap(100, 270);
                Graphics TempGraphics = Graphics.FromImage(TempImage);
                TempGraphics.FillRectangle(Brushes.White, new Rectangle(0, 0, 100, 270));
                Pen TempPen = new Pen(Color.Black);
                Pen RedPen = new Pen(Color.Red);
                TempGraphics.DrawArc(TempPen, 45, 230, 30, 30, 320, 260);
                TempGraphics.DrawLine(TempPen, 49, 235, 49, 20);
                TempGraphics.DrawLine(TempPen, 71, 235, 71, 20);
                TempGraphics.DrawArc(TempPen, 49, 15, 22, 11, 180, 180);
                GreyFloodFill(TempImage);
                FloodFill(TempImage, Percent);
                bool DrawText = true;
                float Amount = Max;
                for (float y = 20.0f; y < 235.0f; y += 10.75f)
                {
                    TempGraphics.DrawLine(TempPen, 49, y, 55, y);
                    if (DrawText)
                    {
                        Font TempFont = new Font("Arial", 8.0f, FontStyle.Bold);
                        if (Dollar)
                        {
                            TempGraphics.DrawString("$" + Amount.ToString(), TempFont, Brushes.Black, 5, y);
                        }
                        else
                        {
                            TempGraphics.DrawString(Amount.ToString(), TempFont, Brushes.Black, 5, y);
                        }
                        DrawText = false;
                    }
                    else DrawText = true;
                    Amount -= ((Max / 100) * 5.0f);
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
                for (int x = 50; x <= 70; ++x)
                {
                    float Distance = Math.Abs(60 - x);
                    RedColor = (int)(255.0f * (1.0f - (Distance / 30.0f)));
                    TempImage.SetPixel(x, y, Color.FromArgb(RedColor, RedColor, RedColor));
                }
            }
        }
        private void FloodFill(Bitmap TempImage, int Percent)
        {
            if (Percent == 100)
            {
                FillCircle(TempImage, 60, 245, 15);
                FillCircle(TempImage, 60, 20, 10);
                FillRectangle(TempImage, 60, 20, 235);
            }
            else
            {
                FillCircle(TempImage, 60, 245, 15);
                FillRectangle(TempImage, 60, (int)(235.0f - ((float)Percent * 2.15f)), 235);
            }
        }
        private void FillRectangle(Bitmap TempImage, int x, int y1, int y2)
        {
            int MaxDistance = 10;
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
    }
}

