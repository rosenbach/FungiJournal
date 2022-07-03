namespace FungiJournal.Domain.Models.APIClients
{
    public class WMSRequest
    {
        public string Scheme { get; set; } = "https";
        public string Host { get; set; } = "lfu.bayern.de";
        public string Path { get; set; } = "/gdi/wms/natur/schutzgebiete";
        public string Request { get; set; } = "GetFeatureInfo";
        public string Service { get; set; } = "WMS";
        public string Version { get; set; } = "1.3.0";
        public string BBox { get; set; } = "47.3835,10.87069441716961116,47.72168968365326691,11.04317273279461098";
        public string Crs { get; set; } = "EPSG:4326";
        public int Width { get; set; } = 328;
        public int Height { get; set; } = 643;
        public string Layers { get; set; } = "naturschutzgebiet";
        public string Styles { get; set; } = string.Empty;
        public string Format { get; set; } = "image/jpeg";
        public string QueryLayers { get; set; } = "naturschutzgebiet";
        public string InfoFormat { get; set; } = "application/geojson";
        public int I { get; set; } = 199;
        public int J { get; set; } = 346;
        public int FeatureCount { get; set; } = 10;

        public Uri Uri
        {
            get
            {
                UriBuilder uriBuilder = new()
                {
                    Scheme = Scheme,
                    Host = Host,
                    Path = Path
                };
                uriBuilder.AddParameter("SERVICE", Service);
                uriBuilder.AddParameter("VERSION", Version);
                uriBuilder.AddParameter("REQUEST", Request);
                uriBuilder.AddParameter("BBOX", BBox);
                uriBuilder.AddParameter("CRS", Crs);
                uriBuilder.AddParameter("WIDTH", Width.ToString());
                uriBuilder.AddParameter("HEIGHT", Height.ToString());
                uriBuilder.AddParameter("LAYERS", Layers);
                uriBuilder.AddParameter("STYLES", Styles);
                uriBuilder.AddParameter("FORMAT", Format);
                uriBuilder.AddParameter("QUERY_LAYERS", QueryLayers);
                uriBuilder.AddParameter("INFO_FORMAT", InfoFormat);
                uriBuilder.AddParameter("I", I.ToString());
                uriBuilder.AddParameter("J", J.ToString());
                uriBuilder.AddParameter("FEATURE_COUNT", FeatureCount.ToString());

                return uriBuilder.Uri;
            }
        }
    }
}
