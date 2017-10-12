IF EXISTS ( SELECT * FROM sys.objects WHERE name = 'USP_MAKE_HISTORY' AND user_name(schema_id) = 'dbo')
	DROP PROC USP_MAKE_HISTORY
GO
/* *****************************************************************************
 *  System Name				: DOC_DB
 *  Component Name			: USP_MAKE_HISTORY
 *
 *  Create Date				: 
 *  Creator					: 
 *  Contents				: 履歴登録処理
 *
 *  ParmeterInfomation	
 *	@p_doc_control_no						CHAR(13)					伝票番号
 *	@p_pgm_cd							VARCHAR(50)					プログラムコード
 *	@p_user_cd							VARCHAR(6)					ユーザコード
 *	@p_seq_upd_history					CHAR(15)		OUTPUT,		SEQ更新履歴
 *	@p_resultId							INT				OUTPUT,		結果ID
 *	@p_result_msg						VARCHAR(2000)	OUTPUT		結果メッセージ
 *
 *  Update Date				:
 *  Updater					:
 *  Update Contents			:
 *
 *  Copyright(c) 
 * *****************************************************************************/

CREATE PROCEDURE [dbo].[USP_MAKE_HISTORY]
(
	@p_doc_control_no						CHAR(13),						--書類管理番号
	@p_pgm_cd							VARCHAR(50),					--プログラムコード
	@p_user_cd							VARCHAR(6),						--ユーザコード
	@p_seq_upd_history					CHAR(15)		OUTPUT,			--履歴番号
	@p_result_id						INT				OUTPUT,			--結果ID			
	@p_result_msg						VARCHAR(2000)	OUTPUT			--結果メッセージ

)
AS
	/***************/ 
	/*  定数	　*/
	/***************/
	DECLARE	@CON_RESULT_SUCCESS INT		= 0	--成功（実行結果）
	DECLARE @CON_SAIBAN_KBN_SEQ_UPDATE_HISTORY 		CHAR(3)	= '102'	--履歴番号


	/***************/ 
	/*  変数	　*/
	/***************/
	DECLARE	@innerResult 		INT		= 0	--内部処理の実行結果
	DECLARE @active_spid		INT			--アクティブなサーバープロセスID
	DECLARE @login_time		DATETIME		--ログイン時刻
	DECLARE	@p_inner_result		INT			--内部処理の実行結果
	
BEGIN
    SET NOCOUNT ON;

	/***************/ 
	/*  初期化	　 */
	/***************/

	----- 変数  -----
	SET @p_result_id =0								--OUTPUT結果ID
	SET @p_result_msg = ''							--OUTPUT結果メッセージ
	SET @p_seq_upd_history = ''						--OUTPUT結果SEQ更新履歴

	/********************/
	/*  Try				*/
	/********************/
	BEGIN TRY

		/*-- 個車更新チェック --*/
		IF @p_doc_control_no IS NOT NULL AND @p_doc_control_no != ''	--書類管理番号
		BEGIN
			--SET @active_spid = sysdb.ssma_oracle.get_active_spid()
			--SET @login_time = sysdb.ssma_oracle.get_active_login_time()

