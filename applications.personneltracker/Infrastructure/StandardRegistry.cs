namespace Applications.PersonnelTracker
{
    using Applications.Core.Repository;
    using Applications.PersonnelTracker.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using StructureMap;

    public class StandardRegistry : Registry
    {
        public StandardRegistry(IConfiguration configuration)
        {
            Scan(scan =>
            {
                scan.AssemblyContainingType<StandardRegistry>();
                scan.WithDefaultConventions();

                var contextOptionBuilder = new DbContextOptionsBuilder<PersonnelTrackerContext>();
                contextOptionBuilder.UseSqlServer(configuration.GetConnectionString("Application"));

                var coreBusinessContext = new PersonnelTrackerContext(contextOptionBuilder.Options);
                For<DbContext>()
                .Use(coreBusinessContext);
            });

            
        }
    }
}