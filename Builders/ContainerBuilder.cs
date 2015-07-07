using System;
using appPDU.Models;
using System.Threading.Tasks;

namespace appPDU.Builders
{
    class ContainerBuilder<TBuilder, TOutput, TInput> : ObjectBuilder<TBuilder, TOutput, TInput>
    where TBuilder : ContainerBuilder<TBuilder, TOutput, TInput>
    where TOutput : ContainerModel, new()
    where TInput : class,IObjectModel
    {
        public ContainerBuilder(TInput obj) : base(obj) { }

        public TBuilder AddClass(string className)
        {
            _objectModel.Attributes.ClassList.Add(className);
            return _this;
        }
    }
    class ContainerBuilder : ContainerBuilder<ContainerBuilder, ContainerModel, IObjectModel>
    {
        public ContainerBuilder(IObjectModel obj) : base(obj) { }
        internal async Task RestoreChildren(IObjectModelRepository<IObjectModel> repo)
        {
            foreach (var successor in _objectModel.Successors)
            {
                _objectModel.Children.Add(await repo.GetByIdAsync(successor.SuccessorId));
            }
        }
    }

    class HtmlBuilder:ContainerBuilder<HtmlBuilder,HtmlModel,IObjectModel>
    {
        public HtmlBuilder(IObjectModel obj) : base(obj) { }
    }

    class MenuBuilder : ContainerBuilder<MenuBuilder, MenuModel, IObjectModel>
    {
        public MenuBuilder(IObjectModel obj) : base(obj) { }
    }
}