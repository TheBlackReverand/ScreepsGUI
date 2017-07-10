using Newtonsoft.Json;
using ScreepsGUI.Tools.Json.Attributes;
using ScreepsGUI.Tools.Json.Converters;

namespace ScreepsGUI.ClientAPI.DTO
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class UserAccount : BaseAccount
    {
        [JsonProperty("_id")]
        public string Id { get; set; }


        [JsonPath("steam.id")]
        public string SteamId { get; set; }
    }
}