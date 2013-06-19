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
	#region Tables Struct
	public partial struct Tables
	{
		
		public static readonly string AspnetApplication = @"aspnet_Applications";
        
		public static readonly string AspnetMembership = @"aspnet_Membership";
        
		public static readonly string AspnetPath = @"aspnet_Paths";
        
		public static readonly string AspnetPersonalizationAllUser = @"aspnet_PersonalizationAllUsers";
        
		public static readonly string AspnetPersonalizationPerUser = @"aspnet_PersonalizationPerUser";
        
		public static readonly string AspnetProfile = @"aspnet_Profile";
        
		public static readonly string AspnetRole = @"aspnet_Roles";
        
		public static readonly string AspnetSchemaVersion = @"aspnet_SchemaVersions";
        
		public static readonly string AspnetUser = @"aspnet_Users";
        
		public static readonly string AspnetUsersInRole = @"aspnet_UsersInRoles";
        
		public static readonly string AspnetWebEventEvent = @"aspnet_WebEvent_Events";
        
		public static readonly string AttendeeList = @"Attendee_List";
        
		public static readonly string BillingAgreement = @"Billing_Agreement";
        
		public static readonly string BillingPayment = @"Billing_Payment";
        
		public static readonly string CCError = @"CCErrors";
        
		public static readonly string EventX = @"Events";
        
		public static readonly string EventsFundraiserPdf = @"Events_Fundraiser_PDF";
        
		public static readonly string FbUser = @"FB_Users";
        
		public static readonly string FbUsersDemoTicket = @"FB_Users_DemoTicket";
        
		public static readonly string FbUsersEmail = @"FB_Users_Email";
        
		public static readonly string FbUsersPage = @"FB_Users_Pages";
        
		public static readonly string FbUsersResource = @"FB_Users_Resource";
        
		public static readonly string FbUsersSeller = @"FB_Users_Sellers";
        
		public static readonly string FbUsersSellersCustomize = @"FB_Users_Sellers_Customize";
        
		public static readonly string FbUsersSellersPf = @"FB_Users_Sellers_PF";
        
		public static readonly string InfoCountry = @"InfoCountry";
        
		public static readonly string InfoMerchant = @"InfoMerchants";
        
		public static readonly string InfoRegion = @"InfoRegion";
        
		public static readonly string InfoTimezone = @"InfoTimezones";
        
		public static readonly string LogActivity = @"Log_Activities";
        
		public static readonly string LogActivitiesPossibility = @"Log_Activities_Possibilities";
        
		public static readonly string LogFbUser = @"Log_FB_Users";
        
		public static readonly string LogPostAuthorizeRemove = @"Log_Post_Authorize_Remove";
        
		public static readonly string Network = @"Networks";
        
		public static readonly string PayForward = @"Pay_Forward";
        
		public static readonly string PaypalConfirmation = @"Paypal_Confirmation";
        
		public static readonly string PayPalDemoPay = @"PayPal_DemoPay";
        
		public static readonly string PayPalInfo = @"PayPal_Info";
        
		public static readonly string PfReferral = @"PF_Referral";
        
		public static readonly string Question = @"Question";
        
		public static readonly string QuestionsAnswered = @"Questions_Answered";
        
		public static readonly string QuestionsDropDown = @"Questions_DropDowns";
        
		public static readonly string Resource = @"Resource";
        
		public static readonly string ResourceReadingOther = @"Resource_Reading_Others";
        
		public static readonly string Test = @"Test";
        
		public static readonly string Ticket = @"Tickets";
        
		public static readonly string TicketsPurchased = @"Tickets_Purchased";
        
		public static readonly string TransactionError = @"Transaction_Errors";
        
		public static readonly string TransactionStatusType = @"Transaction_Status_Type";
        
		public static readonly string Transaction = @"Transactions";
        
		public static readonly string TransactionsOut = @"Transactions_Out";
        
	}
	#endregion
    #region Schemas
    public partial class Schemas {
		
		public static TableSchema.Table AspnetApplication
		{
            get { return DataService.GetSchema("aspnet_Applications", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetMembership
		{
            get { return DataService.GetSchema("aspnet_Membership", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetPath
		{
            get { return DataService.GetSchema("aspnet_Paths", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetPersonalizationAllUser
		{
            get { return DataService.GetSchema("aspnet_PersonalizationAllUsers", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetPersonalizationPerUser
		{
            get { return DataService.GetSchema("aspnet_PersonalizationPerUser", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetProfile
		{
            get { return DataService.GetSchema("aspnet_Profile", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetRole
		{
            get { return DataService.GetSchema("aspnet_Roles", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetSchemaVersion
		{
            get { return DataService.GetSchema("aspnet_SchemaVersions", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetUser
		{
            get { return DataService.GetSchema("aspnet_Users", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetUsersInRole
		{
            get { return DataService.GetSchema("aspnet_UsersInRoles", "Eventomatic"); }
		}
        
		public static TableSchema.Table AspnetWebEventEvent
		{
            get { return DataService.GetSchema("aspnet_WebEvent_Events", "Eventomatic"); }
		}
        
		public static TableSchema.Table AttendeeList
		{
            get { return DataService.GetSchema("Attendee_List", "Eventomatic"); }
		}
        
		public static TableSchema.Table BillingAgreement
		{
            get { return DataService.GetSchema("Billing_Agreement", "Eventomatic"); }
		}
        
		public static TableSchema.Table BillingPayment
		{
            get { return DataService.GetSchema("Billing_Payment", "Eventomatic"); }
		}
        
		public static TableSchema.Table CCError
		{
            get { return DataService.GetSchema("CCErrors", "Eventomatic"); }
		}
        
		public static TableSchema.Table EventX
		{
            get { return DataService.GetSchema("Events", "Eventomatic"); }
		}
        
		public static TableSchema.Table EventsFundraiserPdf
		{
            get { return DataService.GetSchema("Events_Fundraiser_PDF", "Eventomatic"); }
		}
        
		public static TableSchema.Table FbUser
		{
            get { return DataService.GetSchema("FB_Users", "Eventomatic"); }
		}
        
		public static TableSchema.Table FbUsersDemoTicket
		{
            get { return DataService.GetSchema("FB_Users_DemoTicket", "Eventomatic"); }
		}
        
		public static TableSchema.Table FbUsersEmail
		{
            get { return DataService.GetSchema("FB_Users_Email", "Eventomatic"); }
		}
        
		public static TableSchema.Table FbUsersPage
		{
            get { return DataService.GetSchema("FB_Users_Pages", "Eventomatic"); }
		}
        
		public static TableSchema.Table FbUsersResource
		{
            get { return DataService.GetSchema("FB_Users_Resource", "Eventomatic"); }
		}
        
		public static TableSchema.Table FbUsersSeller
		{
            get { return DataService.GetSchema("FB_Users_Sellers", "Eventomatic"); }
		}
        
		public static TableSchema.Table FbUsersSellersCustomize
		{
            get { return DataService.GetSchema("FB_Users_Sellers_Customize", "Eventomatic"); }
		}
        
		public static TableSchema.Table FbUsersSellersPf
		{
            get { return DataService.GetSchema("FB_Users_Sellers_PF", "Eventomatic"); }
		}
        
		public static TableSchema.Table InfoCountry
		{
            get { return DataService.GetSchema("InfoCountry", "Eventomatic"); }
		}
        
		public static TableSchema.Table InfoMerchant
		{
            get { return DataService.GetSchema("InfoMerchants", "Eventomatic"); }
		}
        
		public static TableSchema.Table InfoRegion
		{
            get { return DataService.GetSchema("InfoRegion", "Eventomatic"); }
		}
        
		public static TableSchema.Table InfoTimezone
		{
            get { return DataService.GetSchema("InfoTimezones", "Eventomatic"); }
		}
        
		public static TableSchema.Table LogActivity
		{
            get { return DataService.GetSchema("Log_Activities", "Eventomatic"); }
		}
        
		public static TableSchema.Table LogActivitiesPossibility
		{
            get { return DataService.GetSchema("Log_Activities_Possibilities", "Eventomatic"); }
		}
        
		public static TableSchema.Table LogFbUser
		{
            get { return DataService.GetSchema("Log_FB_Users", "Eventomatic"); }
		}
        
		public static TableSchema.Table LogPostAuthorizeRemove
		{
            get { return DataService.GetSchema("Log_Post_Authorize_Remove", "Eventomatic"); }
		}
        
		public static TableSchema.Table Network
		{
            get { return DataService.GetSchema("Networks", "Eventomatic"); }
		}
        
		public static TableSchema.Table PayForward
		{
            get { return DataService.GetSchema("Pay_Forward", "Eventomatic"); }
		}
        
		public static TableSchema.Table PaypalConfirmation
		{
            get { return DataService.GetSchema("Paypal_Confirmation", "Eventomatic"); }
		}
        
		public static TableSchema.Table PayPalDemoPay
		{
            get { return DataService.GetSchema("PayPal_DemoPay", "Eventomatic"); }
		}
        
		public static TableSchema.Table PayPalInfo
		{
            get { return DataService.GetSchema("PayPal_Info", "Eventomatic"); }
		}
        
		public static TableSchema.Table PfReferral
		{
            get { return DataService.GetSchema("PF_Referral", "Eventomatic"); }
		}
        
		public static TableSchema.Table Question
		{
            get { return DataService.GetSchema("Question", "Eventomatic"); }
		}
        
		public static TableSchema.Table QuestionsAnswered
		{
            get { return DataService.GetSchema("Questions_Answered", "Eventomatic"); }
		}
        
		public static TableSchema.Table QuestionsDropDown
		{
            get { return DataService.GetSchema("Questions_DropDowns", "Eventomatic"); }
		}
        
		public static TableSchema.Table Resource
		{
            get { return DataService.GetSchema("Resource", "Eventomatic"); }
		}
        
		public static TableSchema.Table ResourceReadingOther
		{
            get { return DataService.GetSchema("Resource_Reading_Others", "Eventomatic"); }
		}
        
		public static TableSchema.Table Test
		{
            get { return DataService.GetSchema("Test", "Eventomatic"); }
		}
        
		public static TableSchema.Table Ticket
		{
            get { return DataService.GetSchema("Tickets", "Eventomatic"); }
		}
        
		public static TableSchema.Table TicketsPurchased
		{
            get { return DataService.GetSchema("Tickets_Purchased", "Eventomatic"); }
		}
        
		public static TableSchema.Table TransactionError
		{
            get { return DataService.GetSchema("Transaction_Errors", "Eventomatic"); }
		}
        
		public static TableSchema.Table TransactionStatusType
		{
            get { return DataService.GetSchema("Transaction_Status_Type", "Eventomatic"); }
		}
        
		public static TableSchema.Table Transaction
		{
            get { return DataService.GetSchema("Transactions", "Eventomatic"); }
		}
        
		public static TableSchema.Table TransactionsOut
		{
            get { return DataService.GetSchema("Transactions_Out", "Eventomatic"); }
		}
        
	
    }
    #endregion
    #region View Struct
    public partial struct Views 
    {
		
		public static readonly string VwAspnetMembershipUser = @"vw_aspnet_MembershipUsers";
        
		public static readonly string VwAspnetProfile = @"vw_aspnet_Profiles";
        
		public static readonly string VwAspnetRole = @"vw_aspnet_Roles";
        
		public static readonly string VwAspnetUsersInRole = @"vw_aspnet_UsersInRoles";
        
		public static readonly string VwAspnetWebPartStatePath = @"vw_aspnet_WebPartState_Paths";
        
		public static readonly string VwAspnetWebPartStateShared = @"vw_aspnet_WebPartState_Shared";
        
		public static readonly string VwAspnetWebPartStateUser = @"vw_aspnet_WebPartState_User";
        
		public static readonly string VwAttendeeListTransaction = @"vw_Attendee_List_Transactions";
        
    }
    #endregion
    
    #region Query Factories
	public static partial class DB
	{
        public static DataProvider _provider = DataService.Providers["Eventomatic"];
        static ISubSonicRepository _repository;
        public static ISubSonicRepository Repository 
        {
            get 
            {
                if (_repository == null)
                    return new SubSonicRepository(_provider);
                return _repository; 
            }
            set { _repository = value; }
        }
        public static Select SelectAllColumnsFrom<T>() where T : RecordBase<T>, new()
	    {
            return Repository.SelectAllColumnsFrom<T>();
	    }
	    public static Select Select()
	    {
            return Repository.Select();
	    }
	    
		public static Select Select(params string[] columns)
		{
            return Repository.Select(columns);
        }
	    
		public static Select Select(params Aggregate[] aggregates)
		{
            return Repository.Select(aggregates);
        }
   
	    public static Update Update<T>() where T : RecordBase<T>, new()
	    {
            return Repository.Update<T>();
	    }
	    
	    public static Insert Insert()
	    {
            return Repository.Insert();
	    }
	    
	    public static Delete Delete()
	    {
            return Repository.Delete();
	    }
	    
	    public static InlineQuery Query()
	    {
            return Repository.Query();
	    }
	    	    
	    
	}
    #endregion
    
}
#region Databases
public partial struct Databases 
{
	
	public static readonly string Eventomatic = @"Eventomatic";
    
}
#endregion