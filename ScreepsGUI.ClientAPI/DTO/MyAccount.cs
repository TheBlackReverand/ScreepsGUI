using Newtonsoft.Json;
using ScreepsGUI.Tools.Json.Attributes;
using ScreepsGUI.Tools.Json.Converters;

namespace ScreepsGUI.ClientAPI.DTO
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class MyAccount : BaseAccount
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("cpu")]
        public int CPU { get; set; }

        [JsonProperty("password")]
        public bool UsePassword { get; set; }

        [JsonProperty("credits")]
        public int Credits { get; set; }

        [JsonProperty("money")]
        public int Money { get; set; }

        [JsonProperty("subscriptionTokens")]
        public int SubscriptionTokens { get; set; }


        [JsonPath("steam.id")]
        public string SteamId { get; set; }
        
        [JsonPath("steam.displayName")]
        public string SteamDisplayName { get; set; }
    }
}