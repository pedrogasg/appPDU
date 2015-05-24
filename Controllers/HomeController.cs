using System;
using Microsoft.AspNet.Mvc;
using appPDU.Models;

namespace appPDU.Controllers
{
	[RouteAttribute("/[controller]"), RouteAttribute("/")]
	public class HomeController:Controller
	{
		[RouteAttribute("[action]"),RouteAttribute("")]
		public IActionResult Index()
		{
			var model = new ObjectModel()
			{
				Id=Guid.NewGuid(),
				Title="Test Container",
				Name="test-container",
				MetaData="{Attributes:{Id:'container_ id',ClassList:['container','test']},Children:[]}"
				
			};
			return View(model);
		}
	}
}