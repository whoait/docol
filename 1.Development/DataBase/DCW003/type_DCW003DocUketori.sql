IF EXISTS ( SELECT * FROM sys.types WHERE name = 'DCW003DocUketoriUpdate' AND user_name(schema_id) = 'dbo')
	DROP TYPE DCW003DocUketoriUpdate
GO

CREATE TYPE [dbo].[DCW003DocUketoriUpdate] AS TABLE(

	[DocControlNo]				[varchar](13) NULL,
	[ShakenLimitDate]			[datetime] NULL,
	[ShoruiLimitDate]			[datetime] NULL,
	[MeihenShakenTorokuDate]	[datetime] NULL
)
GO