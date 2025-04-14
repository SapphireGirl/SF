using SF.Model;
using SF.Data.Context;
using Serilog;
using System.Data;

namespace SF.Data.Repositories
{
    public class HomeRepository : GenericRepository<Home>
    {
        private readonly string _tableName = "Homes";
        private readonly List<string> _columnNames;
        private readonly ILogger _logger;

        public HomeRepository(DapperContext dbContext, ILogger logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
            _columnNames = typeof(Home).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name).ToList();
            _logger = logger.ForContext<HomeRepository>();
        }
    }
}