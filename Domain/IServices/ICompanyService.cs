using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface ICompanyService
    {
        //Task<List<CompanyDto>?> GetAllCompanies();
        Task<ResponseApiDto?> GetAllCompanies();
    }
}
