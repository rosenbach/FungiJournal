namespace FungiJournal.Domain.Models.APIClients
{
    public class GetFeatureInfoResponse
    {
        public string Type { get; set; } = "FeatureCollection";
        public Feature[] Features { get; set; }
    }
}
