using API.Models;
using AutoMapper;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var users = await _userRepo.GetUsersAsync();

        return _mapper.Map<List<UserDto>>(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userRepo.GetUserByIdAsync(id);

        if (user == null) return NotFound();
        
        return _mapper.Map<UserDto>(user);
    }
}
