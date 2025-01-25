using Newtonsoft.Json;
using System;

namespace App.Entities.DataBase
{
    public class GetLogIntegrationForChartYearDb
    {
        public GetLogIntegrationForChartYearDb() { }

        public GetLogIntegrationForChartYearDb(dynamic pRowData) {
            NuYear = pRowData.NU_YEAR;
            DtStartRange = pRowData.DT_START_RANGE;
            DtEndRange = pRowData.DT_END_RANGE;
            NuSuccess = pRowData.NU_SUCCESS;
            NuError = pRowData.NU_ERROR;
        }

        [JsonProperty("nuYear")]
        public int NuYear { get; set; }

        [JsonProperty("dtStartRange")]
        public DateTime DtStartRange { get; set; }

        [JsonProperty("dtEndRange")]
        public DateTime DtEndRange { get; set; }

        [JsonProperty("nuSuccess")]
        public long NuSuccess { get; set; }

        [JsonProperty("nuError")]
        public long NuError { get; set; }
    }
}
