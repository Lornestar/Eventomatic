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
using System.IO;


namespace Eventomatic.Addons
{
    public partial class Upload : System.Web.UI.UserControl
    {
        /*int Event_Key = 0;
        public int Event_Key_Set //property
        {
            set { Event_Key = value; } //write into property
        }*/
        int Event_Resource;
        //0 is Event
        //1 is Resource
        string Event_Resource_FileName;
        string Event_Resource_URL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Event_Key.Value != "-1")
                {
                    ReloadVars();

                    Site Sitetemp = new Site();
                    if (Sitetemp.ImgExists(Event_Resource, Event_Resource_FileName + ".jpg"))
                    {
                        pnlImg.Visible = true;
                        ImgEvent.ImageUrl = Event_Resource_URL + Event_Resource_FileName + ".jpg?Time=" + DateTime.Now.TimeOfDay.ToString(); 
                        lblCurrentImage.Visible = false;
                    }
                }                
            }
        }

        public void LoadfbEvent()
            //If they are importing a fb Event
        {
            ReloadVars();

            Site Sitetemp = new Site();
            if (Sitetemp.ImgExists(Event_Resource, Event_Resource_FileName + ".jpg"))
            {
                pnlImg.Visible = true;
                ImgEvent.ImageUrl = Event_Resource_URL + Event_Resource_FileName + ".jpg?Time=" + DateTime.Now.TimeOfDay.ToString();
                lblCurrentImage.Visible = false;
            }
        }

        protected void ReloadVars(){
            if (Convert.ToInt32(Event_Key.Value) > 0)
            {
                Event_Resource = 0;
                Event_Resource_URL = "/Images/Events/";
                Event_Resource_FileName = Event_Key.Value;
            }
            else if (Convert.ToInt32(Resource_Key.Value) > 0)
            {
                Event_Resource = 1;
                Event_Resource_URL = "/Images/Groups/";
                Event_Resource_FileName = Resource_Key.Value;
            }            
            else if (Event_Key.Value == "-1")
            {
                Event_Resource = 0;
                Event_Resource_URL = "/Images/Events/";
                Event_Resource_FileName = "Temp" + Current_Resource.Value;
            }
        }

        protected void btnImage_Click(object sender, EventArgs e)
        {
                ReloadVars();
                string imgresult = uploadFile(Event_Resource_FileName + ".jpg", Event_Resource_URL);
                if ((imgresult == "File uploaded successfully") && (Event_Resource == 1))
                {
                    //createthumbnail(Event_Resource_FileName + ".jpg", Event_Resource_URL);
                }
                lblImageError.Text = imgresult;
                lblImageError.Visible = true;
                if (imgresult != "File uploaded successfully")
                {
                    lblImageError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblImageError.ForeColor = System.Drawing.Color.Blue;
                    lblCurrentImage.Visible = false;
                    ImgEvent.ImageUrl = Event_Resource_URL + Event_Resource_FileName + ".jpg?Time=" + DateTime.Now.TimeOfDay.ToString();
                    pnlImg.Visible = true;
                }
            
        }

        protected string createthumbnail(string fileName, string folderName)
        {
            try
            {
                // get the file name
                string file = Server.MapPath(folderName) + "\\" + fileName;

                // create an image object, using the filename we just retrieved
                System.Drawing.Image image = System.Drawing.Image.FromFile(file);

                // create the actual thumbnail image
                int Maxheight = 90;
                System.Drawing.Image thumbnailImage;
                if (image.Height > Maxheight)
                {
                    decimal newwidth = image.Width* ((decimal)Maxheight/(decimal)image.Height);
                    thumbnailImage = image.GetThumbnailImage(Maxheight, Convert.ToInt32(newwidth), new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
                }
                else
                {
                    thumbnailImage = image;
                }
                

                // make a memory stream to work with the image bytes
                MemoryStream imageStream = new MemoryStream();

                // put the image into the memory stream
                thumbnailImage.Save(Server.MapPath(folderName) + "\\Thumbs\\" + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                /*thumbnailImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                // make byte array the same size as the image
                byte[] imageContent = new Byte[imageStream.Length];

                // rewind the memory stream
                imageStream.Position = 0;

                // load the byte array with the image
                imageStream.Read(imageContent, 0, (int)imageStream.Length);
                
                // return byte array to caller with image type
                Response.ContentType = "image/jpeg";
                Response.BinaryWrite(imageContent);*/
                

                return "File uploaded successfully";
            }
            catch (UnauthorizedAccessException ex)
            {
                return ex.Message + "Error creating thumbnail";
            }
        }

        public bool ThumbnailCallback()
        {
            return true;
        }

        protected string uploadFile(string fileName, string folderName)
        {
            if (fileName == "")
            {
                return "Invalid filename supplied";
            }
            if (fileUpload.PostedFile.ContentLength == 0)
            {
                return "Invalid file content";
            }
            fileName = System.IO.Path.GetFileName(fileName);
            if (folderName == "")
            {
                return "Path not found";
            }
            try
            {
                if ((fileUpload.PostedFile.ContentType.ToString() != "image/jpeg") && (fileUpload.PostedFile.ContentType.ToString() != "image/jpg") && (fileUpload.PostedFile.ContentType.ToString() != "image/pjpeg"))
                {
                    return "Must be of type jpg";
                }
                else if (fileUpload.PostedFile.ContentLength <= 500000)
                {
                    fileUpload.PostedFile.SaveAs(Server.MapPath(folderName) + "\\" + fileName);
                    return "File uploaded successfully";
                }
                else
                {
                    return "Unable to upload,file exceeds maximum limit";
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return ex.Message + "Permission to upload file denied";
            }
        }

        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {
            //Delete Image
            ReloadVars();
            System.IO.File.Delete(Server.MapPath(Event_Resource_URL) + Event_Resource_FileName + ".jpg");
            if (Event_Resource == 1)
            {
                System.IO.File.Delete(Server.MapPath(Event_Resource_URL) + "Thumbs/" + Event_Resource_FileName + ".jpg");
            }
            lblImageError.Visible = true;
            lblImageError.ForeColor = System.Drawing.Color.Blue;
            lblImageError.Text = "Image has been removed.";
            lblCurrentImage.Visible = false;
            pnlImg.Visible = false;
        }
    }
}