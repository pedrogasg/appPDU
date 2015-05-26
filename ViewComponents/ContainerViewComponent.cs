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
		public async Task<IViewComponentResult> InvokeAsync(ObjectModel model)
		{
			_model = new ContainerModel(model);
			var defaultView = _model.Subtype ?? "Default";
			_children  = await _repository.GetByIdsAsync(_model.ChildrenIds);
			return View(defaultView, _model);
		}
	}
}