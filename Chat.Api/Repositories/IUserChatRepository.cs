using Chat.Api.Entities;

namespace Chat.Api.Repositories
{
    public interface IUserChatRepository
    {
        Task AdduserChat(UserChat userChat);
        Task DeleteUserChat(UserChat userChat);
    }
}
