namespace Chat.Api.Repositories
{
    public interface IChatRepository
    {
        Task<List<Entities.Chat>> GetAllChats();
    }
}
