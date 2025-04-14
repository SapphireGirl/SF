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
        private readonly IRepository<Home> _homeRepository;

        public HomeController(IRepository<Home> homeRepository, Serilog.ILogger logger)
        {            
            _homeRepository = homeRepository;
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

        [HttpPost]
        public async Task<Home> Insert([FromBody] Home home)
        {
            try
            {
                _logger.Information($"Insert: {home}");
                return await _homeRepository.InsertAsync(home);

            }
            catch (Exception ex)
            {
                _logger.Error("Error in Insert method", ex);
                return new Home();
            }
        }

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
                _logger.Error("Error in Put method", ex);
                return new Home();
            }
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(Home home)
        {
            try
            {
                _logger.Information($"Delete: {home}");
                return await _homeRepository.DeleteAsync(home.ID);

            }
            catch (Exception ex)
            {
                _logger.Error("Error in Delete method", ex);
                return 0;
            }
        }
    }
}
