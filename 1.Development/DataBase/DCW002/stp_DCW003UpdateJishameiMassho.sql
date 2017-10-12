IF EXISTS ( SELECT * FROM sys.objects WHERE name = 'stp_DCW003UpdateJishameiMassho' AND user_name(schema_id) = 'dbo') 
        DROP PROC stp_DCW003UpdateJishameiMassho 
GO


CREATE PROCEDURE [dbo].[stp_DCW003UpdateJishameiMassho]
	@List DCW003JishameiMassho READONLY
	, @ErrorMsg VARCHAR (10)
	, @ListError	varchar(500)  OUT
	, @ListNoMap	varchar(500)  OUT
	, @ListImport varchar(500)  OUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ct_DOC_CONTROL_NO varchar(13)
	DECLARE @ct_IRAI_DATE	datetime
	DECLARE @ct_SHOP_CD	char(6)
	DECLARE @ct_GENSHA_LOCATION	varchar(25)
	DECLARE @ct_CAR_NAME varchar(100)
	DECLARE @ct_CHASSIS_NO varchar(25)
	DECLARE @ct_JISHAME_MASSHO_TYPE varchar(100)
	DECLARE @ct_NOTE varchar(1000)
	DECLARE @ct_TYPE varchar(3)

	DECLARE csv_cursor CURSOR FOR SELECT DC.DOC_CONTROL_NO
										,L.IraiDate
										,L.ShopCd
										,L.GenshaLocation 
										,L.CarName
										,L.ChassisNo
										,L.Jisame_Masso_Type
										,L.Note
	FROM 
	@List L INNER JOIN TT_DOC_CONTROL AS DC WITH(NOLOCK) ON (DC.CHASSIS_NO = L.ChassisNo AND DC.DOC_STATUS = '102')

	OPEN csv_cursor
	FETCH NEXT FROM csv_cursor INTO @ct_DOC_CONTROL_NO 
								,@ct_IRAI_DATE
								,@ct_SHOP_CD
								,@ct_GENSHA_LOCATION
								,@ct_CAR_NAME
								,@ct_CHASSIS_NO
								,@ct_JISHAME_MASSHO_TYPE
								,@ct_NOTE
	WHILE @@FETCH_STATUS = 0
		BEGIN
		IF (@ct_JISHAME_MASSHO_TYPE = '抹消依頼')
		BEGIN
			SET @ct_TYPE = '201'
		END
		ELSE IF(@ct_JISHAME_MASSHO_TYPE = '自社名依頼')
		BEGIN
			SET @ct_TYPE = '101'
		END
		ELSE SET @ct_TYPE = NULL
		 UPDATE TT_DOC_JISHAMEI_MASSHO_IF SET
		 
			IRAI_DATE = @ct_IRAI_DATE
			,SHOP_CD = @ct_SHOP_CD
			,GENSHA_LOCATION = @ct_GENSHA_LOCATION
			,CAR_NAME = @ct_CAR_NAME
			,CHASSIS_NO = @ct_CHASSIS_NO
			,JISHAME_MASSHO_TYPE = @ct_TYPE
			,NOTE = @ct_NOTE
		 
		 WHERE DOC_CONTROL_NO = @ct_DOC_CONTROL_NO
		 IF @@ROWCOUNT <> 0
		 BEGIN
			SET @ListImport = CONCAT(@ListImport,','+ @ct_CHASSIS_NO)
		 END
		 ELSE
		 BEGIN
			SET @ListNoMap = CONCAT(@ListNoMap,','+ @ct_CHASSIS_NO)
		 END
	FETCH NEXT FROM csv_cursor INTO @ct_DOC_CONTROL_NO
								,@ct_IRAI_DATE
								,@ct_SHOP_CD
								,@ct_GENSHA_LOCATION
								,@ct_CAR_NAME
								,@ct_CHASSIS_NO
								,@ct_JISHAME_MASSHO_TYPE
								,@ct_NOTE	
		END
	CLOSE csv_cursor
    DEALLOCATE csv_cursor  
END
