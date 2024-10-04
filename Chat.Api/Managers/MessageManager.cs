using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Extentions;
using Chat.Api.Models;
using Chat.Api.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Chat.Api.Managers
{
    public class MessageManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _hostEnvironment;

        public MessageManager(IUnitOfWork unitOfWork, IHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<List<MessageDto>> GetMessages()
        {
            var messages = await _unitOfWork.MessageRepository.GetMessages();
            return messages.ParseToDtos();
        }

        public async Task<List<MessageDto>> GetChatMessages(Guid chatId)
        {
            var messages = await _unitOfWork
                .MessageRepository.
                GetChatMessages(chatId);
            return messages.ParseToDtos();
        }

        public async Task<MessageDto> GetMessageById(int messageId)
        {
            var message = await _unitOfWork.MessageRepository.GetMessageById(messageId);
            return message.ParseToDto();
        }

        public async Task<MessageDto> GetChatMessageById(Guid userId, int messageId)
        {
            var message = await _unitOfWork.MessageRepository.GetChatMessageById(userId, messageId);
            return message.ParseToDto();
        }

        public async Task<MessageDto> SendTextMessage(Guid userId, Guid chatId, TextModel model)
        {
            await CheckingUserInChat(userId: userId, chatId: chatId);

            var user = await _unitOfWork.userIntegration.GetUserById(userId);

            var message = new Message()
            {
                Text = model.Text,
                FromUserId = userId,
                FromUserName = user.UserName,
                ChatId = chatId
            };
            await _unitOfWork.MessageRepository.AddMessage(message);
            return message.ParseToDto();
        }

        public async Task<MessageDto> SendFileMessage(Guid userId, Guid chatId, FileModel model)
        {
            var user = await _unitOfWork.userIntegration.GetUserById(userId);

            await CheckingUserInChat(userId: userId, chatId: chatId);

            var ms = new MemoryStream();

            await model.File.CopyToAsync(ms);

            var data = ms.ToArray();

            var fileUrl = GetFilePath();

            await File.WriteAllBytesAsync(fileUrl, data);

            var content = new Content()
            {
                FileUrl = fileUrl,
                Type = model.File.ContentType
            };

            var message = new Message()
            {
                FromUserId = userId,
                FromUserName = user.UserName,
                ChatId = chatId,
                ContentId = content.Id,
                Content = content
            };

            await _unitOfWork.MessageRepository.AddMessage(message);
            return message.ParseToDto();
        }

        public string GetFilePath()
        {
            var generalPath = _hostEnvironment.ContentRootPath;

            var name = Guid.NewGuid();

            var fileName = generalPath + "\\wwwroot\\MessageFiles\\" + name;
            return fileName;
        }


        private async  Task CheckingUserInChat(Guid userId, Guid chatId)
        {
            await _unitOfWork.UserChatRepository.GetUserChat(userId, chatId);
        }
    }
}
