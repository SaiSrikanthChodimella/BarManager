using BarManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BarManagerAPI.Repositories
{
    public class GenericRepository<T>(BarManagerDBContext dBContext) : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> dBSet = dBContext.Set<T>();

        public async Task AddAsync(T entity) => await dBSet.AddAsync(entity);

        public async Task<IEnumerable<T>> GetAllAsync() => await dBSet.AsNoTracking().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await dBSet.FindAsync(id);

        public void Update(T entity) => dBSet.Update(entity);

        public void Delete(T entity) => dBSet.Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dBSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

    }
}
