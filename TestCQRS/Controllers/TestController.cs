using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MediatR;
using TestCQRS.Models;
using TestCQRS.Providers;
using static TestCQRS.Startup;
using TestCQRS.Infrastructure;

namespace TestCQRS.Controllers
{
    [Route("api/test")]
    public class TestController : ApiControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IEmailProvider _emailProvider;
        private readonly SalaryCalculatorDelegate _salaryCalculatorDelegate;

        public TestController(ILogger<TestController> logger, IMediator mediator, IEmailProvider emailProvider,
            SalaryCalculatorDelegate salaryCalculatorDelegate) : base(mediator)
        {
            _logger = logger;
            _emailProvider = emailProvider;
            _salaryCalculatorDelegate = salaryCalculatorDelegate;
        }

        [HttpGet]
        [Route("hello-world")]
        public async Task<IActionResult> HelloWorld()
        {
            await QueryAsync(new Queries.GetValuesQuery.Query());
            return Ok();
        }

        [HttpPost]
        [Route("make-order")]
        public async Task<IActionResult> MakeOrder([FromBody] MakeOrderRequestModel makeOrderRequest)
        {
            var r = await CommandAsync(makeOrderRequest);

            await _mediator.Publish(new Notifications.ValueAddedNotification { Value = makeOrderRequest.OrderName });

            return Ok();
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            var emailMessage = _emailProvider.SendEmail();
            return Ok(emailMessage);
        }

        [HttpGet]
        [Route("test2")]
        public IActionResult Test2()
        {
            var emailMessage = _emailProvider.SendEmail();
            return Ok(emailMessage);
        }

        [HttpGet]
        [Route("salaryCalculator")]
        public async Task<IActionResult> SalaryCalculatorAsync(int developerType)
        {
            var salaryCalculator = _salaryCalculatorDelegate((DeveloperLevel)developerType);
            var totalSalary = await salaryCalculator.CalculateTotalSalary(40);
            return Ok(totalSalary);
        }

    }
}
