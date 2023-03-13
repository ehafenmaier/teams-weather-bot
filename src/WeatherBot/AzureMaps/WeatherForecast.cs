using System.Text.Json.Serialization;

namespace WeatherBot.AzureMaps
{
    public class WeatherHourlyForecast
    {
        [JsonPropertyName("forecasts")]
        public List<WeatherForecast> Forecasts { get; set; }
    }

    public class WeatherForecast
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("iconCode")]
        public int IconCode { get; set; }

        [JsonPropertyName("iconPhrase")]
        public string IconPhrase { get; set; }

        [JsonPropertyName("hasPrecipitation")]
        public bool HasPrecipitation { get; set; }

        [JsonPropertyName("isDaylight")]
        public bool IsDaylight { get; set; }

        [JsonPropertyName("temperature")]
        public WeatherUnit Temperature { get; set; }

        [JsonPropertyName("realFeelTemperature")]
        public WeatherUnit RealFeelTemperature { get; set; }

        [JsonPropertyName("wetBulbTemperature")]
        public WeatherUnit WetBulbTemperature { get; set; }

        [JsonPropertyName("dewPoint")]
        public WeatherUnit DewPoint { get; set; }

        [JsonPropertyName("wind")]
        public WeatherWind Wind { get; set; }

        [JsonPropertyName("windGust")]
        public WeatherWind WindGust { get; set; }

        [JsonPropertyName("relativeHumidity")]
        public int RelativeHumidity { get; set; }

        [JsonPropertyName("visibility")]
        public WeatherUnit Visibility { get; set; }

        [JsonPropertyName("cloudCover")]
        public int CloudCover { get; set; }

        [JsonPropertyName("ceiling")]
        public WeatherUnit Ceiling { get; set; }

        [JsonPropertyName("uvIndex")]
        public int UVIndex { get; set; }

        [JsonPropertyName("uvIndexPhrase")]
        public string UVIndexPhrase { get; set; }

        [JsonPropertyName("precipitationProbability")]
        public int PrecipitationProbability { get; set; }

        [JsonPropertyName("rainProbability")]
        public int RainProbability { get; set; }

        [JsonPropertyName("snowProbability")]
        public int SnowProbability { get; set; }

        [JsonPropertyName("iceProbability")]
        public int IceProbability { get; set; }

        [JsonPropertyName("totalLiquid")]
        public WeatherUnit TotalLiquid { get; set; }

        [JsonPropertyName("rain")]
        public WeatherUnit Rain { get; set; }

        [JsonPropertyName("snow")]
        public WeatherUnit Snow { get; set; }

        [JsonPropertyName("ice")]
        public WeatherUnit Ice { get; set; }
    }
}

