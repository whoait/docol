USE [dbDOC]
GO

/****** Object:  UserDefinedTableType [dbo].[DCW003DocUketoriDetail]    Script Date: 2015/12/25 11:44:57 ******/
CREATE TYPE [dbo].[DCW003DocUketoriDetail] AS TABLE(
	[DocControlNo] [varchar](13) NULL,
	[DocFuzokuhinCd] [char](3) NULL,
	[DocCount] [int] NULL,
	[Note] [varchar](200) NULL,
	[IsChecked] [int] NULL
)
GO

