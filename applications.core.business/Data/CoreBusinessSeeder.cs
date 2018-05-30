using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;

namespace Applications.Core.Business.Data
{
    public static class CoreBusinessSeeder
    {
        public static void EnsureSeedData(this CoreBusinessContext context)
        {
            if (!context.AllMigrationsApplied())
            {
                return;
            }

            context.Assignments.RemoveRange(context.Assignments);
            context.Personnel.RemoveRange(context.Personnel);
            context.ToDoItems.RemoveRange(context.ToDoItems);

            var person1 = context.Personnel.Add(new Person() { FirstName = "Deleep", LastName = "Nair", MiddleName = "K.", NetworkID = "DNair" }).Entity;
            var person2 = context.Personnel.Add(new Person() { FirstName = "Jane", LastName = "Doe", NetworkID = "JaneDoe" }).Entity;
            var person3 = context.Personnel.Add(new Person() { FirstName = "Alison", LastName = "Kay", NetworkID = "AKay" }).Entity;

            var task1 = context.ToDoItems.Add(new ToDoItem() { Description = "Task 1", EscalateAfter = 60 }).Entity;
            var task2 = context.ToDoItems.Add(new ToDoItem() { Description = "Task 2", }).Entity;
            var task3 = context.ToDoItems.Add(new ToDoItem() { Description = "Task 3", }).Entity;

            context.Assignments.AddRange(
                new Assignment() { AssignedTo = person1, ToDoItem = task1, AssignedOn = DateTime.Now, DueOn = DateTime.Now.AddDays(1), Escalated = true },
                new Assignment() { AssignedTo = person1, ToDoItem = task2, AssignedOn = DateTime.Now, },
                new Assignment() { AssignedTo = person2, ToDoItem = task3, AssignedOn = DateTime.Now, },
                new Assignment() { AssignedTo = person3, ToDoItem = task1, AssignedOn = DateTime.Now, DueOn = DateTime.Now.AddDays(1), Escalated = true },
                new Assignment() { AssignedTo = person3, ToDoItem = task3, AssignedOn = DateTime.Now, }
                );

            context.SaveChanges();
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
