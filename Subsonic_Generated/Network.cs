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
	/// Strongly-typed collection for the Network class.
	/// </summary>
    [Serializable]
	public partial class NetworkCollection : ActiveList<Network, NetworkCollection>
	{	   
		public NetworkCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NetworkCollection</returns>
		public NetworkCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Network o = this[i];
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
	/// This is an ActiveRecord class which wraps the Networks table.
	/// </summary>
	[Serializable]
	public partial class Network : ActiveRecord<Network>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Network()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Network(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Network(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Network(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Networks", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarNetworkKey = new TableSchema.TableColumn(schema);
				colvarNetworkKey.ColumnName = "Network_Key";
				colvarNetworkKey.DataType = DbType.Int32;
				colvarNetworkKey.MaxLength = 0;
				colvarNetworkKey.AutoIncrement = true;
				colvarNetworkKey.IsNullable = false;
				colvarNetworkKey.IsPrimaryKey = true;
				colvarNetworkKey.IsForeignKey = false;
				colvarNetworkKey.IsReadOnly = false;
				colvarNetworkKey.DefaultSetting = @"";
				colvarNetworkKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNetworkKey);
				
				TableSchema.TableColumn colvarNetworkName = new TableSchema.TableColumn(schema);
				colvarNetworkName.ColumnName = "Network_Name";
				colvarNetworkName.DataType = DbType.String;
				colvarNetworkName.MaxLength = 300;
				colvarNetworkName.AutoIncrement = false;
				colvarNetworkName.IsNullable = false;
				colvarNetworkName.IsPrimaryKey = false;
				colvarNetworkName.IsForeignKey = false;
				colvarNetworkName.IsReadOnly = false;
				colvarNetworkName.DefaultSetting = @"";
				colvarNetworkName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNetworkName);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("Networks",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("NetworkKey")]
		[Bindable(true)]
		public int NetworkKey 
		{
			get { return GetColumnValue<int>(Columns.NetworkKey); }
			set { SetColumnValue(Columns.NetworkKey, value); }
		}
		  
		[XmlAttribute("NetworkName")]
		[Bindable(true)]
		public string NetworkName 
		{
			get { return GetColumnValue<string>(Columns.NetworkName); }
			set { SetColumnValue(Columns.NetworkName, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varNetworkName)
		{
			Network item = new Network();
			
			item.NetworkName = varNetworkName;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varNetworkKey,string varNetworkName)
		{
			Network item = new Network();
			
				item.NetworkKey = varNetworkKey;
			
				item.NetworkName = varNetworkName;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn NetworkKeyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn NetworkNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string NetworkKey = @"Network_Key";
			 public static string NetworkName = @"Network_Name";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
