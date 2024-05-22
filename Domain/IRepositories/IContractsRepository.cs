using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IContractsRepository
    {
        Task<int?> AddContract(Contract model);
        Task<Contract?> ByName(string name);
        Task<Company?> CompanyByNit(string Nit);
        Task<Contract?> SearchContractById(int id);
        Task<UserContract?> UserContractFilter(int contractId, int userId);
        Task<int?> AddUserContract(UserContract model);
        Task<ContractDto> GetContractsByIdDto(string identifier); 
        Task<List<UserContractDto>?> GetContractsByUserId(string email);
    }
}
