using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using SF.Model;
using Serilog;

namespace SF.Data.Context
{
    public class DapperContext : DbContext
    {
        private string _connectionString;
        private SqlConnection _connection = null!;
        private readonly ILogger _logger = null!;

        public DbSet<Home> Homes { get; set; }
        public DapperContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string cannot be null or empty.", nameof(connectionString));
            }
            _connectionString = connectionString;
        }
        public DapperContext(DbContextOptions options, ILogger logger) : base(options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }
        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open(); // Connection pooling is automatically handled by SqlConnection
            return connection;
        }

        public override void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null; // Set _connection to null after disposing
            }
        }
    }
}

