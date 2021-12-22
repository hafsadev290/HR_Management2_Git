using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management2.Models
{
    public class Absence
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }


        [Required]
        public int Duration { get; set; }

        [DisplayName("Absence type")]
        public int AbsenceTypeId { get; set; }
        [ForeignKey("AbsenceTypeId")]
        public virtual AbsenceType AbsenceType { get; set; }

        [DisplayName("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
