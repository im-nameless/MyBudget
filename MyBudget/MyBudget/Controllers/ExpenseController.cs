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
        private readonly IServiceBase<Income> _service;
        public IncomeController(ILogger<IncomeController> logger, IServiceBase<Income> service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Guid>> Post([FromBody] Income income)
        {
            _logger.LogInformation("Creating a new income");
            return Ok(await _service.AddAsync(income));
        }

        [HttpGet(Name = "Income")]
        public IEnumerable<Income> Get()
        {
            return new List<Income>();
        }
    }
}
