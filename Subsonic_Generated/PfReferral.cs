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
	/// Strongly-typed collection for the PfReferral class.
	/// </summary>
    [Serializable]
	public partial class PfReferralCollection : ActiveList<PfReferral, PfReferralCollection>
	{	   
		public PfReferralCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>PfReferralCollection</returns>
		public PfReferralCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                PfReferral o = this[i];
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
	/// This is an ActiveRecord class which wraps the PF_Referral table.
	/// </summary>
	[Serializable]
	public partial class PfReferral : ActiveRecord<PfReferral>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public PfReferral()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public PfReferral(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public PfReferral(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public PfReferral(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("PF_Referral", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarPfReferralKey = new TableSchema.TableColumn(schema);
				colvarPfReferralKey.ColumnName = "PF_Referral_Key";
				colvarPfReferralKey.DataType = DbType.Int32;
				colvarPfReferralKey.MaxLength = 0;
				colvarPfReferralKey.AutoIncrement = true;
				colvarPfReferralKey.IsNullable = false;
				colvarPfReferralKey.IsPrimaryKey = true;
				colvarPfReferralKey.IsForeignKey = false;
				colvarPfReferralKey.IsReadOnly = false;
				colvarPfReferralKey.DefaultSetting = @"";
				colvarPfReferralKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPfReferralKey);
				
				TableSchema.TableColumn colvarFbReferrer = new TableSchema.TableColumn(schema);
				colvarFbReferrer.ColumnName = "fb_referrer";
				colvarFbReferrer.DataType = DbType.Int64;
				colvarFbReferrer.MaxLength = 0;
				colvarFbReferrer.AutoIncrement = false;
				colvarFbReferrer.IsNullable = false;
				colvarFbReferrer.IsPrimaryKey = false;
				colvarFbReferrer.IsForeignKey = false;
				colvarFbReferrer.IsReadOnly = false;
				colvarFbReferrer.DefaultSetting = @"";
				colvarFbReferrer.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFbReferrer);
				
				TableSchema.TableColumn colvarResourceKey = new TableSchema.TableColumn(schema);
				colvarResourceKey.ColumnName = "resource_key";
				colvarResourceKey.DataType = DbType.Int32;
				colvarResourceKey.MaxLength = 0;
				colvarResourceKey.AutoIncrement = false;
				colvarResourceKey.IsNullable = false;
				colvarResourceKey.IsPrimaryKey = false;
				colvarResourceKey.IsForeignKey = false;
				colvarResourceKey.IsReadOnly = false;
				colvarResourceKey.DefaultSetting = @"";
				colvarResourceKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarResourceKey);
				
				TableSchema.TableColumn colvarAddadmin = new TableSchema.TableColumn(schema);
				colvarAddadmin.ColumnName = "addadmin";
				colvarAddadmin.DataType = DbType.Boolean;
				colvarAddadmin.MaxLength = 0;
				colvarAddadmin.AutoIncrement = false;
				colvarAddadmin.IsNullable = false;
				colvarAddadmin.IsPrimaryKey = false;
				colvarAddadmin.IsForeignKey = false;
				colvarAddadmin.IsReadOnly = false;
				colvarAddadmin.DefaultSetting = @"";
				colvarAddadmin.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddadmin);
				
				TableSchema.TableColumn colvarSmsnumber = new TableSchema.TableColumn(schema);
				colvarSmsnumber.ColumnName = "smsnumber";
				colvarSmsnumber.DataType = DbType.String;
				colvarSmsnumber.MaxLength = 20;
				colvarSmsnumber.AutoIncrement = false;
				colvarSmsnumber.IsNullable = true;
				colvarSmsnumber.IsPrimaryKey = false;
				colvarSmsnumber.IsForeignKey = false;
				colvarSmsnumber.IsReadOnly = false;
				colvarSmsnumber.DefaultSetting = @"";
				colvarSmsnumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSmsnumber);
				
				TableSchema.TableColumn colvarLastChange = new TableSchema.TableColumn(schema);
				colvarLastChange.ColumnName = "last_change";
				colvarLastChange.DataType = DbType.DateTime;
				colvarLastChange.MaxLength = 0;
				colvarLastChange.AutoIncrement = false;
				colvarLastChange.IsNullable = false;
				colvarLastChange.IsPrimaryKey = false;
				colvarLastChange.IsForeignKey = false;
				colvarLastChange.IsReadOnly = false;
				colvarLastChange.DefaultSetting = @"";
				colvarLastChange.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastChange);
				
				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "email";
				colvarEmail.DataType = DbType.String;
				colvarEmail.MaxLength = 100;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = true;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("PF_Referral",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("PfReferralKey")]
		[Bindable(true)]
		public int PfReferralKey 
		{
			get { return GetColumnValue<int>(Columns.PfReferralKey); }
			set { SetColumnValue(Columns.PfReferralKey, value); }
		}
		  
		[XmlAttribute("FbReferrer")]
		[Bindable(true)]
		public long FbReferrer 
		{
			get { return GetColumnValue<long>(Columns.FbReferrer); }
			set { SetColumnValue(Columns.FbReferrer, value); }
		}
		  
		[XmlAttribute("ResourceKey")]
		[Bindable(true)]
		public int ResourceKey 
		{
			get { return GetColumnValue<int>(Columns.ResourceKey); }
			set { SetColumnValue(Columns.ResourceKey, value); }
		}
		  
		[XmlAttribute("Addadmin")]
		[Bindable(true)]
		public bool Addadmin 
		{
			get { return GetColumnValue<bool>(Columns.Addadmin); }
			set { SetColumnValue(Columns.Addadmin, value); }
		}
		  
		[XmlAttribute("Smsnumber")]
		[Bindable(true)]
		public string Smsnumber 
		{
			get { return GetColumnValue<string>(Columns.Smsnumber); }
			set { SetColumnValue(Columns.Smsnumber, value); }
		}
		  
		[XmlAttribute("LastChange")]
		[Bindable(true)]
		public DateTime LastChange 
		{
			get { return GetColumnValue<DateTime>(Columns.LastChange); }
			set { SetColumnValue(Columns.LastChange, value); }
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
		public static void Insert(long varFbReferrer,int varResourceKey,bool varAddadmin,string varSmsnumber,DateTime varLastChange,string varEmail)
		{
			PfReferral item = new PfReferral();
			
			item.FbReferrer = varFbReferrer;
			
			item.ResourceKey = varResourceKey;
			
			item.Addadmin = varAddadmin;
			
			item.Smsnumber = varSmsnumber;
			
			item.LastChange = varLastChange;
			
			item.Email = varEmail;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varPfReferralKey,long varFbReferrer,int varResourceKey,bool varAddadmin,string varSmsnumber,DateTime varLastChange,string varEmail)
		{
			PfReferral item = new PfReferral();
			
				item.PfReferralKey = varPfReferralKey;
			
				item.FbReferrer = varFbReferrer;
			
				item.ResourceKey = varResourceKey;
			
				item.Addadmin = varAddadmin;
			
				item.Smsnumber = varSmsnumber;
			
				item.LastChange = varLastChange;
			
				item.Email = varEmail;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn PfReferralKeyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn FbReferrerColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ResourceKeyColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn AddadminColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SmsnumberColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn LastChangeColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn EmailColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string PfReferralKey = @"PF_Referral_Key";
			 public static string FbReferrer = @"fb_referrer";
			 public static string ResourceKey = @"resource_key";
			 public static string Addadmin = @"addadmin";
			 public static string Smsnumber = @"smsnumber";
			 public static string LastChange = @"last_change";
			 public static string Email = @"email";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}