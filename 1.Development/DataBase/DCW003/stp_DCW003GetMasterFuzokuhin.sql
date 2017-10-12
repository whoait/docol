IF EXISTS ( SELECT * FROM sys.objects WHERE name = 'stp_DCW003GetMasterFuzokuhin' AND user_name(schema_id) = 'dbo')
	DROP PROC stp_DCW003GetMasterFuzokuhin
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
