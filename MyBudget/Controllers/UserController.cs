using Application.Dto;
using Application.Interfaces;
using Controller.Middleware;
using Domain.Core.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BCrypt.Net.BCrypt;

namespace MyBudget.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IApplicationServiceUser _application;

        public UserController(ILogger<UserController> logger, IApplicationServiceUser application)
        {
            _logger = logger;
            _application = application;
        }

        [HttpPost(Name = "User")]
        public async Task<RequestResult> Post([FromBody] UserDto user)
        {
            try
            {
                _logger.LogInformation("Creating a new user");
                return new RequestResult(await _application.Register(user));
            }
            catch(Exception e)
            {
                return new RequestResult(false, e.Message, false);
            }
        }
    }
}
