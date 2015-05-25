using appPDU.Models;
namespace appPDU.Builders
{
	class ObjectBuilderBase<TBuilder,TOutput,TInput>
		where TBuilder:ObjectBuilderBase<TBuilder,TOutput,TInput>
		where TOutput : class, IObjectModel,new()
		where TInput : IObjectModel
	{
		protected TOutput objectModel;
		protected TBuilder _this;
		protected ObjectBuilderBase(TInput obj)
		{
			objectModel = new TOutput();
			objectModel.AddInternalObject(obj);
			_this = (TBuilder)this; 	
		}
		
		public TOutput Build()
		{
			TOutput result = objectModel;
			objectModel = null;
			return result;
		}	
	}
}