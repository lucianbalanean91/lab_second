﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2_restapi_1205_taskmgmt.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<HistoryUserRole> HistoryUserRole { get; set; }
    }
}
