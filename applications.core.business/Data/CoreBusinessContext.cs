using Microsoft.EntityFrameworkCore;

namespace Applications.Core.Business.Data
{
    public class CoreBusinessContext : DbContext
    {
        public CoreBusinessContext(DbContextOptions<CoreBusinessContext> options)
            :base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DbSet<Person> Personnel { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                
        }        
    }
}
