using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarcodeLib.Barcode;
using System.IO;
using System.Drawing;
using System.Configuration;

namespace Eventomatic.Addons
{
    public class qrcodes
    {

        public void GenerateEventqrimg(int event_key)
        {
            /*BarcodeLib.Barcode.QRCode.QRCode qrCode = new BarcodeLib.Barcode.QRCode.QRCode();
            qrCode.Data = ctrData.Text;
            qrCode.Encoding = BarcodeUtils.convertString2QRCodeEncoding(ctrEncoding.Text);
            qrCode.Version = (BarcodeLib.Barcode.QRCode.QRCodeVersion)(ctrVersion.SelectedIndex);
            qrCode.ECL = (BarcodeLib.Barcode.QRCode.ErrorCorrectionLevel)(ctrECL.SelectedIndex);
            qrCode.EnableStructuredAppend = bool.Parse(ctrStructuredAppend.Text);
            qrCode.StructuredAppendCount = int.Parse(ctrStructuredAppendCount.Text);
            qrCode.StructuredAppendIndex = int.Parse(ctrStructuredAppendIndex.Text);
            qrCode.FNC1Mode = (BarcodeLib.Barcode.QRCode.QRCodeFNC1Mode)(ctrFNC1Mode.SelectedIndex);
            qrCode.ApplicationIndicator = byte.Parse(ctrApplicationIndicator.Text);
            qrCode.ECI = int.Parse(ctrECI.Text);
            qrCode.ProcessTilde = bool.Parse(ctrProcessTilde.Text);

            qrCode.UOM = (UnitOfMeasure)ctrUOM.SelectedIndex;
            qrCode.ModuleSize = float.Parse(ctrModuleSize.Text);
            qrCode.TopMargin = float.Parse(ctrTopMargin.Text);
            qrCode.LeftMargin = float.Parse(ctrLeftMargin.Text);
            qrCode.RightMargin = float.Parse(ctrRightMargin.Text);
            qrCode.BottomMargin = float.Parse(ctrBottomMargin.Text);

            //  Todo: Resolution
            qrCode.Resolution = int.Parse(ctrResolution.Text);

            qrCode.ImageFormat = BarcodeUtils.convertString2ImageFormat(ctrImageFormat.Text, ImageFormat.Gif); ;

            qrCode.Refresh();*/

            string Event_logo_url = System.Web.HttpContext.Current.Server.MapPath("/Images/qr/");
            string filename = Event_logo_url + event_key.ToString() + ".png";

            if (!File.Exists(filename))
            {

                BarcodeLib.Barcode.QRCode.QRCode barcode = new BarcodeLib.Barcode.QRCode.QRCode();

                barcode.Data = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "mobile.aspx?event=" + event_key.ToString();//"123456789012";

                barcode.ModuleSize = 10;
                barcode.LeftMargin = 15;
                barcode.RightMargin = 15;
                barcode.TopMargin = 15;
                barcode.BottomMargin = 15;

                barcode.UOM = BarcodeLib.Barcode.Linear.UnitOfMeasure.Pixel;                
                //barcode.ImageWidth = 125;
                //barcode.ImageHeight = 125;
                
                barcode.Encoding = BarcodeLib.Barcode.QRCode.QRCodeEncoding.Auto;

                barcode.Version = BarcodeLib.Barcode.QRCode.QRCodeVersion.Auto;

                barcode.ECL = BarcodeLib.Barcode.QRCode.ErrorCorrectionLevel.L;

                // more barcode settings here

                // save barcode image into your system

                barcode.drawBarcode(filename);

            }
            /*
	        // generate barcode & output to byte array
	        byte[] barcodeInBytes = barcode.drawBarcodeAsBytes();
	            
	        // generate barcode to Graphics object
	        Graphics graphics = barcode.s;
	        barcode.drawBarcode(graphics);
	
	        // generate barcode and output to HttpResponse object
	        HttpResponse response = ...;
	        barcode.drawBarcode(response);
	
	        // generate barcode and output to Stream object
	        Stream stream = ...;
	        barcode.drawBarcode(stream);*/
        }

