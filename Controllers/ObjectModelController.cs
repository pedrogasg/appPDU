using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using appPDU.Models;

namespace appPDU.Controllers
{
		[RouteAttribute("api/[controller]")]
		public class ObjectModelController:Controller
		{
			const string ROUTE_BY_ID = "GetByIdRoute"; 
			static readonly List<ObjectModel> _models = new List<ObjectModel>()
			{
				new ObjectModel
				{
					Id = Guid.NewGuid(),
					Title = "First Element",
					Name = "first_element",
					Type = 0
				}
			};
			[HttpGetAttribute]
			public IEnumerable<ObjectModel> GetAll()
			{
				return _models;
			}
			[HttpGetAttribute("{id:Guid}", Name=ROUTE_BY_ID)]
			public IActionResult GetById(Guid Id)
			{
				var model = _models.FirstOrDefault(x=>x.Id == Id);
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
					_models.Add(model);
					var url =  Url.RouteUrl(ROUTE_BY_ID, new{id=model.Id});
					Context.Response.StatusCode = 201;
					Context.Response.Headers["Location"] = url;	
				}
			}
			[HttpDeleteAttribute("{id}")]
			public IActionResult DeleteObjectModel(Guid Id)
			{
				var model = _models.FirstOrDefault(x=> x.Id == Id);
				if(model == null)
				{
					return HttpNotFound();
				}
				_models.Remove(model);
				return new HttpStatusCodeResult(204);
			}	
		}
}