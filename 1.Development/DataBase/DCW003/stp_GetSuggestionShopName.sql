IF EXISTS ( SELECT * FROM sys.objects WHERE name = 'stp_GetSuggestionShopName' AND user_name(schema_id) = 'dbo')
	DROP PROC stp_GetSuggestionShopName
GO

CREATE PROC [dbo].[stp_GetSuggestionShopName]
(
	@MaxResult					INT
	,@TextPattern				VARCHAR(60)
)
AS
-----------------------------------------------------------------------------
-- Version		: 001
-- Designer		: NghiaDT1
-- Programmer	: NghiaDT1
-- Date			: 2015/12/07
-- Comment		: Create new
-----------------------------------------------------------------------------
BEGIN
	SET NOCOUNT ON

	-------------------------------------------------------------------------
	-- STP Process
	-------------------------------------------------------------------------

	SELECT TOP (@MaxResult) 
		TEMPO_CD AS FieldCode
		,ISNULL(TEMPO_NAME, '') AS FieldName
	FROM
		TM_SHOP WITH (NOLOCK)
	WHERE
		TEMPO_NAME LIKE '%'+@TextPattern+'%'COLLATE JAPANESE_BIN
		AND DELETE_FLG = 0
	ORDER BY
		TEMPO_CD ASC
END

GO


