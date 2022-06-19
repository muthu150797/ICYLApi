--
USE [ICYL]
GO
set identity_insert [LookupGroup] on
INSERT INTO [dbo].[LookupGroup]
           (GroupId,[GroupName]
           ,[GroupDescription]
           ,[CreatedBy]
           ,[CreatedOn]
)
     VALUES
           (8,'Downloaded Date'
           ,'Store Last Downloaded Date'
           ,'Navin Jolly'
           ,GetDate()
)
set identity_insert [LookupGroup] off

set identity_insert [LookupValue] on
INSERT INTO [dbo].[LookupValue]
           (Valueid,[GroupId]
           ,[Value]
           ,[ValueDescription]
           ,[Active]
           ,[CreatedBy]
           ,[CreatedOn]
)
     VALUES
           (11,8,GetDate(),'Date',1,'Navin Jolly',GetDate())
		   set identity_insert [LookupValue] off

GO


-- 04152021
alter Table [PaymentConfig] add  [FirstNameSecond] [varchar](50) NULL,	[LastNameSecond] [varchar](50)  NULL
set identity_insert LookupValue on
  INSERT INTO [dbo].[LookupValue]
           (Valueid,[GroupId]
           ,[Value]
           ,[ValueDescription]
           ,[Active]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[DisplayOrder])
select 10,2,'Other','Other',1,'Navin Jolly','2021-01-16 09:28:17.230','Navin Jolly','2021-01-16 09:28:17.230',50
set identity_insert LookupValue off

alter table [PaymentConfig] add CheckNumber varchar(30)
--03242021

USE [ICYL]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ICYLEmail](
	[EmailId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Subject] [nvarchar](100) NULL,
	[Body] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_ICYLEmail] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ICYLEmail] ON 
