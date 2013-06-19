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
//using System.Web.Mail;
using System.Net.Mail;
using System.IO;
using System.Collections;

namespace Eventomatic
{
    public class Send_Email
    {
        public void Send_Email_Function(string From, string To, string Subject, string Body, int Tx_Key)
        {
            string EmailFrom;
            if (From == "")// put the from address here
            {
                EmailFrom = System.Configuration.ConfigurationSettings.AppSettings.Get("Default_Email_From").ToString();
            }
            else
            {
                EmailFrom = From; 
            }
            MailMessage mail = new MailMessage(EmailFrom, To);
            Hashtable hstemp = new Hashtable();
            Eventomatic.Addons.PDFReceipt2 pdftemp = new Eventomatic.Addons.PDFReceipt2();
            hstemp = pdftemp.CreatePDFReceipt(Tx_Key);
            foreach (DictionaryEntry de in hstemp)
            {
                Attachment att = new Attachment(de.Value.ToString());
                mail.Attachments.Add(att);
            }
            
            //Check if its a product or hotel or lessons
            if (IsProduct(Tx_Key))
            {
                //add resource's email address
                Site sitetemp = new Site();
                string strbccemail = sitetemp.GetResourceEmailTx(Tx_Key);
                mail.Bcc.Add(strbccemail);
            }

            //mail.To = To;             // put to address here
            mail.Subject = Subject;        // put subject here	
            mail.Body = Body;           // put body of email here            
            //SmtpMail.SmtpServer = "localhost"; // put smtp server you will use here 
            // and then send the mail
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Send(mail);
            }
            catch
            {
            }                        
        
        }


        public void Send_Transaction_Email(int Tx_Key,string PDFReceipt)
        {
            Site sitetemp = new Site();
            bool ispayforward = sitetemp.isPayForward(Tx_Key);

            Hashtable hstemp = new Hashtable();
            if (ispayforward)
            {
                hstemp = GetPurchasedPayForwardinfo(Tx_Key);
            }
            else
            {
                hstemp = Getpurchasedtixinfo(Tx_Key, PDFReceipt);
            }
            
            //Send Confirmation email to Buyer
            string strEmailFrom = System.Configuration.ConfigurationSettings.AppSettings.Get("Default_Email_From").ToString();
            try
            {
                Send_Email_Function(strEmailFrom, hstemp["strpayer_email"].ToString(), "Snappay Purchase Confirmation", hstemp["thebody"].ToString(), Tx_Key);
            }
            catch
            {
            }

            Send_Ticket_SellerEmail(Tx_Key);
        }


