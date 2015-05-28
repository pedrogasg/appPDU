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
        [Route("[action]/{name:regex(\\w.+-.*)?}"), Route("{name:regex(\\w.+-.*)?}")]
        public async Task<IActionResult> Index(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                var model = await _repository.GetByNameAsync(name);
                return View(model);
            }else
            {
                var models = await _repository.AllModelsByTypeAsync(1);
                return View("List",models);
            }
        }
        [Route("[action]")]
        public async Task<IActionResult> Editor()
        {
            var model = await _repository.GetByIdAsync(new Guid("D3562E83-C5C8-45CD-B365-8F1D81EEFA80"));
            return View(model);
        }

    }
}