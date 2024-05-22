using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserContract
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int ContractId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int StateId { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
