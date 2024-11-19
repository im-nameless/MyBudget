using Application.Dto;
using Application.Requests;
using Domain.Entities;

namespace Application.Interfaces;

public interface IApplicationServiceIncome
{
    Task<Guid> AddAsync(Income req);
}
