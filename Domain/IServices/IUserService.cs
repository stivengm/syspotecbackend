using Domain.Dtos;
using Domain.Entities;
using Domain.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IUserService
    {
        Task<ResponseApiDto> Authentication(LoginInput request);
        Task<ResponseApiDto?> Enrollment(EnrollmentUserInput request);
        Task<ResponseApiDto?> UpdateInfoUser(UpdateUserInput request);

        Task<Users?> SearchByEmail(string email);
        
    }
}
