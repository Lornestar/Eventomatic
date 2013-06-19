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
	/// Strongly-typed collection for the TransactionStatusType class.
	/// </summary>
    [Serializable]
	public partial class TransactionStatusTypeCollection : ActiveList<TransactionStatusType, TransactionStatusTypeCollection>
	{	   
		public TransactionStatusTypeCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TransactionStatusTypeCollection</returns>
		public TransactionStatusTypeCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TransactionStatusType o = this[i];
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
	/// This is an ActiveRecord class which wraps the Transaction_Status_Type table.
	/// </summary>
	[Serializable]
	public partial class TransactionStatusType : ActiveRecord<TransactionStatusType>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TransactionStatusType()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TransactionStatusType(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TransactionStatusType(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TransactionStatusType(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Transaction_Status_Type", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarTransactionStatusTypeKey = new TableSchema.TableColumn(schema);
				colvarTransactionStatusTypeKey.ColumnName = "Transaction_Status_Type_Key";
				colvarTransactionStatusTypeKey.DataType = DbType.Int32;
				colvarTransactionStatusTypeKey.MaxLength = 0;
				colvarTransactionStatusTypeKey.AutoIncrement = true;
				colvarTransactionStatusTypeKey.IsNullable = false;
				colvarTransactionStatusTypeKey.IsPrimaryKey = true;
				colvarTransactionStatusTypeKey.IsForeignKey = false;
				colvarTransactionStatusTypeKey.IsReadOnly = false;
				colvarTransactionStatusTypeKey.DefaultSetting = @"";
				colvarTransactionStatusTypeKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTransactionStatusTypeKey);
				
				TableSchema.TableColumn colvarTransactionStatusTypeX = new TableSchema.TableColumn(schema);
				colvarTransactionStatusTypeX.ColumnName = "Transaction_Status_Type";
				colvarTransactionStatusTypeX.DataType = DbType.String;
				colvarTransactionStatusTypeX.MaxLength = 50;
				colvarTransactionStatusTypeX.AutoIncrement = false;
				colvarTransactionStatusTypeX.IsNullable = false;
				colvarTransactionStatusTypeX.IsPrimaryKey = false;
				colvarTransactionStatusTypeX.IsForeignKey = false;
				colvarTransactionStatusTypeX.IsReadOnly = false;
				colvarTransactionStatusTypeX.DefaultSetting = @"";
				colvarTransactionStatusTypeX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTransactionStatusTypeX);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("Transaction_Status_Type",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("TransactionStatusTypeKey")]
		[Bindable(true)]
		public int TransactionStatusTypeKey 
		{
			get { return GetColumnValue<int>(Columns.TransactionStatusTypeKey); }
			set { SetColumnValue(Columns.TransactionStatusTypeKey, value); }
		}
		  
		[XmlAttribute("TransactionStatusTypeX")]
		[Bindable(true)]
		public string TransactionStatusTypeX 
		{
			get { return GetColumnValue<string>(Columns.TransactionStatusTypeX); }
			set { SetColumnValue(Columns.TransactionStatusTypeX, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varTransactionStatusTypeX)
		{
			TransactionStatusType item = new TransactionStatusType();
			
			item.TransactionStatusTypeX = varTransactionStatusTypeX;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varTransactionStatusTypeKey,string varTransactionStatusTypeX)
		{
			TransactionStatusType item = new TransactionStatusType();
			
				item.TransactionStatusTypeKey = varTransactionStatusTypeKey;
			
				item.TransactionStatusTypeX = varTransactionStatusTypeX;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn TransactionStatusTypeKeyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn TransactionStatusTypeXColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string TransactionStatusTypeKey = @"Transaction_Status_Type_Key";
			 public static string TransactionStatusTypeX = @"Transaction_Status_Type";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}