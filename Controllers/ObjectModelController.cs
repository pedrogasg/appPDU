using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using appPDU.Models;
using System.Threading.Tasks;

namespace appPDU.Controllers
{
    [Route("api/[controller]")]
    public class ObjectModelController : Controller
    {
        const string ROUTE_BY_ID = "GetByIdRoute";

        private readonly IObjectModelRepository _repository;

        public ObjectModelController(IObjectModelRepository repository)
        {
            _repository = repository;
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
        [HttpPostAttribute]
        public async Task<IActionResult> CreateObjectModel([FromBodyAttribute] ObjectModel model)
        {
            int status;
            if (!ModelState.IsValid)
            {
                status = 400;
            }
            else
            {
                model.Id = Guid.NewGuid();
                model.DateCreate = DateTime.Now;
                await _repository.AddAsync(model);
                var url = Url.RouteUrl(ROUTE_BY_ID, new { id = model.Id });
                status = 201;
                Context.Response.Headers["Location"] = url;
            }
            return new HttpStatusCodeResult(status);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateObjectModel([FromBodyAttribute] ObjectModel model)
        {
            var updated = await _repository.TryUpdateAsync(model);
            var status = updated ? 200 : 204;
            return new HttpStatusCodeResult(status);
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