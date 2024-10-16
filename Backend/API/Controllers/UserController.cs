using API.Models;
using AutoMapper;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;

    public UserController(ILogger<UserController> logger, IUserRepository userRepo, IMapper mapper)
    {
        _logger = logger;
        _userRepo = userRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> Get()
    {
        var users = await _userRepo.GetUsersAsync();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
