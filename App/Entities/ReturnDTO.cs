namespace App.Entities
{
    public class ReturnDTO
    {
        #region Constructor
        public ReturnDTO() { }

        public ReturnDTO(bool isSuccess, string deMessage, object resultObject)
        {
            IsSuccess = isSuccess;
            DeMessage = deMessage;
            ResultObject = resultObject;
        }
        #endregion

        #region Atributtes
        /// <summary>
        /// Status do retorno. Se for = true, é retorno OK
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Descrição do retorno. Se IsSuccess = false, retorna o motivo do erro
        /// </summary>
        public string DeMessage { get; set; }

        /// <summary>
        /// Status do retorno. Se for = true, é retorno OK
        /// </summary>
        public object ResultObject { get; set; }
        #endregion
    }
}