GO
INSERT [dbo].[ICYLEmail] ([EmailId], [Description], [Subject], [Body], [Active], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Donation Email', N'Email Receipt From Islamic Center of Yorba Linda', N'<style type="text/css">body {background-color: #1ea7d9; margin: 9px 9px padding:0; text-align:left;font-family: Arial,Tahoma,Verdana,sans-serif;  color: #666666;}.mailStyle{border:1px solid #999999; background-color: #ffffff;  width:760px;}a {color:#666666;text-decoration:none;} a:hover {color:#333333; text-decoration:underline;}li.raquo {display: list-itemlist-style-image:url(''raquo.gif''); margin: 5px 10px 0px -10px;} li.raquo a {color: #666666;}.title{padding: 5px 10px 0px 20px; text-align: left;font-size: 15px; font-weight: bold;  }.raquo{width:54px;}.header{background-color:#1ea7d9;}.headertitle{font-family:Georgia, ''Times New Roman'', Times, serif; font-size:34px;color: #000000;}.headersubtitle{font-family:Georgia, ''Times New Roman'', Times, serif; font-size:16px;color: #000000;}</style><div><div align="center"><table border="0" cellspacing="0" cellpadding="0" class="mailStyle"><tbody><tr class="header"> <td style="padding:6px;width:100px;">  <img src="http://donation.icyl.org/images/Logo/logo.png" alt="">  </td><td> <div class="headertitle">ICYL</div> <div class="headersubtitle">Masjid</div></td></tr><tr><td style="padding: 0px 20px 0px 5px" colspan="2">&nbsp;<table width="680" cellpadding="2" cellspacing="2" border="0" style="padding-top:5px"><tbody><tr><td class="title">Online Donation </td></tr><tr> <td style="height: 15px">&nbsp;</td> </tr><tr><td style="width: 680px">Dear&nbsp; _Name_,<br>﻿<p>Thank you for your contribution of&nbsp; _Amount_&nbsp; to Islamic Center of Yorba Linda. Your commitment is appreciated. Attached is your receipt. If you have questions or concerns regarding your payment, please contact Islamic Center of Yorba Linda at finance@icyl.org or 714-983-7464 </p><br><p>PLEASE DO NOT REPLY TO THIS EMAIL</p><p>&nbsp;</p></td></tr><tr> <td style="height: 15px">&nbsp;</td> </tr><tr> <td style="height: 15px">&nbsp;</td> </tr><tr><td class="title" style="padding: 0px 10px 0px 20px text-align: left"><a href="http://icyl.org/">http://icyl.org</a></td></tr><tr> <td> &nbsp; </td></tr> <tr><td style="text-align: center height: 15px"><div style="font-size:0pxline-height:1px width:680px height:1px background-color:#CCCCCC"></div></td></tr><tr><td style="height: 20px">Sincerely, <br>ICYL</td></tr><tr><td style="height: 20px">&nbsp;</td></tr></tbody></table></td></tr></tbody></table><table border="0" style="width: 720px" cellspacing="0" cellpadding="0" id="table4"><tbody><tr></tr></tbody></table></div>            </div>
            <div class="clearfix"></div>', 1, CAST(N'2021-03-20T00:00:00.000' AS DateTime), N'Navin Jolly', CAST(N'2021-03-25T13:46:20.747' AS DateTime), N'Icyladmin')
GO
SET IDENTITY_INSERT [dbo].[ICYLEmail] OFF
GO
ALTER TABLE [dbo].[ICYLEmail] ADD  CONSTRAINT [DF_ICYLEmail_Active]  DEFAULT ((1)) FOR [Active]
GO


--03232021
set identity_insert LookupValue on

INSERT INTO [dbo].[LookupValue]
           (Valueid,[GroupId]
           ,[Value]
           ,[ValueDescription]
           ,[Active]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[DisplayOrder])
select 9,2,'Cash','Cash',1,'Navin Jolly','2021-01-16 09:28:17.230','Navin Jolly','2021-01-16 09:28:17.230',30

INSERT INTO [dbo].[LookupValue]
           (Valueid,[GroupId]
           ,[Value]
           ,[ValueDescription]
           ,[Active]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[DisplayOrder])
select 8,2,'Check','Check',1,'Navin Jolly','2021-01-16 09:28:17.230','Navin Jolly','2021-01-16 09:28:17.230',40
set identity_insert LookupValue off



--03/15/2021

USE [icyl]
GO
set identity_insert  LookupValue on
INSERT INTO [dbo].[LookupValue]
           (ValueId,[GroupId]
           ,[Value]
           ,[ValueDescription]
           ,[Active]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[DisplayOrder]
           ,[APIId]
           ,[APIKey])
     VALUES
           (7,1,'Landscape/Tree Project','Landscape/Tree Project',1,'Navin Jolly',GetDate(),'Navin Jolly',GetDate(),12,'ZBV2riLZdVnjNviCXSrDNw==','1iOLhJXzl256hPqT4NqOfibfiHaWZiMp')

set identity_insert  LookupValue off
GO

--03/10/2021

alter Table LookupValue add APIId  nvarchar(200) , APIKey  nvarchar(200)
UPDATE [dbo].[LookupValue]  SET [APIId] = 'ZBV2riLZdVnjNviCXSrDNw==',  [APIKey] = '1iOLhJXzl256hPqT4NqOfibfiHaWZiMp'   WHERE valueid =1 --Building
UPDATE [dbo].[LookupValue]  SET [APIId] = '0hUcPwiVCeFp3Esllx1a7Q==',  [APIKey] = 'mP01dRFeL+T4Znzwp81DkQ60fEJTymUU'   WHERE valueid =2 --General Fund
UPDATE [dbo].[LookupValue]  SET [APIId] = 'Najp+++41dAMyLBErHJ6TQ==',  [APIKey] = 'bv0oMVGMPWwJ+BprXDvPsc/yHaG/GjN8'   WHERE valueid =3 --Sadaqa
UPDATE [dbo].[LookupValue]  SET [APIId] = 'bFYbiE40dKm/To0nPpPbnw==',  [APIKey] = 'jvnYFZEscSaLXGCd1aPhUkY6lRGYp+o3'   WHERE valueid =4 --Zakat
UPDATE [dbo].[LookupValue]  SET [APIId] = '8sWg1TUnFU9H2PZPksCPOg==',  [APIKey] = 'Yq8A2+FOy4LQH51+ZwUoRqA5yUCOA04X'   WHERE valueid =277 --Sunday School


--03/09/2021
ALTER TABLE PaymentConfig
    ADD RecurringInterval varchar(20)  null

ALTER TABLE PaymentConfig
    ADD isDownloaded bit  
    CONSTRAINT DT_PaymentConfig_isDownloaded DEFAULT 0
    WITH VALUES;

set identity_insert [LookupGroup] on
INSERT INTO [dbo].[LookupGroup]
           (GroupId, [GroupName]
           ,[GroupDescription]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
select 3,'RecurringType','RecurringType','Navin Jolly',getDate(),'Navin Jolly',GetDate()
set identity_insert [LookupGroup] off



--03/08/2021

  set identity_insert LookupValue on
  --delete from  LookupValue  where GroupId=2 and ValueId=1
  --delete from LookupValue  where GroupId=2 and ValueId=2

  USE [ICYL]
GO

INSERT INTO [dbo].[LookupValue]
           (Valueid,[GroupId]
           ,[Value]
           ,[ValueDescription]
           ,[Active]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[DisplayOrder])
select 5,2,'Credit Card','Credit Card',1,'Navin Jolly','2021-01-16 09:28:17.230','Navin Jolly','2021-01-16 09:28:17.230',10

INSERT INTO [dbo].[LookupValue]
           (Valueid,[GroupId]
           ,[Value]
           ,[ValueDescription]
           ,[Active]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[DisplayOrder])
select 6,2,'eCheck','eCheck',1,'Navin Jolly','2021-01-16 09:28:17.230','Navin Jolly','2021-01-16 09:28:17.230',20

set identity_insert LookupValue off


ALTER TABLE LookupValue
ADD CONSTRAINT UC_LookupValue UNIQUE (ValueId);


--03/02/2021
USE [icyl]
GO
/****** Object:  StoredProcedure [dbo].[upsICYLErrorLog]    Script Date: 3/3/2021 10:48:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************
exec ICYLReceipt
*********************************************/
create Procedure [dbo].[ICYLReceipt]
as 
Begin

        select '' MemberId,''EmailId,'' MemberName,'' CardHolderName, CAST(0 AS BIT) IsRecurringTransaction , '' RecurringTransaction,'' Freequency,''LastFour,
        ''PaymentType ,''TransactionType,''Category,'' Amount,''ApprovalCode,''ConfirmationNumber
End



--02/28/2021
	alter table [dbo].[PaymentConfig] add dtPaymentStart  [datetime] NULL,dtPaymentEnd  [datetime] NULL, PaymentEndType int,PaymentMaxOccurences int

--02/22/2021
ALTER TABLE dbo.[LookupValue] 
ADD CONSTRAINT PK_LookupValue PRIMARY KEY NONCLUSTERED (ValueId);


ALTER TABLE dbo.[LookupGroup] 
ADD CONSTRAINT PK_LookupGroup PRIMARY KEY NONCLUSTERED (GroupId)


--02142021
USE [ICYL]
GO
/****** Object:  StoredProcedure [dbo].[upsCPSErrorLog]    Script Date: 2/14/2021 10:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************
exec upsICYLErrorLog
*********************************************/
create Procedure [dbo].[upsICYLErrorLog]
(
	@ControllerName nvarchar(200)=NULL,
	@ActionName nvarchar(200) = NULL,
	@ErrorMessage nvarchar(max) = NULL,
	@StackTrace  nvarchar(max) = NULL,
	@CreatedBy nvarchar(100) = NULL,
	@ErrorId nvarchar(100) output 
)
as 
Begin
DECLARE @Date DateTime = GetDate()

INSERT INTO [dbo].[ICYLErrorLog]
           ([ControllerName]
           ,[ActionName]
           ,[ErrorMessage]
           ,[ErrorTrace]
           ,[CreatedDate]
           ,[CreatedBy])
     VALUES
           (@ControllerName
           ,@ActionName
           ,@ErrorMessage
           ,@StackTrace
		   ,@Date
		   ,@CreatedBy
		   )
set @ErrorId= 'ERR0XICYL'+ CAST(SCOPE_IDENTITY() as nvarchar(100))  
End


--Table
USE [ICYL]
GO

/****** Object:  Table [dbo].[ICYLErrorLog]    Script Date: 2/14/2021 11:49:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ICYLErrorLog](
	[ErrorId] [bigint] IDENTITY(1,1) NOT NULL,
	[ControllerName] [nvarchar](200) NULL,
	[ActionName] [nvarchar](200) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[ErrorTrace] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
 CONSTRAINT [PK_ICYLErrorLog] PRIMARY KEY CLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO






--02132021
alter table PaymentTransaction Add CustomerProfileId varchar(50)
alter table PaymentTransaction Add CustomerAddressId varchar(50)

alter table PaymentConfig Add CustomerProfileId varchar(50)
alter table PaymentConfig Add CustomerAddressId varchar(50)

use icyl
select * From PaymentBatch
select * From PaymentConfig
select * From PaymentTransaction order by 1 desc


--02062021
Url : https://sandbox.authorize.net/
Login Id: kodemasters19
Password: K0d3m@st3er


--01262021
Alter table PaymentConfig 
	Add MailingAddressLine1 varchar(50),
	MailingAddressLine2 varchar(50),
	MailingCity varchar(100),
	MailingState varchar(50),
	MailingZip varchar(10),
	MailingCountry varchar(50) 

--01192021
use ICYL
Alter table PaymentConfig
	Add SubscriptionTransId varchar(100)
	, SubscriptionResponseCode varchar(100)
	, SubscriptionResponseText varchar(250)

--01172021
Create table PaymentBatch
(
	BatchId int Primary Key,
	settlementTimeUTC dateTime,
	settlementTimeLocal DateTime,
	settlementState varchar(50),
	marketType varchar(50),
	product varchar(50)
)

Alter table PaymentTransaction Add BatchId int


--01152021
alter Table [PaymentConfig] add PaymentType int null
set identity_insert LookupGroup on
INSERT INTO [dbo].[LookupGroup]
           (GroupId,[GroupName]
           ,[GroupDescription]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           (2,'PaymentType'
           ,'Payment Type'
           ,'Navin Jolly'
           ,GetDate()
           ,'Navin Jolly'
           ,GetDate())
set identity_insert LookupGroup off
GO


set identity_insert LookupValue on
INSERT INTO [dbo].[LookupValue]
           (ValueId,[GroupId]
           ,[Value]
           ,[ValueDescription]
           ,[Active]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[DisplayOrder])
		   select 1,2,'CreditCard','Credit Card',1,'Navin Jolly',getDate(),'Navin Jolly',GETDATE(),10
		   UNION
		   select 2,2,'eCheck','eCheck',1,'Navin Jolly',getDate(),'Navin Jolly',GETDATE(),20

set identity_insert LookupValue off
GO


--01142021
USE [ICYL] 
CREATE TABLE [dbo].[PaymentTransaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentConfigId] [int] NOT NULL,
	[AmtTransaction] [money] NOT NULL,
	[TransId] [varchar](100) NULL,
	[TransResponseCode] [varchar](100) NULL,
	[TransMessageCode] [varchar](100) NULL,
	[TransDescription] [varchar](250) NULL,
	[TransAuthCode] [varchar](100) NULL,
	[TransErrorCode] [varchar](100) NULL,
	[TransErrorText] [varchar](250) NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_PaymentTransaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 




USE [ICYL]
GO

set identity_insert LookupGroup on
INSERT INTO [dbo].[LookupGroup]
           (GroupId,[GroupName]
           ,[GroupDescription]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           (1,'DonationCategory'
           ,'Donation Category'
           ,'Navin Jolly'
           ,GetDate()
           ,'Navin Jolly'
           ,GetDate())
set identity_insert LookupGroup off
GO


set identity_insert LookupValue on
INSERT INTO [dbo].[LookupValue]
           (ValueId,[GroupId]
           ,[Value]
           ,[ValueDescription]
           ,[Active]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[DisplayOrder])
		   select 1,1,'Zakkath','Zakkath',1,'Navin Jolly',getDate(),'Navin Jolly',GETDATE(),10
		   UNION
		   select 2,1,'Sadakka','Sadakka',1,'Navin Jolly',getDate(),'Navin Jolly',GETDATE(),20
		   UNION
		   select 3,1,'Kids Program','Kids Program',1,'Navin Jolly',getDate(),'Navin Jolly',GETDATE(),30
		   UNION
		   select 4,1,'Test Campaign','Test Campaign',1,'Navin Jolly',getDate(),'Navin Jolly',GETDATE(),40

set identity_insert LookupValue off
GO

--02/19/2021
USE [ICYL]
GO

/****** Object:  Table [dbo].[ICYLErrorLog]    Script Date: 2/19/2021 5:18:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ICYLErrorLog](
	[ErrorId] [bigint] IDENTITY(1,1) NOT NULL,
	[ControllerName] [nvarchar](200) NULL,
	[ActionName] [nvarchar](200) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[ErrorTrace] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
 CONSTRAINT [PK_ICYLErrorLog] PRIMARY KEY CLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [ICYL]
GO
/****** Object:  StoredProcedure [dbo].[upsICYLErrorLog]    Script Date: 2/19/2021 5:17:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************
exec upsICYLErrorLog
*********************************************/
ALTER Procedure [dbo].[upsICYLErrorLog]
(
	@ControllerName nvarchar(200)=NULL,
	@ActionName nvarchar(200) = NULL,
	@ErrorMessage nvarchar(max) = NULL,
	@StackTrace  nvarchar(max) = NULL,
	@CreatedBy nvarchar(100) = NULL,
	@ErrorId nvarchar(100) output 
)
as 
Begin
DECLARE @Date DateTime = GetDate()

INSERT INTO [dbo].[ICYLErrorLog]
           ([ControllerName]
           ,[ActionName]
           ,[ErrorMessage]
           ,[ErrorTrace]
           ,[CreatedDate]
           ,[CreatedBy])
     VALUES
           (@ControllerName
           ,@ActionName
           ,@ErrorMessage
           ,@StackTrace
		   ,@Date
		   ,@CreatedBy
		   )
set @ErrorId= 'ERR0XICYL'+ CAST(SCOPE_IDENTITY() as nvarchar(100))  
End

