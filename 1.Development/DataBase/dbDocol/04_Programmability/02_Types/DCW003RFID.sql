USE [dbDOC]
GO

/****** Object:  UserDefinedTableType [dbo].[DCW003RFID]    Script Date: 2015/12/25 11:45:46 ******/
CREATE TYPE [dbo].[DCW003RFID] AS TABLE(
	[ID] [int] NULL,
	[RFIDKey] [varchar](25) NULL
)
GO

