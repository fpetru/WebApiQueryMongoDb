using Microsoft.Extensions.Options;
using MongoDB.Driver;
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

        public IMongoCollection<TravelItem> TravelItems
        {
            get
            {
                return _database.GetCollection<TravelItem>("WikiVoyage");
            }
        }
    }
}
