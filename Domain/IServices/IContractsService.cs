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
    public interface IContractsService
    {
        Task<ResponseApiDto?> AddContract(ContractInput request);

        Task<Contract?> ByName(string name);

        Task<Company?> CompanyByNit(string Nit);

        Task<ResponseApiDto?> AddUserContract(UserContractInput request);

        Task<Contract?> SearchContractById(int id);

        Task<List<UserContractDto>?> GetContractsByUserId(string email);

        Task<ContractDto> GetContractsByIdDto(string id);

    }
}
