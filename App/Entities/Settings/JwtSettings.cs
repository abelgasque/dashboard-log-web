using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities.Settings
{
    public class JwtSettings
    {
        [JsonProperty("TokenType")]
        public string TokenType { get; set; }

        [JsonProperty("Secret")]
        public string Secret { get; set; }

        [JsonProperty("MinutesToExpire")]
        public int MinutesToExpire { get; set; }
    }
}
