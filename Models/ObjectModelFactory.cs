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
                builder.AddTemplate(metadata.Template);
            }
            return await builder.BuildAsync();
        }

    }
}
