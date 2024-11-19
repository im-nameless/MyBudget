using Application.Dto;
using Application.Requests;

namespace Application.Interfaces;

public interface IApplicationServiceLogin
{
    Task<string> Authenticate(LoginRequest req);
}
