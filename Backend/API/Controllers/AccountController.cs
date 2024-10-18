using AutoMapper;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class AccountController : BaseApiController<AccountController>
    {
        public AccountController(ILogger<AccountController> logger, IMapper mapper) : base(logger, mapper)
        {
        }
    }
}
