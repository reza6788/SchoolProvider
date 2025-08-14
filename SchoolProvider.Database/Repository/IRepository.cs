using System.Linq.Expressions;
using SchoolProvider.Database.Entities;

namespace SchoolProvider.Database.Repository;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id,T entity);
}