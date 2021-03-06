using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace Eventomatic_DB
{
    /// <summary>
    /// Controller class for Tickets
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TicketController
    {
        // Preload our schema..
        Ticket thisSchemaLoad = new Ticket();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public TicketCollection FetchAll()
        {
            TicketCollection coll = new TicketCollection();
            Query qry = new Query(Ticket.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TicketCollection FetchByID(object TicketKey)
        {
            TicketCollection coll = new TicketCollection().Where("Ticket_Key", TicketKey).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TicketCollection FetchByQuery(Query qry)
        {
            TicketCollection coll = new TicketCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object TicketKey)
        {
            return (Ticket.Delete(TicketKey) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object TicketKey)
        {
            return (Ticket.Destroy(TicketKey) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int EventKey,string TicketDescription,decimal? Price,int? Capacity,DateTime? BeginSelling,DateTime? SellingDeadline,DateTime? LastModified,bool? Isdonation,int? Type,string ProductDescription,bool? CalendarSensitive,int? CalendarType,int? LessonLength,int? LessonEarliestTime,int? LessonLatestTime,string CouponCode,bool? IsDemo,bool? Visible)
	    {
		    Ticket item = new Ticket();
		    
            item.EventKey = EventKey;
            
            item.TicketDescription = TicketDescription;
            
            item.Price = Price;
            
            item.Capacity = Capacity;
            
            item.BeginSelling = BeginSelling;
            
            item.SellingDeadline = SellingDeadline;
            
            item.LastModified = LastModified;
            
            item.Isdonation = Isdonation;
            
            item.Type = Type;
            
            item.ProductDescription = ProductDescription;
            
            item.CalendarSensitive = CalendarSensitive;
            
            item.CalendarType = CalendarType;
            
            item.LessonLength = LessonLength;
            
            item.LessonEarliestTime = LessonEarliestTime;
            
            item.LessonLatestTime = LessonLatestTime;
            
            item.CouponCode = CouponCode;
            
            item.IsDemo = IsDemo;
            
            item.Visible = Visible;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int TicketKey,int EventKey,string TicketDescription,decimal? Price,int? Capacity,DateTime? BeginSelling,DateTime? SellingDeadline,DateTime? LastModified,bool? Isdonation,int? Type,string ProductDescription,bool? CalendarSensitive,int? CalendarType,int? LessonLength,int? LessonEarliestTime,int? LessonLatestTime,string CouponCode,bool? IsDemo,bool? Visible)
	    {
		    Ticket item = new Ticket();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.TicketKey = TicketKey;
				
			item.EventKey = EventKey;
				
			item.TicketDescription = TicketDescription;
				
			item.Price = Price;
				
			item.Capacity = Capacity;
				
			item.BeginSelling = BeginSelling;
				
			item.SellingDeadline = SellingDeadline;
				
			item.LastModified = LastModified;
				
			item.Isdonation = Isdonation;
				
			item.Type = Type;
				
			item.ProductDescription = ProductDescription;
				
			item.CalendarSensitive = CalendarSensitive;
				
			item.CalendarType = CalendarType;
				
			item.LessonLength = LessonLength;
				
			item.LessonEarliestTime = LessonEarliestTime;
				
			item.LessonLatestTime = LessonLatestTime;
				
			item.CouponCode = CouponCode;
				
			item.IsDemo = IsDemo;
				
			item.Visible = Visible;
				
	        item.Save(UserName);
	    }
    }
}
