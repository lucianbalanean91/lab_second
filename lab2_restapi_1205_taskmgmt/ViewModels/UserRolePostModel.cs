using lab2_restapi_1205_taskmgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_restapi_1205_taskmgmt.ViewModels
{
    public class UserRolePostModel
    {   
      
        public string Title { get; set; }

        public static UserRole ToUserRole (UserRolePostModel userRolePostModel)
        {
            return new UserRole
            {
                Title = userRolePostModel.Title
            };
        }
    }
}
