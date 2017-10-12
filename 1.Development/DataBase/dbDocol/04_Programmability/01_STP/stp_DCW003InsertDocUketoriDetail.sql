USE [dbDOC]
GO

/****** Object:  StoredProcedure [dbo].[stp_DCW003InsertDocUketoriDetail]    Script Date: 2015/12/25 11:53:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[stp_DCW003InsertDocUketoriDetail]
(	
	@ListDocUketoriDetail		DCW003DocUketoriDetail		READONLY
	,@User	varchar(5)
	,@ErrorMsg	varchar(10)  OUT
)
AS

---------------------------------------------------------------------------
-- Version			: 001
-- Designer			: NghiaDT1
-- Programmer		: NghiaDT1
-- Created Date		: 2015/12/05
-- Comment			: Store insert TT_DOC_AUTO_SEARCH of DCW003
---------------------------------------------------------------------------
BEGIN
	SET NOCOUNT ON

	-------------------------------------------------------------------------
	-- DELETE
	-------------------------------------------------------------------------
	DELETE FROM TT_DOC_UKETORI_DETAIL
	WHERE TT_DOC_UKETORI_DETAIL.DOC_CONTROL_NO IN (SELECT DocControlNo FROM @ListDocUketoriDetail)

	-------------------------------------------------------------------------
	-- STP Process
	-------------------------------------------------------------------------
	INSERT INTO TT_DOC_UKETORI_DETAIL
	(
		[TT_DOC_UKETORI_DETAIL].DOC_CONTROL_NO
		,[TT_DOC_UKETORI_DETAIL].DOC_FUZOKUHIN_CD
		,[TT_DOC_UKETORI_DETAIL].DOC_COUNT
		,[TT_DOC_UKETORI_DETAIL].DOC_HAKKO_DATE
		,[TT_DOC_UKETORI_DETAIL].DOC_UKE_DATE
		,[TT_DOC_UKETORI_DETAIL].NOTE
		,[TT_DOC_UKETORI_DETAIL].CREATE_DATE
		,[TT_DOC_UKETORI_DETAIL].CREATE_USER_CD
		,[TT_DOC_UKETORI_DETAIL].UPDATE_DATE
		,[TT_DOC_UKETORI_DETAIL].UPDATE_USER_CD
		,[TT_DOC_UKETORI_DETAIL].DELETE_FLG
	)
	SELECT
		List.DocControlNo
		,List.DocFuzokuhinCd
		,List.DocCount
		,GETDATE()
		,GETDATE()
		,List.Note
		,GETDATE()
		,@User
		,GETDATE()
		,@User
		,0
	FROM @ListDocUketoriDetail List 
END

GO

