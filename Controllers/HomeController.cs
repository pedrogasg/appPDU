using System;
using Microsoft.AspNet.Mvc;
using appPDU.Models;
using appPDU.Builders;
using System.Threading.Tasks;

namespace appPDU.Controllers
{
    [Route("/[controller]"), Route("/")]
    public class HomeController : Controller
    {
        private readonly IObjectModelRepository _repository;

        public HomeController(IObjectModelRepository repository)
        {
            _repository = repository;
        }
        [Route("[action]"), Route("")]
        public async Task<IActionResult> Index()
        {
            var builder = new ObjectBuilder(new ObjectModel());
            var model = builder.
                    Id(new Guid("B556DFED-C847-4C54-A7AB-D8FA99F71295")).
                    Title("Test Container 2").
                    Name("test-container 2").
                    Metadata("{attributes:{id:'container_ id',classList:['container','test']},childrenIds:[]}").
                    Data("<span>Het there</span>").
                    Build();
            await _repository.AddAsync(model);
            return View(model);
        }
    }
}