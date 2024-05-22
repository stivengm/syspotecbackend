using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Input
{
    public class UpdateUserInput
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }

        [Required]
        public string IdentificationNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public string? CompanyId { get; set; }
        public int GenderId { get; set; }
        public int IdentificationTypeId { get; set; }

    }
}
