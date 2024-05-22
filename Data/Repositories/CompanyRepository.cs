using Domain.Dtos;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {

        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompanyDto>?> GetAllCompanies()
        {
            List<CompanyDto> response = new List<CompanyDto>();
            var consult = await _context.Company
                                .OrderBy(c => c.Id)
                                .ToListAsync();

            if (consult.Count > 0)
            {
                response.AddRange(consult.AsEnumerable().Select(g => CompanyDto(g)).ToList()!);
            }

            return response;
        }

        private CompanyDto CompanyDto(Company company)
        {
            CompanyDto companyDto = new CompanyDto();

            if (company != null)
            {
                companyDto.Name = company.Name;
                companyDto.Nit = company.Nit;
                companyDto.Phone = company.Phone;
                companyDto.Address = company.Address;
            }

            return companyDto;
        }

    }
}
