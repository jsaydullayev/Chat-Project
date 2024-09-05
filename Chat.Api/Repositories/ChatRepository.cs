
using Chat.Api.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories
{
    public class ChatRepository(ChatDbContext context) : IChatRepository
    {
        private readonly ChatDbContext _context = context;
        public async Task<List<Entities.Chat>> GetAllChats()
        {
            var chats = await _context.Chats.AsNoTracking().ToListAsync();
            return chats;
        }
    }
}
