using Domain.Entities;
using Domain.Core.Interfaces.Repositories;
using System.Linq.Expressions;
using Domain.Core.Interfaces.Services;

namespace MyBudget.Domain.Services.Services;

public class ServiceBase<T> : IServiceBase<T> where T : BaseEntity
{
    private readonly IRepositoryBase<T> _repository;

    public ServiceBase(IRepositoryBase<T> repository)
    {
        _repository = repository;
    }

    public async Task<bool> AddAsync(ICollection<T> model) => await _repository.AddAsync(model);

    public async Task<Guid> AddAsync(T model) => await _repository.AddAsync(model);

    public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);

    public async Task<bool> DeleteAsync(ICollection<T> model) => await _repository.DeleteAsync(model);

    public async Task<T?> GetAsync(Guid id) => await _repository.GetAsync(id);

    public async Task<T?> GetAsync(int id) => await _repository.GetAsync(id);

    public async Task<ICollection<T>?> GetAsync(Expression<Func<T, bool>>? where = null, ICollection<Expression<Func<T, object>>>? includes = null) 
        => await _repository.GetAsync(where, includes);

    public async Task<ICollection<T>?> GetAsync(bool active = true) => await _repository.GetAsync(active);

    public async Task<bool> UpdateAsync(T model) => await _repository.UpdateAsync(model);

    public async Task<bool> UpdateAsync(ICollection<T> model) => await _repository.UpdateAsync(model);
}
