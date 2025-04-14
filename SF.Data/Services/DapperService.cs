using SF.Data.Repositories;
using SF.Model;

namespace SF.Data.Services
{
    class DapperServices : IDapperServices
    {
        private readonly IHomeRepository<Home> houseRepository;
        public DapperServices(IHomeRepository<Home> houseRepository)
        {
            this.houseRepository = houseRepository;
        }
    }
}
