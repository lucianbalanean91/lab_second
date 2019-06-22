using lab2_restapi_1205_taskmgmt;
using lab2_restapi_1205_taskmgmt.Models;
using lab2_restapi_1205_taskmgmt.Services;
using lab2_restapi_1205_taskmgmt.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class UserRolesServiceTest
    {
        private IOptions<AppSettings> config;

        [SetUp]
        public void Setup()
        {
            config = Options.Create(new AppSettings
            {
                Secret = "asadlfdsmvscddscsdvcsdsmdvsvsd"
            });
        }

        [Test]
        public void CreateShouldATitleValid()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(CreateShouldATitleValid))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var userRoleService = new UserRoleService(context);

                var toAdd = new UserRolePostModel
                {
                    Title = "My Boss"
                };

                UserRole userRole =  userRoleService.Create(toAdd);

                Assert.AreEqual(toAdd.Title, userRole.Title);
                Assert.IsNotNull(userRoleService.GetById(userRole.Id));
            }
        }

        [Test]
        public void Upsert()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(Upsert))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var userRoleService = new UserRoleService(context);
                var toAdd = new UserRolePostModel
                {
                    Title = "Moderator"
                };

                var toUpdate = new UserRolePostModel
                {
                    Title = "Reporter"
                };

                UserRole userRoleGetModel = userRoleService.Create(toAdd);
                  context.Entry(userRoleGetModel).State = EntityState.Detached;
                UserRoleGetModel updated = userRoleService.Upsert(userRoleGetModel.Id, toUpdate);

                Assert.AreNotEqual(userRoleGetModel.Title, updated.Title);
            }
        }

        [Test]
        public void Delete()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(Delete))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var userRoleService = new UserRoleService(context);
                var toAdd = new UserRolePostModel
                {
                    Title = "Big Boss"
                };
                UserRole userRole = userRoleService.Create(toAdd);
                userRoleService.Delete(userRole.Id);

                Assert.IsNull(context.UserRoles.Find(userRole.Id));
            }
        }

        [Test]
        public void GetById()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(GetById))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var userRoleService = new UserRoleService(context);
                var toAdd = new UserRolePostModel
                {
                    Title = "My GOD"
                };
                UserRole expected = userRoleService.Create(toAdd);
                UserRoleGetModel actual = userRoleService.GetById(expected.Id);

                Assert.AreNotEqual(expected, actual);
            }
        }

        [Test]
        public void GetAll()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(GetAll))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var userRoleService = new UserRoleService(context);
                var first = new UserRolePostModel
                {
                    Title = "Sobolan"
                };
                var second = new UserRolePostModel
                {
                    Title = "Soarece"
                };
                var third = new UserRolePostModel
                {
                    Title = "Mistret"
                };
                var fourth = new UserRolePostModel
                {
                    Title = "Soparla"
                };
                var fifth = new UserRolePostModel
                {
                    Title = "Girafa"
                };
                userRoleService.Create(first);
                userRoleService.Create(second);
                userRoleService.Create(third);
                userRoleService.Create(fourth);
                userRoleService.Create(fifth);
                context.SaveChanges();

               var populated = userRoleService.GetAll();
               

                Assert.AreEqual(5, populated.Count());
           

            }
        }
    }
}