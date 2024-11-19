using System;
using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IApplicationServiceBase<T, Z> where T : BaseDto where Z : BaseEntity
{
    Task<bool> AddAsync(ICollection<Z> model);
    Task<Guid> AddAsync(Z model);
    Task<T?> GetAsync(Guid id);
    Task<ICollection<T>?> GetAsync(bool active = true);
    Task<bool> UpdateAsync(T model);
    Task<bool> UpdateAsync(ICollection<T> model);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> DeleteAsync(ICollection<T> model);
}
