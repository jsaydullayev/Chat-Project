using Chat.Api.Exeptions;
using Chat.Api.Helpers;
using Chat.Api.Managers;
using Chat.Api.Models.UserModels;
using Chat.Api.Validators;
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

    [Authorize(Roles = "admin,user")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.GetAllUsers();
        return Ok(users);
    }

    [Authorize(Roles ="admin,user")]
    [HttpGet("profile")]
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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]CreateUserModel model)
    {
        var validator = new CreateUserValidator();
        var state = await validator.ValidateAsync(model);
        if (!state.IsValid)
        {
            return BadRequest(state.Errors);
        }

        var result = await _userManager.Register(model);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var result = await _userManager.Login(model);
            return Ok(result);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "admin,user")]
    [HttpPost("Add-Or-Update-UserPhoto")]
    public async Task<IActionResult> AddOrUpdateUserPhoto([FromForm] FileClass model)
    {
        var result = _userManager.AddOrUpdatePhoto(UserId, model.File);
        return Ok(result);
    }

    [Authorize(Roles = "admin,user")]
    [HttpPost("update-bio")]
    public async Task<IActionResult> UpdateBio([FromBody] string bio)
    {
        var result = _userManager.UpdateBio(UserId, bio);
        return Ok(result);
    }

    [Authorize(Roles = "admin,user")]
    [HttpPost("update-user-general-info")]
    public async Task<IActionResult> UpdateUserGeneralInfo([FromBody] UpdateUserGeneralInfo model)
    {
        try
        {
            var result = _userManager.UpdateUserGeneralInfo(UserId, model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles ="admin,user")]
    [HttpPost("update-username")]
    public async Task<IActionResult> UpdateUsername([FromBody] UpdateUserNameModel model)
    {
        try
        {
            var result = _userManager.UpdateUserName(UserId, model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Copy-Users")]
    public async Task<IActionResult> GetAllCopyUsers()
    {
        var users = await _userManager.GetAllCopyUsers();
        return Ok(users);
    }


}
    public class FileClass
    {
        public IFormFile File { get; set; }
    }