        public Hashtable GetPurchasedPayForwardinfo(int Tx_Key)
        {
            Site sitetemp = new Site();
            string thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/PayForward_Reciept.txt"));
            DataSet dspf = Eventomatic_DB.SPs.PfViewTransactionDetailsTxkey(Tx_Key).GetDataSet();

            if (dspf.Tables[0].Rows[0]["Latitude"] != DBNull.Value)
            {
                thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/PayForward_Receipt2.txt"));
            }

            string theNote = "";
            string strpayer_email = "";
            string TxTime = "";
            string AmountPaid = "";
            string FirstName = "";
            string LastName = "";
            string strtxn_id = "";
            string strconfirmation = "";
            string strLatitude = "";
            string strLongitude = "";

            if (dspf.Tables[0].Rows[0]["Note"] != DBNull.Value)
            {
                theNote = dspf.Tables[0].Rows[0]["Note"].ToString();
            }
            if (dspf.Tables[0].Rows[0]["payer_email"] != DBNull.Value)
            { strpayer_email = dspf.Tables[0].Rows[0]["payer_email"].ToString(); }
            if (dspf.Tables[0].Rows[0]["Confirmation_Date"] != DBNull.Value)
            { TxTime = dspf.Tables[0].Rows[0]["Confirmation_Date"].ToString(); }
            if (dspf.Tables[0].Rows[0]["amount"] != DBNull.Value)
            { AmountPaid = dspf.Tables[0].Rows[0]["amount"].ToString(); }
            if (dspf.Tables[0].Rows[0]["first_name"] != DBNull.Value)
            { FirstName = dspf.Tables[0].Rows[0]["first_name"].ToString(); }
            if (dspf.Tables[0].Rows[0]["last_name"] != DBNull.Value)
            { LastName = dspf.Tables[0].Rows[0]["last_name"].ToString(); }
            if (dspf.Tables[0].Rows[0]["txn_id"] != DBNull.Value)
            { strtxn_id = dspf.Tables[0].Rows[0]["txn_id"].ToString(); }
            if (dspf.Tables[0].Rows[0]["Confirmation_Text"] != DBNull.Value)
            { strconfirmation = dspf.Tables[0].Rows[0]["Confirmation_Text"].ToString(); }
            if (dspf.Tables[0].Rows[0]["Latitude"] != DBNull.Value)
            { strLatitude = dspf.Tables[0].Rows[0]["Latitude"].ToString(); }
            if (dspf.Tables[0].Rows[0]["Longitude"] != DBNull.Value)
            { strLongitude = dspf.Tables[0].Rows[0]["Longitude"].ToString(); }

            if (dspf.Tables[0].Rows[0]["Latitude"] != DBNull.Value)
            {
                string googleurl = "http://maps.google.com/maps?q=" +sitetemp.getgooglemapsaddress(strLatitude, strLongitude) +"&amp;z=12";
                string googleimg = "https://maps-api-ssl.google.com/maps/api/staticmap?center=" + strLatitude + "%2C" + strLongitude + "&amp;zoom=12&amp;size=1125x400&amp;&amp;sensor=false";
                thebody = thebody.Replace("GOOGLEIMG", googleimg);
                thebody = thebody.Replace("GOOGLE", googleurl);                
            }

            thebody = thebody.Replace("THENOTE", theNote);
            thebody = thebody.Replace("TXTIME", TxTime);
            thebody = thebody.Replace("AMOUNTPAID", "$" + AmountPaid);
            thebody = thebody.Replace("FIRSTNAME", FirstName);
            thebody = thebody.Replace("LASTNAME", LastName);
            thebody = thebody.Replace("TXID", strtxn_id);
            thebody = thebody.Replace("CONFIRMATIONTEXT", strconfirmation);
            
            
            Hashtable emailinfo = new Hashtable();
            emailinfo.Add("strpayer_email", strpayer_email);
            emailinfo.Add("thebody", thebody);
            return emailinfo;
        }

