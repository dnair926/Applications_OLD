namespace Applications.Core.Infrastructure
{
    using Models;
    using StructureMap;

    public class StructureMapContainer : IIocContainer
    {
        public T GetInstance<T>() => container.GetInstance<T>();

        private readonly IContainer container;
        public StructureMapContainer(IContainer container)
        {
            this.container = container;
        }

    }
}