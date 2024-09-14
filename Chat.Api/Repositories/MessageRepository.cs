using Chat.Api.Context;
using Chat.Api.Entities;
using Chat.Api.Managers;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;
        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }
        public async Task AddMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> GetChatMessageById(Guid chatId, int messageId)
        {
            var result = await _context.Messages.SingleOrDefaultAsync(ch => ch.ChatId == chatId && ch.Id == messageId);
            if (result == null)
                throw new Exception("Message not found");

            return result;
        }

        public async Task<List<Message>> GetChatMessages(Guid chatId)
        {
            var message = await _context.Messages.Include(m => m.Content).Where(m => m.ChatId == chatId).ToListAsync();
            return message;
        }

        public async Task<Message> GetMessageById(int Id)
        {
            var message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == Id);

            if (message == null)
                throw new Exception("Message not found");

            return message;
        }

        public async Task<List<Message>> GetMessages()
        {
            var messages = await _context.Messages.Include(m => m.Content).ToListAsync();
            return messages;
        }
    }
}
