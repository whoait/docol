IF EXISTS ( SELECT * FROM sys.types WHERE name = 'DCW003JishameiMassho' AND user_name(schema_id) = 'dbo') 
DROP TYPE DCW003JishameiMassho
GO

/****** Object:  UserDefinedTableType [dbo].[DCW003JishameiMassho]    Script Date: 12/19/2015 1:29:31 PM ******/
CREATE TYPE [dbo].[DCW003JishameiMassho] AS TABLE(
	[IraiDate] [varchar](10) NULL,
	[ShopCd] [varchar](6) NULL,
	[GenshaLocation] [varchar](30) NULL,
	[CarName] [varchar](50) NULL,
	[ChassisNo] [varchar](25) NULL,
	[Jisame_Masso_Type] [varchar](100) NULL,
	[Note] [varchar](500) NULL
)
GO


