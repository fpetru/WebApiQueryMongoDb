using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiQueryMongoDb.Interfaces;
using WebApiQueryMongoDb.Model;
using WebApiQueryMongoDb.Infrastructure;
using System.Collections.Generic;

namespace WebApiQueryMongoDb.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TravelQueryController : Controller
    {
        private readonly ITravelQueryRepository _travelItemRepository;

        public TravelQueryController(ITravelQueryRepository travelItemRepository)
        {
            _travelItemRepository = travelItemRepository;
        }

        [NoCache]
        [HttpGet]
        // GET api/TravelQuery
        public async Task<IEnumerable<object>> Get()
        {
            return await _travelItemRepository.GetTravelItems();
        }

        // GET api/TravelQuery/Paris?doAction=do
        [NoCache]
        [HttpGet("{city}")]
        public async Task<IEnumerable<TravelItem>> Get(string city, [FromQuery]string doAction)
        {
            return await _travelItemRepository.GetTravelItems(city, doAction);
        }
    }
}
