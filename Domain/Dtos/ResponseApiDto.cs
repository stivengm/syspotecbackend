using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class ResponseApiDto
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public virtual object Data { get; set; }
    }
}
