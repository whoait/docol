USE [dbDOC]
GO

/****** Object:  Table [dbo].[TM_SHOP]    Script Date: 2015/12/25 10:41:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TM_SHOP](
	[TEMPO_CD] [char](6) NOT NULL,
	[TEMPO_NAME] [varchar](60) NULL,
	[TEMPO_NAME_KANA] [varchar](60) NULL,
	[TEMPO_KBN_CD] [char](1) NULL,
	[STATUS_CD] [nvarchar](2) NULL,
	[STATUS_NAME] [nvarchar](30) NULL,
	[PASSWORD] [varchar](20) NULL,
	[IP_ADDRESS] [varchar](15) NULL,
	[OPEN_DATE] [char](10) NULL,
	[ZIP_CODE] [varchar](8) NULL,
	[JUSHO_1] [nvarchar](100) NULL,
	[JUSHO_2] [nvarchar](100) NULL,
	[TEL_NO] [varchar](13) NULL,
	[FAX_NO] [varchar](13) NULL,
	[FREE_DIAL] [varchar](13) NULL,
	[MAIL_ADDRESS] [varchar](100) NULL,
	[KEN_CD] [char](2) NULL,
	[KEN_NAME] [nvarchar](10) NULL,
	[SHICHOUSON_NAME] [nvarchar](50) NULL,
	[CHIKU_CD] [nvarchar](1) NULL,
	[CHIKU_NAME] [nvarchar](10) NULL,
	[BLOCK_CD] [int] NULL,
	[BLOCK_NAME] [varchar](40) NULL,
	[OPEN_TIME] [nvarchar](6) NULL,
	[CLOSE_TIME] [nvarchar](6) NULL,
	[LOCATION] [varchar](50) NULL,
	[OWNER_NO] [char](6) NULL,
	[JNET_TEL_NO] [nvarchar](10) NULL,
	[JNET_FAX_NO] [nvarchar](10) NULL,
	[HIKITSUGI_TEMPO_CD] [char](6) NULL,
	[HONBU_FLG] [int] NULL,
	[CLOSE_DATE] [datetime] NULL,
	[TEMPO_NAME_UPDATE_DATE] [datetime] NULL,
	[JUSHO_UPDATE_DATE] [datetime] NULL,
	[SAP_BUMON_CD] [varchar](6) NULL,
	[BIKO] [nvarchar](500) NULL,
	[CREATE_DATE] [datetime] NOT NULL,
	[CREATE_USER_CD] [varchar](10) NOT NULL,
	[CREATE_PG_CD] [varchar](10) NOT NULL,
	[UPDATE_DATE] [datetime] NULL,
	[UPDATE_USER_CD] [varchar](10) NULL,
	[UPDATE_PG_CD] [varchar](10) NULL,
	[DELETE_DATE] [datetime] NULL,
	[DELETE_FLG] [char](1) NULL,
 CONSTRAINT [PK_TM_SHOP] PRIMARY KEY CLUSTERED 
(
	[TEMPO_CD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [dbDOC_DATA]
) ON [dbDOC_DATA]

GO

SET ANSI_PADDING OFF
GO

