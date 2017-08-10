using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiQueryMongoDb.Interfaces;
using WebApiQueryMongoDb.Infrastructure;
using WebApiQueryMongoDb.Model;

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

        // GET: api/values
        [NoCache]
        [HttpGet]
        public Task<IEnumerable<object>> Get()
        {
            return GetCitiesInternal();
        }

        private async Task<IEnumerable<object>> GetCitiesInternal()
        {
            return await _travelItemRepository.GetCitiesLinq2("FR", "");
        }
    }
}
