using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using WebApiQueryMongoDb.Interfaces;
using WebApiQueryMongoDb.Model;
using MongoDB.Bson;

namespace WebApiQueryMongoDb.Data
{
    public class TravelQueryRepository : ITravelQueryRepository
    {
        private readonly TravelQueryContext _context = null;

        public TravelQueryRepository(IOptions<Settings> settings)
        {
            _context = new TravelQueryContext(settings);
        }

        public async Task<IEnumerable<TravelItem>> GetAllTravelItems()
        {
            try
            {
                return await _context.TravelItems
                    .Take(500).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<TravelItem> GetTravelItem(string id)
        {
            try
            {
                // TODO: This is not correct
                //return await _context.TravelItems
                //                .Where(p => p.Id == id)
                //                .FirstOrDefaultAsync();

                return null;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

    }
}
