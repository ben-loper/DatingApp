using AutoMapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
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
        public async Task<ActionResult> Register(string username, string password)
        {
            await _accountService.CreateUser(username, password);

            return Ok();
        }
    }
}
