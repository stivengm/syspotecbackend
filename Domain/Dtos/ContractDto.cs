using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class ContractDto
    {

        public CompanyDto Company { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ContractFile { get; set; }

    }
}
