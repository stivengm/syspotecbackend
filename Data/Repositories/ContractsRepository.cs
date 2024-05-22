using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ContractsRepository : IContractsRepository
    {

        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public ContractsRepository(
            AppDbContext context,
            IUserRepository userRepository
            )
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<int?> AddContract(Contract model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<Contract?> ByName(string name)
        {
            return await _context.Contract.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name && c.StateId == (int)StateEnum.Active);
        }

        public async Task<Company?> CompanyByNit(string Nit)
        {
            return await _context.Company.AsNoTracking().FirstOrDefaultAsync(c => c.Nit == Nit);
        }

        public async Task<Contract?> SearchContractById(int id)
        {
            return await _context.Contract.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id && c.StateId == (int)StateEnum.Active);
        }

        public async Task<UserContract?> UserContractFilter(int contractId, int userId)
        {
            return await _context.UserContract
                 .AsNoTracking()
                 .Where(c => c.ContractId == contractId && c.UserId == userId)
                 .FirstOrDefaultAsync();
        }

        public async Task<int?> AddUserContract(UserContract model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<UserContractDto>?> GetContractsByUserId(string email)
        {
            List<UserContractDto> response = new List<UserContractDto>();
            var consultUser = await _userRepository.SearchByEmail(email);
            if (consultUser != null)
            {
                var list = await _context.UserContract
                                .AsNoTracking()
                                .Include("Contract")
                                .Include("Contract.Company")
                                .Include("Contract.State")
                                .Include("State")
                                .Where(c => c.UserId == consultUser.Id)
                                .ToListAsync();

                if (list.Count > 0)
                {
                    foreach (UserContract item in list)
                    {
                        response.Add(await UserContractDto(item, consultUser));
                    }
                }
            }

            return response;
        }

        private async Task<UserContractDto> UserContractDto(UserContract? consult, Users users)
        {
            UserContractDto response = new UserContractDto();

            if (consult != null)
            {
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Contract = ContractDto(consult.Contract);
                response.State = objState;
                //response.User = await _userRepository.ByEmailDto(consult.Users.Email);
                response.UserName = users.Name;
            }

            return response;
        }

        public async Task<ContractDto> GetContractsByIdDto(string id)
        {
            var obj = await _context.Contract
                 .AsNoTracking()
                 .Include("Company")
                 .Include("State")
                 .FirstOrDefaultAsync(c => c.Id == int.Parse(id) && c.StateId == (int)StateEnum.Active);

            return ContractDto(obj);
        }

        private ContractDto ContractDto(Contract? consult)
        {
            ContractDto response = new ContractDto();

            if (consult != null)
            {
                CompanyDto objCompany = new CompanyDto();
                objCompany.Name = consult.Company.Name;
                objCompany.Nit = consult.Company.Nit;
                objCompany.Phone = consult.Company.Phone;
                objCompany.Address = consult.Company.Address;

                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Company = objCompany;
                response.Name = consult.Name;
                response.Description= consult.Description;
                response.ContractFile = consult.ContractFile;
            }

            return response;
        }
    }
}
