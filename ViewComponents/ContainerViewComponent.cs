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
		private readonly IObjectModelRepository<IObjectModel> _repository;
		public ContainerViewComponent(IObjectModelRepository<IObjectModel> repository)
		{
			_repository = repository;	
		}

        public async Task<IViewComponentResult> InvokeAsync(IObjectModel model)
		{
            var builder = new ContainerBuilder(model);
			_model = await builder.BuildAsync();
            var defaultView = "Default";
			return View(defaultView, _model);
		}
        public async Task<IViewComponentResult> InvokeAsync(IObjectModel model, string view)
        {
            var builder = new ContainerBuilder(model);
            _model = await builder.BuildAsync();
            return View(view, _model);
        }
    }
}