using System;
using Microsoft.AspNet.Mvc;

namespace appPDU.Controllers
{
	[RouteAttribute("/[controller]"), RouteAttribute("/")]
	public class HomeController:Controller
	{
		[Route("[action]"),Route("")]
		public IActionResult Index()
		{
			return View();
		}
	}
}