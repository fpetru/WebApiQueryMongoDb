using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using WebApiQueryMongoDb.Interfaces;
using WebApiQueryMongoDb.Model;
using MongoDB.Bson;
using System.Linq;
using System.Linq.Expressions;

namespace WebApiQueryMongoDb.Data
{
    public class TravelQueryRepository : ITravelQueryRepository
    {
        private readonly TravelQueryContext _context = null;

        public TravelQueryRepository(IOptions<Settings> settings)
        {
            _context = new TravelQueryContext(settings);
        }

        public async Task<IEnumerable<TravelItem>> GetTravelItems()
        {
            try
            {
                return await _context.TravelItemsLinq.Take(500).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<TravelItem>> GetTravelItems(string cityName, string action)
        {
            try
            {
                if (action != null)
                    return await _context.TravelItemsLinq
                        .Where(p => p.City == cityName && p.Action == action).ToListAsync();

                return await _context.TravelItemsLinq.Where(p => p.City == cityName)
                    .OrderBy(p => p.Action)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<object>> GroupTravelItems(string cityName)
        {
            try
            {
                return await _context.TravelItemsLinq
                                    .Where(p => p.City == cityName)
                                    .GroupBy(grp => new { grp.City, grp.Action })
                                    .Select(g => new { g.Key.City, g.Key.Action }).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<object>> GetCitiesInitial(string countryCode, int minPopulation = 0)
        {
            try
            {
                var filterBuilder = Builders<City>.Filter;
                var filter = filterBuilder.Eq(x => x.CountryCode, countryCode)
                                & filterBuilder.Gte(x => x.Population, minPopulation);

                var query = _context.Cities.Find(filter)
                                            .SortByDescending(p => p.Id)
                                            .Limit(200);
                var items = await query.ToListAsync();

                return items.Select(x => new
                {
                    BsonId = x.Id.ToString(),
                    Timestamp = x.Id.Timestamp,
                    ServerUpdatedOn = x.Id.CreationTime,
                    x.Name,
                    x.CountryCode,
                    x.Population
                });
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<object>> GetCities(string countryCode, string lastBsonId, int minPopulation = 0)
        {
            try
            {
                var filterBuilder = Builders<City>.Filter;
                var filter = filterBuilder.Eq(x => x.CountryCode, countryCode)
                                & filterBuilder.Gte(x => x.Population, minPopulation)
                                & filterBuilder.Lte(x => x.Id, ObjectId.Parse(lastBsonId));

                var query = _context.Cities.Find(filter)
                                            .SortByDescending(p => p.Id)
                                            .Limit(200);

                var items = await query.ToListAsync();

                return items.Select(x => new
                {
                    BsonId = x.Id.ToString(),
                    Timestamp = x.Id.Timestamp,
                    ServerUpdatedOn = x.Id.CreationTime,
                    x.Name,
                    x.CountryCode,
                    x.Population
                });
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // building where clause
        //
        private Expression<Func<City, bool>> GetConditions(string countryCode, string lastBsonId, int minPopulation = 0)
        {
            Expression<Func<City, bool>> conditions = (x => x.CountryCode == countryCode
                                                        && x.Population >= minPopulation);

            ObjectId id;
            if (string.IsNullOrEmpty(lastBsonId) && ObjectId.TryParse(lastBsonId, out id))
            {
                conditions = (x => x.CountryCode == countryCode
                                && x.Population >= minPopulation
                                && x.Id < id);
            }

            return conditions;

        }

        public async Task<object> GetCitiesLinq(string countryCode, string lastBsonId, int minPopulation = 0)
        {

            try
            {
                var items = await _context.CitiesLinq
                                    .Where(GetConditions(countryCode, lastBsonId, minPopulation))
                                    .OrderByDescending(x => x.Id)
                                    .Take(200)
                                    .ToListAsync();

                // select just few elements
                var returnItems = items.Select(x => new
                                    {
                                        BsonId = x.Id.ToString(),
                                        Timestamp = x.Id.Timestamp,
                                        ServerUpdatedOn = x.Id.CreationTime,
                                        x.Name,
                                        x.CountryCode,
                                        x.Population
                                    });

                int countItems = await _context.CitiesLinq
                                    .Where(GetConditions(countryCode, "", minPopulation))
                                    .CountAsync();


                return new
                    {
                        count = countItems,
                        items = returnItems
                    };
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<CityExtended>> GetCityExtendedList()
        {
            try
            {
                return await _context.CityExtendedLinq
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<object>> GetTravelItemsOfCityAsync(string cityName)
        {
            var query = from travelItem in _context.TravelItemsLinq
                        join city in _context.CityExtendedLinq
                           on travelItem.City equals city.Name
                        into joinedItem
                        where (travelItem.City == cityName)
                        select new
                        {
                            Action = travelItem.Action,
                            Name = travelItem.Name,
                            FirstCityMatched = joinedItem.First(),
                        };

            return await query.Take(10).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetTravelStat()
        {
            var groupTravelItemsByCityAndAction = _context.TravelItemsLinq
                        .GroupBy(s => new { s.City, s.Action })
                        .Select(n => new
                        {
                            Location = n.Key,
                            Count = n.Count(),
                            Latitude = n.Max(p=> p.Latitude),
                            Longitude = n.Max(p => p.Longitude)
                        });
            return await groupTravelItemsByCityAndAction.ToListAsync();
        }
    }
}
