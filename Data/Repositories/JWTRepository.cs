using Domain.Dtos;
using Domain.General;
using Domain.Input;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class JWTRepository : IJWTRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration iconfiguration;

        public JWTRepository(IConfiguration iconfiguration, IUserRepository userRepository)
        {
            this.iconfiguration = iconfiguration;
            _userRepository = userRepository;
        }

        public async Task<ResponseApiDto> Authentication(LoginInput request)
        {
            var response = new ResponseApiDto();
            var valid = await _userRepository.ValidAuthentication(request);

            if (valid?.IdentificationNumber == null)
            {
                response.Code = "TRX003";
                response.Message = "Email o Password incorrectos.";
                response.Data = new List<dynamic>();
                return response;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                new Claim(ClaimTypes.Name, valid.Name),
                new Claim(ClaimTypes.Email, request.Email),
              }),
                Expires = DateTime.UtcNow.AddMinutes(1440),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            response.Code = "TRX001";
            response.Message = "Sesión correctamente iniciada.";
            var data = new List<dynamic>();
            var informationData = new Tokens { Token = tokenHandler.WriteToken(token), RefreshToken = DateTime.UtcNow.AddMinutes(1440).ToString(), UserData = valid };
            data.Add(informationData);

            response.Data = data;

            return response;

        }
    }
}
