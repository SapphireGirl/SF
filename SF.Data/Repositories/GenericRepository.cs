using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SF.Data.Context;
using Serilog;
using Dapper;
using SF.Data.Exceptions;

namespace SF.Data.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DapperContext _dbContext;
        private readonly string _tableName = typeof(T).Name;
        private readonly List<string> _columnNames;
        private readonly ILogger _logger;

        protected GenericRepository(DapperContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger; 
            _columnNames = typeof(T).GetProperties().Select(p => p.Name).ToList();
        }

        public async virtual Task<int> DeleteAsync(int id)
        {
            _logger.Information("DeleteAsync");

            try
            {
                var query = String.Format("DELETE FROM {_tableName} WHERE id = {id}", _tableName, id);
                var entityToDelete = await _dbContext.FindAsync<T>(id);

                if (entityToDelete == null)
                {
                    throw new ArgumentNullExceptionWithLogging(nameof(entityToDelete), "Entity Not Found", _logger);
                }

                using (IDbConnection connection = _dbContext.CreateConnection())
                {
                
                    var result = await connection.ExecuteAsync(query, new { Id = id });
                    _logger.Information($"DeleteAsync: Should be 1 {@result}");

                    if (result != 1)
                    {
                        throw new ArgumentNullExceptionWithLogging(nameof(result), "Delete Error", _logger);
                    }

                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error in DeleteAsync");
                return 0;
            }
            
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            _logger.Information("GetAllAsync");
            // Construct the SQL query to select all records from the table.
            var query = $"SELECT * FROM {_tableName}";

            using (var connection = _dbContext.CreateConnection())
            {
                var result = await connection.QueryAsync<T>(query);
                _logger.Information($"GetAllAsync {@result.ToList<T>()}");
                return result ?? Enumerable.Empty<T>();
            }
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            _logger.Information("GetById");

            var query = $"SELECT * FROM {_tableName} WHERE Id = {id}";

            using (var connection = _dbContext.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
                _logger.Information($"GetByIdAsync {@result}");
                
                if (result == null)
                {
                    throw new ArgumentNullExceptionWithLogging(nameof(result), "GetByIdAsync: Entity not found", _logger);
                }
                return result;
            }
        }

        public async virtual Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _dbContext.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync();

            _dbContext.AddRange(result);

            return result ?? Enumerable.Empty<T>();
        }
        public async virtual Task<T> InsertAsync(T entity)
        {
            _logger.Information("Insert");

            var query = $"INSERT INTO {_tableName} ({string.Join(',', _columnNames)}) VALUES (@{string.Join(", @", _columnNames)});" +
                         "SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = _dbContext.CreateConnection())
            {
                var HomeResult = await connection.QuerySingleAsync<T>(query, entity);
                _logger.Information($"InsertAsync {@HomeResult}");
                _dbContext.Add(HomeResult);
                return HomeResult;
            }
        }

        public async virtual Task<T> UpdateAsync(T entity)
        {
            _logger.Information("Update");
            try
            {
                string tableName = _tableName;
                //string tableName = GetTableName();
                string keyColumn = GetKeyColumnName() ?? 
                            throw new InvalidOperationExceptionWithLogging(_logger, "Key column not found");
                string keyProperty = GetKeyPropertyName();

                StringBuilder query = new StringBuilder();
                query.Append($"UPDATE {tableName} SET ");

                foreach (var property in GetProperties(true))
                {
                    var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

                    string propertyName = property.Name;
                    string columnName = columnAttr.Name;

                    query.Append($"{columnName} = @{propertyName},");
                }

                query.Remove(query.Length - 1, 1);

                query.Append($" WHERE {keyColumn} = @{keyProperty}");

                using(var _connection = _dbContext.CreateConnection())
                {
                    string queryString = query.ToString();
                    var result = await _connection.QuerySingleAsync<T>(queryString, entity);
                    _logger.Information($"UpdateAsync {@result}");
                    _dbContext.Update(result);
                    _dbContext.Entry(result).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, "Error in UpdateAsync");
                return null;
            }
        }
        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                                      .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);
            
            return properties;
        }

        public static string GetKeyColumnName()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

                if (keyAttributes != null && keyAttributes.Length > 0)
                {
                    object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                    if (columnAttributes != null && columnAttributes.Length > 0)
                    {
                        ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                        return columnAttribute.Name;
                    }
                    else
                    {
                        return property.Name;
                    }
                }
            }

            return null;
        }
    
        public static string? GetKeyPropertyName()
        {
            var properties = typeof(T).GetProperties()
                                      .Where(p => p.GetCustomAttribute<KeyAttribute>() != null);

            if (properties.Any())
            {
                return properties.FirstOrDefault().Name;
            }
            return null;
        }
    }
    
}
