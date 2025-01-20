using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dto;
using Application.Interfaces;
using Application.Requests;
using AutoMapper;
using Domain.Auth;
using Domain.Core.Interfaces.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using MyBudget.Domain.Extensions;
using static BCrypt.Net.BCrypt;

namespace Application;

public class ApplicationServiceUser : IApplicationServiceUser
{
    private readonly IServiceBase<User> _service;
    private readonly IMapper _mapper;

    public ApplicationServiceUser(IServiceBase<User> service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<bool> Register(UserDto req)
    {
        try
        {
            var user = await _service.GetAsync(x => x.Email == req.Email);

            if (user!.Any())
            {
                throw new BudgetException("Usuário já cadastrado");
            }

            if (string.IsNullOrEmpty(req.Name) || string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password) ||
                string.IsNullOrEmpty(req.Phone) || req.Birthdate == DateTime.MinValue)
            {
                throw new BudgetException("Nome, email e senha são obrigatórios.");
            }

            if (!ValidatePassword(req.Password))
            {
                throw new BudgetException("Senha deve conter ao menos 8 caracteres, uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");
            }

            req.Password = HashPassword(req.Password);
            req.Name = req.Name.RemoveMultipleWhiteSpaces();
            req.Email = req.Email.RemoveWhiteSpaces();
            req.Phone = req.Phone.RemoveMultipleWhiteSpaces().RemoveSpecialCharacters();
            req.Birthdate = req.Birthdate.SetKindUtc();
            
            var model = _mapper.Map<UserDto, User>(req);
            model.CreatedAt = model.CreatedAt.SetKindUtc();

            return await _service.AddAsync(model) != Guid.Empty;
        }
        catch (Exception e)
        {
            throw new BudgetException(e.Message);
        }
    }
        
    private bool ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8 )
        {
            return false;
        }

        var hasUpperCase = new System.Text.RegularExpressions.Regex(@"[A-Z]+");
        var hasLowerCase = new System.Text.RegularExpressions.Regex(@"[a-z]+");
        var hasDigit = new System.Text.RegularExpressions.Regex(@"\d+");
        var hasSpecialChar = new System.Text.RegularExpressions.Regex(@"[\W_]+");

        return hasUpperCase.IsMatch(password) && hasLowerCase.IsMatch(password) && hasDigit.IsMatch(password) && hasSpecialChar.IsMatch(password);
    }

}
