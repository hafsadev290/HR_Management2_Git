using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management2.Models
{
    public class AbsenceType
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Absence name")]
        [Required]
        public string AbsenceName { get; set; }
    }
}
