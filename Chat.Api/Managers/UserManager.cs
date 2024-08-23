using Chat.Api.Context;
using Chat.Api.DTOs;
using Chat.Api.Models.UserModels;
using Chat.Api.Repositories;

namespace Chat.Api.Managers
{
    public class UserManager(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<List<UserDto>> GetAllUsers(CreateUserModel model)
        {
            var users = await _userRepository.GetAllUsers();

        }
    }
}
