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
    /// Controller class for Resource_Reading_Others
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class ResourceReadingOtherController
    {
        // Preload our schema..
        ResourceReadingOther thisSchemaLoad = new ResourceReadingOther();
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
        public ResourceReadingOtherCollection FetchAll()
        {
            ResourceReadingOtherCollection coll = new ResourceReadingOtherCollection();
            Query qry = new Query(ResourceReadingOther.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ResourceReadingOtherCollection FetchByID(object ResourceReadingOthersKey)
        {
            ResourceReadingOtherCollection coll = new ResourceReadingOtherCollection().Where("Resource_Reading_Others_Key", ResourceReadingOthersKey).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public ResourceReadingOtherCollection FetchByQuery(Query qry)
        {
            ResourceReadingOtherCollection coll = new ResourceReadingOtherCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object ResourceReadingOthersKey)
        {
            return (ResourceReadingOther.Delete(ResourceReadingOthersKey) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object ResourceReadingOthersKey)
        {
            return (ResourceReadingOther.Destroy(ResourceReadingOthersKey) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int ResourceKey,int ResourceKeyReading,DateTime DateAdded,long FBidAdded)
	    {
		    ResourceReadingOther item = new ResourceReadingOther();
		    
            item.ResourceKey = ResourceKey;
            
            item.ResourceKeyReading = ResourceKeyReading;
            
            item.DateAdded = DateAdded;
            
            item.FBidAdded = FBidAdded;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int ResourceReadingOthersKey,int ResourceKey,int ResourceKeyReading,DateTime DateAdded,long FBidAdded)
	    {
		    ResourceReadingOther item = new ResourceReadingOther();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.ResourceReadingOthersKey = ResourceReadingOthersKey;
				
			item.ResourceKey = ResourceKey;
				
			item.ResourceKeyReading = ResourceKeyReading;
				
			item.DateAdded = DateAdded;
				
			item.FBidAdded = FBidAdded;
				
	        item.Save(UserName);
	    }
    }
}