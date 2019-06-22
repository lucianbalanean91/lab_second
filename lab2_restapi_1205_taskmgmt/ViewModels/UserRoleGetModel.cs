using lab2_restapi_1205_taskmgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_restapi_1205_taskmgmt.ViewModels
{
    public class UserRoleGetModel
    {
        public int Id { get; set; }
        public string Title { get; set; }



        public static UserRoleGetModel FromUserRole (UserRole userRole)
        {
            return new UserRoleGetModel
            {
                Id = userRole.Id,
                Title = userRole.Title
            };
        }
    }
}
