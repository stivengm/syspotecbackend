using Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class UserContractDto
    {
        public int Id { get; set; }

        public ContractDto Contract { get; set; }

        public StateDto State { get; set; }

        public UserDto User { get; set; }

        public string UserName { get; set; }

    }
}
