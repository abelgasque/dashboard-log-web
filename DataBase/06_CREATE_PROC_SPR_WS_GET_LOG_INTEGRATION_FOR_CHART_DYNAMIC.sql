USE LOG_DB
GO

CREATE PROCEDURE dbo.[SPR_WS_GET_LOG_INTEGRATION_FOR_CHART_DYNAMIC] (
	@P_IS_TEST BIT = 0,
	@P_MUST_FILTER_YEAR BIT = 0
)
AS
/*
/// <resumo>
/// Autor....: Abel Gasque
/// Data.....: 21/02/2022
/// Ticket...: #DODP-0000
/// Descrição: Rotina.
///
///	Log de Alterações:
///
///		<Data>, <Autor>, <#Ticket>
///         <Descrição>
///
/// </resumo>
*/
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY	
		IF(@P_MUST_FILTER_YEAR > 0)
		BEGIN
			DECLARE @V_NU_YEAR INT, @V_NU_COUNT_WHILE INT = 0, @V_QTD_YEAR_FILTER INT = 5;

			SELECT TOP 1 @V_NU_YEAR = YEAR(DT_CREATION) FROM LOG_INTEGRATION ORDER BY ID_LOG_INTEGRATION DESC;		
			
			CREATE TABLE #TMP_AUX_YEAR (
				NU_YEAR INT NULL,
				DT_START_RANGE DATETIME NULL,
				DT_END_RANGE DATETIME NULL,
			);
			
			SET @V_NU_YEAR = (@V_NU_YEAR - (@V_QTD_YEAR_FILTER - 1));
			
			WHILE (@V_NU_COUNT_WHILE < @V_QTD_YEAR_FILTER)
			BEGIN  	
				INSERT INTO #TMP_AUX_YEAR(
					NU_YEAR,
					DT_START_RANGE,
					DT_END_RANGE
				)VALUES(
					@V_NU_YEAR,
					(DATETIMEFROMPARTS(@V_NU_YEAR, 1, 1, 00, 00, 00, 000)),
					(DATETIMEFROMPARTS(@V_NU_YEAR, 12, DAY(EOMONTH(DATEFROMPARTS(YEAR(GETDATE()), 12, 1))), 23, 59, 59, 000))
				);

				SET @V_NU_YEAR = (@V_NU_YEAR + 1);
				SET @V_NU_COUNT_WHILE = (@V_NU_COUNT_WHILE + 1);
			END  

			SELECT 
				TMP.*,
				(
					SELECT 
						COUNT(*) 
					FROM LOG_INTEGRATION 
					WHERE IS_SUCCESS = 1 
					AND IS_ACTIVE = 1 
					AND IS_TEST = @P_IS_TEST 
					AND DT_CREATION BETWEEN TMP.DT_START_RANGE AND TMP.DT_END_RANGE
				) AS NU_SUCCESS,
				(
					SELECT 
						COUNT(*) 
					FROM LOG_INTEGRATION 
					WHERE IS_SUCCESS = 0 
					AND IS_ACTIVE = 1
					AND IS_TEST = @P_IS_TEST 
					AND DT_CREATION BETWEEN TMP.DT_START_RANGE AND TMP.DT_END_RANGE
				) AS NU_ERROR	
			FROM #TMP_AUX_YEAR TMP
			ORDER BY TMP.NU_YEAR;
					
			DROP TABLE #TMP_AUX_YEAR;
		END
		ELSE
		BEGIN	
			DECLARE @V_AUX_1 VARCHAR(8000), @V_AUX_2 CHAR;

			SET @V_AUX_1 = (SELECT months FROM sys.syslanguages WHERE alias = 'Brazilian');
			SET @V_AUX_2 = ',';

			WITH _SPLIT(ID, _INDEX, _LENGTH) AS
			(
				SELECT 1, 1, CHARINDEX(@V_AUX_2, @V_AUX_1 + @V_AUX_2) UNION ALL
		
				SELECT
					ID + 1,
					_LENGTH + 1,
					CHARINDEX(@V_AUX_2, @V_AUX_1 + @V_AUX_2, _LENGTH + 1)
				FROM _SPLIT
				WHERE CHARINDEX(@V_AUX_2, @V_AUX_1 + @V_AUX_2, _LENGTH + 1) <> 0
			)
			, MONTHS (ID_MONTH, NM_MONTH) AS
			(SELECT ID, SUBSTRING(@V_AUX_1, _INDEX, _LENGTH - _INDEX) FROM _SPLIT)

			SELECT 
				ID_MONTH, 
				NM_MONTH,
				(DATETIMEFROMPARTS(YEAR(GETDATE()), ID_MONTH, 1, 00, 00, 00, 000)) AS DT_START_RANGE,
				(DATETIMEFROMPARTS(YEAR(GETDATE()), ID_MONTH, DAY(EOMONTH(DATEFROMPARTS(YEAR(GETDATE()), ID_MONTH, 1))), 23, 59, 59, 000)) AS DT_END_RANGE
			INTO #TMP_MONTHS
			FROM MONTHS;

			SELECT 
				TMP.*,
				(
					SELECT 
						COUNT(*) 
					FROM LOG_INTEGRATION 
					WHERE IS_SUCCESS = 1 
					AND IS_ACTIVE = 1
					AND IS_TEST = @P_IS_TEST 
					AND DT_CREATION BETWEEN TMP.DT_START_RANGE AND TMP.DT_END_RANGE
				) AS NU_SUCCESS,
				(
					SELECT 
						COUNT(*) 
					FROM LOG_INTEGRATION 
					WHERE IS_SUCCESS = 0 
					AND IS_ACTIVE = 1
					AND IS_TEST = @P_IS_TEST 
					AND DT_CREATION BETWEEN TMP.DT_START_RANGE AND TMP.DT_END_RANGE
				) AS NU_ERROR	
			FROM #TMP_MONTHS TMP
			ORDER BY TMP.ID_MONTH;

			DROP TABLE #TMP_MONTHS;
		END		
	END TRY
	BEGIN CATCH 		
		SELECT 'ERRO NO PROCESSAMENTO' AS DE_MESSAGE, ERROR_MESSAGE() AS DE_ERROR_MESSAGE;
	END CATCH 
END