USE [dbDOC]
GO

/****** Object:  UserDefinedTableType [dbo].[DCW003Csv]    Script Date: 2015/12/25 11:43:48 ******/
CREATE TYPE [dbo].[DCW003Csv] AS TABLE(
	[ID] [int] NULL,
	[RacFileNo] [varchar](5) NULL,
	[KeiCarFlg] [char](1) NULL,
	[TorokuNo] [varchar](24) NULL,
	[HyobanType] [char](1) NULL,
	[ChassisNo] [varchar](25) NULL,
	[GendokiKatashiki] [varchar](12) NULL,
	[ReportType] [char](1) NULL,
	[DocControlNo] [varchar](13) NULL,
	[IraiDate] [datetime] NULL,
	[ShopCd] [char](6) NULL,
	[GenshaLocation] [varchar](25) NULL,
	[CarName] [varchar](100) NULL,
	[JMType] [char](3) NULL,
	[Note] [varchar](1000) NULL
)
GO

