using Application.Interfaces;
using Controller.Middleware;
using Domain.Core.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBudget.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IApplicationServiceExpense _application;
        public ExpenseController(ILogger<ExpenseController> logger, IApplicationServiceExpense application)
        {
            _logger = logger;
            _application = application;
        }

        [HttpPost]
        [Route("")]
        public async Task<RequestResult> Post([FromBody] Expense expense)
        {
            _logger.LogInformation("Creating a new income");
            return new RequestResult(await _application.AddAsync(expense));
        }
    }
}
