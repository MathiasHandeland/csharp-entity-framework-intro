using System.Linq.Expressions;
using System.Threading.Tasks;

namespace exercise.webapi.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T> Add(T entity);
        Task<T?> Update(int id, T entity);
        Task<T?> Delete(object id);
        Task<IEnumerable<T>> GetWithIncludes(params Expression<Func<T, object>>[] includes);
    }
}
