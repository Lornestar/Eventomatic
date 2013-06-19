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
	/// Strongly-typed collection for the FbUsersEmail class.
	/// </summary>
    [Serializable]
	public partial class FbUsersEmailCollection : ActiveList<FbUsersEmail, FbUsersEmailCollection>
	{	   
		public FbUsersEmailCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>FbUsersEmailCollection</returns>
		public FbUsersEmailCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                FbUsersEmail o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the FB_Users_Email table.
	/// </summary>
	[Serializable]
	public partial class FbUsersEmail : ActiveRecord<FbUsersEmail>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public FbUsersEmail()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public FbUsersEmail(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public FbUsersEmail(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public FbUsersEmail(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("FB_Users_Email", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarFbUsersEmailKey = new TableSchema.TableColumn(schema);
				colvarFbUsersEmailKey.ColumnName = "FB_Users_Email_Key";
				colvarFbUsersEmailKey.DataType = DbType.Int32;
				colvarFbUsersEmailKey.MaxLength = 0;
				colvarFbUsersEmailKey.AutoIncrement = true;
				colvarFbUsersEmailKey.IsNullable = false;
				colvarFbUsersEmailKey.IsPrimaryKey = true;
				colvarFbUsersEmailKey.IsForeignKey = false;
				colvarFbUsersEmailKey.IsReadOnly = false;
				colvarFbUsersEmailKey.DefaultSetting = @"";
				colvarFbUsersEmailKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFbUsersEmailKey);
				
				TableSchema.TableColumn colvarFbUsers = new TableSchema.TableColumn(schema);
				colvarFbUsers.ColumnName = "FB_Users";
				colvarFbUsers.DataType = DbType.Int64;
				colvarFbUsers.MaxLength = 0;
				colvarFbUsers.AutoIncrement = false;
				colvarFbUsers.IsNullable = false;
				colvarFbUsers.IsPrimaryKey = false;
				colvarFbUsers.IsForeignKey = false;
				colvarFbUsers.IsReadOnly = false;
				colvarFbUsers.DefaultSetting = @"";
				colvarFbUsers.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFbUsers);
				
				TableSchema.TableColumn colvarEmailType = new TableSchema.TableColumn(schema);
				colvarEmailType.ColumnName = "Email_Type";
				colvarEmailType.DataType = DbType.String;
				colvarEmailType.MaxLength = 50;
				colvarEmailType.AutoIncrement = false;
				colvarEmailType.IsNullable = false;
				colvarEmailType.IsPrimaryKey = false;
				colvarEmailType.IsForeignKey = false;
				colvarEmailType.IsReadOnly = false;
				colvarEmailType.DefaultSetting = @"";
				colvarEmailType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmailType);
				
				TableSchema.TableColumn colvarDateSent = new TableSchema.TableColumn(schema);
				colvarDateSent.ColumnName = "date_sent";
				colvarDateSent.DataType = DbType.DateTime;
				colvarDateSent.MaxLength = 0;
				colvarDateSent.AutoIncrement = false;
				colvarDateSent.IsNullable = false;
				colvarDateSent.IsPrimaryKey = false;
				colvarDateSent.IsForeignKey = false;
				colvarDateSent.IsReadOnly = false;
				colvarDateSent.DefaultSetting = @"";
				colvarDateSent.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDateSent);
				
				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "email";
				colvarEmail.DataType = DbType.String;
				colvarEmail.MaxLength = 100;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = false;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("FB_Users_Email",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("FbUsersEmailKey")]
		[Bindable(true)]
		public int FbUsersEmailKey 
		{
			get { return GetColumnValue<int>(Columns.FbUsersEmailKey); }
			set { SetColumnValue(Columns.FbUsersEmailKey, value); }
		}
		  
		[XmlAttribute("FbUsers")]
		[Bindable(true)]
		public long FbUsers 
		{
			get { return GetColumnValue<long>(Columns.FbUsers); }
			set { SetColumnValue(Columns.FbUsers, value); }
		}
		  
		[XmlAttribute("EmailType")]
		[Bindable(true)]
		public string EmailType 
		{
			get { return GetColumnValue<string>(Columns.EmailType); }
			set { SetColumnValue(Columns.EmailType, value); }
		}
		  
		[XmlAttribute("DateSent")]
		[Bindable(true)]
		public DateTime DateSent 
		{
			get { return GetColumnValue<DateTime>(Columns.DateSent); }
			set { SetColumnValue(Columns.DateSent, value); }
		}
		  
		[XmlAttribute("Email")]
		[Bindable(true)]
		public string Email 
		{
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varFbUsers,string varEmailType,DateTime varDateSent,string varEmail)
		{
			FbUsersEmail item = new FbUsersEmail();
			
			item.FbUsers = varFbUsers;
			
			item.EmailType = varEmailType;
			
			item.DateSent = varDateSent;
			
			item.Email = varEmail;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varFbUsersEmailKey,long varFbUsers,string varEmailType,DateTime varDateSent,string varEmail)
		{
			FbUsersEmail item = new FbUsersEmail();
			
				item.FbUsersEmailKey = varFbUsersEmailKey;
			
				item.FbUsers = varFbUsers;
			
				item.EmailType = varEmailType;
			
				item.DateSent = varDateSent;
			
				item.Email = varEmail;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn FbUsersEmailKeyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn FbUsersColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn EmailTypeColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn DateSentColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn EmailColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string FbUsersEmailKey = @"FB_Users_Email_Key";
			 public static string FbUsers = @"FB_Users";
			 public static string EmailType = @"Email_Type";
			 public static string DateSent = @"date_sent";
			 public static string Email = @"email";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
