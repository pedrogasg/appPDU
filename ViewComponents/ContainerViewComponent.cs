using appPDU.Models;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace appPDU.ViewComponents
{
	public class ContainerViewComponent:ViewComponent
	{
		private ContainerModel _model;
		private IList<ObjectModel> _children;
		private readonly IObjectModelRepository _repository;
		public ContainerViewComponent(IObjectModelRepository repository)
		{
			_repository = repository;	
		}
		public IViewComponentResult Invoke(ObjectModel model)
		{
			var defaultView = "Default";
			_model = new ContainerModel(model);
			_children  = _repository.GetByIds(_model.Children);
			return View(defaultView, _model);
		}		
		public async Task<IViewComponentResult> InvokeAsync(ObjectModel model)
		{
			var defaultView = "Default";
			_model = new ContainerModel(model);
			_children  = await _repository.GetByIdsASync(_model.Children);
			return View(defaultView, _model);
		}
	}
}