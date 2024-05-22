﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.User
{
    public class UserDto
    {
        public CompanyDto Company { get; set; }

        public GenderDto Gender { get; set; }

        public IdentificationTypeDto IdentificationType { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string IdentificationNumber { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }

    }
}

