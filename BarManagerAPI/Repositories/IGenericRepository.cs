using System.Linq.Expressions;

namespace BarManagerAPI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        void Update(T entity);

        void Delete(T entity);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
