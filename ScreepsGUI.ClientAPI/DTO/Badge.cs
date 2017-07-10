using Newtonsoft.Json;
using ScreepsGUI.Tools.Json.Attributes;
using ScreepsGUI.Tools.Json.Converters;
using ScreepsGUI.Tools.MVVM;

namespace ScreepsGUI.ClientAPI.DTO
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Badge : ViewModelBase
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("color1")]
        public string Color1 { get; set; }

        [JsonProperty("color2")]
        public string Color2 { get; set; }

        [JsonProperty("color3")]
        public string Color3 { get; set; }

        [JsonProperty("param")]
        public int Param { get; set; }

        [JsonPath("flip")]
        public bool Flip { get; set; }
    }
}