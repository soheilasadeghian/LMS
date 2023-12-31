USE [lmspricingDB]
GO
/****** Object:  Table [dbo].[agentTbl]    Script Date: 9/1/2023 6:00:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[agentTbl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[company] [nvarchar](250) NOT NULL,
	[fullName] [nvarchar](250) NOT NULL,
	[mobile] [nvarchar](50) NOT NULL,
	[email] [nvarchar](350) NOT NULL,
	[address] [ntext] NOT NULL,
	[description] [ntext] NOT NULL,
	[regDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[factorTbl]    Script Date: 9/1/2023 6:00:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[factorTbl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[price] [int] NOT NULL,
	[packetTitle] [nvarchar](50) NOT NULL,
	[hostDomain] [nvarchar](150) NOT NULL,
	[fullName] [nvarchar](250) NOT NULL,
	[mobile] [nvarchar](50) NOT NULL,
	[email] [nvarchar](350) NOT NULL,
	[description] [ntext] NOT NULL,
	[Authority] [nvarchar](150) NOT NULL,
	[RefID] [nvarchar](350) NULL,
	[regDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[packetUploadServiceTbl]    Script Date: 9/1/2023 6:00:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[packetUploadServiceTbl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[token] [nvarchar](50) NOT NULL,
	[data] [ntext] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tokenUploadServiceTbl]    Script Date: 9/1/2023 6:00:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tokenUploadServiceTbl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[token] [nvarchar](50) NOT NULL,
	[packetCount] [int] NOT NULL,
	[receivedPacket] [int] NOT NULL,
	[format] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userReserveTbl]    Script Date: 9/1/2023 6:00:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userReserveTbl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[courseID] [int] NOT NULL,
	[userID] [bigint] NOT NULL,
	[regDate] [datetime] NOT NULL,
	[isRead] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userTbl]    Script Date: 9/1/2023 6:00:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userTbl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[fullName] [nvarchar](150) NOT NULL,
	[mobile] [nvarchar](50) NOT NULL,
	[image] [nvarchar](250) NOT NULL,
	[regDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
