using Applications.PersonnelTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Applications.PersonnelTracker.Migrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PersonnelTrackerContext>
    {
        public PersonnelTrackerContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<PersonnelTrackerContext>();

            var connectionString = configuration.GetConnectionString("Application");

            builder.UseSqlServer(connectionString);

            return new PersonnelTrackerContext(builder.Options);
        }
    }
}
