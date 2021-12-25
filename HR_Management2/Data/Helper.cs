using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management2.Data
{
    public static class Helper
    {
        public const string Admin = "Admin";
        public static string Employee = "Employee";

        public static List<SelectListItem> GetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text=Helper.Admin, Value = Helper.Admin},
                new SelectListItem{Text=Helper.Employee, Value = Helper.Employee}
            };
        }
    }
}
