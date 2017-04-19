using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiQueryMongoDb.Model;
using MongoDB.Driver;

namespace WebApiQueryMongoDb.Interfaces
{
    public interface ITravelQueryRepository
    {
        Task<IEnumerable<TravelItem>> GetAllTravelItems();
        Task<TravelItem> GetTravelItem(string id);
    }
}
