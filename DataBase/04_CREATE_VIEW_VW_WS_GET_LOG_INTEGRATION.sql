USE LOG_DB
GO

CREATE VIEW dbo.VW_WS_GET_LOG_INTEGRATION
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
SELECT
    LI.ID_LOG_INTEGRATION,
	LI.DE_METHOD,
    LI.DT_CREATION,
    LI.NU_VERSION,
	LI.IS_SUCCESS,
	LI.IS_TEST,
	LI.IS_ACTIVE,
	LIT.NM_LOG_INTEGRATION_TYPE
FROM LOG_INTEGRATION AS LI WITH(NOLOCK)
INNER JOIN LOG_INTEGRATION_TYPE AS LIT ON LI.ID_LOG_INTEGRATION_TYPE = LIT.ID_LOG_INTEGRATION_TYPE;