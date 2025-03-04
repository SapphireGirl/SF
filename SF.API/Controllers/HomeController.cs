using Microsoft.AspNetCore.Mvc;
using SF.Model;
using SF.Data.Repositories;
using Serilog;
using Seq.Extensions.Logging;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SF.API.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        //private readonly Serilog.ILogger _logger;
        private readonly Serilog.ILogger _log = Log.ForContext<HomeRepository>();
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
           // _logger = logger;
            _homeRepository = homeRepository;
            _log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
        }

        [HttpGet]
        [Route("GetAllAsync")]
        public async Task<IEnumerable<Home>> GetAllAsync()
        {
            try
            {
                _log.Information("GetAll method called in HomeController");
                var homes = await _homeRepository.GetAllAsync();
                _log.Information("After GetAllAsync");

                return homes.ToList<Home>();

            }
            catch (Exception ex)
            {
                _log.Error("Error in Get method", ex);
                return Enumerable.Empty<Home>();
            }

        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
