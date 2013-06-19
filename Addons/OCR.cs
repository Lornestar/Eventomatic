using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aquaforest.OCR.Api;


namespace Eventomatic.Addons
{
    public class OCR
    {
        public string OCRImage()
        {
            string image_url = System.Web.HttpContext.Current.Server.MapPath("/Images/") + "mobile_buytix.png";

            // 1. Create Ocr and Preprocessor Objects
            // and enable console output
            Ocr _ocr = new Ocr();
            PreProcessor _preProcessor = new PreProcessor();
            _ocr.EnableConsoleOutput = true;
            // 2. OCR bin folder Location
            // The bin files can be copied to the application bin
            // folder. Alternatively the System Path and ocr
            // Resource folder can be set as shown below and
            // then just the files in the bin_add folder added
            // to the application bin folder.
            string OCRFiles = @"C:\Program Files (x86)\Aquaforest\bin";
            System.Environment.SetEnvironmentVariable("PATH", System.Environment.GetEnvironmentVariable("PATH") + ";" + OCRFiles);
            _ocr.ResourceFolder = OCRFiles;
            // 3. Set PreProcessor Options
            _preProcessor.Deskew = true;
            _preProcessor.Autorotate = false;
            _preProcessor.Despeckle = 9;
                        
            // 4. Set OCR Options
            _ocr.Language = SupportedLanguages.English;
            _ocr.EnablePdfOutput = true;
            // 5. Read Source TIFF File
            _ocr.ReadTIFFSource(@"C:\Program Files (x86)\Aquaforest\docs\tiffs\testcc4.tif");
            // 6. Perform OCR Recognition
            if (_ocr.Recognize(_preProcessor))
            {
            // 7. Save Output as Searchable PDF
           // _ocr.SavePDFOutput(
            //@"C:\Program Files (x86)\Aquaforest\docs\tiffs\testcc.pdf", true);

            _ocr.SaveTextOutput(@"C:\Program Files (x86)\Aquaforest\docs\tiffs\testcc.txt", true);
            string strtest = _ocr.ReadDocumentString();
            }
            // 8. Clean Up Temporary Files
            _ocr.DeleteTemporaryFiles(); 
            
            
            return "test";
        }
    }
}