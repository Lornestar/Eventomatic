using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using TwilioRest;

namespace Eventomatic.Addons
{
    public class Twilio
    {
        // Twilio REST API version
        const string API_VERSION = "2010-04-01";

        // Twilio AccountSid and AuthToken
        const string ACCOUNT_SID = "AC2818cd272973bfeff59364e898cd73d5";
        const string ACCOUNT_TOKEN = "43d4406acee090878e3dfeb179d45333";

        // Outgoing Caller ID previously validated with Twilio
        const string CALLER_ID = "8189635211";

        public void SendSMS(string phonenumber, string thebody)
        {
            TwilioRest.Account account;
            Hashtable h;

            string phonefiltered = phonenumber.Replace("-", "");
            phonefiltered = phonefiltered.Replace("(", "");
            phonefiltered = phonefiltered.Replace(")", "");

            // Create Twilio REST account object using Twilio account ID and token
            account = new TwilioRest.Account(ACCOUNT_SID, ACCOUNT_TOKEN);

            h = new Hashtable();
            h.Add("From", CALLER_ID);
            h.Add("To", phonenumber); // "NNNNNNNNN");
            h.Add("Body", thebody);
            try
            {
                Console.WriteLine(account.request(String.Format(
                                                                "/{0}/Accounts/{1}/SMS/Messages",
                                                                API_VERSION, ACCOUNT_SID), "POST", h));                
            }
            catch (TwilioRestException e)
            {
                Console.WriteLine("An error occurred: {0}", e.Message);
            }

        }
    }
}

