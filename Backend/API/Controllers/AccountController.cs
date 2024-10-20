using API.Models;
using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountController : BaseApiController<AccountController>
    {
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, IAccountService accountService) : base(logger, mapper)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(AuthRequestDto registerRequestDto)
        {
            try
            {
                await _accountService.CreateUserAsync(registerRequestDto.UserName, registerRequestDto.Password);
            }
            catch (UserAlreadyExistsException)
            {
                _logger.Log(LogLevel.Error, "Create account request with username that already exists");
                return BadRequest("Username already exists");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Unexpected error occurred - {ex}");
                return StatusCode(500, "Unexpected error occurred while processing the request");
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(AuthRequestDto loginRequestDto)
        {
            var loginSuccess = false;

            try
            {
                loginSuccess = await _accountService.LogInAsync(loginRequestDto.UserName, loginRequestDto.Password);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Unexpected error occurred - {ex}");
                return StatusCode(500, "Unexpected error occurred while processing the request");
            }

            if (loginSuccess) return Ok();
            else return BadRequest("Username or password incorrect");
        }
    }
}
