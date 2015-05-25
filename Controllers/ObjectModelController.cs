using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using appPDU.Models;

namespace appPDU.Controllers
{
		[Route("api/[controller]")]
		public class ObjectModelController:Controller
		{
			const string ROUTE_BY_ID = "GetByIdRoute"; 
// 			static readonly List<ObjectModel> _models = new List<ObjectModel>()
// 			{
// 				new ObjectModel
// 				{
// 					Id = Guid.NewGuid(),
// 					Title = "First Element",
// 					Name = "first_element",
// 					Type = 0
// 				}
// 			};

			private readonly IObjectModelRepository _repository;
			
			public ObjectModelController(IObjectModelRepository repository){
				_repository = repository;
			}
			[HttpGetAttribute]
			public IEnumerable<ObjectModel> GetAll()
			{
				return _repository.AllModels;
			}
			[HttpGetAttribute("{id:Guid}", Name=ROUTE_BY_ID)]
			public IActionResult GetById(Guid id)
			{
				var model = _repository.GetById(id);
				if(model == null){
					return HttpNotFound();
				}
				return new ObjectResult(model);
			}
			[HttpPostAttribute]
			public void CreateObjectModel([FromBodyAttribute] ObjectModel model)
			{
				if(!ModelState.IsValid)
				{
					Context.Response.StatusCode = 400;
				}
				else
				{
					model.Id = Guid.NewGuid();
					_repository.Add(model);
					var url =  Url.RouteUrl(ROUTE_BY_ID, new{id=model.Id});
					Context.Response.StatusCode = 201;
					Context.Response.Headers["Location"] = url;	
				}
			}
			[HttpDeleteAttribute("{id}")]
			public IActionResult DeleteObjectModel(Guid id)
			{

				if(_repository.TryDelete(id))
				{
					return new HttpStatusCodeResult(204);
				}

				return HttpNotFound();
			}	
		}
}