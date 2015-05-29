using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appPDU.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace appPDU.Models
{
    public class ObjectModelFactory : IObjectModelFactory
    {
        private readonly IObjectModelRepository _repository;
        public ObjectModelFactory(IObjectModelRepository repository)
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
                    return new ObjectModel();
            }
        }

        private async Task<WebPageModel> CreateWebPageModel(IObjectModel model)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var metadata = JsonConvert.DeserializeObject<WebPageMetadata>(model.Metadata, settings);
            var builder = new WebPageBuilder(model);
            var children = await GetNewChildren(model, metadata);
            foreach (var child in children)
            {
                builder.AddChildren(child);
            }
            var finalModel = builder.Build();
            return finalModel;
        }

        private async Task<IList<IObjectModel>> GetNewChildren(IObjectModel model, WebPageMetadata metadata)
        {
            var templateModel = await _repository.GetByNameAsync(model.TypeName);
            var template = new TemplateModel(templateModel);
            var children = await _repository.GetByIdsAsync(template.Moulds[metadata.Template]);
            foreach (var child in children)
            {
                child.Name = model.Name+"-"+child.Name;
                child.Id = Guid.NewGuid();
            }
            await _repository.AddManyAsync(children);
            return children;
        }
    }
}
