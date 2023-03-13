using System.Text.Json.Serialization;

namespace WeatherBot.AzureMaps
{
    public class WeatherUnit
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("unitType")]
        public int UnitType { get; set; }
    }
}
