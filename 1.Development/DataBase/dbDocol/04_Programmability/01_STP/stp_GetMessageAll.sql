USE [dbDOC]
GO

/****** Object:  StoredProcedure [dbo].[stp_GetMessageAll]    Script Date: 2015/12/25 11:55:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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
	FROM M_MESSAGEMASTER WITH (NOLOCK)
END


GO

