using Applications.Core.Business.Data;
using Microsoft.EntityFrameworkCore;

namespace Applications.PersonnelTracker.Data
{
    public class PersonnelTrackerContext : DbContext
    {
        public PersonnelTrackerContext(DbContextOptions<PersonnelTrackerContext> options)
            :base(options)
        {

        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Person> Personnel { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<NameSuffix> NameSuffixes { get; set; }

        public DbSet<NamePrefix> NamePrefixes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }        
    }
}
