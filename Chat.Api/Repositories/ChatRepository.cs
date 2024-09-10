
using Chat.Api.Context;
using Chat.Api.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories
{
    public class ChatRepository(ChatDbContext context) : IChatRepository
    {
        private readonly ChatDbContext _context = context;

        public async Task AddChat(Entities.Chat chat)
        {
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<bool, Entities.Chat?>> CheckChatExist(Guid fromUserId, Guid toUserId)
        {
            var userChat = await _context.UserChats.FirstOrDefaultAsync(uc => uc.UserId == fromUserId && uc.ToUserId == toUserId);
            if (userChat is not null) 
            {
                var chat = await GetUserChatById(userChat.UserId, userChat.ChatId);
                return new(true, chat);
            }
            return new(false, null);
        }

        public async Task DeleteChat(Entities.Chat chat)
        {
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Entities.Chat>> GetAllChats()
        {
            var chats = await _context.Chats.AsNoTracking().ToListAsync();
            return chats;
        }

        public async Task<List<Entities.Chat>> GetAllUserChats(Guid userId)
        {
            var userchats = await _context.UserChats.Where(u => u.UserId == userId).ToListAsync();
            List<Entities.Chat> sortedChats = new();
            var check = userchats == null || userchats.Count == 0;
            if (check)
            {
                return sortedChats;
            }
            foreach (var chat in userchats)
            {
                var sortedChat = await _context.Chats.SingleAsync(ch => ch.Id == chat.ChatId);
                sortedChats.Add(sortedChat);
            }
            return sortedChats;
        }

        public async Task<Entities.Chat> GetUserChatById(Guid chatId, Guid userId)
        {
            var userChat = await _context.UserChats.SingleOrDefaultAsync(uc => uc.UserId == userId && uc.ChatId == userId);
            if (userChat is null)
            {
                throw new ChatNotFoundException();
            }
            var chat = await _context.Chats.SingleAsync(c => c.Id == userChat.ChatId);
            return chat;
        }

        public async Task UpdateChat(Entities.Chat chat)
        {
            _context.Chats.Update(chat);
            await _context.SaveChangesAsync();
        }
    }
}
