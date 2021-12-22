using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management2.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }


        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("Job title")]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Characters are not allowed.")]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Expected period")]
        public int ExpectedPeriod { get; set; }

        public ICollection<Absence> Absences { get; set; }
    }
}
