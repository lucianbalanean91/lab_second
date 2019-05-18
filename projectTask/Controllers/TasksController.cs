using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectTask.Models;
using projectTask.Services;

namespace projectTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        //private TasksDbContext tasksDbContext;
        // public TasksController (TasksDbContext tasksDbContext)
        //{
        //    this.tasksDbContext = tasksDbContext;
        //}

        private ITaskService taskService;
        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        /// <summary>
        /// Gets all Tasks without filter
        /// </summary>
        /// <returns>A list of Tasks without filter</returns>
    [HttpGet]
        public IEnumerable<Task> Get()
        {
            return taskService.GetAll();
        }

        /// <summary>
        /// Gets all filter by tasks
        /// </summary>
        /// <param name="from">Optional, filter by minimum DateTime</param>
        /// <param name="to">Optional,filter by maximum DateTime</param>
        /// <returns>A list of Task objects by filter</returns>
        [HttpGet("filter")]
        public IEnumerable<Task> GetfilterDate([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            //   DbSet<Task> list = tasksDbContext.Tasks;
            //  IList<Task> result = new List<Task>();
            return taskService.GetAllByFilter(from, to);
        }

        /// <summary>
        /// Gets all tasks after id
        /// </summary>
        /// <param name="id">Optional, find tasks after id</param>
        /// <returns>A list of task objects </returns>
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = taskService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        /// <summary>
        /// Adds a new task 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /tasks
        ///     {
        ///             
        ///             "title": "Calculeaza aria unui dreptunghi",
        ///             "description": "laturile sunt egale",
        ///             "added": "2019-05-11T03:59:24.3376595",
        ///             "deadline": "2019-06-15T03:59:24.3407304",
        ///             "important": 2,
        ///             "state": 0,
        ///             "closeAt": "2019-06-15T03:59:24.341252",
        ///             "comments": [
        ///   	                {
        ///  	                   
        ///	                        "text": "Se calculeaza greu",
        ///	                        "important": true
        ///                      }
        ///             ]
        ///     }
        ///</remarks>
        /// <param name="task">Optional,Add a new task to the fields</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status418ImATeapot)]
        [HttpPost]
        public void Post([FromBody] Task task)
        {
            taskService.CreateTask(task);
        }

        /// <summary>
        /// Updating a task
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /tasks
        ///     {
        ///             "id": 1,
        ///             "title": "Calculeaza aria unui dreptunghi",
        ///             "description": "laturile sunt egale",
        ///             "added": "2019-05-11T03:59:24.3376595",
        ///             "deadline": "2019-06-15T03:59:24.3407304",
        ///             "important": 2,
        ///             "state": 0,
        ///             "closeAt": "2019-06-15T03:59:24.341252",
        ///             "comments": [
        ///   	                {
        ///  	                    "id": 1,
        ///	                        "text": "Se calculeaza greu",
        ///	                        "important": true
        ///                      }
        ///             ]
        ///     }
        ///</remarks>
        /// <param name="id">Specify the id to be modified</param>
        /// <param name="task">Specify task object in JSON format </param>
        /// <returns>Returns the modified object</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Task task)
        {

            var result = taskService.UpInsert(id, task);
            return Ok(result);
        }

        /// <summary>
        /// Deleting a task by id
        /// </summary>
        /// <param name="id">Specify id in URL to delete</param>
        /// <returns>Remove the task object from the list</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = taskService.Delete(id);
            if (existing == null)
            {
                return NotFound();
            }
            return Ok(existing);
        }
    }
}
