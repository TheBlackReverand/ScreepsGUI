using Newtonsoft.Json;
using ScreepsGUI.Tools.Json.Attributes;
using ScreepsGUI.Tools.Json.Converters;

namespace ScreepsGUI.ClientAPI.DTO
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class OwnerAccount : BaseAccount
    {

    }
}