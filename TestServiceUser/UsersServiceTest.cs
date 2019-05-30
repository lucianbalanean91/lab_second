using lab2_restapi_1205_taskmgmt.Models;
using lab2_restapi_1205_taskmgmt.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Tests
{
    public class UsersServiceTest
    {
        private IOptions<AppSettings> config;

        [SetUp]
        public void Setup()
        {
            config = Options.Create(new AppSettings
            {
                Secret = "dsadhjcghduihdfhdifd8ihadandwqdqfefefqwfq"
            });
        }

        [Test]
        public void ValidRegisterCreateANewUser()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidRegisterCreateANewUser))// "ValidRegisterShouldCreateANewUser")
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var userService = new UserService(context, config);
                var addedUser = new lab2_restapi_1205_taskmgmt.ViewModels.RegisterPostModel
                {
                    Email = "pop@yahoo.com",
                    FirstName = "Pop",
                    LastName = "Mihai",
                    Password = "pop123456",
                    Username = "popmihai01"
                };
                var result = userService.Register(addedUser);

                Assert.IsNotNull(result);
                Assert.AreEqual(addedUser.Username, result.Username);
            }
        }

        [Test]
        public void ValidUsernameAndPasswordShouldLoginSuccessfully()
        {

            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidUsernameAndPasswordShouldLoginSuccessfully))// "ValidUsernameAndPasswordShouldLoginSuccessfully")
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var userService = new UserService(context, config);

                var added = new lab2_restapi_1205_taskmgmt.ViewModels.RegisterPostModel

                {
                    Email = "lucian@yahoo.com",
                    FirstName = "Gavrilut",
                    LastName = "Lucian",
                    Password = "12345678",
                    Username = "glucian"
                };
                userService.Register(added);
                var loggedIn = new lab2_restapi_1205_taskmgmt.ViewModels.LoginPostModel
                {
                    Username = "glucian",
                    Password = "12345678"

                };
                var result = userService.Authenticate(added.Username, added.Password);

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Id);
                Assert.AreEqual(loggedIn.Username, result.Username);
            }


        }
    }
}