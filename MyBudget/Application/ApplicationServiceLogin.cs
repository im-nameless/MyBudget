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

public class ApplicationServiceLogin : IApplicationServiceLogin
{
    private readonly JwtSettings _jwtSettings;
    private readonly IServiceBase<User> _service;

    public ApplicationServiceLogin(IServiceBase<User> service, JwtSettings jwtSettings) 
    {
        _service = service;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> Authenticate(LoginRequest req)
    {
        var user = (await _service.GetAsync(x => x.Email == req.Email))!.FirstOrDefault();

        if (user is null || !Verify(req.Password, user.Password))
        {
            throw new Exception("Invalid email or password");
        }

        return await GenerateToken(user, req);
    }

    private async Task<string> GenerateToken(User user, LoginRequest req)
    {
        var claims = new List<Claim>
        {
            new Claim("Username", user.Name),
            new Claim("Id", user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(req.RememberMe ? 24 * 365 : _jwtSettings.Expiration),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
