namespace Chat.Api.Repositories
{
    public interface IChatRepository
    {
        Task<List<Entities.Chat>> GetAllChats();
        Task<List<Entities.Chat>> GetAllUserChats(Guid userId);
        Task<Entities.Chat> GetUserChatById(Guid chatId,Guid userId);
        Task<Tuple<bool,Entities.Chat?>> CheckChatExist(Guid fromUserId, Guid toUserId);
        Task AddChat(Entities.Chat chat);
        Task DeleteChat(Entities.Chat chat);
        Task UpdateChat(Entities.Chat chat);
    }
}
