IF EXISTS ( SELECT * FROM sys.objects WHERE name = 'stp_GetMessageAll' AND user_name(schema_id) = 'dbo')
	DROP PROC dbo.stp_GetMessageAll
GO


CREATE PROC [dbo].[stp_GetMessageAll]
AS


BEGIN
	SET NOCOUNT ON

	-------------------------------------------------------------------------
	-- STP Process
	-------------------------------------------------------------------------
	SELECT
		MessageId
		, Class
		, Message
		, ButtonOK
		, ButtonNOK
		, ButtonCancel
		, DefaultButton
	FROM TM_MESSAGE WITH (NOLOCK)
END



