using Applications.Core.Business.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;

namespace Applications.PersonnelTracker.Data
{
    public static class PersonnelTrackerSeeder
    {
        public static void EnsureSeedData(this PersonnelTrackerContext context)
        {
            if (context.AllMigrationsApplied())
            {
                context.Assignments.RemoveRange(context.Assignments);
                context.Personnel.RemoveRange(context.Personnel);
                context.Tasks.RemoveRange(context.Tasks);
                context.Statuses.RemoveRange(context.Statuses);

                var person1 = context.Personnel.Add(new Person() { FirstName = "Deleep", LastName = "Nair", MiddleName = "M.", UserID = "DNair" }).Entity;
                var person2 = context.Personnel.Add(new Person() { FirstName = "Jane", LastName = "Doe", MiddleName = "M.", UserID = "JaneDoe" }).Entity;
                var person3 = context.Personnel.Add(new Person() { FirstName = "Alison", LastName = "Kay", MiddleName = "M.", UserID = "AKay" }).Entity;

                var task1 = context.Tasks.Add(new Task() { Name = "Task 1", EscalateInMinutes = 60 }).Entity;
                var task2 = context.Tasks.Add(new Task() { Name = "Task 2", Description = "Task for users" }).Entity;
                var task3 = context.Tasks.Add(new Task() { Name = "Task 3", }).Entity;

                context.Assignments.AddRange(
                    new Assignment() { AssignedTo = person1, Task = task1, AssignedOn = DateTime.Now, Escalated = true },
                    new Assignment() { AssignedTo = person1, Task = task2, AssignedOn = DateTime.Now, },
                    new Assignment() { AssignedTo = person2, Task = task3, AssignedOn = DateTime.Now, },
                    new Assignment() { AssignedTo = person3, Task = task1, AssignedOn = DateTime.Now, Escalated = true },
                    new Assignment() { AssignedTo = person3, Task = task3, AssignedOn = DateTime.Now, }
                    );

                var active = context.Statuses.Add(new Status() { Description = "Current" }).Entity;

                context.NamePrefixes.AddRange(
                        new NamePrefix() { Name = "Mr.", Status = active },
                        new NamePrefix() { Name = "Mrs.", Status = active },
                        new NamePrefix() { Name = "Ms.", Status = active }
                    );

                context.NameSuffixes.AddRange(
                        new NameSuffix() { Name = "Jr", Status = active },
                        new NameSuffix() { Name = "I", Status = active },
                        new NameSuffix() { Name = "II", Status = active },
                        new NameSuffix() { Name = "III", Status = active }
                    );

                context.SaveChanges();
            }
        }

        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}
