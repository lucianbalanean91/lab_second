using Microsoft.EntityFrameworkCore;
using projectTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace projectTask.Services
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAll();
        IEnumerable<Task> GetAllByFilter(DateTime? from = null, DateTime? to = null);
        Task GetById(int id);
        Task CreateTask(Task task);
        Task UpInsert(int id, Task task);
        Task Delete(int id);
    }



    public class TaskService : ITaskService
    {
        private TasksDbContext context;
        public TaskService(TasksDbContext context)
        {
            this.context = context;
        }

        public Task CreateTask(Task task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }

        public Task Delete(int id)
        {

            var existing = context.Tasks.FirstOrDefault(t => t.Id == id);
            if (existing == null)
            {
                return null;
            }
            context.Tasks.Remove(existing);
            context.SaveChanges();
            return existing;
        }



        public IEnumerable<Task> GetAll()
        {
            return context.Tasks.Include(t => t.Comments);
        }

        public IEnumerable<Task> GetAllByFilter(DateTime? from = null, DateTime? to = null)
        {
            IQueryable<Task> result = context
              .Tasks
              .Include(f => f.Comments);
            if (from == null && to == null)
            {
                return context.Tasks;
            }
            if (from != null)
            {
                result = result.Where(t => t.Deadline >= from);
            }
            if (to != null)
            {
                result = result.Where(t => t.Deadline <= to);

            }

            return result;
        }

        public Task GetById(int id)
        {
            return context.
                Tasks.Include(t => t.Comments).
                FirstOrDefault(t => t.Id == id);
        }

        public Task UpInsert(int id, Task task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == id);

            if (existing == null)
            {
                context.Tasks.Add(task);
                context.SaveChanges();
                return task;
            }
            task.Id = id;
            DateTime dateToSet = DateTime.Now;

            if (task.State.Equals(Task.Stare.Closed))
            {
                task.CloseAt = dateToSet;
            }
            if (existing.State.Equals(Task.Stare.Closed) && !task.State.Equals(Task.Stare.Closed))
            {
                task.CloseAt = (DateTime?)null;
              
            }
            context.Tasks.Update(task);
            context.SaveChanges();
            return task;
        }
    }
}
