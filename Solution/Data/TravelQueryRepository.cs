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

        public async Task<IEnumerable<City>> GetCities(string countryCode, int minPopulation = 0)
        {
            var sort = Builders<City>.Sort.Ascending("Name");
            var options = new FindOptions<City, City>
            {
                // Get 200 docs at a time
                BatchSize = 200,
                Sort = sort
            };

            try
            {
                using (var cursor = await _context.Cities.FindAsync(x => x.CountryCode == countryCode 
                                                                       && x.Population >= minPopulation
                                                                   , options))
                {
                    // Retrieve just first batch of docs
                    if (await cursor.MoveNextAsync())
                    {
                        return cursor.Current;
                    }
                }
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

            return new List<City>();
        }

        public async Task<IEnumerable<City>> GetCitiesLinq(string countryCode, string lastId, int minPopulation = 0)
        {
            // ObjectId lastBsonId = new ObjectId(lastId);

            try
            {
                List<City> items = await _context.CitiesLinq
                                    .Where(x => x.CountryCode == countryCode
                                             && x.Population >= minPopulation)
                                    /*&& x.Id == ObjectId.Parse("58fc8ae631a8a6f8d000f9c3"*/
                                    /*&& x.Id >= lastBsonId*/
                                    // .OrderByDescending(x => x.Id)    
                                    // .Take(200)
                                    // .Select()
                                    .ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<object>> GetCitiesLinq2(string countryCode, string lastId, int minPopulation = 0)
        {
            try
            {
                var items = await _context.CitiesLinq
                                    .Where(x => x.CountryCode == countryCode
                                             && x.Population >= minPopulation
                                             && x.Id >= ObjectId.Parse("58fc8ae631a8a6f8d000f9c3"))
                                    .OrderByDescending(x => x.Id)
                                    .Take(1)
                                    .ToListAsync();

                return items.Select(x => new
                                    {
                                        Id3 = x.Id.ToString(),
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
    }
}
