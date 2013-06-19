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
	/// Strongly-typed collection for the InfoRegion class.
	/// </summary>
    [Serializable]
	public partial class InfoRegionCollection : ActiveList<InfoRegion, InfoRegionCollection>
	{	   
		public InfoRegionCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>InfoRegionCollection</returns>
		public InfoRegionCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                InfoRegion o = this[i];
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
	/// This is an ActiveRecord class which wraps the InfoRegion table.
	/// </summary>
	[Serializable]
	public partial class InfoRegion : ActiveRecord<InfoRegion>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public InfoRegion()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public InfoRegion(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public InfoRegion(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public InfoRegion(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("InfoRegion", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarRegionKey = new TableSchema.TableColumn(schema);
				colvarRegionKey.ColumnName = "Region_Key";
				colvarRegionKey.DataType = DbType.Int32;
				colvarRegionKey.MaxLength = 0;
				colvarRegionKey.AutoIncrement = false;
				colvarRegionKey.IsNullable = false;
				colvarRegionKey.IsPrimaryKey = true;
				colvarRegionKey.IsForeignKey = false;
				colvarRegionKey.IsReadOnly = false;
				colvarRegionKey.DefaultSetting = @"";
				colvarRegionKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionKey);
				
				TableSchema.TableColumn colvarRegionValue = new TableSchema.TableColumn(schema);
				colvarRegionValue.ColumnName = "Region_Value";
				colvarRegionValue.DataType = DbType.String;
				colvarRegionValue.MaxLength = 50;
				colvarRegionValue.AutoIncrement = false;
				colvarRegionValue.IsNullable = true;
				colvarRegionValue.IsPrimaryKey = false;
				colvarRegionValue.IsForeignKey = false;
				colvarRegionValue.IsReadOnly = false;
				colvarRegionValue.DefaultSetting = @"";
				colvarRegionValue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionValue);
				
				TableSchema.TableColumn colvarRegionText = new TableSchema.TableColumn(schema);
				colvarRegionText.ColumnName = "Region_Text";
				colvarRegionText.DataType = DbType.String;
				colvarRegionText.MaxLength = 50;
				colvarRegionText.AutoIncrement = false;
				colvarRegionText.IsNullable = true;
				colvarRegionText.IsPrimaryKey = false;
				colvarRegionText.IsForeignKey = false;
				colvarRegionText.IsReadOnly = false;
				colvarRegionText.DefaultSetting = @"";
				colvarRegionText.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionText);
				
				TableSchema.TableColumn colvarCountryKey = new TableSchema.TableColumn(schema);
				colvarCountryKey.ColumnName = "Country_Key";
				colvarCountryKey.DataType = DbType.Int32;
				colvarCountryKey.MaxLength = 0;
				colvarCountryKey.AutoIncrement = false;
				colvarCountryKey.IsNullable = false;
				colvarCountryKey.IsPrimaryKey = false;
				colvarCountryKey.IsForeignKey = false;
				colvarCountryKey.IsReadOnly = false;
				colvarCountryKey.DefaultSetting = @"";
				colvarCountryKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountryKey);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("InfoRegion",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("RegionKey")]
		[Bindable(true)]
		public int RegionKey 
		{
			get { return GetColumnValue<int>(Columns.RegionKey); }
			set { SetColumnValue(Columns.RegionKey, value); }
		}
		  
		[XmlAttribute("RegionValue")]
		[Bindable(true)]
		public string RegionValue 
		{
			get { return GetColumnValue<string>(Columns.RegionValue); }
			set { SetColumnValue(Columns.RegionValue, value); }
		}
		  
		[XmlAttribute("RegionText")]
		[Bindable(true)]
		public string RegionText 
		{
			get { return GetColumnValue<string>(Columns.RegionText); }
			set { SetColumnValue(Columns.RegionText, value); }
		}
		  
		[XmlAttribute("CountryKey")]
		[Bindable(true)]
		public int CountryKey 
		{
			get { return GetColumnValue<int>(Columns.CountryKey); }
			set { SetColumnValue(Columns.CountryKey, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varRegionKey,string varRegionValue,string varRegionText,int varCountryKey)
		{
			InfoRegion item = new InfoRegion();
			
			item.RegionKey = varRegionKey;
			
			item.RegionValue = varRegionValue;
			
			item.RegionText = varRegionText;
			
			item.CountryKey = varCountryKey;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varRegionKey,string varRegionValue,string varRegionText,int varCountryKey)
		{
			InfoRegion item = new InfoRegion();
			
				item.RegionKey = varRegionKey;
			
				item.RegionValue = varRegionValue;
			
				item.RegionText = varRegionText;
			
				item.CountryKey = varCountryKey;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn RegionKeyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn RegionValueColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn RegionTextColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn CountryKeyColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string RegionKey = @"Region_Key";
			 public static string RegionValue = @"Region_Value";
			 public static string RegionText = @"Region_Text";
			 public static string CountryKey = @"Country_Key";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
