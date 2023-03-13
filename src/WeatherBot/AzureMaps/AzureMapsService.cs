using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static Microsoft.Graph.Constants;

namespace WeatherBot.AzureMaps
{
    #region Interface
    public interface IAzureMapsService
    {
        Task<LatLong> GetLocationByAddressPartsAsync(AddressParts addressParts);
        Task<string> GetWeatherForLocationAsync(LatLong location);
    }
    #endregion

    public class AzureMapsService : IAzureMapsService
    {
        #region Private Variables
        private readonly ILogger<AzureMapsService> _logger;
        private readonly AzureMapsOptions _mapsOptions;
        #endregion

        #region Constructors
        public AzureMapsService(ILogger<AzureMapsService> logger, IOptions<AzureMapsOptions> mapsOptions)
        {
            _logger = logger;
            _mapsOptions = mapsOptions.Value;
        }
        #endregion

        #region Public Methods
        public async Task<LatLong> GetLocationByAddressPartsAsync(AddressParts addressParts)
        {
            var latlong = new LatLong();
            var url = GetSearchUrlString(addressParts);

            if (url == null)
                return null;

            try
            {
                using var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_mapsOptions.BaseAddressGeocode);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var results = JsonSerializer.Deserialize<AzureMapSearchResults>(content);
                    var entity = results.Results.FirstOrDefault(r => r.EntityType == "Municipality");

                    latlong.Latitude = (double)entity.Position.Lat;
                    latlong.Longitude = (double)entity.Position.Lon;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{message}", ex.Message);
                return null;
            }

            return latlong;
        }

        public async Task<string> GetWeatherForLocationAsync(LatLong location)
        {
            var url = GetHourlyForecastUrlString(location);
            
            try
            {
                using var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_mapsOptions.BaseAddressWeather);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var hourlyForecast = JsonSerializer.Deserialize<WeatherHourlyForecast>(content);

                    return content;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{message}", ex.Message);
                return null;
            }
        }
        #endregion

        #region Private Methods
        private string GetSearchUrlString(AddressParts addressParts)
        {
            if (addressParts == null)
                return null;

            var sb = new StringBuilder();

            sb.AppendFormat("?subscription-key={0}&api-version=1.0&language=en-US&query=", _mapsOptions.APIKey);

            if (!string.IsNullOrWhiteSpace(addressParts.StreetAddress))
                sb.AppendFormat("{0},", addressParts.StreetAddress);
            if (!string.IsNullOrWhiteSpace(addressParts.City))
                sb.AppendFormat("{0},", addressParts.City);
            if (!string.IsNullOrWhiteSpace(addressParts.StateProvince))
                sb.AppendFormat("{0} ", addressParts.StateProvince);
            if (!string.IsNullOrWhiteSpace(addressParts.CountyParish))
                sb.AppendFormat("{0},", addressParts.CountyParish);
            if (!string.IsNullOrWhiteSpace(addressParts.Country))
                sb.AppendFormat("{0},", addressParts.Country);

            if (sb.Length == 0)
                return null;

            return sb.ToString()[..^1];
        }

        private string GetCurrentConditionsUrlString(LatLong location)
        {
            if (location == null)
                return null;

            var sb = new StringBuilder();

            sb.AppendFormat("currentConditions/json?subscription-key={0}", _mapsOptions.APIKey);
            sb.AppendFormat("&api-version=1.1&query={0},{1}&unit=imperial", location.Latitude, location.Longitude);

            return sb.ToString();
        }

        private string GetHourlyForecastUrlString(LatLong location)
        {
            if (location == null)
                return null;

            var sb = new StringBuilder();

            sb.AppendFormat("forecast/hourly/json?subscription-key={0}", _mapsOptions.APIKey);
            sb.AppendFormat("&api-version=1.1&query={0},{1}&unit=imperial&duration=12", location.Latitude, location.Longitude);

            return sb.ToString();
        }
        #endregion
    }
}
