/****** Object:  DatabaseRole [aspnet_Membership_BasicAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Membership_BasicAccess' AND type = 'R')
CREATE ROLE [aspnet_Membership_BasicAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Membership_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Membership_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_Membership_FullAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Membership_ReportingAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Membership_ReportingAccess' AND type = 'R')
CREATE ROLE [aspnet_Membership_ReportingAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Personalization_BasicAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Personalization_BasicAccess' AND type = 'R')
CREATE ROLE [aspnet_Personalization_BasicAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Personalization_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Personalization_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_Personalization_FullAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Personalization_ReportingAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Personalization_ReportingAccess' AND type = 'R')
CREATE ROLE [aspnet_Personalization_ReportingAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Profile_BasicAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Profile_BasicAccess' AND type = 'R')
CREATE ROLE [aspnet_Profile_BasicAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Profile_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Profile_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_Profile_FullAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Profile_ReportingAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Profile_ReportingAccess' AND type = 'R')
CREATE ROLE [aspnet_Profile_ReportingAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Roles_BasicAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Roles_BasicAccess' AND type = 'R')
CREATE ROLE [aspnet_Roles_BasicAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Roles_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Roles_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_Roles_FullAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_Roles_ReportingAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Roles_ReportingAccess' AND type = 'R')
CREATE ROLE [aspnet_Roles_ReportingAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [aspnet_WebEvent_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_WebEvent_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_WebEvent_FullAccess] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [GroupStore_Prod]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'GroupStore_Prod' AND type = 'R')
CREATE ROLE [GroupStore_Prod] AUTHORIZATION [dbo]
/****** Object:  DatabaseRole [GroupStore_Prod2]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'GroupStore_Prod2' AND type = 'R')
CREATE ROLE [GroupStore_Prod2] AUTHORIZATION [dbo]
/****** Object:  Schema [GroupStore_Prod2]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'GroupStore_Prod2')
EXEC sys.sp_executesql N'CREATE SCHEMA [GroupStore_Prod2] AUTHORIZATION [GroupStore_Prod2]'

/****** Object:  Schema [GroupStore_Prod]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'GroupStore_Prod')
EXEC sys.sp_executesql N'CREATE SCHEMA [GroupStore_Prod] AUTHORIZATION [GroupStore_Prod]'

/****** Object:  Schema [aspnet_WebEvent_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_WebEvent_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_WebEvent_FullAccess] AUTHORIZATION [aspnet_WebEvent_FullAccess]'

/****** Object:  Schema [aspnet_Roles_ReportingAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Roles_ReportingAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Roles_ReportingAccess] AUTHORIZATION [aspnet_Roles_ReportingAccess]'

/****** Object:  Schema [aspnet_Roles_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Roles_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Roles_FullAccess] AUTHORIZATION [aspnet_Roles_FullAccess]'

/****** Object:  Schema [aspnet_Roles_BasicAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Roles_BasicAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Roles_BasicAccess] AUTHORIZATION [aspnet_Roles_BasicAccess]'

/****** Object:  Schema [aspnet_Profile_ReportingAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Profile_ReportingAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Profile_ReportingAccess] AUTHORIZATION [aspnet_Profile_ReportingAccess]'

/****** Object:  Schema [aspnet_Profile_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Profile_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Profile_FullAccess] AUTHORIZATION [aspnet_Profile_FullAccess]'

/****** Object:  Schema [aspnet_Profile_BasicAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Profile_BasicAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Profile_BasicAccess] AUTHORIZATION [aspnet_Profile_BasicAccess]'

/****** Object:  Schema [aspnet_Personalization_ReportingAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Personalization_ReportingAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Personalization_ReportingAccess] AUTHORIZATION [aspnet_Personalization_ReportingAccess]'

/****** Object:  Schema [aspnet_Personalization_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Personalization_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Personalization_FullAccess] AUTHORIZATION [aspnet_Personalization_FullAccess]'

/****** Object:  Schema [aspnet_Personalization_BasicAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Personalization_BasicAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Personalization_BasicAccess] AUTHORIZATION [aspnet_Personalization_BasicAccess]'

/****** Object:  Schema [aspnet_Membership_ReportingAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Membership_ReportingAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Membership_ReportingAccess] AUTHORIZATION [aspnet_Membership_ReportingAccess]'

/****** Object:  Schema [aspnet_Membership_FullAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Membership_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Membership_FullAccess] AUTHORIZATION [aspnet_Membership_FullAccess]'

/****** Object:  Schema [aspnet_Membership_BasicAccess]    Script Date: 03/31/2011 18:02:03 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Membership_BasicAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Membership_BasicAccess] AUTHORIZATION [aspnet_Membership_BasicAccess]'

/****** Object:  Table [dbo].[Paypal_Confirmation]    Script Date: 03/31/2011 18:02:03 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Paypal_Confirmation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Paypal_Confirmation](
	[Paypal_Confirmation_Key] [int] IDENTITY(1,1) NOT NULL,
	[Paypal_Email] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Amount_Sent] [money] NOT NULL,
	[Confirmed] [bit] NULL,
	[Date_Sent] [datetime] NOT NULL
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Networks]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Networks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Networks](
	[Network_Key] [int] IDENTITY(1,1) NOT NULL,
	[Network_Name] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Networks] PRIMARY KEY CLUSTERED 
(
	[Network_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Log_Post_Authorize_Remove]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Log_Post_Authorize_Remove]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Log_Post_Authorize_Remove](
	[Log_Post_Authorize_Remove_Key] [int] IDENTITY(1,1) NOT NULL,
	[Authorize_Remove] [bit] NOT NULL,
	[Signature_Linked] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Signature] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FBid] [bigint] NULL,
	[Last_Change] [datetime] NULL,
 CONSTRAINT [PK_Log_Post_Authorize_Remove] PRIMARY KEY CLUSTERED 
(
	[Log_Post_Authorize_Remove_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Log_FB_Users]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Log_FB_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Log_FB_Users](
	[Log_FB_Users_Key] [int] IDENTITY(1,1) NOT NULL,
	[FBid] [bigint] NOT NULL,
	[Page] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Event_Key] [int] NULL,
	[Resource_Key] [int] NULL,
	[Last_Change] [datetime] NOT NULL,
	[IP_Address] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Log_Activities_Possibilities]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Log_Activities_Possibilities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Log_Activities_Possibilities](
	[Log_Activities_Possibilities_Key] [int] NOT NULL,
	[Activity] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Log_Activities_Possibilities] PRIMARY KEY CLUSTERED 
(
	[Log_Activities_Possibilities_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[InfoTimezones]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InfoTimezones]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InfoTimezones](
	[Timezones_key] [int] NOT NULL,
	[Timezones_Text] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Timezones_Value] [decimal](18, 0) NULL,
	[Timezones_Textshort] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Timezones] PRIMARY KEY CLUSTERED 
(
	[Timezones_key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[InfoRegion]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InfoRegion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InfoRegion](
	[Region_Key] [int] NOT NULL,
	[Region_Value] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Region_Text] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country_Key] [int] NOT NULL,
 CONSTRAINT [PK_InfoRegion] PRIMARY KEY CLUSTERED 
(
	[Region_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[InfoCountry]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InfoCountry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InfoCountry](
	[Country_Key] [int] NOT NULL,
	[Country_Value] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country_Text] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_InfoCountry] PRIMARY KEY CLUSTERED 
(
	[Country_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[FB_Users_Sellers_Customize]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FB_Users_Sellers_Customize]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FB_Users_Sellers_Customize](
	[FBid] [bigint] NOT NULL,
	[Event_Key] [int] NOT NULL,
	[ShowGoals] [bit] NULL,
	[GoalAmount] [money] NULL,
	[Personal_Message] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[FB_Users_Sellers]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FB_Users_Sellers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FB_Users_Sellers](
	[FB_Users_Sellers_Key] [int] IDENTITY(1,1) NOT NULL,
	[FBid] [bigint] NOT NULL,
	[Event_Key] [int] NOT NULL,
	[Ticket_Key] [int] NOT NULL,
	[ShowGoals] [bit] NULL,
	[GoalAmount] [money] NULL,
 CONSTRAINT [PK_FB_Users_Sellers] PRIMARY KEY CLUSTERED 
(
	[FB_Users_Sellers_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[CCErrors]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CCErrors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CCErrors](
	[CCErrors_Key] [bigint] IDENTITY(1,1) NOT NULL,
	[TheError] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Error_Date] [datetime] NOT NULL,
	[Tx_Key] [int] NOT NULL,
	[CC_Mass] [int] NULL,
	[Amount] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Transaction_Status_Type]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transaction_Status_Type]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transaction_Status_Type](
	[Transaction_Status_Type_Key] [int] IDENTITY(1,1) NOT NULL,
	[Transaction_Status_Type] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Transaction_Status_Type] PRIMARY KEY CLUSTERED 
(
	[Transaction_Status_Type_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Transaction_Errors]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transaction_Errors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transaction_Errors](
	[Transaction_Errors_Key] [int] IDENTITY(1,1) NOT NULL,
	[Transaction_Errors_Log] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Last_Updated] [datetime] NULL,
 CONSTRAINT [PK_Transaction_Errors] PRIMARY KEY CLUSTERED 
(
	[Transaction_Errors_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Transactions_Out]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions_Out]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transactions_Out](
	[Tx_Out_Key] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [money] NOT NULL,
	[Email_Recipient] [nchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[tx_date] [datetime] NOT NULL,
	[Resource_Key] [int] NOT NULL,
	[Event_Key] [int] NOT NULL,
	[Paypal_Fee] [money] NOT NULL,
	[Currency] [nchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FBid] [bigint] NOT NULL,
	[type] [int] NOT NULL,
 CONSTRAINT [PK_Transactions_Out] PRIMARY KEY CLUSTERED 
(
	[Tx_Out_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Questions_DropDowns]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Questions_DropDowns]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Questions_DropDowns](
	[Question_Key] [int] NOT NULL,
	[Question_DD_Text] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Question_DD_Value] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Test]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Test]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Test](
	[Test_Key] [int] IDENTITY(1,1) NOT NULL,
	[Test_Text] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Test_update] [datetime] NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[Test_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Applications]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LoweredApplicationName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__aspnet_Applicati__173876EA] PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ__aspnet_Applicati__15502E78] UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ__aspnet_Applicati__164452B1] UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END

/****** Object:  Index [aspnet_Applications_Index]    Script Date: 03/31/2011 18:02:04 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Applications]') AND name = N'aspnet_Applications_Index')
CREATE CLUSTERED INDEX [aspnet_Applications_Index] ON [dbo].[aspnet_Applications] 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
/****** Object:  Table [dbo].[FB_Users]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FB_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FB_Users](
	[FBid] [bigint] NOT NULL,
	[First_Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Last_Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Signed_Up] [datetime] NOT NULL,
	[Last_Change] [datetime] NOT NULL,
	[Resource_Key] [int] NULL,
	[Admin] [bit] NULL,
	[IP_Address] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Session_Key] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Access_Token] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Referral] [bigint] NULL,
	[Referral_Email] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Referral_Rate] [money] NULL,
	[IsSpy] [bit] NULL,
	[GetNavigate] [bit] NULL,
 CONSTRAINT [PK_FB_Users] PRIMARY KEY CLUSTERED 
(
	[FBid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Events_Fundraiser_PDF]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events_Fundraiser_PDF]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Events_Fundraiser_PDF](
	[Event_Key] [int] NOT NULL,
	[Receipt_Number] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Charity_Info] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Charity_Number] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Personal_Message] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Personal_Message_Signature] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[File_Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[bcc_email] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ypossig] [int] NOT NULL,
	[Email_Message] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[aspnet_WebEvent_Events]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_WebEvent_Events]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_WebEvent_Events](
	[EventId] [char](32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EventTimeUtc] [datetime] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[EventType] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EventSequence] [decimal](19, 0) NOT NULL,
	[EventOccurrence] [decimal](19, 0) NOT NULL,
	[EventCode] [int] NOT NULL,
	[EventDetailCode] [int] NOT NULL,
	[Message] [nvarchar](1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ApplicationPath] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ApplicationVirtualPath] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MachineName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RequestUrl] [nvarchar](1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExceptionType] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Details] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__aspnet_WebEvent___09DE7BCC] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RestorePermissions]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Setup_RestorePermissions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Setup_RestorePermissions]
    @name   sysname
AS
BEGIN
    DECLARE @object sysname
    DECLARE @protectType char(10)
    DECLARE @action varchar(60)
    DECLARE @grantee sysname
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT Object, ProtectType, [Action], Grantee FROM #aspnet_Permissions where Object = @name

    OPEN c1

    FETCH c1 INTO @object, @protectType, @action, @grantee
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = @protectType + '' '' + @action + '' on '' + @object + '' TO ['' + @grantee + '']''
        EXEC (@cmd)
        FETCH c1 INTO @object, @protectType, @action, @grantee
    END

    CLOSE c1
    DEALLOCATE c1
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RemoveAllRoleMembers]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Setup_RemoveAllRoleMembers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Setup_RemoveAllRoleMembers]
    @name   sysname
AS
BEGIN
    CREATE TABLE #aspnet_RoleMembers
    (
        Group_name      sysname,
        Group_id        smallint,
        Users_in_group  sysname,
        User_id         smallint
    )

    INSERT INTO #aspnet_RoleMembers
    EXEC sp_helpuser @name

    DECLARE @user_id smallint
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT User_id FROM #aspnet_RoleMembers

    OPEN c1

    FETCH c1 INTO @user_id
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = ''EXEC sp_droprolemember '' + '''''''' + @name + '''''', '''''' + USER_NAME(@user_id) + ''''''''
        EXEC (@cmd)
        FETCH c1 INTO @user_id
    END

    CLOSE c1
    DEALLOCATE c1
END' 
END
/****** Object:  Table [dbo].[aspnet_SchemaVersions]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_SchemaVersions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_SchemaVersions](
	[Feature] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompatibleSchemaVersion] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsCurrentVersion] [bit] NOT NULL,
 CONSTRAINT [PK__aspnet_SchemaVer__1DE57479] PRIMARY KEY CLUSTERED 
(
	[Feature] ASC,
	[CompatibleSchemaVersion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  StoredProcedure [dbo].[View_LiveorTrial]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_LiveorTrial]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_LiveorTrial]
	
AS
	/* SET NOCOUNT ON */
	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Resource_Network]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Resource_Network]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Resource_Network]	
@Resource_Key int
AS
BEGIN

	SELECT *
	FROM Networks	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_ShowGoals]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_ShowGoals]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_ShowGoals]
	@Event_Key int,
	@fbid bigint
AS
BEGIN

SELECT 
	Top 1 ShowGoals,
	GoalAmount = Convert(decimal(20,2),GoalAmount)
	FROM FB_Users_Sellers_Customize
	WHERE Event_Key = @Event_Key 
	AND FBid = @fbid
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_Sellers]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_Sellers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Ticket_Sellers]
	@Event_Key int,
	@Ticket_Key int
AS
BEGIN
	SELECT *	
	FROM FB_Users_Sellers
	WHERE Event_Key = @Event_Key	
	AND Ticket_Key = @Ticket_Key	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_InfoTimezones]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_InfoTimezones]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_InfoTimezones]	
	@Timezones_Key int
AS
BEGIN
	SELECT *
	FROM InfoTimezones	
	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_InfoRegion]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_InfoRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_InfoRegion]	
	@Country_Key int
AS
BEGIN
	SELECT *
	FROM InfoRegion
	WHERE Country_Key = @Country_Key
	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_InfoCountry]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_InfoCountry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_InfoCountry]	
	@test int
AS
BEGIN
	SELECT *
	FROM InfoCountry

	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_IfAdmin]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_IfAdmin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_IfAdmin]
	@FBid bigint
