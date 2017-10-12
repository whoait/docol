USE [dbDOC]
GO

/****** Object:  StoredProcedure [dbo].[stp_DCW001UserLogin]    Script Date: 2015/12/25 11:50:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROC [dbo].[stp_DCW001UserLogin]
(
	@userName	NVARCHAR(40),			-- UserName
	@password	VARCHAR(20)				-- Password
)
AS
---------------------------------------------------------------------------
-- Version			: 1.0
-- Designer			: Dhanya.Ratheesh
-- Programmer		: Dhanya.Ratheesh
-- Created Date		: 2015/02/15
-- Comment			: Create new
---------------------------------------------------------------------------
BEGIN
	SET NOCOUNT ON

	-------------------------------------------------------------------------
	-- Declare constants
	-------------------------------------------------------------------------
	DECLARE @ct_Check_Souce_Flg			SMALLINT		=	1
			, @ct_Current_Date			DATETIME		=	CONVERT(DATE,GETDATE())

	-------------------------------------------------------------------------
	-- Get user
	-------------------------------------------------------------------------
	
	
SELECT
	  [TANTOSHA_CD]
	 ,[TANTOSHA_NAME]
	 ,[PASSWORD]
	 ,[RACK_SEACH_KANO_FLG]
	 ,[EXIST_FLG]
FROM [dbo].[TM_TANTOSHA] WITH (NOLOCK)
WHERE
	[TANTOSHA_CD] = @userName
	AND
	[PASSWORD] = @password
	AND
	[DELETE_FLG] = 0
END








GO

