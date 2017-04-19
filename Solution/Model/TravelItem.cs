using System;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiQueryMongoDb.Model
{
    public class TravelItem
    {
        [BsonId]
        public Object Id { get; set; }
        public string City { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public string AlternativeName { get; set; }
        public Object Address { get; set; }
        public string Directions { get; set; }
        public string Phone { get; set; }
        public string Tollfree { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Url { get; set; }
        public string OpeningHours { get; set; }
        public Object Checkin { get; set; }
        public Object Checkout { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        public Object Latitude { get; set; }
        public Object Longitude { get; set; }
        public string Description { get; set; }
    }
}
