using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using WebApiQueryMongoDb.Interfaces;
using WebApiQueryMongoDb.Model;

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
                return await _context.TravelItems.Take(500).ToListAsync();
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
                    return await _context.TravelItems
                        .Where(p => p.City == cityName && p.Action == action).ToListAsync();

                return await _context.TravelItems.Where(p => p.City == cityName)
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
                return await _context.TravelItems
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
            try
            {
                return await _context.Cities
                                    .Where(p => p.CountryCode == "FR" && p.Population >= 15000)
                                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
