using Application.Services;
using Domain.Input;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {

        private readonly IContractsService _contractsService;

        public ContractController(
            IContractsService contractsService)
        {
            _contractsService = contractsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddContract([FromBody] ContractInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _contractsService.AddContract(request));
        }

        [HttpGet]
        [Route("AllContracts")]
        public dynamic GetAllContracts() 
        {
            return "";
        }

        [HttpPost]
        [Route("AddUserContract")]
        public async Task<IActionResult> AddUserContract(UserContractInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _contractsService.AddUserContract(request));
        }

        [HttpPost]
        [Route("GetContractsByEmail")]
        public async Task<IActionResult> GetContractsByUserId([FromBody] SearchUserByEmailInput request)
        {
            var consult = await _contractsService.GetContractsByUserId(request.Email);
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }


        [HttpGet]
        [Route("GetContractById/{id}")]
        public async Task<IActionResult> GetContractsById(string id)
        {
            var consult = await _contractsService.GetContractsByIdDto(id);
            if (consult.ContractFile == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

    }
}
