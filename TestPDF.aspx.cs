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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Security;
using PdfSharp.Drawing.Layout;


namespace Eventomatic
{
    public partial class TestPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string Receipt_Number = "COPSON10-0000002913-CV";
            string charity_logo_url = "d:/lorne.jpg";
            string charity_info = "Canadian Cancer Society\nOntario Division\n55 St. Clair Ave W, Suite 500\nToronto, Ontario\nM4V 2Y7";
            string location_issued = "Toronto, Ontario";
            string amount_received = "$18.00";
            string date_received = "May 05, 2010";
            string date_issued = "May 05, 2010";
            string donar_info = "debbie osiel\nhome address";
            string charity_number = "11882 9803 RR0006 (CAN) – 98-6001242 (U.S.)";
            string personal_message = "Dear SPONSOR,\n\nPlease accept our heartfelt thanks for your generous support of the Canadian Cancer Society’s Cops for Cancer event.\n\n" +
            "Thanks to the support of generous donors like you, our volunteers and staff are leading the fight against cancer." +
            "Cancer touches so many lives, every year.\n\n" +
            "The good news is that, thanks to research, we are making inroads against many forms of cancer. But without you, such advances would not be possible. You are an important and valued link in the chain of hope that connects yesterday’s groundwork to today’s discoveries and to tomorrow’s victories.\n\n" +
            "Thank you once again for joining the fight.\n\n" +
            "Sincerely,";
            string personal_message_signature = "Rick Perciante\n" +
            "Acting CEO\n" +
            "Canadian Cancer Society, Ontario Division";
            string personal_message_signature_url = "";

            string leftsidetext = "Location Issued: " + location_issued + "\n\nAmount Issued: " + amount_received + "\n\nDate Received: "
                + date_received + "\n\nDate Issued: " + date_received + "\n\n" + donar_info + "\n\nRegistered Charity #:" + charity_number
                + "\n\nCanada Revenue Agency: www.cra-arc.gc.ca/charities";


            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created by Groupstore";

            //Security   
            PdfSecuritySettings securitySettings = document.SecuritySettings;
            //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;
            
            // Restrict some rights.            
            securitySettings.PermitAccessibilityExtractContent = false;
            securitySettings.PermitAnnotations = false;
            securitySettings.PermitAssembleDocument = false;
            securitySettings.PermitExtractContent = false;
            securitySettings.PermitFormsFill = false;
            securitySettings.PermitFullQualityPrint = true;
            securitySettings.PermitModifyDocument = false;
            securitySettings.PermitPrint = true;

            
           
            // Create an empty page
            PdfPage page = document.AddPage();
            int ypos = 0;
            int ypos2 = Convert.ToInt32(page.Height/2);
            int ypossig = 230;
         
            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
         
            //Create header
            XFont font_header = new XFont("Arial", 12, XFontStyle.Underline);
            gfx.DrawString("This is your official receipt for income tax purposes.", font_header, XBrushes.Black, new XRect(0, ypos, page.Width, 20), XStringFormats.TopCenter);
            gfx.DrawString("This is your official receipt for income tax purposes.", font_header, XBrushes.Black, new XRect(0, ypos2, page.Width, 20), XStringFormats.TopCenter);
              
            //Receipt #
            XFont font_receipt = new XFont("Arial", 12, XFontStyle.Bold);
            gfx.DrawString("Receipt Number:", font_receipt, XBrushes.Red, new XRect(page.Width/2, ypos+20, page.Width/2, 20), XStringFormats.TopCenter);
            gfx.DrawString("Receipt Number:", font_receipt, XBrushes.Red, new XRect(page.Width / 2, ypos2 + 20, page.Width / 2, 20), XStringFormats.TopCenter);

            XFont font_receiptnum = new XFont("Arial", 8, XFontStyle.Bold);
            gfx.DrawString(Receipt_Number, font_receiptnum, XBrushes.Black, new XRect(page.Width / 2, ypos + 30, page.Width / 2, 20), XStringFormats.TopCenter);
            gfx.DrawString(Receipt_Number, font_receiptnum, XBrushes.Black, new XRect(page.Width / 2, ypos2 + 30, page.Width / 2, 20), XStringFormats.TopCenter);

            

            //Charity info
            gfx.DrawImage(XImage.FromFile("d:/jump2.jpg"), 10, ypos+40);
            gfx.DrawImage(XImage.FromFile("d:/jump2.jpg"), 10, ypos2 + 40);
            XFont font_regular = new XFont("Arial", 8, XFontStyle.Regular);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(charity_info, font_regular, XBrushes.Black, new XRect(160, ypos + 80, 130, 40), XStringFormats.TopLeft);            
            tf.DrawString(charity_info, font_regular, XBrushes.Black, new XRect(160, ypos2 + 80, 130, 40), XStringFormats.TopLeft);

            tf.DrawString(leftsidetext, font_regular, XBrushes.Black, new XRect(10, ypos + 200, 250, 250), XStringFormats.TopLeft);
            tf.DrawString(leftsidetext, font_regular, XBrushes.Black, new XRect(10, ypos2 + 200, 250, 250), XStringFormats.TopLeft);

            //personal message
            tf.DrawString(personal_message, font_regular, XBrushes.Black, new XRect(300, ypos + 80, 280, 350), XStringFormats.TopLeft);
            tf.DrawString(personal_message, font_regular, XBrushes.Black, new XRect(300, ypos2 + 80, 280, 350), XStringFormats.TopLeft);

            gfx.DrawImage(XImage.FromFile("d:/signature.jpg"), 300, ypossig);
            gfx.DrawImage(XImage.FromFile("d:/signature.jpg"), 300, ypossig);

            tf.DrawString(personal_message_signature, font_regular, XBrushes.Black, new XRect(300,ypossig+50, 280, 50), XStringFormats.TopLeft);
            tf.DrawString(personal_message_signature, font_regular, XBrushes.Black, new XRect(300, ypossig+50, 280, 50), XStringFormats.TopLeft);

            //gfx.DrawImage(XImage.FromFile("d:/lorne.jpg"), 0, 0);
            // Save the document...
            const string filename = "d:/HelloWorld.pdf";
            document.Save(filename);

            

            
        }
    }
}
