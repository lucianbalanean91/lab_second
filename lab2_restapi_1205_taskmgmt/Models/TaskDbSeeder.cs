using System;
using System.Linq;

namespace lab2_restapi_1205_taskmgmt.Models
{
    public static class TaskDbSeeder
    {
        public static void Initialize(TasksDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any tasks.
            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }

            context.Tasks.AddRange(
                new Task
                {
                    Description = "Rose",
                    Title = "sdfsd",
                    Added = DateTime.Now,
                    Deadline = DateTime.Now,
                    Importance = 0,
                    State = 0
                },

                new Task
                {
                    Description = "Shoes",
                    Title = "sdfsd",
                    Added = DateTime.Now,
                    Deadline = DateTime.Now,
                    Importance = 0,
                    State = 0
                }
            );
            //context.Users.AddRange(
            //    new User
            //    {
            //        FirstName = "Balanel",
            //        LastName = "Lucian",
            //        Username = "bman",
            //        Email = "bman@gmail.com",
            //        Password = "1234567",
            //        UserRole = UserRole.User_Manager
            //    },

            //    new User
            //    {
            //        FirstName = "Radu Gasca",
            //        LastName = "Radu",
            //        Username = "radu93",
            //        Email = "radu@gmail.com",
            //        Password = "1234567",
            //        UserRole = UserRole.Admin
            //    });
            context.SaveChanges(); // commit transaction
        }
    }
}