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
using SubSonic;
//using facebook.Schema;
using Eventomatic.Addons;
using Telerik.Web.UI;

namespace Eventomatic
{
    public partial class Group_Members : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblErrorNoName.Visible = false;
            lblErrorNoName2.Visible = false;
            if (Request.Form["__EVENTTARGET"] == "btnremovegroup")
            {
                btnremovegroup_Click();
            }
            if (!IsPostBack)
            {
                FillData();
            }

        }

        protected void FillData()
        {
            RadComboBox ddlgroups = new RadComboBox();
            ddlgroups = (RadComboBox)Master.LoadGroupsList();
            DataSet dsCurrentEvents = Eventomatic_DB.SPs.ViewListGroupMembers(Convert.ToInt32(ddlgroups.SelectedValue.ToString())).GetDataSet();
            //UltraWebGrid1.DataSource = dsCurrentEvents.Tables[0];
            //UltraWebGrid1.DataBind();
            lbAdmins.DataSource = dsCurrentEvents.Tables[0];
            lbAdmins.DataTextField = "Full_Name";
            lbAdmins.DataValueField = "FBid";
            lbAdmins.DataBind();


            DataTable dtFriendsList = Master.getFriendslist(Master.getfbuser());
            ListItem litemp = new ListItem();
            for(int i = 0;i<dtFriendsList.Rows.Count;i++)
            {            
                litemp.Value = dtFriendsList.Rows[i]["fbid"].ToString();
                litemp.Text = dtFriendsList.Rows[i]["Name"].ToString();
                if (lbAdmins.Items.Contains(litemp))
                {
                    dtFriendsList.Rows.Remove(dtFriendsList.Rows[i]);
                    i += 1;
                }
            }
            //IList Friends = (IList)Master.API.friends.getLists();
            /*UltraWebGrid2.DisplayLayout.Bands[0].Columns[0].ValueList.DataSource = dtFriendsList;
            UltraWebGrid2.DisplayLayout.Bands[0].Columns[0].ValueList.DisplayMember = "Name";
            UltraWebGrid2.DisplayLayout.Bands[0].Columns[0].ValueList.DataSourceID = "fbid";
            UltraWebGrid2.DisplayLayout.Bands[0].Columns[0].ValueList.DataBind();*/
            lbFriendsList.DataSource = dtFriendsList;
            lbFriendsList.DataTextField = "Name";
            lbFriendsList.DataValueField = "fbid";
            lbFriendsList.DataBind();

            RadComboBox ddlGroupList = Master.LoadGroupsList();
            lblGroupName.Text = ddlGroupList.SelectedItem.Text + " Administrators";

            Fill_YourGroups();
        }

        

        protected void Fill_YourGroups()
        {
            //DataTable dttemp = Eventomatic_DB.SPs.ViewListFBUserResources(Convert.ToInt64(Master.API.users.getLoggedInUser())).GetDataSet().Tables[0];
            DataTable dttemp = Eventomatic_DB.SPs.ViewListFBUserResources(Convert.ToInt64(Master.getfbid())).GetDataSet().Tables[0];
            lbYourGroups.DataSource = dttemp;
            lbYourGroups.DataTextField = "Group_Name";
            lbYourGroups.DataValueField = "Resource_Key";
            lbYourGroups.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Move data from dropdown to listbox
            ListItem litemp = new ListItem();
            litemp.Value = lbFriendsList.SelectedItem.Value;
            litemp.Text = lbFriendsList.SelectedItem.Text;
            lbAdmins.Items.Add(litemp);
            fbuser friends = Master.getfbuserinfo(Convert.ToInt64(lbFriendsList.SelectedValue));
            RadComboBox ddlGroupList = Master.LoadGroupsList();

            //Eventomatic_DB.SPs.UpdateResource(Convert.ToInt32(friends[0].uid), friends[0].first_name, friends[0].last_name, "").Execute();
            Eventomatic_DB.SPs.UpdateFbUsersResource(friends.UID, Convert.ToInt32(ddlGroupList.SelectedValue), friends.Firstname, friends.Lastname).Execute();
            
            //Record Activity
            Eventomatic.Addons.Activities activity = new Activities();
            activity.NewActivity(1, Convert.ToInt32(ddlGroupList.SelectedValue), Convert.ToInt64(Master.getfbid()), friends.UID,0,0);

            lbFriendsList.Items.RemoveAt(lbFriendsList.SelectedIndex);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {            
            //Move data from listbox to dropdown
            fbuser friends = Master.getfbuserinfo(Convert.ToInt64(lbAdmins.SelectedValue));
            RadComboBox ddlGroupList = Master.LoadGroupsList();
            if (friends.UID.ToString() != Master.getfbid())
            {
                ListItem litemp = new ListItem();
                litemp.Value = lbAdmins.SelectedItem.Value;
                litemp.Text = lbAdmins.SelectedItem.Text;
                lbFriendsList.Items.Add(litemp);

                Eventomatic_DB.SPs.DeleteFbUsersResource(friends.UID, Convert.ToInt32(ddlGroupList.SelectedValue)).Execute();
                lbAdmins.Items.RemoveAt(lbAdmins.SelectedIndex);

                //Record Activity
                Eventomatic.Addons.Activities activity = new Activities();
                activity.NewActivity(6, Convert.ToInt32(ddlGroupList.SelectedValue), Convert.ToInt64(Master.getfbid()), friends.UID,0,0);
                
            }
            else
            {
                lblError.Visible = true;
            }
            
        }

        protected void btnaddgroup_Click(object sender, EventArgs e)
        {
            //Adding New Group
            if (txtNewGroup.Text != "")
            {
                //Check to make sure group does not already Exist
                Site sitetemp = new Site();
                bool IsUnique = sitetemp.IsUniqueGroupStore(txtNewGroup.Text);                

                if (IsUnique)
                {

                    StoredProcedure sp_AddGroup = Eventomatic_DB.SPs.UpdateGroups(Convert.ToInt64(Master.getfbid()), 0, txtNewGroup.Text,
                        Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFP").ToString()),
                        Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFC").ToString()),
                        Convert.ToDecimal(System.Configuration.ConfigurationSettings.AppSettings.Get("Default_SFM").ToString()),
                        0);
                    sp_AddGroup.Execute();

                    ListItem litemp = new ListItem();
                    litemp.Value = sp_AddGroup.Command.Parameters[6].ParameterValue.ToString();
                    litemp.Text = txtNewGroup.Text;
                    lbYourGroups.Items.Add(litemp);

                    lblErrorNoName2.Text = "The Group has been created";
                    lblErrorNoName2.ForeColor = System.Drawing.Color.Blue;
                    lblErrorNoName2.Visible = true;

                    Master.LoadGroupsList();
                }
                else
                {
                    lblErrorNoName2.Text = "Unfortunately the Group Name already Exists. Please select another name.";
                    lblErrorNoName2.Visible = true;                    
                }
                
            }
            else
            {
                lblErrorNoName2.Visible = true;
                lblErrorNoName2.Text = "*Require Name";
            }
        }

        protected void btnremovegroup_Click()
        {
            //Remove Group
            /*var friends = Master.API.users.getInfo(Convert.ToInt64(lbAdmins.SelectedValue));
            DropDownList ddlGroupList = Master.LoadGroupsList();
            if (lbYourGroups.SelectedValue != ddlGroupList.SelectedValue)
            {
                Eventomatic_DB.SPs.DeleteGroups(Convert.ToInt32(lbYourGroups.SelectedValue)).Execute();
                Master.LoadGroupsList();

                lbYourGroups.Items.RemoveAt(lbYourGroups.SelectedIndex);

                lblErrorNoName.Visible = true;
                lblErrorNoName.ForeColor = System.Drawing.Color.Blue;
                lblErrorNoName.Text = "The Group has been Removed.";
            }
            else
            {
                lblErrorNoName.Visible = true;
                lblErrorNoName.Text = "*Must log into another group to remove this one.";
            }*/
        }
    }
}
