IF EXISTS ( SELECT * FROM sys.types WHERE name = 'DCW003ListUpdate' AND user_name(schema_id) = 'dbo')
	DROP TYPE DCW003ListUpdate
GO

CREATE TYPE [dbo].[DCW003ListUpdate] AS TABLE(
	[DocControlNo]				[varchar](13) NULL,
	[UriageShuppinTorokuNo]		[varchar](8) NULL,
	[ChassisNo]					[varchar](25) NULL,
	[RackNo]					[char](1) NULL,
	[FileNo]					[char](4) NULL,
	[MasshoFlg]					[char](1) NULL,
	[JishameiFlg]				[char](1) NULL,
	[DocStatus]					[char](3) NULL,
	[DocNyukoDate]				[datetime] NULL,
	[DocShukkoDate]				[datetime] NULL,
	[JishameiIraiShukkoDate]	[datetime] NULL,
	[JishameiIraiNyukoDate]		[datetime] NULL,
	[MasshoIraiShukkoDate]		[datetime] NULL,
	[MasshoIraiNyukoDate]		[datetime] NULL,
	[Memo]						[varchar](500) NULL
)
GO