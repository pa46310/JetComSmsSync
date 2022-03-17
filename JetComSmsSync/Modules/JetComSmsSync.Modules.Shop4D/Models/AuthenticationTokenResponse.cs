using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JetComSmsSync.Modules.Shop4D.Models
{
    public class AuthenticationTokenResponse
    {
        [JsonProperty("authentication_key")]
        public string AuthenticationKey { get; set; }
    }
}
