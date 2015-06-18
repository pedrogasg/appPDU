using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using appPDU.Models;
using System.Threading.Tasks;
using appPDU.Builders;

namespace appPDU.Controllers
{
    [Route("api/[controller]")]
    public class ObjectModelController : Controller
    {
        const string ROUTE_BY_ID = "GetByIdRoute";

        private readonly IObjectModelRepository _repository;
        private readonly IObjectModelFactory _factory;

        public ObjectModelController(IObjectModelRepository repository, IObjectModelFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        [HttpGetAttribute]
        public async Task<IEnumerable<IObjectModel>> GetAll()
        {
            return await _repository.AllModelsAsync();
        }
        [HttpGetAttribute("{id:Guid}", Name = ROUTE_BY_ID)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(model);
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
        [HttpGet]
        [Route("/api/type/{type:int}")]
        public async Task<IEnumerable<IObjectModel>> GetByType(int type)
        {
            return await _repository.AllModelsByTypeAsync(type);
        }
        [HttpPostAttribute]
        public async Task CreateObjectModel([FromBodyAttribute] ObjectModel model)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
            }
            else
            {
                model.Id = Guid.NewGuid();
                model.DateCreate = DateTime.Now;
                IObjectModel newModel = await _factory.GetObjectModel(model);
                await _repository.AddAsync(newModel);

                var url = Url.RouteUrl(ROUTE_BY_ID, new { id = newModel.Id });
                Context.Response.StatusCode = 201;
                Context.Response.Headers["Id"] = newModel.Id.ToString();
                Context.Response.Headers["Location"] = url;
            }
        }
        [HttpPut]
        public async Task UpdateObjectModel([FromBodyAttribute] ObjectModel model)
        {
            IObjectModel newModel = await _factory.GetObjectModel(model);
            var updated = await _repository.TryUpdateAsync(newModel);
            Context.Response.StatusCode = updated ? 200 : 204;
            Context.Response.Headers["Id"] = model.Id.ToString();
            //return new HttpStatusCodeResult(status);
        }
        [HttpDeleteAttribute("{id}")]
        public async Task<IActionResult> DeleteObjectModel(Guid id)
        {

            var erased = await _repository.TryDeleteAsync(id);
            if (erased)
            {
                return new HttpStatusCodeResult(204);
            }

            return HttpNotFound();
        }
    }
}