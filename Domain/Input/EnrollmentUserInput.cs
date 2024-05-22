using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Input
{
    public class EnrollmentUserInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string IdentificationNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
        public string? Image { get; set; }

        [Required]
        public string CompanyNit { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        public int IdentificationTypeId { get; set; }
    }
}
