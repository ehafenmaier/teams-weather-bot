using System.Text.Json.Serialization;

namespace WeatherBot.AzureMaps
{
    public class AzureMapSearchResults
    {
        [JsonPropertyName("summary")]
        public SearchResultsSummary Summary { get; set; }
        [JsonPropertyName("results")]
        public List<SearchResult> Results { get; set; }
    }

    public class SearchResultsSummary
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }
        [JsonPropertyName("queryType")]
        public string QueryType { get; set; }
        [JsonPropertyName("queryTime")]
        public int? QueryTime { get; set; }
        [JsonPropertyName("numResults")]
        public int? NumResults { get; set; }
        [JsonPropertyName("offset")]
        public int? Offset { get; set; }
        [JsonPropertyName("totalResults")]
        public int? TotalResults { get; set; }
        [JsonPropertyName("fuzzyLevel")]
        public int? FuzzyLevel { get; set; }
    }

    public class SearchResult
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("score")]
        public double? Score { get; set; }
        [JsonPropertyName("entityType")]
        public string EntityType { get; set; }
        [JsonPropertyName("matchConfidence")]
        public SearchResultMatchConfidence MatchConfidence { get; set; }
        [JsonPropertyName("address")]
        public SearchResultAddress Address { get; set; }
        [JsonPropertyName("position")]
        public SearchResultPosition Position { get; set; }
        [JsonPropertyName("viewport")]
        public SearchResultViewport Viewport { get; set; }
        [JsonPropertyName("boundingBox")]
        public SearchResultBoundingBox BoundingBox { get; set; }
        [JsonPropertyName("dataSources")]
        public SearchResultDataSources DataSources { get; set; }
    }

    public class SearchResultMatchConfidence
    {
        [JsonPropertyName("score")]
        public double? Score { get; set; }
    }

    public class SearchResultAddress
    {
        [JsonPropertyName("municipality")]
        public string Municipality { get; set; }
        [JsonPropertyName("countrySecondarySubdivision")]
        public string CountrySecondarySubdivision { get; set; }
        [JsonPropertyName("countrySubdivision")]
        public string CountrySubdivision { get; set; }
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("countryCodeISO3")]
        public string CountryCodeISO3 { get; set; }
        [JsonPropertyName("freeformAddress")]
        public string FreeformAddress { get; set; }
    }

    public class SearchResultPosition
    {
        [JsonPropertyName("lat")]
        public double? Lat { get; set; }
        [JsonPropertyName("lon")]
        public double? Lon { get; set; }
    }

    public class SearchResultViewport
    {
        [JsonPropertyName("topLeftPoint")]
        public TopLeftPoint TopLeftPoint { get; set; }
        [JsonPropertyName("btmRightPoint")]
        public BottomRightPoint BtmRightPoint { get; set; }
    }

    public class SearchResultBoundingBox
    {
        [JsonPropertyName("topLeftPoint")]
        public TopLeftPoint TopLeftPoint { get; set; }
        [JsonPropertyName("btmRightPoint")]
        public BottomRightPoint BtmRightPoint { get; set; }
    }

    public class SearchResultDataSources
    {
        [JsonPropertyName("geometry")]
        public SearchResultGeometry Geometry { get; set; }
    }

    public class SearchResultGeometry
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
    }

    public class TopLeftPoint
    {
        [JsonPropertyName("lat")]
        public double? Lat { get; set; }
        [JsonPropertyName("lon")]
        public double? Lon { get; set; }
    }

    public class BottomRightPoint
    {
        [JsonPropertyName("lat")]
        public double? Lat { get; set; }
        [JsonPropertyName("lon")]
        public double? Lon { get; set; }
    }
}
