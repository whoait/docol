IF EXISTS ( SELECT * FROM sys.objects WHERE name = 'USP_GET_SAIBAN_SUBSTANCE' AND user_name(schema_id) = 'dbo')
	DROP PROC USP_GET_SAIBAN_SUBSTANCE
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* *****************************************************************************
 *  System Name				: 
 *  Component Name			: USP_GET_SAIBAN_SUBSTANCE
 *
 *  Create Date				: 
 *  Creator					: 
 *  Contents				: 新規に各種IDを採番し、そのIDを返す。
 * 
 *  ParmeterInfomation		@p_saiban_kbn				VARCHAR(3),				--採番区分
 *							@p_pgm_cd					VARCHAR(50),			--プログラムコード
 *							@p_user_cd					VARCHAR(6),				--ユーザコード
 *							@p_saiban_value				VARCHAR(16),			--採番値
 *							@p_result_id				INT,					--結果ID(ユーザ定義エラー:-1)
 *							@p_result_msg				VARCHAR(2000),			--結果メッセージ
 *							@p_inner_result				INT						--結果コード
 *  Update Date				: 
 *  Updater					: 
 *  Update Contents			: 
 * 
 * *****************************************************************************/

CREATE PROCEDURE [dbo].[USP_GET_SAIBAN_SUBSTANCE]
(
	@p_saiban_kbn				VARCHAR(3),					--採番区分
	@p_pgm_cd					VARCHAR(50),				--プログラムコード
	@p_user_cd					VARCHAR(6),					--ユーザコード
	@p_saiban_value				VARCHAR(16)		OUTPUT,		--採番値
	@p_result_id				INT				OUTPUT,		--結果ID
	@p_result_msg				VARCHAR(2000)	OUTPUT,		--結果メッセージ
	@p_inner_result				INT				OUTPUT		--結果コード
)
AS
	/***************/ 
	/*  定数	　*/
	/***************/
	DECLARE @CON_NUMBER_TYPE				VARCHAR(30) = '採番値桁数';	--定数マスタ_区分（採番値桁数）

	/***************/ 
	/*  変数	　*/
	/***************/
	DECLARE	@formatNum				INT				--成型桁数
	DECLARE	@sysDate				DATETIME		--システム日付
	DECLARE	@wkSaibanVal			INT				--採番値（一時格納）
	
BEGIN
    SET NOCOUNT ON;

	/***************/ 
	/*  初期化	　 */
	/***************/
	----- 変数  -----
	SET @p_result_id =0			--OUTPUT結果ID
	SET @p_result_msg = ''		--OUTPUT結果メッセージ
	SET @formatNum= 0			--成型桁数


	/***************/ 
	/*  処理	　 */
	/***************/
	SET	LOCK_TIMEOUT 60000                      --1分待機

	/********************/ 
	/*  Try			*/
	/********************/
	BEGIN TRY

		BEGIN TRANSACTION
			/*----- システム日付取得 -----*/
			SET @sysDate = CONVERT(varchar, GETDATE(), 112)

			/*----- 採番テーブル値取得 -----*/
			SELECT 
				@wkSaibanVal = COUNTER_VALUE				--カウンタ値
			FROM 
				TM_NUMBERING_CONTROL					--採番管理テーブル
			WITH
				(ROWLOCK, UPDLOCK)							--行ロック、更新ロック
			WHERE
				NUMBERING_TYPE = @p_saiban_kbn			--採番区分
			AND ADMIN_DATE = @sysDate					--管理日付

			IF(@wkSaibanVal IS NULL) OR (@wkSaibanVal = '')
			BEGIN
				SET @wkSaibanVal = 1
				/*----- 採番テーブル値追加 -----*/
				INSERT INTO				
					TM_NUMBERING_CONTROL
					(
					NUMBERING_TYPE,						--採番区分
					ADMIN_DATE,							--管理日付
					COUNTER_VALUE,						--カウンタ値
					CREATE_DATE,						--作成日時
					CREATE_USER_CD,						--作成ユーザコード
					CREATE_PG_CD,						--作成プログラムコード
					UPDATE_DATE,						--更新日時
					UPDATE_USER_CD,						--更新ユーザコード
					UPDATE_PG_CD,						--更新プログラムコード
					DELETE_FLG							--削除フラグ
					)
				VALUES
					(
					@p_saiban_kbn,
					@sysDate,
					@wkSaibanVal,
					GETDATE(),
					@p_user_cd,
					@p_pgm_cd,
					GETDATE(),
					@p_user_cd,
					@p_pgm_cd,
					'0'
					)
			END
			ELSE
			BEGIN
				/*----- 採番テーブル値インクリメント -----*/
				SET @wkSaibanVal = @wkSaibanVal + 1			--インクリメント
				UPDATE
					TM_NUMBERING_CONTROL
				SET
					COUNTER_VALUE = @wkSaibanVal,			--カウンタ
					UPDATE_DATE = GETDATE(),				--更新日時
					UPDATE_USER_CD = @p_user_cd,			--更新ユーザコード
					UPDATE_PG_CD = @p_pgm_cd,				--更新プログラムコード
					DELETE_FLG = '0'						--削除フラグ
				WHERE
						NUMBERING_TYPE = @p_saiban_kbn			--採番区分
					AND ADMIN_DATE = @sysDate					--管理日付
			END

		/*----- 採番値成型 -----*/
		/*----- 成型のための桁数取得 -----*/
			/*----- 採番値成型 -----*/
			/*----- 成型のための桁数取得 -----*/
			SELECT
				@formatNum = CAST(VALUE AS INT)	--値
			FROM
				TM_CONST					--定数マスタ
			WITH
				(NOLOCK)						--行ロックなし
			WHERE
				TYPE = @CON_NUMBER_TYPE					--区分
			AND TYPE_VALUE = @p_saiban_kbn					--区分値
			AND DELETE_FLG = '0'							--削除フラグ

		IF LEN(CONVERT(varchar, @wkSaibanVal)) > @formatNum
		BEGIN
			/*-- 有効桁数オーバー --*/
			IF @@TRANCOUNT <> 0
			BEGIN
				ROLLBACK TRANSACTION
			END

			SET @p_result_id = -1
			SET @p_result_msg = '採番可能な数値を超えています'
			SET @p_inner_result = 1
			RETURN 1
		END

		/*----- 成型 -----*/
		SET @p_saiban_value = CONVERT(varchar, @sysDate,112) + RIGHT(REPLICATE('0', @formatNum) + CONVERT(varchar, @wkSaibanVal), @formatNum)
		COMMIT TRANSACTION

		SET @p_inner_result = 0
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

		SET @p_inner_result = 1
		RETURN 1
	END CATCH
END

