using Dapper;
using SF.Model;
using SF.Data.Context;
using Microsoft.Data.SqlClient;

namespace SF.Data.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DapperContext _context;
        private readonly string _tableName = "Homes";
        private readonly List<string> _columnNames;
        private readonly string _connectionString;
        public HomeRepository(DapperContext context)
        {
            _context = context;
            _columnNames = typeof(Home).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name).ToList();
            _connectionString = _context.ConnectionString;
        }
        
        public async Task<IEnumerable<Home>> GetAllAsync()
        {
            // Construct the SQL query to select all records from the table.
            var query = $"SELECT * FROM {_tableName}";

            using var connection = new SqlConnection(_connectionString);

            var result = await connection.QueryAsync<Home>(query);

            return result;
        }

        public async Task<Home> GetByIdAsync(int id)
        {
            // Construct the SQL query to select a record by its ID from the table.
            var query = $"SELECT * FROM {_tableName} WHERE Id = {id}";

            using var connection = new SqlConnection(_connectionString);

            var result = await connection.QuerySingleOrDefaultAsync<Home>(query, new { Id = id });

            return result;
        }

        public async Task<Home> InsertAsync(Home house)
        {
            var setValues = _columnNames.Select(prop => $"{prop} = @{prop}");
            // Construct the SQL query to insert a new record into the table.
            var query = $"INSERT INTO {_tableName} ({string.Join(',', _columnNames)}) VALUES (@{string.Join(", @", _columnNames)});" +
                    // Use SCOPE_IDENTITY() in SQL Server to retrieve the latest generated identity value.
                    // This is used to get the auto-incremented identity value after an INSERT operation.
                    "SELECT CAST(SCOPE_IDENTITY() as int)";

            // Open a database connection.
            using var connection = new SqlConnection(_connectionString);
            // Execute the query asynchronously and retrieve the inserted ID.
            var houseResult = await connection.QueryFirstOrDefaultAsync<Home>(query, house);

            // Return the inserted ID.
            return houseResult;
        }

        public async Task<int> UpdateAsync(Home house)
        {
            // Generate SET clause for the SQL query based on column names.
            //var setValues = _columnNames.Select(prop => $"{prop} = @{prop}");

            // Construct the SQL query to update the record in the table.
            var query = $"UPDATE {_tableName} SET Address={house.Address}, Price={house.Price} WHERE id = @Id";

            using var connection = new SqlConnection(_connectionString);

            var result = await connection.ExecuteAsync(query, house);

            // Return true if at least one record was affected; otherwise, return false.
            return result;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            // Construct the SQL query to delete the record from the table.
            var query = String.Format("DELETE FROM {_tableName} WHERE id = {id}", _tableName, id);

            // Open a database connection.
            using var connection = new SqlConnection(_connectionString);

            var result = await connection.ExecuteAsync(query, new { Id = id });

            // Return true if at least one record was affected; otherwise, return false.
            return result > 0;
        }

    }
}