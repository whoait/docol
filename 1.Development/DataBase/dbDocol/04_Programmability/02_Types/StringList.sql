USE [dbDOC]
GO

/****** Object:  UserDefinedTableType [dbo].[StringList]    Script Date: 2015/12/25 11:46:36 ******/
CREATE TYPE [dbo].[StringList] AS TABLE(
	[Item] [nvarchar](max) NULL,
	[ID] [int] NULL
)
GO

