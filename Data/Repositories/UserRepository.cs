using Domain.Dtos;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Input;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Enrollment(Users user)
        {
            await _context.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<Users?> SearchByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Users?> SearchByIdentificationNumber(string identificationNumber)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(c => c.IdentificationNumber == identificationNumber);
        }

        public async Task<UserDto?> ValidAuthentication(LoginInput model)
        {
            var consult = await _context.Users
                 .AsNoTracking()
                 .Include("Company")
                 .Include("Gender")
                 .Include("IdentificationType")
                 .FirstOrDefaultAsync((u => u.Email == model.Email && u.Password == model.Password));

            return await UserDto(consult);
        }

        private async Task<UserDto> UserDto(Users? consult)
        {
            UserDto response = new UserDto();

            if (consult != null)
            {

                CompanyDto objCompany = new CompanyDto();
                objCompany.Name = consult.Company.Name;
                objCompany.Nit = consult.Company.Nit;
                objCompany.Phone = consult.Company.Phone;
                objCompany.Address = consult.Company.Address;


                GenderDto objGender = new GenderDto();
                objGender.Id = consult.Gender.Id;
                objGender.Name = consult.Gender.Name;

                IdentificationTypeDto objIdentificationType = new IdentificationTypeDto();
                objIdentificationType.Id = consult.IdentificationType.Id;
                objIdentificationType.Name = consult.IdentificationType.Name;


                response.Company = objCompany;
                response.Gender = objGender;
                response.IdentificationType = objIdentificationType;
                response.Email = consult.Email;
                response.Name = consult.Name;
                response.LastName = consult.LastName;
                response.IdentificationNumber = consult.IdentificationNumber;
                response.Phone = consult.Phone;
                response.Address = consult.Address;
                response.Image = consult.Image;

                //response.File = await AllFileByUser(consult.Identifier);
            }

            return response;
        }

        public async Task<UserDto> ByEmailDto(string email)
        {
            var obj = await _context.Users
                 .AsNoTracking()
                 .Include("Company")
                 .Include("Gender")
                 .Include("TypeIdentification")
                 .FirstOrDefaultAsync(c => c.Email == email);

            return await UserDto(obj);
        }

        public async Task<Company?> CompanyByNit(string Nit)
        {
            return await _context.Company.AsNoTracking().FirstOrDefaultAsync(c => c.Nit == Nit);
        }

        public async Task<int?> UpdateUser(Users model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }
    }
}
