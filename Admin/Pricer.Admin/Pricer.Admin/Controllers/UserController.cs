using Microsoft.AspNetCore.Mvc;
using Pricer.Service.Models;
using Pricer.Service.Services.Abstract;

namespace Pricer.Admin.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<UserViewModel[]> GetAll()
    {
        return await _userService.GetAll();
    }
}