using Newtonsoft.Json;
using System;

namespace App.Entities.DataBase
{
    public class GetLogIntegrationDb
    {
        public GetLogIntegrationDb() { }

        public GetLogIntegrationDb(dynamic pRowData) {
            IdLogIntegration = pRowData.ID_LOG_INTEGRATION;            
            IdMailing = pRowData.ID_MAILING;
            DtCreation = pRowData.DT_CREATION;
            DeMethod = pRowData.DE_METHOD;
            IsSuccess = pRowData.IS_SUCCESS;
            NuVersion = pRowData.NU_VERSION;
            NmLogIntegrationType = pRowData.NM_LOG_INTEGRATION_TYPE;
        }

        [JsonProperty("idLogIntegration")]
        public long? IdLogIntegration { get; set; }

        [JsonProperty("idMailing")]
        public long? IdMailing { get; set; }

        [JsonProperty("dtCreation")]
        public DateTime? DtCreation { get; set; }

        [JsonProperty("deMethod")]
        public string DeMethod { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        
        [JsonProperty("nuVersion")]
        public string NuVersion { get; set; }

        [JsonProperty("nmLogIntegrationType")]
        public string NmLogIntegrationType { get; set; }
    }
}
