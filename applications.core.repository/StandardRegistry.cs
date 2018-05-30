using Applications.Core.Infrastructure;
using Applications.Core.Models;
using StructureMap;

namespace Applications.Repository
{
    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.AssemblyContainingType<StandardRegistry>();
                scan.WithDefaultConventions();
            });

            For(typeof(IObjectRelationalMapper<>))
                .Use(typeof(ADORelationalMapper<>));

            For<IIocContainer>()
                .Use<StructureMapContainer>();

            For(typeof(IRepository<>))
                .Use(typeof(Repository<>));
        }
    }
}