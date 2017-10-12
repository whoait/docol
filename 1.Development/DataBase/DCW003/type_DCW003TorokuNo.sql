IF EXISTS ( SELECT * FROM sys.types WHERE name = 'DCW003TorokuNo' AND user_name(schema_id) = 'dbo')
	DROP TYPE DCW003TorokuNo
GO

CREATE TYPE [dbo].[DCW003TorokuNo] AS TABLE(
	[TorokuNo]	[varchar](8) NULL
)
GO