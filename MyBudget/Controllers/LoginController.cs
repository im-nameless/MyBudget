using Application.Dto;
using Application.Interfaces;
using Application.Requests;
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
        public async Task<ActionResult<string>> Post([FromBody] LoginRequest req)
        {
            _logger.LogInformation($"Authenticating user {req.Email}");
            return Ok(await _application.Authenticate(req));
        }
    }
}
