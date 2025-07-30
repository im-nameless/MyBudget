using Application.Dto;
using Application.Interfaces;
using Application.Requests;
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
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IApplicationServiceLogin _application;

        public LoginController(ILogger<LoginController> logger, IApplicationServiceLogin application)
        {
            _logger = logger;
            _application = application;
        }

        [HttpPost(Name = "Login")]
        public async Task<RequestResult> Post([FromBody] LoginRequest req)
        {
            try
            {
                _logger.LogInformation($"Authenticating user {req.Email}");
                return new RequestResult(await _application.Authenticate(req));
            }
            catch (Exception e)
            {
                return new RequestResult(false, e.Message, false);
            }
        }
    }
}
