using System;
using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Core.Interfaces.Repositories
;
using MyBudget.Infra.Data.Context;

namespace MyBudget.Infra.Data.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    private readonly MyBudgetContext _context;

    public RepositoryBase(MyBudgetContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAsync(ICollection<T> model)
    {
        using var _ctx = new MyBudgetContextFactory().CreateDbContext();
        await _context.Set<T>().AddRangeAsync(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Guid> AddAsync(T model)
    {
        await _context.Set<T>().AddAsync(model);
        await _context.SaveChangesAsync();
        return model.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var model = await _context.Set<T>().FindAsync(id);
        if (model is null) return false;
        _context.Set<T>().Remove(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(ICollection<T> model)
    {
        _context.Set<T>().RemoveRange(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<T?> GetAsync(Guid id) => await _context.Set<T>().FindAsync(id);

    public async Task<T?> GetAsync(int id) => await _context.Set<T>().FindAsync(id);

    public async Task<ICollection<T>?> GetAsync(Expression<Func<T, bool>>? where = null, ICollection<Expression<Func<T, object>>>? includes = null)
    {
        var query = _context.Set<T>().AsQueryable();
        if (where is not null) query = query.Where(where);
        if (includes is not null) query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    public async Task<ICollection<T>?> GetAsync(bool active = true)
    {
        var query = _context.Set<T>().AsQueryable();
        if (active) query = query.Where(x => x.IsActive);
        return await query.ToListAsync();
    }

    public async Task<bool> UpdateAsync(T model)
    {
        _context.Set<T>().Update(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(ICollection<T> model)
    {
        _context.Set<T>().UpdateRange(model);
        return await _context.SaveChangesAsync() > 0;
    }
}
