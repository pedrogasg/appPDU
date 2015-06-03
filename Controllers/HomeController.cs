using System;
using Microsoft.AspNet.Mvc;
using appPDU.Models;
using appPDU.Builders;
using System.Threading.Tasks;
using System.Collections.Generic;

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
                var pages = new List<WebPageModel>();
                foreach (var model in models)
                {
                    pages.Add(new WebPageModel(model));
                }
                return View("List",pages);
            }
        }
        [Route("[action]/{name:regex(\\w.+-.*)?}")]
        public async Task<IActionResult> Editor(string name)
        { 
            if (!String.IsNullOrEmpty(name))
            {
                var model = await _repository.GetByNameAsync(name);
                return View(model);
            }
            return HttpNotFound();
        }

    }
}