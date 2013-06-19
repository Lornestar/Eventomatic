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

namespace Eventomatic.Addons
{
    public class PDFReceipt
    {
        public string CreatePDFReceipt(int Tx_Key, string donar_info, string location_issued)
        {            
            string TxTime = "";
            string AmountPaid = "";
            DataSet dstemp = Eventomatic_DB.SPs.ViewEmailReceipt(Tx_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["Confirmation_Date"] != DBNull.Value)
            { TxTime = dstemp.Tables[0].Rows[0]["Confirmation_Date"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Amount2Dec"] != DBNull.Value)
            { AmountPaid = dstemp.Tables[0].Rows[0]["Amount2Dec"].ToString(); }

            DataSet dstemppdf = Eventomatic_DB.SPs.ViewEventsFundraiserPdf(Tx_Key).GetDataSet();

            string Receipt_Number = Tx_Key.ToString();
            if (dstemppdf.Tables[0].Rows[0]["Receipt_Number"] != DBNull.Value)
            { Receipt_Number = dstemppdf.Tables[0].Rows[0]["Receipt_Number"].ToString() + Tx_Key.ToString(); }

            string Resource_Key = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString();
            string charity_logo_url = System.Web.HttpContext.Current.Server.MapPath("/Images/Groups/") + Resource_Key+ ".jpg";

            string charity_info = "Canadian Cancer Society\nOntario Division\n55 St. Clair Ave W, Suite 500\nToronto, Ontario\nM4V 2Y7";
            if (dstemppdf.Tables[0].Rows[0]["Charity_Info"] != DBNull.Value)
            { charity_info = dstemppdf.Tables[0].Rows[0]["Charity_Info"].ToString(); }
            //string location_issued = "Toronto, Ontario";
            
            string amount_received = "$" + AmountPaid;
            string date_received = TxTime;
            string date_issued = TxTime;

            //donar_info = "debbie osiel\nhome address";
            string charity_number = "11882 9803 RR0006 (CAN) – 98-6001242 (U.S.)";
            if (dstemppdf.Tables[0].Rows[0]["Charity_Number"] != DBNull.Value)
            { charity_number = dstemppdf.Tables[0].Rows[0]["Charity_Number"].ToString(); }

            string personal_message = "Dear SPONSOR,\n\nPlease accept our heartfelt thanks for your generous support of the Canadian Cancer Society’s Cops for Cancer event.\n\n" +
            "Thanks to the support of generous donors like you, our volunteers and staff are leading the fight against cancer." +
            "Cancer touches so many lives, every year.\n\n" +
            "The good news is that, thanks to research, we are making inroads against many forms of cancer. But without you, such advances would not be possible. You are an important and valued link in the chain of hope that connects yesterday’s groundwork to today’s discoveries and to tomorrow’s victories.\n\n" +
            "Thank you once again for joining the fight.\n\n" +
            "Sincerely,";
            if (dstemppdf.Tables[0].Rows[0]["Personal_Message"] != DBNull.Value)
            { personal_message = dstemppdf.Tables[0].Rows[0]["Personal_Message"].ToString(); }

            string personal_message_signature = "Rick Perciante\n" +
            "Acting CEO\n" +
            "Canadian Cancer Society, Ontario Division";
            if (dstemppdf.Tables[0].Rows[0]["Personal_Message_Signature"] != DBNull.Value)
            { personal_message_signature = dstemppdf.Tables[0].Rows[0]["Personal_Message_Signature"].ToString(); }
            personal_message = personal_message.Replace("&cr", "\n");
            charity_info = charity_info.Replace("&cr", "\n");
            personal_message_signature = personal_message_signature.Replace("&cr", "\n");
            
            string personal_message_signature_url = System.Web.HttpContext.Current.Server.MapPath("/Images/Signature/") + dstemppdf.Tables[0].Rows[0]["Event_Key"].ToString() + ".jpg";
            

            string leftsidetext = "Location Issued: " + location_issued + "\n\nAmount Issued: " + amount_received + "\n\nDate Received: "
                + date_received + "\n\nDate Issued: " + date_received + "\n\n" + donar_info + "\n\nRegistered Charity #:" + charity_number
                + "\n\nCanada Revenue Agency: www.cra-arc.gc.ca/charities";


            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Please print off your tax receipt";
            document.Info.Author = "Groupstore";


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
            int ypos = 0;
            int ypos2 = Convert.ToInt32(page.Height / 2);


            int ypossig = 230;
            if (dstemppdf.Tables[0].Rows[0]["ypossig"] != DBNull.Value)
            { ypossig = Convert.ToInt32(dstemppdf.Tables[0].Rows[0]["ypossig"].ToString()); }
            int ypossig2 = ypossig + Convert.ToInt32(page.Height / 2);

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //Create header
            XFont font_header = new XFont("Arial", 12, XFontStyle.Underline);
            gfx.DrawString("This is your official receipt for income tax purposes.", font_header, XBrushes.Black, new XRect(0, ypos, page.Width, 20), XStringFormats.TopCenter);
            gfx.DrawString("This is your official receipt for income tax purposes.", font_header, XBrushes.Black, new XRect(0, ypos2, page.Width, 20), XStringFormats.TopCenter);

            //Receipt #
            XFont font_receipt = new XFont("Arial", 12, XFontStyle.Bold);
            gfx.DrawString("Receipt Number:", font_receipt, XBrushes.Red, new XRect(page.Width / 2, ypos + 20, page.Width / 2, 20), XStringFormats.TopCenter);
            gfx.DrawString("Receipt Number:", font_receipt, XBrushes.Red, new XRect(page.Width / 2, ypos2 + 20, page.Width / 2, 20), XStringFormats.TopCenter);

            XFont font_receiptnum = new XFont("Arial", 8, XFontStyle.Bold);
            gfx.DrawString(Receipt_Number, font_receiptnum, XBrushes.Black, new XRect(page.Width / 2, ypos + 30, page.Width / 2, 20), XStringFormats.TopCenter);
            gfx.DrawString(Receipt_Number, font_receiptnum, XBrushes.Black, new XRect(page.Width / 2, ypos2 + 30, page.Width / 2, 20), XStringFormats.TopCenter);



            //Charity info
            gfx.DrawImage(XImage.FromFile(charity_logo_url), 10, ypos + 80);
            gfx.DrawImage(XImage.FromFile(charity_logo_url), 10, ypos2 + 80);
            XFont font_regular = new XFont("Arial", 8, XFontStyle.Regular);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(charity_info, font_regular, XBrushes.Black, new XRect(160, ypos + 80, 130, 40), XStringFormats.TopLeft);
            tf.DrawString(charity_info, font_regular, XBrushes.Black, new XRect(160, ypos2 + 80, 130, 40), XStringFormats.TopLeft);

            tf.DrawString(leftsidetext, font_regular, XBrushes.Black, new XRect(10, ypos + 200, 250, 250), XStringFormats.TopLeft);
            tf.DrawString(leftsidetext, font_regular, XBrushes.Black, new XRect(10, ypos2 + 200, 250, 250), XStringFormats.TopLeft);

            //personal message
            tf.DrawString(personal_message, font_regular, XBrushes.Black, new XRect(300, ypos + 80, 280, 350), XStringFormats.TopLeft);
            tf.DrawString(personal_message, font_regular, XBrushes.Black, new XRect(300, ypos2 + 80, 280, 350), XStringFormats.TopLeft);

            gfx.DrawImage(XImage.FromFile(personal_message_signature_url), 300, ypossig);
            gfx.DrawImage(XImage.FromFile(personal_message_signature_url), 300, ypossig2);

            tf.DrawString(personal_message_signature, font_regular, XBrushes.Black, new XRect(300, ypossig + 50, 280, 50), XStringFormats.TopLeft);
            tf.DrawString(personal_message_signature, font_regular, XBrushes.Black, new XRect(300, ypossig2 + 50, 280, 50), XStringFormats.TopLeft);

            //gfx.DrawImage(XImage.FromFile("d:/lorne.jpg"), 0, 0);
            // Save the document...
            string filedir = System.Web.HttpContext.Current.Server.MapPath("/Images/PDFs/");
            string LastName = "";
            if (dstemp.Tables[0].Rows[0]["last_name"] != DBNull.Value)
            { LastName = dstemp.Tables[0].Rows[0]["last_name"].ToString(); }

            string filename = filedir + "JUMP_" + Tx_Key.ToString() + "_" + LastName + ".pdf";
            if (dstemppdf.Tables[0].Rows[0]["File_Name"] != DBNull.Value)
            { filename = filedir + dstemppdf.Tables[0].Rows[0]["File_Name"].ToString() + Tx_Key.ToString() + "_" + LastName + ".pdf"; }
            document.Save(filename);

            return filename;
        }
    }
}
