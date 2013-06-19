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
    /// Controller class for Billing_Payment
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class BillingPaymentController
    {
        // Preload our schema..
        BillingPayment thisSchemaLoad = new BillingPayment();
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
        public BillingPaymentCollection FetchAll()
        {
            BillingPaymentCollection coll = new BillingPaymentCollection();
            Query qry = new Query(BillingPayment.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public BillingPaymentCollection FetchByID(object BillingPaymentKey)
        {
            BillingPaymentCollection coll = new BillingPaymentCollection().Where("Billing_Payment_Key", BillingPaymentKey).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public BillingPaymentCollection FetchByQuery(Query qry)
        {
            BillingPaymentCollection coll = new BillingPaymentCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object BillingPaymentKey)
        {
            return (BillingPayment.Delete(BillingPaymentKey) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object BillingPaymentKey)
        {
            return (BillingPayment.Destroy(BillingPaymentKey) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? TxKey,decimal? Amount,int BillingType,DateTime? BillingDate,string CorrelationId,string TxnId,int? BillingAgreementKey)
	    {
		    BillingPayment item = new BillingPayment();
		    
            item.TxKey = TxKey;
            
            item.Amount = Amount;
            
            item.BillingType = BillingType;
            
            item.BillingDate = BillingDate;
            
            item.CorrelationId = CorrelationId;
            
            item.TxnId = TxnId;
            
            item.BillingAgreementKey = BillingAgreementKey;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int BillingPaymentKey,int? TxKey,decimal? Amount,int BillingType,DateTime? BillingDate,string CorrelationId,string TxnId,int? BillingAgreementKey)
	    {
		    BillingPayment item = new BillingPayment();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.BillingPaymentKey = BillingPaymentKey;
				
			item.TxKey = TxKey;
				
			item.Amount = Amount;
				
			item.BillingType = BillingType;
				
			item.BillingDate = BillingDate;
				
			item.CorrelationId = CorrelationId;
				
			item.TxnId = TxnId;
				
			item.BillingAgreementKey = BillingAgreementKey;
				
	        item.Save(UserName);
	    }
    }
}
