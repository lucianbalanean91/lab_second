using lab2_restapi_1205_taskmgmt;
using lab2_restapi_1205_taskmgmt.Models;
using lab2_restapi_1205_taskmgmt.Services;
using lab2_restapi_1205_taskmgmt.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class TasksServiceTests
    {
        //   private IOptions<AppSettings> config;

        [SetUp]
        public void Setup()
        {
            //config = Options.Create(new AppSettings
            //{
            //    Secret = "dsadhjcghduihdfhdifd8ih"
            //});
        }

        [Test]
        public void ValidCreateShouldANewTask()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidCreateShouldANewTask))// "CreateANewTask")
              .Options;
            using (var context = new TasksDbContext(options))
            {
                var tasksService = new TaskService(context);

                Task task = new Task();
                User addedBy = task.Owner;
                var create = new lab2_restapi_1205_taskmgmt.ViewModels.TaskPostModel
                {
                    Title = "Read1",
                    Description = "Read1",
                    Added = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(15),
                    ClosedAt = null,
                    Importance = "Medium",
                    State = "InProgress",
                    Comments = task.Comments
                };
                var result = tasksService.Create(create, addedBy);
                Assert.NotNull(result);
                Assert.AreEqual(create.Title, result.Title);
            }

        }

        [Test]
        public void ValidDeleteShouldANewTask()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidDeleteShouldANewTask))// "DeleteExistingTask")
              .Options;
            using (var context = new TasksDbContext(options))
            {
                var tasksService = new TaskService(context);
                Task task = new Task();
                User addedBy = task.Owner;
                var result = new lab2_restapi_1205_taskmgmt.ViewModels.TaskPostModel
                {
                    Title = "Read2",
                    Description = "Read2",
                    Added = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(15),
                    Importance = "Low",
                    State = "InProgress",
                    ClosedAt = null,
                    Comments = null
                };
                Task savetask = tasksService.Create(result, addedBy);
                Task task2 = tasksService.Delete(savetask.Id);

                Assert.IsNull(tasksService.GetById(task2.Id));
            }
        }

        [Test]
        public void GetAllShouldReturnCorrectNumberOfPages()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(GetAllShouldReturnCorrectNumberOfPages))
              .Options;

            using (var context = new TasksDbContext(options))
            {

                var commentService = new CommentService(context);
                var taskService = new TaskService(context);
                Task task = new Task();
                User addedBy = task.Owner;
                var addedTask = taskService.Create(new lab2_restapi_1205_taskmgmt.ViewModels.TaskPostModel
                {
                    Title = "Read1",
                    Description = "Read1",
                    Added = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(15),
                    ClosedAt = null,
                    Importance = "Medium",
                    State = "InProgress",
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Important = true,
                            Text = "Read1"
                        }
                    },



                }, addedBy);

                var allTask = taskService.GetAll(1, DateTime.Now);
                Assert.AreEqual(1, allTask.NumberOfPages);
            }
        }

        //[Test]
        //public void ValidUpdateTaskShouldModifyGivenTask()
        //{
        //    var options = new DbContextOptionsBuilder<TasksDbContext>()
        //      .UseInMemoryDatabase(databaseName: nameof(ValidUpdateTaskShouldModifyGivenTask))// "ValidUpdateTaskShouldModifyGivenTask")
        //      .Options;
        //    using (var context = new TasksDbContext(options))
        //    {   
               
        //        var tasksService = new TaskService(context);
        //        // Task task = new Task();
      
        //        var addedTask = new TaskPostModel
        //        {   
        //            Title = "Read3",
        //            Description = "Read3",
        //            Added = DateTime.Now,
        //            Deadline = DateTime.Now.AddDays(15),
        //            ClosedAt = null,
        //            Importance = "Medium",
        //            State = "InProgress",
        //            Comments = new List<Comment>()
        //            {
        //                new Comment()
        //                {
        //                    Text = "Read More",
        //                    Important = true,
        //                    Owner = null
        //                }
        //            }
        //        };
        //        //User added = task.Owner;

                
        //        var saveTask = tasksService.Create(addedTask, null);
                



        //        var toUpdateTask = new TaskPostModel
        //        {
        //            Title = "Read4",
        //            Description = "Read4",
        //            Added = DateTime.Now,
        //            Deadline = DateTime.Now.AddDays(30),
        //            ClosedAt = null,
        //            Importance = "Low",
        //            State ="Open",
        //            Comments = new List<Comment>
        //            {
        //                new Comment()
        //                {
        //                    Text = "Write More",
        //                    Important = false ,
        //                    Owner = null
        //                }
        //            }
        //        };
        //        //User addedByTask = task.Owner;

        //       // var existing = context.Tasks.AsNoTracking().FirstOrDefaultAsync();
        //        var updatedTask = tasksService.Upsert(saveTask.Id, toUpdateTask);
             

        //        Assert.IsNotNull(toUpdateTask);
        //        Assert.AreEqual(toUpdateTask.Title, updatedTask.Title);
        //        Assert.AreEqual(toUpdateTask.Description, updatedTask.Description);
        //        Assert.AreEqual(toUpdateTask.Deadline, toUpdateTask.Deadline);
        //        Assert.AreEqual(toUpdateTask.Comments, toUpdateTask.Comments);
        //    }
        //}
    }
}