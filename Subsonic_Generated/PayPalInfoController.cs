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
    /// Controller class for PayPal_Info
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PayPalInfoController
    {
        // Preload our schema..
        PayPalInfo thisSchemaLoad = new PayPalInfo();
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
        public PayPalInfoCollection FetchAll()
        {
            PayPalInfoCollection coll = new PayPalInfoCollection();
            Query qry = new Query(PayPalInfo.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PayPalInfoCollection FetchByID(object ResourceKey)
        {
            PayPalInfoCollection coll = new PayPalInfoCollection().Where("Resource_Key", ResourceKey).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PayPalInfoCollection FetchByQuery(Query qry)
        {
            PayPalInfoCollection coll = new PayPalInfoCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object ResourceKey)
        {
            return (PayPalInfo.Delete(ResourceKey) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object ResourceKey)
        {
            return (PayPalInfo.Destroy(ResourceKey) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int ResourceKey,string Payerid,string FirstName,string LastName,string Email,string Country,bool Verified,DateTime LastChange)
	    {
		    PayPalInfo item = new PayPalInfo();
		    
            item.ResourceKey = ResourceKey;
            
            item.Payerid = Payerid;
            
            item.FirstName = FirstName;
            
            item.LastName = LastName;
            
            item.Email = Email;
            
            item.Country = Country;
            
            item.Verified = Verified;
            
            item.LastChange = LastChange;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int ResourceKey,string Payerid,string FirstName,string LastName,string Email,string Country,bool Verified,DateTime LastChange)
	    {
		    PayPalInfo item = new PayPalInfo();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.ResourceKey = ResourceKey;
				
			item.Payerid = Payerid;
				
			item.FirstName = FirstName;
				
			item.LastName = LastName;
				
			item.Email = Email;
				
			item.Country = Country;
				
			item.Verified = Verified;
				
			item.LastChange = LastChange;
				
	        item.Save(UserName);
	    }
    }
}
