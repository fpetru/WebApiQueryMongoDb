using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApiQueryMongoDb.Model
{
    public class CityExtended
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string[] AlternateNames { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public long Population { get; set; }
        public int DigitalElevationModel { get; set; }
        public string TimeZone { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
    }
}
