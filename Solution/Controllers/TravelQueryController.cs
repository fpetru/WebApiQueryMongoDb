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
        public Task<IEnumerable<TravelItem>> Get()
        {
            return GetTravelItemsInternal();
        }

        private async Task<IEnumerable<TravelItem>> GetTravelItemsInternal()
        {
            return await _travelItemRepository.GetTravelItems();
        }

        // GET api/TravelQuery/Paris?doAction=do
        [NoCache]
        [HttpGet("{city}")]
        public Task<IEnumerable<TravelItem>> Get(string city, [FromQuery]string doAction)
        {
            return GetTravelItemsInternal(city, doAction);
        }

        private async Task<IEnumerable<TravelItem>> GetTravelItemsInternal(string city, string action)
        {
            return await _travelItemRepository.GetTravelItems(city, action);
        }
    }
}
