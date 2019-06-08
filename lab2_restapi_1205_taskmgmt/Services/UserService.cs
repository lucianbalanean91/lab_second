using lab2_restapi_1205_taskmgmt.Models;
using lab2_restapi_1205_taskmgmt.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace lab2_restapi_1205_taskmgmt.Services
{

    public interface IUserService
    {
        UserGetModel Authenticate(string username, string password);
        UserGetModel Register(RegisterPostModel register);
        User GetCurentUser(HttpContext httpContext);
        IEnumerable<UserGetModel> GetAll();

        UserGetModel GetById(int id);
        User Create(UserPostModel userModel);
        UserGetModel Upsert(int id, UserPostModel userPostModel, User userLogat);
        UserGetModel Delete(int id,User addedBy);
    }

    public class UserService : IUserService
    {
        private TasksDbContext dbcontext;

        private readonly AppSettings appSettings;

        public UserService(TasksDbContext context, IOptions<AppSettings> appSettings)
        {
            this.dbcontext = context;
            this.appSettings = appSettings.Value;
        }

        public UserGetModel Authenticate(string username, string password)
        {
            var user = dbcontext.Users
                .SingleOrDefault(x => x.Username == username && x.Password == ComputeSha256Hash(password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                    new Claim(ClaimTypes.UserData, user.DateRegister.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = new UserGetModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token)

            };


            return result;
        }

        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            // TODO: also use salt
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public UserGetModel Register(RegisterPostModel register)
        {
            User existing = dbcontext.Users.FirstOrDefault(u => u.Username == register.Username);
            if (existing != null)
            {
                return null;
            }

            dbcontext.Users.Add(new User
            {
                Email = register.Email,
                LastName = register.LastName,
                FirstName = register.FirstName,
                Password = ComputeSha256Hash(register.Password),
                Username = register.Username,
                DateRegister = DateTime.Now

            });
            dbcontext.SaveChanges();
            return Authenticate(register.Username, register.Password);
        }

        public User GetCurentUser(HttpContext httpContext)
        {
            string username = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            //string accountType = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.AuthenticationMethod).Value;
            //return _context.Users.FirstOrDefault(u => u.Username == username && u.AccountType.ToString() == accountType);

            return dbcontext.Users.FirstOrDefault(u => u.Username == username);
        }


        public IEnumerable<UserGetModel> GetAll()
        {
            // return users without passwords
            return dbcontext.Users.Select(user => new UserGetModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Token = null
            });
        }

        public UserGetModel GetById(int id)
        {
            User user = dbcontext.Users
                .FirstOrDefault(u => u.Id == id);
            return UserGetModel.FromUser(user);
        }

        public User Create(UserPostModel userModel)
        {
            User toAdd = UserPostModel.ToUser(userModel);

            dbcontext.Users.Add(toAdd);
            dbcontext.SaveChanges();
            return toAdd;

        }


        public UserGetModel Upsert(int id, UserPostModel user, User userLogat)
        {
            var existing = dbcontext.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            User toAdd = UserPostModel.ToUser(user);
            if (existing == null)
            {

                dbcontext.Users.Add(toAdd);
                dbcontext.SaveChanges();
                return UserGetModel.FromUser(toAdd);
            }
            if (userLogat.UserRole.Equals(UserRole.User_Manager))
            {
                //  https://www.aspforums.net/Threads/289493/Get-Number-of-months-between-two-dates-in-C/
                var dateRegister = userLogat.DateRegister;
                var dateCurrent = DateTime.Now;
                int months = dateCurrent.Subtract(dateRegister).Days / 30;
                toAdd.Id = id;

                if (months >= 6)
                {
                    dbcontext.Users.Update(toAdd);
                    dbcontext.SaveChanges();
                    return UserGetModel.FromUser(toAdd);
                }
                return null;
                //  dbcontext.Users.Update(toAdd);
                //dbcontext.SaveChanges();
                //return toAdd;
            }
            if (userLogat.UserRole.Equals(UserRole.Admin))
            {
                toAdd.Id = id;
                dbcontext.Users.Update(toAdd);
                dbcontext.SaveChanges();
                return UserGetModel.FromUser(toAdd);
            }
            return null;
        }
        public UserGetModel Delete(int id,User addedBy)
        {
            var existing = dbcontext.Users.FirstOrDefault(u => u.Id == id);
            if (existing == null)
            {
                return null;
            }
            if (addedBy.UserRole.Equals(UserRole.User_Manager))
            {
                //  https://www.aspforums.net/Threads/289493/Get-Number-of-months-between-two-dates-in-C/
                var dateRegister = addedBy.DateRegister;
                var dateCurrent = DateTime.Now;
                int months = dateCurrent.Subtract(dateRegister).Days / 30;
               

                if (months >= 6)
                {
                    dbcontext.Comments.RemoveRange(dbcontext.Comments.Where(u => u.Owner.Id == existing.Id));
                    dbcontext.SaveChanges();
                    dbcontext.Tasks.RemoveRange(dbcontext.Tasks.Where(u => u.Owner.Id == existing.Id));
                    dbcontext.SaveChanges();

                    dbcontext.Users.Remove(existing);
                    dbcontext.SaveChanges();
                    return UserGetModel.FromUser(existing);
                }
                return null;
                //  dbcontext.Users.Update(toAdd);
                //dbcontext.SaveChanges();
                //return toAdd;
            }
            if (addedBy.UserRole.Equals(UserRole.Admin))
            {
                dbcontext.Comments.RemoveRange(dbcontext.Comments.Where(u => u.Owner.Id == existing.Id));
                dbcontext.SaveChanges();
                dbcontext.Tasks.RemoveRange(dbcontext.Tasks.Where(u => u.Owner.Id == existing.Id));
                dbcontext.SaveChanges();

                dbcontext.Users.Remove(existing);
                dbcontext.SaveChanges();
                return UserGetModel.FromUser(existing);
            }
            return null;
        }

    }
}

