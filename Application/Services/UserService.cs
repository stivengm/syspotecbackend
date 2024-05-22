using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Input;
using Domain.IServices;

using System.Security.Cryptography;
using Domain.IRepositories;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTRepository _jWTRepository;

        public UserService(
            IUserRepository userRepository,
            IJWTRepository jWTRepository)
        {
            _userRepository = userRepository;
            _jWTRepository = jWTRepository;
        }

        public async Task<ResponseApiDto> Authentication(LoginInput request)
        {
            request.Password = EcryptText(request.Password);
            return await _jWTRepository.Authentication(request);
        }

        public async Task<ResponseApiDto?> Enrollment(EnrollmentUserInput request)
        {

            var response = new ResponseApiDto();

            var user = await _userRepository.SearchByEmail(request.Email);
            if (user == null)
            {
                var respCompany = await _userRepository.CompanyByNit(request.CompanyNit);
                if (respCompany != null)
                {
                    Users newUser = new()
                    {
                        CompanyId = respCompany.Id,
                        GenderId = request.GenderId,
                        IdentificationTypeId = request.IdentificationTypeId,
                        IdentificationNumber = request.IdentificationNumber,

                        Email = request.Email,
                        Password = EcryptText(request.Password),
                        Name = request.Name,
                        LastName = request.LastName,
                        Phone = request.Phone ?? "",
                        Address = request.Address ?? "",
                        Image = request.Image ?? ""
                    };

                    var responseAdd = await _userRepository.Enrollment(newUser);
                    if (responseAdd == 1)
                    {
                        response.Code = "TRX001";
                        response.Message = "Se ha guardado al usuario correctamente.";
                    }
                    else
                    {
                        response.Code = "TRX002";
                        response.Message = "Ha ocurrido un error al guardar el usuario.";
                    }
                }
                else
                {
                    response.Code = "TRX002";
                    response.Message = "La compañía no existe.";
                }
            }
            else
            {
                response.Code = "TRX002";
                response.Message = "El usuario ya se encuentra registrado.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> UpdateInfoUser(UpdateUserInput request)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.SearchByIdentificationNumber(request.IdentificationNumber);
            if (user == null)
            {
                response.Code = "TRX001";
                response.Message = "El usuario no existe.";
            }
            else
            {
                user.Name = request.Name != null ? request.Name : user.Name;
                user.LastName = request.LastName != null ? request.LastName : user.LastName;
                user.IdentificationNumber = request.IdentificationNumber != null ? request.IdentificationNumber : user.IdentificationNumber;
                user.Email = request.Email != null ? request.Email : user.Email;
                user.Password = request.Password != null ? EcryptText(request.Password) : user.Password;
                user.Image = request.Image != null ? request.Image : user.Image;
                user.Phone = request.Phone != null ? request.Phone : user.Phone;
                user.Address = request.Address != null ? request.Address : user.Address;
                user.GenderId = request.GenderId > 0 ? request.GenderId : user.GenderId;
                user.IdentificationTypeId = request.IdentificationTypeId > 0 ? request.IdentificationTypeId : user.IdentificationTypeId;

                var responseUpdate = await _userRepository.UpdateUser(user);
                if (responseUpdate == 1)
                {
                    response.Code = "TRX001";
                    response.Message = "Se ha guardado la información del usuario.";
                }
                else
                {
                    response.Code = "TRX002";
                    response.Message = "Ocurrio un error al actualizar la información del usuario.";
                }
            }

            return response;
        }

        public async Task<Users?> SearchByEmail(string email)
        {
            return await _userRepository.SearchByEmail(email);
        }

        private static string EcryptText(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (var t in result)
            {
                strBuilder.Append(t.ToString("x2"));
            }

            return strBuilder.ToString();
        }

    }
}
