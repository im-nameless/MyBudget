using Application.Dto;
using Application.Requests;
using Domain.Entities;

namespace Application.Interfaces;

public interface IApplicationServiceExpense
{
    Task<Guid> AddAsync(Expense req);
}
