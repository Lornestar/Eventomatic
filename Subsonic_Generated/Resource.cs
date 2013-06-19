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
	/// Strongly-typed collection for the Resource class.
	/// </summary>
    [Serializable]
	public partial class ResourceCollection : ActiveList<Resource, ResourceCollection>
	{	   
		public ResourceCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ResourceCollection</returns>
		public ResourceCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Resource o = this[i];
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
	/// This is an ActiveRecord class which wraps the Resource table.
	/// </summary>
	[Serializable]
	public partial class Resource : ActiveRecord<Resource>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Resource()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Resource(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Resource(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Resource(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Resource", TableType.Table, DataService.GetInstance("Eventomatic"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarResourceKey = new TableSchema.TableColumn(schema);
				colvarResourceKey.ColumnName = "Resource_Key";
				colvarResourceKey.DataType = DbType.Int32;
				colvarResourceKey.MaxLength = 0;
				colvarResourceKey.AutoIncrement = true;
				colvarResourceKey.IsNullable = false;
				colvarResourceKey.IsPrimaryKey = true;
				colvarResourceKey.IsForeignKey = false;
				colvarResourceKey.IsReadOnly = false;
				colvarResourceKey.DefaultSetting = @"";
				colvarResourceKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarResourceKey);
				
				TableSchema.TableColumn colvarGroupName = new TableSchema.TableColumn(schema);
				colvarGroupName.ColumnName = "Group_Name";
				colvarGroupName.DataType = DbType.AnsiString;
				colvarGroupName.MaxLength = 100;
				colvarGroupName.AutoIncrement = false;
				colvarGroupName.IsNullable = true;
				colvarGroupName.IsPrimaryKey = false;
				colvarGroupName.IsForeignKey = false;
				colvarGroupName.IsReadOnly = false;
				colvarGroupName.DefaultSetting = @"";
				colvarGroupName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGroupName);
				
				TableSchema.TableColumn colvarFBCreator = new TableSchema.TableColumn(schema);
				colvarFBCreator.ColumnName = "FBCreator";
				colvarFBCreator.DataType = DbType.Int64;
				colvarFBCreator.MaxLength = 0;
				colvarFBCreator.AutoIncrement = false;
				colvarFBCreator.IsNullable = false;
				colvarFBCreator.IsPrimaryKey = false;
				colvarFBCreator.IsForeignKey = true;
				colvarFBCreator.IsReadOnly = false;
				colvarFBCreator.DefaultSetting = @"";
				
					colvarFBCreator.ForeignKeyTableName = "FB_Users";
				schema.Columns.Add(colvarFBCreator);
				
				TableSchema.TableColumn colvarSignedUp = new TableSchema.TableColumn(schema);
				colvarSignedUp.ColumnName = "Signed_Up";
				colvarSignedUp.DataType = DbType.DateTime;
				colvarSignedUp.MaxLength = 0;
				colvarSignedUp.AutoIncrement = false;
				colvarSignedUp.IsNullable = false;
				colvarSignedUp.IsPrimaryKey = false;
				colvarSignedUp.IsForeignKey = false;
				colvarSignedUp.IsReadOnly = false;
				colvarSignedUp.DefaultSetting = @"";
				colvarSignedUp.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSignedUp);
				
				TableSchema.TableColumn colvarLastChange = new TableSchema.TableColumn(schema);
				colvarLastChange.ColumnName = "Last_Change";
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
				
				TableSchema.TableColumn colvarDesiredCurrency = new TableSchema.TableColumn(schema);
				colvarDesiredCurrency.ColumnName = "Desired_Currency";
				colvarDesiredCurrency.DataType = DbType.String;
				colvarDesiredCurrency.MaxLength = 3;
				colvarDesiredCurrency.AutoIncrement = false;
				colvarDesiredCurrency.IsNullable = true;
				colvarDesiredCurrency.IsPrimaryKey = false;
				colvarDesiredCurrency.IsForeignKey = false;
				colvarDesiredCurrency.IsReadOnly = false;
				colvarDesiredCurrency.DefaultSetting = @"";
				colvarDesiredCurrency.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDesiredCurrency);
				
				TableSchema.TableColumn colvarEmailPaypal = new TableSchema.TableColumn(schema);
				colvarEmailPaypal.ColumnName = "Email_Paypal";
				colvarEmailPaypal.DataType = DbType.AnsiString;
				colvarEmailPaypal.MaxLength = 200;
				colvarEmailPaypal.AutoIncrement = false;
				colvarEmailPaypal.IsNullable = true;
				colvarEmailPaypal.IsPrimaryKey = false;
				colvarEmailPaypal.IsForeignKey = false;
				colvarEmailPaypal.IsReadOnly = false;
				colvarEmailPaypal.DefaultSetting = @"";
				colvarEmailPaypal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmailPaypal);
				
				TableSchema.TableColumn colvarServiceFeePercentage = new TableSchema.TableColumn(schema);
				colvarServiceFeePercentage.ColumnName = "Service_Fee_Percentage";
				colvarServiceFeePercentage.DataType = DbType.Currency;
				colvarServiceFeePercentage.MaxLength = 0;
				colvarServiceFeePercentage.AutoIncrement = false;
				colvarServiceFeePercentage.IsNullable = true;
				colvarServiceFeePercentage.IsPrimaryKey = false;
				colvarServiceFeePercentage.IsForeignKey = false;
				colvarServiceFeePercentage.IsReadOnly = false;
				colvarServiceFeePercentage.DefaultSetting = @"";
				colvarServiceFeePercentage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceFeePercentage);
				
				TableSchema.TableColumn colvarServiceFeeCents = new TableSchema.TableColumn(schema);
				colvarServiceFeeCents.ColumnName = "Service_Fee_Cents";
				colvarServiceFeeCents.DataType = DbType.Currency;
				colvarServiceFeeCents.MaxLength = 0;
				colvarServiceFeeCents.AutoIncrement = false;
				colvarServiceFeeCents.IsNullable = true;
				colvarServiceFeeCents.IsPrimaryKey = false;
				colvarServiceFeeCents.IsForeignKey = false;
				colvarServiceFeeCents.IsReadOnly = false;
				colvarServiceFeeCents.DefaultSetting = @"";
				colvarServiceFeeCents.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceFeeCents);
				
				TableSchema.TableColumn colvarServiceFeeMax = new TableSchema.TableColumn(schema);
				colvarServiceFeeMax.ColumnName = "Service_Fee_Max";
				colvarServiceFeeMax.DataType = DbType.Currency;
				colvarServiceFeeMax.MaxLength = 0;
				colvarServiceFeeMax.AutoIncrement = false;
				colvarServiceFeeMax.IsNullable = true;
				colvarServiceFeeMax.IsPrimaryKey = false;
				colvarServiceFeeMax.IsForeignKey = false;
				colvarServiceFeeMax.IsReadOnly = false;
				colvarServiceFeeMax.DefaultSetting = @"";
				colvarServiceFeeMax.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceFeeMax);
				
				TableSchema.TableColumn colvarDemo = new TableSchema.TableColumn(schema);
				colvarDemo.ColumnName = "Demo";
				colvarDemo.DataType = DbType.Boolean;
				colvarDemo.MaxLength = 0;
				colvarDemo.AutoIncrement = false;
				colvarDemo.IsNullable = true;
				colvarDemo.IsPrimaryKey = false;
				colvarDemo.IsForeignKey = false;
				colvarDemo.IsReadOnly = false;
				colvarDemo.DefaultSetting = @"";
				colvarDemo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDemo);
				
				TableSchema.TableColumn colvarStoreDescription = new TableSchema.TableColumn(schema);
				colvarStoreDescription.ColumnName = "Store_Description";
				colvarStoreDescription.DataType = DbType.AnsiString;
				colvarStoreDescription.MaxLength = 2147483647;
				colvarStoreDescription.AutoIncrement = false;
				colvarStoreDescription.IsNullable = true;
				colvarStoreDescription.IsPrimaryKey = false;
				colvarStoreDescription.IsForeignKey = false;
				colvarStoreDescription.IsReadOnly = false;
				colvarStoreDescription.DefaultSetting = @"";
				colvarStoreDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStoreDescription);
				
				TableSchema.TableColumn colvarStoreContact = new TableSchema.TableColumn(schema);
				colvarStoreContact.ColumnName = "Store_Contact";
				colvarStoreContact.DataType = DbType.AnsiString;
				colvarStoreContact.MaxLength = 200;
				colvarStoreContact.AutoIncrement = false;
				colvarStoreContact.IsNullable = true;
				colvarStoreContact.IsPrimaryKey = false;
				colvarStoreContact.IsForeignKey = false;
				colvarStoreContact.IsReadOnly = false;
				colvarStoreContact.DefaultSetting = @"";
				colvarStoreContact.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStoreContact);
				
				TableSchema.TableColumn colvarStoreTitle = new TableSchema.TableColumn(schema);
				colvarStoreTitle.ColumnName = "Store_Title";
				colvarStoreTitle.DataType = DbType.AnsiString;
				colvarStoreTitle.MaxLength = 200;
				colvarStoreTitle.AutoIncrement = false;
				colvarStoreTitle.IsNullable = true;
				colvarStoreTitle.IsPrimaryKey = false;
				colvarStoreTitle.IsForeignKey = false;
				colvarStoreTitle.IsReadOnly = false;
				colvarStoreTitle.DefaultSetting = @"";
				colvarStoreTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStoreTitle);
				
				TableSchema.TableColumn colvarPayMethod = new TableSchema.TableColumn(schema);
				colvarPayMethod.ColumnName = "Pay_Method";
				colvarPayMethod.DataType = DbType.Int32;
				colvarPayMethod.MaxLength = 0;
				colvarPayMethod.AutoIncrement = false;
				colvarPayMethod.IsNullable = true;
				colvarPayMethod.IsPrimaryKey = false;
				colvarPayMethod.IsForeignKey = false;
				colvarPayMethod.IsReadOnly = false;
				colvarPayMethod.DefaultSetting = @"";
				colvarPayMethod.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayMethod);
				
				TableSchema.TableColumn colvarAdminActive = new TableSchema.TableColumn(schema);
				colvarAdminActive.ColumnName = "Admin_Active";
				colvarAdminActive.DataType = DbType.Boolean;
				colvarAdminActive.MaxLength = 0;
				colvarAdminActive.AutoIncrement = false;
				colvarAdminActive.IsNullable = true;
				colvarAdminActive.IsPrimaryKey = false;
				colvarAdminActive.IsForeignKey = false;
				colvarAdminActive.IsReadOnly = false;
				colvarAdminActive.DefaultSetting = @"";
				colvarAdminActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAdminActive);
				
				TableSchema.TableColumn colvarNetworkKey = new TableSchema.TableColumn(schema);
				colvarNetworkKey.ColumnName = "Network_Key";
				colvarNetworkKey.DataType = DbType.Int32;
				colvarNetworkKey.MaxLength = 0;
				colvarNetworkKey.AutoIncrement = false;
				colvarNetworkKey.IsNullable = true;
				colvarNetworkKey.IsPrimaryKey = false;
				colvarNetworkKey.IsForeignKey = false;
				colvarNetworkKey.IsReadOnly = false;
				colvarNetworkKey.DefaultSetting = @"";
				colvarNetworkKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNetworkKey);
				
				TableSchema.TableColumn colvarMobileSales = new TableSchema.TableColumn(schema);
				colvarMobileSales.ColumnName = "Mobile_Sales";
				colvarMobileSales.DataType = DbType.Boolean;
				colvarMobileSales.MaxLength = 0;
				colvarMobileSales.AutoIncrement = false;
				colvarMobileSales.IsNullable = true;
				colvarMobileSales.IsPrimaryKey = false;
				colvarMobileSales.IsForeignKey = false;
				colvarMobileSales.IsReadOnly = false;
				colvarMobileSales.DefaultSetting = @"";
				colvarMobileSales.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMobileSales);
				
				TableSchema.TableColumn colvarPermRequestToken = new TableSchema.TableColumn(schema);
				colvarPermRequestToken.ColumnName = "Perm_Request_Token";
				colvarPermRequestToken.DataType = DbType.String;
				colvarPermRequestToken.MaxLength = 100;
				colvarPermRequestToken.AutoIncrement = false;
				colvarPermRequestToken.IsNullable = true;
				colvarPermRequestToken.IsPrimaryKey = false;
				colvarPermRequestToken.IsForeignKey = false;
				colvarPermRequestToken.IsReadOnly = false;
				colvarPermRequestToken.DefaultSetting = @"";
				colvarPermRequestToken.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPermRequestToken);
				
				TableSchema.TableColumn colvarPermVerificationCode = new TableSchema.TableColumn(schema);
				colvarPermVerificationCode.ColumnName = "Perm_Verification_Code";
				colvarPermVerificationCode.DataType = DbType.String;
				colvarPermVerificationCode.MaxLength = 100;
				colvarPermVerificationCode.AutoIncrement = false;
				colvarPermVerificationCode.IsNullable = true;
				colvarPermVerificationCode.IsPrimaryKey = false;
				colvarPermVerificationCode.IsForeignKey = false;
				colvarPermVerificationCode.IsReadOnly = false;
				colvarPermVerificationCode.DefaultSetting = @"";
				colvarPermVerificationCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPermVerificationCode);
				
				TableSchema.TableColumn colvarDodirectpayment = new TableSchema.TableColumn(schema);
				colvarDodirectpayment.ColumnName = "dodirectpayment";
				colvarDodirectpayment.DataType = DbType.Boolean;
				colvarDodirectpayment.MaxLength = 0;
				colvarDodirectpayment.AutoIncrement = false;
				colvarDodirectpayment.IsNullable = true;
				colvarDodirectpayment.IsPrimaryKey = false;
				colvarDodirectpayment.IsForeignKey = false;
				colvarDodirectpayment.IsReadOnly = false;
				colvarDodirectpayment.DefaultSetting = @"";
				colvarDodirectpayment.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDodirectpayment);
				
				TableSchema.TableColumn colvarPfConfirmation = new TableSchema.TableColumn(schema);
				colvarPfConfirmation.ColumnName = "pfConfirmation";
				colvarPfConfirmation.DataType = DbType.String;
				colvarPfConfirmation.MaxLength = 400;
				colvarPfConfirmation.AutoIncrement = false;
				colvarPfConfirmation.IsNullable = true;
				colvarPfConfirmation.IsPrimaryKey = false;
				colvarPfConfirmation.IsForeignKey = false;
				colvarPfConfirmation.IsReadOnly = false;
				colvarPfConfirmation.DefaultSetting = @"";
				colvarPfConfirmation.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPfConfirmation);
				
				TableSchema.TableColumn colvarPermAccessToken = new TableSchema.TableColumn(schema);
				colvarPermAccessToken.ColumnName = "Perm_Access_Token";
				colvarPermAccessToken.DataType = DbType.String;
				colvarPermAccessToken.MaxLength = 100;
				colvarPermAccessToken.AutoIncrement = false;
				colvarPermAccessToken.IsNullable = true;
				colvarPermAccessToken.IsPrimaryKey = false;
				colvarPermAccessToken.IsForeignKey = false;
				colvarPermAccessToken.IsReadOnly = false;
				colvarPermAccessToken.DefaultSetting = @"";
				colvarPermAccessToken.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPermAccessToken);
				
				TableSchema.TableColumn colvarPermAccessTokenSecret = new TableSchema.TableColumn(schema);
				colvarPermAccessTokenSecret.ColumnName = "Perm_Access_Token_Secret";
				colvarPermAccessTokenSecret.DataType = DbType.String;
				colvarPermAccessTokenSecret.MaxLength = 100;
				colvarPermAccessTokenSecret.AutoIncrement = false;
				colvarPermAccessTokenSecret.IsNullable = true;
				colvarPermAccessTokenSecret.IsPrimaryKey = false;
				colvarPermAccessTokenSecret.IsForeignKey = false;
				colvarPermAccessTokenSecret.IsReadOnly = false;
				colvarPermAccessTokenSecret.DefaultSetting = @"";
				colvarPermAccessTokenSecret.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPermAccessTokenSecret);
				
				TableSchema.TableColumn colvarThirdPartyPayPal = new TableSchema.TableColumn(schema);
				colvarThirdPartyPayPal.ColumnName = "ThirdPartyPayPal";
				colvarThirdPartyPayPal.DataType = DbType.Boolean;
				colvarThirdPartyPayPal.MaxLength = 0;
				colvarThirdPartyPayPal.AutoIncrement = false;
				colvarThirdPartyPayPal.IsNullable = true;
				colvarThirdPartyPayPal.IsPrimaryKey = false;
				colvarThirdPartyPayPal.IsForeignKey = false;
				colvarThirdPartyPayPal.IsReadOnly = false;
				colvarThirdPartyPayPal.DefaultSetting = @"";
				colvarThirdPartyPayPal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThirdPartyPayPal);
				
				TableSchema.TableColumn colvarDescriptor = new TableSchema.TableColumn(schema);
				colvarDescriptor.ColumnName = "Descriptor";
				colvarDescriptor.DataType = DbType.String;
				colvarDescriptor.MaxLength = 11;
				colvarDescriptor.AutoIncrement = false;
				colvarDescriptor.IsNullable = true;
				colvarDescriptor.IsPrimaryKey = false;
				colvarDescriptor.IsForeignKey = false;
				colvarDescriptor.IsReadOnly = false;
				colvarDescriptor.DefaultSetting = @"";
				colvarDescriptor.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescriptor);
				
				TableSchema.TableColumn colvarPayerid = new TableSchema.TableColumn(schema);
				colvarPayerid.ColumnName = "payerid";
				colvarPayerid.DataType = DbType.String;
				colvarPayerid.MaxLength = 50;
				colvarPayerid.AutoIncrement = false;
				colvarPayerid.IsNullable = true;
				colvarPayerid.IsPrimaryKey = false;
				colvarPayerid.IsForeignKey = false;
				colvarPayerid.IsReadOnly = false;
				colvarPayerid.DefaultSetting = @"";
				colvarPayerid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayerid);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Eventomatic"].AddSchema("Resource",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ResourceKey")]
		[Bindable(true)]
		public int ResourceKey 
		{
			get { return GetColumnValue<int>(Columns.ResourceKey); }
			set { SetColumnValue(Columns.ResourceKey, value); }
		}
		  
		[XmlAttribute("GroupName")]
		[Bindable(true)]
		public string GroupName 
		{
			get { return GetColumnValue<string>(Columns.GroupName); }
			set { SetColumnValue(Columns.GroupName, value); }
		}
		  
		[XmlAttribute("FBCreator")]
		[Bindable(true)]
		public long FBCreator 
		{
			get { return GetColumnValue<long>(Columns.FBCreator); }
			set { SetColumnValue(Columns.FBCreator, value); }
		}
		  
		[XmlAttribute("SignedUp")]
		[Bindable(true)]
		public DateTime SignedUp 
		{
			get { return GetColumnValue<DateTime>(Columns.SignedUp); }
			set { SetColumnValue(Columns.SignedUp, value); }
		}
		  
		[XmlAttribute("LastChange")]
		[Bindable(true)]
		public DateTime LastChange 
		{
			get { return GetColumnValue<DateTime>(Columns.LastChange); }
			set { SetColumnValue(Columns.LastChange, value); }
		}
		  
		[XmlAttribute("DesiredCurrency")]
		[Bindable(true)]
		public string DesiredCurrency 
		{
			get { return GetColumnValue<string>(Columns.DesiredCurrency); }
			set { SetColumnValue(Columns.DesiredCurrency, value); }
		}
		  
		[XmlAttribute("EmailPaypal")]
		[Bindable(true)]
		public string EmailPaypal 
		{
			get { return GetColumnValue<string>(Columns.EmailPaypal); }
			set { SetColumnValue(Columns.EmailPaypal, value); }
		}
		  
		[XmlAttribute("ServiceFeePercentage")]
		[Bindable(true)]
		public decimal? ServiceFeePercentage 
		{
			get { return GetColumnValue<decimal?>(Columns.ServiceFeePercentage); }
			set { SetColumnValue(Columns.ServiceFeePercentage, value); }
		}
		  
		[XmlAttribute("ServiceFeeCents")]
		[Bindable(true)]
		public decimal? ServiceFeeCents 
		{
			get { return GetColumnValue<decimal?>(Columns.ServiceFeeCents); }
			set { SetColumnValue(Columns.ServiceFeeCents, value); }
		}
		  
		[XmlAttribute("ServiceFeeMax")]
		[Bindable(true)]
		public decimal? ServiceFeeMax 
		{
			get { return GetColumnValue<decimal?>(Columns.ServiceFeeMax); }
			set { SetColumnValue(Columns.ServiceFeeMax, value); }
		}
		  
		[XmlAttribute("Demo")]
		[Bindable(true)]
		public bool? Demo 
		{
			get { return GetColumnValue<bool?>(Columns.Demo); }
			set { SetColumnValue(Columns.Demo, value); }
		}
		  
		[XmlAttribute("StoreDescription")]
		[Bindable(true)]
		public string StoreDescription 
		{
			get { return GetColumnValue<string>(Columns.StoreDescription); }
			set { SetColumnValue(Columns.StoreDescription, value); }
		}
		  
		[XmlAttribute("StoreContact")]
		[Bindable(true)]
		public string StoreContact 
		{
			get { return GetColumnValue<string>(Columns.StoreContact); }
			set { SetColumnValue(Columns.StoreContact, value); }
		}
		  
		[XmlAttribute("StoreTitle")]
		[Bindable(true)]
		public string StoreTitle 
		{
			get { return GetColumnValue<string>(Columns.StoreTitle); }
			set { SetColumnValue(Columns.StoreTitle, value); }
		}
		  
		[XmlAttribute("PayMethod")]
		[Bindable(true)]
		public int? PayMethod 
		{
			get { return GetColumnValue<int?>(Columns.PayMethod); }
			set { SetColumnValue(Columns.PayMethod, value); }
		}
		  
		[XmlAttribute("AdminActive")]
		[Bindable(true)]
		public bool? AdminActive 
		{
			get { return GetColumnValue<bool?>(Columns.AdminActive); }
			set { SetColumnValue(Columns.AdminActive, value); }
		}
		  
		[XmlAttribute("NetworkKey")]
		[Bindable(true)]
		public int? NetworkKey 
		{
			get { return GetColumnValue<int?>(Columns.NetworkKey); }
			set { SetColumnValue(Columns.NetworkKey, value); }
		}
		  
		[XmlAttribute("MobileSales")]
		[Bindable(true)]
		public bool? MobileSales 
		{
			get { return GetColumnValue<bool?>(Columns.MobileSales); }
			set { SetColumnValue(Columns.MobileSales, value); }
		}
		  
		[XmlAttribute("PermRequestToken")]
		[Bindable(true)]
		public string PermRequestToken 
		{
			get { return GetColumnValue<string>(Columns.PermRequestToken); }
			set { SetColumnValue(Columns.PermRequestToken, value); }
		}
		  
		[XmlAttribute("PermVerificationCode")]
		[Bindable(true)]
		public string PermVerificationCode 
		{
			get { return GetColumnValue<string>(Columns.PermVerificationCode); }
			set { SetColumnValue(Columns.PermVerificationCode, value); }
		}
		  
		[XmlAttribute("Dodirectpayment")]
		[Bindable(true)]
		public bool? Dodirectpayment 
		{
			get { return GetColumnValue<bool?>(Columns.Dodirectpayment); }
			set { SetColumnValue(Columns.Dodirectpayment, value); }
		}
		  
		[XmlAttribute("PfConfirmation")]
		[Bindable(true)]
		public string PfConfirmation 
		{
			get { return GetColumnValue<string>(Columns.PfConfirmation); }
			set { SetColumnValue(Columns.PfConfirmation, value); }
		}
		  
		[XmlAttribute("PermAccessToken")]
		[Bindable(true)]
		public string PermAccessToken 
		{
			get { return GetColumnValue<string>(Columns.PermAccessToken); }
			set { SetColumnValue(Columns.PermAccessToken, value); }
		}
		  
		[XmlAttribute("PermAccessTokenSecret")]
		[Bindable(true)]
		public string PermAccessTokenSecret 
		{
			get { return GetColumnValue<string>(Columns.PermAccessTokenSecret); }
			set { SetColumnValue(Columns.PermAccessTokenSecret, value); }
		}
		  
		[XmlAttribute("ThirdPartyPayPal")]
		[Bindable(true)]
		public bool? ThirdPartyPayPal 
		{
			get { return GetColumnValue<bool?>(Columns.ThirdPartyPayPal); }
			set { SetColumnValue(Columns.ThirdPartyPayPal, value); }
		}
		  
		[XmlAttribute("Descriptor")]
		[Bindable(true)]
		public string Descriptor 
		{
			get { return GetColumnValue<string>(Columns.Descriptor); }
			set { SetColumnValue(Columns.Descriptor, value); }
		}
		  
		[XmlAttribute("Payerid")]
		[Bindable(true)]
		public string Payerid 
		{
			get { return GetColumnValue<string>(Columns.Payerid); }
			set { SetColumnValue(Columns.Payerid, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public Eventomatic_DB.EventXCollection Events()
		{
			return new Eventomatic_DB.EventXCollection().Where(EventX.Columns.ResourceKey, ResourceKey).Load();
		}
		public Eventomatic_DB.FbUsersResourceCollection FbUsersResourceRecords()
		{
			return new Eventomatic_DB.FbUsersResourceCollection().Where(FbUsersResource.Columns.ResourceKey, ResourceKey).Load();
		}
		public Eventomatic_DB.LogActivityCollection LogActivities()
		{
			return new Eventomatic_DB.LogActivityCollection().Where(LogActivity.Columns.ResourceKey, ResourceKey).Load();
		}
		public Eventomatic_DB.ResourceReadingOtherCollection ResourceReadingOthers()
		{
			return new Eventomatic_DB.ResourceReadingOtherCollection().Where(ResourceReadingOther.Columns.ResourceKey, ResourceKey).Load();
		}
		public Eventomatic_DB.ResourceReadingOtherCollection ResourceReadingOthersFromResource()
		{
			return new Eventomatic_DB.ResourceReadingOtherCollection().Where(ResourceReadingOther.Columns.ResourceKeyReading, ResourceKey).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a FbUser ActiveRecord object related to this Resource
		/// 
		/// </summary>
		public Eventomatic_DB.FbUser FbUser
		{
			get { return Eventomatic_DB.FbUser.FetchByID(this.FBCreator); }
			set { SetColumnValue("FBCreator", value.FBid); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varGroupName,long varFBCreator,DateTime varSignedUp,DateTime varLastChange,string varDesiredCurrency,string varEmailPaypal,decimal? varServiceFeePercentage,decimal? varServiceFeeCents,decimal? varServiceFeeMax,bool? varDemo,string varStoreDescription,string varStoreContact,string varStoreTitle,int? varPayMethod,bool? varAdminActive,int? varNetworkKey,bool? varMobileSales,string varPermRequestToken,string varPermVerificationCode,bool? varDodirectpayment,string varPfConfirmation,string varPermAccessToken,string varPermAccessTokenSecret,bool? varThirdPartyPayPal,string varDescriptor,string varPayerid)
		{
			Resource item = new Resource();
			
			item.GroupName = varGroupName;
			
			item.FBCreator = varFBCreator;
			
			item.SignedUp = varSignedUp;
			
			item.LastChange = varLastChange;
			
			item.DesiredCurrency = varDesiredCurrency;
			
			item.EmailPaypal = varEmailPaypal;
			
			item.ServiceFeePercentage = varServiceFeePercentage;
			
			item.ServiceFeeCents = varServiceFeeCents;
			
			item.ServiceFeeMax = varServiceFeeMax;
			
			item.Demo = varDemo;
			
			item.StoreDescription = varStoreDescription;
			
			item.StoreContact = varStoreContact;
			
			item.StoreTitle = varStoreTitle;
			
			item.PayMethod = varPayMethod;
			
			item.AdminActive = varAdminActive;
			
			item.NetworkKey = varNetworkKey;
			
			item.MobileSales = varMobileSales;
			
			item.PermRequestToken = varPermRequestToken;
			
			item.PermVerificationCode = varPermVerificationCode;
			
			item.Dodirectpayment = varDodirectpayment;
			
			item.PfConfirmation = varPfConfirmation;
			
			item.PermAccessToken = varPermAccessToken;
			
			item.PermAccessTokenSecret = varPermAccessTokenSecret;
			
			item.ThirdPartyPayPal = varThirdPartyPayPal;
			
			item.Descriptor = varDescriptor;
			
			item.Payerid = varPayerid;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varResourceKey,string varGroupName,long varFBCreator,DateTime varSignedUp,DateTime varLastChange,string varDesiredCurrency,string varEmailPaypal,decimal? varServiceFeePercentage,decimal? varServiceFeeCents,decimal? varServiceFeeMax,bool? varDemo,string varStoreDescription,string varStoreContact,string varStoreTitle,int? varPayMethod,bool? varAdminActive,int? varNetworkKey,bool? varMobileSales,string varPermRequestToken,string varPermVerificationCode,bool? varDodirectpayment,string varPfConfirmation,string varPermAccessToken,string varPermAccessTokenSecret,bool? varThirdPartyPayPal,string varDescriptor,string varPayerid)
		{
			Resource item = new Resource();
			
				item.ResourceKey = varResourceKey;
			
				item.GroupName = varGroupName;
			
				item.FBCreator = varFBCreator;
			
				item.SignedUp = varSignedUp;
			
				item.LastChange = varLastChange;
			
				item.DesiredCurrency = varDesiredCurrency;
			
				item.EmailPaypal = varEmailPaypal;
			
				item.ServiceFeePercentage = varServiceFeePercentage;
			
				item.ServiceFeeCents = varServiceFeeCents;
			
				item.ServiceFeeMax = varServiceFeeMax;
			
				item.Demo = varDemo;
			
				item.StoreDescription = varStoreDescription;
			
				item.StoreContact = varStoreContact;
			
				item.StoreTitle = varStoreTitle;
			
				item.PayMethod = varPayMethod;
			
				item.AdminActive = varAdminActive;
			
				item.NetworkKey = varNetworkKey;
			
				item.MobileSales = varMobileSales;
			
				item.PermRequestToken = varPermRequestToken;
			
				item.PermVerificationCode = varPermVerificationCode;
			
				item.Dodirectpayment = varDodirectpayment;
			
				item.PfConfirmation = varPfConfirmation;
			
				item.PermAccessToken = varPermAccessToken;
			
				item.PermAccessTokenSecret = varPermAccessTokenSecret;
			
				item.ThirdPartyPayPal = varThirdPartyPayPal;
			
				item.Descriptor = varDescriptor;
			
				item.Payerid = varPayerid;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ResourceKeyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn GroupNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn FBCreatorColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SignedUpColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn LastChangeColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn DesiredCurrencyColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn EmailPaypalColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn ServiceFeePercentageColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn ServiceFeeCentsColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn ServiceFeeMaxColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn DemoColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn StoreDescriptionColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn StoreContactColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn StoreTitleColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn PayMethodColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn AdminActiveColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn NetworkKeyColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn MobileSalesColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn PermRequestTokenColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn PermVerificationCodeColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn DodirectpaymentColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn PfConfirmationColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn PermAccessTokenColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn PermAccessTokenSecretColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn ThirdPartyPayPalColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn DescriptorColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        public static TableSchema.TableColumn PayeridColumn
        {
            get { return Schema.Columns[26]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ResourceKey = @"Resource_Key";
			 public static string GroupName = @"Group_Name";
			 public static string FBCreator = @"FBCreator";
			 public static string SignedUp = @"Signed_Up";
			 public static string LastChange = @"Last_Change";
			 public static string DesiredCurrency = @"Desired_Currency";
			 public static string EmailPaypal = @"Email_Paypal";
			 public static string ServiceFeePercentage = @"Service_Fee_Percentage";
			 public static string ServiceFeeCents = @"Service_Fee_Cents";
			 public static string ServiceFeeMax = @"Service_Fee_Max";
			 public static string Demo = @"Demo";
			 public static string StoreDescription = @"Store_Description";
			 public static string StoreContact = @"Store_Contact";
			 public static string StoreTitle = @"Store_Title";
			 public static string PayMethod = @"Pay_Method";
			 public static string AdminActive = @"Admin_Active";
			 public static string NetworkKey = @"Network_Key";
			 public static string MobileSales = @"Mobile_Sales";
			 public static string PermRequestToken = @"Perm_Request_Token";
			 public static string PermVerificationCode = @"Perm_Verification_Code";
			 public static string Dodirectpayment = @"dodirectpayment";
			 public static string PfConfirmation = @"pfConfirmation";
			 public static string PermAccessToken = @"Perm_Access_Token";
			 public static string PermAccessTokenSecret = @"Perm_Access_Token_Secret";
			 public static string ThirdPartyPayPal = @"ThirdPartyPayPal";
			 public static string Descriptor = @"Descriptor";
			 public static string Payerid = @"payerid";
						
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