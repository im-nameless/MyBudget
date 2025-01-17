using Application.Dto;
using Application.Requests;

namespace Application.Interfaces;

public interface IApplicationServiceUser
{
    Task<bool> Register(UserDto req);
}
