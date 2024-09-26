using Chat.Api.Entities;

namespace Chat.Api.Repositories
{
    public interface IuserIntegration
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserByUserName(string username);
        Task<User> GetUserById(Guid Id);
        Task AddUser(User user);
        Task DeleteUser(User user);
        Task UpdateUser(User user);
    }
}
