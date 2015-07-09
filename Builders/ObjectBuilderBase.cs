using appPDU.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace appPDU.Builders
{
	class ObjectBuilderBase<TBuilder,TOutput,TInput>
		where TBuilder:ObjectBuilderBase<TBuilder,TOutput,TInput>
		where TOutput : class, IObjectModel,new()
		where TInput : class,IObjectModel
	{
		protected TOutput _objectModel;
		protected TBuilder _this;
        internal List<Task> _tasks = new List<Task>();
        [FromServices]
        public IObjectModelRepository<IObjectModel> Repository { get; set; }

        protected ObjectBuilderBase(TInput obj = null)
		{
			_objectModel = new TOutput();
            if(obj != null)
            {
			    _objectModel.AddInternalObject(obj);
            }
			_this = (TBuilder)this; 	
		}
		public TOutput Build()
		{
			TOutput result = _objectModel;
			_objectModel = null;
			return result;
		}

        public async Task<TOutput> AsyncBuild()
        {
            TOutput result = _objectModel;
            await Task.WhenAll(_tasks);
            _objectModel = null;
            return result;
        }	
	}
}