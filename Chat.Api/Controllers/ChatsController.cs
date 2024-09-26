using Chat.Api.Helpers;
using Chat.Api.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [Authorize(Roles = "admin,user")]
    [Route("api/users/user_Id/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ChatManager _chatManager;
        private readonly UserHelper _userHelper;
        public ChatsController(ChatManager chatManager, UserHelper userHelper)
        {
            _chatManager = chatManager;
            _userHelper = userHelper;
        }
        
        [HttpGet("GetAllChats")]
        public async Task<IActionResult> GetUserChats()
        {
            var chats = await _chatManager.GetAllUserChats(_userHelper.GetUserId());
            return Ok(chats);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEnterChat([FromBody] Guid toUserId)
        {
            var chat = await _chatManager.AddOrEnterChat(_userHelper.GetUserId(), toUserId);
            return Ok(chat);
        }

        [HttpDelete("{chatId:guid}")]
        public async Task<IActionResult> DeleteChat(Guid chatId) 
        {
            try
            {
                var result = await _chatManager.DeleteChat(_userHelper.GetUserId(),chatId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
