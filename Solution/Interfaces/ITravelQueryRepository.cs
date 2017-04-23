using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiQueryMongoDb.Model;
using MongoDB.Driver;
using System;

namespace WebApiQueryMongoDb.Interfaces
{
    public interface ITravelQueryRepository
    {
        Task<IEnumerable<TravelItem>> GetTravelItems();
        Task<IEnumerable<TravelItem>> GetTravelItems(string cityName, string action);
        Task<IEnumerable<object>> GroupTravelItems(string cityName);
    }
}