        public void GenerateEventqrimg(int event_key,Int64 fbid)
        {
            string Event_logo_url = System.Web.HttpContext.Current.Server.MapPath("/Images/qr/");
            string filename = Event_logo_url + event_key.ToString() + "-" + fbid.ToString() + ".png";

            if (!File.Exists(filename))
            {

                BarcodeLib.Barcode.QRCode.QRCode barcode = new BarcodeLib.Barcode.QRCode.QRCode();

                barcode.Data = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "mobile.aspx?event=" + event_key.ToString() + "&fbid=" + fbid;

                barcode.ModuleSize = 10;
                barcode.LeftMargin = 15;
                barcode.RightMargin = 15;
                barcode.TopMargin = 15;
                barcode.BottomMargin = 15;

                barcode.UOM = BarcodeLib.Barcode.Linear.UnitOfMeasure.Pixel;
                //barcode.ImageWidth = 125;
                //barcode.ImageHeight = 125;

                barcode.Encoding = BarcodeLib.Barcode.QRCode.QRCodeEncoding.Auto;

                barcode.Version = BarcodeLib.Barcode.QRCode.QRCodeVersion.Auto;

                barcode.ECL = BarcodeLib.Barcode.QRCode.ErrorCorrectionLevel.L;

                // more barcode settings here

                // save barcode image into your system

                barcode.drawBarcode(filename);

            }         
        }

        public void Generatetxqrimg(int tx_key)
        {
            string Event_logo_url = System.Web.HttpContext.Current.Server.MapPath("/Images/qr/tx/");
            string filename = Event_logo_url + tx_key.ToString() + ".png";

            if (!File.Exists(filename))
            {

                BarcodeLib.Barcode.QRCode.QRCode barcode = new BarcodeLib.Barcode.QRCode.QRCode();

                barcode.Data = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "mobilepay.aspx?tx=" + tx_key.ToString();

                barcode.ModuleSize = 10;
                barcode.LeftMargin = 15;
                barcode.RightMargin = 15;
                barcode.TopMargin = 15;
                barcode.BottomMargin = 15;

                barcode.UOM = BarcodeLib.Barcode.Linear.UnitOfMeasure.Pixel;
                //barcode.ImageWidth = 125;
                //barcode.ImageHeight = 125;

                barcode.Encoding = BarcodeLib.Barcode.QRCode.QRCodeEncoding.Auto;

                barcode.Version = BarcodeLib.Barcode.QRCode.QRCodeVersion.Auto;

                barcode.ECL = BarcodeLib.Barcode.QRCode.ErrorCorrectionLevel.L;

                // more barcode settings here

                // save barcode image into your system

                barcode.drawBarcode(filename);

            }
        }

        public void Generatepfqrimg(int resource_key)
        {
            string Event_logo_url = System.Web.HttpContext.Current.Server.MapPath("/Images/qr/pf/");
            string filename = Event_logo_url + resource_key.ToString() + ".png";

            if (!File.Exists(filename))
            {

                BarcodeLib.Barcode.QRCode.QRCode barcode = new BarcodeLib.Barcode.QRCode.QRCode();

                barcode.Data = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Payforward2.aspx?resource_key=" + resource_key.ToString();

                barcode.ModuleSize = 10;
                barcode.LeftMargin = 15;
                barcode.RightMargin = 15;
                barcode.TopMargin = 15;
                barcode.BottomMargin = 15;

                barcode.UOM = BarcodeLib.Barcode.Linear.UnitOfMeasure.Pixel;
                //barcode.ImageWidth = 125;
                //barcode.ImageHeight = 125;

                barcode.Encoding = BarcodeLib.Barcode.QRCode.QRCodeEncoding.Auto;

                barcode.Version = BarcodeLib.Barcode.QRCode.QRCodeVersion.Auto;

                barcode.ECL = BarcodeLib.Barcode.QRCode.ErrorCorrectionLevel.L;

                // more barcode settings here

                // save barcode image into your system

                barcode.drawBarcode(filename);

            }
        }
    }
}