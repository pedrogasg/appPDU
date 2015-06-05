using appPDU.Models;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using appPDU.Builders;

namespace appPDU.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IObjectModelRepository _repository;
        private MenuModel _model;
        public MenuViewComponent(IObjectModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(IObjectModel model)
        {
            var builder = new MenuBuilder(model);
            _model = builder.Build();
            _model.Children = await _repository.GetByIdsAsync(_model.ChildrenIds);
            var defaultView = "Default";
            return View(defaultView, _model);
        }
        public async Task<IViewComponentResult> InvokeAsync(IObjectModel model, string view)
        {
            var builder = new MenuBuilder(model);
            _model = builder.Build();
            _model.Children = await _repository.GetByIdsAsync(_model.ChildrenIds);
            return View(view, _model);
        }
    }
}
