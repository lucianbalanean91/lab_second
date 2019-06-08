using lab2_restapi_1205_taskmgmt.Models;
using lab2_restapi_1205_taskmgmt.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class CommentsServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void ValidGetAllShouldDisplayAllComments()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidGetAllShouldDisplayAllComments))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var commentService = new CommentService(context);
               
                var added = new lab2_restapi_1205_taskmgmt.ViewModels.CommentPostModel

                {
                    Text = "Write a Book",
                    Important = true,

                };
                var added2 = new lab2_restapi_1205_taskmgmt.ViewModels.CommentPostModel

                {
                    Text = "Read a Book",
                    Important = false,

                };

                commentService.Create(added);
                commentService.Create(added2);

                // Act
                var result = commentService.GetAll(null);

                // Assert

                Assert.AreEqual(0, result.Count());

            }
        }
    }
}
