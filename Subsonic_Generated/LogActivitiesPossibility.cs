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
	/// Strongly-typed collection for the LogActivitiesPossibility class.
	/// </summary>
    [Serializable]
	public partial class LogActivitiesPossibilityCollection : ActiveList<LogActivitiesPossibility, LogActivitiesPossibilityCollection>
	{	   
		public LogActivitiesPossibilityCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LogActivitiesPossibilityCollection</returns>
		public LogActivitiesPossibilityCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LogActivitiesPossibility o = this[i];
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
	/// This is an ActiveRecord class which wraps the Log_Activities_Possibilities table.
	/// </summary>
	[Serializable]
	public partial class LogActivitiesPossibility : ActiveRecord<LogActivitiesPossibility>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LogActivitiesPossibility()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LogActivitiesPossibility(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LogActivitiesPossibility(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LogActivitiesPossibility(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Log_Activities_Possibilities", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarLogActivitiesPossibilitiesKey = new TableSchema.TableColumn(schema);
				colvarLogActivitiesPossibilitiesKey.ColumnName = "Log_Activities_Possibilities_Key";
				colvarLogActivitiesPossibilitiesKey.DataType = DbType.Int32;
				colvarLogActivitiesPossibilitiesKey.MaxLength = 0;
				colvarLogActivitiesPossibilitiesKey.AutoIncrement = false;
				colvarLogActivitiesPossibilitiesKey.IsNullable = false;
				colvarLogActivitiesPossibilitiesKey.IsPrimaryKey = true;
				colvarLogActivitiesPossibilitiesKey.IsForeignKey = false;
				colvarLogActivitiesPossibilitiesKey.IsReadOnly = false;
				colvarLogActivitiesPossibilitiesKey.DefaultSetting = @"";
				colvarLogActivitiesPossibilitiesKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLogActivitiesPossibilitiesKey);
				
				TableSchema.TableColumn colvarActivity = new TableSchema.TableColumn(schema);
				colvarActivity.ColumnName = "Activity";
				colvarActivity.DataType = DbType.String;
				colvarActivity.MaxLength = 200;
				colvarActivity.AutoIncrement = false;
				colvarActivity.IsNullable = false;
				colvarActivity.IsPrimaryKey = false;
				colvarActivity.IsForeignKey = false;
				colvarActivity.IsReadOnly = false;
				colvarActivity.DefaultSetting = @"";
				colvarActivity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActivity);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("Log_Activities_Possibilities",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("LogActivitiesPossibilitiesKey")]
		[Bindable(true)]
		public int LogActivitiesPossibilitiesKey 
		{
			get { return GetColumnValue<int>(Columns.LogActivitiesPossibilitiesKey); }
			set { SetColumnValue(Columns.LogActivitiesPossibilitiesKey, value); }
		}
		  
		[XmlAttribute("Activity")]
		[Bindable(true)]
		public string Activity 
		{
			get { return GetColumnValue<string>(Columns.Activity); }
			set { SetColumnValue(Columns.Activity, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public Eventomatic_DB.LogActivityCollection LogActivities()
		{
			return new Eventomatic_DB.LogActivityCollection().Where(LogActivity.Columns.Activity, LogActivitiesPossibilitiesKey).Load();
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varLogActivitiesPossibilitiesKey,string varActivity)
		{
			LogActivitiesPossibility item = new LogActivitiesPossibility();
			
			item.LogActivitiesPossibilitiesKey = varLogActivitiesPossibilitiesKey;
			
			item.Activity = varActivity;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varLogActivitiesPossibilitiesKey,string varActivity)
		{
			LogActivitiesPossibility item = new LogActivitiesPossibility();
			
				item.LogActivitiesPossibilitiesKey = varLogActivitiesPossibilitiesKey;
			
				item.Activity = varActivity;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn LogActivitiesPossibilitiesKeyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ActivityColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string LogActivitiesPossibilitiesKey = @"Log_Activities_Possibilities_Key";
			 public static string Activity = @"Activity";
						
		}
		#endregion
		
		#region Update PK Collections
		
        public void SetPKValues()
        {
}
        #endregion
    
        #region Deep Save
		
        public void DeepSave()
        {
            Save();
            
}
        #endregion
	}
}
