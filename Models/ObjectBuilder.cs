using System;
using appPDU.Models;

namespace appPDU.Builders
{
	class ObjectBuilder<TBuilder,TOutput,TInput>:ObjectBuilderBase<TBuilder,TOutput,TInput>
		where TBuilder: ObjectBuilder<TBuilder,TOutput,TInput>
		where TOutput: class, IObjectModel, new()
		where TInput : IObjectModel
	{
		public ObjectBuilder(TInput obj):base(obj){}
		public TBuilder Id(Guid id)
		{
			_objectModel.Id = id;
			return _this;
		}
		public TBuilder Name(string name)
		{
			_objectModel.Name = name;
			return _this;
		}
		public TBuilder Title(string title)
		{
			_objectModel.Title = title;
			return _this;
		}
		public TBuilder Order(int order)
		{
			_objectModel.Order = order;
			return _this;
		}
		public TBuilder DateCreate(DateTime date)
		{
			_objectModel.DateCreate = date;
			return _this;
		}
		public TBuilder DateClose(DateTime date)
		{
			_objectModel.DateClose = date;
			return _this;
		}
		
		public TBuilder Visible(bool visible)
		{
			_objectModel.Visible = visible;
			return _this;
		}
		
		public TBuilder Data(string data)
		{
			_objectModel.Data = data;
			return _this;
		}
		
		public TBuilder Metadata(string data)
		{
			_objectModel.Metadata = data;
			return _this;
		}
	}
	
	class ObjectBuilder:ObjectBuilder<ObjectBuilder,ObjectModel,IObjectModel>
	{
		public ObjectBuilder(ObjectModel obj):base(obj){}
	}
}