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
using Telerik.Web.UI;
using System.IO;

namespace Eventomatic
{
    public partial class Store : System.Web.UI.Page
    {
        int Resource_Key = 0;
        bool editing = false;
        Int64 fbid = 0;
        bool NoNetwork = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["Storeid"] != null) && (Request.QueryString["Storeid"] != ""))
                {
                    Resource_Key = Convert.ToInt32(Request.QueryString["Storeid"].ToString());

                    pnledit.Visible = false;
                    if (!IsPostBack)
                    {
                        

                        Site Sitetemp = new Site();
                        if ((Request.QueryString["edit"] != null) && (Request.QueryString["edit"] != ""))
                        {
                            if (Request.QueryString["edit"] == "true")
                            {//In editing mode                                
                                pnledit.Visible = true;

                                //check if logged in or out of fb
                                Eventomatic.Addons.ConnectService fbconnect = new Eventomatic.Addons.ConnectService();
                                if (fbconnect.IsConnected())
                                {
                                    pnlloggedin.Visible = true;
                                    fbid = fbconnect.UserId;
                                    hdnfbid.Value = fbid.ToString();

                                    imgfbuser.ImageUrl = "http://graph.facebook.com/" + fbid.ToString() + "/picture";
                                    lblfbuser.Text += Sitetemp.graph_getfbname(fbid);

                                    //check if fbid is store admin
                                    if (Sitetemp.CheckIfIsStoreAdmin(fbid, Resource_Key))
                                    {
                                        //is store admin
                                        lblfbuserstoreadmin.Text = "You are an admin of this store";

                                        //allow user to edit
                                        pnleditcontrols.Visible = true;
                                        editing = true;
                                        ChangeEditing(editing);
                                        LoadListboxes();
                                    }
                                    else
                                    {
                                        //not store admin
                                    }
                                }
                                else
                                {
                                    pnlloggedout.Visible = true;
                                }
                            }
                        }

                        
                        //DataSet dstemp = Eventomatic_DB.SPs.ViewStore(Resource_Key).GetDataSet();
                        DataSet dstemp = Eventomatic_DB.SPs.ViewStoreWithSelectedGroups(Resource_Key).GetDataSet();

                        RadListView1.DataSource = 
                        RadListView1.DataSource = dstemp.Tables[0];
                        RadListView1.DataBind();

                        if ((RadListView1.Items.Count == 0) || (dstemp.Tables[0].Rows.Count==0))
                        {
                            pnlGroupName.Visible = false;
                            lblheader.Visible = true;
                            lblheader.Text = "There are currently no events on sale."; }
                                                

                        DataSet dstemp2 = Eventomatic_DB.SPs.ViewStoreInfo(Resource_Key).GetDataSet();
                        if (dstemp2.Tables[0].Rows[0]["Store_Title"] != DBNull.Value)
                        {
                            lblStoreTitle.Text = dstemp2.Tables[0].Rows[0]["Store_Title"].ToString();
                            txtStoreTitle.Text = dstemp2.Tables[0].Rows[0]["Store_Title"].ToString();
                        }
                        if (dstemp2.Tables[0].Rows[0]["Store_Description"] != DBNull.Value)
                        {       
                            
                            string strComments = dstemp2.Tables[0].Rows[0]["Store_Description"].ToString();
                            string strComments2 = strComments.Replace(new String((char)13, 1), "<br>");
                            if (strComments2.ToLower().Contains("http://"))
                            {
                                Site sitetemp2 = new Site();
                                strComments2 = sitetemp2.ReplaceLinks(strComments2);
                            }
                            lblStoreDescription.Text = strComments2;
                            txtStoreDescription.Text = dstemp2.Tables[0].Rows[0]["Store_Description"].ToString();
                        }
                        if (dstemp2.Tables[0].Rows[0]["Store_Contact"] != DBNull.Value)
                        {
                            lblContact.Text = dstemp2.Tables[0].Rows[0]["Store_Contact"].ToString();
                            txtContact.Text = dstemp2.Tables[0].Rows[0]["Store_Contact"].ToString();
                        }
                        if ((lblStoreDescription.Text == "") && (lblStoreTitle.Text == "") && (lblContact.Text == "") && (!editing))
                        {
                            pnlInfo.Visible = false;
                        }
                        if (dstemp2.Tables[0].Rows[0]["Group_Name"] != DBNull.Value)
                        {
                            lblGroupName.Text = dstemp2.Tables[0].Rows[0]["Group_Name"].ToString();
                            Page.Title = lblGroupName.Text + "'s groupstore";
                        }

                        if ((lblStoreDescription.Text.Length < 1) && (RadListView1.Items.Count == 1) && (!pnledit.Visible))
                        {
                            System.Web.UI.WebControls.HyperLink hypEventName = (System.Web.UI.WebControls.HyperLink)RadListView1.Items[0].FindControl("hypEvent_Name");
                            string strurl = hypEventName.NavigateUrl;
                            Response.Redirect(strurl);
                        }

                        //Group image
                        
                        imgGroup.ImageUrl = Sitetemp.GetResourcePic(Resource_Key.ToString());
                        if (imgGroup.ImageUrl == "")
                        {
                            imgGroup.Visible = false;
                        }
                        Thumbnail.ImageUrl = imgGroup.ImageUrl;
                    }
            }
        }

        protected void ChangeEditing(bool Editmode)
        {
            bool ineditmode = Editmode;
            bool innoneditmode = !Editmode;

            pnlInfo.Visible = true;

            lblStoreTitle.Visible = innoneditmode;
            txtStoreTitle.Visible = ineditmode;

            lblStoreDescription.Visible = innoneditmode;
            txtStoreDescription.Visible = ineditmode;

            lblContact.Visible = innoneditmode;
            txtContact.Visible = ineditmode;

            imgGroup.Visible = innoneditmode;
            Thumbnail.Visible = ineditmode;
            AsyncUpload1.Visible = ineditmode;

            if (ineditmode)
            {
                DataSet dstemp2 = Eventomatic_DB.SPs.ViewStoreInfo(Resource_Key).GetDataSet();
                if (dstemp2.Tables[0].Rows[0]["Network_Key"] != DBNull.Value)
                {
                    if (dstemp2.Tables[0].Rows[0]["Network_Key"].ToString() == "1")
                    {//No network
                        NoNetwork = true;
                    }
                }
            }

            if (NoNetwork == false)
            {
                pnlOthergroups.Visible = ineditmode;
            }            
            
        }

        protected void AsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {            
            Thumbnail.Height = Unit.Pixel(150);

            using (Stream stream = e.File.InputStream)
            {
                byte[] imageData = new byte[stream.Length];
                stream.Read(imageData, 0, (int)stream.Length);
                Thumbnail.DataValue = imageData;
            }            

        }

        protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
        
        //protected void RadListView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{           
            Site sitetemp = new Site(); 
            //if (e.Row.RowType == DataControlRowType.DataRow)
            if (e.Item is RadListViewDataItem)            
            {
                System.Web.UI.WebControls.Label lblEventKey = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblEvent_Key");
                RadBinaryImage imgEvent = (RadBinaryImage)e.Item.FindControl("ImgEvent");
                

                //Put in hyperlink
                int Event_Key = Convert.ToInt32(lblEventKey.Text);
                System.Web.UI.WebControls.HyperLink hypEventName = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("hypEvent_Name");
                System.Web.UI.WebControls.HyperLink hypEventText = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("hypEvent_Description");
                System.Web.UI.WebControls.Panel pnldate = (System.Web.UI.WebControls.Panel)e.Item.FindControl("pnldate");
                Label lbleventtype = (Label)e.Item.FindControl("lbleventtype");
                Label lbleid = (Label)e.Item.FindControl("lbleid");
                HyperLink hypbuynow = (HyperLink)e.Item.FindControl("hypbuynow");
                Label lblimagelink = (Label)e.Item.FindControl("lblimagelink");
                

                imgEvent.ImageUrl = sitetemp.GetEventPic(Event_Key.ToString());                
                
                bool IsProduct= sitetemp.Isproduct(Event_Key);
                if (IsProduct)
                {
                    pnldate.Visible = false;
                    hypEventName.NavigateUrl = hypEventName.NavigateUrl.Replace("Order_Form.aspx", "Order_Form_product.aspx");
                }                
                                
                if ((sitetemp.IsSoldOutEvent(Event_Key)) && (IsProduct == false))
                {                    
                    hypEventName.Text += " - Sold Out";
                }

                hypEventName.NavigateUrl += Event_Key.ToString(); //lblEventKey.Text;
                //check if is a promoted event
                if (lbleventtype.Text == "1") //it's a promoted event
                {
                    hypEventName.NavigateUrl = "http://www.facebook.com/event.php?eid=" + lbleid.Text;
                    imgEvent.ImageUrl = sitetemp.getgraphimg(lbleid.Text);
                    hypbuynow.Visible = false;
                }
                hypEventText.NavigateUrl = hypEventName.NavigateUrl;
                hypbuynow.NavigateUrl = hypEventName.NavigateUrl;
                lblimagelink.Text = "<a href='"+ hypEventName.NavigateUrl.Replace("~/","") + "'>";                

                //Put in Calendar info
                System.Web.UI.WebControls.Label lblMonth = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblMonth");
                System.Web.UI.WebControls.Label lblDay = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblDay");
                System.Web.UI.WebControls.Label lblDayofWeek = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblDayofWeek");
                System.Web.UI.WebControls.Label lblBegins = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblEvent_Begins");

                DateTime dtBegins = Convert.ToDateTime(lblBegins.Text);

                lblMonth.Text = dtBegins.ToString("MMM");
                lblDay.Text = dtBegins.ToString("dd");
                lblDayofWeek.Text = dtBegins.ToString("ddd");
                
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            //save changes
            Eventomatic_DB.SPs.UpdateStore(Resource_Key, txtStoreTitle.Text, txtStoreDescription.Text, txtContact.Text).Execute();

            //save image
            if (!Thumbnail.ImageUrl.Contains("Images/Groups"))
            {
                string file = Server.MapPath("/Images/Groups/") + "\\" + Resource_Key.ToString() + ".jpg";
                //AsyncUpload1.UploadedFiles[0].SaveAs(file);

                string fullurl = Request.Url.AbsoluteUri;
                string saveimgurl =  fullurl.Substring(0,fullurl.IndexOf("Store.aspx")) + Thumbnail.ImageUrl.Replace("~/", "");

                Site sitetemp = new Site();
                sitetemp.savepicurl2(saveimgurl, file);
            }                  
      
            //save Other groups            
            Eventomatic_DB.SPs.DeleteResourceReadingOthers(Resource_Key).Execute();

            foreach (RadListBoxItem rdlb in lbCurrentDisplaying.Items)
            {
                if (rdlb.Value != Resource_Key.ToString())
                {
                    Eventomatic_DB.SPs.UpdateResourceReadingOthers(Resource_Key, Convert.ToInt32(rdlb.Value), Convert.ToInt64(hdnfbid.Value)).Execute();
                }                
            }

            //reload events
            DataSet dstemp = Eventomatic_DB.SPs.ViewStoreWithSelectedGroups(Resource_Key).GetDataSet();

            RadListView1.DataSource = dstemp.Tables[0];
            RadListView1.DataBind();
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            //leave edit mode  cancel
            Response.Redirect("Store.aspx?storeid=" + Resource_Key.ToString());
        }

        protected void LoadListboxes()
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewStoresInNetwork(Resource_Key).GetDataSet();

            lbOthergroupstores.DataSource = dstemp.Tables[0];
            lbOthergroupstores.DataTextField = "Group_Name";
            lbOthergroupstores.DataValueField = "Resource_Key";
            lbOthergroupstores.DataBind();

            dstemp = Eventomatic_DB.SPs.ViewStoresCurrentlyDisplaying(Resource_Key).GetDataSet();
            
            lbCurrentDisplaying.DataSource = dstemp.Tables[0];
            lbCurrentDisplaying.DataTextField = "Group_Name_Reading";
            lbCurrentDisplaying.DataValueField = "Resource_Key_Reading";
            lbCurrentDisplaying.DataBind();

            RadListBoxItem rdlb = new RadListBoxItem();
            rdlb.Value = Resource_Key.ToString() ;
            rdlb.Text = "This Groupstore";
            rdlb.Enabled = false;
            rdlb.Checkable = false;
            lbCurrentDisplaying.Items.Insert(0, rdlb);

            if ((lbOthergroupstores.Items.Count == 0) && (lbCurrentDisplaying.Items.Count == 1))
            {
                pnlOthergroups.Visible = false;
            }
        }

        protected void lbOthergroupstores_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            /*DataSet dstemp = Eventomatic_DB.SPs.ViewStoreWithSelectedGroups(Resource_Key).GetDataSet();

            RadListView1.DataSource = dstemp.Tables[0];
            RadListView1.DataBind();*/
        }

        protected void lbCurrentDisplaying_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            /*DataSet dstemp = Eventomatic_DB.SPs.ViewStoreWithSelectedGroups(Resource_Key).GetDataSet();

            RadListView1.DataSource = dstemp.Tables[0];
            RadListView1.DataBind();*/
        }
    }
}
