namespace Applications.Core.Infrastructure
{
    using Microsoft.AspNetCore.Http;
    using Models;
    using StructureMap;

    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
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
        }
    }
}