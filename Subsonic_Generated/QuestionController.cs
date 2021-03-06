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
    /// Controller class for Question
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class QuestionController
    {
        // Preload our schema..
        Question thisSchemaLoad = new Question();
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
        public QuestionCollection FetchAll()
        {
            QuestionCollection coll = new QuestionCollection();
            Query qry = new Query(Question.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public QuestionCollection FetchByID(object QuestionKey)
        {
            QuestionCollection coll = new QuestionCollection().Where("Question_Key", QuestionKey).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public QuestionCollection FetchByQuery(Query qry)
        {
            QuestionCollection coll = new QuestionCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object QuestionKey)
        {
            return (Question.Delete(QuestionKey) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object QuestionKey)
        {
            return (Question.Destroy(QuestionKey) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int EventKey,string TheQuestion,bool? Mandatory,int? QuestionType)
	    {
		    Question item = new Question();
		    
            item.EventKey = EventKey;
            
            item.TheQuestion = TheQuestion;
            
            item.Mandatory = Mandatory;
            
            item.QuestionType = QuestionType;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int QuestionKey,int EventKey,string TheQuestion,bool? Mandatory,int? QuestionType)
	    {
		    Question item = new Question();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.QuestionKey = QuestionKey;
				
			item.EventKey = EventKey;
				
			item.TheQuestion = TheQuestion;
				
			item.Mandatory = Mandatory;
				
			item.QuestionType = QuestionType;
				
	        item.Save(UserName);
	    }
    }
}
