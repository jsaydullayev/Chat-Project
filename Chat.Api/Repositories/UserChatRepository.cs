using Chat.Api.Context;
using Chat.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories
{
    public class UserChatRepository : IUserChatRepository
    {
        private readonly ChatDbContext _chatDbContext;
        public UserChatRepository(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }
        public async Task AddUserChat(UserChat userChat)
        {
            await _chatDbContext.UserChats.AddAsync(userChat);
            await _chatDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserChat(UserChat userChat)
        {
            _chatDbContext.UserChats.Remove(userChat);
             await _chatDbContext.SaveChangesAsync();
        }

        public async Task GetUserChat(Guid userId, Guid chatId)
        {
            var userChat = await _chatDbContext.UserChats
                .SingleOrDefaultAsync(u => u.UserId == userId &&
                u.ChatId == chatId);
            if (userChat is null)
                throw new Exception("Not found chat");
        }
    }
}
