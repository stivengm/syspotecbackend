using Domain.Dtos.User;
using Domain.Entities;
using Domain.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<int?> Enrollment(Users user);
        Task<Users?> SearchByEmail(string email);
        Task<UserDto?> ValidAuthentication(LoginInput model);
        Task<UserDto> ByEmailDto(string identifier);
        Task<Company?> CompanyByNit(string nit);
        Task<Users?> SearchByIdentificationNumber(string identificationNumber);
        Task<int?> UpdateUser(Users model);
    }
}
