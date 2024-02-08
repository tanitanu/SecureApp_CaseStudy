using System.Linq.Expressions;
using System.Threading.Tasks;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Delete(T entity);

        Task<T> GetById(int id);
    }
}
