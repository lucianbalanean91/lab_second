using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectTask.Models;

namespace projectTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private TasksDbContext tasksDbContext;
         public TasksController (TasksDbContext tasksDbContext)
        {
            this.tasksDbContext = tasksDbContext;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return tasksDbContext.Tasks.Include(t => t.Comments);
        }


      

        // GET: api/Tasks/filter
        [HttpGet("filter")]
        public IEnumerable<Task> GetfilterDate([FromBody]IntervalDate interval)
        {
            DbSet<Task> list = tasksDbContext.Tasks;
            IList<Task> result = new List<Task>();

            foreach (Task task in list)
            {
                if (task.Deadline > interval.begin && task.Deadline < interval.end)
                {
                    System.Diagnostics.Debug.WriteLine(task.State);
                    result.Add(task);
                }
            }

            return result;
        }


        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tasks
        [HttpPost]
        public void Post([FromBody] Task task)
        {
            tasksDbContext.Tasks.Add(task);
            tasksDbContext.SaveChanges();
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Task task)
        {

            var existing = tasksDbContext.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == id);

            if (existing == null)
            {
                tasksDbContext.Tasks.Add(task);
                tasksDbContext.SaveChanges();
                return Ok(task);
            }
            task.Id = id;
            DateTime dateToSet = DateTime.Now;

            if (task.State.Equals(Task.Stare.Closed))
            {
                task.CloseAt = dateToSet;
            }
            if (existing.State.Equals(Task.Stare.Closed) && !task.State.Equals(Task.Stare.Closed))
            {
                task.CloseAt = DateTime.MinValue;
            }
            tasksDbContext.Tasks.Update(task);
            tasksDbContext.SaveChanges();
            return Ok(task);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = tasksDbContext.Tasks.FirstOrDefault(t => t.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            tasksDbContext.Tasks.Remove(existing);
            tasksDbContext.SaveChanges();
            return Ok();
        }
    }
}
