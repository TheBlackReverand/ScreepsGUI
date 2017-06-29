using Newtonsoft.Json;
using ScreepsGUI.Tools.Json.Attributes;
using ScreepsGUI.Tools.Json.Converters;

namespace ScreepsGUI.DTO
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Account
    {
        [JsonProperty("_id")]
        public string Login;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("username")]
        public string Username;

        [JsonPath("steam.displayName")]
        public string SteamDisplayName;
    }
}