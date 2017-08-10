using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WebApiQueryMongoDb.Model;

namespace WebApiQueryMongoDb.Data
{
    public class TravelQueryContext
    {
        private readonly IMongoDatabase _database = null;

        public TravelQueryContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoQueryable<TravelItem> TravelItemsLinq
        {
            get
            {
                return _database.GetCollection<TravelItem>("WikiVoyage").AsQueryable<TravelItem>();
            }
        }

        public IMongoCollection<TravelItem> TravelItems
        {
            get
            {
                return _database.GetCollection<TravelItem>("WikiVoyage");
            }
        }

        public IMongoQueryable<City> CitiesLinq
        {
            get
            {
                return _database.GetCollection<City>("Cities").AsQueryable<City>();
            }
        }

        public IMongoCollection<City> Cities
        {
            get
            {
                return _database.GetCollection<City>("Cities");
            }
        }
    }
}
