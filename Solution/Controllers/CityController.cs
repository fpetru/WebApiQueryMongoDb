using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiQueryMongoDb.Interfaces;
using WebApiQueryMongoDb.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace WebApiQueryMongoDb.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly ITravelQueryRepository _travelItemRepository;
        public CityController(ITravelQueryRepository travelItemRepository)
        {
            _travelItemRepository = travelItemRepository;
        }

        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<object>> Get(string countryCode, int? population, string lastId)
        {
            IEnumerable<object> list = await _travelItemRepository
                            .GetCitiesLinq(countryCode, lastId, population ?? 0);
            return list;
        }
    }
}
