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
	/// Strongly-typed collection for the FbUsersPage class.
	/// </summary>
    [Serializable]
	public partial class FbUsersPageCollection : ActiveList<FbUsersPage, FbUsersPageCollection>
	{	   
		public FbUsersPageCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>FbUsersPageCollection</returns>
		public FbUsersPageCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                FbUsersPage o = this[i];
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
	/// This is an ActiveRecord class which wraps the FB_Users_Pages table.
	/// </summary>
	[Serializable]
	public partial class FbUsersPage : ActiveRecord<FbUsersPage>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public FbUsersPage()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public FbUsersPage(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public FbUsersPage(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public FbUsersPage(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("FB_Users_Pages", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarFbUsersPages = new TableSchema.TableColumn(schema);
				colvarFbUsersPages.ColumnName = "FB_Users_Pages";
				colvarFbUsersPages.DataType = DbType.Int32;
				colvarFbUsersPages.MaxLength = 0;
				colvarFbUsersPages.AutoIncrement = true;
				colvarFbUsersPages.IsNullable = false;
				colvarFbUsersPages.IsPrimaryKey = true;
				colvarFbUsersPages.IsForeignKey = false;
				colvarFbUsersPages.IsReadOnly = false;
				colvarFbUsersPages.DefaultSetting = @"";
				colvarFbUsersPages.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFbUsersPages);
				
				TableSchema.TableColumn colvarFbUsers = new TableSchema.TableColumn(schema);
				colvarFbUsers.ColumnName = "FB_Users";
				colvarFbUsers.DataType = DbType.Int64;
				colvarFbUsers.MaxLength = 0;
				colvarFbUsers.AutoIncrement = false;
				colvarFbUsers.IsNullable = false;
				colvarFbUsers.IsPrimaryKey = false;
				colvarFbUsers.IsForeignKey = true;
				colvarFbUsers.IsReadOnly = false;
				colvarFbUsers.DefaultSetting = @"";
				
					colvarFbUsers.ForeignKeyTableName = "FB_Users";
				schema.Columns.Add(colvarFbUsers);
				
				TableSchema.TableColumn colvarPageId = new TableSchema.TableColumn(schema);
				colvarPageId.ColumnName = "Page_id";
				colvarPageId.DataType = DbType.Int64;
				colvarPageId.MaxLength = 0;
				colvarPageId.AutoIncrement = false;
				colvarPageId.IsNullable = false;
				colvarPageId.IsPrimaryKey = false;
				colvarPageId.IsForeignKey = false;
				colvarPageId.IsReadOnly = false;
				colvarPageId.DefaultSetting = @"";
				colvarPageId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPageId);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("FB_Users_Pages",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("FbUsersPages")]
		[Bindable(true)]
		public int FbUsersPages 
		{
			get { return GetColumnValue<int>(Columns.FbUsersPages); }
			set { SetColumnValue(Columns.FbUsersPages, value); }
		}
		  
		[XmlAttribute("FbUsers")]
		[Bindable(true)]
		public long FbUsers 
		{
			get { return GetColumnValue<long>(Columns.FbUsers); }
			set { SetColumnValue(Columns.FbUsers, value); }
		}
		  
		[XmlAttribute("PageId")]
		[Bindable(true)]
		public long PageId 
		{
			get { return GetColumnValue<long>(Columns.PageId); }
			set { SetColumnValue(Columns.PageId, value); }
		}
		
		#endregion
		
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a FbUser ActiveRecord object related to this FbUsersPage
		/// 
		/// </summary>
		public Eventomatic_DB.FbUser FbUser
		{
			get { return Eventomatic_DB.FbUser.FetchByID(this.FbUsers); }
			set { SetColumnValue("FB_Users", value.FBid); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varFbUsers,long varPageId)
		{
			FbUsersPage item = new FbUsersPage();
			
			item.FbUsers = varFbUsers;
			
			item.PageId = varPageId;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varFbUsersPages,long varFbUsers,long varPageId)
		{
			FbUsersPage item = new FbUsersPage();
			
				item.FbUsersPages = varFbUsersPages;
			
				item.FbUsers = varFbUsers;
			
				item.PageId = varPageId;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn FbUsersPagesColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn FbUsersColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn PageIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string FbUsersPages = @"FB_Users_Pages";
			 public static string FbUsers = @"FB_Users";
			 public static string PageId = @"Page_id";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
