IF EXISTS ( SELECT * FROM sys.types WHERE name = 'DCW003DocControlNo' AND user_name(schema_id) = 'dbo')
	DROP TYPE DCW003DocControlNo
GO

CREATE TYPE [dbo].[DCW003DocControlNo] AS TABLE(
	[DocControlNo]	[varchar](13) NULL
)
GO