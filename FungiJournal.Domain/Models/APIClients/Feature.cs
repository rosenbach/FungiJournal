namespace FungiJournal.Domain.Models.APIClients
{
    public class Feature
    {
        public String? Type { get; set; }
        public Object? Geometry { get; set; }
        public FeatureProperties? Properties { get; set; }
        public String? LayerName { get; set; }
    }

    public class FeatureProperties
    {
        public String? ID { get; set; }
        public String? Name { get; set; }
        public String? NR { get; set; }
        public String? Fläche_ha { get; set; }
        public String? Shape { get; set; }
    }

}

