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
			objectModel.Id = id;
			return _this;
		}
		public TBuilder Name(string name)
		{
			objectModel.Name = name;
			return _this;
		}
		public TBuilder Title(string title)
		{
			objectModel.Title = title;
			return _this;
		}
		public TBuilder Order(int order)
		{
			objectModel.Order = order;
			return _this;
		}
		public TBuilder DateCreate(DateTime date)
		{
			objectModel.DateCreate = date;
			return _this;
		}
		public TBuilder DateClose(DateTime date)
		{
			objectModel.DateClose = date;
			return _this;
		}
		
		public TBuilder Visible(bool visible)
		{
			objectModel.Visible = visible;
			return _this;
		}
		
		public TBuilder Data(string data)
		{
			objectModel.Data = data;
			return _this;
		}
		
		public TBuilder Metadata(string data)
		{
			objectModel.Metadata = data;
			return _this;
		}
	}
	
	class ObjectBuilder:ObjectBuilder<ObjectBuilder,ObjectModel,IObjectModel>
	{
		public ObjectBuilder(ObjectModel obj):base(obj){}
	}
}