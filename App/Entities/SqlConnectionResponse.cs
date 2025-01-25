﻿using System;

namespace App.Entities
{
    public class SqlConnectionResponse
    {
        #region Constructor
        public SqlConnectionResponse() { }

        public SqlConnectionResponse(bool isSuccess, string deMessage, object dataObject) {
            IsSuccess = isSuccess;
            DeMessage = deMessage;
            DeExceptionMessage = null;
            DeStackTrace = null;
            DataObject = dataObject;
        }

        public SqlConnectionResponse(bool isSuccess, string deMessage, string deExceptionMessage, string deStackTrace, object dataObject)
        {
            IsSuccess = isSuccess;
            DeMessage = deMessage;
            DeExceptionMessage = deExceptionMessage;
            DeStackTrace = deStackTrace;
            DataObject = dataObject;
        }

        public SqlConnectionResponse(dynamic pRowData)
        {
            IsSuccess = Convert.ToBoolean(pRowData.IS_SUCCESS);
            DeMessage = pRowData.DE_MESSAGE;
            DeExceptionMessage = pRowData.DE_EXCEPTION_MESSAGE;
            DeStackTrace = pRowData.DE_STACK_TRACE;
            DataObject = pRowData.DATA_OBJECT;
        }
        #endregion

        #region Atributtes
        public bool IsSuccess { get; set; }

        public string DeMessage { get; set; }

        public string DeExceptionMessage { get; set; }

        public string DeStackTrace { get; set; }

        public object DataObject { get; set; }
        #endregion
    }
}
