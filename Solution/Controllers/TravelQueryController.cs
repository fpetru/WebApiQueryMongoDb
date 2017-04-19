using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiQueryMongoDb.Interfaces;
using WebApiQueryMongoDb.Model;
using WebApiQueryMongoDb.Infrastructure;
using System;
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
            return GetTravelItemInternal();
        }

        private async Task<IEnumerable<TravelItem>> GetTravelItemInternal()
        {
            return await _travelItemRepository.GetAllTravelItems();
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public Task<TravelItem> Get(string id)
        {
            return GetTravelItemByIdInternal(id);
        }

        private async Task<TravelItem> GetTravelItemByIdInternal(string id)
        {
            return await _travelItemRepository.GetTravelItem(id) ?? new TravelItem();
        }
    }
}
