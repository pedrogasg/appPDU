using System;
using appPDU.Models;
using System.Threading.Tasks;

namespace appPDU.Builders
{

    class WebPageBuilder<TBuilder, TOutput, TInput> : ObjectBuilder<TBuilder, TOutput, TInput>
    where TBuilder : WebPageBuilder<TBuilder, TOutput, TInput>
    where TOutput : WebPageModel, new()
    where TInput : IObjectModel
    {
        public WebPageBuilder(TInput obj) : base(obj) { }

        public TBuilder StartContruction()
        {
            return _this;
        }

    }
    class WebPageBuilder : WebPageBuilder<WebPageBuilder, WebPageModel, IObjectModel>
    {
        public WebPageBuilder(IObjectModel obj) : base(obj) { }
    }
}