AS
SELECT Admin 
FROM FB_Users 
WHERE FBid = @FBid' 
END
/****** Object:  StoredProcedure [dbo].[View_Question_DropDown]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Question_DropDown]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Question_DropDown]
	@Question_Key int
AS
BEGIN
	SELECT 	*		
	FROM Questions_DropDowns
	WHERE Question_Key = @Question_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Paypal_Confirmation]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Paypal_Confirmation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Paypal_Confirmation]
	@Email_Address varchar(200)
AS
BEGIN

	SELECT *
	FROM Paypal_Confirmation
	WHERE Paypal_Email = @Email_Address
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Errors_Lastone]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Errors_Lastone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Errors_Lastone]
	@Test int
AS
BEGIN
	SELECT top 1 *
	FROM aspnet_webevent_events
	WHERE Details LIKE ''%Exception message%''
	ORDER BY EventTime Desc
	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Errors]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Errors]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Errors]
	@Test int
AS
BEGIN
	SELECT *
	FROM aspnet_webevent_events
	WHERE Details LIKE ''%Exception message%''
	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_FB_Users_Log]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_FB_Users_Log]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_FB_Users_Log]
	@fbid bigint,
	@page nvarchar(50)
AS
BEGIN
	SELECT COUNT(Log_FB_Users_Key) as visits
	FROM Log_FB_Users
	WHERE FBid = @fbid
	AND Page = @page
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_FB_Users]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_FB_Users]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_FB_Users]
	@fbid bigint
AS
BEGIN

	SELECT *
	FROM FB_Users
	WHERE FBid = @fbid
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_Errors]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_Errors]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Update_Transaction_Errors]
	@Tx_Errors_Text text
AS
BEGIN

	INSERT INTO Transaction_Errors
	(Transaction_Errors_Log,Last_Updated)
	VALUES
	(@Tx_Errors_Text,getdate())
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_Out]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_Out]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Transaction_Out]	
	@Amount money,
	@Email_Recipient nchar(200),
	@Resource_Key int,
	@Event_Key int,
	@Paypal_Fee money,
	@Currency nchar(3),
	@FBid bigint,
	@Type int,
	@Tx_out_Key_Return int Output

AS
BEGIN
SET @Tx_out_Key_Return = 0

DECLARE @CurrentDate datetime
SET @CurrentDate = getdate()
	
	INSERT INTO Transactions_Out
	(Amount,Email_Recipient,tx_date,Resource_Key,Event_Key,Paypal_Fee,Currency,FBid,Type)
	Values
	(@Amount,@Email_Recipient,@CurrentDate,@Resource_Key,@Event_Key,@Paypal_Fee,@Currency,@FBid,@Type)

SET @Tx_out_Key_Return = (SELECT Tx_Out_Key FROM Transactions_Out WHERE tx_date = @CurrentDate)	
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Admins]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Admins]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Admins]
	@Search int
AS
IF @Search = 0 
	BEGIN
	SELECT 
		FBid,
		Full_Name = First_Name + '' '' + Last_Name
	FROM FB_Users
	END
IF @Search = 1
	BEGIN
	SELECT 
		FBid,
		Full_Name = First_Name + '' '' + Last_Name
	FROM FB_Users
	WHERE Admin = 1
	END	


	RETURN' 
END
/****** Object:  StoredProcedure [dbo].[Update_Paypal_Confirmation]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Paypal_Confirmation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Paypal_Confirmation]
	@Paypal_Email varchar(200),
	@type int, -- 0 = sent confirmation / 1 = email is confirmed,
	@Amount_Sent money
AS
BEGIN



IF @type = 0
	BEGIN
			
	INSERT INTO Paypal_Confirmation
	(Paypal_Email,Amount_Sent,Confirmed,Date_Sent)
	Values
	(@Paypal_Email,@Amount_Sent,0,getdate())
	
	END

IF @type = 1	
	BEGIN
		
	UPDATE Paypal_Confirmation
	SET
		Confirmed=1
	WHERE Paypal_Email=@Paypal_Email
	
	END
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Log_Post_Authorize_Remove]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Log_Post_Authorize_Remove]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Update_Log_Post_Authorize_Remove]
	@FBid bigint,
	@Authorize_Remove bit,
	@Signature_Linked nvarchar(200),
	@Signature nvarchar(200)
AS
BEGIN


	INSERT INTO Log_Post_Authorize_Remove
	(Authorize_Remove,Signature_Linked,Signature,FBid,Last_Change)
	VALUES
	(@Authorize_Remove,@Signature_Linked,@Signature,@FBid,getdate())
	

END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_CustomMessage]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_CustomMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_CustomMessage]
	@Event_Key int,
	@fbid bigint,
	@CustomMessage text
AS
BEGIN
DECLARE @Check_Rowthere int
SET @Check_Rowthere = (SELECT Top 1 Event_Key FROM FB_Users_Sellers_Customize WHERE Event_Key = @Event_Key AND FBid = @fbid)

IF (@Check_Rowthere is null)
	BEGIN
	INSERT INTO FB_Users_Sellers_Customize
	(Event_Key,FBid)
	Values
	(@Event_Key,@fbid)
	END


	UPDATE FB_Users_Sellers_Customize
	SET
		Personal_Message = @CustomMessage
	WHERE Event_Key = @Event_Key 
	AND FBid = @fbid	
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_CCErrors]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_CCErrors]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_CCErrors]
	@Error nvarchar(400),
	@Tx_Key int,
	@CC_Mass int, --0 = Credit Card Error, 1 = Mass Payment Error
	@Amount nvarchar(200)
AS
BEGIN

	INSERT INTO CCErrors
	(TheError,Error_Date,Tx_Key,CC_Mass,Amount)
	VALUES
	(@Error,getdate(),@Tx_Key,@CC_Mass,@Amount)
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Admins]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Admins]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Admins]
	@FBid bigint
	
AS
BEGIN
	UPDATE FB_Users
	SET
		Admin = 1
	WHERE @FBid = FBid
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_GoalAmount]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_GoalAmount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_GoalAmount]
	@Event_Key int,
	@fbid bigint,
	@Amount money
AS
BEGIN
DECLARE @Check_Rowthere int
SET @Check_Rowthere = (SELECT Top 1 Event_Key FROM FB_Users_Sellers_Customize WHERE Event_Key = @Event_Key AND FBid = @fbid)

IF (@Check_Rowthere is null)
	BEGIN
	INSERT INTO FB_Users_Sellers_Customize
	(Event_Key,FBid)
	Values
	(@Event_Key,@fbid)
	END


	UPDATE FB_Users_Sellers_Customize
	SET
		GoalAmount = @Amount
	WHERE Event_Key = @Event_Key 
	AND FBid = @fbid	
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Resource_NavigateURL]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Resource_NavigateURL]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Update_Resource_NavigateURL]
	@FBid bigint,
	@NavigateURL bit
AS
BEGIN
	
	UPDATE FB_Users
	SET
		GetNavigate = @NavigateURL
	WHERE FBid=@FBid
	

END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Resource]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Resource]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_Resource]
	@FBid bigint,
	@First_Name varchar(50),
	@Last_Name varchar(50),
	@Email varchar(100),
	@IP_Address varchar(50),
	@Pagename nvarchar(50),
	@Resource_Key int,
	@Event_Key int,
	@Session_Key nvarchar(200),
	@Access_Token nvarchar(200),
	@Referral bigint
AS
BEGIN
DECLARE @Check_FBid bigint
SET @Check_FBid = (SELECT FBid FROM FB_Users WHERE FBid = @FBid)

If @Check_FBid is null --Create new record
	BEGIN
	INSERT INTO FB_Users
	(FBid,First_Name,Last_Name,Email,Signed_Up,Last_Change,IP_Address,Session_Key,Access_Token,Referral)
	VALUES
	(@FBid,@First_Name,@Last_Name,@Email,getdate(),getdate(),@IP_Address,@Session_Key,@Access_Token,@Referral)
	END
ELSE
	BEGIN
	UPDATE FB_Users
	SET
		First_Name = @First_Name,
		Last_Name = @Last_Name,
		Email = @Email,
		Last_Change = getdate(),
		IP_Address=@IP_Address,
		Session_Key = @Session_Key,
		Access_Token = @Access_Token
	WHERE FBid=@FBid
	END

INSERT INTO Log_FB_Users
(FBid,Page,Resource_Key,Event_Key,Last_Change,IP_Address)
VALUES
(@FBid,@Pagename,@Resource_Key,@Event_Key,getdate(),@IP_Address)
	

END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Referral_Email]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Referral_Email]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Update_Referral_Email]
	@FBid bigint,	
	@Email_Paypal varchar(200)
AS
BEGIN

	UPDATE FB_Users
	SET		
		Referral_Email=@Email_Paypal		
	WHERE FBid=@FBid
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Questions_DropDowns]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Questions_DropDowns]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Questions_DropDowns]
	@Question_Key int,
	@Question_DD nvarchar(100)

AS
BEGIN


	INSERT INTO Questions_DropDowns
	(Question_Key,Question_DD_Text,Question_DD_Value)
	Values
	(@Question_Key,@Question_DD,@Question_DD)
	
END' 
END
/****** Object:  StoredProcedure [dbo].[Update_ShowGoals]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_ShowGoals]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_ShowGoals]
	@Event_Key int,
	@fbid bigint
AS
BEGIN
DECLARE @Check_Rowthere int
SET @Check_Rowthere = (SELECT Top 1 Event_Key FROM FB_Users_Sellers_Customize WHERE Event_Key = @Event_Key AND FBid = @fbid)

IF (@Check_Rowthere is null)
	BEGIN
	INSERT INTO FB_Users_Sellers_Customize
	(Event_Key,FBid)
	Values
	(@Event_Key,@fbid)
	END


DECLARE @Check_ShowGoals bit
SET @Check_ShowGoals = (SELECT Top 1 ShowGoals FROM FB_Users_Sellers_Customize WHERE Event_Key = @Event_Key AND FBid = @fbid)


If (@Check_ShowGoals is null) OR (@Check_ShowGoals=0) --It is off
	BEGIN
	UPDATE FB_Users_Sellers_Customize
	SET
		ShowGoals = 1
	WHERE Event_Key = @Event_Key 
	AND FBid = @fbid	
	END	
ELSE
	BEGIN
	UPDATE FB_Users_Sellers_Customize
	SET
		ShowGoals = 0
	WHERE Event_Key = @Event_Key 
	AND FBid = @fbid
	END
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Ticket_Sellers_SpecificFundraiser]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Ticket_Sellers_SpecificFundraiser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Ticket_Sellers_SpecificFundraiser]	
	@fbid bigint,
	@fbappid nvarchar(100)

AS
BEGIN
DECLARE @Event_Key int
DECLARE @Ticket_Key int
SET @Event_Key = 0
SET @Ticket_Key = 0
IF @fbappid = ''115168081834700''
	BEGIN
	SET @Event_Key = 86
	SET @Ticket_Key = 112
	END

	DECLARE @Check_FBid bigint	
	SET @Check_FBid = (SELECT FBid FROM FB_Users_Sellers WHERE Event_Key = @Event_Key AND Ticket_Key = @Ticket_Key AND FBid = @fbid)
	
	If @Check_FBid is null --Create new record
	BEGIN
		INSERT INTO FB_Users_Sellers
		(FBid,Event_Key,Ticket_Key)
		Values
		(@fbid,@Event_Key,@Ticket_Key)
	END
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Ticket_Sellers]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Ticket_Sellers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Ticket_Sellers]
	@Ticket_Key int,
	@Event_Key int,
	@fbid bigint

AS
BEGIN

	INSERT INTO FB_Users_Sellers
	(FBid,Event_Key,Ticket_Key)
	Values
	(@fbid,@Event_Key,@Ticket_Key)
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Test]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Test]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Test]
	@Test_Text text
AS
BEGIN

	INSERT INTO Test
	(Test_Text,Test_update)
	VALUES
	(@Test_Text,getdate())
END
' 
END
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LoweredUserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MobileAlias] [nvarchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
 CONSTRAINT [PK__aspnet_Users__3B75D760] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END

/****** Object:  Index [aspnet_Users_Index]    Script Date: 03/31/2011 18:02:04 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Users]') AND name = N'aspnet_Users_Index')
CREATE UNIQUE CLUSTERED INDEX [aspnet_Users_Index] ON [dbo].[aspnet_Users] 
(
	[ApplicationId] ASC,
	[LoweredUserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
/****** Object:  StoredProcedure [dbo].[aspnet_UnRegisterSchemaVersion]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UnRegisterSchemaVersion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UnRegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    DELETE FROM dbo.aspnet_SchemaVersions
        WHERE   Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_CheckSchemaVersion]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_CheckSchemaVersion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_CheckSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    IF (EXISTS( SELECT  *
                FROM    dbo.aspnet_SchemaVersions
                WHERE   Feature = LOWER( @Feature ) AND
                        CompatibleSchemaVersion = @CompatibleSchemaVersion ))
        RETURN 0

    RETURN 1
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_CreateApplication]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Applications_CreateApplication]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Applications_CreateApplication]
    @ApplicationName      nvarchar(256),
    @ApplicationId        uniqueidentifier OUTPUT
AS
BEGIN
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName

    IF(@ApplicationId IS NULL)
    BEGIN
        DECLARE @TranStarted   bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
	        BEGIN TRANSACTION
	        SET @TranStarted = 1
        END
        ELSE
    	    SET @TranStarted = 0

        SELECT  @ApplicationId = ApplicationId
        FROM dbo.aspnet_Applications WITH (UPDLOCK, HOLDLOCK)
        WHERE LOWER(@ApplicationName) = LoweredApplicationName

        IF(@ApplicationId IS NULL)
        BEGIN
            SELECT  @ApplicationId = NEWID()
            INSERT  dbo.aspnet_Applications (ApplicationId, ApplicationName, LoweredApplicationName)
            VALUES  (@ApplicationId, @ApplicationName, LOWER(@ApplicationName))
        END


        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
	        SET @TranStarted = 0
	        COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END' 
END
/****** Object:  Table [dbo].[aspnet_Paths]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Paths]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Paths](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NOT NULL,
	[Path] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LoweredPath] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK__aspnet_Paths__403A8C7D] PRIMARY KEY NONCLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END

/****** Object:  Index [aspnet_Paths_index]    Script Date: 03/31/2011 18:02:04 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Paths]') AND name = N'aspnet_Paths_index')
CREATE UNIQUE CLUSTERED INDEX [aspnet_Paths_index] ON [dbo].[aspnet_Paths] 
(
	[ApplicationId] ASC,
	[LoweredPath] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
/****** Object:  StoredProcedure [dbo].[aspnet_Personalization_GetApplicationId]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Personalization_GetApplicationId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Personalization_GetApplicationId] (
    @ApplicationName NVARCHAR(256),
    @ApplicationId UNIQUEIDENTIFIER OUT)
AS
BEGIN
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
END' 
END
/****** Object:  Table [dbo].[aspnet_Roles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LoweredRoleName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__aspnet_Roles__4316F928] PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END

/****** Object:  Index [aspnet_Roles_index1]    Script Date: 03/31/2011 18:02:04 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]') AND name = N'aspnet_Roles_index1')
CREATE UNIQUE CLUSTERED INDEX [aspnet_Roles_index1] ON [dbo].[aspnet_Roles] 
(
	[ApplicationId] ASC,
	[LoweredRoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
/****** Object:  StoredProcedure [dbo].[aspnet_RegisterSchemaVersion]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_RegisterSchemaVersion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_RegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128),
    @IsCurrentVersion          bit,
    @RemoveIncompatibleSchema  bit
AS
BEGIN
    IF( @RemoveIncompatibleSchema = 1 )
    BEGIN
        DELETE FROM dbo.aspnet_SchemaVersions WHERE Feature = LOWER( @Feature )
    END
    ELSE
    BEGIN
        IF( @IsCurrentVersion = 1 )
        BEGIN
            UPDATE dbo.aspnet_SchemaVersions
            SET IsCurrentVersion = 0
            WHERE Feature = LOWER( @Feature )
        END
    END

    INSERT  dbo.aspnet_SchemaVersions( Feature, CompatibleSchemaVersion, IsCurrentVersion )
    VALUES( LOWER( @Feature ), @CompatibleSchemaVersion, @IsCurrentVersion )
END' 
END
/****** Object:  UserDefinedFunction [dbo].[Resource_NetRevenue_Paidout_Currency]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_NetRevenue_Paidout_Currency]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Resource_NetRevenue_Paidout_Currency]
	(@Resource_Key int,
	@Currency nvarchar(3)
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Paid_Out decimal(20,2)
	SELECT @Paid_Out = Case 
					WHEN(SELECT SUM(Amount) FROM Transactions_Out
						WHERE 						
						Resource_Key = @Resource_Key
						AND Currency=@Currency
						AND Amount > 0
						AND (Type = 1))
					IS NULL THEN 0					
					ELSE (
						SELECT SUM(Amount) FROM Transactions_Out
						WHERE 						
						Resource_Key = @Resource_Key
						AND Currency=@Currency			
						AND Amount > 0
						AND (Type = 1)
					)
					END
	RETURN @Paid_Out
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_Paid_Out_Currency]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Paid_Out_Currency]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Event_Paid_Out_Currency]
	(@Event_Key int,
	@Currency nvarchar(3)
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Paid_Out decimal(20,2)
	SELECT @Paid_Out = Case 
					WHEN(SELECT SUM(Amount) FROM Transactions_Out
						WHERE 
						Event_Key = @Event_Key								
						AND Currency=@Currency
						AND Amount > 0
						AND ((Type = 0) OR (Type IS NULL)))
					IS NULL THEN 0					
					ELSE (
						SELECT SUM(Amount) FROM Transactions_Out
						WHERE 
						Event_Key = @Event_Key
						AND Currency=@Currency			
						AND Amount > 0
						AND ((Type = 0) OR (Type IS NULL))
					)
					END
	RETURN @Paid_Out
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_Paid_Out]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Paid_Out]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
create FUNCTION [dbo].[Event_Paid_Out]
	(@Event_Key int
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Paid_Out decimal(20,2)
	SELECT @Paid_Out = Case 
					WHEN(SELECT SUM(Amount) FROM Transactions_Out
						WHERE 
						Event_Key = @Event_Key								
						AND Amount > 0)
					IS NULL THEN 0					
					ELSE (
						SELECT SUM(Amount) FROM Transactions_Out
						WHERE 
						Event_Key = @Event_Key			
						AND Amount > 0
					)
					END
	RETURN @Paid_Out
	END


' 
END

/****** Object:  StoredProcedure [dbo].[Delete_Admins]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Admins]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Delete_Admins]
	@FBid bigint
	
AS
BEGIN
	UPDATE FB_Users
	SET
		Admin = 0
	WHERE @FBid = FBid
END
' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_LogEvent]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_WebEvent_LogEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_WebEvent_LogEvent]
        @EventId         char(32),
        @EventTimeUtc    datetime,
        @EventTime       datetime,
        @EventType       nvarchar(256),
        @EventSequence   decimal(19,0),
        @EventOccurrence decimal(19,0),
        @EventCode       int,
        @EventDetailCode int,
        @Message         nvarchar(1024),
        @ApplicationPath nvarchar(256),
        @ApplicationVirtualPath nvarchar(256),
        @MachineName    nvarchar(256),
        @RequestUrl      nvarchar(1024),
        @ExceptionType   nvarchar(256),
        @Details         ntext
AS
BEGIN
    INSERT
        dbo.aspnet_WebEvent_Events
        (
            EventId,
            EventTimeUtc,
            EventTime,
            EventType,
            EventSequence,
            EventOccurrence,
            EventCode,
            EventDetailCode,
            Message,
            ApplicationPath,
            ApplicationVirtualPath,
            MachineName,
            RequestUrl,
            ExceptionType,
            Details
        )
    VALUES
    (
        @EventId,
        @EventTimeUtc,
        @EventTime,
        @EventType,
        @EventSequence,
        @EventOccurrence,
        @EventCode,
        @EventDetailCode,
        @Message,
        @ApplicationPath,
        @ApplicationVirtualPath,
        @MachineName,
        @RequestUrl,
        @ExceptionType,
        @Details
    )
END' 
END
/****** Object:  Table [dbo].[Resource]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Resource](
	[Resource_Key] [int] IDENTITY(1,1) NOT NULL,
	[Group_Name] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FBCreator] [bigint] NOT NULL,
	[Signed_Up] [datetime] NOT NULL,
	[Last_Change] [datetime] NOT NULL,
	[Desired_Currency] [nchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email_Paypal] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Service_Fee_Percentage] [money] NULL,
	[Service_Fee_Cents] [money] NULL,
	[Service_Fee_Max] [money] NULL,
	[Demo] [bit] NULL,
	[Store_Description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Store_Contact] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Store_Title] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Pay_Method] [int] NULL,
	[Admin_Active] [bit] NULL,
	[Network_Key] [int] NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[Resource_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[FB_Users_Pages]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FB_Users_Pages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FB_Users_Pages](
	[FB_Users_Pages] [int] IDENTITY(1,1) NOT NULL,
	[FB_Users] [bigint] NOT NULL,
	[Page_id] [bigint] NOT NULL,
 CONSTRAINT [PK_FB_Users_Pages] PRIMARY KEY CLUSTERED 
(
	[FB_Users_Pages] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  StoredProcedure [dbo].[Delete_Questions_DropDowns]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Questions_DropDowns]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Delete_Questions_DropDowns]
	@Question_Key int
AS
BEGIN
	DELETE
	FROM Questions_DropDowns
	WHERE Question_Key = @Question_Key	
END
' 
END
/****** Object:  StoredProcedure [dbo].[Delete_Ticket_Sellers_TicketKey]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Ticket_Sellers_TicketKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Delete_Ticket_Sellers_TicketKey]
	@Ticket_Key int
AS
BEGIN
	DELETE
	FROM FB_Users_Sellers
	WHERE Ticket_Key = @Ticket_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Delete_Ticket_Sellers_EventKey]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Ticket_Sellers_EventKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Delete_Ticket_Sellers_EventKey]
	@Event_Key int
AS
BEGIN
	DELETE
	FROM FB_Users_Sellers
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  Table [dbo].[Log_Activities]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Log_Activities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Log_Activities](
	[Log_Activities_Key] [int] IDENTITY(1,1) NOT NULL,
	[fbid] [bigint] NOT NULL,
	[Activity] [int] NOT NULL,
	[Date_Occured] [datetime] NOT NULL,
	[Resource_Key] [int] NOT NULL,
	[fbid_added] [bigint] NULL,
	[event_key] [int] NULL,
	[tx_out_key] [int] NULL,
 CONSTRAINT [PK_Log_Activities] PRIMARY KEY CLUSTERED 
(
	[Log_Activities_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[FB_Users_Resource]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FB_Users_Resource]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FB_Users_Resource](
	[FB_Users_Resource_Key] [int] IDENTITY(1,1) NOT NULL,
	[FBid] [bigint] NOT NULL,
	[Resource_Key] [int] NOT NULL,
	[Last_Change] [datetime] NOT NULL,
 CONSTRAINT [PK_FB_Users_Resource] PRIMARY KEY CLUSTERED 
(
	[FB_Users_Resource_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_CreateRole]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles_CreateRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Roles_CreateRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO dbo.aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
/****** Object:  Table [dbo].[Resource_Reading_Others]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Reading_Others]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Resource_Reading_Others](
	[Resource_Reading_Others_Key] [int] IDENTITY(1,1) NOT NULL,
	[Resource_Key] [int] NOT NULL,
	[Resource_Key_Reading] [int] NOT NULL,
	[Date_Added] [datetime] NOT NULL,
	[FBid_Added] [bigint] NOT NULL,
 CONSTRAINT [PK_Resource_Reading_Others] PRIMARY KEY CLUSTERED 
(
	[Resource_Reading_Others_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[aspnet_PersonalizationPerUser]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser](
	[Id] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK__aspnet_Personali__38996AB5] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

/****** Object:  Index [aspnet_PersonalizationPerUser_index1]    Script Date: 03/31/2011 18:02:04 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]') AND name = N'aspnet_PersonalizationPerUser_index1')
CREATE UNIQUE CLUSTERED INDEX [aspnet_PersonalizationPerUser_index1] ON [dbo].[aspnet_PersonalizationPerUser] 
(
	[PathId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
/****** Object:  Table [dbo].[aspnet_Profile]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Profile](
	[UserId] [uniqueidentifier] NOT NULL,
	[PropertyNames] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PropertyValuesString] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PropertyValuesBinary] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK__aspnet_Profile__48CFD27E] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MobilePIN] [nvarchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LoweredEmail] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PasswordQuestion] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PasswordAnswer] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__aspnet_Membershi__45F365D3] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

/****** Object:  Index [aspnet_Membership_index]    Script Date: 03/31/2011 18:02:04 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]') AND name = N'aspnet_Membership_index')
CREATE CLUSTERED INDEX [aspnet_Membership_index] ON [dbo].[aspnet_Membership] 
(
	[ApplicationId] ASC,
	[LoweredEmail] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_CreatePath]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Paths_CreatePath]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Paths_CreatePath]
    @ApplicationId UNIQUEIDENTIFIER,
    @Path           NVARCHAR(256),
    @PathId         UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    BEGIN TRANSACTION
    IF (NOT EXISTS(SELECT * FROM dbo.aspnet_Paths WHERE LoweredPath = LOWER(@Path) AND ApplicationId = @ApplicationId))
    BEGIN
        INSERT dbo.aspnet_Paths (ApplicationId, Path, LoweredPath) VALUES (@ApplicationId, @Path, LOWER(@Path))
    END
    COMMIT TRANSACTION
    SELECT @PathId = PathId FROM dbo.aspnet_Paths WHERE LOWER(@Path) = LoweredPath AND ApplicationId = @ApplicationId
END' 
END
/****** Object:  Table [dbo].[aspnet_PersonalizationAllUsers]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers](
	[PathId] [uniqueidentifier] NOT NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK__aspnet_Personali__36B12243] PRIMARY KEY CLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Events]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Events](
	[Event_Key] [int] IDENTITY(1,1) NOT NULL,
	[Event_Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Host] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Event_Begins] [datetime] NULL,
	[Event_Ends] [datetime] NULL,
	[Location] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Street] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [nvarchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Additional_Comments] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Image_URL] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Display_Tickets_Available] [bit] NULL,
	[Confirmation] [nvarchar](4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Resource_Key] [int] NULL,
	[eid] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Begin_Selling] [datetime] NULL,
	[Selling_Deadline] [datetime] NULL,
	[Last_Modified] [datetime] NULL,
	[Service_Fee_Percentage] [money] NULL,
	[Service_Fee_Cents] [money] NULL,
	[Service_Fee_Max] [money] NULL,
	[Ticket_Max] [int] NULL,
	[Visible] [bit] NULL,
	[BkImgUrl] [nchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Timezone] [decimal](18, 0) NULL,
	[TicketNum] [int] NOT NULL,
	[Donation] [bit] NULL,
	[Views] [int] NULL,
	[IsFundraiser] [bit] NULL,
	[IsHideFee] [bit] NULL,
	[Event_Type] [int] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Event_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  StoredProcedure [dbo].[aspnet_Users_CreateUser]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Users_CreateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Users_CreateUser]
    @ApplicationId    uniqueidentifier,
    @UserName         nvarchar(256),
    @IsUserAnonymous  bit,
    @LastActivityDate DATETIME,
    @UserId           uniqueidentifier OUTPUT
AS
BEGIN
    IF( @UserId IS NULL )
        SELECT @UserId = NEWID()
    ELSE
    BEGIN
        IF( EXISTS( SELECT UserId FROM dbo.aspnet_Users
                    WHERE @UserId = UserId ) )
            RETURN -1
    END

    INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
    VALUES (@ApplicationId, @UserId, @UserName, LOWER(@UserName), @IsUserAnonymous, @LastActivityDate)

    RETURN 0
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_RoleExists]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles_RoleExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Roles_RoleExists]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(0)
    IF (EXISTS (SELECT RoleName FROM dbo.aspnet_Roles WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId ))
        RETURN(1)
    ELSE
        RETURN(0)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetAllRoles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles_GetAllRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Roles_GetAllRoles] (
    @ApplicationName           nvarchar(256))
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN
    SELECT RoleName
    FROM   dbo.aspnet_Roles WHERE ApplicationId = @ApplicationId
    ORDER BY RoleName
END' 
END
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__aspnet_UsersInRo__286302EC] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  StoredProcedure [dbo].[Update_Store]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Store]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_Store]
	@Resource_Key int,
	@Title varchar(200),
	@Description text,
	@Contact varchar(200)
AS
BEGIN

	UPDATE Resource
	SET
		Store_Title=@Title,
		Store_Description=@Description,
		Store_Contact=@Contact
	WHERE Resource_Key=@Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Resource_Profile]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Resource_Profile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_Resource_Profile]
	@Resource_Key int,
	@Desired_Currency varchar(3),
	@Email_Paypal varchar(200),
	@Demo bit,
	@Pay_Method int
AS
BEGIN

	UPDATE Resource
	SET
		Desired_Currency=@Desired_Currency,
		Email_Paypal=@Email_Paypal,
		Demo = @Demo,
		Last_Change=getdate(),
		Pay_Method = @Pay_Method
	WHERE Resource_Key=@Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Resource_Network]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Resource_Network]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Resource_Network]	
	@Resource_Key int,
	@Network_Key int
AS
BEGIN
	UPDATE Resource
	SET
		Network_Key = @Network_Key
	WHERE Resource_Key=@Resource_Key
	

END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_fbPages]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_fbPages]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_fbPages]
	@FBid bigint,
	@page_id bigint
AS
DECLARE @Check_pageidexists int
SET @Check_pageidexists = (SELECT Count(*) FROM FB_Users_Pages WHERE FB_Users = @FBid AND Page_id = @page_id)


If @Check_pageidexists = 0 --Create new record
	BEGIN
	
	INSERT INTO FB_Users_Pages
	(FB_Users, Page_id)
	VALUES
	(@FBid,@page_id)
		
	END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Admin_ServiceFee]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Admin_ServiceFee]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Admin_ServiceFee]
	@Service_Fee_Percentage money,
	@Service_Fee_Cents money,
	@Service_Fee_Max money,
	@Resource_Key int
AS
UPDATE Resource
SET
	Service_Fee_Percentage = @Service_Fee_Percentage,
	Service_Fee_Cents = @Service_Fee_Cents,
	Service_Fee_Max = @Service_Fee_Max
WHERE Resource_Key = @Resource_Key
	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_List_All_Events_Referral]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_All_Events_Referral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_List_All_Events_Referral]
	@fbid bigint
AS

SELECT Resource.Resource_Key,
	Resource.Group_Name,
	Resource.FBCreator,
	FBid_Name = FB_Users.First_Name + '' '' + FB_Users.Last_Name
FROM FB_Users
INNER JOIN Resource
ON FB_Users.FBid = Resource.FBCreator
WHERE FB_Users.Referral = @fbid
--AND Begin_Selling <= getdate()
--AND dbo.Event_Finish_Selling(Event_Key) > getdate()
' 
END
/****** Object:  StoredProcedure [dbo].[View_HasPaypalEmail_ResourceKey]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_HasPaypalEmail_ResourceKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_HasPaypalEmail_ResourceKey]
	@Resource_Key int
AS
BEGIN

	SELECT Email_Paypal
	FROM Resource	
	WHERE Resource_Key = @Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Store_Info]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Store_Info]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Store_Info]
	@Resource_Key int
AS
BEGIN
	SELECT
		Resource.Store_Description,
		Resource.Store_Contact,
		Resource.Store_Title,
		Resource.Group_Name,
		Resource.Network_Key
	FROM Resource
	WHERE
		Resource_Key = @Resource_Key	
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Resource_Error404]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Resource_Error404]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Resource_Error404]
	@Group_Name varchar(100)
AS
BEGIN

	SELECT *
	FROM Resource
	WHERE --REPLACE(Group_Name,''!'','''') = @Group_Name
	
		replace(replace(replace(replace(replace(replace(replace(
		replace(replace(replace(replace(replace(replace(replace(
		replace(replace(replace(replace(replace(
			Group_Name,
		'' '',''''),''!'',''''),''#'',''''),''$'',''''),''%'',''''),''^'',''''),
		''&'',''''),''*'',''''),''('',''''),'')'',''''),''<'',''''),''>'',''''),
		''?'',''''),''/'',''''),'':'',''''),'';'',''''),''+'',''''),''='',''''),char(39),'''')
		= @Group_Name

END' 
END
/****** Object:  StoredProcedure [dbo].[View_Resource_All]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Resource_All]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Resource_All]
	@Resource_Key int
AS
BEGIN

	SELECT *
	FROM Resource	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Resource]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Resource]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Resource]
	@Resource_Key int
AS
BEGIN

	SELECT *
	FROM Resource
	WHERE Resource_Key = @Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Service_Fee_ResourceKey]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Service_Fee_ResourceKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Service_Fee_ResourceKey]
	@Resource_Key int
AS
BEGIN
DECLARE @IsHideFee bit
SET @IsHideFee = 0
--SET @IsHideFee = (SELECT IsHideFee FROM Events WHERE Event_Key = @Event_Key)

IF @IsHideFee IS NULL OR @IsHideFee = 0
	BEGIN

		SELECT 
			Service_Fee_Percentage,
			Service_Fee_Cents,
			Service_Fee_Max
		FROM Resource
	WHERE Resource_Key = @Resource_Key
	
	END
ELSE
	BEGIN
		SELECT 
			Service_Fee_Percentage = 0 ,
			Service_Fee_Cents = 0,
			Service_Fee_Max = 1
		FROM Resource
		WHERE Resource_Key = @Resource_Key
	END	
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Resource_NetRevenue_Paidout_Currency]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Resource_NetRevenue_Paidout_Currency]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Resource_NetRevenue_Paidout_Currency]
	@Resource_Key int
AS

BEGIN
	SELECT 
		Revenue_CAD = Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource_Key,''CAD'')),
		Revenue_USD = Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource_Key,''USD'')),
		Revenue_EUR = Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource_Key,''EUR'')),
		Revenue_GBP = Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource_Key,''GBP'')),
		Revenue_ILS = Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource_Key,''ILS''))
	FROM Resource
	WHERE Resource_Key = @Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Transaction_Out_Details_Referral]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Transaction_Out_Details_Referral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Transaction_Out_Details_Referral]
	
	@FBid bigint
AS
	SELECT *, 
		Group_Name = (SELECT Group_Name FROM Resource WHERE Resource.Resource_Key = Transactions_Out.Resource_Key),
		Amount_Total = Convert(decimal(20,2),Amount),
		Paypal_Fee_Total = Convert(decimal(20,2),-1 * Paypal_Fee),
		FBid_Name = (SELECT First_Name + '' '' + Last_Name FROM FB_Users WHERE FB_Users.FBid = Transactions_Out.FBid)
	FROM Transactions_Out
	WHERE @FBid = FBid
	AND 1 = Type
	RETURN
' 
END
/****** Object:  View [dbo].[vw_aspnet_Roles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_Roles]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_Roles]
  AS SELECT [dbo].[aspnet_Roles].[ApplicationId], [dbo].[aspnet_Roles].[RoleId], [dbo].[aspnet_Roles].[RoleName], [dbo].[aspnet_Roles].[LoweredRoleName], [dbo].[aspnet_Roles].[Description]
  FROM [dbo].[aspnet_Roles]
  ' 
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Paths]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_WebPartState_Paths]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_WebPartState_Paths]
  AS SELECT [dbo].[aspnet_Paths].[ApplicationId], [dbo].[aspnet_Paths].[PathId], [dbo].[aspnet_Paths].[Path], [dbo].[aspnet_Paths].[LoweredPath]
  FROM [dbo].[aspnet_Paths]
  ' 
/****** Object:  View [dbo].[vw_aspnet_UsersInRoles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_UsersInRoles]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_UsersInRoles]
  AS SELECT [dbo].[aspnet_UsersInRoles].[UserId], [dbo].[aspnet_UsersInRoles].[RoleId]
  FROM [dbo].[aspnet_UsersInRoles]
  ' 
/****** Object:  View [dbo].[vw_aspnet_Profiles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_Profiles]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_Profiles]
  AS SELECT [dbo].[aspnet_Profile].[UserId], [dbo].[aspnet_Profile].[LastUpdatedDate],
      [DataSize]=  DATALENGTH([dbo].[aspnet_Profile].[PropertyNames])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesString])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesBinary])
  FROM [dbo].[aspnet_Profile]
  ' 
/****** Object:  View [dbo].[vw_aspnet_MembershipUsers]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_MembershipUsers]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_MembershipUsers]
  AS SELECT [dbo].[aspnet_Membership].[UserId],
            [dbo].[aspnet_Membership].[PasswordFormat],
            [dbo].[aspnet_Membership].[MobilePIN],
            [dbo].[aspnet_Membership].[Email],
            [dbo].[aspnet_Membership].[LoweredEmail],
            [dbo].[aspnet_Membership].[PasswordQuestion],
            [dbo].[aspnet_Membership].[PasswordAnswer],
            [dbo].[aspnet_Membership].[IsApproved],
            [dbo].[aspnet_Membership].[IsLockedOut],
            [dbo].[aspnet_Membership].[CreateDate],
            [dbo].[aspnet_Membership].[LastLoginDate],
            [dbo].[aspnet_Membership].[LastPasswordChangedDate],
            [dbo].[aspnet_Membership].[LastLockoutDate],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptWindowStart],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptWindowStart],
            [dbo].[aspnet_Membership].[Comment],
            [dbo].[aspnet_Users].[ApplicationId],
            [dbo].[aspnet_Users].[UserName],
            [dbo].[aspnet_Users].[MobileAlias],
            [dbo].[aspnet_Users].[IsAnonymous],
            [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Membership] INNER JOIN [dbo].[aspnet_Users]
      ON [dbo].[aspnet_Membership].[UserId] = [dbo].[aspnet_Users].[UserId]
  ' 
/****** Object:  StoredProcedure [dbo].[View_Transaction_Out_Details]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Transaction_Out_Details]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Transaction_Out_Details]
	
	@Resource_Key int
AS
	SELECT *, 
		Event_Name = (SELECT Event_Name FROM Events WHERE Events.Event_Key = Transactions_Out.Event_Key),
		Amount_Total = Convert(decimal(20,2),Amount),
		Paypal_Fee_Total = Convert(decimal(20,2),-1 * Paypal_Fee),
		FBid_Name = (SELECT First_Name + '' '' + Last_Name FROM FB_Users WHERE FB_Users.FBid = Transactions_Out.FBid)
	FROM Transactions_Out
	WHERE @Resource_Key = Resource_Key
	AND 0 = Type
	RETURN
' 
END
/****** Object:  View [dbo].[vw_aspnet_WebPartState_User]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_WebPartState_User]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_WebPartState_User]
  AS SELECT [dbo].[aspnet_PersonalizationPerUser].[PathId], [dbo].[aspnet_PersonalizationPerUser].[UserId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationPerUser].[PageSettings]), [dbo].[aspnet_PersonalizationPerUser].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationPerUser]
  ' 
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Shared]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_WebPartState_Shared]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_WebPartState_Shared]
  AS SELECT [dbo].[aspnet_PersonalizationAllUsers].[PathId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationAllUsers].[PageSettings]), [dbo].[aspnet_PersonalizationAllUsers].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationAllUsers]
  ' 
/****** Object:  StoredProcedure [dbo].[View_Resource_FromEventKey]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Resource_FromEventKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Resource_FromEventKey]
	@Event_Key int
AS
BEGIN

	SELECT Resource_Key
	FROM Events
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Paypal_Email]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Paypal_Email]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Paypal_Email]
	@Event_Key int
AS
	SELECT 
		Email_Paypal,
		Demo
	FROM Events
	INNER JOIN Resource
	ON Resource.Resource_Key = Events.Resource_Key
	WHERE @Event_Key = Event_Key

	RETURN' 
END
/****** Object:  StoredProcedure [dbo].[View_Service_Fee]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Service_Fee]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Service_Fee]
	@Event_Key int
AS
BEGIN
DECLARE @IsHideFee bit
SET @IsHideFee = (SELECT IsHideFee FROM Events WHERE Event_Key = @Event_Key)

IF @IsHideFee IS NULL OR @IsHideFee = 0
	BEGIN

		SELECT 
			Service_Fee_Percentage,
			Service_Fee_Cents,
			Service_Fee_Max
		FROM Events
	WHERE Event_Key = @Event_Key
	
	END
ELSE
	BEGIN
		SELECT 
			Service_Fee_Percentage = 0 ,
			Service_Fee_Cents = 0,
			Service_Fee_Max = 1
		FROM Events
		WHERE Event_Key = @Event_Key
	END	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Reporting_HostStats]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Reporting_HostStats]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[View_Reporting_HostStats]
	@Resource_Key int
AS

SELECT 
	Current_Amount = (SELECT Count(Event_Key) FROM Events WHERE 
				Resource_Key=@Resource_Key AND (Visible = 1 OR Visible is null)
				AND Begin_Selling <= getdate()
				AND Selling_Deadline > getdate())

FROM Events
WHERE Resource_Key=@Resource_Key
AND (Visible = 1 OR Visible is null)
' 
END
/****** Object:  StoredProcedure [dbo].[View_Store_Sellers_EventKey]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Store_Sellers_EventKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Store_Sellers_EventKey]
	@fbid bigint,
	@Event_Key int
AS
BEGIN
	SELECT
		DISTINCT (FB_Users_Sellers.Event_Key),
		Event_Name, 
		Description = CASE 
						WHEN (SELECT Personal_Message FROM FB_Users_Sellers_Customize WHERE FBid=@fbid AND FB_Users_Sellers.Event_Key = FB_Users_Sellers_Customize.Event_Key)  is null THEN CONVERT(nvarchar(75),Additional_Comments)
					    ELSE CONVERT(nvarchar(75),(SELECT Personal_Message FROM FB_Users_Sellers_Customize WHERE FBid=@fbid AND FB_Users_Sellers.Event_Key = FB_Users_Sellers_Customize.Event_Key))
					  END,
		Descriptionfull = CASE 
						WHEN (SELECT Personal_Message FROM FB_Users_Sellers_Customize WHERE FBid=@fbid AND FB_Users_Sellers.Event_Key = FB_Users_Sellers_Customize.Event_Key) is null THEN CONVERT(VARCHAR(MAX),Additional_Comments)
					    ELSE CONVERT(VARCHAR(MAX),(SELECT Personal_Message FROM FB_Users_Sellers_Customize WHERE FBid=@fbid AND FB_Users_Sellers.Event_Key = FB_Users_Sellers_Customize.Event_Key))
					  END,
		Event_Begins,		
		Ticket_Max,
		Service_Fee_Percentage,
		Service_Fee_Cents,
		Service_Fee_Max,
		eid
	FROM FB_Users_Sellers
	INNER JOIN Events
	ON Events.Event_Key = FB_Users_Sellers.Event_Key
	WHERE
		FB_Users_Sellers.FBid = @fbid
		AND @Event_Key = FB_Users_Sellers.Event_Key
		AND (Visible = 1 OR Visible is null)
		--AND DATEADD(dd,+1,DATEADD(hh,Events.Timezone,Events.Event_Ends)) > getdate()
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Store_Sellers]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Store_Sellers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Store_Sellers]
	@page_id bigint
AS
BEGIN


		SELECT
		--DISTINCT (FB_Users_Sellers.Event_Key),
		Events.Event_Key,
		Event_Name, 
		Description = CONVERT(VARCHAR(75),Additional_Comments), 
					--= CASE 
					--	WHEN (SELECT Personal_Message FROM FB_Users_Sellers_Customize WHERE FBid=@fbid AND FB_Users_Sellers.Event_Key = FB_Users_Sellers_Customize.Event_Key)  is null THEN CONVERT(nvarchar(75),Additional_Comments)
					--    ELSE CONVERT(nvarchar(75),(SELECT Personal_Message FROM FB_Users_Sellers_Customize WHERE FBid=@fbid AND FB_Users_Sellers.Event_Key = FB_Users_Sellers_Customize.Event_Key))
					--  END,
		Descriptionfull = CONVERT(VARCHAR(MAX),Additional_Comments), 
						--= CASE 
						--WHEN (SELECT Personal_Message FROM FB_Users_Sellers_Customize WHERE FBid=@fbid AND FB_Users_Sellers.Event_Key = FB_Users_Sellers_Customize.Event_Key) is null THEN CONVERT(VARCHAR(MAX),Additional_Comments)
					    --ELSE CONVERT(VARCHAR(MAX),(SELECT Personal_Message FROM FB_Users_Sellers_Customize WHERE FBid=@fbid AND FB_Users_Sellers.Event_Key = FB_Users_Sellers_Customize.Event_Key))
					  --END,
		Event_Begins,		
		Ticket_Max,
		Events.Service_Fee_Percentage,
		Events.Service_Fee_Cents,
		Events.Service_Fee_Max,
		eid
	FROM FB_Users_Pages
	INNER JOIN FB_Users_Resource
	ON FB_Users_Pages.FB_Users = FB_Users_Resource.FBid
	INNER JOIN Resource
	ON FB_Users_Resource.Resource_Key = Resource.Resource_Key	
	INNER JOIN Events
	ON Events.Resource_Key = Resource.Resource_Key
	WHERE
		FB_Users_Pages.Page_id = @page_id
		AND (Visible = 1 OR Visible is null)
		AND DATEADD(dd,+1,DATEADD(hh,Events.Timezone,Events.Event_Ends)) > getdate()
		AND (Events.Event_Type is NULL OR Events.Event_Type = 0)
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Store]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Store]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Store]
	@Resource_Key int
AS
BEGIN
	SELECT
		Event_Name, 
		Description = CONVERT(nvarchar(75),Additional_Comments),
		Event_Begins,
		Event_Key,
		Group_Name,
		Ticket_Max
	FROM Events
	INNER JOIN Resource
	ON Events.Resource_Key = Resource.Resource_Key
	WHERE
		Events.Resource_Key = @Resource_Key
		AND (Visible = 1 OR Visible is null)
		AND DATEADD(dd,+1,DATEADD(hh,Events.Timezone,Events.Event_Ends)) > getdate()
	ORDER BY Event_Ends Asc
		--AND (SELECT COUNT(Tickets.Event_Key) FROM Tickets
		--	WHERE Tickets.Event_Key = Events.Event_Key			
		--	AND DATEADD(dd,-7,DATEADD(hh,Events.Timezone,Tickets.Selling_Deadline)) > getdate()) > 0
		--AND Events.Begin_Selling <= getdate()
		--AND Events.Selling_Deadline > getdate()
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Stores_In_Network]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Stores_In_Network]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Stores_In_Network]
	@Resource_Key int
AS
BEGIN
DECLARE @CurrentNetwork int
SET @CurrentNetwork = (SELECT Network_Key FROM Resource WHERE Resource_Key = @Resource_Key)

	SELECT
		Resource_Key,
		Group_Name
	FROM Resource	
	WHERE
		Network_Key = @CurrentNetwork
		AND Resource_Key <> @Resource_Key
		AND (SELECT Count(*) 
			FROM Resource_Reading_Others 
			WHERE Resource_Reading_Others.Resource_Key = @Resource_Key 
			AND Resource_Reading_Others.Resource_Key_Reading = Resource.Resource_Key) <> 1
	ORDER BY Group_Name Asc		
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Stores_Currently_Displaying]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Stores_Currently_Displaying]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Stores_Currently_Displaying]
	@Resource_Key int
AS
BEGIN

	SELECT
		Resource_Key,
		Group_Name = (SELECT Group_Name FROM Resource WHERE @Resource_Key = Resource.Resource_Key),
		Resource_Key_Reading,
		Group_Name_Reading = (SELECT Group_Name FROM Resource WHERE Resource_Reading_Others.Resource_Key_Reading = Resource.Resource_Key)
	FROM Resource_Reading_Others	
	WHERE
		Resource_Key = @Resource_Key
	ORDER BY Group_Name_Reading Asc		
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Store_WithSelectedGroups]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Store_WithSelectedGroups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Store_WithSelectedGroups]
	@Resource_Key int
AS
BEGIN
	SELECT
		Events.Event_Name, 
		Description = CONVERT(nvarchar(75),Additional_Comments),
		Event_Begins,
		Event_Key,
		Group_Name = (SELECT Group_Name FROM Resource WHERE @Resource_Key = Resource.Resource_Key),
		Ticket_Max,
		Events.Resource_Key,
		eid,
		event_type
	FROM Events
	INNER JOIN Resource
	ON Events.Resource_Key = Resource.Resource_Key	
	WHERE
		((Events.Resource_Key = @Resource_Key)
		OR
		((SELECT Count(*) FROM Resource_Reading_Others WHERE @Resource_Key = Resource_Reading_Others.Resource_Key AND Events.Resource_Key = Resource_Reading_Others.Resource_Key_Reading) = 1))
		AND (Visible = 1 OR Visible is null)
		AND DATEADD(dd,+1,DATEADD(hh,Events.Timezone,Events.Event_Ends)) > getdate()
	ORDER BY Events.Event_Ends Asc
		--AND (SELECT COUNT(Tickets.Event_Key) FROM Tickets
		--	WHERE Tickets.Event_Key = Events.Event_Key			
		--	AND DATEADD(dd,-7,DATEADD(hh,Events.Timezone,Tickets.Selling_Deadline)) > getdate()) > 0
		--AND Events.Begin_Selling <= getdate()
		--AND Events.Selling_Deadline > getdate()
END' 
END
/****** Object:  StoredProcedure [dbo].[View_IfInGroup]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_IfInGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_IfInGroup]
	@FBid bigint
AS
SELECT NumGroups = Count(*) 
FROM FB_Users_Resource 
WHERE FBid = @FBid' 
END
/****** Object:  StoredProcedure [dbo].[View_HasPaypalEmail]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_HasPaypalEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_HasPaypalEmail]
	@Event_Key int
AS
BEGIN

	SELECT Email_Paypal
	FROM Events
	INNER JOIN Resource
	ON Events.Resource_Key = Resource.Resource_Key
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Event_PaidOut_Currency]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Event_PaidOut_Currency]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Event_PaidOut_Currency]
	@Event_Key int
AS

BEGIN
	SELECT 
		Revenue_CAD = Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''CAD'')),
		Revenue_USD = Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''USD'')),
		Revenue_EUR = Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''EUR'')),
		Revenue_GBP = Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''GBP'')),
		Revenue_ILS = Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''ILS''))
	FROM Events	
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Event_Details]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Event_Details]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[View_Event_Details]
	@Event_Key int
AS

BEGIN
	SELECT *
	FROM Events
	INNER JOIN Resource
	ON Events.Resource_Key = Resource.Resource_Key	
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Event_BkImgUrl]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Event_BkImgUrl]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Event_BkImgUrl]
	@Event_Key int
AS
BEGIN
	SELECT 	BkImgUrl		
	FROM Events
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_List_Group_Members]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_Group_Members]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_List_Group_Members]
	@Resource_Key int
AS
SELECT FB_Users_Resource.FBid,
	Full_Name = First_Name + '' '' + Last_Name,
	Email
FROM FB_Users_Resource
INNER JOIN FB_Users
		ON FB_Users.FBid = FB_Users_Resource.FBid
WHERE @Resource_Key = FB_Users_Resource.Resource_Key' 
END
/****** Object:  StoredProcedure [dbo].[View_List_FBUser_Resources]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_FBUser_Resources]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_List_FBUser_Resources]
	@FBid bigint
AS

DECLARE @Resource_Key int
SET @Resource_Key = (SELECT Max(Resource_Key) FROM FB_Users_Resource WHERE FBid = @FBid)

DECLARE @IsAdmin bit
SET @IsAdmin = (SELECT Admin FROM FB_Users WHERE FBid=@FBid)
--If @Resource_Key is null --Create new record
--	BEGIN
--	INSERT INTO Resource
--	(FBCreator,Group_Name,Desired_Currency,Signed_Up,Last_Change)
--	VALUES
--	(@FBid,''Default Group'',''CAD'',getdate(),getdate())	
--	SET @Resource_Key = (SELECT Max(Resource_Key) FROM FB_Users WHERE FBid = @FBid)
	
--	INSERT INTO FB_Users_Resource
--	(FBid,Resource_Key,Last_Change)
--	VALUES
--	(@FBid,@Resource_Key,getdate())		
--	END

IF @IsAdmin = 1
	BEGIN
	SELECT DISTINCT FB_Users_Resource.Resource_Key,Group_Name
	FROM FB_Users_Resource
	INNER JOIN Resource
			ON Resource.Resource_Key = FB_Users_Resource.Resource_Key	
	WHERE Resource.Admin_Active = 1
	OR Resource.Admin_Active IS NULL
	END
ELSE
	BEGIN
	SELECT FB_Users_Resource.Resource_Key,Group_Name
	FROM FB_Users_Resource
	INNER JOIN Resource
			ON Resource.Resource_Key = FB_Users_Resource.Resource_Key	
	WHERE 	FBid=@FBid	
	END

' 
END
/****** Object:  StoredProcedure [dbo].[View_IsStoreAdmin]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_IsStoreAdmin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_IsStoreAdmin]
	@FBid bigint,
	@Resource_Key int
AS
SELECT Count(*)
FROM FB_Users_Resource
WHERE FBid = @FBid
AND Resource_Key = @Resource_Key' 
END
/****** Object:  StoredProcedure [dbo].[View_IsFundraiser]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_IsFundraiser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_IsFundraiser]
	@Event_Key int
AS
BEGIN
	SELECT IsFundraiser
	FROM Events
	WHERE Event_Key = @Event_Key
	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_List_Resources_All]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_Resources_All]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_List_Resources_All]
	@Nothing int	
AS

--If @Resource_Key is null --Create new record
--	BEGIN
--	INSERT INTO Resource
--	(FBCreator,Group_Name,Desired_Currency,Signed_Up,Last_Change)
--	VALUES
--	(@FBid,''Default Group'',''CAD'',getdate(),getdate())	
--	SET @Resource_Key = (SELECT Max(Resource_Key) FROM FB_Users WHERE FBid = @FBid)
	
--	INSERT INTO FB_Users_Resource
--	(FBid,Resource_Key,Last_Change)
--	VALUES
--	(@FBid,@Resource_Key,getdate())		
--	END

	BEGIN
	SELECT DISTINCT FB_Users_Resource.Resource_Key,Group_Name
	FROM FB_Users_Resource
	INNER JOIN Resource
			ON Resource.Resource_Key = FB_Users_Resource.Resource_Key	
	WHERE Resource.Admin_Active = 1
	OR Resource.Admin_Active IS NULL
	END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Admin_Users]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Admin_Users]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Admin_Users]
	@Test int
AS
	SELECT 
		FBid,
		Full_Name = First_Name + '' '' + Last_Name,
		Signed_Up,
		Last_Change,
		Admin,
		Groups = (SELECT Count(*) FROM FB_Users_Resource WHERE FB_Users.FBid=FB_Users_Resource.FBid)
FROM FB_Users


	RETURN' 
END
/****** Object:  StoredProcedure [dbo].[View_Admin_ServiceFee]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Admin_ServiceFee]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Admin_ServiceFee]
	@Test int
AS
	SELECT Resource_Key, Group_Name, Service_Fee_Percentage, Service_Fee_Cents, Service_Fee_Max,
	Current_Events = (SELECT Count(Event_Key) FROM Events WHERE Events.Resource_Key=Resource.Resource_Key AND Selling_Deadline > getdate()),
	Past_Events = (SELECT Count(Event_Key) FROM Events WHERE Events.Resource_Key=Resource.Resource_Key AND Selling_Deadline <= getdate()),
	Creator = (SELECT Max(First_Name) + '' '' + Max(Last_Name) FROM FB_Users WHERE FBCreator = FBid)
FROM Resource

	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Admin_Events_Removed]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Admin_Events_Removed]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Admin_Events_Removed]
	@Test int
AS
	SELECT 
		Event_Key, Event_Name, 
	Events.Resource_Key, Group_Name, Events.Service_Fee_Percentage, Events.Service_Fee_Cents,
	Current_Previous = CASE 
						WHEN Selling_Deadline <= getdate() THEN ''Previous''
						WHEN Selling_Deadline > getdate() THEN ''Current''
						END
FROM Events
INNER JOIN Resource
ON Events.Resource_Key = Resource.Resource_Key
WHERE Visible = 0

	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Admin_Events]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Admin_Events]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Admin_Events]
	@Test int
AS
	SELECT 
		Event_Key, Event_Name, 
	Events.Resource_Key, Group_Name, Events.Service_Fee_Percentage, Events.Service_Fee_Max, Events.Service_Fee_Cents,
	Current_Previous = CASE 
						WHEN Selling_Deadline <= getdate() THEN ''Previous''
						WHEN Selling_Deadline > getdate() THEN ''Current''
						END
FROM Events
INNER JOIN Resource
ON Events.Resource_Key = Resource.Resource_Key

	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Activities]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Activities]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Activities]
	@Resource_Key int
AS
	BEGIN
	SELECT *,
		Activity_Text = (SELECT Activity FROM Log_Activities_Possibilities WHERE Log_Activities_Possibilities_Key = Log_Activities.Activity),
		event_name = (SELECT Event_Name FROM Events WHERE Events.Event_Key = Log_Activities.Event_Key),
		Paypal_Email = (SELECT Email_Recipient FROM Transactions_Out WHERE Transactions_Out.Tx_Out_Key = Log_Activities.tx_out_key),
		Amount_Sent = Convert(decimal(20,2),(SELECT Amount FROM Transactions_Out WHERE Transactions_Out.Tx_Out_Key = Log_Activities.tx_out_key)),
		Currency_Sent = (SELECT Currency FROM Transactions_Out WHERE Transactions_Out.Tx_Out_Key = Log_Activities.tx_out_key)
	FROM Log_Activities
	WHERE @Resource_Key = Resource_Key	
	ORDER by Date_Occured desc
	END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Admin_Events]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Admin_Events]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Admin_Events]
	@Service_Fee_Percentage money,
	@Service_Fee_Cents money,
	@Service_Fee_Max money,
	@Event_Key int
AS
UPDATE Events
SET
	Service_Fee_Percentage = @Service_Fee_Percentage,
	Service_Fee_Cents = @Service_Fee_Cents,
	Service_Fee_Max = @Service_Fee_Max
WHERE Event_Key = @Event_Key
	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Activity]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Activity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Activity]
	@Resource_Key int,
	@fbid bigint,
	@Activity int,
	@fbid_added bigint,
	@event_key int,
	@tx_out_key int
AS
BEGIN
	Insert INTO
		Log_Activities
		(Resource_Key,fbid,Activity,Date_Occured,fbid_added,event_key,tx_out_key)
	VALUES
		(@Resource_Key,@fbid,@Activity,getdate(),@fbid_added,@event_key,@tx_out_key)
END' 
END
/****** Object:  StoredProcedure [dbo].[Update_Groups]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Groups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Groups]
	@FBid bigint,
	@Resource_Key int,
	@Group_Name varchar(50),
	@Default_SFP money,
	@Default_SFC money,
	@Default_SFM money,
	@Resource_Key_Return int Output
AS
DECLARE @Check_Resource int
SET @Check_Resource = (SELECT Resource_Key FROM Resource WHERE Resource_Key = @Resource_Key)

SET @Resource_Key_Return = 0

If @Check_Resource is null --Create new record
	BEGIN
	DECLARE @New_Resource_Date datetime
	SET @New_Resource_Date = getdate()
	
	INSERT INTO Resource
	(FBCreator,Group_Name,Signed_Up,Last_Change,Service_Fee_Percentage,Service_Fee_Cents,Service_Fee_Max,Demo)
	VALUES
	(@FBid,@Group_Name,@New_Resource_Date,@New_Resource_Date,@Default_SFP,@Default_SFC,@Default_SFM,1)
		
	SET @Resource_Key_Return = (SELECT Resource_Key FROM Resource WHERE Signed_Up = @New_Resource_Date)
		
	
	INSERT INTO FB_Users_Resource
	(FBid,Resource_Key,Last_Change)
	VALUES
	(@FBid,@Resource_Key_Return,getdate())
			
		
	END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_FB_Users_Resource]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_FB_Users_Resource]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_FB_Users_Resource]
	@FBid bigint,
	@Resource_Key int,
	@First_Name varchar(50),
	@Last_Name varchar(50)
	
AS
DECLARE @Check_FBid bigint
SET @Check_FBid = (SELECT FBid FROM FB_Users WHERE FBid = @FBid)

If @Check_FBid is null --Create new record
	BEGIN
	INSERT INTO FB_Users
	(FBid,First_Name,Last_Name,Signed_Up,Last_Change)
	VALUES
	(@FBid,@First_Name,@Last_Name,getdate(),getdate())
	
	IF @FBid = 121100861
		BEGIN
		UPDATE FB_Users
		SET
		Admin = 1
		WHERE FBid = @FBid
		END
	END

SET @Check_FBid = (SELECT FBid FROM FB_Users_Resource WHERE FBid = @FBid AND Resource_Key=@Resource_Key)

If @Check_FBid is null --Create new record
	BEGIN
	INSERT INTO FB_Users_Resource
	(FBid,Resource_Key,Last_Change)
	VALUES
	(@FBid,@Resource_Key,getdate())
	END' 
END
/****** Object:  StoredProcedure [dbo].[Update_Event_Views]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Event_Views]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Event_Views]
	@Event_Key int

AS
BEGIN

DECLARE @Check_Event_Views int
SET @Check_Event_Views = (SELECT Views FROM Events WHERE Event_Key = @Event_Key)

If @Check_Event_Views is null --Set to 1
	Begin
	UPDATE Events
	SET
		Views = 1
	WHERE Event_Key = @Event_Key
	End

Else
	Begin
	UPDATE Events
	SET
		Views = Views + 1
	WHERE Event_Key = @Event_Key
	End	
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Event_Remove]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Event_Remove]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Event_Remove]
	@Event_Key int,
	@Visible bit

AS
BEGIN
	--0 means invisible & 1 means visible & null means visible
	UPDATE Events
	SET
		Visible = @Visible,
		Last_Modified = getdate()
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Event_ForTicket]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Event_ForTicket]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Event_ForTicket]			
	@Event_Key int = NULL

AS
BEGIN

UPDATE Events
	SET
	Visible = 0
	WHERE Event_Key = @Event_Key
	
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Event_BkImgUrl]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Event_BkImgUrl]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Event_BkImgUrl]
	@Event_Key int,
	@BkImgUrl nvarchar(200)

AS
BEGIN

	UPDATE Events
	SET
		BkImgUrl = @BkImgUrl	
	WHERE Event_Key = @Event_Key
END' 
END
/****** Object:  StoredProcedure [dbo].[Update_Resource_Reading_Others]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Resource_Reading_Others]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Resource_Reading_Others]
	@Resource_Key int,
	@Resource_Key_Reading int,
	@fbid bigint	

AS
BEGIN

	INSERT INTO Resource_Reading_Others
	(FBid_Added,Resource_Key,Resource_Key_Reading,Date_Added)
	Values
	(@fbid,@Resource_Key,@Resource_Key_Reading,getdate())
END
' 
END
/****** Object:  UserDefinedFunction [dbo].[Resource_AmountEvents_ByDate]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_AmountEvents_ByDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Resource_AmountEvents_ByDate]
	(@Resource_Key int,
	 @Begin_Date datetime,
	 @End_Date datetime
	)
RETURNS int
AS
	BEGIN
	declare @AmountEvents int
	SELECT @AmountEvents = ( SELECT COUNT(Event_Key) FROM Events						
						WHERE 
						Resource_Key = @Resource_Key									
						AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
						OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends)))
						AND (Visible = 1 OR Visible is null))			
	RETURN @AmountEvents
	END
' 
END

/****** Object:  StoredProcedure [dbo].[Delete_Resource_Reading_Others]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Resource_Reading_Others]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Delete_Resource_Reading_Others]		
	@Resource_Key int
AS
BEGIN
	DELETE
	FROM Resource_Reading_Others
	WHERE Resource_Key=@Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000)
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)


	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames  table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles  table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers  table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num	  int
	DECLARE @Pos	  int
	DECLARE @NextPos  int
	DECLARE @Name	  nvarchar(256)
	DECLARE @CountAll int
	DECLARE @CountU	  int
	DECLARE @CountR	  int


	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N'','', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId
	SELECT @CountR = @@ROWCOUNT

	IF (@CountR <> @Num)
	BEGIN
		SELECT TOP 1 N'''', Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END


	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1


	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N'','', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	SELECT @CountU = @@ROWCOUNT
	IF (@CountU <> @Num)
	BEGIN
		SELECT TOP 1 Name, N''''
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT au.LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE u.UserId = au.UserId)

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(1)
	END

	SELECT  @CountAll = COUNT(*)
	FROM	dbo.aspnet_UsersInRoles ur, @tbUsers u, @tbRoles r
	WHERE   ur.UserId = u.UserId AND ur.RoleId = r.RoleId

	IF (@CountAll <> @CountU * @CountR)
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 @tbUsers tu, @tbRoles tr, dbo.aspnet_Users u, dbo.aspnet_Roles r
		WHERE		 u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND
					 tu.UserId NOT IN (SELECT ur.UserId FROM dbo.aspnet_UsersInRoles ur WHERE ur.RoleId = tr.RoleId) AND
					 tr.RoleId NOT IN (SELECT ur.RoleId FROM dbo.aspnet_UsersInRoles ur WHERE ur.UserId = tu.UserId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	DELETE FROM dbo.aspnet_UsersInRoles
	WHERE UserId IN (SELECT UserId FROM @tbUsers)
	  AND RoleId IN (SELECT RoleId FROM @tbRoles)
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_IsUserInRole]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_IsUserInRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_IsUserInRole]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(2)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    DECLARE @RoleId uniqueidentifier
    SELECT  @RoleId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(2)

    SELECT  @RoleId = RoleId
    FROM    dbo.aspnet_Roles
    WHERE   LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
        RETURN(3)

    IF (EXISTS( SELECT * FROM dbo.aspnet_UsersInRoles WHERE  UserId = @UserId AND RoleId = @RoleId))
        RETURN(1)
    ELSE
        RETURN(0)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetUsersInRoles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_GetUsersInRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetUsersInRoles]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId
    ORDER BY u.UserName
    RETURN(0)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetRolesForUser]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_GetRolesForUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetRolesForUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(1)

    SELECT r.RoleName
    FROM   dbo.aspnet_Roles r, dbo.aspnet_UsersInRoles ur
    WHERE  r.RoleId = ur.RoleId AND r.ApplicationId = @ApplicationId AND ur.UserId = @UserId
    ORDER BY r.RoleName
    RETURN (0)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_FindUsersInRole]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_FindUsersInRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_FindUsersInRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256),
    @UserNameToMatch  nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId AND LoweredUserName LIKE LOWER(@UserNameToMatch)
    ORDER BY u.UserName
    RETURN(0)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_AddUsersToRoles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_AddUsersToRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_AddUsersToRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000),
	@CurrentTimeUtc   datetime
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)
	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num		int
	DECLARE @Pos		int
	DECLARE @NextPos	int
	DECLARE @Name		nvarchar(256)

	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N'','', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		SELECT TOP 1 Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END

	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1

	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N'','', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		DELETE FROM @tbNames
		WHERE LOWER(Name) IN (SELECT LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE au.UserId = u.UserId)

		INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
		  SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
		  FROM   @tbNames

		INSERT INTO @tbUsers
		  SELECT  UserId
		  FROM	dbo.aspnet_Users au, @tbNames t
		  WHERE   LOWER(t.Name) = au.LoweredUserName AND au.ApplicationId = @AppId
	END

	IF (EXISTS (SELECT * FROM dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE		u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	INSERT INTO dbo.aspnet_UsersInRoles (UserId, RoleId)
	SELECT UserId, RoleId
	FROM @tbUsers, @tbRoles

	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     ' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Users_DeleteUser]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Users_DeleteUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Users_DeleteUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @TablesToDeleteFrom int,
    @NumTablesDeletedFrom int OUTPUT
AS
BEGIN
    DECLARE @UserId               uniqueidentifier
    SELECT  @UserId               = NULL
    SELECT  @NumTablesDeletedFrom = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    DECLARE @ErrorCode   int
    DECLARE @RowCount    int

    SET @ErrorCode = 0
    SET @RowCount  = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   u.LoweredUserName       = LOWER(@UserName)
        AND u.ApplicationId         = a.ApplicationId
        AND LOWER(@ApplicationName) = a.LoweredApplicationName

    IF (@UserId IS NULL)
    BEGIN
        GOTO Cleanup
    END

    -- Delete from Membership table if (@TablesToDeleteFrom & 1) is set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_MembershipUsers'') AND (type = ''V''))))
    BEGIN
        DELETE FROM dbo.aspnet_Membership WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
               @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_UsersInRoles table if (@TablesToDeleteFrom & 2) is set
    IF ((@TablesToDeleteFrom & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_UsersInRoles'') AND (type = ''V''))) )
    BEGIN
        DELETE FROM dbo.aspnet_UsersInRoles WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Profile table if (@TablesToDeleteFrom & 4) is set
    IF ((@TablesToDeleteFrom & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_Profiles'') AND (type = ''V''))) )
    BEGIN
        DELETE FROM dbo.aspnet_Profile WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_PersonalizationPerUser table if (@TablesToDeleteFrom & 8) is set
    IF ((@TablesToDeleteFrom & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_WebPartState_User'') AND (type = ''V''))) )
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Users table if (@TablesToDeleteFrom & 1,2,4 & 8) are all set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (@TablesToDeleteFrom & 2) <> 0 AND
        (@TablesToDeleteFrom & 4) <> 0 AND
        (@TablesToDeleteFrom & 8) <> 0 AND
        (EXISTS (SELECT UserId FROM dbo.aspnet_Users WHERE @UserId = UserId)))
    BEGIN
        DELETE FROM dbo.aspnet_Users WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:
    SET @NumTablesDeletedFrom = 0

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
	    ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_DeleteRole]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles_DeleteRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Roles_DeleteRole]
    @ApplicationName            nvarchar(256),
    @RoleName                   nvarchar(256),
    @DeleteOnlyIfRoleIsEmpty    bit
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    DECLARE @RoleId   uniqueidentifier
    SELECT  @RoleId = NULL
    SELECT  @RoleId = RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
    BEGIN
        SELECT @ErrorCode = 1
        GOTO Cleanup
    END
    IF (@DeleteOnlyIfRoleIsEmpty <> 0)
    BEGIN
        IF (EXISTS (SELECT RoleId FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId))
        BEGIN
            SELECT @ErrorCode = 2
            GOTO Cleanup
        END
    END


    DELETE FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DELETE FROM dbo.aspnet_Roles WHERE @RoleId = RoleId  AND ApplicationId = @ApplicationId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUserInfo]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_UpdateUserInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserInfo]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @IsPasswordCorrect              bit,
    @UpdateLastLoginActivityDate    bit,
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @LastLoginDate                  datetime,
    @LastActivityDate               datetime
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @IsApproved                             bit
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @IsApproved = m.IsApproved,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        GOTO Cleanup
    END

    IF( @IsPasswordCorrect = 0 )
    BEGIN
        IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAttemptWindowStart ) )
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = 1
        END
        ELSE
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = @FailedPasswordAttemptCount + 1
        END

        BEGIN
            IF( @FailedPasswordAttemptCount >= @MaxInvalidPasswordAttempts )
            BEGIN
                SET @IsLockedOut = 1
                SET @LastLockoutDate = @CurrentTimeUtc
            END
        END
    END
    ELSE
    BEGIN
        IF( @FailedPasswordAttemptCount > 0 OR @FailedPasswordAnswerAttemptCount > 0 )
        BEGIN
            SET @FailedPasswordAttemptCount = 0
            SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
            SET @FailedPasswordAnswerAttemptCount = 0
            SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
            SET @LastLockoutDate = CONVERT( datetime, ''17540101'', 112 )
        END
    END

    IF( @UpdateLastLoginActivityDate = 1 )
    BEGIN
        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @LastActivityDate
        WHERE   @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END

        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @LastLoginDate
        WHERE   UserId = @UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END


    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
        FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
        FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
        FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
        FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
    WHERE @UserId = UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUser]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_UpdateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUser]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @Email                nvarchar(256),
    @Comment              ntext,
    @IsApproved           bit,
    @LastLoginDate        datetime,
    @LastActivityDate     datetime,
    @UniqueEmail          int,
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId, @ApplicationId = a.ApplicationId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership WITH (UPDLOCK, HOLDLOCK)
                    WHERE ApplicationId = @ApplicationId  AND @UserId <> UserId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            RETURN(7)
        END
    END

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    UPDATE dbo.aspnet_Users WITH (ROWLOCK)
    SET
         LastActivityDate = @LastActivityDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    UPDATE dbo.aspnet_Membership WITH (ROWLOCK)
    SET
         Email            = @Email,
         LoweredEmail     = LOWER(@Email),
         Comment          = @Comment,
         IsApproved       = @IsApproved,
         LastLoginDate    = @LastLoginDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN -1
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UnlockUser]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_UnlockUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_UnlockUser]
    @ApplicationName                         nvarchar(256),
    @UserName                                nvarchar(256)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
        RETURN 1

    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = 0,
        FailedPasswordAttemptCount = 0,
        FailedPasswordAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 ),
        FailedPasswordAnswerAttemptCount = 0,
        FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 ),
        LastLockoutDate = CONVERT( datetime, ''17540101'', 112 )
    WHERE @UserId = UserId

    RETURN 0
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_SetPassword]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_SetPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_SetPassword]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @NewPassword      nvarchar(128),
    @PasswordSalt     nvarchar(128),
    @CurrentTimeUtc   datetime,
    @PasswordFormat   int = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    UPDATE dbo.aspnet_Membership
    SET Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt,
        LastPasswordChangedDate = @CurrentTimeUtc
    WHERE @UserId = UserId
    RETURN(0)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ResetPassword]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_ResetPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_ResetPassword]
    @ApplicationName             nvarchar(256),
    @UserName                    nvarchar(256),
    @NewPassword                 nvarchar(128),
    @MaxInvalidPasswordAttempts  int,
    @PasswordAttemptWindow       int,
    @PasswordSalt                nvarchar(128),
    @CurrentTimeUtc              datetime,
    @PasswordFormat              int = 0,
    @PasswordAnswer              nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserId                                 uniqueidentifier
    SET     @UserId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = IsLockedOut,
           @LastLockoutDate = LastLockoutDate,
           @FailedPasswordAttemptCount = FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Membership WITH ( UPDLOCK )
    WHERE @UserId = UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Membership
    SET    Password = @NewPassword,
           LastPasswordChangedDate = @CurrentTimeUtc,
           PasswordFormat = @PasswordFormat,
           PasswordSalt = @PasswordSalt
    WHERE  @UserId = UserId AND
           ( ( @PasswordAnswer IS NULL ) OR ( LOWER( PasswordAnswer ) = LOWER( @PasswordAnswer ) ) )

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByUserId]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetUserByUserId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByUserId]
    @UserId               uniqueidentifier,
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    IF ( @UpdateLastActivity = 1 )
    BEGIN
        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        FROM     dbo.aspnet_Users
        WHERE    @UserId = UserId

        IF ( @@ROWCOUNT = 0 ) -- User ID not found
            RETURN -1
    END

    SELECT  m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate, m.LastLoginDate, u.LastActivityDate,
            m.LastPasswordChangedDate, u.UserName, m.IsLockedOut,
            m.LastLockoutDate
    FROM    dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   @UserId = u.UserId AND u.UserId = m.UserId

    IF ( @@ROWCOUNT = 0 ) -- User ID not found
       RETURN -1

    RETURN 0
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByName]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetUserByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByName]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier

    IF (@UpdateLastActivity = 1)
    BEGIN
        -- select user ID from aspnet_users table
        SELECT TOP 1 @UserId = u.UserId
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1

        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        WHERE    @UserId = UserId

        SELECT m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut, m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  @UserId = u.UserId AND u.UserId = m.UserId 
    END
    ELSE
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1
    END

    RETURN 0
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByEmail]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetUserByEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByEmail]
    @ApplicationName  nvarchar(256),
    @Email            nvarchar(256)
AS
BEGIN
    IF( @Email IS NULL )
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.LoweredEmail IS NULL
    ELSE
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                LOWER(@Email) = m.LoweredEmail

    IF (@@rowcount = 0)
        RETURN(1)
    RETURN(0)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPasswordWithFormat]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetPasswordWithFormat]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetPasswordWithFormat]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @UpdateLastLoginActivityDate    bit,
    @CurrentTimeUtc                 datetime
AS
BEGIN
    DECLARE @IsLockedOut                        bit
    DECLARE @UserId                             uniqueidentifier
    DECLARE @Password                           nvarchar(128)
    DECLARE @PasswordSalt                       nvarchar(128)
    DECLARE @PasswordFormat                     int
    DECLARE @FailedPasswordAttemptCount         int
    DECLARE @FailedPasswordAnswerAttemptCount   int
    DECLARE @IsApproved                         bit
    DECLARE @LastActivityDate                   datetime
    DECLARE @LastLoginDate                      datetime

    SELECT  @UserId          = NULL

    SELECT  @UserId = u.UserId, @IsLockedOut = m.IsLockedOut, @Password=Password, @PasswordFormat=PasswordFormat,
            @PasswordSalt=PasswordSalt, @FailedPasswordAttemptCount=FailedPasswordAttemptCount,
		    @FailedPasswordAnswerAttemptCount=FailedPasswordAnswerAttemptCount, @IsApproved=IsApproved,
            @LastActivityDate = LastActivityDate, @LastLoginDate = LastLoginDate
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF (@UserId IS NULL)
        RETURN 1

    IF (@IsLockedOut = 1)
        RETURN 99

    SELECT   @Password, @PasswordFormat, @PasswordSalt, @FailedPasswordAttemptCount,
             @FailedPasswordAnswerAttemptCount, @IsApproved, @LastLoginDate, @LastActivityDate

    IF (@UpdateLastLoginActivityDate = 1 AND @IsApproved = 1)
    BEGIN
        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @CurrentTimeUtc
        WHERE   UserId = @UserId

        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @CurrentTimeUtc
        WHERE   @UserId = UserId
    END


    RETURN 0
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPassword]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetPassword]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @PasswordAnswer                 nvarchar(128) = NULL
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @PasswordFormat                         int
    DECLARE @Password                               nvarchar(128)
    DECLARE @passAns                                nvarchar(128)
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @Password = m.Password,
            @passAns = m.PasswordAnswer,
            @PasswordFormat = m.PasswordFormat,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    IF ( NOT( @PasswordAnswer IS NULL ) )
    BEGIN
        IF( ( @passAns IS NULL ) OR ( LOWER( @passAns ) <> LOWER( @PasswordAnswer ) ) )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
        ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
            END
        END

        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    IF( @ErrorCode = 0 )
        SELECT @Password, @PasswordFormat

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetNumberOfUsersOnline]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetNumberOfUsersOnline]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetNumberOfUsersOnline]
    @ApplicationName            nvarchar(256),
    @MinutesSinceLastInActive   int,
    @CurrentTimeUtc             datetime
AS
BEGIN
    DECLARE @DateActive datetime
    SELECT  @DateActive = DATEADD(minute,  -(@MinutesSinceLastInActive), @CurrentTimeUtc)

    DECLARE @NumOnline int
    SELECT  @NumOnline = COUNT(*)
    FROM    dbo.aspnet_Users u(NOLOCK),
            dbo.aspnet_Applications a(NOLOCK),
            dbo.aspnet_Membership m(NOLOCK)
    WHERE   u.ApplicationId = a.ApplicationId                  AND
            LastActivityDate > @DateActive                     AND
            a.LoweredApplicationName = LOWER(@ApplicationName) AND
            u.UserId = m.UserId
    RETURN(@NumOnline)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetAllUsers]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetAllUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetAllUsers]
    @ApplicationName       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0


    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
    SELECT u.UserId
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u
    WHERE  u.ApplicationId = @ApplicationId AND u.UserId = m.UserId
    ORDER BY u.UserName

    SELECT @TotalRecords = @@ROWCOUNT

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName
    RETURN @TotalRecords
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByName]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_FindUsersByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByName]
    @ApplicationName       nvarchar(256),
    @UserNameToMatch       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT u.UserId
        FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND u.LoweredUserName LIKE LOWER(@UserNameToMatch)
        ORDER BY u.UserName


    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByEmail]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_FindUsersByEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByEmail]
    @ApplicationName       nvarchar(256),
    @EmailToMatch          nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    IF( @EmailToMatch IS NULL )
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.Email IS NULL
            ORDER BY m.LoweredEmail
    ELSE
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.LoweredEmail LIKE LOWER(@EmailToMatch)
            ORDER BY m.LoweredEmail

    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY m.LoweredEmail

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_CreateUser]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_CreateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_CreateUser]
    @ApplicationName                        nvarchar(256),
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),
    @PasswordSalt                           nvarchar(128),
    @Email                                  nvarchar(256),
    @PasswordQuestion                       nvarchar(256),
    @PasswordAnswer                         nvarchar(128),
    @IsApproved                             bit,
    @CurrentTimeUtc                         datetime,
    @CreateDate                             datetime = NULL,
    @UniqueEmail                            int      = 0,
    @PasswordFormat                         int      = 0,
    @UserId                                 uniqueidentifier OUTPUT
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @NewUserId uniqueidentifier
    SELECT @NewUserId = NULL

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate  datetime
    SET @LastLockoutDate = CONVERT( datetime, ''17540101'', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart  datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )

    DECLARE @NewUserCreated bit
    DECLARE @ReturnValue   int
    SET @ReturnValue = 0

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SET @CreateDate = @CurrentTimeUtc

    SELECT  @NewUserId = UserId FROM dbo.aspnet_Users WHERE LOWER(@UserName) = LoweredUserName AND @ApplicationId = ApplicationId
    IF ( @NewUserId IS NULL )
    BEGIN
        SET @NewUserId = @UserId
        EXEC @ReturnValue = dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CreateDate, @NewUserId OUTPUT
        SET @NewUserCreated = 1
    END
    ELSE
    BEGIN
        SET @NewUserCreated = 0
        IF( @NewUserId <> @UserId AND @UserId IS NOT NULL )
        BEGIN
            SET @ErrorCode = 6
            GOTO Cleanup
        END
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @ReturnValue = -1 )
    BEGIN
        SET @ErrorCode = 10
        GOTO Cleanup
    END

    IF ( EXISTS ( SELECT UserId
                  FROM   dbo.aspnet_Membership
                  WHERE  @NewUserId = UserId ) )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    SET @UserId = @NewUserId

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership m WITH ( UPDLOCK, HOLDLOCK )
                    WHERE ApplicationId = @ApplicationId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    IF (@NewUserCreated = 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate = @CreateDate
        WHERE  @UserId = UserId
        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    INSERT INTO dbo.aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
         VALUES ( @ApplicationId,
                  @UserId,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  LOWER(@Email),
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsLockedOut,
                  @CreateDate,
                  @CreateDate,
                  @CreateDate,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart )

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]
    @ApplicationName       nvarchar(256),
    @UserName              nvarchar(256),
    @NewPasswordQuestion   nvarchar(256),
    @NewPasswordAnswer     nvarchar(128)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Membership m, dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId
    IF (@UserId IS NULL)
    BEGIN
        RETURN(1)
    END

    UPDATE dbo.aspnet_Membership
    SET    PasswordQuestion = @NewPasswordQuestion, PasswordAnswer = @NewPasswordAnswer
    WHERE  UserId=@UserId
    RETURN(0)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_AnyDataInTables]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_AnyDataInTables]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_AnyDataInTables]
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_MembershipUsers'') AND (type = ''V''))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Membership))
        BEGIN
            SELECT N''aspnet_Membership''
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_Roles'') AND (type = ''V''))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 RoleId FROM dbo.aspnet_Roles))
        BEGIN
            SELECT N''aspnet_Roles''
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_Profiles'') AND (type = ''V''))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Profile))
        BEGIN
            SELECT N''aspnet_Profile''
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 8) is set
    IF ((@TablesToCheck & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_WebPartState_User'') AND (type = ''V''))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_PersonalizationPerUser))
        BEGIN
            SELECT N''aspnet_PersonalizationPerUser''
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 16) is set
    IF ((@TablesToCheck & 16) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''aspnet_WebEvent_LogEvent'') AND (type = ''P''))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 * FROM dbo.aspnet_WebEvent_Events))
        BEGIN
            SELECT N''aspnet_WebEvent_Events''
            RETURN
        END
    END

    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Users))
        BEGIN
            SELECT N''aspnet_Users''
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 ApplicationId FROM dbo.aspnet_Applications))
        BEGIN
            SELECT N''aspnet_Applications''
            RETURN
        END
    END
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetUserState]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_ResetUserState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetUserState] (
    @Count                  int                 OUT,
    @ApplicationName        NVARCHAR(256),
    @InactiveSinceDate      DATETIME            = NULL,
    @UserName               NVARCHAR(256)       = NULL,
    @Path                   NVARCHAR(256)       = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser
        WHERE Id IN (SELECT PerUser.Id
                     FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
                     WHERE Paths.ApplicationId = @ApplicationId
                           AND PerUser.UserId = Users.UserId
                           AND PerUser.PathId = Paths.PathId
                           AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
                           AND (@UserName IS NULL OR Users.LoweredUserName = LOWER(@UserName))
                           AND (@Path IS NULL OR Paths.LoweredPath = LOWER(@Path)))

        SELECT @Count = @@ROWCOUNT
    END
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetSharedState]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_ResetSharedState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetSharedState] (
    @Count int OUT,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationAllUsers
        WHERE PathId IN
            (SELECT AllUsers.PathId
             FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
             WHERE Paths.ApplicationId = @ApplicationId
                   AND AllUsers.PathId = Paths.PathId
                   AND Paths.LoweredPath = LOWER(@Path))

        SELECT @Count = @@ROWCOUNT
    END
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_GetCountOfState]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_GetCountOfState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_GetCountOfState] (
    @Count int OUT,
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN

    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
        IF (@AllUsersScope = 1)
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND AllUsers.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
        ELSE
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND PerUser.UserId = Users.UserId
                  AND PerUser.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
                  AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
                  AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_FindState]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_FindState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_FindState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @PageIndex              INT,
    @PageSize               INT,
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound INT
    DECLARE @PageUpperBound INT
    DECLARE @TotalRecords   INT
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table to store the selected results
    CREATE TABLE #PageIndex (
        IndexId int IDENTITY (0, 1) NOT NULL,
        ItemId UNIQUEIDENTIFIER
    )

    IF (@AllUsersScope = 1)
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT Paths.PathId
        FROM dbo.aspnet_Paths Paths,
             ((SELECT Paths.PathId
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND AllUsers.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT DISTINCT Paths.PathId
               FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND PerUser.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path,
               SharedDataPerPath.LastUpdatedDate,
               SharedDataPerPath.SharedDataLength,
               UserDataPerPath.UserDataLength,
               UserDataPerPath.UserCount
        FROM dbo.aspnet_Paths Paths,
             ((SELECT PageIndex.ItemId AS PathId,
                      AllUsers.LastUpdatedDate AS LastUpdatedDate,
                      DATALENGTH(AllUsers.PageSettings) AS SharedDataLength
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, #PageIndex PageIndex
               WHERE AllUsers.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT PageIndex.ItemId AS PathId,
                      SUM(DATALENGTH(PerUser.PageSettings)) AS UserDataLength,
                      COUNT(*) AS UserCount
               FROM aspnet_PersonalizationPerUser PerUser, #PageIndex PageIndex
               WHERE PerUser.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
               GROUP BY PageIndex.ItemId
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC
    END
    ELSE
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT PerUser.Id
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
        WHERE Paths.ApplicationId = @ApplicationId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
              AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
        ORDER BY Paths.Path ASC, Users.UserName ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path, PerUser.LastUpdatedDate, DATALENGTH(PerUser.PageSettings), Users.UserName, Users.LastActivityDate
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths, #PageIndex PageIndex
        WHERE PerUser.Id = PageIndex.ItemId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
        ORDER BY Paths.Path ASC, Users.UserName ASC
    END

    RETURN @TotalRecords
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_DeleteAllState]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_DeleteAllState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_DeleteAllState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Count int OUT)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        IF (@AllUsersScope = 1)
            DELETE FROM aspnet_PersonalizationAllUsers
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)
        ELSE
            DELETE FROM aspnet_PersonalizationPerUser
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)

        SELECT @Count = @@ROWCOUNT
    END
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_SetPageSettings]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser_SetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CurrentTimeUtc, @UserId OUTPUT
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationPerUser WHERE UserId = @UserId AND PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationPerUser SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE UserId = @UserId AND PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationPerUser(UserId, PathId, PageSettings, LastUpdatedDate) VALUES (@UserId, @PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser_ResetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE PathId = @PathId AND UserId = @UserId
    RETURN 0
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetPageSettings]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser_GetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationPerUser p WHERE p.PathId = @PathId AND p.UserId = @UserId
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers_SetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationAllUsers SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationAllUsers(PathId, PageSettings, LastUpdatedDate) VALUES (@PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    DELETE FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId
    RETURN 0
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers_GetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationAllUsers p WHERE p.PathId = @PathId
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_SetProperties]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_SetProperties]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_SetProperties]
    @ApplicationName        nvarchar(256),
    @PropertyNames          ntext,
    @PropertyValuesString   ntext,
    @PropertyValuesBinary   image,
    @UserName               nvarchar(256),
    @IsUserAnonymous        bit,
    @CurrentTimeUtc         datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
       BEGIN TRANSACTION
       SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DECLARE @UserId uniqueidentifier
    DECLARE @LastActivityDate datetime
    SELECT  @UserId = NULL
    SELECT  @LastActivityDate = @CurrentTimeUtc

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, @IsUserAnonymous, @LastActivityDate, @UserId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Users
    SET    LastActivityDate=@CurrentTimeUtc
    WHERE  UserId = @UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS( SELECT *
               FROM   dbo.aspnet_Profile
               WHERE  UserId = @UserId))
        UPDATE dbo.aspnet_Profile
        SET    PropertyNames=@PropertyNames, PropertyValuesString = @PropertyValuesString,
               PropertyValuesBinary = @PropertyValuesBinary, LastUpdatedDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    ELSE
        INSERT INTO dbo.aspnet_Profile(UserId, PropertyNames, PropertyValuesString, PropertyValuesBinary, LastUpdatedDate)
             VALUES (@UserId, @PropertyNames, @PropertyValuesString, @PropertyValuesBinary, @CurrentTimeUtc)

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProperties]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_GetProperties]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProperties]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)

    IF (@UserId IS NULL)
        RETURN
    SELECT TOP 1 PropertyNames, PropertyValuesString, PropertyValuesBinary
    FROM         dbo.aspnet_Profile
    WHERE        UserId = @UserId

    IF (@@ROWCOUNT > 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    END
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProfiles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_GetProfiles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @PageIndex              int,
    @PageSize               int,
    @UserNameToMatch        nvarchar(256) = NULL,
    @InactiveSinceDate      datetime      = NULL
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT  u.UserId
        FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
        WHERE   ApplicationId = @ApplicationId
            AND u.UserId = p.UserId
            AND (@InactiveSinceDate IS NULL OR LastActivityDate <= @InactiveSinceDate)
            AND (     (@ProfileAuthOptions = 2)
                   OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                   OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                 )
            AND (@UserNameToMatch IS NULL OR LoweredUserName LIKE LOWER(@UserNameToMatch))
        ORDER BY UserName

    SELECT  u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate,
            DATALENGTH(p.PropertyNames) + DATALENGTH(p.PropertyValuesString) + DATALENGTH(p.PropertyValuesBinary)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p, #PageIndexForUsers i
    WHERE   u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= @PageLowerBound AND i.IndexId <= @PageUpperBound

    SELECT COUNT(*)
    FROM   #PageIndexForUsers

    DROP TABLE #PageIndexForUsers
END' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT 0
        RETURN
    END

    SELECT  COUNT(*)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
    WHERE   ApplicationId = @ApplicationId
        AND u.UserId = p.UserId
        AND (LastActivityDate <= @InactiveSinceDate)
        AND (
                (@ProfileAuthOptions = 2)
                OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
            )
END' 
END
/****** Object:  Table [dbo].[Tickets]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tickets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tickets](
	[Ticket_Key] [int] IDENTITY(1,1) NOT NULL,
	[Event_Key] [int] NOT NULL,
	[Ticket_Description] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Price] [money] NULL,
	[Capacity] [int] NULL,
	[Begin_Selling] [datetime] NULL,
	[Selling_Deadline] [datetime] NULL,
	[Last_Modified] [datetime] NULL,
	[isdonation] [bit] NULL,
	[Type] [int] NULL,
	[Product_Description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Calendar_Sensitive] [bit] NULL,
	[Calendar_Type] [int] NULL,
	[Lesson_Length] [int] NULL,
	[Lesson_Earliest_Time] [int] NULL,
	[Lesson_Latest_Time] [int] NULL,
	[Coupon_Code] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDemo] [bit] NULL,
	[Visible] [bit] NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[Ticket_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Question]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Question]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Question](
	[Question_Key] [int] IDENTITY(1,1) NOT NULL,
	[Event_Key] [int] NOT NULL,
	[The_Question] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Mandatory] [bit] NULL,
	[Question_Type] [int] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[Question_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Transactions]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transactions](
	[Tx_Key] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [money] NOT NULL,
	[Email_Buyer] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email_Seller] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Init_Date] [datetime] NOT NULL,
	[Event_Key] [int] NOT NULL,
	[Currency] [nchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Item_Description] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Tx_Status] [int] NULL,
	[Confirmation_Date] [datetime] NULL,
	[txn_id] [nchar](19) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[mc_gross] [money] NULL,
	[payer_id] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[tax] [money] NULL,
	[payment_status] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[payer_status] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[business_email] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[payer_email] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[payment_type] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[mc_currency] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[transaction_subject] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[last_name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[first_name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Transaction_Log] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GuestList_First_Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GuestList_Last_Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Service_Fee] [money] NULL,
	[IP_Address] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[fbid_Seller] [bigint] NULL,
	[fbid_Buyer] [bigint] NULL,
	[Merchant_Fee] [money] NULL,
	[Groupstore_Profit] [money] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Tx_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  StoredProcedure [dbo].[Delete_Groups]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Groups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Delete_Groups]
	@Resource_Key int
	
AS
BEGIN
	DELETE
	FROM FB_Users_Resource
	WHERE Resource_Key=@Resource_Key
	
	DELETE
	FROM Resource
	WHERE Resource_Key=@Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Delete_FB_Users_Resource]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_FB_Users_Resource]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Delete_FB_Users_Resource]
		@FBid bigint,
	@Resource_Key int
AS
BEGIN
	DELETE
	FROM FB_Users_Resource
	WHERE FBid = @FBid AND Resource_Key=@Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteInactiveProfiles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_DeleteInactiveProfiles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT  0
        RETURN
    END

    DELETE
    FROM    dbo.aspnet_Profile
    WHERE   UserId IN
            (   SELECT  UserId
                FROM    dbo.aspnet_Users u
                WHERE   ApplicationId = @ApplicationId
                        AND (LastActivityDate <= @InactiveSinceDate)
                        AND (
                                (@ProfileAuthOptions = 2)
                             OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                             OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                            )
            )

    SELECT  @@ROWCOUNT
END' 
END
/****** Object:  UserDefinedFunction [dbo].[Event_Finish_Selling]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Finish_Selling]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Event_Finish_Selling]
	(@Event_Key int
	)
RETURNS datetime
AS
	BEGIN
	declare @Begins_Selling datetime
	SELECT @Begins_Selling = Case WHEN(
							SELECT Max(Selling_Deadline) 
							FROM Tickets T
							WHERE Event_Key = @Event_Key)
					IS NULL THEN ''1/1/2100''
					ELSE	(SELECT Max(Selling_Deadline) 
							FROM Tickets T
							WHERE Event_Key = @Event_Key)
					END
	declare @Begins_Selling_Offset datetime
	SET @Begins_Selling_Offset = DATEADD(hh,(SELECT Timezone FROM Events WHERE Event_Key = @Event_Key),@Begins_Selling)
	RETURN @Begins_Selling_Offset
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_Begins_Selling]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Begins_Selling]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Event_Begins_Selling]
	(@Event_Key int
	)
RETURNS datetime
AS
	BEGIN
	declare @Begins_Selling datetime
	SELECT @Begins_Selling = Case WHEN(
							SELECT Min(Begin_Selling) 
							FROM Tickets T
							WHERE Event_Key = @Event_Key)
					IS NULL THEN ''1/1/2000''
					ELSE	(SELECT Min(Begin_Selling) 
							FROM Tickets T
							WHERE Event_Key = @Event_Key)
					END
	declare @Begins_Selling_Offset datetime
	SET @Begins_Selling_Offset = DATEADD(hh,(SELECT Timezone FROM Events WHERE Event_Key = @Event_Key),@Begins_Selling)
	RETURN @Begins_Selling_Offset
	END
' 
END

/****** Object:  Table [dbo].[Attendee_List]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Attendee_List]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Attendee_List](
	[Attendee_List_Key] [int] IDENTITY(1,1) NOT NULL,
	[Event_Key] [int] NOT NULL,
	[Tx_Key] [int] NOT NULL
) ON [PRIMARY]
END
/****** Object:  UserDefinedFunction [dbo].[Event_TicketsSold_Capacity_ByDate]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_TicketsSold_Capacity_ByDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'create FUNCTION [dbo].[Event_TicketsSold_Capacity_ByDate]
	(@Event_Key int,
	@Begin_Date datetime,
	 @End_Date datetime
	)
RETURNS int
AS
	BEGIN
	declare @TicketsSold int
	SELECT @TicketsSold = 			
				
			Case WHEN(SELECT Sum(Capacity) FROM Tickets			
					INNER JOIN Events
						ON Events.Event_Key = Tickets.Event_Key
					WHERE 
					Events.Event_Key = @Event_Key
					AND (Visible = 1 OR Visible is null)
					AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
					OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends)))
						AND (Visible = 1 OR Visible is null))
			IS NULL THEN 0
			ELSE (SELECT Sum(Capacity) FROM Tickets	
					INNER JOIN Events
						ON Events.Event_Key = Tickets.Event_Key		
					WHERE 
					Events.Event_Key = @Event_Key
					AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
					OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends)))
						AND (Visible = 1 OR Visible is null))
			END
			
	FROM Tickets
	WHERE Event_Key = @Event_Key
	
	RETURN @TicketsSold
	END' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_TicketsSold_Capacity]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_TicketsSold_Capacity]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Event_TicketsSold_Capacity]
	(@Event_Key int
	)
RETURNS int
AS
	BEGIN
	declare @TicketsSold int
	SELECT @TicketsSold = 			
				
			Case WHEN(SELECT Sum(Capacity) FROM Tickets			
					WHERE 
					Event_Key = @Event_Key)
			IS NULL THEN 0
			ELSE (SELECT Sum(Capacity) FROM Tickets			
					WHERE 
					Event_Key = @Event_Key)
			END
			
	FROM Tickets
	WHERE Event_Key = @Event_Key
	
	RETURN @TicketsSold
	END' 
END

/****** Object:  UserDefinedFunction [dbo].[Merchant_Fee_Calculate]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Merchant_Fee_Calculate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Merchant_Fee_Calculate]
	(@tx_Key int
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Revenue decimal(20,2)
	
	SELECT @Revenue = 
	(SELECT
	ROUND(CASE 
		WHEN Merchant_Fee IS NULL THEN 
			CASE WHEN Amount <= 0 THEN 0 ELSE (- 1 * (((Amount - Service_Fee) * .029) + .3)) END 
		ELSE - 1 * Merchant_Fee END, 2))
	FROM Transactions
	WHERE Tx_Key = @tx_Key
	
	RETURN Convert(decimal(20,2),@Revenue)
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_Revenue_ByDate]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Revenue_ByDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'Create FUNCTION [dbo].[Event_Revenue_ByDate]
	(@Event_Key int,
	@Begin_Date datetime,
	 @End_Date datetime
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Revenue decimal(20,2)
	SELECT @Revenue = Case WHEN( SELECT SUM(Amount) FROM Transactions
						INNER JOIN Events
						ON Events.Event_Key = Transactions.Event_Key
						WHERE 
						Events.Event_Key = @Event_Key			
						AND Tx_Status = 2
						AND @Begin_Date < Transactions.Init_Date
						AND @End_Date > Transactions.Init_Date
						AND (Visible = 1 OR Visible is null))
					IS NULL THEN 0
					ELSE (
						SELECT SUM(Amount) FROM Transactions
						INNER JOIN Events
						ON Events.Event_Key = Transactions.Event_Key
						WHERE 
						Events.Event_Key = @Event_Key			
						AND Tx_Status = 2
						AND @Begin_Date < Transactions.Init_Date
						AND @End_Date > Transactions.Init_Date
						AND (Visible = 1 OR Visible is null)
					)
					END
	RETURN @Revenue
	END
' 
END

/****** Object:  StoredProcedure [dbo].[Delete_Ticket_EventKey]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Ticket_EventKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Delete_Ticket_EventKey]
	@Event_Key int
AS
BEGIN
	DELETE
	FROM Tickets
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Delete_Ticket]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Ticket]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Delete_Ticket]
	@Ticket_Key int
AS
BEGIN

UPDATE Tickets	
	SET
		Visible = 0
	WHERE Ticket_Key = @Ticket_Key
END
' 
END
/****** Object:  Table [dbo].[Tickets_Purchased]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tickets_Purchased]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tickets_Purchased](
	[Tickets_Purchased_Key] [int] IDENTITY(1,1) NOT NULL,
	[Tx_Key] [int] NOT NULL,
	[Ticket_Key] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[TicketNum] [int] NULL,
	[Got_Tickets] [bigint] NULL,
	[DonationAmount] [money] NULL,
	[Start_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[First_Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Last_Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Last_Modified] [datetime] NULL,
	[Visible] [bit] NULL,
 CONSTRAINT [PK_Tickets_Purchased] PRIMARY KEY CLUSTERED 
(
	[Tickets_Purchased_Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  UserDefinedFunction [dbo].[Resource_TicketsSold_Capacity_ByDate]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_TicketsSold_Capacity_ByDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Resource_TicketsSold_Capacity_ByDate]
	(@Resource_Key int,
		@Begin_Date datetime,
	 @End_Date datetime
	)
RETURNS int
AS
	BEGIN
	declare @TicketsSold int
	SELECT @TicketsSold = 			
				
			Case WHEN (SELECT Sum(Capacity) FROM Tickets			
					INNER JOIN Events
					ON Events.Event_Key = Tickets.Event_Key
					WHERE
					Events.Resource_Key = @Resource_Key
					AND (Visible = 1 OR Visible is null)
					AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
					OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends))))
			IS NULL THEN 0
			ELSE (SELECT Sum(Capacity) FROM Tickets			
					INNER JOIN Events
					ON Events.Event_Key = Tickets.Event_Key
					WHERE
					Events.Resource_Key = @Resource_Key
					AND (Visible = 1 OR Visible is null)
					AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
					OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends))))
			END
			
	FROM Tickets
	INNER JOIN Events
	ON Events.Event_Key = Tickets.Event_Key
	WHERE
	Events.Resource_Key = @Resource_Key
	AND (Visible = 1 OR Visible is null)
	
	RETURN @TicketsSold
	END' 
END

/****** Object:  UserDefinedFunction [dbo].[Resource_Revenue_ByDate]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Revenue_ByDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Resource_Revenue_ByDate]
	(@Resource_Key int,
	 @Begin_Date datetime,
	 @End_Date datetime
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Revenue decimal(20,2)
	SELECT @Revenue = Case WHEN( SELECT SUM(Amount) FROM Transactions
						INNER JOIN Events
						ON Events.Event_Key = Transactions.Event_Key
						WHERE 
						Resource_Key = @Resource_Key			
						AND Tx_Status = 2
						AND @Begin_Date < Transactions.Init_Date
						AND @End_Date > Transactions.Init_Date
						AND (Visible = 1 OR Visible is null))
					IS NULL THEN 0
					ELSE (
						SELECT SUM(Amount) FROM Transactions
						INNER JOIN Events
						ON Events.Event_Key = Transactions.Event_Key
						WHERE 
						Resource_Key = @Resource_Key			
						AND Tx_Status = 2
						AND @Begin_Date < Transactions.Init_Date
						AND @End_Date > Transactions.Init_Date
						AND (Visible = 1 OR Visible is null)
					)
					END
	RETURN @Revenue
	END
' 
END

/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteProfiles]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_DeleteProfiles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteProfiles]
    @ApplicationName        nvarchar(256),
    @UserNames              nvarchar(4000)
AS
BEGIN
    DECLARE @UserName     nvarchar(256)
    DECLARE @CurrentPos   int
    DECLARE @NextPos      int
    DECLARE @NumDeleted   int
    DECLARE @DeletedUser  int
    DECLARE @TranStarted  bit
    DECLARE @ErrorCode    int

    SET @ErrorCode = 0
    SET @CurrentPos = 1
    SET @NumDeleted = 0
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    WHILE (@CurrentPos <= LEN(@UserNames))
    BEGIN
        SELECT @NextPos = CHARINDEX(N'','', @UserNames,  @CurrentPos)
        IF (@NextPos = 0 OR @NextPos IS NULL)
            SELECT @NextPos = LEN(@UserNames) + 1

        SELECT @UserName = SUBSTRING(@UserNames, @CurrentPos, @NextPos - @CurrentPos)
        SELECT @CurrentPos = @NextPos+1

        IF (LEN(@UserName) > 0)
        BEGIN
            SELECT @DeletedUser = 0
            EXEC dbo.aspnet_Users_DeleteUser @ApplicationName, @UserName, 4, @DeletedUser OUTPUT
            IF( @@ERROR <> 0 )
            BEGIN
                SET @ErrorCode = -1
                GOTO Cleanup
            END
            IF (@DeletedUser <> 0)
                SELECT @NumDeleted = @NumDeleted + 1
        END
    END
    SELECT @NumDeleted
    IF (@TranStarted = 1)
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END
    SET @TranStarted = 0

    RETURN 0

Cleanup:
    IF (@TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END
    RETURN @ErrorCode
END' 
END
/****** Object:  StoredProcedure [dbo].[Update_Question]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Question]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Question]
	@Question_Key int = NULL,
	@Event_Key int,
	@The_Question nvarchar(200),
	@Mandatory bit,
	@Question_Type int,
	@Question_Key_Return int Output
	
AS
BEGIN
DECLARE @Check_Question_Key int
SET @Check_Question_Key = (SELECT Question_Key FROM Question WHERE Question_Key = @Question_Key)

SET @Question_Key_Return = 0

If @Check_Question_Key is null --Create new record
	BEGIN
	INSERT INTO Question
	(Event_Key,The_Question,Mandatory,Question_Type)
	Values
	(@Event_Key,@The_Question,@Mandatory,@Question_Type)
	
	SET @Question_Key_Return = (SELECT MAX(Question_Key) FROM Question)
	END
ELSE --Update Existing Record
	BEGIN
	UPDATE Question
	SET
		Event_Key = @Event_Key,
		The_Question = @The_Question,
		Mandatory = @Mandatory,
		Question_Type = @Question_Type
	WHERE Question_Key = @Question_Key
	END
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Event]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Event]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Event]
	@Event_Key int = NULL,
	@Event_Name nvarchar(100),
	@Host nvarchar(100),
	@Event_Begins datetime,
	@Event_Ends datetime,
	@Location nvarchar(100),
	@Street nvarchar(100),
	@City nvarchar(100),
	@Phone nvarchar(15),
	@Email nvarchar(100),
	@Additional_Comments ntext,
	@Display_Tickets_Available bit,
	@Confirmation nvarchar(4000),
	@Resource_Key int,
	@eid varchar(40),	
	@Event_Key_Return int Output,
	@Ticket_Max int,
	@Timezone decimal,
	@TicketNum int,
	@Donation bit,
	@Event_Type int -- 0 or NULL = Regular / 1 = promote fb event

AS
BEGIN
SET @Event_Key_Return = 0

DECLARE @Check_Event_Key int
SET @Check_Event_Key = (SELECT Event_Key FROM Events WHERE Event_Key = @Event_Key)

If @Check_Event_Key is null --Create new record
	BEGIN
	DECLARE @CurrentDate datetime
	SET @CurrentDate = getdate()
	
	DECLARE @SFP money
	SET @SFP = (SELECT Service_Fee_Percentage FROM Resource WHERE Resource_Key = @Resource_Key)
	
	DECLARE @SFC money
	SET @SFC = (SELECT Service_Fee_Cents FROM Resource WHERE Resource_Key = @Resource_Key)
	
	DECLARE @SFM money
	SET @SFM = (SELECT Service_Fee_Max FROM Resource WHERE Resource_Key = @Resource_Key)
	
	INSERT INTO Events
	(Event_Name,Host,Event_Begins,Event_Ends,Location,Street,Phone,Email,
	Additional_Comments,Display_Tickets_Available,Confirmation,Resource_Key,City,eid,
	Last_Modified,Service_Fee_Percentage,Service_Fee_Cents,
	Service_Fee_Max,Ticket_Max,Timezone,TicketNum,Donation,Event_Type)
	Values
	(@Event_Name,@Host,@Event_Begins,@Event_Ends,@Location,@Street,@Phone,
	@Email,@Additional_Comments,@Display_Tickets_Available,@Confirmation,@Resource_Key,@City,@eid,
	@CurrentDate,@SFP,@SFC,@SFM,@Ticket_Max,@Timezone,@TicketNum,@Donation,@Event_Type)

	SET @Event_Key_Return = (SELECT Event_Key FROM Events WHERE Last_Modified = @CurrentDate)
	
	INSERT INTO Tickets
	(Event_Key,Ticket_Description,Price,Capacity,Begin_Selling,Selling_Deadline,Last_Modified,isdonation,IsDemo)
	Values
	(@Event_Key_Return,''Try Groupstore Demo Ticket'',20,100,''1/1/2011'',''1/1/2013'',@CurrentDate,0,1)
	
	END
ELSE --Update Existing Record
	BEGIN
	
	UPDATE Events
	SET
		Event_Name = @Event_Name,
		Host = @Host,
		Event_Begins = @Event_Begins,
		Event_Ends = @Event_Ends,
		Location = @Location,
		Street = @Street,
		City = @City,
		Phone = @Phone,
		Email = @Email,
		Additional_Comments = @Additional_Comments,
		Display_Tickets_Available = @Display_Tickets_Available,
		Confirmation = @Confirmation,		
		Ticket_Max = @Ticket_Max,
		Timezone=@Timezone,
		Donation=@Donation,
		Visible=1,
		Last_Modified = getdate()
	WHERE Event_Key = @Event_Key
	END
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_Out_Txkey]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_Out_Txkey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Transaction_Out_Txkey]	
	@Tx_Key int

AS
BEGIN

DECLARE @gsprofit decimal
SET @gsprofit = (SELECT Round(Groupstore_Profit,2) FROM Transactions WHERE Tx_Key = @Tx_Key)

DECLARE @amountpaid decimal
SET @amountpaid = (SELECT Round(Amount,2) FROM Transactions WHERE Tx_Key = @Tx_Key)

DECLARE @Event_Key int
SET @Event_Key = (SELECT Event_Key FROM Transactions WHERE Tx_Key = @Tx_Key)

DECLARE @Resource_Key int
SET @Resource_Key = (SELECT Resource_Key FROM Events WHERE Event_Key = @Event_Key)

DECLARE @Email_Recipient varchar(200)
SET @Email_Recipient = (SELECT Email_Paypal FROM Resource WHERE Resource_Key = @Resource_Key)

DECLARE @Amount decimal
SET @Amount = @amountpaid - @gsprofit

DECLARE @Currency nchar(3)
SET @Currency = (SELECT Currency FROM Transactions WHERE Tx_Key = @Tx_Key)
	
	INSERT INTO Transactions_Out
	(Amount,Email_Recipient,tx_date,Resource_Key,Event_Key,Paypal_Fee,Currency,FBid,Type)
	Values
	(@Amount,@Email_Recipient,getdate(),@Resource_Key,@Event_Key,0,@Currency,391377955486,0)

END' 
END
/****** Object:  StoredProcedure [dbo].[Update_Ticket]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Ticket]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Ticket]
	@Ticket_Key int = NULL,
	@Event_Key int,
	@Ticket_Description varchar(200),
	@Price money,
	@Capacity int,
	@Begin_Selling datetime,
	@Selling_Deadline datetime,
	@Ticket_Key_Return int Output,
	@IsDonation bit

AS
BEGIN
SET @Ticket_Key_Return = 0

DECLARE @Check_Ticket_Key int
SET @Check_Ticket_Key = (SELECT Ticket_Key FROM Tickets WHERE Ticket_Key = @Ticket_Key)

If @Check_Ticket_Key is null --Create new record
	BEGIN
	DECLARE @CurrentDate datetime
	SET @CurrentDate = getdate()
	
	INSERT INTO Tickets
	(Event_Key,Ticket_Description,Price,Capacity,Begin_Selling,Selling_Deadline,Last_Modified,isdonation)
	Values
	(@Event_Key,@Ticket_Description,@Price,@Capacity,@Begin_Selling,@Selling_Deadline,@CurrentDate,@IsDonation)
	
	SET @Ticket_Key_Return = (SELECT Ticket_Key FROM Tickets WHERE Last_Modified = @CurrentDate)
	END
ELSE --Update Existing Record
	BEGIN
	UPDATE Tickets
	SET
		Event_Key = @Event_Key,
		Ticket_Description = @Ticket_Description,
		Price = @Price,
		Capacity = @Capacity,
		Begin_Selling = @Begin_Selling,
		Selling_Deadline = @Selling_Deadline,
		isdonation = @IsDonation
	WHERE Ticket_Key = @Ticket_Key
	END
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_fbSeller]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_fbSeller]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Transaction_fbSeller]	
	@Tx_Key int,
	@fbseller bigint
AS
BEGIN
	
	UPDATE Transactions
	SET
		fbid_Seller = @fbseller
	WHERE Tx_Key = @Tx_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_fbids]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_fbids]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Transaction_fbids]
	@fbid_seller bigint,
	@fbid_buyer bigint,
	@Tx_Key int
AS
BEGIN
	
	UPDATE Transactions
	SET
		fbid_Seller = @fbid_seller,
		fbid_Buyer = @fbid_buyer
	WHERE Tx_Key = @Tx_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_DemoPay]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_DemoPay]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Transaction_DemoPay]
	@Tx_Key int
AS
BEGIN	
	UPDATE Transactions
	SET
		Tx_Status = 2		
	WHERE Tx_Key = @Tx_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_currency]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_currency]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Transaction_currency]	
	@Tx_Key int,
	@Currency nvarchar(3)
AS
BEGIN
	
	UPDATE Transactions
	SET
		Currency = @Currency
	WHERE Tx_Key = @Tx_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_AlreadyCart]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_AlreadyCart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Transaction_AlreadyCart]
	@Tx_Key int = NULL,		
	@Amount money,		
	@Service_Fee money

AS
BEGIN

DECLARE @Amount_Already money
SET @Amount_Already = (SELECT Amount FROM Transactions WHERE @Tx_Key = Tx_Key)

DECLARE @Service_Fee_Already money
SET @Service_Fee_Already = (SELECT Service_Fee FROM Transactions WHERE @Tx_Key = Tx_Key)

			UPDATE Transactions
			SET
			Amount = @Amount + @Amount_Already,
			Service_Fee = @Service_Fee + @Service_Fee_Already
			WHERE Tx_Key = @Tx_Key
			
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Events_Fundraiser_PDF]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Events_Fundraiser_PDF]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Events_Fundraiser_PDF]
	
	@Tx_Key int
AS
	DECLARE @Event_Key int
	SET @Event_Key = (SELECT Event_Key FROM Transactions WHERE Tx_Key = @Tx_Key)
	
	SELECT *
	FROM Events_Fundraiser_PDF	
	INNER JOIN Events
	ON Events.Event_Key = @Event_Key
	WHERE @Event_Key = Events_Fundraiser_PDF.Event_Key
	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Question_Specific]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Question_Specific]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Question_Specific]	
	@Question_Key int
AS
BEGIN
	SELECT Question_Key,
			Event_Key,
			The_Question,
			Mandatory,
			Question_Type,
		Question_Text = CASE WHEN Mandatory = 1 THEN ''*'' + The_Question + '':''
						ELSE The_Question + '':''
						END
	FROM Question
	WHERE Question_Key = @Question_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Question_FreeTickets]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Question_FreeTickets]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Question_FreeTickets]
	@Question_Key int
AS
BEGIN
	SELECT 	Event_Key,
			The_Question,
			Mandatory,
		Question_Text = CASE WHEN Mandatory = 1 THEN ''*'' + The_Question + '':''
						ELSE The_Question + '':''
						END
	FROM Question
	WHERE Question_Key = @Question_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Question]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Question]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Question]
	@Event_Key int
AS
BEGIN
	SELECT Question_Key,
			Event_Key,
			The_Question,
			Mandatory,
			Question_Type,
		Question_Text = CASE WHEN Mandatory = 1 THEN ''*'' + The_Question + '':''
						ELSE The_Question + '':''
						END
	FROM Question
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_IsProductEvent]    Script Date: 03/31/2011 18:02:04 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_IsProductEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_IsProductEvent]
	@Event_Key int
AS
BEGIN
	SELECT Type
	FROM Tickets
	WHERE Event_Key = @Event_Key
	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_Specific2]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_Specific2]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Ticket_Specific2]
	@Ticket_key int
AS
BEGIN
	SELECT *
	FROM Tickets T
	WHERE 
	Ticket_Key = @Ticket_Key	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_Lesson_Length]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_Lesson_Length]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Ticket_Lesson_Length]
	@Event_Key int,
	@Lesson_Length int
AS
BEGIN
	SELECT Ticket_Key, Price
	FROM Tickets T
	WHERE 
	Event_Key = @Event_Key
	AND Lesson_Length = @Lesson_Length
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Resource_FromTxKey]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Resource_FromTxKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Resource_FromTxKey]
	@Tx_Key int
AS
BEGIN

	SELECT Events.Resource_Key,
		Resource.Group_Name,
		Resource.Desired_Currency,
		Resource.Email_Paypal,
		Resource.Service_Fee_Percentage,
		Resource.Service_Fee_Cents,
		Resource.Service_Fee_Max,
		Resource.Pay_Method
	FROM Transactions
	INNER JOIN Events
	ON Transactions.Event_Key = Events.Event_Key
	INNER JOIN Resource
	ON Events.Resource_Key = Resource.Resource_Key
	WHERE Tx_Key = @Tx_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Transaction_Isdemo]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Transaction_Isdemo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Transaction_Isdemo]	
	@Tx_Key int
AS
	SELECT Resource.Demo		
	FROM Transactions
	INNER JOIN Events
	ON Transactions.Event_Key = Events.Event_Key
	INNER JOIN Resource
	ON Resource.Resource_Key = Events.Resource_Key
	WHERE @Tx_Key = Transactions.Tx_Key
	RETURN

' 
END
/****** Object:  StoredProcedure [dbo].[View_Transaction_Details_Txnid]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Transaction_Details_Txnid]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Transaction_Details_Txnid]
	
	@Tx_Key int
AS
	SELECT *
	FROM Transactions
	WHERE @Tx_Key = Tx_Key
	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Transaction_Details]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Transaction_Details]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Transaction_Details]
	
	@Tx_Key int
AS
	SELECT *
	FROM Transactions
	WHERE @Tx_Key = Tx_Key
	RETURN
' 
END
/****** Object:  View [dbo].[vw_Attendee_List_Transactions]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_Attendee_List_Transactions]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vw_Attendee_List_Transactions]
AS
SELECT     Event_Key, fbid_Seller, fbid_Buyer, GuestList_First_Name, GuestList_Last_Name, Amount, Init_Date, Confirmation_Date, Tx_Key, ROUND(Service_Fee, 2) 
                      AS Service_Fee, ROUND(Amount - Service_Fee, 2) AS Price, dbo.Merchant_Fee_Calculate(Tx_Key) AS Paypal_Fee, Amount - ROUND(Service_Fee, 2) 
                      + dbo.Merchant_Fee_Calculate(Tx_Key) AS Revenue, Currency, Merchant_Fee, Groupstore_Profit, 
                      Groupstore_Profit + (CASE WHEN Groupstore_Profit <= 0 THEN 0 ELSE - 1 * ((Groupstore_Profit * .029) + .3) END) AS Groupstore_Profit_Adjusted,
                          (SELECT     Resource_Key
                            FROM          dbo.Events
                            WHERE      (dbo.Transactions.Event_Key = Event_Key)) AS Resource_Key
FROM         dbo.Transactions
WHERE     (Tx_Status = 2)
' 
/****** Object:  StoredProcedure [dbo].[View_TicketSpecificIPNing_Soldout]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_TicketSpecificIPNing_Soldout]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_TicketSpecificIPNing_Soldout]
	@Tx_key int
AS
BEGIN
	SELECT 
		Sold_Out = Case 
					WHEN (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2) >= T.Capacity THEN ''Sold Out''
					ELSE ''Available''
					END
	FROM Tickets T
	INNER JOIN Tickets_Purchased TP
	ON TP.Ticket_Key = T.Ticket_Key
	WHERE TP.Tx_Key = @Tx_Key
END' 
END
/****** Object:  StoredProcedure [dbo].[View_TicketSpecific_Soldout]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_TicketSpecific_Soldout]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[View_TicketSpecific_Soldout]
	@Ticket_Key int
AS
BEGIN
	SELECT 
		Sold_Out = Case 
					WHEN (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2) >= T.Capacity THEN ''Sold Out''
					ELSE ''Available''
					END
	FROM Tickets T
	WHERE Ticket_Key = @Ticket_Key
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Tickets_Soldout]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Tickets_Soldout]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Tickets_Soldout]
	@Event_Key int
AS
BEGIN
	SELECT 
		Ticket_Key,
		isdonation,
		Capacity,
		Tickets_Left = (Capacity - Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
						ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END)
						,
		Sold_Out = Case 
					WHEN (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2) >= T.Capacity THEN ''Sold Out''
					ELSE ''Available''
					END
	FROM Tickets T
	WHERE Event_Key = @Event_Key
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Reporting_HostTicketByUser]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Reporting_HostTicketByUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Reporting_HostTicketByUser]
	@Resource_Key int,
	@Begin_Date datetime,
	 @End_Date datetime
AS
--DECLARE @Resource_Key int
--SET @Resource_Key = (SELECT Resource_Key FROM FB_Users WHERE FBid = @FBid)

BEGIN

	SELECT *,Resource_Key,
		Tickets_Sold = Convert(varchar(10),(
		
			Case WHEN(
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND Tx_Status = 2)
			IS NULL THEN 0
			ELSE (
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND Tx_Status = 2)
			END
			)
			) 
			+ ''/'' + Convert(varchar(10),Capacity)
	FROM Tickets
	INNER JOIN Events
	ON Events.Event_Key = Tickets.Event_Key
	WHERE Resource_Key = @Resource_Key 
	AND (Visible = 1 OR Visible is null)
	AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
	OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends)))
END

' 
END
/****** Object:  StoredProcedure [dbo].[View_Reporting_HostChart]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Reporting_HostChart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Reporting_HostChart]
	@Resource_Key int,
	@Begin_Date datetime,
	 @End_Date datetime
AS
	SELECT  		
		Tix_Sold_Date = CAST( FLOOR( CAST( Init_Date AS FLOAT ) ) AS DATETIME ),		
		Event_Name
	FROM Tickets_Purchased
		INNER JOIN Transactions
		ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
		INNER JOIN Tickets
		ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
		INNER JOIN Events
		ON Events.Event_Key = Transactions.Event_Key
	WHERE
	Events.Resource_Key = @Resource_Key
	AND (Events.Visible = 1 OR Events.Visible is null)
	AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
	OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends)))

	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Store_Tickets_Soldout_Sellers]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Store_Tickets_Soldout_Sellers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Store_Tickets_Soldout_Sellers]
	@Event_Key int,
	@fbid bigint
AS
BEGIN
	SELECT 
		T.Ticket_Key,
		T.Capacity,
		Tickets_Left = (Capacity - Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
						ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END)
						,
		Sold_Out = Case 
					WHEN (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2) >= T.Capacity THEN ''Sold Out''
					ELSE ''Available''
					END
	FROM FB_Users_Sellers
	INNER JOIN Tickets T
	ON T.Ticket_Key = FB_Users_Sellers.Ticket_Key
	WHERE FB_Users_Sellers.Event_Key = @Event_Key
	AND FB_Users_Sellers.FBid = @fbid
	AND T.Begin_Selling < getdate()
	AND T.Selling_Deadline > getdate()
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Store_Ticket_Sellers]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Store_Ticket_Sellers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Store_Ticket_Sellers]
	@Event_Key int,
	@fbid bigint
AS
BEGIN
	SELECT 
		FB_Users_Sellers.Ticket_Key,
		FB_Users_Sellers.Event_Key,
		T.Ticket_Description,
		T.Price,
		T.Capacity,
		T.Begin_Selling,
		T.Selling_Deadline,
		T.Last_Modified,
		T.isdonation,
		Display_Tickets_Available = (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key),
		Quantity_Remaining = CONVERT(varchar(4),Capacity -(
									
									Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP																				
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
									ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP										
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END)),
		ROUND(Price,2) AS PriceRounded,
		Order_Form_Description = CASE (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key)
									WHEN 1 THEN Ticket_Description + '' - '' +  CONVERT(varchar(4),Capacity -(
									
									Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP																				
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
						ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP										
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END
									
									
									)) + '' remaining''
									ELSE Ticket_Description
								END,
		Sale_Begins = CONVERT(VARCHAR(20), T.Begin_Selling,100) ,
		Sale_Ends = CONVERT(VARCHAR(20), T.Selling_Deadline,100),
		Ticket_Max = (SELECT Ticket_Max FROM Events WHERE Events.Event_Key = @Event_Key)
	FROM FB_Users_Sellers
	INNER JOIN Tickets T
	ON T.Ticket_Key = FB_Users_Sellers.Ticket_Key
	WHERE FB_Users_Sellers.Event_Key = @Event_Key
	AND T.Begin_Selling < getdate()
	AND T.Selling_Deadline > getdate()
--	AND FB_Users_Sellers.FBid = @fbid
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_ForEmail]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_ForEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Ticket_ForEmail]
	@Tx_Key int
AS
BEGIN
	SELECT *, Purchase_Description = (CONVERT(varchar(20), Quantity) + '' '' + Ticket_Description + '' [$'' + CONVERT(varchar(20), Price) + '']  '')
	FROM Tickets_Purchased TP
	INNER JOIN Tickets
	ON Tickets.Ticket_Key = TP.Ticket_Key
	WHERE Tx_Key = @Tx_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_Demo]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_Demo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Ticket_Demo]
	@Event_Key int
AS
BEGIN
	SELECT 
		*,
		Display_Tickets_Available, --= (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key),
		Quantity_Remaining = CONVERT(varchar(4),Capacity -(
									
									Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP																				
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
									ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP										
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END)),
		ROUND(Price,2) AS PriceRounded,
		Order_Form_Description = CASE (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key)
									WHEN 1 THEN Ticket_Description + '' - '' +  CONVERT(varchar(4),Capacity -(
									
									Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP																				
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
						ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP										
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END
									
									
									)) + '' remaining''
									ELSE Ticket_Description
								END,
		--Sale_Begins = CONVERT(VARCHAR(20), T.Begin_Selling,100) ,
		--Sale_Ends = CONVERT(VARCHAR(20), T.Selling_Deadline,100),
		Sale_Begins = CONVERT(VARCHAR(20), DATEADD(hh,(SELECT Timezone FROM Events WHERE Event_Key = @Event_Key),T.Begin_Selling),100) ,
		Sale_Ends = CONVERT(VARCHAR(20), DATEADD(hh,(SELECT Timezone FROM Events WHERE Event_Key = @Event_Key),T.Selling_Deadline),100),
		Ticket_Max, --= (SELECT Ticket_Max FROM Events WHERE Event_Key = @Event_Key),
		Timezone,
		Timezoneshort = (SELECT Timezones_Textshort FROM InfoTimezones WHERE InfoTimezones.Timezones_Value = Timezone)
	FROM Tickets T
	INNER JOIN Events
	ON T.Event_Key = Events.Event_Key
	WHERE T.Event_Key = @Event_Key
	AND T.Begin_Selling < getdate()
	AND T.Selling_Deadline > getdate()
	AND (T.Visible = 1 OR T.Visible is null)	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_ByUser]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_ByUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Ticket_ByUser]
	@Resource_Key int,
	@Current_Previous int --0 for Current, 1 for Previous
AS
--DECLARE @Resource_Key int
--SET @Resource_Key = (SELECT Resource_Key FROM FB_Users WHERE FBid = @FBid)

BEGIN
IF @Current_Previous = 0
	BEGIN
	SELECT *,Resource_Key,
		Tickets_Sold = Convert(varchar(10),(
		
			Case WHEN(
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND Tx_Status = 2)
			IS NULL THEN 0
			ELSE (
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND Tx_Status = 2)
			END
			)
			) 
			+ ''/'' + Convert(varchar(10),Capacity)
	FROM Tickets
	INNER JOIN Events
	ON Events.Event_Key = Tickets.Event_Key
	WHERE Resource_Key = @Resource_Key 
	--AND Events.Begin_Selling <= getdate()
	--AND Events.Selling_Deadline > getdate()
	AND dbo.Event_Finish_Selling(Events.Event_Key) > getdate()
	AND (Events.Visible = 1 OR Events.Visible is null)
	AND (Tickets.IsDemo = 0 OR Tickets.IsDemo is null)
	END	
IF @Current_Previous = 1
	BEGIN
	SELECT *,Resource_Key,
		Tickets_Sold = Convert(varchar(10),(
		
		Case WHEN(
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND Tx_Status = 2)
			IS NULL THEN 0
			ELSE (
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND Tx_Status = 2)
			END
		
		)
		) 
			+ ''/'' + Convert(varchar(10),Capacity)
	FROM Tickets
	INNER JOIN Events
	ON Events.Event_Key = Tickets.Event_Key
	WHERE Resource_Key = @Resource_Key 
	--AND Events.Selling_Deadline < getdate()
	AND dbo.Event_Finish_Selling(Events.Event_Key) < getdate()
	AND (Events.Visible = 1 OR Events.Visible is null)
	AND (Tickets.IsDemo = 0 OR Tickets.IsDemo is null)
	END
END

' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_All]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_All]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Ticket_All]
	@Event_Key int
AS
BEGIN
	SELECT 
		*,
		Display_Tickets_Available = (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key),
		Quantity_Remaining = CONVERT(varchar(4),Capacity -(
									
									Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP																				
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
									ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP										
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END)),
		ROUND(Price,2) AS PriceRounded,
		Order_Form_Description = CASE (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key)
									WHEN 1 THEN Ticket_Description + '' - '' +  CONVERT(varchar(4),Capacity -(
									
									Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP																				
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
						ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP										
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END
									
									
									)) + '' remaining''
									ELSE Ticket_Description
								END,
		Sale_Begins = CONVERT(VARCHAR(20), DATEADD(hh,(SELECT Timezone FROM Events WHERE Event_Key = @Event_Key),Begin_Selling),100) ,
		Sale_Ends = CONVERT(VARCHAR(20), DATEADD(hh,(SELECT Timezone FROM Events WHERE Event_Key = @Event_Key),Selling_Deadline),100)
	FROM Tickets T
	WHERE Event_Key = @Event_Key
	AND (Visible = 1 OR Visible is null)
	AND (T.IsDemo = 0 OR T.IsDemo is null)
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Ticket]
	@Event_Key int
AS
BEGIN
	SELECT 
		*,
		Display_Tickets_Available, --= (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key),
		Quantity_Remaining = CONVERT(varchar(4),Capacity -(
									
									Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP																				
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
									ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP										
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END)),
		ROUND(Price,2) AS PriceRounded,
		Order_Form_Description = CASE (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key)
									WHEN 1 THEN Ticket_Description + '' - '' +  CONVERT(varchar(4),Capacity -(
									
									Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP																				
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
						ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP										
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END
									
									
									)) + '' remaining''
									ELSE Ticket_Description
								END,
		--Sale_Begins = CONVERT(VARCHAR(20), T.Begin_Selling,100) ,
		--Sale_Ends = CONVERT(VARCHAR(20), T.Selling_Deadline,100),
		Sale_Begins = CONVERT(VARCHAR(20), DATEADD(hh,(SELECT Timezone FROM Events WHERE Event_Key = @Event_Key),T.Begin_Selling),100) ,
		Sale_Ends = CONVERT(VARCHAR(20), DATEADD(hh,(SELECT Timezone FROM Events WHERE Event_Key = @Event_Key),T.Selling_Deadline),100),
		Ticket_Max, --= (SELECT Ticket_Max FROM Events WHERE Event_Key = @Event_Key),
		Timezone,
		Timezoneshort = (SELECT Timezones_Textshort FROM InfoTimezones WHERE InfoTimezones.Timezones_Value = Timezone)
	FROM Tickets T
	INNER JOIN Events
	ON T.Event_Key = Events.Event_Key
	WHERE T.Event_Key = @Event_Key
	AND T.Begin_Selling < getdate()
	AND T.Selling_Deadline > getdate()
	AND (T.Visible = 1 OR T.Visible is null)
	AND (T.IsDemo = 0 OR T.IsDemo is null)
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_Specific]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_Specific]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Ticket_Specific]
	@Ticket_Description nvarchar(200),
	@Event_Key int,
	@Price money
AS
BEGIN
	SELECT Ticket_Key
	FROM Tickets T
	WHERE 
	(Ticket_Description = @Ticket_Description
	OR 
	@Ticket_Description = Ticket_Description + '' - '' +  CONVERT(varchar(4),Capacity -(
						Case	WHEN(SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP																				
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
										IS NULL THEN 0
						ELSE (SELECT SUM(Quantity) 
										FROM Tickets_Purchased TP										
										INNER JOIN Transactions
										ON TP.Tx_Key = Transactions.Tx_Key
										WHERE TP.Ticket_Key = T.Ticket_Key
										AND Transactions.Tx_Status=2)
						END)) + '' remaining'')
	AND Event_Key = @Event_Key
	AND Price = @Price
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_ProductAvailability_Lessons]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_ProductAvailability_Lessons]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Ticket_ProductAvailability_Lessons]
	@Event_Key int,
	@Start_Date datetime
AS
BEGIN



	SELECT 
		*,
		Display_Tickets_Available = (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key),		
		ROUND(Price,2) AS PriceRounded,
		Sale_Begins = CONVERT(VARCHAR(20), Begin_Selling,100) ,
		Sale_Ends = CONVERT(VARCHAR(20), Selling_Deadline,100)
	FROM Tickets_Purchased
	INNER JOIN Transactions
	ON Tickets_Purchased.Tx_Key = Transactions.Tx_Key
	INNER JOIN Tickets
	ON Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
	WHERE @Event_Key = Tickets.Event_Key
	AND Transactions.Tx_Status=2
	AND Datediff(Day,@Start_Date,Tickets_Purchased.Start_Date) = 0
	ORDER BY Tickets_Purchased.Start_Date ASC
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_IsProducttxkey]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_IsProducttxkey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_IsProducttxkey]
	@Tx_Key int
AS
BEGIN
	SELECT Type
	FROM Tickets_Purchased
	INNER JOIN Tickets
	ON Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
	WHERE Tickets_Purchased.Tx_Key = @Tx_Key
	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_PayCC]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_PayCC]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_PayCC]
	@Tx_Key int
AS
BEGIN
	SELECT *, Purchase_Description = (CONVERT(varchar(20), Quantity) + '' '' + Ticket_Description + '' [$'' + CONVERT(varchar(20), Price) + '']  ''),
			Sale_Ends = CONVERT(VARCHAR(20), Tickets.Selling_Deadline,100),
			ROUND(Price,2) AS PriceRounded,
			Tix_Amount = CONVERT(VARCHAR(20),ROUND((Price * Quantity),2))
	FROM Tickets_Purchased TP
	INNER JOIN Tickets
	ON Tickets.Ticket_Key = TP.Ticket_Key
	WHERE Tx_Key = @Tx_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Reporting_AdminChart]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Reporting_AdminChart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Reporting_AdminChart]	
	@Begin_Date datetime,
	 @End_Date datetime
AS
	SELECT  		
		Tix_Sold_Date = CAST( FLOOR( CAST( Init_Date AS FLOAT ) ) AS DATETIME ),		
		Event_Name
	FROM Tickets_Purchased
		INNER JOIN Transactions
		ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
		INNER JOIN Tickets
		ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
		INNER JOIN Events
		ON Events.Event_Key = Transactions.Event_Key
	WHERE	
	 (Events.Visible = 1 OR Events.Visible is null)
	AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
	OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends)))
	--ORDER BY Event_Name ASC

	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Attendee_List_Sellers]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Attendee_List_Sellers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Attendee_List_Sellers]
	@Event_Key int,
	@fbid bigint
AS
BEGIN		
		SELECT 
		GuestList_First_Name = CASE 
						WHEN GuestList_First_Name = '''' THEN first_name 
						ELSE GuestList_First_Name
						END,
		GuestList_Last_Name = CASE 
						WHEN GuestList_Last_Name = '''' THEN last_name
						ELSE GuestList_Last_Name
						END,
		GuestList_Full_Name = CASE 
						WHEN GuestList_First_Name = '''' THEN first_name 
						ELSE GuestList_First_Name
						END
						 + '' '' 
						 +CASE 
						WHEN GuestList_Last_Name = '''' THEN last_name
						ELSE GuestList_Last_Name
						END,
		Email = Payer_Email,
		Quantity,
		Ticket_Description,
		Amount,
		Init_Date,
		Confirmation_Date,
		Tickets_Purchased.Tx_Key,
		Ticket_Transaction_Key = Convert(varchar(10),Transactions.Tx_Key) + Convert(varchar(10),Tickets_Purchased_Key),
		First_Name,
		Last_Name,
		TixNum = ''#'' + Convert(varchar(10),Tickets_Purchased.TicketNum),
		Got_Tickets,
		Got_Tickets_Name = (SELECT First_Name + '' '' + Last_Name FROM FB_Users WHERE FBid = Tickets_Purchased.Got_Tickets),
		Transactions.fbid_Seller,
		Transactions.fbid_Buyer	
	FROM Tickets_Purchased
	INNER JOIN Transactions
		ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
	INNER JOIN Tickets
		ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
	WHERE 
		Tickets.Event_Key = @Event_Key
		AND Tx_Status = 2 
		AND fbid_Seller = @fbid--It is in confirmation state
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Attendee_List]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Attendee_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Attendee_List]
	@Event_Key int
AS
BEGIN		
		SELECT 
		GuestList_First_Name = CASE 
						WHEN Tickets_Purchased.First_Name is not null THEN Tickets_Purchased.First_Name
						WHEN GuestList_First_Name = '''' THEN Transactions.first_name 
						ELSE GuestList_First_Name
						END,
		GuestList_Last_Name = CASE 
						WHEN Tickets_Purchased.Last_Name is not null THEN Tickets_Purchased.Last_Name
						WHEN GuestList_Last_Name = '''' THEN Transactions.last_name
						ELSE GuestList_Last_Name
						END,
		GuestList_Full_Name = CASE 
						WHEN Tickets_Purchased.First_Name is not null THEN Tickets_Purchased.First_Name
						WHEN GuestList_First_Name = '''' THEN Transactions.first_name 
						ELSE GuestList_First_Name
						END
						 + '' '' 
						 +CASE 
						 WHEN Tickets_Purchased.Last_Name is not null THEN Tickets_Purchased.Last_Name
						WHEN GuestList_Last_Name = '''' THEN Transactions.last_name
						ELSE GuestList_Last_Name
						END,
		Email = Payer_Email,
		Quantity,
		Ticket_Description,
		Amount,
		Init_Date,
		Confirmation_Date = CASE 
							WHEN Confirmation_Date is null THEN Init_Date
							ELSE Confirmation_Date
							END,
		Tickets_Purchased.Tx_Key,
		Ticket_Transaction_Key = Convert(varchar(10),Transactions.Tx_Key) + Convert(varchar(10),Tickets_Purchased.Tickets_Purchased_Key),
		Transactions.First_Name,
		Transactions.Last_Name,
		TixNum = ''#'' + Convert(varchar(10),Tickets_Purchased.TicketNum),
		Got_Tickets,
		Got_Tickets_Name = (SELECT First_Name + '' '' + Last_Name FROM FB_Users WHERE FBid = Tickets_Purchased.Got_Tickets),
		Transactions.fbid_Seller,
		fbid_Seller_Name = (SELECT First_Name + '' '' + Last_Name FROM FB_Users WHERE FBid = Transactions.fbid_Seller),
		Transactions.fbid_Buyer,
		Currency,
		Tickets_Purchased_Key
	FROM Tickets_Purchased
	INNER JOIN Transactions
		ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
	INNER JOIN Tickets
		ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
	WHERE 
		Tickets.Event_Key = @Event_Key
		AND Tx_Status = 2 --It is in confirmation state
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_Update_Fee_Profit]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_Update_Fee_Profit]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Transaction_Update_Fee_Profit]
	(
	@Tx_Key int	)


AS
	BEGIN
	DECLARE @Event_Key int
	SET @Event_Key = (SELECT Event_Key FROM Transactions WHERE Tx_Key = @Tx_Key)
		
	DECLARE @Service_Fee money
	SET @Service_Fee = (SELECT Service_Fee FROM Transactions WHERE Tx_Key = @Tx_Key)
	
	DECLARE @Amount money
	SET @Amount = (SELECT Amount FROM Transactions WHERE Tx_Key = @Tx_Key)
	
	DECLARE @Amount_Minus_Paypal money
	SET @Amount_Minus_Paypal = (CASE WHEN @Amount <= 0 THEN 0 ELSE ((((@Amount - @Service_Fee) * .029) + .3)) END)
	
	DECLARE @Resource_Key int
	SET @Resource_Key = (SELECT Resource_Key FROM Events WHERE Event_Key = @Event_Key)
	
	DECLARE @Merchant_Fee_Percentage money
	SET @Merchant_Fee_Percentage = (SELECT Service_Fee_Percentage FROM Events WHERE Event_Key = @Event_Key)
	
	DECLARE @Merchant_Fee_Cents money
	SET @Merchant_Fee_Cents = (SELECT Service_Fee_Cents FROM Events WHERE Event_Key = @Event_Key)
	
	DECLARE @Merchant_Fee_Max money
	SET @Merchant_Fee_Max = (SELECT Service_Fee_Max FROM Events WHERE Event_Key = @Event_Key)
	
	DECLARE @Profit_Amount money
	SET @Profit_Amount = 0
	
	DECLARE @Profit_Amount_temp money
	SET @Profit_Amount_temp = 0

--Do the cursor to calculate Profit amount
	DECLARE @Ticket_Key int
	DECLARE @Quantity int
	DECLARE @DonationAmount money
	
	DECLARE c1 CURSOR READ_ONLY
	FOR
	SELECT Ticket_Key, Quantity, DonationAmount
	FROM Tickets_Purchased
	WHERE Tx_Key = @Tx_Key
	
	OPEN c1

	FETCH NEXT FROM c1
	INTO @Ticket_Key,@Quantity,@DonationAmount

	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		SET @Profit_Amount_temp = @Merchant_Fee_Cents + (.01 * @Merchant_Fee_Percentage *
			CASE
			WHEN @DonationAmount IS NULL OR @DonationAmount <= 0 --Regular Ticket
			THEN
				(SELECT Price FROM Tickets WHERE @Ticket_Key = Ticket_Key)
			 
			ELSE  --Donation Ticket								
				@DonationAmount
			END)

		SET @Profit_Amount = @Profit_Amount + (@Quantity *
			CASE 
			WHEN @Profit_Amount_temp < @Merchant_Fee_Max
			THEN @Profit_Amount_temp
			ELSE @Merchant_Fee_Max
			END
			)

		FETCH NEXT FROM c1
		INTO @Ticket_Key,@Quantity,@DonationAmount

	END

	CLOSE c1
	DEALLOCATE c1
	
	
--Cursor ends	
		
	
	UPDATE Transactions
	SET
	Merchant_Fee = Case 
					WHEN (SELECT IsHideFee FROM Events WHERE Event_Key=@Event_Key) = 1
					THEN @Profit_Amount --Merchant accepts service fee cost,
					ELSE @Amount_Minus_Paypal
					END,					
	Groupstore_Profit = @Profit_Amount
	WHERE Tx_Key = @Tx_Key


	END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Calendar]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Calendar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Calendar]
	@Resource_Key int,
	@Type int
AS
BEGIN

	SELECT 
		Tickets_Purchased.Tx_Key,
		Item_Description,
		Confirmation_Date,
		Amount,
		Full_Name = First_Name + '' '' + Last_Name,
		Calendar_Start_Date = Tickets_Purchased.Start_Date,
		Calendar_End_Date = Tickets_Purchased.End_Date,
		Tickets.Calendar_Type
FROM Tickets_Purchased
INNER JOIN Transactions
ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
INNER JOIN Tickets
ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
INNER JOIN Events
ON Events.Event_Key = Tickets.Event_Key
WHERE Events.Resource_Key = @Resource_Key
AND Tickets_Purchased.Start_Date IS NOT NULL
AND Tickets_Purchased.End_Date IS NOT NULL
AND Transactions.Tx_Status=2
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Attendee_List_Transactions_Sellers]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Attendee_List_Transactions_Sellers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Attendee_List_Transactions_Sellers]
	@Event_Key int,
	@fbid bigint
AS
BEGIN		
		SELECT 
		GuestList_First_Name,
		GuestList_Last_Name,		
		Amount,
		Init_Date,
		Confirmation_Date,
		Tx_Key,
		Service_Fee,
		Price = (Amount - Service_Fee),
		Paypal_Fee = dbo.Merchant_Fee_Calculate(Tx_Key),
		Revenue = (Amount - Service_Fee) + dbo.Merchant_Fee_Calculate(Tx_Key),
		Currency
													
	FROM Transactions
	WHERE
		Event_Key = @Event_Key
		AND Tx_Status = 2
		AND fbid_Seller = @fbid --It is in confirmation state
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Attendee_List_Transactions]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Attendee_List_Transactions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Attendee_List_Transactions]
	@Event_Key int
AS
BEGIN		
		SELECT 		
		GuestList_First_Name = CASE 						
						WHEN GuestList_First_Name = '''' THEN Transactions.first_name 
						ELSE GuestList_First_Name
						END,
		GuestList_Last_Name = CASE 						
						WHEN GuestList_Last_Name = '''' THEN Transactions.last_name
						ELSE GuestList_Last_Name
						END,
		GuestList_Full_Name = CASE 						
						WHEN GuestList_First_Name = '''' THEN Transactions.first_name 
						ELSE GuestList_First_Name
						END
						 + '' '' 
						 +CASE 						
						WHEN GuestList_Last_Name = '''' THEN Transactions.last_name
						ELSE GuestList_Last_Name
						END,
		Amount,--3
		Init_Date,--4
		Confirmation_Date= CASE 
							WHEN Confirmation_Date is null THEN Init_Date
							ELSE Confirmation_Date
							END,--5
		Transactions.Tx_Key,--6
		Service_Fee = ROUND(Service_Fee,2),--7
		Price = ROUND((Amount - Service_Fee),2),--8
		Paypal_Fee = ROUND(dbo.Merchant_Fee_Calculate(Transactions.Tx_Key),2),--9
		Revenue = ROUND((Amount - Service_Fee) + dbo.Merchant_Fee_Calculate(Transactions.Tx_Key),2),--10
		Currency,--11
		fbid_Seller,--12
		fbid_Seller_Name = (SELECT First_Name + '' '' + Last_Name FROM FB_Users WHERE FBid = Transactions.fbid_Seller)
													
	FROM Transactions
	WHERE
		Event_Key = @Event_Key
		AND Tx_Status = 2 --It is in confirmation state
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Email_Receipt]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Email_Receipt]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Email_Receipt]
	
	@Tx_Key int
AS
	SELECT *, Amount2Dec = convert(numeric(10,2), mc_gross),
	Ticket_Number = Convert(varchar(10),Tickets_Purchased.TicketNum),
		txn_id,
		Transactions.Event_Key,
		payer_email 
	FROM Transactions
	INNER JOIN Events
	ON Events.Event_Key = Transactions.Event_Key
	INNER JOIN Tickets_Purchased
	ON Tickets_Purchased.Tx_Key = Transactions.Tx_Key
	INNER JOIN Resource
	ON Resource.Resource_Key = Events.Resource_Key
	WHERE @Tx_Key = Transactions.Tx_Key
	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Tickets_Purchased_Demo]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Tickets_Purchased_Demo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Tickets_Purchased_Demo]
	@Tx_Key int,
	@Ticket_Key int,
	@Quantity int,
	@donationamount money,
	@Tx_Key_Return int Output,
	@First_Name varchar(50),
	@Last_Name varchar(50)
AS
BEGIN

DECLARE @CurrentDate datetime
	SET @CurrentDate = getdate()

	Insert INTO
		Tickets_Purchased
		(Tx_Key,Ticket_Key,Quantity,DonationAmount,Last_Modified,First_Name,Last_Name)
	VALUES
		(@Tx_Key,@Ticket_Key,@Quantity,@donationamount,@CurrentDate,@First_Name,@Last_Name)

	SET @Tx_Key_Return = (SELECT Top 1 Tickets_Purchased_Key FROM Tickets_Purchased WHERE Last_Modified = @CurrentDate)
END' 
END
/****** Object:  StoredProcedure [dbo].[Update_Tickets_Purchased_Calendar]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Tickets_Purchased_Calendar]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_Tickets_Purchased_Calendar]
	@Tx_Key int,
	@Ticket_Key int,
	@Quantity int,
	@donationamount money,
	@Start_Date datetime,
	@End_Date datetime
AS
BEGIN
	Insert INTO
		Tickets_Purchased
		(Tx_Key,Ticket_Key,Quantity,DonationAmount,Start_Date,End_Date)
	VALUES
		(@Tx_Key,@Ticket_Key,@Quantity,@donationamount,@Start_Date,@End_Date)
END' 
END
/****** Object:  StoredProcedure [dbo].[Update_Tickets_Purchased]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Tickets_Purchased]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Tickets_Purchased]
	@Tx_Key int,
	@Ticket_Key int,
	@Quantity int,
	@donationamount money,
	@Tx_Key_Return int Output,
	@First_Name varchar(50),
	@Last_Name varchar(50)
AS
BEGIN

DECLARE @CurrentDate datetime
	SET @CurrentDate = getdate()

	Insert INTO
		Tickets_Purchased
		(Tx_Key,Ticket_Key,Quantity,DonationAmount,Last_Modified,First_Name,Last_Name)
	VALUES
		(@Tx_Key,@Ticket_Key,@Quantity,@donationamount,@CurrentDate,@First_Name,@Last_Name)

	SET @Tx_Key_Return = (SELECT Top 1 Tickets_Purchased_Key FROM Tickets_Purchased WHERE Last_Modified = @CurrentDate)
END' 
END
/****** Object:  StoredProcedure [dbo].[Update_GotTicket]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_GotTicket]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Update_GotTicket]
	@Tx_Key int,
	@fbid bigint
AS
BEGIN
IF @fbid = 0 --Set back to null
	BEGIN
		UPDATE Tickets_Purchased
		SET
			Got_Tickets=null
		WHERE Tx_Key=@Tx_Key		
	END
ELSE
	BEGIN
		UPDATE Tickets_Purchased
		SET
			Got_Tickets=@fbid
		WHERE Tx_Key=@Tx_Key		
	END
END' 
END
/****** Object:  UserDefinedFunction [dbo].[Event_TicketsSold_Progress_Sellers]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_TicketsSold_Progress_Sellers]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'create FUNCTION [dbo].[Event_TicketsSold_Progress_Sellers]
	(@Event_Key int, @fbid bigint
	)
RETURNS int
AS
	BEGIN
	declare @TicketsSold int
	SELECT @TicketsSold = 			
			
		
		
			Case WHEN(
				SELECT SUM(Quantity) FROM Tickets_Purchased
				INNER JOIN Transactions
				ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
				INNER JOIN Tickets
				ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
				WHERE 
				Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
				AND Tx_Status = 2
				AND Tickets.Event_Key=@Event_Key
				AND fbid_Seller = @fbid)
				IS NULL THEN 0
			ELSE (
				SELECT SUM(Quantity) FROM Tickets_Purchased
				INNER JOIN Transactions
				ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
				INNER JOIN Tickets
				ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
				WHERE 
				Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
				AND Tx_Status = 2
				AND Tickets.Event_Key=@Event_Key
				AND fbid_Seller = @fbid)
			END
			
	FROM Tickets
	WHERE Event_Key = @Event_Key
	
	RETURN @TicketsSold
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_TicketsSold_Progress_ByDate]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_TicketsSold_Progress_ByDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Event_TicketsSold_Progress_ByDate]
	(@Event_Key int,
	@Begin_Date datetime,
	 @End_Date datetime
	)
RETURNS int
AS
	BEGIN
	declare @TicketsSold int
	SELECT @TicketsSold = 			
			
		
		
			Case WHEN(
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			INNER JOIN Tickets
			ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
			INNER JOIN Events
			ON Events.Event_Key = Transactions.Event_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND (Visible = 1 OR Visible is null)
			AND Tx_Status = 2
			AND Tickets.Event_Key=@Event_Key
			AND @Begin_Date < Transactions.Init_Date
			AND @End_Date > Transactions.Init_Date)
			IS NULL THEN 0
			ELSE (
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			INNER JOIN Tickets
			ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
			INNER JOIN Events
			ON Events.Event_Key = Transactions.Event_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND (Visible = 1 OR Visible is null)
			AND Tx_Status = 2
			AND Tickets.Event_Key=@Event_Key
			AND @Begin_Date < Transactions.Init_Date
			AND @End_Date > Transactions.Init_Date)
			END
			
	FROM Tickets
	INNER JOIN Events
	ON Events.Event_Key = Tickets.Event_Key
	WHERE Events.Event_Key = @Event_Key
	AND (Visible = 1 OR Visible is null)
	RETURN @TicketsSold
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_TicketsSold_Progress]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_TicketsSold_Progress]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Event_TicketsSold_Progress]
	(@Event_Key int
	)
RETURNS int
AS
	BEGIN
	declare @TicketsSold int
	SELECT @TicketsSold = 			
			
		
		
			Case WHEN(
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			INNER JOIN Tickets
			ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND Tx_Status = 2
			AND Tickets.Event_Key=@Event_Key)
			IS NULL THEN 0
			ELSE (
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			INNER JOIN Tickets
			ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND Tx_Status = 2
			AND Tickets.Event_Key=@Event_Key)
			END
			
	FROM Tickets
	WHERE Event_Key = @Event_Key
	
	RETURN @TicketsSold
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Ticket_Number_Generator]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Number_Generator]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Ticket_Number_Generator]
	(@Tickets_Purchased_Key int
	)
RETURNS int
AS
	BEGIN
	--declare @TicketNum int
--	SELECT @TicketNum = Case WHEN
	--						(SELECT SUM(Quantity) FROM Tickets_Purchased WHERE @Ticket_Key = Ticket_Key)
					--IS NULL THEN 0
					--ELSE	(SELECT SUM(Quantity) FROM Tickets_Purchased WHERE @Ticket_Key = Ticket_Key)
					--END
	
	declare @Ticket_Key int
	SET @Ticket_Key = (SELECT Ticket_Key FROM Tickets_Purchased WHERE @Tickets_Purchased_Key = Tickets_Purchased_Key)

	declare @Event_Key int
	SELECT @Event_Key = (SELECT Event_Key FROM Tickets WHERE @Ticket_Key = Ticket_Key)
	
--	declare @strtxkey nvarchar(20)
--	SET @strtxkey = CONVERT(nvarchar(20), (SELECT top 1 tx_key FROM Tickets_Purchased WHERE @Ticket_Key = Ticket_Key ORDER By tx_key desc))

	declare @TicketNum_return int
	--SET @TicketNum_return = @strtxkey +  CONVERT(nvarchar(50),@TicketNum + (SELECT TicketNum FROM Events WHERE @Event_Key = Event_Key))
	SET @TicketNum_return = CONVERT(nvarchar(50),(SELECT TicketNum FROM Events WHERE @Event_Key = Event_Key)) +  CONVERT(nvarchar(50),@Tickets_Purchased_Key)
	RETURN @TicketNum_return
	END
' 
END

/****** Object:  Table [dbo].[Questions_Answered]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Questions_Answered]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Questions_Answered](
	[Questions_Answered_key] [int] IDENTITY(1,1) NOT NULL,
	[Tx_Key] [int] NOT NULL,
	[Question_Key] [int] NOT NULL,
	[The_Answer] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Event_Key] [int] NULL,
	[Tickets_Purchased_Key] [int] NULL,
 CONSTRAINT [PK_Questions_Answered] PRIMARY KEY CLUSTERED 
(
	[Questions_Answered_key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  UserDefinedFunction [dbo].[Product_TicketsPurchased_Price]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product_TicketsPurchased_Price]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'create FUNCTION [dbo].[Product_TicketsPurchased_Price]
	(@Tickets_Purchased_Key int)

RETURNS money
AS
	BEGIN

	DECLARE @Ticket_Key int
	SET @Ticket_Key = (SELECT Ticket_Key FROM Tickets_Purchased WHERE @Tickets_Purchased_Key = Tickets_Purchased_Key)

	DECLARE @isdonation bit
	SET @isdonation = (SELECT isdonation FROM Tickets WHERE Ticket_Key = @Ticket_Key)

	IF (SELECT DonationAmount FROM Tickets_Purchased WHERE Tickets_Purchased_Key = @Tickets_Purchased_Key) > 0
		BEGIN
		SET @isdonation = 1
		END

	DECLARE @Correct_Price money	

	IF @isdonation = 1 --isdonation
		BEGIN
		SET @Correct_Price = (SELECT DonationAmount FROM Tickets_Purchased WHERE Tickets_Purchased_Key = @Tickets_Purchased_Key)
		END
	ELSE
		BEGIN
		SET @Correct_Price = (SELECT Price FROM Tickets WHERE Ticket_Key = @Ticket_Key) * (SELECT Quantity FROM Tickets_Purchased WHERE @Tickets_Purchased_Key = Tickets_Purchased_Key)
		END


	
	RETURN @Correct_Price
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Product_Availability]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product_Availability]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Product_Availability]
	(@Ticket_Key int,
	@Start_Date datetime,
	@End_Date datetime	
	)
RETURNS int
AS
	BEGIN
	DECLARE @Calendar_Sensitive bit
	SET @Calendar_Sensitive = (Select Calendar_Sensitive FROM Tickets WHERE Ticket_Key = @Ticket_Key) 
	
	DECLARE @Already_Taken int
	SET @Already_Taken = (CASE 
						WHEN @Calendar_Sensitive = 1 --it is calendar sensitive
						THEN 
							(SELECT SUM(Quantity) FROM Tickets_Purchased
							INNER JOIN Transactions
							ON Tickets_Purchased.Tx_Key = Transactions.Tx_Key
							WHERE Tickets_Purchased.Ticket_Key = @Ticket_Key 
							AND 
							
							((Tickets_Purchased.End_Date > @Start_Date AND Tickets_Purchased.End_Date < @End_Date)
							OR (Tickets_Purchased.Start_Date < @End_Date AND Tickets_Purchased.End_Date > @End_Date)
							OR (Tickets_Purchased.Start_Date = @Start_Date AND Tickets_Purchased.End_Date = @End_Date))
							
							AND Transactions.Tx_Status=2)
						ELSE
							(SELECT SUM(Quantity) FROM Tickets_Purchased 
							INNER JOIN Transactions
							ON Tickets_Purchased.Tx_Key = Transactions.Tx_Key
							WHERE Tickets_Purchased.Ticket_Key = @Ticket_Key							
							AND Transactions.Tx_Status=2)
						END)
						
	IF @Already_Taken is null
		BEGIN
		SET @Already_Taken = 0
		END
	
	DECLARE @Availability int
	SET @Availability = ((Select Capacity FROM Tickets WHERE Ticket_Key = @Ticket_Key) 
						- @Already_Taken)
						
	IF @Availability < 0
		BEGIN
		SET @Availability = 0
		END
	
	RETURN @Availability
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Resource_TicketsSold_Progress_ByDate]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_TicketsSold_Progress_ByDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Resource_TicketsSold_Progress_ByDate]
	(@Resource_Key int,
		@Begin_Date datetime,
	 @End_Date datetime
	)
RETURNS int
AS
	BEGIN
	declare @TicketsSold int
	SELECT @TicketsSold = 			
			
		
		
			Case WHEN(
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			INNER JOIN Tickets
			ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
			INNER JOIN Events
			ON Events.Event_Key = Transactions.Event_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND (Visible = 1 OR Visible is null)
			AND Tx_Status = 2			
			AND Events.Resource_Key = @Resource_Key
			AND @Begin_Date < Transactions.Init_Date
			AND @End_Date > Transactions.Init_Date)
			IS NULL THEN 0
			ELSE (
			SELECT SUM(Quantity) FROM Tickets_Purchased
			INNER JOIN Transactions
			ON Transactions.Tx_Key = Tickets_Purchased.Tx_Key
			INNER JOIN Tickets
			ON Tickets.Ticket_Key = Tickets_Purchased.Ticket_Key
			INNER JOIN Events
			ON Events.Event_Key = Transactions.Event_Key
			WHERE 
			Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
			AND (Visible = 1 OR Visible is null)
			AND Tx_Status = 2			
			AND Events.Resource_Key = @Resource_Key
			AND @Begin_Date < Transactions.Init_Date
			AND @End_Date > Transactions.Init_Date)
			END
			
	FROM Tickets
	INNER JOIN Events
	ON Events.Event_Key = Tickets.Event_Key
	WHERE
	Events.Resource_Key = @Resource_Key
	AND (Visible = 1 OR Visible is null)
	RETURN @TicketsSold
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Resource_NetRevenue_Currency]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_NetRevenue_Currency]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Resource_NetRevenue_Currency]
	(@Resource_Key int,
	@Currency nvarchar(3)
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Revenue decimal(20,2)
	SELECT @Revenue = Case 
					WHEN(SELECT SUM(Groupstore_Profit_Adjusted) FROM vw_Attendee_List_Transactions
						WHERE 
						Resource_Key = @Resource_Key
						AND Currency = @Currency
						AND Amount > 0)
					IS NULL THEN 0					
					ELSE (
						SELECT SUM(Groupstore_Profit_Adjusted) FROM vw_Attendee_List_Transactions
						WHERE 
						Resource_Key = @Resource_Key			
						AND Currency = @Currency
						AND Amount > 0
					)
					END
	RETURN @Revenue
	END
' 
END

/****** Object:  StoredProcedure [dbo].[Delete_Question_EventKey]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Question_EventKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[Delete_Question_EventKey]
	@Event_Key int
AS
BEGIN
	DELETE
	FROM Questions_Answered
	WHERE Event_Key = @Event_Key

	DELETE
	FROM Question
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[Delete_Question]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delete_Question]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Delete_Question]
	@Question_Key int
AS
BEGIN
	DELETE
	FROM Questions_Answered
	WHERE Question_Key = @Question_Key

	DELETE
	FROM Question
	WHERE Question_Key = @Question_Key
END
' 
END
/****** Object:  UserDefinedFunction [dbo].[Event_Revenue]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Revenue]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Event_Revenue]
	(@Event_Key int
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Revenue decimal(20,2)
	SELECT @Revenue = Case 
					WHEN(SELECT SUM(Revenue) FROM vw_Attendee_List_Transactions
						WHERE 
						Event_Key = @Event_Key								
						AND Amount > 0)
					IS NULL THEN 0					
					ELSE (
						SELECT SUM(Revenue) FROM vw_Attendee_List_Transactions
						WHERE 
						Event_Key = @Event_Key			
						AND Amount > 0
					)
					END
	RETURN @Revenue
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_Revenue_Sellers_FullAmount]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Revenue_Sellers_FullAmount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'create FUNCTION [dbo].[Event_Revenue_Sellers_FullAmount]
	(@Event_Key int, @fbid bigint
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Revenue decimal(20,2)
	SELECT @Revenue = Case 
					WHEN(SELECT SUM(Amount) FROM vw_Attendee_List_Transactions
						WHERE 
						Event_Key = @Event_Key								
						AND Amount > 0
						AND fbid_Seller = @fbid)
					IS NULL THEN 0					
					ELSE (
						SELECT SUM(Amount) FROM vw_Attendee_List_Transactions
						WHERE 
						Event_Key = @Event_Key			
						AND Amount > 0
						AND fbid_Seller = @fbid
					)
					END
	RETURN @Revenue
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_Revenue_Sellers]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Revenue_Sellers]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'create FUNCTION [dbo].[Event_Revenue_Sellers]
	(@Event_Key int, @fbid bigint
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Revenue decimal(20,2)
	SELECT @Revenue = Case 
					WHEN(SELECT SUM(Revenue) FROM vw_Attendee_List_Transactions
						WHERE 
						Event_Key = @Event_Key								
						AND Amount > 0
						AND fbid_Seller = @fbid)
					IS NULL THEN 0					
					ELSE (
						SELECT SUM(Revenue) FROM vw_Attendee_List_Transactions
						WHERE 
						Event_Key = @Event_Key			
						AND Amount > 0
						AND fbid_Seller = @fbid
					)
					END
	RETURN @Revenue
	END
' 
END

/****** Object:  UserDefinedFunction [dbo].[Event_Revenue_Currency]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Revenue_Currency]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'create FUNCTION [dbo].[Event_Revenue_Currency]
	(@Event_Key int,
	@Currency nvarchar(3)
	)
RETURNS decimal(20,2)
AS
	BEGIN
	declare @Revenue decimal(20,2)
	SELECT @Revenue = Case 
					WHEN(SELECT SUM(Revenue) FROM vw_Attendee_List_Transactions
						WHERE 
						Event_Key = @Event_Key								
						AND Currency = @Currency
						AND Amount > 0)
					IS NULL THEN 0					
					ELSE (
						SELECT SUM(Revenue) FROM vw_Attendee_List_Transactions
						WHERE 
						Event_Key = @Event_Key			
						AND Currency = @Currency
						AND Amount > 0
					)
					END
	RETURN @Revenue
	END
' 
END

/****** Object:  StoredProcedure [dbo].[Update_Ticket_NumFreeEvents]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Ticket_NumFreeEvents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Ticket_NumFreeEvents]
	@Tx_Key int = NULL

AS
BEGIN

	UPDATE Tickets_Purchased
			SET
			TicketNum = dbo.Ticket_Number_Generator(Tickets_Purchased_Key)
			WHERE Tx_Key = @Tx_Key
			
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Questions_Answered]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Questions_Answered]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Questions_Answered]
	@Tx_Key int = NULL,
	@Question_Key int,
	@Event_Key int,
	@The_Answer nvarchar(500),
	@Tix_Purchased_Key int

AS
BEGIN


	INSERT INTO Questions_Answered
	(Tx_Key,Question_Key,Event_Key,The_Answer,Tickets_Purchased_Key)
	Values
	(@Tx_Key,@Question_Key,@Event_Key,@The_Answer,@Tix_Purchased_Key)
	
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Accepted_Money]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Accepted_Money]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Accepted_Money]
	@Resource_Key int
AS
BEGIN
	SELECT 
	TotalAmount = SUM(Amount)
	FROM vw_Attendee_List_Transactions
	INNER JOIN Events
	ON Events.Event_Key = vw_Attendee_List_Transactions.Event_Key
	WHERE Events.Resource_Key = @Resource_Key
	
END
' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction_CartRemoveTicket]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction_CartRemoveTicket]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Transaction_CartRemoveTicket]
	@Tx_Key int,
	@Tickets_Purchased_Key int

AS
BEGIN

DECLARE @Ticket_Key int
SET @Ticket_Key = (SELECT Ticket_Key FROM Tickets_Purchased WHERE @Tickets_Purchased_Key = Tickets_Purchased_Key)

DECLARE @Amount_Already money
SET @Amount_Already = (SELECT Amount FROM Transactions WHERE @Tx_Key = Tx_Key)

DECLARE @Service_Fee_Already money
SET @Service_Fee_Already = (SELECT Service_Fee FROM Transactions WHERE @Tx_Key = Tx_Key)

DECLARE @isdonation bit
SET @isdonation = (SELECT isdonation FROM Tickets WHERE Ticket_Key = @Ticket_Key)

IF (SELECT DonationAmount FROM Tickets_Purchased WHERE Tickets_Purchased_Key = @Tickets_Purchased_Key) > 0
	BEGIN
	SET @isdonation = 1
	END

DECLARE @Event_Key int
SET @Event_Key = (SELECT Event_Key FROM Tickets WHERE Ticket_Key = @Ticket_Key)

DECLARE @SFP money
SET @SFP = (SELECT Service_Fee_Percentage FROM Events WHERE @Event_Key = Event_Key)

DECLARE @SFC money
SET @SFC = (SELECT Service_Fee_Cents FROM Events WHERE @Event_Key = Event_Key)

DECLARE @SFM money
SET @SFM = (SELECT Service_Fee_Max FROM Events WHERE @Event_Key = Event_Key)

DECLARE @Quantity money
SET @Quantity = (SELECT Quantity FROM Tickets_Purchased WHERE @Tickets_Purchased_Key = Tickets_Purchased_Key)

DECLARE @TempAmount money
SET @TempAmount = dbo.Product_TicketsPurchased_Price(@Tickets_Purchased_Key)

--IF @isdonation = 1 --isdonation
--	BEGIN
--	SET @TempAmount = (SELECT DonationAmount FROM Tickets_Purchased WHERE Tickets_Purchased_Key = @Tickets_Purchased_Key)
--	END
--ELSE
--	BEGIN
--	SET @TempAmount = (SELECT Price FROM Tickets WHERE Ticket_Key = @Ticket_Key) * @Quantity						
--	END

DECLARE @Service_Fee_Remove money
SET @Service_Fee_Remove = (@TempAmount * @SFP * .01) + @SFC
IF @Service_Fee_Remove > @SFM
	BEGIN
	SET @Service_Fee_Remove = @SFM
	END


			UPDATE Transactions
			SET
			Amount = @Amount_Already - @TempAmount - @Service_Fee_Remove,
			Service_Fee = @Service_Fee_Already - @Service_Fee_Remove
			WHERE Tx_Key = @Tx_Key
			
			
			
	DELETE
	FROM Tickets_Purchased
	WHERE Tickets_Purchased_Key = @Tickets_Purchased_Key	
END' 
END
/****** Object:  StoredProcedure [dbo].[Update_Transaction]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_Transaction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Update_Transaction]
	@Tx_Key int = NULL,
	@Event_Key int,
	@Item_Description nvarchar(500),	
	@Amount money,
	@Currency nchar(3),
	@Tx_Key_Return int Output,
	@First_Name varchar(50),
	@Last_Name varchar(50),
	@Email_Buyer nvarchar(200),
	@Email_Seller nvarchar(200),
	@Tx_Status int,
	@txn_id nchar(19),
	@payer_id nvarchar(100),
	@tax money,
	@payment_status nvarchar(100),
	@payer_status nvarchar(100),
	@business_email nvarchar(200),
	@payer_email nvarchar(200),
	@payment_type nvarchar(200),
	@mc_currency nchar(20),
	@Transaction_Log nvarchar(400),
	@GuestList_First_Name nvarchar(200),
	@GuestList_Last_Name nvarchar(200),
	@Service_Fee money,
	@IP_Address nvarchar(50)

AS
BEGIN


SET @Tx_Key_Return = 0

IF @Tx_Status = 1 --Clicked on order form & went to paypal
	BEGIN
	
	DECLARE @CurrentDate datetime
	SET @CurrentDate = getdate()

	INSERT INTO Transactions
	(Event_Key,Item_Description,Amount,Currency,Tx_Status,Init_Date,Email_Buyer,Email_Seller,GuestList_First_Name,GuestList_Last_Name,Service_Fee,IP_Address)
	Values
	(@Event_Key,@Item_Description,@Amount,@Currency,1,@CurrentDate,@Email_Buyer,@Email_Seller,@GuestList_First_Name,@GuestList_Last_Name,@Service_Fee,@IP_Address)
	
	SET @Tx_Key_Return = (SELECT Tx_Key FROM Transactions WHERE Init_Date = @CurrentDate)
	END
IF @Tx_Status = 2 --Update Existing Record to be confirmed payment
	BEGIN
	--DECLARE @Check_Tx_Key int
	--SET @Check_Tx_Key = (SELECT Tx_Key FROM Transactions WHERE Tx_Key = @Tx_Key)
	--	IF @Check_Tx_Key <> NULL
	--		BEGIN
			UPDATE Transactions
			SET
			Tx_Status = 2,		
			Confirmation_Date = getdate(),
			First_Name = @First_Name,
			Last_Name = @Last_Name,
			txn_id = @txn_id,
			mc_gross = @Amount,
			payer_id = @payer_id,
			tax = @tax,
			payment_status = @payment_status,
			payer_status = @payer_status,
			business_email = @business_email,
			payer_email = @payer_email,
			payment_type = @payment_type,
			mc_currency = @mc_currency,
			transaction_subject = @Item_Description,
			Transaction_Log = @Transaction_Log
			WHERE Tx_Key = @Tx_Key
			
			UPDATE Tickets_Purchased
			SET
			TicketNum = dbo.Ticket_Number_Generator(Tickets_Purchased_Key)
			WHERE Tx_Key = @Tx_Key
			
			exec dbo.Update_Transaction_Update_Fee_Profit @Tx_Key
	--		END
		-- ELSE NEED ELSE IF ERROR OCCURS, WRONG TX_KEY
	END
IF @Tx_Status = 3 --Clicked on order form for a FREE event
	BEGIN
	
	DECLARE @CurrentDateFree datetime
	SET @CurrentDateFree = getdate()

	INSERT INTO Transactions
	(Event_Key,Item_Description,Amount,Currency,Tx_Status,Init_Date,Email_Buyer,Email_Seller,First_Name,Last_Name,payer_email,GuestList_First_Name,GuestList_Last_Name,Service_Fee,IP_Address)
	Values
	(@Event_Key,@Item_Description,@Amount,@Currency,2,@CurrentDateFree,@Email_Buyer,@Email_Seller,@First_Name,@Last_Name,@payer_email,@GuestList_First_Name,@GuestList_Last_Name,@Service_Fee,@IP_Address)
	
	
	
	SET @Tx_Key_Return = (SELECT Tx_Key FROM Transactions WHERE Init_Date = @CurrentDateFree)
	END

IF @Tx_Status = 4 --Clicked on Credit Card adding info
	BEGIN
		UPDATE Transactions
			SET
			Tx_Status = 2,		
			Confirmation_Date = getdate(),
			First_Name = @First_Name,
			Last_Name = @Last_Name,
			txn_id = @txn_id,
			mc_currency = @mc_currency,
			mc_gross = @Amount,			
			payer_email = @payer_email			
			WHERE Tx_Key = @Tx_Key
			
			UPDATE Tickets_Purchased
			SET
			TicketNum = dbo.Ticket_Number_Generator(Ticket_Key)
			WHERE Tx_Key = @Tx_Key
			
			exec dbo.Update_Transaction_Update_Fee_Profit @Tx_Key
	END
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Cart_Tickets]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Cart_Tickets]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Cart_Tickets]
	@Tx_Key int
AS
BEGIN



	SELECT *,
	Tickets.Ticket_Description,
	PriceTotal = dbo.Product_TicketsPurchased_Price(Tickets_Purchased.Tickets_Purchased_Key),
	Amount_Total = Convert(decimal(20,2),Amount),
	Service_Fee_Total = Convert(decimal(20,2),Service_Fee)
	FROM Tickets_Purchased
	INNER JOIN Tickets
	ON Tickets_Purchased.Ticket_Key = Tickets.Ticket_Key
	INNER JOIN Transactions
	ON Tickets_Purchased.Tx_Key = Transactions.Tx_Key
	WHERE Tickets_Purchased.Tx_Key = @Tx_Key	
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Questions_Answered_Sellers]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Questions_Answered_Sellers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Questions_Answered_Sellers]
	@Event_Key int,
	@fbid bigint
AS
BEGIN
	SELECT 
		Questions_Answered_Key,
		Questions_Answered.Tx_Key,
		Question_Key,
		The_Answer,
		Questions_Answered.Event_Key,
		Ticket_Transaction_Key = Convert(varchar(10),Questions_Answered.Tx_Key) + Convert(varchar(10),Tickets_Purchased_Key)
		
	FROM Questions_Answered
	INNER JOIN Transactions
		ON Transactions.Tx_Key = Questions_Answered.Tx_Key	
	INNER JOIN Tickets_Purchased
		ON Tickets_Purchased.Tx_Key = Questions_Answered.Tx_Key	
	WHERE 
		Questions_Answered.Event_Key = @Event_Key
		AND Tx_Status = 2 --It is in confirmation state
		AND fbid_Seller = @fbid
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Questions_Answered]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Questions_Answered]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Questions_Answered]
	@Event_Key int
AS
BEGIN
	SELECT 
		Questions_Answered_Key,
		Questions_Answered.Tx_Key,
		Question_Key,
		The_Answer,
		Questions_Answered.Event_Key,
		Questions_Answered.Tickets_Purchased_Key		
	FROM Questions_Answered
	INNER JOIN Transactions
		ON Transactions.Tx_Key = Questions_Answered.Tx_Key	
	INNER JOIN Tickets_Purchased
		ON Tickets_Purchased.Tx_Key = Questions_Answered.Tx_Key	
	WHERE 
		Questions_Answered.Event_Key = @Event_Key
		AND Tx_Status = 2 --It is in confirmation state
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Ticket_ProductAvailability]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Ticket_ProductAvailability]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Ticket_ProductAvailability]
	@Event_Key int,
	@Start_Date datetime,
	@End_Date datetime,
	@Amount int
AS
BEGIN



	SELECT 
		*,
		Display_Tickets_Available = (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key),		
		ROUND(Price,2) AS PriceRounded,
		Order_Form_Description = CASE (SELECT Display_Tickets_Available FROM Events WHERE @Event_Key = Events.Event_Key)
									WHEN 1 THEN Ticket_Description + '' - '' +  CONVERT(varchar(4),
									dbo.Product_Availability(Ticket_Key,@Start_Date,@End_Date))
									 + '' remaining''
									ELSE Ticket_Description
								END,
		Sale_Begins = CONVERT(VARCHAR(20), Begin_Selling,100) ,
		Sale_Ends = CONVERT(VARCHAR(20), Selling_Deadline,100),
		Ticket_Max = (SELECT Ticket_Max FROM Events WHERE Event_Key = @Event_Key),
		Quantity_Remaining = dbo.Product_Availability(Ticket_Key,@Start_Date,@End_Date)
	FROM Tickets T
	WHERE Event_Key = @Event_Key
	AND Begin_Selling < getdate()
	AND Selling_Deadline > getdate()
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Reporting_HostEvents]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Reporting_HostEvents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Reporting_HostEvents]
	@Resource_Key int,
	@Begin_Date datetime,
	 @End_Date datetime
AS
	SELECT  
	Event_Key,Event_Name,eid,
	Revenue = Convert(decimal(20,2),dbo.Event_Revenue_ByDate(Event_Key,@Begin_Date,@End_Date)),
		   Tickets_Sold = dbo.Event_TicketsSold_Progress_ByDate(Event_Key,@Begin_Date,@End_Date),
		   Tickets_Capacity = dbo.Event_TicketsSold_Capacity_ByDate(Event_Key,@Begin_Date,@End_Date)
	FROM Events
	WHERE
	Events.Resource_Key = @Resource_Key
	AND (Visible = 1 OR Visible is null)
	AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
	OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends)))

	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Reporting_AdminOverall]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Reporting_AdminOverall]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Reporting_AdminOverall]
	@Begin_Date datetime,
	 @End_Date datetime
AS
	SELECT Resource_Key, Group_Name, Service_Fee_Percentage, Service_Fee_Cents,
	Current_Events = (SELECT Count(Event_Key) FROM Events WHERE Events.Resource_Key=Resource.Resource_Key AND Selling_Deadline > getdate() AND (Visible = 1 OR Visible is null)),
	Total_Events = (SELECT Count(Event_Key) FROM Events WHERE Events.Resource_Key=Resource.Resource_Key AND (Visible = 1 OR Visible is null)),
	Creator = (SELECT Max(First_Name) + '' '' + Max(Last_Name) FROM FB_Users WHERE FBCreator = FBid),
	Revenue_Date = dbo.Resource_Revenue_ByDate(Resource_Key,@Begin_Date,@End_Date),
	Revenue_Total = dbo.Resource_Revenue_ByDate(Resource_Key,''01/01/2000'',''01/01/2200''),
	TicketsSold_Date = dbo.Resource_TicketsSold_Progress_ByDate(Resource_Key,@Begin_Date,@End_Date),
	TicketsSold_Total = dbo.Resource_TicketsSold_Progress_ByDate(Resource_Key,''01/01/2000'',''01/01/2200''),
	TicketsCapacity_Date = dbo.Resource_TicketsSold_Capacity_ByDate(Resource_Key,@Begin_Date,@End_Date),
	TicketsCapacity_Total = dbo.Resource_TicketsSold_Capacity_ByDate(Resource_Key,''01/01/2000'',''01/01/2200''),
	AmountEvents_Date = dbo.Resource_AmountEvents_ByDate(Resource_Key,@Begin_Date,@End_Date)
	FROM Resource

	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Reporting_AdminEvents]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Reporting_AdminEvents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Reporting_AdminEvents]
	@Begin_Date datetime,
	 @End_Date datetime
AS
	SELECT Resource_Key, 
	Event_Key,Event_Name,eid,
	Revenue = Convert(decimal(20,2),dbo.Event_Revenue_ByDate(Event_Key,@Begin_Date,@End_Date)),
		   Tickets_Sold = dbo.Event_TicketsSold_Progress_ByDate(Event_Key,@Begin_Date,@End_Date),
		   Tickets_Capacity = dbo.Event_TicketsSold_Capacity_ByDate(Event_Key,@Begin_Date,@End_Date)
	FROM Events
	WHERE
	(Visible = 1 OR Visible is null)
	AND NOT (((@Begin_Date < Events.Event_Begins) AND (@End_Date < Events.Event_Begins))
	OR ((@Begin_Date > Events.Event_Ends) AND (@End_Date > Events.Event_Ends)))

	RETURN
' 
END
/****** Object:  StoredProcedure [dbo].[View_Sellers_EventKey]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Sellers_EventKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Sellers_EventKey]	
	@Event_Key int
AS
BEGIN
	SELECT
		DISTINCT (FB_Users_Sellers.Event_Key),
		FB_Users_Sellers.FBid,
		Event_Name, 		
		Event_Begins,		
		Ticket_Max,
		Service_Fee_Percentage,
		Service_Fee_Cents,
		Service_Fee_Max,
		eid,
		Amount_Sold = dbo.Event_Revenue_Sellers_FullAmount(@Event_Key,FB_Users_Sellers.FBid)
	FROM FB_Users_Sellers
	INNER JOIN Events
	ON Events.Event_Key = FB_Users_Sellers.Event_Key
	INNER JOIN FB_Users
	ON FB_Users_Sellers.FBid = FB_Users.FBid
	WHERE		
		@Event_Key = FB_Users_Sellers.Event_Key
		AND (Visible = 1 OR Visible is null)
		AND FB_Users.Session_Key is not null
		--AND DATEADD(dd,+1,DATEADD(hh,Events.Timezone,Events.Event_Ends)) > getdate()
		ORDER By Amount_Sold desc
END' 
END
/****** Object:  StoredProcedure [dbo].[View_Resource_NetRevenue_Currency]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Resource_NetRevenue_Currency]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Resource_NetRevenue_Currency]
	@Resource_Key int
AS

BEGIN
	SELECT 
		Revenue_CAD = Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource_Key,''CAD'')),
		Revenue_USD = Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource_Key,''USD'')),
		Revenue_EUR = Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource_Key,''EUR'')),
		Revenue_GBP = Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource_Key,''GBP'')),
		Revenue_ILS = Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource_Key,''ILS''))
	FROM Resource
	WHERE Resource_Key = @Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Resource_Amount_Owed]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Resource_Amount_Owed]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Resource_Amount_Owed]
	@Resource_Key int
AS

BEGIN
	SELECT 
		Owed_CAD = SUM(Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''CAD''))) - SUM(Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''CAD''))),
		Owed_USD = SUM(Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''USD''))) - SUM(Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''USD''))),
		Owed_EUR = SUM(Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''EUR''))) - SUM(Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''EUR''))),
		Owed_GBP = SUM(Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''GBP''))) - SUM(Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''GBP''))),
		Owed_ILS = SUM(Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''ILS''))) - SUM(Convert(decimal(20,2),dbo.Event_Paid_Out_Currency(Event_Key,''ILS'')))
	FROM Events	
	WHERE Resource_Key = @Resource_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Referral_Amount_Owed]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Referral_Amount_Owed]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Referral_Amount_Owed]
	@FBid bigint
AS

BEGIN
DECLARE @referral_rate money
SET @referral_rate = (SELECT Referral_Rate FROM FB_Users WHERE FBid = @FBid)
IF @referral_rate is null
	BEGIN
		SET @referral_rate = .5
	END

	SELECT 
		Owed_CAD = (SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource.Resource_Key,''CAD''))) * @referral_rate) - SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource.Resource_Key,''CAD''))),
		Owed_USD = (SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource.Resource_Key,''USD''))) * @referral_rate) - SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource.Resource_Key,''USD''))),
		Owed_EUR = (SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource.Resource_Key,''EUR''))) * @referral_rate) - SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource.Resource_Key,''EUR''))),
		Owed_GBP = (SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource.Resource_Key,''GBP''))) * @referral_rate) - SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource.Resource_Key,''GBP''))),
		Owed_ILS = (SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Currency(Resource.Resource_Key,''ILS''))) * @referral_rate) - SUM(Convert(decimal(20,2),dbo.Resource_NetRevenue_Paidout_Currency(Resource.Resource_Key,''ILS'')))
	FROM FB_Users
	INNER JOIN Resource
	ON FB_Users.FBid = Resource.FBCreator
	WHERE FB_Users.Referral = @fbid
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_List_Store_Sellers_Previous]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_Store_Sellers_Previous]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_List_Store_Sellers_Previous]
	@fbid bigint
AS
BEGIN
	SELECT
		DISTINCT (FB_Users_Sellers.Event_Key),
		Event_Name, 		
		Event_Begins,		
		Revenue = Convert(decimal(20,2),dbo.Event_Revenue(Events.Event_Key)),
		Tickets_Sold = dbo.Event_TicketsSold_Progress(Events.Event_Key),
		Tickets_Capacity = dbo.Event_TicketsSold_Capacity(Events.Event_Key),
		Finish_Selling = dbo.Event_Finish_Selling(Events.Event_Key)
		
	FROM FB_Users_Sellers
	INNER JOIN Events
	ON Events.Event_Key = FB_Users_Sellers.Event_Key
	WHERE
		FBid = @fbid
		AND (Visible = 1 OR Visible is null)
		AND dbo.Event_Finish_Selling(Events.Event_Key) < getdate()	
END' 
END
/****** Object:  StoredProcedure [dbo].[View_List_Store_Sellers_Current]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_Store_Sellers_Current]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_List_Store_Sellers_Current]
	@fbid bigint
AS
BEGIN
	SELECT
		DISTINCT (FB_Users_Sellers.Event_Key),
		Event_Name, 		
		Event_Begins,		
		Revenue = Convert(decimal(20,2),dbo.Event_Revenue(Events.Event_Key)),
		Tickets_Sold = dbo.Event_TicketsSold_Progress(Events.Event_Key),
		Tickets_Capacity = dbo.Event_TicketsSold_Capacity(Events.Event_Key),
		Finish_Selling = dbo.Event_Finish_Selling(Events.Event_Key)
		
	FROM FB_Users_Sellers
	INNER JOIN Events
	ON Events.Event_Key = FB_Users_Sellers.Event_Key
	WHERE
		FBid = @fbid
		AND (Visible = 1 OR Visible is null)
		AND dbo.Event_Finish_Selling(Events.Event_Key) > getdate()
END' 
END
/****** Object:  StoredProcedure [dbo].[View_List_Previous_Events]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_Previous_Events]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_List_Previous_Events]
	@Resource_Key int
AS

	SELECT Events.Event_Key,
		   Event_Name,
		   eid,
		   Revenue = Convert(decimal(20,2),dbo.Event_Revenue(Event_Key)),
		   Tickets_Sold = dbo.Event_TicketsSold_Progress(Event_Key),
		   Tickets_Capacity = dbo.Event_TicketsSold_Capacity(Event_Key),
		   Views = Case
					   WHEN Views is NULL THEN 0
					   ELSE Views
				   END
		   
	FROM Events	
	WHERE Resource_Key=@Resource_Key	
	AND dbo.Event_Finish_Selling(Event_Key) < getdate()	
	AND (Visible = 1 OR Visible is null)
	AND (Events.Event_Type is NULL OR Events.Event_Type = 0)



' 
END
/****** Object:  StoredProcedure [dbo].[View_List_All_Events_Profile]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_All_Events_Profile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_List_All_Events_Profile]
	@Resource_Key int
AS

SELECT Event_Key,Event_Name,eid,
		   Revenue = Convert(decimal(20,2),dbo.Event_Revenue(Event_Key)),
		   Tickets_Sold = dbo.Event_TicketsSold_Progress(Event_Key),
		   Tickets_Capacity = dbo.Event_TicketsSold_Capacity(Event_Key),
		   Finish_Selling = dbo.Event_Finish_Selling(Event_Key),		   
		   Views = Case
					   WHEN Views is NULL THEN 0
					   ELSE Views
				   END,
		   Event_Ends,
		   Paid_Out = Convert(decimal(20,2),dbo.Event_Paid_Out(Event_Key))		   
FROM Events
WHERE Resource_Key=@Resource_Key
--AND Begin_Selling <= getdate()
--AND dbo.Event_Finish_Selling(Event_Key) > getdate()
AND (Visible = 1 OR Visible is null)
' 
END
/****** Object:  StoredProcedure [dbo].[View_List_Current_Events_Promoting]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_Current_Events_Promoting]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
create PROCEDURE [dbo].[View_List_Current_Events_Promoting]
	@Resource_Key int
AS

SELECT Event_Key,Event_Name,eid,
		   Revenue = Convert(decimal(20,2),dbo.Event_Revenue(Event_Key)),
		   Tickets_Sold = dbo.Event_TicketsSold_Progress(Event_Key),
		   Tickets_Capacity = dbo.Event_TicketsSold_Capacity(Event_Key),
		   Finish_Selling = dbo.Event_Finish_Selling(Event_Key),
		   Views = Case
					   WHEN Views is NULL THEN 0
					   ELSE Views
				   END

FROM Events
WHERE Resource_Key=@Resource_Key
--AND Begin_Selling <= getdate()
AND dbo.Event_Finish_Selling(Event_Key) > getdate()
AND (Visible = 1 OR Visible is null)
AND Event_Type = 1
' 
END
/****** Object:  StoredProcedure [dbo].[View_List_Current_Events]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_List_Current_Events]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[View_List_Current_Events]
	@Resource_Key int
AS

SELECT Event_Key,Event_Name,eid,
		   Revenue = Convert(decimal(20,2),dbo.Event_Revenue(Event_Key)),
		   Tickets_Sold = dbo.Event_TicketsSold_Progress(Event_Key),
		   Tickets_Capacity = dbo.Event_TicketsSold_Capacity(Event_Key),
		   Finish_Selling = dbo.Event_Finish_Selling(Event_Key),
		   Views = Case
					   WHEN Views is NULL THEN 0
					   ELSE Views
				   END

FROM Events
WHERE Resource_Key=@Resource_Key
--AND Begin_Selling <= getdate()
AND dbo.Event_Finish_Selling(Event_Key) > getdate()
AND (Visible = 1 OR Visible is null)
AND (Events.Event_Type is NULL OR Events.Event_Type = 0)
' 
END
/****** Object:  StoredProcedure [dbo].[View_Event_Revenue_Currency]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Event_Revenue_Currency]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Event_Revenue_Currency]
	@Event_Key int
AS

BEGIN
	SELECT 
		Revenue_CAD = Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''CAD'')),
		Revenue_USD = Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''USD'')),
		Revenue_EUR = Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''EUR'')),
		Revenue_GBP = Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''GBP'')),
		Revenue_ILS = Convert(decimal(20,2),dbo.Event_Revenue_Currency(Event_Key,''ILS''))
	FROM Events	
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Attendee_List_Summary_Sellers]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Attendee_List_Summary_Sellers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[View_Attendee_List_Summary_Sellers]
	@Event_Key int,
	@fbid bigint
AS

BEGIN
	SELECT 
		Event_Name,
		Tickets_Sold = dbo.Event_TicketsSold_Progress_Sellers(Event_Key,@fbid),
		Revenue = Convert(decimal(20,2),dbo.Event_Revenue_Sellers(Event_Key,@fbid)),
		FullAmount = Convert(decimal(20,2),dbo.Event_Revenue_Sellers_FullAmount(Event_Key,@fbid))
	FROM Events
	INNER JOIN Resource
	ON Events.Resource_Key = Resource.Resource_Key	
	WHERE Event_Key = @Event_Key
END
' 
END
/****** Object:  StoredProcedure [dbo].[View_Attendee_List_Summary]    Script Date: 03/31/2011 18:02:05 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_Attendee_List_Summary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[View_Attendee_List_Summary]
	@Event_Key int
AS

BEGIN
	SELECT 
		Event_Name,
		Tickets_Sold = dbo.Event_TicketsSold_Progress(Event_Key),
		Revenue = Convert(decimal(20,2),dbo.Event_Revenue(Event_Key))
	FROM Events
	INNER JOIN Resource
	ON Events.Resource_Key = Resource.Resource_Key	
	WHERE Event_Key = @Event_Key
END
' 
END
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__aspnet_Ap__Appli__182C9B23]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Applications] ADD  CONSTRAINT [DF__aspnet_Ap__Appli__182C9B23]  DEFAULT (newid()) FOR [ApplicationId]
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__Appli__56E8E7AB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Users]'))
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Us__Appli__56E8E7AB] FOREIGN KEY([ApplicationId])
REFERENCES [aspnet_Applications] ([ApplicationId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__Appli__56E8E7AB]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Users]'))
ALTER TABLE [dbo].[aspnet_Users] CHECK CONSTRAINT [FK__aspnet_Us__Appli__56E8E7AB]
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__aspnet_Us__UserI__3C69FB99]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Users] ADD  CONSTRAINT [DF__aspnet_Us__UserI__3C69FB99]  DEFAULT (newid()) FOR [UserId]
END

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__aspnet_Us__Mobil__3D5E1FD2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Users] ADD  CONSTRAINT [DF__aspnet_Us__Mobil__3D5E1FD2]  DEFAULT (NULL) FOR [MobileAlias]
END

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__aspnet_Us__IsAno__3E52440B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Users] ADD  CONSTRAINT [DF__aspnet_Us__IsAno__3E52440B]  DEFAULT ((0)) FOR [IsAnonymous]
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pa__Appli__51300E55]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Paths]'))
ALTER TABLE [dbo].[aspnet_Paths]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Pa__Appli__51300E55] FOREIGN KEY([ApplicationId])
REFERENCES [aspnet_Applications] ([ApplicationId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pa__Appli__51300E55]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Paths]'))
ALTER TABLE [dbo].[aspnet_Paths] CHECK CONSTRAINT [FK__aspnet_Pa__Appli__51300E55]
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__aspnet_Pa__PathI__412EB0B6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Paths] ADD  CONSTRAINT [DF__aspnet_Pa__PathI__412EB0B6]  DEFAULT (newid()) FOR [PathId]
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__55F4C372]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Ro__Appli__55F4C372] FOREIGN KEY([ApplicationId])
REFERENCES [aspnet_Applications] ([ApplicationId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__55F4C372]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles] CHECK CONSTRAINT [FK__aspnet_Ro__Appli__55F4C372]
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__aspnet_Ro__RoleI__440B1D61]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Roles] ADD  CONSTRAINT [DF__aspnet_Ro__RoleI__440B1D61]  DEFAULT (newid()) FOR [RoleId]
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Resource] FOREIGN KEY([FBCreator])
REFERENCES [FB_Users] ([FBid])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_Resource]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FB_Users_Pages_FB_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[FB_Users_Pages]'))
ALTER TABLE [dbo].[FB_Users_Pages]  WITH CHECK ADD  CONSTRAINT [FK_FB_Users_Pages_FB_Users] FOREIGN KEY([FB_Users])
REFERENCES [FB_Users] ([FBid])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FB_Users_Pages_FB_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[FB_Users_Pages]'))
ALTER TABLE [dbo].[FB_Users_Pages] CHECK CONSTRAINT [FK_FB_Users_Pages_FB_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Log_Activities_FB_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Log_Activities]'))
ALTER TABLE [dbo].[Log_Activities]  WITH CHECK ADD  CONSTRAINT [FK_Log_Activities_FB_Users] FOREIGN KEY([fbid])
REFERENCES [FB_Users] ([FBid])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Log_Activities_FB_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Log_Activities]'))
ALTER TABLE [dbo].[Log_Activities] CHECK CONSTRAINT [FK_Log_Activities_FB_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Log_Activities_Log_Activities_Possibilities]') AND parent_object_id = OBJECT_ID(N'[dbo].[Log_Activities]'))
ALTER TABLE [dbo].[Log_Activities]  WITH CHECK ADD  CONSTRAINT [FK_Log_Activities_Log_Activities_Possibilities] FOREIGN KEY([Activity])
REFERENCES [Log_Activities_Possibilities] ([Log_Activities_Possibilities_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Log_Activities_Log_Activities_Possibilities]') AND parent_object_id = OBJECT_ID(N'[dbo].[Log_Activities]'))
ALTER TABLE [dbo].[Log_Activities] CHECK CONSTRAINT [FK_Log_Activities_Log_Activities_Possibilities]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Log_Activities_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Log_Activities]'))
ALTER TABLE [dbo].[Log_Activities]  WITH CHECK ADD  CONSTRAINT [FK_Log_Activities_Resource] FOREIGN KEY([Resource_Key])
REFERENCES [Resource] ([Resource_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Log_Activities_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Log_Activities]'))
ALTER TABLE [dbo].[Log_Activities] CHECK CONSTRAINT [FK_Log_Activities_Resource]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FB_Users_Resource_FB_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[FB_Users_Resource]'))
ALTER TABLE [dbo].[FB_Users_Resource]  WITH CHECK ADD  CONSTRAINT [FK_FB_Users_Resource_FB_Users] FOREIGN KEY([FBid])
REFERENCES [FB_Users] ([FBid])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FB_Users_Resource_FB_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[FB_Users_Resource]'))
ALTER TABLE [dbo].[FB_Users_Resource] CHECK CONSTRAINT [FK_FB_Users_Resource_FB_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FB_Users_Resource_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[FB_Users_Resource]'))
ALTER TABLE [dbo].[FB_Users_Resource]  WITH CHECK ADD  CONSTRAINT [FK_FB_Users_Resource_Resource] FOREIGN KEY([Resource_Key])
REFERENCES [Resource] ([Resource_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FB_Users_Resource_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[FB_Users_Resource]'))
ALTER TABLE [dbo].[FB_Users_Resource] CHECK CONSTRAINT [FK_FB_Users_Resource_Resource]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Reading_Others_FB_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource_Reading_Others]'))
ALTER TABLE [dbo].[Resource_Reading_Others]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Reading_Others_FB_Users] FOREIGN KEY([FBid_Added])
REFERENCES [FB_Users] ([FBid])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Reading_Others_FB_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource_Reading_Others]'))
ALTER TABLE [dbo].[Resource_Reading_Others] CHECK CONSTRAINT [FK_Resource_Reading_Others_FB_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Reading_Others_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource_Reading_Others]'))
ALTER TABLE [dbo].[Resource_Reading_Others]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Reading_Others_Resource] FOREIGN KEY([Resource_Key])
REFERENCES [Resource] ([Resource_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Reading_Others_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource_Reading_Others]'))
ALTER TABLE [dbo].[Resource_Reading_Others] CHECK CONSTRAINT [FK_Resource_Reading_Others_Resource]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Reading_Others_Resource1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource_Reading_Others]'))
ALTER TABLE [dbo].[Resource_Reading_Others]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Reading_Others_Resource1] FOREIGN KEY([Resource_Key_Reading])
REFERENCES [Resource] ([Resource_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Reading_Others_Resource1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource_Reading_Others]'))
ALTER TABLE [dbo].[Resource_Reading_Others] CHECK CONSTRAINT [FK_Resource_Reading_Others_Resource1]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__531856C7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]'))
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Pe__PathI__531856C7] FOREIGN KEY([PathId])
REFERENCES [aspnet_Paths] ([PathId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__531856C7]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]'))
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] CHECK CONSTRAINT [FK__aspnet_Pe__PathI__531856C7]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pe__UserI__540C7B00]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]'))
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Pe__UserI__540C7B00] FOREIGN KEY([UserId])
REFERENCES [aspnet_Users] ([UserId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pe__UserI__540C7B00]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]'))
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] CHECK CONSTRAINT [FK__aspnet_Pe__UserI__540C7B00]
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__aspnet_Perso__Id__398D8EEE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] ADD  CONSTRAINT [DF__aspnet_Perso__Id__398D8EEE]  DEFAULT (newid()) FOR [Id]
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pr__UserI__55009F39]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Profile]'))
ALTER TABLE [dbo].[aspnet_Profile]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Pr__UserI__55009F39] FOREIGN KEY([UserId])
REFERENCES [aspnet_Users] ([UserId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pr__UserI__55009F39]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Profile]'))
ALTER TABLE [dbo].[aspnet_Profile] CHECK CONSTRAINT [FK__aspnet_Pr__UserI__55009F39]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4F47C5E3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Me__Appli__4F47C5E3] FOREIGN KEY([ApplicationId])
REFERENCES [aspnet_Applications] ([ApplicationId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__4F47C5E3]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership] CHECK CONSTRAINT [FK__aspnet_Me__Appli__4F47C5E3]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__UserI__503BEA1C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Me__UserI__503BEA1C] FOREIGN KEY([UserId])
REFERENCES [aspnet_Users] ([UserId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__UserI__503BEA1C]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership] CHECK CONSTRAINT [FK__aspnet_Me__UserI__503BEA1C]
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__aspnet_Me__Passw__46E78A0C]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[aspnet_Membership] ADD  CONSTRAINT [DF__aspnet_Me__Passw__46E78A0C]  DEFAULT ((0)) FOR [PasswordFormat]
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__5224328E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers]'))
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Pe__PathI__5224328E] FOREIGN KEY([PathId])
REFERENCES [aspnet_Paths] ([PathId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__5224328E]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers]'))
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers] CHECK CONSTRAINT [FK__aspnet_Pe__PathI__5224328E]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Resource] FOREIGN KEY([Resource_Key])
REFERENCES [Resource] ([Resource_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Resource]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__57DD0BE4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Us__RoleI__57DD0BE4] FOREIGN KEY([RoleId])
REFERENCES [aspnet_Roles] ([RoleId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__57DD0BE4]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles] CHECK CONSTRAINT [FK__aspnet_Us__RoleI__57DD0BE4]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__UserI__58D1301D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK__aspnet_Us__UserI__58D1301D] FOREIGN KEY([UserId])
REFERENCES [aspnet_Users] ([UserId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__UserI__58D1301D]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles] CHECK CONSTRAINT [FK__aspnet_Us__UserI__58D1301D]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tickets_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tickets]'))
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Event] FOREIGN KEY([Event_Key])
REFERENCES [Events] ([Event_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tickets_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tickets]'))
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Event]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Question_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[Question]'))
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Events] FOREIGN KEY([Event_Key])
REFERENCES [Events] ([Event_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Question_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[Question]'))
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Events]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transactions_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transactions]'))
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Events] FOREIGN KEY([Event_Key])
REFERENCES [Events] ([Event_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transactions_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transactions]'))
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Events]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attendee_List_Events1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attendee_List]'))
ALTER TABLE [dbo].[Attendee_List]  WITH CHECK ADD  CONSTRAINT [FK_Attendee_List_Events1] FOREIGN KEY([Event_Key])
REFERENCES [Events] ([Event_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attendee_List_Events1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attendee_List]'))
ALTER TABLE [dbo].[Attendee_List] CHECK CONSTRAINT [FK_Attendee_List_Events1]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attendee_List_Transaction]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attendee_List]'))
ALTER TABLE [dbo].[Attendee_List]  WITH CHECK ADD  CONSTRAINT [FK_Attendee_List_Transaction] FOREIGN KEY([Tx_Key])
REFERENCES [Transactions] ([Tx_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attendee_List_Transaction]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attendee_List]'))
ALTER TABLE [dbo].[Attendee_List] CHECK CONSTRAINT [FK_Attendee_List_Transaction]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tickets_Purchased_Tickets]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tickets_Purchased]'))
ALTER TABLE [dbo].[Tickets_Purchased]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Purchased_Tickets] FOREIGN KEY([Ticket_Key])
REFERENCES [Tickets] ([Ticket_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tickets_Purchased_Tickets]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tickets_Purchased]'))
ALTER TABLE [dbo].[Tickets_Purchased] CHECK CONSTRAINT [FK_Tickets_Purchased_Tickets]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tickets_Purchased_Transactions]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tickets_Purchased]'))
ALTER TABLE [dbo].[Tickets_Purchased]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Purchased_Transactions] FOREIGN KEY([Tx_Key])
REFERENCES [Transactions] ([Tx_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tickets_Purchased_Transactions]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tickets_Purchased]'))
ALTER TABLE [dbo].[Tickets_Purchased] CHECK CONSTRAINT [FK_Tickets_Purchased_Transactions]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Questions_Answered_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[Questions_Answered]'))
ALTER TABLE [dbo].[Questions_Answered]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Answered_Events] FOREIGN KEY([Event_Key])
REFERENCES [Events] ([Event_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Questions_Answered_Events]') AND parent_object_id = OBJECT_ID(N'[dbo].[Questions_Answered]'))
ALTER TABLE [dbo].[Questions_Answered] CHECK CONSTRAINT [FK_Questions_Answered_Events]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Questions_Answered_Question1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Questions_Answered]'))
ALTER TABLE [dbo].[Questions_Answered]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Answered_Question1] FOREIGN KEY([Tickets_Purchased_Key])
REFERENCES [Tickets_Purchased] ([Tickets_Purchased_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Questions_Answered_Question1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Questions_Answered]'))
ALTER TABLE [dbo].[Questions_Answered] CHECK CONSTRAINT [FK_Questions_Answered_Question1]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Questions_Answered_Questions_Answered]') AND parent_object_id = OBJECT_ID(N'[dbo].[Questions_Answered]'))
ALTER TABLE [dbo].[Questions_Answered]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Answered_Questions_Answered] FOREIGN KEY([Questions_Answered_key])
REFERENCES [Questions_Answered] ([Questions_Answered_key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Questions_Answered_Questions_Answered]') AND parent_object_id = OBJECT_ID(N'[dbo].[Questions_Answered]'))
ALTER TABLE [dbo].[Questions_Answered] CHECK CONSTRAINT [FK_Questions_Answered_Questions_Answered]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Questions_Answered_Transactions]') AND parent_object_id = OBJECT_ID(N'[dbo].[Questions_Answered]'))
ALTER TABLE [dbo].[Questions_Answered]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Answered_Transactions] FOREIGN KEY([Tx_Key])
REFERENCES [Transactions] ([Tx_Key])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Questions_Answered_Transactions]') AND parent_object_id = OBJECT_ID(N'[dbo].[Questions_Answered]'))
ALTER TABLE [dbo].[Questions_Answered] CHECK CONSTRAINT [FK_Questions_Answered_Transactions]


