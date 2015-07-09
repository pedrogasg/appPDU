using System;
using appPDU.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace appPDU.Builders
{

    class WebPageBuilder<TBuilder, TOutput, TInput> : ObjectBuilder<TBuilder, TOutput, TInput>
    where TBuilder : WebPageBuilder<TBuilder, TOutput, TInput>
    where TOutput : WebPageModel, new()
    where TInput : class,IObjectModel
    {
        public WebPageBuilder(TInput obj) : base(obj) { }

        public TBuilder AddTemplate(Guid templateId)
        {
            _tasks.Add(CreateNewChild(_objectModel, templateId));
            return _this;
        }

        private async Task CreateNewChild(IObjectModel model, Guid templateId)
        {
            var templateMain = await Repository.GetByIdAsync(templateId);
            var templateModel = templateMain.GetPlainCopy();
            templateModel.Type = 4;
            templateModel.Name = model.Name + "-Template";
            var template = new TemplateModel(templateModel);

            var successors = template.Successors;
            foreach (var successor in successors)
            {
                successor.Successor.Name = model.Name + "-" + successor.Successor.Name;
            }
            await Repository.AddManyAsync(successors.Select(s => s.Successor as IObjectModel).ToList());
        }

    }
    class WebPageBuilder : WebPageBuilder<WebPageBuilder, WebPageModel, IObjectModel>
    {
        public WebPageBuilder(IObjectModel obj) : base(obj) { }
    }
}