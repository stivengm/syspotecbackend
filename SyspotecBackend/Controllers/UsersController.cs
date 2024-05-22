using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Domain.Entities;
using Domain.Input;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SyspotecBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("authentication")]
        public async Task<ActionResult<IEnumerable<Users>>> Authentication([FromBody] LoginInput request )
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok( await _userService.Authentication(request) );
        }

        [HttpPost]
        [Route("enrollment")]
        public async Task<ActionResult> Enrollment([FromBody] EnrollmentUserInput request)
        {

            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.Enrollment(request));

        }

        // TODO: Crear un SearchByEmail

        [HttpPut]
        public async Task<IActionResult> UpdateInfoUser([FromBody] UpdateUserInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.UpdateInfoUser(request));
        }

    }
}
