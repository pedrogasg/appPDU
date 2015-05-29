using appPDU.Models;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using appPDU.Builders;

namespace appPDU.ViewComponents
{
	public class ContainerViewComponent:ViewComponent
	{
		private ContainerModel _model;
		private readonly IObjectModelRepository _repository;
		public ContainerViewComponent(IObjectModelRepository repository)
		{
			_repository = repository;	
		}

        public async Task<IViewComponentResult> InvokeAsync(IObjectModel model)
		{
            var builder = new ContainerBuilder(model);
			_model = builder.Build();
            _model.Children = await _repository.GetByIdsAsync(_model.ChildrenIds);
            var defaultView = _model.Subtype ?? "Default";
			return View(defaultView, _model);
		}
	}
}