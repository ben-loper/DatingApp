using API.Models;
using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Services.Account;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(AuthRequestDto registerRequestDto)
        {
            try
            {
                var userToken = await _accountService.CreateUserAsync(registerRequestDto.UserName, registerRequestDto.Password);

                return new UserDto
                {
                    Jwt = userToken,
                    NameId = registerRequestDto.UserName
                };
            }
            catch (UserAlreadyExistsException)
            {
                _logger.Log(LogLevel.Error, "Create account request with username that already exists");
                return BadRequest("Username already exists");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Unexpected error occurred - {ex}", ex);
                return StatusCode(500, "Unexpected error occurred while processing the request");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(AuthRequestDto loginRequestDto)
        {
            try
            {
                var userToken = await _accountService.LogInAsync(loginRequestDto.UserName, loginRequestDto.Password);

                return new UserDto
                {
                    Jwt = userToken,
                    NameId = loginRequestDto.UserName
                };
            }
            catch (Exception ex) when (ex is UserDoesNotExistException or IncorrectPasswordException)
            {
                _logger.Log(LogLevel.Error, "Failed login attempt for user {loginRequestDto.UserName}", loginRequestDto.UserName);
                return BadRequest("Username or password is incorrect");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Unexpected error occurred - {ex}", ex);
                return StatusCode(500, "Unexpected error occurred while processing the request");
            }
        }
    }
}
