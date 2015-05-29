using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appPDU.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace appPDU.Models
{
    public class ObjectModelFactory
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
                    break;
            }
            return new ObjectModel();
        }

        private async Task<WebPageModel> CreateWebPageModel(IObjectModel model)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var metadata = JsonConvert.DeserializeObject<WebPageMetadata>(model.Metadata, settings);
            var template = await _repository.GetByNameAsync(model.TypeName);

            var builder = new WebPageBuilder(model);            
            return builder.Build();
        }
    }
}
