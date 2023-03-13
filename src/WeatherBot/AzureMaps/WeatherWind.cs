using System.Text.Json.Serialization;

namespace WeatherBot.AzureMaps
{
    public class WeatherWind
    {
        [JsonPropertyName("direction")]
        public WeatherWindDirection Direction { get; set; }

        [JsonPropertyName("speed")]
        public WeatherUnit Speed { get; set; }
    }

    public class WeatherWindDirection
    {
        [JsonPropertyName("degrees")]
        public double Degrees { get; set; }

        [JsonPropertyName("localizedDescription")]
        public string LocalizedDescription { get; set; }
    }
}
