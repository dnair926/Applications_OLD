namespace Applications.Core.Business
{
    using Applications.Core.Models;
    using StructureMap;

    public class StructureMapContainer : IIocContainer
    {
        public T GetInstance<T>() => container.GetInstance<T>();

        readonly IContainer container;
        public StructureMapContainer(IContainer container)
        {
            this.container = container;
        }

    }
}