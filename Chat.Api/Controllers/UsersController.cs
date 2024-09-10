﻿using Chat.Api.Exeptions;
using Chat.Api.Managers;
using Chat.Api.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager userManager) : ControllerBase
    {
        private readonly UserManager _userManager = userManager;
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
    }
}
