using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectTask.Models
{
    public class TasksDbSeeder
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
                    Title ="Calculeaza aria unui dreptunghi",
                    Description ="laturile sunt egale",
                    Added = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(35),
                    Important = Task.Importance.High,
                    State = Task.Stare.InProgress,
                    CloseAt = DateTime.Now.AddDays(35)

                },
                new Task
                {
                    Title = "Calculeaza aria unui romb",
                    Description = "laturile sunt egale",
                    Added = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(14),
                    Important = Task.Importance.Low,
                    State = Task.Stare.Open,
                    CloseAt = DateTime.Now.AddDays(14)

                }
            );
            context.SaveChanges();
        }
    }
}

