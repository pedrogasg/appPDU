
using appPDU.Models;
namespace appPDU.Builders
{
    class ContainerBuilder<TBuilder,TOutput,TInput>:ObjectBuilder<TBuilder,TOutput,TInput>
	where TBuilder:ContainerBuilder<TBuilder,TOutput,TInput>
	where TOutput:ContainerModel,new()
	where TInput:IObjectModel
    {
		public ContainerBuilder(TInput obj):base(obj){}
	}
}