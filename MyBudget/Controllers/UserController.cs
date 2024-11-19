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
        private readonly IApplicationServiceBase<UserDto, User> _application;

        public UserController(ILogger<UserController> logger, IApplicationServiceBase<UserDto, User> application)
        {
            _logger = logger;
            _application = application;
        }

        [HttpPost(Name = "User")]
        public async Task<RequestResult> Post([FromBody] User user)
        {
            _logger.LogInformation("Creating a new user");
            user.Password = HashPassword(user.Password);
            return new RequestResult(await _application.AddAsync(user));
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IList<UserDto>>> GetAll()
        {
            _logger.LogInformation("Getting all users");
            var response = await _application.GetAsync(true);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<RequestResult> Get([FromRoute] Guid id)
        {
            _logger.LogInformation("Getting all users");
            var response = await _application.GetAsync(id);
            return new RequestResult(response);
        }
    }
}
