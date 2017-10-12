USE [dbDOC]
GO

/****** Object:  StoredProcedure [dbo].[stp_DCW003GetMasterFuzokuhin]    Script Date: 2015/12/25 11:53:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[stp_DCW003GetMasterFuzokuhin]

AS

---------------------------------------------------------------------------
-- Version			: 001
-- Designer			: NghiaDT1
-- Programmer		: NghiaDT1
-- Created Date		: 2015/12/03
-- Comment			: 
---------------------------------------------------------------------------
BEGIN
	SET NOCOUNT ON

	-------------------------------------------------------------------------
	-- STP Process
	-------------------------------------------------------------------------
	SELECT
		TM_DOC_FUZOKUHIN.DOC_FUZOKUHIN_CD				AS	DocFuzokuhinCd
		,TM_DOC_FUZOKUHIN.DOC_FUZOKUHIN_DISP_NAME		AS	DocFuzokuhinName
		,TM_DOC_FUZOKUHIN.DOC_FUZOKUHIN_TYPE_CD			AS	DocFuzokuhinType
	-------------------------------------------------------------------------
	-- Source table
	-------------------------------------------------------------------------
	FROM	
		TM_DOC_FUZOKUHIN WITH(NOLOCK)
	WHERE TM_DOC_FUZOKUHIN.DISP_ORDER <> 0
	AND TM_DOC_FUZOKUHIN.DELETE_FLG = 0
	
	ORDER BY TM_DOC_FUZOKUHIN.DOC_FUZOKUHIN_TYPE_CD, TM_DOC_FUZOKUHIN.DISP_ORDER

END


GO

