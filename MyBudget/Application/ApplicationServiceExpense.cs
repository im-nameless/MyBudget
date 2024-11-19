using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Application.Requests;
using Domain.Auth;
using Domain.Core.Interfaces.Services;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using static BCrypt.Net.BCrypt;

namespace Application;

public class ApplicationServiceExpense : IApplicationServiceExpense
{
    private readonly JwtSettings _jwtSettings;
    private readonly IServiceBase<Expense> _service;
    private readonly IServiceBase<User> _serviceUser;

    public ApplicationServiceExpense(IServiceBase<Expense> service, 
                                    JwtSettings jwtSettings,
                                    IServiceBase<User> serviceUser) 
    {
        _service = service;
        _serviceUser = serviceUser;
        _jwtSettings = jwtSettings;
    }

    public async Task<Guid> AddAsync(Expense req)
    {
        var user = await _serviceUser.GetAsync(req.UserId) ?? throw new Exception("Invalid user");
        return await _service.AddAsync(req);
    }
}
