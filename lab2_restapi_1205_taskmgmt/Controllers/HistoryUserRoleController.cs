using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2_restapi_1205_taskmgmt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab2_restapi_1205_taskmgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryUserRoleController : ControllerBase
    {
        private IUserService userService;

        public HistoryUserRoleController(IUserService userservice)
        {
            userService = userservice;
        }



        [HttpGet]
        // [Authorize(Roles = "User_Manager,Admin")]
        public IActionResult GetAll()
        {
            var histories = userService.GetAllHistory();
            return Ok(histories);
        }




    }


}