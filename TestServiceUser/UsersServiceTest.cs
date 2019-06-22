using lab2_restapi_1205_taskmgmt;
using lab2_restapi_1205_taskmgmt.Constants;
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
        public void ValidRegisterShouldCreateANewUser()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidRegisterShouldCreateANewUser))// "ValidRegisterShouldCreateANewUser")
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var userService = new UserService(context, config);
                var addedUser = new RegisterPostModel
                {
                    Email = "pop@yahoo.com",
                    FirstName = "Pop",
                    LastName = "Mihai",
                    Password = "pop123456",
                    Username = "popmihai01",
                    DateRegister = DateTime.Now,
                    
                   
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
                Assert.AreEqual(5, result.Id);
                Assert.AreEqual(loggedIn.Username, result.Username);
            }


        }

        [Test]
        public void ValidGetAllShouldDisplayAllUsers()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidGetAllShouldDisplayAllUsers))
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
                //var added2 = new lab2_restapi_1205_taskmgmt.ViewModels.RegisterPostModel

                //{
                //    Email = "pop@yahoo.com",
                //    FirstName = "Pop",
                //    LastName = "Mihai",
                //    Password = "pop123456",
                //    Username = "pmihai"
                //};


                UserGetModel userGetModel = userService.Register(added);
               // userService.Register(added2);

                // Act
                var result = userService.GetAll();

                // Assert

                Assert.AreEqual(1, result.Count());

            }
        }


        [Test]
        public void ValidGetAllShouldDisplayAllHistoryUserRole()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidGetAllShouldDisplayAllHistoryUserRole))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var userService = new UserService(context, config);

                var added = new HistoryUserRole

                {
                    User = new User
                    {
                        Username = "Mircea",
                    },
                    UserRole = new UserRole
                    {
                        Title = "Asistent"
                    },
                    StartTime = DateTime.Now,
                    EndTime = null
                    
                };
                var added2 = new HistoryUserRole

                {
                    User = new User
                    {
                        Username = "Claudiu",
                    },
                    UserRole = new UserRole
                    {
                        Title = "Moderator"
                    },
                    StartTime = DateTime.Now.AddDays(10),
                    EndTime = null

                };

                context.HistoryUserRoles.Add(added);
                context.HistoryUserRoles.Add(added2);

                // Act
                var result = userService.GetAllHistory();

                // Assert

                Assert.AreEqual(0, result.Count());
                Assert.AreNotEqual(2, result.Count());

            }
        }
    }
}