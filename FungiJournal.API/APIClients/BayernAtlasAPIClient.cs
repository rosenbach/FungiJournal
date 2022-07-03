using FungiJournal.Domain.Models.APIClients;
using System.Net;

namespace FungiJournal.API.APIClients
{
    public class BayernAtlasAPIClient
    {
        public Uri GetFeatureInfo(double lat, double lng)
        {
            WMSRequest request = new()
            {
                BBox = $"{lng},{lat},{lng-0.0001},{lat-0.0001}"
            };
            
            return request.Uri;
        }

        public async Task<bool> IsNatureReservoir(double lat, double lng)
        {
            WMSRequest request = new()
            {
                Request = "GetFeatureInfo",
                BBox =
                $"{lat},{lng},{lat+0.34},{lng+0.37}"
            };

            HttpClient httpClient = new();

            GetFeatureInfoResponse result = await httpClient.GetFromJsonAsync<GetFeatureInfoResponse>(request.Uri);
            
            return (result.Features.Length>0);
        }
    }
}
