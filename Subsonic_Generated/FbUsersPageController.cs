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
    /// Controller class for FB_Users_Pages
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class FbUsersPageController
    {
        // Preload our schema..
        FbUsersPage thisSchemaLoad = new FbUsersPage();
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
        public FbUsersPageCollection FetchAll()
        {
            FbUsersPageCollection coll = new FbUsersPageCollection();
            Query qry = new Query(FbUsersPage.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public FbUsersPageCollection FetchByID(object FbUsersPages)
        {
            FbUsersPageCollection coll = new FbUsersPageCollection().Where("FB_Users_Pages", FbUsersPages).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public FbUsersPageCollection FetchByQuery(Query qry)
        {
            FbUsersPageCollection coll = new FbUsersPageCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object FbUsersPages)
        {
            return (FbUsersPage.Delete(FbUsersPages) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object FbUsersPages)
        {
            return (FbUsersPage.Destroy(FbUsersPages) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long FbUsers,long PageId)
	    {
		    FbUsersPage item = new FbUsersPage();
		    
            item.FbUsers = FbUsers;
            
            item.PageId = PageId;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int FbUsersPages,long FbUsers,long PageId)
	    {
		    FbUsersPage item = new FbUsersPage();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.FbUsersPages = FbUsersPages;
				
			item.FbUsers = FbUsers;
				
			item.PageId = PageId;
				
	        item.Save(UserName);
	    }
    }
}