--			EXECUTE master.dbo.xp_ora2ms_exec2
--				@active_spid,
--				@login_time, 
----				'"dbCLAIM"',
--				'dbo',
				EXECUTE dbo.USP_GET_SAIBAN_SUBSTANCE
				@CON_SAIBAN_KBN_SEQ_UPDATE_HISTORY,
				@p_pgm_cd,
				@p_user_cd,
				@p_seq_upd_history	OUTPUT,
				@p_result_id	OUTPUT,
				@p_result_msg	OUTPUT,
				@p_inner_result	OUTPUT

			IF @p_inner_result != @CON_RESULT_SUCCESS
			BEGIN
				/*-- 採番エラー	--*/
				RETURN 1
			END
			
			/*-- 書類管理 --*/
			INSERT INTO TH_DOC_CONTROL	
			(
			HISTORY_NO
			,HISTORY_TOROKU_DATE
			,DOC_CONTROL_NO
			   ,SHOHIN_TYPE
			   ,CHASSIS_NO
			   ,DOC_STATUS
			   ,SHIIRE_SHUPPINN_TOROKU_NO
			   ,URIAGE_SHUPPINN_TOROKU_NO
			   ,SHOP_CD
			   ,RAKUSATSU_SHOP_CD
			   ,SHIIRE_AA_KAIJO
			   ,URIAGE_AA_KAIJO
			   ,AA_KAISAI_KAISU
			   ,AA_KAISAI_DATE
			   ,AA_SHUPPIN_NO
			   ,DN_SEIYAKU_DATE
			   ,BBNO
			   ,SHIIRE_NO
			   ,NENSHIKI
			   ,MAKER_NAME
			   ,CAR_NAME
			   ,GRADE_NAME
			   ,KATASHIKI
			   ,CC
			   ,JOSHA_TEIIN_NUM
			   ,KEI_CAR_FLG
			   ,TOROKU_NO
			   ,SHAKEN_LIMIT_DATE
			   ,SHORUI_LIMIT_DATE
			   ,MEIHEN_SHAKEN_TOROKU_DATE
			   ,JISHAMEI_FLG
			   ,MASSHO_FLG
			   ,DOC_NYUKO_DATE
			   ,DOC_SHUKKO_DATE
			   ,JISHAMEI_IRAI_SHUKKO_DATE
			   ,JISHAMEI_KANRYO_NYUKO_DATE
			   ,MASSHO_IRAI_SHUKKO_DATE
			   ,MASSHO_KANRYO_NYUKO_DATE
			   ,RACK_NO
			   ,FILE_NO
			   ,MEMO
			   ,CAR_ID
			   ,CAR_SUB_ID
			   ,CREATE_DATE	           --作成日時
			   ,CREATE_USER_CD	           --作成ユーザコード
			   ,CREATE_PG_CD	           --作成プログラムコード
			   ,UPDATE_DATE	           --更新日時
			   ,UPDATE_USER_CD	           --更新ユーザコード
			   ,UPDATE_PG_CD	           --更新プログラムコード
			   ,DELETE_DATE	           --削除日時
			   ,DELETE_FLG	           --削除フラグ
			 )
			(
			SELECT 
					@p_seq_upd_history						 --履歴番号
				   ,GETDATE()	           --履歴番号登録日時
				   ,DOC_CONTROL_NO
				   ,SHOHIN_TYPE
				 ,CHASSIS_NO
				,DOC_STATUS
				   ,SHIIRE_SHUPPINN_TOROKU_NO
				   ,URIAGE_SHUPPINN_TOROKU_NO
				   ,SHOP_CD
				   ,RAKUSATSU_SHOP_CD
				   ,SHIIRE_AA_KAIJO
				   ,URIAGE_AA_KAIJO
				   ,AA_KAISAI_KAISU
				   ,AA_KAISAI_DATE
				   ,AA_SHUPPIN_NO
				   ,DN_SEIYAKU_DATE
				   ,BBNO
				   ,SHIIRE_NO
				   ,NENSHIKI
				   ,MAKER_NAME
				   ,CAR_NAME
				   ,GRADE_NAME
				   ,KATASHIKI
				   ,CC
				   ,JOSHA_TEIIN_NUM
				   ,KEI_CAR_FLG
				   ,TOROKU_NO
				   ,SHAKEN_LIMIT_DATE
				   ,SHORUI_LIMIT_DATE
				   ,MEIHEN_SHAKEN_TOROKU_DATE
				   ,JISHAMEI_FLG
				   ,MASSHO_FLG
				   ,DOC_NYUKO_DATE
				   ,DOC_SHUKKO_DATE
				   ,JISHAMEI_IRAI_SHUKKO_DATE
				   ,JISHAMEI_KANRYO_NYUKO_DATE
				   ,MASSHO_IRAI_SHUKKO_DATE
				   ,MASSHO_KANRYO_NYUKO_DATE
				   ,RACK_NO
				   ,FILE_NO
				   ,MEMO
				   ,CAR_ID
				   ,CAR_SUB_ID
				   ,CREATE_DATE	           --作成日時
				   ,CREATE_USER_CD	           --作成ユーザコード
				   ,CREATE_PG_CD	           --作成プログラムコード
				   ,UPDATE_DATE	           --更新日時
				   ,UPDATE_USER_CD	           --更新ユーザコード
				   ,UPDATE_PG_CD	           --更新プログラムコード
				   ,DELETE_DATE	           --削除日時
				   ,DELETE_FLG	           --削除フラグ
				FROM 
					TT_DOC_CONTROL 
				WHERE
					DOC_CONTROL_NO = @p_doc_control_no
				AND
					DELETE_FLG = '0'
			)

			/*-- 書類受取詳細 --*/
			INSERT INTO TH_DOC_UKETORI_DETAIL	
			(
			   HISTORY_NO	 --履歴番号
			   ,HISTORY_TOROKU_DATE	           --履歴登録日時
				,DOC_CONTROL_NO	
				  ,DOC_FUZOKUHIN_CD
				  ,DOC_COUNT					
				  ,DOC_HAKKO_DATE					
				  ,DOC_UKE_DATE					
				  ,NOTE					
				  ,CREATE_DATE					
				  ,CREATE_USER_CD					
				  ,CREATE_PG_CD					
				  ,UPDATE_DATE					
				  ,UPDATE_USER_CD					
				  ,UPDATE_PG_CD					
				  ,DELETE_DATE					
				  ,DELETE_FLG
			 )
			(
			SELECT 
					@p_seq_upd_history						 --履歴番号
				   ,GETDATE()	           --履歴番号登録日時			
				,DOC_CONTROL_NO	
				  ,DOC_FUZOKUHIN_CD
				  ,DOC_COUNT					
				  ,DOC_HAKKO_DATE					
				  ,DOC_UKE_DATE					
				  ,NOTE					
				  ,CREATE_DATE					
				  ,CREATE_USER_CD					
				  ,CREATE_PG_CD					
				  ,UPDATE_DATE					
				  ,UPDATE_USER_CD					
				  ,UPDATE_PG_CD					
				  ,DELETE_DATE					
				  ,DELETE_FLG
			FROM				 
				   TT_DOC_UKETORI_DETAIL 
			  WHERE
				  DOC_CONTROL_NO = @p_doc_control_no
			  AND
				  DELETE_FLG = '0'
			)
			
		END
		
		RETURN 0
	END TRY

	/********************/ 
	/*  ExceptionError  */
	/********************/
	BEGIN CATCH
		--異常終了
		IF @@TRANCOUNT <> 0
		BEGIN
			ROLLBACK TRANSACTION
		END

		IF (@p_result_id = 0)
		BEGIN
			SET @p_result_id = ERROR_NUMBER()
		END

		IF (@p_result_msg IS NULL) OR (@p_result_msg = '')
		BEGIN
			SET @p_result_msg = ERROR_MESSAGE()
		END
		RETURN 1
	END CATCH
END







