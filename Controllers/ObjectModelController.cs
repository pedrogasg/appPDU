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
        const string ROUTE_BY_ID = "ObjectModelByIdRoute";

        private readonly IObjectModelRepository _repository;
        private readonly IObjectModelFactory _factory;

        public ObjectModelController(IObjectModelRepository repository, IObjectModelFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        [HttpGet]
        public async Task<IEnumerable<IObjectModel>> GetAll()
        {
            return await _repository.AllModelsAsync();
        }
        [HttpGet("{id:Guid}", Name = ROUTE_BY_ID)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(model);
        }

        [HttpPost]
        public async Task CreateObjectModel([FromBody] ObjectModel model)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                //return new BadRequestResult();
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
                //return new CreatedResult(url, newModel);
            }
        }
        [HttpPut]
        public async Task UpdateObjectModel([FromBody] ObjectModel model)
        {
            IObjectModel newModel = await _factory.GetObjectModel(model);
            var updated = await _repository.TryUpdateAsync(newModel);
            Context.Response.StatusCode = updated ? 200 : 204;
            Context.Response.Headers["Id"] = model.Id.ToString();
            //return new HttpStatusCodeResult(status);
        }
        [HttpDelete("{id}")]
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