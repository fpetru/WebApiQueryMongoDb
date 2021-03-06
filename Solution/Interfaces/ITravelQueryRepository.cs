﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiQueryMongoDb.Model;

namespace WebApiQueryMongoDb.Interfaces
{
    public interface ITravelQueryRepository
    {
        Task<IEnumerable<TravelItem>> GetTravelItems();
        Task<IEnumerable<TravelItem>> GetTravelItems(string cityName, string action);
        Task<IEnumerable<object>> GroupTravelItems(string cityName);

        Task<IEnumerable<object>> GetCitiesInitial(string countryCode, int minPopulation = 0);
        Task<IEnumerable<object>> GetCities(string countryCode, string lastBsonId, int minPopulation = 0);
        Task<object> GetCitiesLinq(string countryCode, string lastBsonId, int minPopulation = 0);

        Task<IEnumerable<CityExtended>> GetCityExtendedList();

        Task<IEnumerable<object>> GetTravelItemStat();
        Task<IEnumerable<object>> GetTravelItemsOfCityAsync(string cityName);
        Task<IEnumerable<object>> GetTravelDestinations(string cityName);
    }
}
