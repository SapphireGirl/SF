using Microsoft.AspNetCore.Mvc;
using SF.Model;
using SF.Data.Repositories;
using Serilog;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SF.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        //private readonly Serilog.ILogger _log = Log.ForContext<HomeRepository>();
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository, Serilog.ILogger logger)
        {
            // _logger = logger;
            _homeRepository = homeRepository;
            //_log = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.Seq("http://localhost:5341")
            //    .CreateLogger();
            _logger = logger.ForContext<HomeController>();
        }

        [HttpGet]
        [Route("GetAllAsync")]
        public async Task<IEnumerable<Home>> GetAllAsync()
        {
            try
            {
                _logger.Information("GetAllAsync");
                var homes = await _homeRepository.GetAllAsync();
                _logger.Information("After GetAllAsync");

                return homes.ToList<Home>();

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetAllAsync method", ex);
                return Enumerable.Empty<Home>();
            }

        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public async Task<Home> GetById(int id)
        {
            try
            {
                _logger.Information($"GetById: {id}");
                return await _homeRepository.GetByIdAsync(id);

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetById method", ex);
                return new Home();
            }
        }

        // POST api/<HomeController>
        [HttpPost]
        public async Task<Home> Insert([FromBody] Home home)
        {
            try
            {
                _logger.Information($"GetById: {home}");
                return await _homeRepository.InsertAsync(home);

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetById method", ex);
                return new Home();
            }
        }

        // PUT api/<HomeController>/5
        [HttpPut("{Home}")]
        public async Task<Home> Put([FromBody] Home home)
        {
            try
            {
                _logger.Information($"Put: {home}");
                return await _homeRepository.InsertAsync(home);

            }
            catch (Exception ex)
            {
                _logger.Error("Error in GetById method", ex);
                return new Home();
            }
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
