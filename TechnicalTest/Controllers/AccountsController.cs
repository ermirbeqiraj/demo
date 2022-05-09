using BusinessLogic;
using DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalTest.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ICustomersManager _customersManager;

        public AccountsController(ICustomersManager customersManager)
        {
            _customersManager = customersManager;
        }

        [HttpGet("api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            var balance = _customersManager.GetBalance(id);
            return Ok(balance);
        }

        [HttpPost("api/[controller]/{id}/[action]")]
        public IActionResult Deposit(int id, [FromBody] CustomerFundsDto model)
        {
            _customersManager.Deposit(id, model);
            return Ok();
        }

        [HttpPost("api/[controller]/{id}/[action]")]
        public IActionResult Withdraw(int id, [FromBody] CustomerFundsDto model)
        {
            _customersManager.Withdraw(id, model);
            return Ok();
        }

        [HttpPost("api/[controller]/[action]")]
        public IActionResult Transfer(TransferFundsDto model)
        {
            _customersManager.Transfer(model);
            return Ok();
        }
    }
}
