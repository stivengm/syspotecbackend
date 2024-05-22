using Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.General
{
    public class Tokens
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public UserDto UserData { get; set; }
    }
}
