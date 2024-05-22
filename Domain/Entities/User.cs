using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string IdentificationNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Image { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int IdentificationTypeId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }

        [ForeignKey("IdentificationTypeId")]
        public IdentificationType IdentificationType { get; set; }
    }
}
