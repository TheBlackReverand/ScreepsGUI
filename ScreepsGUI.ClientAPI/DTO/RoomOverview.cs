using Newtonsoft.Json;
using ScreepsGUI.Tools.Json.Converters;

namespace ScreepsGUI.ClientAPI.DTO
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class RoomOverview
    {
        [JsonProperty("owner")]
        public OwnerAccount Owner { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }
    }  
}