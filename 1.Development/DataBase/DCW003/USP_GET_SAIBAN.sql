IF EXISTS ( SELECT * FROM sys.objects WHERE name = 'USP_GET_SAIBAN' AND user_name(schema_id) = 'dbo')
	DROP PROC USP_GET_SAIBAN
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* *****************************************************************************
 *  System Name				: 
 *  Component Name			: USP_GET_SAIBAN
 *
 *  Create Date				: 
 *  Creator					: 
 *  Contents				: 新規に各種IDを採番し、そのIDを返す。
 * 
 *  ParmeterInfomation		@p_saiban_kbn				VARCHAR(3),				--採番区分
 *							@p_sys_kbn					VARCHAR(3),				--システム区分
 *							@p_pgm_cd					VARCHAR(50),			--プログラムコード
 *							@p_user_cd					VARCHAR(6),				--ユーザコード
 *							@p_saiban_value				VARCHAR(16),			--採番値
 *							@p_result_id				INT,					--結果ID(ユーザ定義エラー:-1)
 *							@p_result_msg				VARCHAR(2000),			--結果メッセージ
 *  Update Date				: 2011/03/03
 *  Updater					: Atsushi Takahashi
 *  Update Contents			: 自律型トランザクションに変更
 * 
 * *****************************************************************************/

CREATE PROCEDURE [dbo].[USP_GET_SAIBAN]
(
	@p_saiban_kbn				VARCHAR(3),					--採番区分
	--@p_sys_kbn					VARCHAR(3),					--システム区分
	@p_pgm_cd					VARCHAR(50),				--プログラムコード
	@p_user_cd					VARCHAR(6),					--ユーザコード
	@p_saiban_value				VARCHAR(16)		OUTPUT,		--採番値
	@p_result_id				INT				OUTPUT,		--結果ID
	@p_result_msg				VARCHAR(2000)	OUTPUT		--結果メッセージ
)
AS
	/***************/ 
	/*  変数	　*/
	/***************/
	DECLARE @active_spid		INT				--アクティブなサーバープロセスID
	DECLARE @login_time			DATETIME		--ログイン時刻
	DECLARE	@p_inner_result		INT				--内部処理の実行結果
	
BEGIN
    SET NOCOUNT ON;

	--SET @active_spid = sysdb.ssma_oracle.get_active_spid()
	--SET @login_time = sysdb.ssma_oracle.get_active_login_time()

	/********************/ 
	/*  Try			*/
	/********************/
	BEGIN TRY
		--EXECUTE 
	--	master.dbo.xp_ora2ms_exec2
	--		@active_spid,
	--		@login_time, 
	----		'"dbCLAIM"',
	--		'dbo',
			EXECUTE dbo.USP_GET_SAIBAN_SUBSTANCE
			@p_saiban_kbn,
			--@p_sys_kbn,
			@p_pgm_cd,
			@p_user_cd,
			@p_saiban_value OUTPUT,
			@p_result_id OUTPUT,
			@p_result_msg OUTPUT,
			@p_inner_result OUTPUT
			
			--SET @p_saiban_value = 'AAA'
			RETURN @p_inner_result
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

