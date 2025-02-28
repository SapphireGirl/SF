using SF.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SF.Data.Services
{
    class DapperServices : IDapperServices
    {
        private readonly IHomeRepository houseRepository;
        public DapperServices(IHomeRepository houseRepository)
        {
            this.houseRepository = houseRepository;
        }


    }
}
