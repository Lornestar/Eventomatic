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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Security;
using PdfSharp.Drawing.Layout;
using System.Collections;
using System.IO;

namespace Eventomatic.Addons
{
    public class PDFReceipt2
    {
        public Hashtable CreatePDFReceipt(int Tx_Key)
        {
            Hashtable hsreturn = new Hashtable();
            DataSet dstemp2 = Eventomatic_DB.SPs.ViewTicketForEmail(Tx_Key).GetDataSet();
            int counter = 0;
            foreach (DataRow r in dstemp2.Tables[0].Rows)
            {
                counter += 1;

                string TxTime = "";
                string AmountPaid = "";
                DataSet dstemp = Eventomatic_DB.SPs.ViewEmailReceipt(Tx_Key).GetDataSet();
                if (dstemp.Tables[0].Rows[0]["Confirmation_Date"] != DBNull.Value)
                { TxTime = dstemp.Tables[0].Rows[0]["Confirmation_Date"].ToString(); }
                if (dstemp.Tables[0].Rows[0]["Amount2Dec"] != DBNull.Value)
                { AmountPaid = dstemp.Tables[0].Rows[0]["Amount2Dec"].ToString(); }

                string TicketNumber = Tx_Key.ToString();
                if (r["TicketNum"] != DBNull.Value)
                { TicketNumber = r["TicketNum"].ToString(); }
                string filedir = System.Web.HttpContext.Current.Server.MapPath("/Images/PDFs/");                
                string filename = filedir + TicketNumber  + ".pdf";                

                string Event_Key = dstemp.Tables[0].Rows[0]["Event_Key"].ToString();
                string Event_logo_url = System.Web.HttpContext.Current.Server.MapPath("/Images/Events/");

                Site sitetemp = new Site();
                string strimagefilename = sitetemp.GetEventPic(Event_Key.ToString());
                Event_logo_url += strimagefilename.Replace("/Images/Events/", "");

                System.Drawing.Image i = System.Drawing.Image.FromFile(Event_logo_url);
                float docHeight = (i.Height / i.VerticalResolution);
                float docWidth = (i.Width / i.HorizontalResolution);
                float docratio = (docHeight / docWidth);
                float imgheight = docratio * 100;


                string gs_logo_url = System.Web.HttpContext.Current.Server.MapPath("/Images/") + "groupstore_Order_Logo2.png";

                string EventName = "";
                if (dstemp.Tables[0].Rows[0]["Event_Name"] != DBNull.Value)
                { EventName = dstemp.Tables[0].Rows[0]["Event_Name"].ToString(); }

                DateTime tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Event_Begins"].ToString());
                string EventBeginsDate = tempdatetime.ToString("dddd, MMMM d yyyy hh:mm tt");
                //string location_issued = "Toronto, Ontario";

                string amount_received = "$" + AmountPaid;
                string date_received = TxTime;
                string date_issued = TxTime;

                string FirstName = "";
                if (r["first_name"] != DBNull.Value)
                { FirstName = r["first_name"].ToString(); }
                
                string LastName = "";
                if (r["last_name"] != DBNull.Value)
                { LastName = r["last_name"].ToString(); }

                string description = "";
                if (r["Purchase_Description"] != DBNull.Value)
                { description = r["Purchase_Description"].ToString(); }
                
                string bought = "";
                if (dstemp.Tables[0].Rows[0]["Confirmation_Date"] != DBNull.Value)
                { bought = "Bought " + dstemp.Tables[0].Rows[0]["Confirmation_Date"].ToString(); }

                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Please print and bring this ticket with you";
                document.Info.Author = "Groupstore";


                string locationinfo = "";
                string contactinfo = "";
                string Host = "";
                string Location = "";
                string Street = "";
                string City = "";
                string Phone = "";                
                if (dstemp.Tables[0].Rows[0]["Host"] != DBNull.Value)
                { Host = dstemp.Tables[0].Rows[0]["Host"].ToString(); }
                if (dstemp.Tables[0].Rows[0]["Location"] != DBNull.Value)
                { Location = dstemp.Tables[0].Rows[0]["Location"].ToString();
                locationinfo += Location;
                }
                if (dstemp.Tables[0].Rows[0]["Street"] != DBNull.Value)
                { 
                    Street = dstemp.Tables[0].Rows[0]["Street"].ToString();
                    if (Street.Length > 0)
                    {
                        if (locationinfo.Length > 0)
                        {
                            locationinfo += ", ";
                        }
                        locationinfo += Street;
                    }                
                }
                if (dstemp.Tables[0].Rows[0]["City"] != DBNull.Value)
                { 
                    City = dstemp.Tables[0].Rows[0]["City"].ToString();
                    if (City.Length > 0)
                    {
                        if (locationinfo.Length > 0)
                        {
                            locationinfo += ", ";
                        }
                    }
                    locationinfo += City;
                }
                if (dstemp.Tables[0].Rows[0]["Phone"] != DBNull.Value)
                { 
                    Phone = dstemp.Tables[0].Rows[0]["Phone"].ToString();
                    if (Phone.Length >0)
                    {
                        contactinfo += "Phone :" + Phone;
                    }
                }                
                                

                //Security   
                PdfSecuritySettings securitySettings = document.SecuritySettings;

                //securitySettings.UserPassword = "Lornestar";            
                securitySettings.OwnerPassword = "gojetsgo!@#";
                securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted128Bit;

                // Restrict some rights.            
                securitySettings.PermitAccessibilityExtractContent = true;
                securitySettings.PermitAnnotations = false;
                securitySettings.PermitAssembleDocument = false;
                securitySettings.PermitExtractContent = true;
                securitySettings.PermitFormsFill = false;
                securitySettings.PermitFullQualityPrint = true;
                securitySettings.PermitModifyDocument = false;
                securitySettings.PermitPrint = true;



                // Create an empty page
                PdfPage page = document.AddPage();
                int ypos = 10;
                //int ypos2 = Convert.ToInt32(page.Height / 2);                

                int ypossig = 230;                
                //int ypossig2 = ypossig + Convert.ToInt32(page.Height / 2);

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XPen pen = new XPen(XColors.Gray, 3);
                int x1 = 50;
                int x2 = 550;
                int y1 = ypos + 48;
                int y2 = ypos + 280;
                gfx.DrawLine(pen, x1, y1, x1, y2);
                gfx.DrawLine(pen, x1-1.5, y1, x2+1.5, y1);
                gfx.DrawLine(pen, x1-1.5, y2, x2+1.5, y2);
                gfx.DrawLine(pen, x2, y1, x2, y2);

                //Create header
                XFont font_header = new XFont("Arial", 16, XFontStyle.Underline);
                gfx.DrawString(document.Info.Title, font_header, XBrushes.Black, new XRect(0, ypos, page.Width, 20), XStringFormats.TopCenter);                

                //Receipt #                

                XFont font_receiptnum = new XFont("Arial", 12, XFontStyle.Bold);
                gfx.DrawString("#" + TicketNumber, font_receiptnum, XBrushes.Black, new XRect(x1 + 25, y1 + 25 + imgheight, 80, 130), XStringFormats.TopLeft);
                
                //Event info
                gfx.DrawImage(XImage.FromFile(Event_logo_url), x1+20, y1 + 20,100,imgheight);
                XFont font_header2 = new XFont("Arial", 14, XFontStyle.Bold);
                XFont font_regular = new XFont("Arial", 12, XFontStyle.Regular);
                XTextFormatter tf = new XTextFormatter(gfx);
                tf.DrawString("Name", font_header2, XBrushes.Gray, new XRect(x1 + 130, y1 + 20, 450, 20), XStringFormats.TopLeft);
                tf.DrawString(FirstName + " " + LastName, font_regular, XBrushes.Black, new XRect(x1 + 130, y1 + 35, 450, 20), XStringFormats.TopLeft);

                tf.DrawString("Event", font_header2, XBrushes.Gray, new XRect(x1 + 130, y1 + 60, 370, 20), XStringFormats.TopLeft);
                tf.DrawString(EventName, font_regular, XBrushes.Black, new XRect(x1 + 130, y1 + 75, 370, 20), XStringFormats.TopLeft);
                tf.DrawString(EventBeginsDate, font_regular, XBrushes.Black, new XRect(x1 + 130, y1 + 90, 370, 20), XStringFormats.TopLeft);
                tf.DrawString(locationinfo, font_regular, XBrushes.Black, new XRect(x1 + 130, y1 + 105, 370, 40), XStringFormats.TopLeft);
                tf.DrawString(contactinfo, font_regular, XBrushes.Black, new XRect(x1 + 130, y1 + 145, 370, 20), XStringFormats.TopLeft);                

                tf.DrawString("Ticket", font_header2, XBrushes.Gray, new XRect(x1 + 130, y1 + 160, 370, 20), XStringFormats.TopLeft);
                tf.DrawString(description, font_regular, XBrushes.Black, new XRect(x1 + 130, y1 + 175, 370, 20), XStringFormats.TopLeft);
                tf.DrawString(bought, font_regular, XBrushes.Black, new XRect(x1 + 130, y1 + 190, 370, 20), XStringFormats.TopLeft);                                
                
                //groupstore logo
                gfx.DrawImage(XImage.FromFile(gs_logo_url), x2 - 90, y2 - 45, 80, 30);
                XFont font_small = new XFont("Arial", 10, XFontStyle.Regular);
                tf.DrawString("theGroupstore.com", font_small, XBrushes.Black, new XRect(x2 - 90, y2 - 15, 100, 20), XStringFormats.TopLeft);
                
                // Save the document...                                 
                if (!File.Exists(filename))
                {
                    document.Save(filename);
                }                

                hsreturn.Add(counter, filename);
            }

            return hsreturn;           
 
        }
    }
}