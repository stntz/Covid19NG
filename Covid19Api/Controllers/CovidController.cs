using System.Linq;
using System.Threading.Tasks;
using Covid19NG.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Covid19NG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CovidController : ControllerBase
    {
        
        private readonly ILogger<CovidController> _logger;

        public CovidController(ILogger<CovidController> logger)
        {
            _logger = logger;
        }        


        [HttpGet]
        public async Task<IActionResult> GetCovidCases([FromQuery]string state)
        {
            var scraper = new DataScraper();

            //TODO: Cache this Data and only run scraping when absolutely necessary
            var result = await scraper.ScrapeData();
            if(string.IsNullOrWhiteSpace(state))
                return Ok(result);

            var stateData = result.Where(x => x.State.Equals(state, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            return Ok(stateData);
        }
    }
}