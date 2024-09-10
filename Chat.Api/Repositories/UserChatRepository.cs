using Chat.Api.Context;
using Chat.Api.Entities;

namespace Chat.Api.Repositories
{
    public class UserChatRepository : IUserChatRepository
    {
        private readonly ChatDbContext _chatDbContext;
        public UserChatRepository(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }
        public async Task AdduserChat(UserChat userChat)
        {
            await _chatDbContext.UserChats.AddAsync(userChat);
            await _chatDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserChat(UserChat userChat)
        {
            _chatDbContext.UserChats.Remove(userChat);
             await _chatDbContext.SaveChangesAsync();
        }
    }
}
