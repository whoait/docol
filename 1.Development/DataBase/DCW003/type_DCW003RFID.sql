IF EXISTS ( SELECT * FROM sys.types WHERE name = 'DCW003RFID' AND user_name(schema_id) = 'dbo')
	DROP TYPE DCW003RFID
GO

CREATE TYPE [dbo].[DCW003RFID] AS TABLE(
	[ID]	int	NULL
	,[RFIDKey] [varchar](25) NULL
)
GO