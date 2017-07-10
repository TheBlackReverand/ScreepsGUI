using Newtonsoft.Json;
using ScreepsGUI.Tools.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreepsGUI.ClientAPI.DTO
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class RoomOverview
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("owner")]
        public OwnerAccount Owner { get; set; }
    }  
}