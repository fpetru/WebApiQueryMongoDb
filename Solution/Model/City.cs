using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApiQueryMongoDb.Model
{
    public class City
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int GeoNameId { get; set; }
        public string Name { get; set; }
        public string AsciiName { get; set; }
        public string AlternateNames { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FeatureClass { get; set; }
        public string FeatureCode { get; set; }
        public string CountryCode { get; set; }
        public string AlternateCountryCodes { get; set; }
        public string Admin1Code { get; set; }
        public string Admin2Code { get; set; }
        public string Admin3Code { get; set; }
        public string Admin4Code { get; set; }
        public long Population { get; set; }
        public string Elevation { get; set; }
        public int DigitalElevationModel { get; set; }
        public string Timezone { get; set; }
        public string ModificationDate { get; set; }
    }
}
