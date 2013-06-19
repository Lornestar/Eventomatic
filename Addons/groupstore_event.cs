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

namespace Eventomatic.Addons
{
    public class groupstore_event
    {
        private int eventkey;
        private string eventname;
        private string host;
        private DateTime eventbegins;
        private DateTime eventends;
        private string location;
        private string street;
        private string city;
        private string phone;
        private string email;
        private string additionalcomments;
        private string imageurl;
        private bool displaytickets;
        private string confirmation;
        private int resourcekey;
        private string eid;
        private int ticketnum;
        private groupstore_ticket[] tickets;
        

        public int Event_Key
        {
            get
            {
                return eventkey;
            }
            set
            {
                eventkey = value;
            }
        }
        public string Eventname
        {
            get
            {
                return eventname;
            }
            set
            {
                eventname = value;
            }
        }
        public string Host
        {
            get
            {
                return host;
            }
            set
            {
                host = value;
            }
        }
        public DateTime EventBegins
        {
            get
            {
                return eventbegins;
            }
            set
            {
                eventbegins = value;
            }
        }
        public DateTime EventEnds
        {
            get
            {
                return eventends;
            }
            set
            {
                eventends = value;
            }
        }
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                street = value;
            }
        }
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public string Additionalcomments
        {
            get
            {
                return additionalcomments;
            }
            set
            {
                additionalcomments = value;
            }
        }
        public string Imageurl
        {
            get
            {
                return imageurl;
            }
            set
            {
                imageurl = value;
            }
        }
        public bool Displaytickets
        {
            get
            {
                return displaytickets;
            }
            set
            {
                displaytickets = value;
            }
        }
        public string Confirmation
        {
            get
            {
                return confirmation;
            }
            set
            {
                confirmation = value;
            }
        }
        public int Resourcekey
        {
            get
            {
                return resourcekey;
            }
            set
            {
                resourcekey = value;
            }
        }
        public string Eid
        {
            get
            {
                return eid;
            }
            set
            {
                eid = value;
            }
        }
        public int Ticketnum
        {
            get
            {
                return ticketnum;
            }
            set
            {
                ticketnum = value;
            }
        }
        public groupstore_ticket[] Tickets
        {
            get
            {
                return tickets;
            }
            set
            {
                tickets = value;
            }
        }

    }
}
