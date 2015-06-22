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
                    return model;
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
            if (metadata.Template != null && metadata.Template != Guid.Empty && (model.ChildrenIds == null || model.ChildrenIds.Count == 0))
            {
                var children = await GetNewChildren(model, metadata);
                builder.AddChildren(children);

            }
            var finalModel = builder.Build();
            return finalModel;
        }

        private async Task<IObjectModel> GetNewChildren(IObjectModel model, WebPageMetadata metadata)
        {
            var templateModel = await _repository.GetByIdAsync(metadata.Template);
            templateModel.Id = Guid.NewGuid();
            templateModel.Type = 4;
            model.ChildrenIds = new List<Guid>();
            model.ChildrenIds.Add(templateModel.Id);
            var template = new TemplateModel(templateModel);

            var children = await _repository.GetByIdsAsync(new Guid[] { metadata.Template });
            foreach (var child in children)
            {
                child.Name = model.Name+"-"+child.Name;
                child.Id = Guid.NewGuid();
                template.ChildrenIds.Add(child.Id);
            }
            children.Add(templateModel);
            await _repository.AddManyAsync(children);
            return template;
        }
    }
}
