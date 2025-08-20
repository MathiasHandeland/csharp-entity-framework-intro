using exercise.webapi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace exercise.webapi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext _db;
        private DbSet<T> _table = null!;

        public Repository(DataContext db)
        {
            _db = db;
            _table = db.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            await _table.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(object id)
        {
            T entity = await _table.FindAsync(id);
            if (entity == null) return null;
            _table.Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _table;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> Update(int id, T entity)
        {
            _table.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return await _table.FindAsync(id);
        }
    }
}
