using System;
using Newtonsoft.Json;

namespace DiscordBotSurvivor
{
    public struct JsonConfig
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
    }
}
