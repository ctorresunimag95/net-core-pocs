using Microsoft.AspNetCore.Mvc;
using TestCQRS.Lifecycle;

namespace TestCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ITransientService _transientService1;
        private ITransientService _transientService2;
        private IScopedService _scopedService1;
        private IScopedService _scopedService2;
        private ISingletonService _singletonService1;
        private ISingletonService _singletonService2;

        public HomeController(ITransientService transientService1,
            ITransientService transientService2,
            IScopedService scopedService1,
            IScopedService scopedService2,
            ISingletonService singletonService1,
            ISingletonService singletonService2)
        {
            _transientService1 = transientService1;
            _transientService2 = transientService2;

            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;

            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Index()
        {
            var guid1 = _transientService1.GetID().ToString();
            var guid2 = _transientService2.GetID().ToString();

            var guid3 = _scopedService1.GetID().ToString();
            var guid4 = _scopedService2.GetID().ToString();

            var guid5 = _singletonService1.GetID().ToString();
            var guid6 = _singletonService2.GetID().ToString();
            return Ok();
        }
    }
}
