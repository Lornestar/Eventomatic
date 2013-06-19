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

namespace Eventomatic
{
    public class groupstore_ticket
    {
        private int ticketkey;
        private string description;
        private decimal price;
        private int capacity;
        private DateTime beginselling;
        private DateTime sellingdeadline;

        public int Ticket_key
        {
            get
            {
                return ticketkey;
            }
            set
            {
                ticketkey = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                capacity = value;
            }
        }
        public DateTime BeginSelling
        {
            get
            {
                return beginselling;
            }
            set
            {
                beginselling = value;
            }
        }
        public DateTime SellingDeadline
        {
            get
            {
                return sellingdeadline;
            }
            set
            {
                sellingdeadline = value;
            }
        }
    }
}
