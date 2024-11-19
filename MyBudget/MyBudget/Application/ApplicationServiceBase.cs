using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Interfaces.Services;
using Domain.Entities;

namespace Application;

public class ApplicationServiceBase<T, Z> : IApplicationServiceBase<T, Z> where T : BaseDto where Z : BaseEntity
{
    private readonly IServiceBase<Z> _service;
    private readonly IMapper _mapper;

    public ApplicationServiceBase(IMapper mapper, IServiceBase<Z> service) 
    {
        _mapper = mapper;
        _service = service;
    }

    public Task<bool> AddAsync(ICollection<Z> model)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> AddAsync(Z model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(ICollection<T> model)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> GetAsync(Guid id)
    {
        var response = await _service.GetAsync(id) ?? throw new Exception("Not found");
        return _mapper.Map<Z, T>(response); 
    }

    public async Task<ICollection<T>?> GetAsync(bool active = true)
    {
        var response = await _service.GetAsync(active);
        return _mapper.Map<ICollection<Z>, ICollection<T>>(response);
    }

    public async Task<bool> UpdateAsync(T model)
    {
        var entity = await _service.GetAsync(model.Id) ?? throw new Exception("Not found");
        return await _service.UpdateAsync(_mapper.Map(model, entity));
    }

    public Task<bool> UpdateAsync(ICollection<T> model)
    {
        throw new NotImplementedException();
    }
}
