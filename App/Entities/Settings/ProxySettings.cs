using Newtonsoft.Json;

namespace App.Entities.Settings
{
    public class ProxySettings
    {

        [JsonProperty("MustUseWebProxy")]
        public bool MustUseWebProxy { get; set; }

        [JsonProperty("BaseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("MustUseDomain")]
        public bool MustUseDomain { get; set; }

        [JsonProperty("Domain")]
        public string Domain { get; set; }

        [JsonProperty("UseDefaultCredentials")]
        public bool UseDefaultCredentials { get; set; }
    }
}
