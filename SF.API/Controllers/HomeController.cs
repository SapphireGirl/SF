using Microsoft.AspNetCore.Mvc;
using SF.Model;
using SF.Data;
using SF.Logger;
using SF.Data.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SF.API.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ISFLogger _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ISFLogger logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        // GET: api/<HomeController>

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Home>> GetAll()
        {
            try
            {
                var homes = await _homeRepository.GetAllAsync();

                return homes;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Get method", ex);
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
