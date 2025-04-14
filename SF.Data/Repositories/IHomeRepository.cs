using SF.Model;
using System.Linq.Expressions;

namespace SF.Data.Repositories
{
    public interface IHomeRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<Home> GetByIdAsync(int id);
        Task<Home> InsertAsync(T entity);
        Task<Home> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<Home>> FindAsync(Expression<Func<Task, bool>> predicate);
        void SaveChanges();
    }
}