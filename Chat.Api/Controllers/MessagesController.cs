using Chat.Api.Helpers;
using Chat.Api.Managers;
using Chat.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Chat.Api.Controllers
{ 
    [Route("api/users/user_id/chats/{chatId}/[controller]")] 
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageManager _messageManager;
        private readonly UserHelper _userHelper;

        public MessagesController(MessageManager messageManager,UserHelper userHelper)
        {
            _messageManager = messageManager;
            _userHelper = userHelper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/api/messages")]
        public async Task<IActionResult> GetAllMessages()
        {
            try
            {
                var messages = _messageManager.GetMessages();
                return Ok(messages);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles ="admin,user")]
        [HttpGet("/api/messages/{messageId:int}")]
        public async Task<IActionResult> GetMessageById(int messageId)
        {
            try
            {
                var message = _messageManager.GetMessageById(messageId);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<IActionResult> GetChatMessages(Guid userId)
        {
            try
            {
                var messages = _messageManager.GetChatMessages(userId);
                return Ok(messages);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("{messageId:int}")]
        public async Task<IActionResult> GetChatMessageByid(Guid userId, int messageId)
        {
            try
            {
                var message = _messageManager.GetChatMessageById(userId, messageId);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost("send-text-message")]
        public async Task<IActionResult> SendTextMessage(Guid chatId, [FromBody] TextModel model)
        {
            try
            {
                var userId = _userHelper.GetUserId();
                var message = _messageManager.SendTextMessage(userId, chatId, model);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost("send-file-message")]
        public async Task<IActionResult> SendFileMessage(Guid chatId, [FromBody] FileModel model)
        {
            try
            {
                var userId = _userHelper.GetUserId();
                var message = _messageManager.SendFileMessage(userId, chatId, model);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
