using Dapper;
using SF.Model;
using SF.Data.Context;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;

namespace SF.Data.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DapperContext _context;
        private readonly string _tableName = "Homes";
        private readonly List<string> _columnNames;
        private readonly string _connectionString;
        //private readonly ILogger _log = Log.ForContext<HomeRepository>();
        private readonly Serilog.ILogger _logger;

        public HomeRepository(DapperContext context, Serilog.ILogger logger)
        {
            _context = context.SetConnectionString().CreateConnection();
            _columnNames = typeof(Home).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name).ToList();
            _connectionString = _context.ConnectionString;
            _logger = logger.ForContext<HomeRepository>();
        }

        public async Task<IEnumerable<Home>> GetAllAsync()
        {
            _logger.Information("GetAllAsync");
            // Construct the SQL query to select all records from the table.
            var query = $"SELECT * FROM {_tableName}";

            using var connection = new SqlConnection(_connectionString);

            var result = await connection.QueryAsync<Home>(query);
            _logger.Information($"GetAllAsync {@result.ToList<Home>()}");

            return result;
        }

        public async Task<Home> GetByIdAsync(int id)
        {
            _logger.Information("GetByIdAsync");
            // Construct the SQL query to select a record by its ID from the table.
            var query = $"SELECT * FROM {_tableName} WHERE Id = {id}";

            using var connection = new SqlConnection(_connectionString);

            var result = await connection.QuerySingleOrDefaultAsync<Home>(query, new { Id = id });

            _logger.Information($"GetAllAsync {@result}");

            return result;
        }

        public async Task<Home> InsertAsync(Home home)
        {
            _logger.Information("InsertAsync");

            var setValues = _columnNames.Select(prop => $"{prop} = @{prop}");
            // Construct the SQL query to insert a new record into the table.
            var query = $"INSERT INTO {_tableName} ({string.Join(',', _columnNames)}) VALUES (@{string.Join(", @", _columnNames)});" +
                    // Use SCOPE_IDENTITY() in SQL Server to retrieve the latest generated identity value.
                    // This is used to get the auto-incremented identity value after an INSERT operation.
                    "SELECT CAST(SCOPE_IDENTITY() as int)";

            // Open a database connection.
            using var connection = new SqlConnection(_connectionString);
            // Execute the query asynchronously and retrieve the inserted ID.
            var HomeResult = await connection.QueryFirstOrDefaultAsync<Home>(query, home);

            // Return the inserted ID.
            return HomeResult;
        }

        public async Task<int> UpdateAsync(Home home)
        {
            _logger.Information("UpdateAsync");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", home.ID);

            parameters.Add("Address", home.Address, dbType: DbType.String, direction: ParameterDirection.Input, size: 50);
            parameters.Add("Price", home.Price, dbType: DbType.Decimal);
            parameters.Add("ZipCode", home.ZipCode, dbType: DbType.Int32);
            parameters.Add("City", home.City, dbType: DbType.String, size: 50);
            parameters.Add("State", home.State, dbType: DbType.String, size: 50);
            parameters.Add("Comments", home.Comments, dbType: DbType.String, size: 255);
            parameters.Add("Image", home.Image, dbType: DbType.String, size: 100);
            parameters.Add("Url", home.Url, dbType: DbType.String, size: 50);

            var query = $"UPDATE {_tableName} SET " +
                            "Address = @Address, " +
                            "Price = @Price, " +
                            "ZipCode = @ZipCode, " +
                            "City = @City, " +
                            "State = @State, " +
                            "Comments = @Comments, " +
                            "Image = @Image, " +
                            "Url = @Url " +
                            "WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);

            var result = await connection.ExecuteAsync(query, parameters);

            // Return true if at least one record was affected; otherwise, return false.
            return result;
        }
        public async Task<int> DeleteAsync(int id)
        {
            _logger.Information("DeleteAsync");

            // Construct the SQL query to delete the record from the table.
            var query = String.Format("DELETE FROM {_tableName} WHERE id = {id}", _tableName, id);

            // Open a database connection.
            using var connection = new SqlConnection(_connectionString);

            var result = await connection.ExecuteAsync(query, new { Id = id });

            // Return true if at least one record was affected; otherwise, return false.
            return result;
        }

    }
}