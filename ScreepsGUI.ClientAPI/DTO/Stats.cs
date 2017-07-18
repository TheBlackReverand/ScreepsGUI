using Newtonsoft.Json;
using ScreepsGUI.ClientAPI.DTO.Enum;
using System.Collections.Generic;

namespace ScreepsGUI.ClientAPI.DTO
{
    public class Stats : Dictionary<StatName, List<Stat>>
    {
        [JsonProperty("energyHarvested")]
        public List<Stat> EnergyHarvested
        {
            get { return this[StatName.energyHarvested]; }
            set { this[StatName.energyHarvested] = value; }
        }

        [JsonProperty("energyConstruction")]
        public List<Stat> EnergyConstruction
        {
            get { return this[StatName.energyConstruction]; }
            set { this[StatName.energyConstruction] = value; }
        }

        [JsonProperty("energyCreeps")]
        public List<Stat> EnergyCreeps
        {
            get { return this[StatName.energyCreeps]; }
            set { this[StatName.energyCreeps] = value; }
        }

        [JsonProperty("energyControl")]
        public List<Stat> EnergyControl
        {
            get { return this[StatName.energyControl]; }
            set { this[StatName.energyControl] = value; }
        }

        [JsonProperty("creepsProduced")]
        public List<Stat> CreepsProduced
        {
            get { return this[StatName.creepsProduced]; }
            set { this[StatName.creepsProduced] = value; }
        }

        [JsonProperty("creepsLost")]
        public List<Stat> CreepsLost
        {
            get { return this[StatName.creepsLost]; }
            set { this[StatName.creepsLost] = value; }
        }

        [JsonProperty("powerProcessed")]
        public List<Stat> PowerProcessed
        {
            get { return this[StatName.powerProcessed]; }
            set { this[StatName.powerProcessed] = value; }
        }
    }
}