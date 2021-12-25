using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management2.Models.ViewModels
{
    public class AbsenceVM
    {
        public Absence Absence { get; set; }
        public IEnumerable<SelectListItem> AbsenceType { get; set; }
        public IEnumerable<SelectListItem> Employee { get; set; }
    }
}
