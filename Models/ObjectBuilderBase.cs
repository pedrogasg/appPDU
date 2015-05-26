using appPDU.Models;
namespace appPDU.Builders
{
	class ObjectBuilderBase<TBuilder,TOutput,TInput>
		where TBuilder:ObjectBuilderBase<TBuilder,TOutput,TInput>
		where TOutput : class, IObjectModel,new()
		where TInput : IObjectModel
	{
		protected TOutput _objectModel;
		protected TBuilder _this;
		protected ObjectBuilderBase(TInput obj)
		{
			_objectModel = new TOutput();
			_objectModel.AddInternalObject(obj);
			_this = (TBuilder)this; 	
		}
		
		public TOutput Build()
		{
			TOutput result = _objectModel;
			_objectModel = null;
			return result;
		}	
	}
}