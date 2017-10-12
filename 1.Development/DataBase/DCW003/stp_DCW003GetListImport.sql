IF EXISTS ( SELECT * FROM sys.objects WHERE name = 'stp_DCW003GetListImport' AND user_name(schema_id) = 'dbo')
	DROP PROC stp_DCW003GetListImport
GO

CREATE PROC [dbo].[stp_DCW003GetListImport]
(	
	@ListDocControlNo	DCW003DocControlNo	READONLY	
	, @PageIndex int = 1
	, @PageSize int = 10
)
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
	SELECT * FROM(
	SELECT
		[TT_DOC_CONTROL].DOC_CONTROL_NO					AS DocControlNo							-- ���ފǗ��ԍ�
		,[TT_DOC_CONTROL].SHIIRE_SHUPPINN_TOROKU_NO		AS ShiireShuppinnTorokuNo				-- �d��DN�o�i�ԍ�
		,[TT_DOC_CONTROL].URIAGE_SHUPPINN_TOROKU_NO		AS UriageShuppinnTorokuNo				-- ����DN�o�i�ԍ�
		,[TT_DOC_CONTROL].CHASSIS_NO					AS ChassisNo							-- �ԑ�ԍ�
		,ISNULL([SHOP].TEMPO_CD,'') +' '+ISNULL([SHOP].TEMPO_NAME,'')								AS ShopName								-- �o�i�X���
		,ISNULL([RAKUSATSU_SHOP].TEMPO_CD,'') +' '+ ISNULL([RAKUSATSU_SHOP].TEMPO_NAME,'')			AS RakusatsuShopName					-- ���D�X���
		,[TT_DOC_CONTROL].SHIIRE_AA_KAIJO				AS ShiireAaKaijo						-- �d��AA���
		,[TT_DOC_CONTROL].URIAGE_AA_KAIJO				AS UriageAaKaijo						-- ����AA���
		,[TT_DOC_CONTROL].NENSHIKI						AS Nenshiki								-- �N��
		,[TT_DOC_CONTROL].KEI_CAR_FLG					AS KeiCarFlg							-- ���q�敪
		,[TT_DOC_CONTROL].AA_KAISAI_DATE				AS AaKaisaiDate							-- AA�J�Ó�
		,[TT_DOC_CONTROL].MAKER_NAME					AS MakerName							-- ���[�J�[
		,[TT_DOC_CONTROL].CAR_NAME						AS CarName								-- �Ԗ�
		,[TT_DOC_CONTROL].GRADE_NAME					AS GradeName							-- �O���[�h
		,[TT_DOC_CONTROL].AA_SHUPPIN_NO					AS AaShuppinNo							-- AA�ԍ�
		,[TT_DOC_CONTROL].DN_SEIYAKU_DATE				AS DnSeiyakuDate						-- DN�����
		,[TT_DOC_CONTROL].KATASHIKI						AS Katashiki							-- �^��
		,[TT_DOC_CONTROL].MASSHO_FLG					AS MasshoFlg							-- ���ދ敪
		,[TT_DOC_CONTROL].JISHAMEI_FLG					AS JishameiFlg							-- ���Ж��敪
		,[TT_DOC_CONTROL].DOC_STATUS					AS DocStatus							-- ���ރX�e�[�^�X
		,[TT_DOC_CONTROL].TOROKU_NO						AS TorokuNo								-- �o�^�i���o�[
		,[TT_DOC_CONTROL].SHORUI_LIMIT_DATE				AS ShoruiLimitDate						-- ���ޗL������
		,[TT_DOC_CONTROL].FILE_NO						AS FileNo								-- �t�@�C���ԍ�
		,[TT_DOC_CONTROL].RACK_NO						AS RacNo
		,[TT_DOC_CONTROL].SHIIRE_NO						AS ShiireNo								-- �d���ԍ�
		,[TT_DOC_CONTROL].SHAKEN_LIMIT_DATE				AS ShakenLimitDate						-- �Ԍ�������
		,[TT_DOC_CONTROL].DOC_NYUKO_DATE				AS DocNyukoDate							-- ���ޓ��ɓ�
		,[TT_DOC_CONTROL].JISHAMEI_IRAI_SHUKKO_DATE		AS JishameiIraiShukkoDate				-- ���Ж��˗���
		,[TT_DOC_CONTROL].MASSHO_IRAI_SHUKKO_DATE		AS MasshoIraiShukkoDate					-- �����˗���
		,[TT_DOC_CONTROL].DOC_SHUKKO_DATE				AS DocShukkoDate						-- ���ޏo�ɓ�
		,[TT_DOC_CONTROL].JISHAMEI_KANRYO_NYUKO_DATE	AS JishameiKanryoNyukoDate				-- ���Ж�������
		,[TT_DOC_CONTROL].MASSHO_KANRYO_NYUKO_DATE		AS MasshoKanryoNyukoDate				-- ����������
		,[TT_DOC_CONTROL].MEMO							AS Memo									-- ����
		, ROW_NUMBER() OVER(ORDER BY TT_DOC_CONTROL.DOC_CONTROL_NO) AS RowNum
		, COUNT (*) OVER() AS [RowCount]
	-------------------------------------------------------------------------
	-- Source table
	-------------------------------------------------------------------------
	FROM	
		TT_DOC_CONTROL WITH(NOLOCK)
	LEFT JOIN TM_SHOP SHOP
	ON [TT_DOC_CONTROL].SHOP_CD = [SHOP].TEMPO_CD
	AND [SHOP].DELETE_FLG = 0
	LEFT JOIN TM_SHOP RAKUSATSU_SHOP
	ON [TT_DOC_CONTROL].RAKUSATSU_SHOP_CD = [RAKUSATSU_SHOP].TEMPO_CD
	AND [RAKUSATSU_SHOP].DELETE_FLG = 0
	INNER JOIN @ListDocControlNo List
	ON [TT_DOC_CONTROL].DOC_CONTROL_NO = [List].DocControlNo
	) AS SearchInfoTempTable

		WHERE RowNum > @PageSize * (@PageIndex - 1) AND RowNum < @PageSize * @PageIndex + 1
END


GO


