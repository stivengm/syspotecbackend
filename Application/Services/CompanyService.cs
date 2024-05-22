using Domain.Dtos;
using Domain.General;
using Domain.IRepositories;
using Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(
            ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ResponseApiDto?> GetAllCompanies()
        {
            var response = new ResponseApiDto();
            var companies = await _companyRepository.GetAllCompanies();

            if (companies != null)
            {
                response.Code = "TRX001";
                response.Message = "Se han encontrado compañías.";
                response.Data = companies;
            }
            else
            {
                response.Code = "TRX002";
                response.Message = "No se ha encontrado ninguna Compañía.";
            }

            return response;
        }
    }
}
