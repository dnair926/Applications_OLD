using Applications.Core.Business.Services;
using Applications.Core.Models;
using Applications.Core.Repository;
using Microsoft.AspNetCore.Http;
using StructureMap;

namespace Applications.Core.Business
{

    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            this.Scan(scan =>
            {
                scan.AssemblyContainingType<Core.AssemblyHook>();
                scan.WithDefaultConventions();
            });

            this.Scan(scan =>
            {
                scan.AssemblyContainingType<Repository.AssemblyHook>();
                scan.WithDefaultConventions();
            });

            this.Scan(scan =>
            {
                scan.AssemblyContainingType<StandardRegistry>();
                scan.WithDefaultConventions();
            });

            For<IIocContainer>()
                .Use<StructureMapContainer>();

            For<IHttpContextAccessor>()
                .Use<HttpContextAccessor>()
                .Singleton();

            For<IAuthenticationService>()
                .Use<WindowsAuthenticationService>();

            For(typeof(IObjectRelationalMapper<>))
                .Use(typeof(EntityFameworkRelationalMapper<>));

            For<IRepositoryService>()
                .Use<RepositoryService>();

            For(typeof(IRepository<>))
                .Use(typeof(Repository<>));

            For(typeof(IListService<>))
                .Use(typeof(ListService<>));

            For<IFormService>()
                .Use<FormService>();
        }
    }
}