using WebApiQueryMongoDb.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApiQueryMongoDb.Infrastructure;
using System.Threading.Tasks;

namespace WebApiQueryMongoDb.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DisplayController : Controller
    {
        private readonly ITravelQueryRepository _displayRepository;

        public DisplayController(ITravelQueryRepository displayRepository)
        {
            _displayRepository = displayRepository;
        }

        // GET api/Display/GroupBy?city=CityName
        [NoCache]
        [HttpGet("{type}")]
        public async Task<IActionResult> Get(string type, [FromQuery]string city)
        {
            if (!string.IsNullOrEmpty(city) && city.Length > 1) 
                return Ok(await _displayRepository.GetTravelDestinations(city));

            return NotFound();
        }
    }
}