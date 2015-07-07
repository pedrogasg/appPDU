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

        private readonly IObjectModelRepository<IObjectModel> _repository;
        private readonly IObjectModelFactory _factory;

        public ObjectModelController(IObjectModelRepository<IObjectModel> repository, IObjectModelFactory factory)
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
        public async Task<IActionResult> CreateObjectModel([FromBody] ObjectModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            else
            {
                model.Visible = true;
                model.Version = 1;
                IObjectModel newModel = await _factory.GetObjectModel(model);
                await _repository.AddAsync(newModel.GetPlainModel());

                var url = Url.RouteUrl(ROUTE_BY_ID, new { id = newModel.Id });
                Context.Response.Headers["Id"] = newModel.Id.ToString();
                return new CreatedResult(url, newModel);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateObjectModel([FromBody] ObjectModel model)
        {
            IObjectModel newModel = await _factory.GetObjectModel(model);
            var updated = await _repository.TryUpdateAsync(newModel);
            var status = updated ? 200 : 204;
            Context.Response.Headers["Id"] = model.Id.ToString();
            return new ObjectResult(newModel)
            {
                StatusCode = status
            };
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