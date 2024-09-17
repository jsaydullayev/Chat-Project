using Chat.Api.Context;
using Chat.Api.Entities;
using Chat.Api.Exeptions;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Repositories
{
    public class UserRepository(ChatDbContext context) : IUserRepository
    {
        private readonly ChatDbContext _context = context;
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);   
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users;
        }

        public async Task<User> GetUserById(Guid Id)
        { 
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == Id);
            if (user is null)
                throw new UserNotFoundExeption();
        
            return user;
        }

        public async Task<User?> GetUserByUserName(string username)
        {
            var user = await _context.Users.AsNoTracking()
     .SingleOrDefaultAsync(u => u.UserName == username);
            return user;
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(); 
        }
    }
}
