using Chat.Api.Exeptions;
using Chat.Api.Helpers;
using Chat.Api.Managers;
using Chat.Api.Models.UserModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    public UsersController(UserManager userManager, UserHelper userHelper)
    {
        _userManager = userManager;
        _userHelper = userHelper;
    }
    private readonly UserManager _userManager;
    private readonly UserHelper _userHelper;
    private Guid UserId => _userHelper.GetUserId();
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.GetAllUsers();
        return Ok(users);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAllUserById(Guid userId)
    {
        try
        {
            var user = await _userManager.GetUserById(userId);
            return Ok(user);
        }
        catch (UserNotFoundExeption ex) 
        {
            return NotFound();
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserModel model)
    {
        var result = await _userManager.Register(model);
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginModel model)
    {
        var result = await _userManager.Login(model);
        return Ok(result);
    }

    [Authorize(Roles ="admin,user")]
    [HttpPost("Add-Or-Update-UserPhoto")]
    public async Task<IActionResult> AddOrUpdateUserPhoto([FromForm] FileClass model)
    {
        var result = _userManager.AddOrUpdatePhoto(UserId, model.File);
        return Ok(result);
    }
}

public class FileClass
{
    public IFormFile File { get; set; }
}
