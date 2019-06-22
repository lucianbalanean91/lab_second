using lab2_restapi_1205_taskmgmt.Models;
using lab2_restapi_1205_taskmgmt.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_restapi_1205_taskmgmt.Services
{
    public interface IUserRoleService
    {
        UserRoleGetModel GetById(int id);
        UserRoleGetModel Delete(int id);
        IEnumerable<UserRoleGetModel> GetAll();
        UserRole Create(UserRolePostModel userRolePostModel);
        UserRoleGetModel Upsert(int id, UserRolePostModel userRolePostModel);
    }

    public class UserRoleService : IUserRoleService
    {
        private TasksDbContext context;

        public UserRoleService(TasksDbContext context)
        {
            this.context = context;
        }

        public UserRole Create(UserRolePostModel userRolePostModel)
        {
            UserRole toAdd = UserRolePostModel.ToUserRole(userRolePostModel);

            context.UserRoles.Add(toAdd);
            context.SaveChanges();
            // return UserRoleGetModel.FromUserRole(toAdd);
            return toAdd;
        }

        public UserRoleGetModel Delete(int id)
        {
            var existing = context
                 .UserRoles
                 .FirstOrDefault(uRole => uRole.Id == id);
            if (existing == null)
            {
                return null;
            }

            context.UserRoles.Remove(existing);
            context.SaveChanges();

            return UserRoleGetModel.FromUserRole(existing);
        }

        public IEnumerable<UserRoleGetModel> GetAll()
        {
            return context.UserRoles.Select(userRol => UserRoleGetModel.FromUserRole(userRol));
        }

        public UserRoleGetModel GetById(int id)
        {
            UserRole userRole = context
                .UserRoles
                .AsNoTracking()
                .FirstOrDefault(uRole => uRole.Id == id);

            return UserRoleGetModel.FromUserRole(userRole);
        }

        public UserRoleGetModel Upsert(int id, UserRolePostModel userRolePostModel)
        {
            var existing = context.UserRoles.AsNoTracking().FirstOrDefault(uRole => uRole.Id == id);
            if (existing == null)
            {
                
                UserRole toAdd = UserRolePostModel.ToUserRole(userRolePostModel);
                context.UserRoles.Add(toAdd);
                context.SaveChanges();
                return UserRoleGetModel.FromUserRole(toAdd);
            }

            UserRole Update = UserRolePostModel.ToUserRole(userRolePostModel);
            Update.Id = id;
            context.UserRoles.Update(Update);
            context.SaveChanges();
            return UserRoleGetModel.FromUserRole(Update);
        }
    }
}

