USE [dbDOC]
GO

/****** Object:  StoredProcedure [dbo].[stp_DCW001CheckDomainLogin]    Script Date: 2015/12/25 11:50:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROC [dbo].[stp_DCW001CheckDomainLogin]
(
	@domain_Login	NVARCHAR(100)			-- UserName

)
AS
---------------------------------------------------------------------------
-- Version			: 1.0
-- Designer			: HoaVV
-- Programmer		: HoaVV
-- Created Date		: 2015/14/12
-- Comment			: Create new
---------------------------------------------------------------------------
BEGIN
	SET NOCOUNT ON

	-------------------------------------------------------------------------
	
	
	
SELECT
	TM_CONST.VALUE
FROM [dbo].[TM_CONST] WITH (NOLOCK)
WHERE
	TM_CONST.VALUE = @domain_Login
	AND
	TM_CONST.TYPE_VALUE = 1
	AND
	TM_CONST.TYPE IN('遷移許可URL');
	
END



GO