        public void Send_Ticket_SellerEmail(int Tx_Key)
        {
            //Send email to seller
            DataSet dstemp = Eventomatic_DB.SPs.ViewTransactionSellerEmail(Tx_Key).GetDataSet();
            if (dstemp.Tables[0].Rows[0]["fbid_Seller"] != DBNull.Value)
            {
                //There is a seller
                string sellername = "";
                string buyername = "";
                string selleremail = "";
                string eventname = "";
                string accesstoken = "";
                bool FacebookWall = false;
                string FacebookWallInfo = "News of this sale will be not be posted on your Facebook Wall. <a href='http://www.thegroupstore.com/MobileFacebookSettings.aspx?fbid="+ dstemp.Tables[0].Rows[0]["fbid_Seller"].ToString() +"'>Click here </a> to post all future sales on your Facebook Wall.";
                string thebody = "";
                string fbid = "";

                if (dstemp.Tables[0].Rows[0]["Event_Name"] != DBNull.Value)
                {
                    eventname = dstemp.Tables[0].Rows[0]["Event_Name"].ToString();
                }
                if (dstemp.Tables[0].Rows[0]["Seller_OktoPostFacebook"] != DBNull.Value)
                {
                    FacebookWall = Convert.ToBoolean(dstemp.Tables[0].Rows[0]["Seller_OktoPostFacebook"]);
                }
                if (dstemp.Tables[0].Rows[0]["Seller_AccessToken"] != DBNull.Value)
                {
                    accesstoken = dstemp.Tables[0].Rows[0]["Seller_AccessToken"].ToString();
                }
                if (dstemp.Tables[0].Rows[0]["Seller_FullName"] != DBNull.Value)
                {
                    sellername = dstemp.Tables[0].Rows[0]["Seller_FullName"].ToString();
                }
                if (dstemp.Tables[0].Rows[0]["Seller_Email"] != DBNull.Value)
                {
                    selleremail = dstemp.Tables[0].Rows[0]["Seller_Email"].ToString();
                }
                if (dstemp.Tables[0].Rows[0]["Buyer_FullName"] != DBNull.Value)
                {
                     buyername= dstemp.Tables[0].Rows[0]["Buyer_FullName"].ToString();
                }                
                if (dstemp.Tables[0].Rows[0]["fbid_Seller"] != DBNull.Value)
                {
                     fbid = dstemp.Tables[0].Rows[0]["fbid_Seller"].ToString();
                }                

                thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/Sold_Ticket.txt"));
                thebody.Replace("SELLERFULLNAME",sellername);
                thebody.Replace("BUYERFULLNAME",buyername);
                thebody.Replace("EVENTNAME", eventname);
                if (FacebookWall)
                {
                    FacebookWallInfo = FacebookWallInfo = "News of this sale will be posted on your Facebook Wall. <a href='http://www.thegroupstore.com/MobileFacebookSettings.aspx?fbid="+ dstemp.Tables[0].Rows[0]["fbid_Seller"].ToString() +"'>Click here </a> to not post all future sales on your Facebook Wall.";
                }   
                thebody.Replace("FACEBOOKWALLINFO",FacebookWallInfo);

                //Send the email
                if (selleremail != "")
                {
                    Send_Email_Function("", selleremail, "Completed Groupstore Ticket Sale", thebody, Tx_Key);
                }                

                //Post story of news on Seller's wall
                if (FacebookWall)
                {
                    Site sitetemp = new Site();
                    string eventkey = "";
                    string description = "";
                    if (dstemp.Tables[0].Rows[0]["Event_Key"] != DBNull.Value)
                    {
                         eventkey = dstemp.Tables[0].Rows[0]["Event_Key"].ToString();
                    }
                    if (dstemp.Tables[0].Rows[0]["Event_Description"] != DBNull.Value)
                    {
                        description = dstemp.Tables[0].Rows[0]["Event_Description"].ToString();
                    }
                    string eventlink = ConfigurationSettings.AppSettings.Get("Store_URL").ToString() + "Order_Form.aspx?Event_Key="+eventkey;
                    string themessage = "Click here to start sell tickets and get credit, just like " + sellername;
                    string strCaption = sellername + " sold "+ eventname + " tickets to " + buyername;
                    string eventpic = "http://www.thegroupstore.com/"+ sitetemp.GetEventPic(eventkey);
                    
                    

                    sitetemp.Facebook_PostLink_OnWall(fbid, eventlink, themessage, eventpic, strCaption, accesstoken, strCaption, description.Substring(0,500));
                }
                
            }            
        }


