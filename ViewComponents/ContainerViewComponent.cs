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
			_model = new ContainerModel(model);
            var defaultView = _model.Subtype ?? "Default";
            _children  = _repository.GetByIds(_model.ChildrenIds);
			return View(defaultView, _model);
		}		
		public async Task<IViewComponentResult> InvokeAsync(ObjectModel model)
		{
			_model = new ContainerModel(model);
			var defaultView = _model.Subtype ?? "Default";
			_children  = await _repository.GetByIdsASync(_model.ChildrenIds);
			return View(defaultView, _model);
		}
	}
}