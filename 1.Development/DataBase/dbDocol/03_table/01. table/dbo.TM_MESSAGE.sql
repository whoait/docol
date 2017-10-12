USE [dbDOC]
GO

/****** Object:  Table [dbo].[TM_MESSAGE]    Script Date: 2015/12/25 10:38:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TM_MESSAGE](
	[MessageId] [nvarchar](5) NOT NULL,
	[Class] [nvarchar](1) NULL,
	[Message] [nvarchar](200) NULL,
	[ButtonOK] [nvarchar](10) NULL,
	[ButtonNOK] [nvarchar](10) NULL,
	[ButtonCancel] [nvarchar](10) NULL,
	[DefaultButton] [nvarchar](10) NULL,
	[RegisterDate] [smalldatetime] NULL,
	[RegisterCd] [nvarchar](8) NULL,
	[UpdateDate] [smalldatetime] NULL,
	[UpdateByCd] [nvarchar](8) NULL,
	[DelDatetime] [smalldatetime] NULL,
	[DeletedByCd] [nvarchar](8) NULL,
	[DeletedFlg] [char](1) NULL,
	[Timestamp] [timestamp] NULL
) ON [dbDOC_DATA]

GO

SET ANSI_PADDING OFF
GO

