using Microsoft.AspNet.Mvc;
using appPDU.Models;
using appPDU.Builders;

namespace appPDU.Controllers
{
    [Route("/[controller]"), Route("/")]
    public class HomeController : Controller
    {
        [Route("[action]"), Route("")]
        public IActionResult Index()
        {
            var builder = new ObjectBuilder(new ObjectModel());
            var model = builder.
                    Title("Test Container").
                    Name("test-container").
                    Metadata("{Attributes:{Id:'container_ id',ClassList:['container','test']},ChildrenIds:[]}").
                    Build();
            return View(model);
        }
    }
}