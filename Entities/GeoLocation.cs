namespace HomelessAPI.Entities
{
    public struct GeoLocation(double latitude, double longitude)
    {
        public double Latitude { get; set; } = latitude;
        public double Longitude { get; set; } = longitude;
    }
}
