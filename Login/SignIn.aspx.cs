using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Eventomatic.Addons;

namespace Eventomatic.Login
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsignin_Click(object sender, EventArgs e)
        {
            //Check login credetials
            bool validcredentials = false;

            DataSet dstemp = Eventomatic_DB.SPs.ViewFBUserEmail(txtemail.Text).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                if (dstemp.Tables[0].Rows[0]["Password"] != DBNull.Value)
                {
                    if (dstemp.Tables[0].Rows[0]["Password"].ToString() == txtpwd.Text)
                    {
                        validcredentials = true;
                    }
                }
            }

            if (validcredentials)
            {
                //log them in
                fbuser fbuser = new fbuser();
                fbuser.UID = Convert.ToInt64(dstemp.Tables[0].Rows[0]["FBid"].ToString());
                fbuser.Email = txtemail.Text;
                if (dstemp.Tables[0].Rows[0]["First_Name"] != DBNull.Value)
                {
                    fbuser.Firstname = dstemp.Tables[0].Rows[0]["First_Name"].ToString();
                }
                if (dstemp.Tables[0].Rows[0]["Last_Name"] != DBNull.Value)
                {
                    fbuser.Lastname = dstemp.Tables[0].Rows[0]["Last_Name"].ToString();
                }
                fbuser.Fullname = fbuser.Firstname + " " + fbuser.Lastname;
                Session["fbuser"] = fbuser;
                Response.Redirect("Settings.aspx");
            }
            else
            {
                //give error msg
                lblerror.Visible = true;
            }
        }

        protected void btnforgotpwd_Click(object sender, EventArgs e)
        {
            //send password
            

            DataSet dstemp = Eventomatic_DB.SPs.ViewFBUserEmail(txtforgotpwd.Text).GetDataSet();
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                Send_Email se = new Send_Email();
                string strbody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/sp_Forgotpwd.txt"));                

                if (dstemp.Tables[0].Rows[0]["Password"] != DBNull.Value)
                {
                    strbody = strbody.Replace("PWD", dstemp.Tables[0].Rows[0]["Password"].ToString());                    
                }
                if (dstemp.Tables[0].Rows[0]["First_Name"] != DBNull.Value)
                {
                    strbody = strbody.Replace("NAME", dstemp.Tables[0].Rows[0]["First_Name"].ToString());
                }
                else
                {
                    strbody = strbody.Replace("NAME", txtforgotpwd.Text);
                }

                se.Send_Email_Simple("forgotpwd@snap-pay.com", txtforgotpwd.Text, "Your Snappay Password", strbody);
                lblforgotpwdsent.Visible = true;
            }
            else
            {
                //email doesn't exist
                lblforgotpwdsent.Text = "This email has not signed up for Snappay.";
            }

            
        }
    }
}