using Newtonsoft.Json;
using ScreepsGUI.Tools.Json.Attributes;
using ScreepsGUI.Tools.Json.Converters;
using ScreepsGUI.Tools.MVVM;

namespace ScreepsGUI.ClientAPI.DTO
{
    [JsonConverter(typeof(JsonPathConverter))]
    public abstract class BaseAccount : ViewModelBase
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("badge")]
        public Badge Badge { get; set; }
    }
}