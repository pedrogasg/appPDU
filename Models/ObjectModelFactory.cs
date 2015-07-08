using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using appPDU.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace appPDU.Models
{
    public class ObjectModelFactory : IObjectModelFactory
    {
        private readonly IObjectModelRepository<IObjectModel> _repository;
        public ObjectModelFactory(IObjectModelRepository<IObjectModel> repository)
        {
            _repository = repository;
        }
        public async Task<IObjectModel> GetObjectModel(IObjectModel model)
        {
            switch (model.Type)
            {
                case 1:
                    return await CreateWebPageModel(model);
                default:
                    return model;
            }
        }

        private async Task<WebPageModel> CreateWebPageModel(IObjectModel model)
        {
            var metadata = JsonConvert.DeserializeObject<WebPageMetadata>(model.Metadata);
            var builder = new WebPageBuilder(model);
            if (metadata.Template != null && metadata.Template != Guid.Empty && (model.Successors.Count == 0))
            {
                await CreateNewChild(model, metadata);
            }
            var finalModel = builder.Build();
            return finalModel;
        }

        private async Task CreateNewChild(IObjectModel model, WebPageMetadata metadata)
        {
            var templateMain = await _repository.GetByIdAsync(metadata.Template);
            var templateModel = templateMain.GetPlainCopy();
            templateModel.Type = 4;
            templateModel.Name = model.Name+"-Template";
            var template = new TemplateModel(templateModel);

            var successors = template.Successors;
            foreach (var successor in successors)
            {
                successor.Successor.Name = model.Name+"-"+ successor.Successor.Name;
            }
            await _repository.AddManyAsync(successors.Select(s=>s.Successor as IObjectModel).ToList());
        }
    }
}
