using Newtonsoft.Json;

namespace ScreepsGUI.ClientAPI.DTO
{
    public class Stat
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("endTime")]
        public int EndTime { get; set; }

        public override string ToString()
        {
            return "At " + EndTime + " : " + Value;
        }
    }
}