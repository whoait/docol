USE [dbDOC]
GO

/****** Object:  Table [dbo].[TT_DOC_CONTROL]    Script Date: 2015/12/25 10:43:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TT_DOC_CONTROL](
	[DOC_CONTROL_NO] [varchar](13) NOT NULL,
	[SHOHIN_TYPE] [char](3) NULL,
	[CHASSIS_NO] [varchar](25) NULL,
	[DOC_STATUS] [char](3) NULL,
	[SHIIRE_SHUPPINN_TOROKU_NO] [varchar](8) NULL,
	[URIAGE_SHUPPINN_TOROKU_NO] [varchar](8) NULL,
	[SHOP_CD] [char](6) NULL,
	[RAKUSATSU_SHOP_CD] [char](6) NULL,
	[SHIIRE_AA_KAIJO] [char](6) NULL,
	[URIAGE_AA_KAIJO] [char](6) NULL,
	[AA_KAISAI_KAISU] [int] NULL,
	[AA_KAISAI_DATE] [datetime] NULL,
	[AA_SHUPPIN_NO] [varchar](5) NULL,
	[DN_SEIYAKU_DATE] [datetime] NULL,
	[BBNO] [varchar](14) NULL,
	[SHIIRE_NO] [varchar](50) NULL,
	[NENSHIKI] [varchar](8) NULL,
	[MAKER_NAME] [varchar](50) NULL,
	[CAR_NAME] [varchar](100) NULL,
	[GRADE_NAME] [varchar](100) NULL,
	[KATASHIKI] [varchar](24) NULL,
	[CC] [real] NULL,
	[JOSHA_TEIIN_NUM] [int] NULL,
	[KEI_CAR_FLG] [char](1) NULL,
	[TOROKU_NO] [varchar](30) NULL,
	[SHAKEN_LIMIT_DATE] [datetime] NULL,
	[SHORUI_LIMIT_DATE] [datetime] NULL,
	[MEIHEN_SHAKEN_TOROKU_DATE] [datetime] NULL,
	[JISHAMEI_FLG] [char](1) NULL,
	[MASSHO_FLG] [char](1) NULL,
	[DOC_NYUKO_DATE] [datetime] NULL,
	[DOC_SHUKKO_DATE] [datetime] NULL,
	[JISHAMEI_IRAI_SHUKKO_DATE] [datetime] NULL,
	[JISHAMEI_KANRYO_NYUKO_DATE] [datetime] NULL,
	[MASSHO_IRAI_SHUKKO_DATE] [datetime] NULL,
	[MASSHO_KANRYO_NYUKO_DATE] [datetime] NULL,
	[RACK_NO] [char](1) NULL,
	[FILE_NO] [char](4) NULL,
	[MEMO] [varchar](500) NULL,
	[CAR_ID] [char](16) NULL,
	[CAR_SUB_ID] [char](3) NULL,
	[SHIIRE_CANSEL_FLG] [char](1) NULL,
	[URIAGE_CANSEL_FLG] [char](1) NULL,
	[CREATE_DATE] [datetime] NULL,
	[CREATE_USER_CD] [varchar](10) NULL,
	[CREATE_PG_CD] [varchar](10) NULL,
	[UPDATE_DATE] [datetime] NULL,
	[UPDATE_USER_CD] [varchar](10) NULL,
	[UPDATE_PG_CD] [varchar](10) NULL,
	[DELETE_DATE] [datetime] NULL,
	[DELETE_FLG] [char](1) NULL,
	[RowVersion] [timestamp] NULL,
	CONSTRAINT [PK_TT_DOC_CONTROL] PRIMARY KEY CLUSTERED 
(
	[DOC_CONTROL_NO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [dbDOC_DATA]
) ON [dbDOC_DATA]

GO

SET ANSI_PADDING OFF
GO

