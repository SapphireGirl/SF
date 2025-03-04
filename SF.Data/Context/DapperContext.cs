using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;


namespace SF.Data.Context
{
    public class DapperContext : ContextBase, IDisposable
    {
        private readonly IConfiguration _configuration;
        private string? _connectionString { get; set;}
        private SqlConnection _connection { get; set; }

        public override string ConnectionString { 
            get
            {
                return _connectionString;
            }  
            set => _connectionString = value;
        }

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override DapperContext CreateConnection()
        {
            _connection = new SqlConnection(_connectionString);
            return this;
        }
           
        public override DapperContext SetConnectionString()
        {
            try
            {
                _connectionString = _configuration.GetConnectionString("SFConnectionString");
                 return this;
            }
            catch (Exception ex)
            {
                throw new Exception("Error setting connection string", ex);
            }

        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
