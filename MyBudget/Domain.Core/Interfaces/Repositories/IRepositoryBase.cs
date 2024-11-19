using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Core.Interfaces.Repositories
;

public interface IRepositoryBase<T> where T : BaseEntity
{
    Task<bool> AddAsync(ICollection<T> model);
    Task<Guid> AddAsync(T model);
    Task<T?> GetAsync(Guid id);
    Task<T?> GetAsync(int id);
    Task<ICollection<T>?> GetAsync(Expression<Func<T, bool>>? where = null, ICollection<Expression<Func<T, object>>>? includes = null);
    Task<ICollection<T>?> GetAsync(bool active = true);
    Task<bool> UpdateAsync(T model);
    Task<bool> UpdateAsync(ICollection<T> model);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> DeleteAsync(ICollection<T> model);
}
