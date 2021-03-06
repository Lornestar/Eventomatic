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
    /// Controller class for Transaction_Status_Type
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TransactionStatusTypeController
    {
        // Preload our schema..
        TransactionStatusType thisSchemaLoad = new TransactionStatusType();
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
        public TransactionStatusTypeCollection FetchAll()
        {
            TransactionStatusTypeCollection coll = new TransactionStatusTypeCollection();
            Query qry = new Query(TransactionStatusType.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TransactionStatusTypeCollection FetchByID(object TransactionStatusTypeKey)
        {
            TransactionStatusTypeCollection coll = new TransactionStatusTypeCollection().Where("Transaction_Status_Type_Key", TransactionStatusTypeKey).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TransactionStatusTypeCollection FetchByQuery(Query qry)
        {
            TransactionStatusTypeCollection coll = new TransactionStatusTypeCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object TransactionStatusTypeKey)
        {
            return (TransactionStatusType.Delete(TransactionStatusTypeKey) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object TransactionStatusTypeKey)
        {
            return (TransactionStatusType.Destroy(TransactionStatusTypeKey) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string TransactionStatusTypeX)
	    {
		    TransactionStatusType item = new TransactionStatusType();
		    
            item.TransactionStatusTypeX = TransactionStatusTypeX;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int TransactionStatusTypeKey,string TransactionStatusTypeX)
	    {
		    TransactionStatusType item = new TransactionStatusType();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.TransactionStatusTypeKey = TransactionStatusTypeKey;
				
			item.TransactionStatusTypeX = TransactionStatusTypeX;
				
	        item.Save(UserName);
	    }
    }
}
