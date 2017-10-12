IF EXISTS ( SELECT * FROM sys.types WHERE name = 'StringList' AND user_name(schema_id) = 'dbo')
	DROP TYPE StringList
GO

CREATE TYPE [dbo].[StringList] AS TABLE(
	[Item] [nvarchar](max) NULL,
	[ID] [int] NULL
)
GO
