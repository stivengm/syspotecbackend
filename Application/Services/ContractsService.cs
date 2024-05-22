using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.Input;
using Domain.IRepositories;
using Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContractsService : IContractsService
    {
        private readonly IContractsRepository _contractsRepository;
        private readonly IUserService _userService;

        public ContractsService(
            IContractsRepository contracstRepository, IUserService userService)
        {
            _contractsRepository = contracstRepository;
            _userService = userService;
        }

        public async Task<ResponseApiDto?> AddContract(ContractInput request)
        {
            var response = new ResponseApiDto();

            var user = await _contractsRepository.ByName(request.Name);
            if (user == null)
            {
                var consultCompany = await _contractsRepository.CompanyByNit(request.CompanyId);
                if (consultCompany != null)
                {
                    Contract model = new()
                    {
                        CompanyId = consultCompany.Id,
                        StateId = (int)StateEnum.Active,
                        Name = request.Name,
                        Description = request.Description,
                        ContractFile = request.ContractFile,
                    };

                    var responseAdd = await _contractsRepository.AddContract(model);
                    if (responseAdd == 1)
                    {
                        response.Code = "TRX001";
                        response.Message = "Se ha guardado el documento correctamente";
                    }
                    else
                    {
                        response.Code = "TRX002";
                        response.Message = "Ocurrio un error inesperado al guardar el contrato.";
                    }
                }
                else
                {
                    response.Code = "TRX003";
                    response.Message = "La compañía no existe.";
                }
            }
            else
            {
                response.Code = "TRX003";
                response.Message = "El contrato ya se encuentra registrado.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> AddUserContract(UserContractInput request)
        {
            var response = new ResponseApiDto();

            var consultContract = await _contractsRepository.SearchContractById(int.Parse(request.ContractId));
            if (consultContract != null)
            {
                var consultUser = await _userService.SearchByEmail(request.Email);
                if (consultUser != null)
                {
                    var consultUserContract = await _contractsRepository.UserContractFilter(consultContract.Id, consultUser.Id);
                    if (consultUserContract == null)
                    {
                        UserContract model = new()
                        {
                            ContractId = consultContract.Id,
                            UserId = consultUser.Id,
                            StateId = (int)StateEnum.Assigned,
                            UserName = request.UserName
                        };

                        var responseAdd = await _contractsRepository.AddUserContract(model);
                        if (responseAdd == 1)
                        {
                            response.Code = "TRX001";
                            response.Message = "Se ha asignado el contrato.";
                        }
                        else
                        {
                            response.Code = "TRX002";
                            response.Message = "Ocurrio un error inesperado al asignar el contrato.";
                        }
                    }
                    else
                    {
                        response.Code = "TRX002";
                        response.Message = "El contrato ya fue asignado anteriormente a este usuario.";
                    }
                }
                else
                {
                    response.Code = "TRX003";
                    response.Message = "El usuario no existe.";
                }
            }
            else
            {
                response.Code = "TRX003";
                response.Message = "El contrato no existe.";
            }

            return response;
        }

        public async Task<Contract?> SearchContractById(int id)
        {
            return await _contractsRepository.SearchContractById(id);
        }

        public async Task<Contract?> ByName(string name)
        {
            return await _contractsRepository.ByName(name);
        }

        public async Task<Company?> CompanyByNit(string Nit)
        {
            return await _contractsRepository.CompanyByNit(Nit);
        }

        public async Task<List<UserContractDto>?> GetContractsByUserId(string email)
        {
            return await _contractsRepository.GetContractsByUserId(email);
        }

        public async Task<ContractDto> GetContractsByIdDto(string id)
        {
            return await _contractsRepository.GetContractsByIdDto(id);
        }
    }
}
