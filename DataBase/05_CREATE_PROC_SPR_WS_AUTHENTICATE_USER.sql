USE LOG_DB
GO

CREATE PROCEDURE [dbo].[SPR_WS_AUTHENTICATE_USER] (
	@P_USER_NAME		VARCHAR(200),
	@P_PASSWORD			VARCHAR(200)
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
	
	DECLARE 		
		@V_IS_SUCCESS				BIT = 0,
        @V_DE_MESSAGE				VARCHAR(255) = NULL,
        @V_DE_EXCEPTION_MESSAGE		VARCHAR(255) = NULL,
        @V_DE_STACK_TRACE			VARCHAR(MAX) = NULL,
        @V_DATA_OBJECT				VARCHAR(255) = NULL;
	
	BEGIN TRY
		DECLARE 
			@V_ID_USER					BIGINT;

		SELECT 
			@V_ID_USER = U.ID_USER
		FROM [USER] U WITH(NOLOCK)
		WHERE U.NM_USER = @P_USER_NAME 
		AND U.NM_PASSWORD = @P_PASSWORD
		AND U.IS_ACTIVE = 1;

		IF (ISNULL(@V_ID_USER, 0) > 0)
		BEGIN  			
			SET @V_DE_MESSAGE = 'OK';
			SET @V_IS_SUCCESS = 1;
		END  
		ELSE   
		BEGIN  
			SET @V_DE_MESSAGE = 'Usuário não encontrado na base de dados!';
		END
	END TRY
	BEGIN CATCH
		IF ( @@TRANCOUNT > 0) 
			ROLLBACK;
		
		SET @V_DE_MESSAGE = 'Erro ao processar chamada!';
		SET @V_DE_EXCEPTION_MESSAGE = 'Line: ' + ERROR_LINE();
		SET @V_DE_STACK_TRACE = ERROR_MESSAGE();
	END CATCH	

	SELECT  
		@V_IS_SUCCESS AS IS_SUCCESS,
        @V_DE_MESSAGE AS DE_MESSAGE,
        @V_DE_EXCEPTION_MESSAGE AS DE_EXCEPTION_MESSAGE,
        @V_DE_STACK_TRACE AS DE_STACK_TRACE,
        @V_DATA_OBJECT AS DATA_OBJECT;
END
