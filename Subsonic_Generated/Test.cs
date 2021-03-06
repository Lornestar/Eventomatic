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
	/// Strongly-typed collection for the Test class.
	/// </summary>
    [Serializable]
	public partial class TestCollection : ActiveList<Test, TestCollection>
	{	   
		public TestCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TestCollection</returns>
		public TestCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Test o = this[i];
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
	/// This is an ActiveRecord class which wraps the Test table.
	/// </summary>
	[Serializable]
	public partial class Test : ActiveRecord<Test>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Test()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Test(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Test(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Test(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Test", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarTestKey = new TableSchema.TableColumn(schema);
				colvarTestKey.ColumnName = "Test_Key";
				colvarTestKey.DataType = DbType.Int32;
				colvarTestKey.MaxLength = 0;
				colvarTestKey.AutoIncrement = true;
				colvarTestKey.IsNullable = false;
				colvarTestKey.IsPrimaryKey = true;
				colvarTestKey.IsForeignKey = false;
				colvarTestKey.IsReadOnly = false;
				colvarTestKey.DefaultSetting = @"";
				colvarTestKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTestKey);
				
				TableSchema.TableColumn colvarTestText = new TableSchema.TableColumn(schema);
				colvarTestText.ColumnName = "Test_Text";
				colvarTestText.DataType = DbType.AnsiString;
				colvarTestText.MaxLength = 2147483647;
				colvarTestText.AutoIncrement = false;
				colvarTestText.IsNullable = true;
				colvarTestText.IsPrimaryKey = false;
				colvarTestText.IsForeignKey = false;
				colvarTestText.IsReadOnly = false;
				colvarTestText.DefaultSetting = @"";
				colvarTestText.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTestText);
				
				TableSchema.TableColumn colvarTestUpdate = new TableSchema.TableColumn(schema);
				colvarTestUpdate.ColumnName = "Test_update";
				colvarTestUpdate.DataType = DbType.DateTime;
				colvarTestUpdate.MaxLength = 0;
				colvarTestUpdate.AutoIncrement = false;
				colvarTestUpdate.IsNullable = true;
				colvarTestUpdate.IsPrimaryKey = false;
				colvarTestUpdate.IsForeignKey = false;
				colvarTestUpdate.IsReadOnly = false;
				colvarTestUpdate.DefaultSetting = @"";
				colvarTestUpdate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTestUpdate);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("Test",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("TestKey")]
		[Bindable(true)]
		public int TestKey 
		{
			get { return GetColumnValue<int>(Columns.TestKey); }
			set { SetColumnValue(Columns.TestKey, value); }
		}
		  
		[XmlAttribute("TestText")]
		[Bindable(true)]
		public string TestText 
		{
			get { return GetColumnValue<string>(Columns.TestText); }
			set { SetColumnValue(Columns.TestText, value); }
		}
		  
		[XmlAttribute("TestUpdate")]
		[Bindable(true)]
		public DateTime? TestUpdate 
		{
			get { return GetColumnValue<DateTime?>(Columns.TestUpdate); }
			set { SetColumnValue(Columns.TestUpdate, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varTestText,DateTime? varTestUpdate)
		{
			Test item = new Test();
			
			item.TestText = varTestText;
			
			item.TestUpdate = varTestUpdate;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varTestKey,string varTestText,DateTime? varTestUpdate)
		{
			Test item = new Test();
			
				item.TestKey = varTestKey;
			
				item.TestText = varTestText;
			
				item.TestUpdate = varTestUpdate;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn TestKeyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn TestTextColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TestUpdateColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string TestKey = @"Test_Key";
			 public static string TestText = @"Test_Text";
			 public static string TestUpdate = @"Test_update";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
