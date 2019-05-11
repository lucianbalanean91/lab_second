using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectTask.Models
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext (DbContextOptions<TasksDbContext> options) : base(options)
        {

        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
