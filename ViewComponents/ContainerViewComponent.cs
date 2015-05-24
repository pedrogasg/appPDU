using Microsoft.AspNet.Mvc;
using appPDU.Models;

namespace appPDU.ViewComponents
{
	public class ContainerViewComponent:ViewComponent
	{
		private ContainerModel _model;
		public IViewComponentResult Invoke(ObjectModel model)
		{
			_model = new ContainerModel(model);
			return View(_model);
		}
	}
}