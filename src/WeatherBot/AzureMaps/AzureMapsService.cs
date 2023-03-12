using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WeatherBot.AzureMaps
{
    #region Interface
    public interface IAzureMapsService
    {
        Task<LatLong> GetLocationByAddressPartsAsync(AddressParts addressParts);
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
                httpClient.BaseAddress = new Uri(_mapsOptions.BaseAddress);
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
        #endregion
    }
}
