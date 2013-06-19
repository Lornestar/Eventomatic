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

namespace Eventomatic.Admin
{
    public partial class Admins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            if (!IsPostBack)
            {
                FillData();
            }
        }
        
        protected void FillData()
        {
            lbAdmins.DataSource = Eventomatic_DB.SPs.ViewAdmins(1).GetDataSet().Tables[0];
            lbAdmins.DataTextField = "Full_Name";
            lbAdmins.DataValueField = "FBid";
            lbAdmins.DataBind();

            lbUsersList.DataSource = Eventomatic_DB.SPs.ViewAdmins(0).GetDataSet().Tables[0];
            lbUsersList.DataTextField = "Full_Name";
            lbUsersList.DataValueField = "FBid";
            lbUsersList.DataBind();

            Hashtable htemp = new Hashtable();
            foreach (ListItem litemp in lbUsersList.Items)
            {
                if (lbAdmins.Items.Contains(litemp))
                {
                    
                    htemp.Add(litemp.Value, litemp.Text);
                }                
            }

            ListItem litemp2 = new ListItem();
            foreach (DictionaryEntry de in htemp)
            {
                litemp2.Value = de.Key.ToString();
                litemp2.Text = de.Value.ToString();
                lbUsersList.Items.Remove(litemp2);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {            
            Eventomatic_DB.SPs.UpdateAdmins(Convert.ToInt64(lbUsersList.SelectedValue)).Execute();
            lbAdmins.Items.Add(lbUsersList.SelectedItem);
            lbUsersList.Items.Remove(lbUsersList.SelectedItem);
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            //if (Master.API.users.getLoggedInUser().ToString() != lbAdmins.SelectedValue)
            if (Master.getfbid() != lbAdmins.SelectedValue)
            {
                Eventomatic_DB.SPs.DeleteAdmins(Convert.ToInt64(lbAdmins.SelectedValue)).Execute();
                lbUsersList.Items.Add(lbAdmins.SelectedItem);
                lbAdmins.Items.Remove(lbAdmins.SelectedItem);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "You cannot remove yourself from Admins"; 
            }
        }
    }
}
