using Newtonsoft.Json;
using ScreepsGUI.Tools.Json.Attributes;
using ScreepsGUI.Tools.Json.Converters;
using ScreepsGUI.Tools.MVVM;

namespace ScreepsGUI.DTO
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Account : ViewModelBase
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonPath("steam.displayName")]
        public string SteamDisplayName { get; set; }
    }
}