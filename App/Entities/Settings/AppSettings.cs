using Newtonsoft.Json;
using System.Data;

namespace App.Entities.Settings
{
    public class AppSettings
    {
        [JsonProperty("NuVersion")]
        public string NuVersion { get; set; }

        [JsonProperty("IdProject")]
        public long? IdProject { get; set; }

        [JsonProperty("IdLogIntegrationType")]
        public int IdLogIntegrationType { get; set; }

        [JsonProperty("IsTest")]
        public bool IsTest { get; set; }

        [JsonProperty("MustRemoveDocSwagger")]
        public bool MustRemoveDocSwagger { get; set; }

        [JsonProperty("MustRemoveSpaApp")]
        public bool MustRemoveSpaApp { get; set; }
    }
}
