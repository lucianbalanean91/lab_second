using lab2_restapi_1205_taskmgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_restapi_1205_taskmgmt.ViewModels
{
    public class HistoryUserRoleGetModel
    {
        public string Username { get; set; }
        public string UserRoleTitle { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }


        public static HistoryUserRoleGetModel FromHistoryUserRole(HistoryUserRole historyUserRole)
        {

            return new HistoryUserRoleGetModel
            {

               Username = historyUserRole.User.Username,
               UserRoleTitle = historyUserRole.UserRole.Title,
               StartTime = historyUserRole.StartTime,
               EndTime = historyUserRole.EndTime


            };
        }
    }
}
