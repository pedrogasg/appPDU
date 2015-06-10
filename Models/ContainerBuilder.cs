using System;
using appPDU.Models;
using System.Threading.Tasks;

namespace appPDU.Builders
{
    class ContainerBuilder<TBuilder, TOutput, TInput> : ObjectBuilder<TBuilder, TOutput, TInput>
    where TBuilder : ContainerBuilder<TBuilder, TOutput, TInput>
    where TOutput : ContainerModel, new()
    where TInput : IObjectModel
    {
        public ContainerBuilder(TInput obj) : base(obj) { }

        public TBuilder AddChildren(Guid id)
        {
            _objectModel.ChildrenIds.Add(id);
            return _this;
        }

        public TBuilder AddChildren(TInput child)
        {
            _objectModel.ChildrenIds.Add(child.Id);
            return _this;
        }

        public TBuilder AddCkass(string className)
        {
            _objectModel.Attributes.ClassList.Add(className);
            return _this;
        }
    }
    class ContainerBuilder : ContainerBuilder<ContainerBuilder, ContainerModel, IObjectModel>
    {
        public ContainerBuilder(IObjectModel obj) : base(obj) { }
        internal async Task RestoreChildren(IObjectModelRepository repo)
        {
            foreach (var childId in _objectModel.ChildrenIds)
            {
                _objectModel.Children.Add(await repo.GetByIdAsync(childId));
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