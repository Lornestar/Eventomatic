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
	/// Strongly-typed collection for the Ticket class.
	/// </summary>
    [Serializable]
	public partial class TicketCollection : ActiveList<Ticket, TicketCollection>
	{	   
		public TicketCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TicketCollection</returns>
		public TicketCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Ticket o = this[i];
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
	/// This is an ActiveRecord class which wraps the Tickets table.
	/// </summary>
	[Serializable]
	public partial class Ticket : ActiveRecord<Ticket>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Ticket()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Ticket(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Ticket(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Ticket(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Tickets", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarTicketKey = new TableSchema.TableColumn(schema);
				colvarTicketKey.ColumnName = "Ticket_Key";
				colvarTicketKey.DataType = DbType.Int32;
				colvarTicketKey.MaxLength = 0;
				colvarTicketKey.AutoIncrement = true;
				colvarTicketKey.IsNullable = false;
				colvarTicketKey.IsPrimaryKey = true;
				colvarTicketKey.IsForeignKey = false;
				colvarTicketKey.IsReadOnly = false;
				colvarTicketKey.DefaultSetting = @"";
				colvarTicketKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTicketKey);
				
				TableSchema.TableColumn colvarEventKey = new TableSchema.TableColumn(schema);
				colvarEventKey.ColumnName = "Event_Key";
				colvarEventKey.DataType = DbType.Int32;
				colvarEventKey.MaxLength = 0;
				colvarEventKey.AutoIncrement = false;
				colvarEventKey.IsNullable = false;
				colvarEventKey.IsPrimaryKey = false;
				colvarEventKey.IsForeignKey = true;
				colvarEventKey.IsReadOnly = false;
				colvarEventKey.DefaultSetting = @"";
				
					colvarEventKey.ForeignKeyTableName = "Events";
				schema.Columns.Add(colvarEventKey);
				
				TableSchema.TableColumn colvarTicketDescription = new TableSchema.TableColumn(schema);
				colvarTicketDescription.ColumnName = "Ticket_Description";
				colvarTicketDescription.DataType = DbType.String;
				colvarTicketDescription.MaxLength = 200;
				colvarTicketDescription.AutoIncrement = false;
				colvarTicketDescription.IsNullable = true;
				colvarTicketDescription.IsPrimaryKey = false;
				colvarTicketDescription.IsForeignKey = false;
				colvarTicketDescription.IsReadOnly = false;
				colvarTicketDescription.DefaultSetting = @"";
				colvarTicketDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTicketDescription);
				
				TableSchema.TableColumn colvarPrice = new TableSchema.TableColumn(schema);
				colvarPrice.ColumnName = "Price";
				colvarPrice.DataType = DbType.Currency;
				colvarPrice.MaxLength = 0;
				colvarPrice.AutoIncrement = false;
				colvarPrice.IsNullable = true;
				colvarPrice.IsPrimaryKey = false;
				colvarPrice.IsForeignKey = false;
				colvarPrice.IsReadOnly = false;
				colvarPrice.DefaultSetting = @"";
				colvarPrice.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPrice);
				
				TableSchema.TableColumn colvarCapacity = new TableSchema.TableColumn(schema);
				colvarCapacity.ColumnName = "Capacity";
				colvarCapacity.DataType = DbType.Int32;
				colvarCapacity.MaxLength = 0;
				colvarCapacity.AutoIncrement = false;
				colvarCapacity.IsNullable = true;
				colvarCapacity.IsPrimaryKey = false;
				colvarCapacity.IsForeignKey = false;
				colvarCapacity.IsReadOnly = false;
				colvarCapacity.DefaultSetting = @"";
				colvarCapacity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCapacity);
				
				TableSchema.TableColumn colvarBeginSelling = new TableSchema.TableColumn(schema);
				colvarBeginSelling.ColumnName = "Begin_Selling";
				colvarBeginSelling.DataType = DbType.DateTime;
				colvarBeginSelling.MaxLength = 0;
				colvarBeginSelling.AutoIncrement = false;
				colvarBeginSelling.IsNullable = true;
				colvarBeginSelling.IsPrimaryKey = false;
				colvarBeginSelling.IsForeignKey = false;
				colvarBeginSelling.IsReadOnly = false;
				colvarBeginSelling.DefaultSetting = @"";
				colvarBeginSelling.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBeginSelling);
				
				TableSchema.TableColumn colvarSellingDeadline = new TableSchema.TableColumn(schema);
				colvarSellingDeadline.ColumnName = "Selling_Deadline";
				colvarSellingDeadline.DataType = DbType.DateTime;
				colvarSellingDeadline.MaxLength = 0;
				colvarSellingDeadline.AutoIncrement = false;
				colvarSellingDeadline.IsNullable = true;
				colvarSellingDeadline.IsPrimaryKey = false;
				colvarSellingDeadline.IsForeignKey = false;
				colvarSellingDeadline.IsReadOnly = false;
				colvarSellingDeadline.DefaultSetting = @"";
				colvarSellingDeadline.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSellingDeadline);
				
				TableSchema.TableColumn colvarLastModified = new TableSchema.TableColumn(schema);
				colvarLastModified.ColumnName = "Last_Modified";
				colvarLastModified.DataType = DbType.DateTime;
				colvarLastModified.MaxLength = 0;
				colvarLastModified.AutoIncrement = false;
				colvarLastModified.IsNullable = true;
				colvarLastModified.IsPrimaryKey = false;
				colvarLastModified.IsForeignKey = false;
				colvarLastModified.IsReadOnly = false;
				colvarLastModified.DefaultSetting = @"";
				colvarLastModified.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastModified);
				
				TableSchema.TableColumn colvarIsdonation = new TableSchema.TableColumn(schema);
				colvarIsdonation.ColumnName = "isdonation";
				colvarIsdonation.DataType = DbType.Boolean;
				colvarIsdonation.MaxLength = 0;
				colvarIsdonation.AutoIncrement = false;
				colvarIsdonation.IsNullable = true;
				colvarIsdonation.IsPrimaryKey = false;
				colvarIsdonation.IsForeignKey = false;
				colvarIsdonation.IsReadOnly = false;
				colvarIsdonation.DefaultSetting = @"";
				colvarIsdonation.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsdonation);
				
				TableSchema.TableColumn colvarType = new TableSchema.TableColumn(schema);
				colvarType.ColumnName = "Type";
				colvarType.DataType = DbType.Int32;
				colvarType.MaxLength = 0;
				colvarType.AutoIncrement = false;
				colvarType.IsNullable = true;
				colvarType.IsPrimaryKey = false;
				colvarType.IsForeignKey = false;
				colvarType.IsReadOnly = false;
				colvarType.DefaultSetting = @"";
				colvarType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarType);
				
				TableSchema.TableColumn colvarProductDescription = new TableSchema.TableColumn(schema);
				colvarProductDescription.ColumnName = "Product_Description";
				colvarProductDescription.DataType = DbType.AnsiString;
				colvarProductDescription.MaxLength = 2147483647;
				colvarProductDescription.AutoIncrement = false;
				colvarProductDescription.IsNullable = true;
				colvarProductDescription.IsPrimaryKey = false;
				colvarProductDescription.IsForeignKey = false;
				colvarProductDescription.IsReadOnly = false;
				colvarProductDescription.DefaultSetting = @"";
				colvarProductDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProductDescription);
				
				TableSchema.TableColumn colvarCalendarSensitive = new TableSchema.TableColumn(schema);
				colvarCalendarSensitive.ColumnName = "Calendar_Sensitive";
				colvarCalendarSensitive.DataType = DbType.Boolean;
				colvarCalendarSensitive.MaxLength = 0;
				colvarCalendarSensitive.AutoIncrement = false;
				colvarCalendarSensitive.IsNullable = true;
				colvarCalendarSensitive.IsPrimaryKey = false;
				colvarCalendarSensitive.IsForeignKey = false;
				colvarCalendarSensitive.IsReadOnly = false;
				colvarCalendarSensitive.DefaultSetting = @"";
				colvarCalendarSensitive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCalendarSensitive);
				
				TableSchema.TableColumn colvarCalendarType = new TableSchema.TableColumn(schema);
				colvarCalendarType.ColumnName = "Calendar_Type";
				colvarCalendarType.DataType = DbType.Int32;
				colvarCalendarType.MaxLength = 0;
				colvarCalendarType.AutoIncrement = false;
				colvarCalendarType.IsNullable = true;
				colvarCalendarType.IsPrimaryKey = false;
				colvarCalendarType.IsForeignKey = false;
				colvarCalendarType.IsReadOnly = false;
				colvarCalendarType.DefaultSetting = @"";
				colvarCalendarType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCalendarType);
				
				TableSchema.TableColumn colvarLessonLength = new TableSchema.TableColumn(schema);
				colvarLessonLength.ColumnName = "Lesson_Length";
				colvarLessonLength.DataType = DbType.Int32;
				colvarLessonLength.MaxLength = 0;
				colvarLessonLength.AutoIncrement = false;
				colvarLessonLength.IsNullable = true;
				colvarLessonLength.IsPrimaryKey = false;
				colvarLessonLength.IsForeignKey = false;
				colvarLessonLength.IsReadOnly = false;
				colvarLessonLength.DefaultSetting = @"";
				colvarLessonLength.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLessonLength);
				
				TableSchema.TableColumn colvarLessonEarliestTime = new TableSchema.TableColumn(schema);
				colvarLessonEarliestTime.ColumnName = "Lesson_Earliest_Time";
				colvarLessonEarliestTime.DataType = DbType.Int32;
				colvarLessonEarliestTime.MaxLength = 0;
				colvarLessonEarliestTime.AutoIncrement = false;
				colvarLessonEarliestTime.IsNullable = true;
				colvarLessonEarliestTime.IsPrimaryKey = false;
				colvarLessonEarliestTime.IsForeignKey = false;
				colvarLessonEarliestTime.IsReadOnly = false;
				colvarLessonEarliestTime.DefaultSetting = @"";
				colvarLessonEarliestTime.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLessonEarliestTime);
				
				TableSchema.TableColumn colvarLessonLatestTime = new TableSchema.TableColumn(schema);
				colvarLessonLatestTime.ColumnName = "Lesson_Latest_Time";
				colvarLessonLatestTime.DataType = DbType.Int32;
				colvarLessonLatestTime.MaxLength = 0;
				colvarLessonLatestTime.AutoIncrement = false;
				colvarLessonLatestTime.IsNullable = true;
				colvarLessonLatestTime.IsPrimaryKey = false;
				colvarLessonLatestTime.IsForeignKey = false;
				colvarLessonLatestTime.IsReadOnly = false;
				colvarLessonLatestTime.DefaultSetting = @"";
				colvarLessonLatestTime.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLessonLatestTime);
				
				TableSchema.TableColumn colvarCouponCode = new TableSchema.TableColumn(schema);
				colvarCouponCode.ColumnName = "Coupon_Code";
				colvarCouponCode.DataType = DbType.String;
				colvarCouponCode.MaxLength = 10;
				colvarCouponCode.AutoIncrement = false;
				colvarCouponCode.IsNullable = true;
				colvarCouponCode.IsPrimaryKey = false;
				colvarCouponCode.IsForeignKey = false;
				colvarCouponCode.IsReadOnly = false;
				colvarCouponCode.DefaultSetting = @"";
				colvarCouponCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCouponCode);
				
				TableSchema.TableColumn colvarIsDemo = new TableSchema.TableColumn(schema);
				colvarIsDemo.ColumnName = "IsDemo";
				colvarIsDemo.DataType = DbType.Boolean;
				colvarIsDemo.MaxLength = 0;
				colvarIsDemo.AutoIncrement = false;
				colvarIsDemo.IsNullable = true;
				colvarIsDemo.IsPrimaryKey = false;
				colvarIsDemo.IsForeignKey = false;
				colvarIsDemo.IsReadOnly = false;
				colvarIsDemo.DefaultSetting = @"";
				colvarIsDemo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDemo);
				
				TableSchema.TableColumn colvarVisible = new TableSchema.TableColumn(schema);
				colvarVisible.ColumnName = "Visible";
				colvarVisible.DataType = DbType.Boolean;
				colvarVisible.MaxLength = 0;
				colvarVisible.AutoIncrement = false;
				colvarVisible.IsNullable = true;
				colvarVisible.IsPrimaryKey = false;
				colvarVisible.IsForeignKey = false;
				colvarVisible.IsReadOnly = false;
				colvarVisible.DefaultSetting = @"";
				colvarVisible.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVisible);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("Tickets",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("TicketKey")]
		[Bindable(true)]
		public int TicketKey 
		{
			get { return GetColumnValue<int>(Columns.TicketKey); }
			set { SetColumnValue(Columns.TicketKey, value); }
		}
		  
		[XmlAttribute("EventKey")]
		[Bindable(true)]
		public int EventKey 
		{
			get { return GetColumnValue<int>(Columns.EventKey); }
			set { SetColumnValue(Columns.EventKey, value); }
		}
		  
		[XmlAttribute("TicketDescription")]
		[Bindable(true)]
		public string TicketDescription 
		{
			get { return GetColumnValue<string>(Columns.TicketDescription); }
			set { SetColumnValue(Columns.TicketDescription, value); }
		}
		  
		[XmlAttribute("Price")]
		[Bindable(true)]
		public decimal? Price 
		{
			get { return GetColumnValue<decimal?>(Columns.Price); }
			set { SetColumnValue(Columns.Price, value); }
		}
		  
		[XmlAttribute("Capacity")]
		[Bindable(true)]
		public int? Capacity 
		{
			get { return GetColumnValue<int?>(Columns.Capacity); }
			set { SetColumnValue(Columns.Capacity, value); }
		}
		  
		[XmlAttribute("BeginSelling")]
		[Bindable(true)]
		public DateTime? BeginSelling 
		{
			get { return GetColumnValue<DateTime?>(Columns.BeginSelling); }
			set { SetColumnValue(Columns.BeginSelling, value); }
		}
		  
		[XmlAttribute("SellingDeadline")]
		[Bindable(true)]
		public DateTime? SellingDeadline 
		{
			get { return GetColumnValue<DateTime?>(Columns.SellingDeadline); }
			set { SetColumnValue(Columns.SellingDeadline, value); }
		}
		  
		[XmlAttribute("LastModified")]
		[Bindable(true)]
		public DateTime? LastModified 
		{
			get { return GetColumnValue<DateTime?>(Columns.LastModified); }
			set { SetColumnValue(Columns.LastModified, value); }
		}
		  
		[XmlAttribute("Isdonation")]
		[Bindable(true)]
		public bool? Isdonation 
		{
			get { return GetColumnValue<bool?>(Columns.Isdonation); }
			set { SetColumnValue(Columns.Isdonation, value); }
		}
		  
		[XmlAttribute("Type")]
		[Bindable(true)]
		public int? Type 
		{
			get { return GetColumnValue<int?>(Columns.Type); }
			set { SetColumnValue(Columns.Type, value); }
		}
		  
		[XmlAttribute("ProductDescription")]
		[Bindable(true)]
		public string ProductDescription 
		{
			get { return GetColumnValue<string>(Columns.ProductDescription); }
			set { SetColumnValue(Columns.ProductDescription, value); }
		}
		  
		[XmlAttribute("CalendarSensitive")]
		[Bindable(true)]
		public bool? CalendarSensitive 
		{
			get { return GetColumnValue<bool?>(Columns.CalendarSensitive); }
			set { SetColumnValue(Columns.CalendarSensitive, value); }
		}
		  
		[XmlAttribute("CalendarType")]
		[Bindable(true)]
		public int? CalendarType 
		{
			get { return GetColumnValue<int?>(Columns.CalendarType); }
			set { SetColumnValue(Columns.CalendarType, value); }
		}
		  
		[XmlAttribute("LessonLength")]
		[Bindable(true)]
		public int? LessonLength 
		{
			get { return GetColumnValue<int?>(Columns.LessonLength); }
			set { SetColumnValue(Columns.LessonLength, value); }
		}
		  
		[XmlAttribute("LessonEarliestTime")]
		[Bindable(true)]
		public int? LessonEarliestTime 
		{
			get { return GetColumnValue<int?>(Columns.LessonEarliestTime); }
			set { SetColumnValue(Columns.LessonEarliestTime, value); }
		}
		  
		[XmlAttribute("LessonLatestTime")]
		[Bindable(true)]
		public int? LessonLatestTime 
		{
			get { return GetColumnValue<int?>(Columns.LessonLatestTime); }
			set { SetColumnValue(Columns.LessonLatestTime, value); }
		}
		  
		[XmlAttribute("CouponCode")]
		[Bindable(true)]
		public string CouponCode 
		{
			get { return GetColumnValue<string>(Columns.CouponCode); }
			set { SetColumnValue(Columns.CouponCode, value); }
		}
		  
		[XmlAttribute("IsDemo")]
		[Bindable(true)]
		public bool? IsDemo 
		{
			get { return GetColumnValue<bool?>(Columns.IsDemo); }
			set { SetColumnValue(Columns.IsDemo, value); }
		}
		  
		[XmlAttribute("Visible")]
		[Bindable(true)]
		public bool? Visible 
		{
			get { return GetColumnValue<bool?>(Columns.Visible); }
			set { SetColumnValue(Columns.Visible, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public Eventomatic_DB.TicketsPurchasedCollection TicketsPurchasedRecords()
		{
			return new Eventomatic_DB.TicketsPurchasedCollection().Where(TicketsPurchased.Columns.TicketKey, TicketKey).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a EventX ActiveRecord object related to this Ticket
		/// 
		/// </summary>
		public Eventomatic_DB.EventX EventX
		{
			get { return Eventomatic_DB.EventX.FetchByID(this.EventKey); }
			set { SetColumnValue("Event_Key", value.EventKey); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varEventKey,string varTicketDescription,decimal? varPrice,int? varCapacity,DateTime? varBeginSelling,DateTime? varSellingDeadline,DateTime? varLastModified,bool? varIsdonation,int? varType,string varProductDescription,bool? varCalendarSensitive,int? varCalendarType,int? varLessonLength,int? varLessonEarliestTime,int? varLessonLatestTime,string varCouponCode,bool? varIsDemo,bool? varVisible)
		{
			Ticket item = new Ticket();
			
			item.EventKey = varEventKey;
			
			item.TicketDescription = varTicketDescription;
			
			item.Price = varPrice;
			
			item.Capacity = varCapacity;
			
			item.BeginSelling = varBeginSelling;
			
			item.SellingDeadline = varSellingDeadline;
			
			item.LastModified = varLastModified;
			
			item.Isdonation = varIsdonation;
			
			item.Type = varType;
			
			item.ProductDescription = varProductDescription;
			
			item.CalendarSensitive = varCalendarSensitive;
			
			item.CalendarType = varCalendarType;
			
			item.LessonLength = varLessonLength;
			
			item.LessonEarliestTime = varLessonEarliestTime;
			
			item.LessonLatestTime = varLessonLatestTime;
			
			item.CouponCode = varCouponCode;
			
			item.IsDemo = varIsDemo;
			
			item.Visible = varVisible;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varTicketKey,int varEventKey,string varTicketDescription,decimal? varPrice,int? varCapacity,DateTime? varBeginSelling,DateTime? varSellingDeadline,DateTime? varLastModified,bool? varIsdonation,int? varType,string varProductDescription,bool? varCalendarSensitive,int? varCalendarType,int? varLessonLength,int? varLessonEarliestTime,int? varLessonLatestTime,string varCouponCode,bool? varIsDemo,bool? varVisible)
		{
			Ticket item = new Ticket();
			
				item.TicketKey = varTicketKey;
			
				item.EventKey = varEventKey;
			
				item.TicketDescription = varTicketDescription;
			
				item.Price = varPrice;
			
				item.Capacity = varCapacity;
			
				item.BeginSelling = varBeginSelling;
			
				item.SellingDeadline = varSellingDeadline;
			
				item.LastModified = varLastModified;
			
				item.Isdonation = varIsdonation;
			
				item.Type = varType;
			
				item.ProductDescription = varProductDescription;
			
				item.CalendarSensitive = varCalendarSensitive;
			
				item.CalendarType = varCalendarType;
			
				item.LessonLength = varLessonLength;
			
				item.LessonEarliestTime = varLessonEarliestTime;
			
				item.LessonLatestTime = varLessonLatestTime;
			
				item.CouponCode = varCouponCode;
			
				item.IsDemo = varIsDemo;
			
				item.Visible = varVisible;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn TicketKeyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn EventKeyColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TicketDescriptionColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn PriceColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn CapacityColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn BeginSellingColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SellingDeadlineColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn LastModifiedColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn IsdonationColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn TypeColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn ProductDescriptionColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn CalendarSensitiveColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn CalendarTypeColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn LessonLengthColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn LessonEarliestTimeColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn LessonLatestTimeColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn CouponCodeColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn IsDemoColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn VisibleColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string TicketKey = @"Ticket_Key";
			 public static string EventKey = @"Event_Key";
			 public static string TicketDescription = @"Ticket_Description";
			 public static string Price = @"Price";
			 public static string Capacity = @"Capacity";
			 public static string BeginSelling = @"Begin_Selling";
			 public static string SellingDeadline = @"Selling_Deadline";
			 public static string LastModified = @"Last_Modified";
			 public static string Isdonation = @"isdonation";
			 public static string Type = @"Type";
			 public static string ProductDescription = @"Product_Description";
			 public static string CalendarSensitive = @"Calendar_Sensitive";
			 public static string CalendarType = @"Calendar_Type";
			 public static string LessonLength = @"Lesson_Length";
			 public static string LessonEarliestTime = @"Lesson_Earliest_Time";
			 public static string LessonLatestTime = @"Lesson_Latest_Time";
			 public static string CouponCode = @"Coupon_Code";
			 public static string IsDemo = @"IsDemo";
			 public static string Visible = @"Visible";
						
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
