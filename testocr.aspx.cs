using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventomatic
{
    public partial class testocr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btntest_Click(object sender, EventArgs e)
        {
            Eventomatic.Addons.OCR ocr = new Eventomatic.Addons.OCR();
            ocr.OCRImage();
        }
    }
}