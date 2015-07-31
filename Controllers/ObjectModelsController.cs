using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using appPDU.Models;
using appPDU.Builders;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace appPDU.Controllers
{

    [Route("api/[controller]")]
    public class ObjectModelsController : Controller
    {
        private readonly IObjectModelRepository<IObjectModel> _repository;
        private readonly IObjectModelFactory _factory;
        const string ROUTE_NAME = "ObjectModelResourceRoute";

        public ObjectModelsController(IObjectModelRepository<IObjectModel> repository, IObjectModelFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        [HttpGet]
        [Route("/api/type/{type:int}")]
        public async Task<IEnumerable<IObjectModel>> GetByType(int type)
        {
            return await _repository.AllModelsByTypeAsync(type);
        }

        [HttpGet]
        [Route("/api/templates")]
        public async Task<IEnumerable<ContainerModel>> GetTemplates()
        {
            var models = await _repository.AllModelsByTypeAsync(2);
            var templates = new List<ContainerModel>(models.Count);
            foreach (var model in models)
            {
                var builder = new ContainerBuilder(model);
                await builder.RestoreChildren(_repository);
                templates.Add(builder.Build());
            }
            return templates;
        }

        [HttpPost]
        [Route("/api/creationbatch")]
        public async Task<IEnumerable<Guid>> CreationBatch([FromBody]ObjectModel[] models)
        {
            IList<Guid> Ids = new List<Guid>(models.Length);
            await _repository.AddManyAsync(models);
            foreach (var model in models)
            {
                Ids.Add(model.Id);
            }
            return Ids;
        }

        [HttpPost]
        [Route("/api/addchildren")]
        public async Task<IEnumerable<Guid>> AddChildren(Guid parentId, [FromBody]ObjectModel[] children)
        {
            var parent = await _repository.GetByIdAsync(parentId);
            var Ids = new List<Guid>(children.Length);
            await _repository.AddSuccessors(parent, children);
            foreach (var child in children)
            {
                Ids.Add(child.Id);
            }
            return Ids;
        }
    }
}
