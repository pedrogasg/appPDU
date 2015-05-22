using System;
using Microsoft.AspNet.Mvc;

namespace appPDU.Controllers
{
	[RouteAttribute("/[controller]"), RouteAttribute("/")]
	public class HomeController:Controller
	{
		[RouteAttribute("[action]"),RouteAttribute("")]
		public IActionResult Index()
		{
			return View();
		}
	}
}