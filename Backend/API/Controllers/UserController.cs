using API.Models;
using AutoMapper;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers;

public class UserController : BaseApiController<UserController>
{
    private readonly IUserRepository _userRepo;

    public UserController(ILogger<UserController> logger, IUserRepository userRepo, IMapper mapper) : base(logger, mapper)
    {
        _userRepo = userRepo;
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
