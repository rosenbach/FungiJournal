using FungiJournal.Domain.Models.APIClients;
using System.Net;

namespace FungiJournal.API.APIClients
{
    public class BayernAtlasAPIClient
    {
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
