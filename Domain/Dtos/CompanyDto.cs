using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class CompanyDto
    {

        public string Name { get; set; }

        public string Nit { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
