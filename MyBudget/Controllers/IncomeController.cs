using Application.Interfaces;
using Domain.Core.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBudget.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly ILogger<IncomeController> _logger;
        private readonly IApplicationServiceIncome _application;
        public IncomeController(ILogger<IncomeController> logger, IApplicationServiceIncome application)
        {
            _logger = logger;
            _application = application;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Guid>> Post([FromBody] Income income)
        {
            _logger.LogInformation("Creating a new income");
            return Ok(await _application.AddAsync(income));
        }

        [HttpGet(Name = "Income")]
        public IEnumerable<Income> Get()
        {
            return new List<Income>();
        }
    }
}