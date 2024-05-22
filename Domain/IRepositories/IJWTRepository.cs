using Domain.Dtos;
using Domain.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IJWTRepository
    {
        Task<ResponseApiDto> Authentication(LoginInput request);
    }
}
