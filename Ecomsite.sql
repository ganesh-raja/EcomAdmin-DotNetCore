CREATE TABLE [dbo].[adminlogin](
	[id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[uname] [varchar](50) NOT NULL,
	[pass] [varchar](50) NOT NULL,
	[access] [bit] NOT NULL,
	[createddate] [datetime] NULL,	
)

CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [varchar](100) NULL,
	[ImageUrl] [varchar](200) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
)

CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](max) NULL,
	[Price] [int] NULL,
	[Quantity] [int] NULL,
	[ImageUrl] [varchar](200) NULL,
	[CategoryId] [int] NULL,	
	[CreatedDate] [datetime] NULL,
)

CREATE TABLE [dbo].[Carts](
	[CartId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[OrderNo] [varchar](12) NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
	[UserId] [int] NULL,
)

CREATE TABLE [dbo].[Contact](
	[ContactId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[Subject] [varchar](200) NULL,
	[Message] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
 )

 CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[OrderNo] [varchar](15) NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
	[Status] [varchar](50) NULL,
	[UserId] [int] NULL,
	[PaymentId] [int] NULL,
	[OrderDate] [datetime] NULL,
)

CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) PRIMARY KEY  NOT NULL,
	[Name] [varchar](100) NULL,
	[Mobile] [varchar](12) NULL,
	[Email] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[City] [varchar](20) NULL,
	[State] [varchar](20) NULL,
	[Zip] [varchar](10) NULL,
	[CardNo] [varchar](12) NULL,
	[PaymentMode] [varchar](10) NULL,
)

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) PRIMARY KEY  NOT NULL,
	[Username] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[Mobile] [varchar](12) NULL,
	[Email] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[City] [varchar](20) NULL,
	[State] [varchar](20) NULL,
	[Zip] [varchar](10) NULL,
	[CreatedDate] [datetime] NULL
)

select * from [dbo].[adminlogin]
select * from [dbo].[Categories]
select * from [dbo].[Products]
select * from [dbo].[Carts]
select * from [dbo].[Contact]
select * from [dbo].[Orders]
select * from [dbo].[Payment]
select * from [dbo].[Users]