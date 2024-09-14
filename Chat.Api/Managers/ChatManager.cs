using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Extentions;
using Chat.Api.Helpers;
using Chat.Api.Repositories;

namespace Chat.Api.Managers
{
    public class ChatManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChatManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ChatDto>> GetAllChats()
        {
            var chats = await _unitOfWork.ChatRepository.GetAllChats();
            return chats.ParseToDtos();
        }

        public async Task<List<ChatDto>> GetAllUserChats(Guid userId)
        {
            var chats = await _unitOfWork.ChatRepository.GetAllUserChats(userId);
            return chats.ParseToDtos();
        }

        public async Task<ChatDto> GetUserChatById(Guid userId, Guid chatId)
        {
            var chat = await _unitOfWork.ChatRepository.GetUserChatById(userId, chatId);
            return chat.ParseToDto();
        }

        public async Task<ChatDto> AddOrEnterChat(Guid fromUserId, Guid toUserId)
        {
            var (check, chat) = await _unitOfWork.ChatRepository.CheckChatExist(fromUserId, toUserId);
            if (check)
            {
                return chat?.ParseToDto();
            }
            var fromUser = await _unitOfWork.UserRepository.GetUserById(fromUserId);
            var toUser = await _unitOfWork.UserRepository.GetUserById(toUserId);
            List<string> names =
                [
                    StaticHelper.GetFullName(fromUser.FirstName, fromUser.LastName),
                    StaticHelper.GetFullName(toUser.FirstName, toUser.LastName)
                ];
            chat = new()
            {
                ChatNames = names,
            };
            await _unitOfWork.ChatRepository.AddChat(chat);

            var fromUserChat = new UserChat()
            {
                UserId = toUserId,
                ChatId = chat.Id,
                ToUserId = fromUserId
            };

            var toUserChat = new UserChat()
            {
                UserId = toUserId,
                ChatId = chat.Id,
                ToUserId = fromUserId
            };

            await _unitOfWork.UserChatRepository.AdduserChat(fromUserChat);
            return chat.ParseToDto();
        }

        public async Task<string> DeleteChat(Guid userId, Guid chatId)
        {
            var chat = await _unitOfWork.ChatRepository.GetUserChatById(userId,chatId);
            await _unitOfWork.ChatRepository.DeleteChat(chat);
            return "Deleted successfuly";
        }
    }
}