        public Hashtable Getpurchasedtixinfo(int Tx_Key, string PDFReceipt)
        {
            DataSet dstemp = Eventomatic_DB.SPs.ViewEmailReceipt(Tx_Key).GetDataSet();
            DataSet dstemp2 = Eventomatic_DB.SPs.ViewTicketForEmail(Tx_Key).GetDataSet();
            string EventName = "";
            DateTime tempdatetime;
            string EventBeginsDate = "";
            string EventBeginTime = "";
            string EventEndDate = "";
            string EventEndTime = "";
            string TxTime = "";
            string AmountPaid = "";
            string FirstName = "";
            string LastName = "";
            string Quantity = "";
            int QuantityNum = 0;
            string Host = "";
            string Location = "";
            string Street = "";
            string City = "";
            string Phone = "";
            string Email = "";
            string AdditionalComments = "";
            string TicketNumber = "";
            string TicketPurchased_Details = "";
            string GroupName = "";
            string Resource_Key = "";
            string strtxn_id = "";
            string Event_Key = "";
            string strpayer_email = "";
            string strConfirmation = "";

            if (dstemp.Tables[0].Rows[0]["Event_Name"] != DBNull.Value)
            { EventName = dstemp.Tables[0].Rows[0]["Event_Name"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Confirmation_Date"] != DBNull.Value)
            { TxTime = dstemp.Tables[0].Rows[0]["Confirmation_Date"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Amount2Dec"] != DBNull.Value)
            { AmountPaid = dstemp.Tables[0].Rows[0]["Amount2Dec"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["first_name"] != DBNull.Value)
            { FirstName = dstemp.Tables[0].Rows[0]["first_name"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["last_name"] != DBNull.Value)
            { LastName = dstemp.Tables[0].Rows[0]["last_name"].ToString(); }
            //if (dstemp.Tables[0].Rows[0]["Quantity"] != DBNull.Value)
            //{Quantity = dstemp.Tables[0].Rows[0]["Quantity"].ToString();}
            if (dstemp.Tables[0].Rows[0]["Host"] != DBNull.Value)
            { Host = dstemp.Tables[0].Rows[0]["Host"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Location"] != DBNull.Value)
            { Location = dstemp.Tables[0].Rows[0]["Location"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Street"] != DBNull.Value)
            { Street = dstemp.Tables[0].Rows[0]["Street"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["City"] != DBNull.Value)
            { City = dstemp.Tables[0].Rows[0]["City"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Phone"] != DBNull.Value)
            { Phone = dstemp.Tables[0].Rows[0]["Phone"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Email"] != DBNull.Value)
            { Email = dstemp.Tables[0].Rows[0]["Email"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Additional_Comments"] != DBNull.Value)
            { AdditionalComments = dstemp.Tables[0].Rows[0]["Additional_Comments"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Ticket_Number"] != DBNull.Value)
            { TicketNumber = dstemp.Tables[0].Rows[0]["Ticket_Number"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Group_Name"] != DBNull.Value)
            { GroupName = dstemp.Tables[0].Rows[0]["Group_Name"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Resource_Key"] != DBNull.Value)
            { Resource_Key = dstemp.Tables[0].Rows[0]["Resource_Key"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["txn_id"] != DBNull.Value)
            { strtxn_id = dstemp.Tables[0].Rows[0]["txn_id"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Event_Key"] != DBNull.Value)
            { Event_Key = dstemp.Tables[0].Rows[0]["Event_Key"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["payer_email"] != DBNull.Value)
            { strpayer_email = dstemp.Tables[0].Rows[0]["payer_email"].ToString(); }
            if (dstemp.Tables[0].Rows[0]["Confirmation"] != DBNull.Value)
            { strConfirmation = dstemp.Tables[0].Rows[0]["Confirmation"].ToString(); }



            tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Event_Begins"].ToString());
            EventBeginsDate = tempdatetime.ToString("dddd, MMMM d yyyy");
            EventBeginTime = "";// tempdatetime.TimeOfDay.ToString();

            tempdatetime = Convert.ToDateTime(dstemp.Tables[0].Rows[0]["Event_Ends"].ToString());
            EventEndDate = tempdatetime.ToString("dddd, MMMM d yyyy");
            EventEndTime = "";//tempdatetime.TimeOfDay.ToString();

            if (dstemp2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dstemp2.Tables[0].Rows)
                {
                    if (TicketPurchased_Details != "")
                    { TicketPurchased_Details += ","; }
                    if (r["Purchase_Description"] != DBNull.Value)
                    { TicketPurchased_Details += r["Purchase_Description"].ToString(); }
                    if (r["Quantity"] != DBNull.Value)
                    { QuantityNum += Convert.ToInt32(r["Quantity"]); }

                }
            }
            Quantity = QuantityNum.ToString();

            //Check if its a donation event
            bool TicketEvent = true;  //true = regular ticket event  false = donation event
            if (dstemp.Tables[0].Rows[0]["Donation"] != DBNull.Value)
            {
                if (dstemp.Tables[0].Rows[0]["Donation"].ToString() == "True")
                {
                    TicketEvent = false;
                }
            }

            //Check if fundraiser  - Fundraiser includes tax receipt, donation does
            DataSet dsisfundraiser = Eventomatic_DB.SPs.ViewIsFundraiser(Convert.ToInt32(Event_Key)).GetDataSet();
            bool isfundraiser = false;
            if (dsisfundraiser.Tables[0].Rows[0]["IsFundraiser"] != DBNull.Value)
            {
                if (dsisfundraiser.Tables[0].Rows[0]["IsFundraiser"].ToString() == "True")
                {
                    isfundraiser = true;
                }
            }

            
            string thebody = "";

            if (isfundraiser) //send tax receipt email
            {
                thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/Fundraiser_Reciept.txt"));
                DataSet dsEventFundraiserPDF = Eventomatic_DB.SPs.ViewEventsFundraiserPdf(Tx_Key).GetDataSet();
                if (dsEventFundraiserPDF.Tables[0].Rows.Count > 0)
                {
                    if (dsEventFundraiserPDF.Tables[0].Rows[0]["Email_Message"] != DBNull.Value)
                    {
                        strConfirmation = dsEventFundraiserPDF.Tables[0].Rows[0]["Email_Message"].ToString();
                        strConfirmation = strConfirmation.Replace(new String((char)13, 1), "<br>");
                        strConfirmation = strConfirmation.Replace("&cr", "<br>");
                    }
                }
            }            
            else if (IsProduct(Tx_Key))
            {
                thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/Product_Reciept.txt"));
            }
            else
            {
                if (TicketEvent)
                {
                    thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/Ticket_Reciept.txt"));
                }
                else
                {
                    thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/Donation_Reciept.txt"));
                }
            }


            thebody = thebody.Replace("CONFIRMATIONTEXT", strConfirmation);
            thebody = thebody.Replace("EVENTNAME", EventName);
            thebody = thebody.Replace("TXTIME", TxTime);
            thebody = thebody.Replace("AMOUNTPAID", "$" + AmountPaid);
            thebody = thebody.Replace("FIRSTNAME", FirstName);
            thebody = thebody.Replace("LASTNAME", LastName);
            thebody = thebody.Replace("QUANTITYPAID", Quantity);
            thebody = thebody.Replace("HOST", Host);
            thebody = thebody.Replace("LOCATION", Location);
            thebody = thebody.Replace("STREET", Street);
            thebody = thebody.Replace("CITY", City);
            thebody = thebody.Replace("PHONE", Phone);
            thebody = thebody.Replace("EMAIL", Email);
            thebody = thebody.Replace("ADDITIONALCOMMENTS", AdditionalComments);
            thebody = thebody.Replace("TICKETNUMBER", TicketNumber);
            thebody = thebody.Replace("EVENTBEGINDATE", EventBeginsDate);
            thebody = thebody.Replace("EVENTBEGINTIME", EventBeginTime);
            thebody = thebody.Replace("EVENTENDDATE", EventEndDate);
            thebody = thebody.Replace("EVENTENDTIME", EventEndTime);
            thebody = thebody.Replace("TICKETSPURCHASEDDETAILS", TicketPurchased_Details);
            thebody = thebody.Replace("GROUPNAME", GroupName);
            thebody = thebody.Replace("TXID", strtxn_id);
            Site Sitetemp = new Site();
            string Rooturl = System.Configuration.ConfigurationSettings.AppSettings.Get("Callback").ToString();
            thebody = thebody.Replace("EVENTPIC", Rooturl + Sitetemp.GetEventPic(Event_Key));
            thebody = thebody.Replace("GROUPPIC", Rooturl + Sitetemp.GetResourcePic(Resource_Key));


            Hashtable emailinfo = new Hashtable();
            emailinfo.Add("strpayer_email", strpayer_email);
            emailinfo.Add("thebody", thebody);
            return emailinfo;
        }

        public void Refer_Friend_Email(string FriendEmail,Int64 fbid)
        {
            string thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/Refer_Friend_Email.txt"));
            DataSet dstemp = Eventomatic_DB.SPs.ViewFbUsers(fbid).GetDataSet();

            string fullname = "";
            string fbemail = "";
            if (dstemp.Tables[0].Rows.Count > 0)
            {
                fullname = dstemp.Tables[0].Rows[0]["First_Name"].ToString() + " " + dstemp.Tables[0].Rows[0]["Last_Name"].ToString();
                if (dstemp.Tables[0].Rows[0]["Referral_Email"] != DBNull.Value)
                {
                    fbemail = dstemp.Tables[0].Rows[0]["Referral_Email"].ToString();
                }
            }
            thebody = thebody.Replace("FBUSER_NAME", fullname);
            thebody = thebody.Replace("FBID", fbid.ToString());
            Send_Email_Function("Referral@Snap-Pay.com", FriendEmail+","+fbemail, "Snappay Referral", thebody, 0);
        }

        protected bool IsProduct(int tx_key)
        {
            //Check if its a product or hotel or lessons
            bool IsProduct = false;
            DataSet dsisproduct = Eventomatic_DB.SPs.ViewIsProducttxkey(tx_key).GetDataSet();
            foreach (DataRow r in dsisproduct.Tables[0].Rows)
            {
                if (r["Type"] != DBNull.Value)
                {
                    if (r["Type"].ToString() == "1")
                    {
                        IsProduct = true;
                    }
                }
            }
            return IsProduct;
        }


        public void Send_Email_Function2(string From, string To, string Subject, string Body, string[] hsbcc, string[] hscc, int Tx_Key)
        {
            string EmailFrom;
            if (From == "")// put the from address here
            {
                EmailFrom = System.Configuration.ConfigurationSettings.AppSettings.Get("Default_Email_From").ToString();
            }
            else
            {
                EmailFrom = From;
            }
            MailMessage mail = new MailMessage(EmailFrom, To);            

            //mail.To = To;             // put to address here
            mail.Subject = Subject;        // put subject here	
            mail.Body = Body;           // put body of email here            
            //SmtpMail.SmtpServer = "localhost"; // put smtp server you will use here 
            // and then send the mail
            mail.IsBodyHtml = true;
            
            Hashtable hstemp = new Hashtable();
            Eventomatic.Addons.PDFReceipt2 pdftemp = new Eventomatic.Addons.PDFReceipt2();
            hstemp = pdftemp.CreatePDFReceipt(Tx_Key);
            foreach (DictionaryEntry de in hstemp)
            {
                Attachment att = new Attachment(de.Value.ToString());
                mail.Attachments.Add(att);
            }

            foreach (string strtemp in hsbcc)
            {
                if ((strtemp != "") && (strtemp !=null))
                {
                    mail.Bcc.Add(strtemp);
                }                
            }

            foreach (string strtemp in hscc)
            {
                if ((strtemp != "")&& (strtemp !=null))
                {
                    mail.CC.Add(strtemp);
                }
            }
            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Send(mail);
            }
            catch
            {
            }

        }

        public void Send_Email_Simple(string From, string To, string Subject, string Body)
        {
            string EmailFrom;
            if (From == "")// put the from address here
            {
                EmailFrom = System.Configuration.ConfigurationSettings.AppSettings.Get("Default_Email_From").ToString();
            }
            else
            {
                EmailFrom = From;
            }
            MailMessage mail = new MailMessage(EmailFrom, To);

            //mail.To = To;             // put to address here
            mail.Subject = Subject;        // put subject here	
            mail.Body = Body;           // put body of email here            
            //SmtpMail.SmtpServer = "localhost"; // put smtp server you will use here 
            // and then send the mail
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Send(mail);
            }
            catch
            {
            }

        }

        public void demoemail(Int64 fbid)
        {
            string emailtype = "demo";
            //check if already has been sent this email
            DataSet dstemp = Eventomatic_DB.SPs.ViewEmailsSent(fbid,emailtype).GetDataSet();

            if (dstemp.Tables[0].Rows.Count == 0) //haven't sent that email yet, so send it
            {
                DataSet dstempfb = Eventomatic_DB.SPs.ViewFbUsers(fbid).GetDataSet();
                string Firstname = "";
                string email = "";
                if (dstempfb.Tables[0].Rows[0]["First_Name"] != DBNull.Value)
                {
                    Firstname = dstempfb.Tables[0].Rows[0]["First_Name"].ToString();
                }
                if (dstempfb.Tables[0].Rows[0]["Email"] != DBNull.Value)
                {
                    email = dstempfb.Tables[0].Rows[0]["Email"].ToString();
                }

                string thebody = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Emails/PayForward_Demo.txt"));
                thebody = thebody.Replace("FIRSTNAME", Firstname);
                Send_Email_Simple("info@thegroupstore.com", email, "Groupstore Demo", thebody);

                Eventomatic_DB.SPs.UpdateEmailsSent(fbid, emailtype, email).Execute();
            }            
        }
    }
}
