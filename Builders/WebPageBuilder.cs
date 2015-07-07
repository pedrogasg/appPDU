using System;
using appPDU.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace appPDU.Builders
{

    class WebPageBuilder<TBuilder, TOutput, TInput> : ObjectBuilder<TBuilder, TOutput, TInput>
    where TBuilder : WebPageBuilder<TBuilder, TOutput, TInput>
    where TOutput : WebPageModel, new()
    where TInput : class,IObjectModel
    {
        public WebPageBuilder(TInput obj) : base(obj) { }

    }
    class WebPageBuilder : WebPageBuilder<WebPageBuilder, WebPageModel, IObjectModel>
    {
        public WebPageBuilder(IObjectModel obj) : base(obj) { }
    }
}