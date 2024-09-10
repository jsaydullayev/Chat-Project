using Chat.Api.Constants;
using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Extensions;
using Chat.Api.Extentions;
using Chat.Api.Models.UserModels;
using Chat.Api.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Chat.Api.Managers
{
    public class UserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserDto>>? GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAllUsers();
            return users.ParseToDtos();
        }
        
        public async Task<UserDto> GetUserById(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(userId);
            return user.ParseToDto();
        }
        
        public async Task<UserDto> Register(CreateUserModel model)
        {
            await CheckForExist(model.UserName);
            var user = new User()
            {
              FirstName = model.FirstName,
              LastName = model.LastName,
              userName = model.UserName,
              Gender = GetGender(model.Gender)
            };
            var passwordHash = new PasswordHasher<User>().HashPassword(user, model.Password);
            user.PasswordHash = passwordHash;
            await _unitOfWork.UserRepository.AddUser(user);
            
            return user.ParseToDto();
        }

        public async Task<string> Login(LoginModel model)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserName(model.Username);
            if (user is null)
                throw new Exception("Username is invalid");
            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash,model.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid password");
            _unitOfWork.ChatRepository.GetAllChats();
            return "Login successfully";
        }

        private async Task CheckForExist(string username)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserName(username);
            if (user is not null) 
            {
                throw new UserExistException();
            }
        }
        
        private string GetGender(string gender)
        {
            var normalizedGender = gender.ToLower();
            return (normalizedGender == UserConstants.Male || normalizedGender == UserConstants.Female)
                ? gender
                : UserConstants.Male;
        }

    }
}
