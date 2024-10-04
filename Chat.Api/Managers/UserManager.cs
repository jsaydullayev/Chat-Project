using Chat.Api.Constants;
using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Exceptions;
using Chat.Api.Exeptions;
using Chat.Api.Extentions;
using Chat.Api.Helpers;
using Chat.Api.Models.UserModels;
using Chat.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Chat.Api.Managers
{
    public class UserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtManager _jwtManager;
        private readonly MemoryCacheManager _memoryCacheManager;
        private const string Key = "users";
        public UserManager(IUnitOfWork unitOfWork, JwtManager jwtManager, MemoryCacheManager memoryCacheManager)
        {
            _unitOfWork = unitOfWork;
            _jwtManager = jwtManager;
            _memoryCacheManager = memoryCacheManager;
        }

        public async Task<List<UserDto>>? GetAllUsers()
        {
            var dtos = _memoryCacheManager.GetDtos(Key);
            if(dtos is not null)
            {
                return (List<UserDto>)dtos;
            }

            var users = await _unitOfWork.userIntegration.GetAllUsers();
            await Set();
            return users.ParseToDtos();
        }

        public async Task<UserDto> GetUserById(Guid userId)
        {
            var dtos = _memoryCacheManager.GetDtos(Key);
            if(dtos is not null)
            {
                List<UserDto> userDtos = (List<UserDto>)dtos;
                
                var userDto = userDtos.SingleOrDefault(u => u.Id == userId);

                if (userDto is null)
                {
                    throw new UserNotFoundExeption();
                }
                return userDto;
            }

            var user = await _unitOfWork.userIntegration.GetUserById(userId);
            await Set();
            return user.ParseToDto();
        }

        public async Task<UserDto> Register(CreateUserModel model)
        {
            await CheckForExist(model.UserName);
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Gender = GetGender(model.Gender),
                Role = UserConstants.UserRole
            };

            if (user.UserName == "super-admin")
            {
                user.Role = UserConstants.AdminRole;
            }

            var passwordHash = new PasswordHasher<User>().HashPassword(user, model.Password);
            user.PasswordHash = passwordHash;
            await _unitOfWork.userIntegration.AddUser(user);

            return user.ParseToDto();
        }

        public async Task<string> Login(LoginModel model)
        {
            var user = await _unitOfWork.userIntegration.GetUserByUserName(model.Username);
            if (user is null)
                throw new Exception("Username is invalid");
            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid password");
            if (string.IsNullOrEmpty(user.Role))
            {
                user.Role = UserConstants.UserRole;
                await _unitOfWork.userIntegration.UpdateUser(user);
            }
            var token = _jwtManager.GenerateToken(user);
            return token;
        }

        public async Task<byte[]> AddOrUpdatePhoto(Guid userId, IFormFile file)
        {
            var user = await _unitOfWork.userIntegration.GetUserById(userId);
            StaticHelper.IsPhoto(file);
            var data = StaticHelper.GetData(file);
            user.PhotoData = data;
            await _unitOfWork.userIntegration.UpdateUser(user);
            await Set();
            return data;
        }

        private async Task CheckForExist(string username)
        {
            var user = await _unitOfWork.userIntegration.GetUserByUserName(username);
            if (user is not null)
            {
                throw new UserExistExeption();
            }
        }

        private string GetGender(string gender)
        {

            var checkingForGenderExist = gender.ToLower() == UserConstants.Male
                                         || gender.ToLower() == UserConstants.Female;


            return checkingForGenderExist ? gender : UserConstants.Male;
        }

        public async Task<UserDto> UpdateBio(Guid userId, string bio)
        {
            var user = await _unitOfWork.userIntegration.GetUserById(userId);
            if (!string.IsNullOrEmpty(bio))
            {

                user.Bio = bio;
                await _unitOfWork.userIntegration.UpdateUser(user);
            }
            await Set();
            return user.ParseToDto();
        }

        public async Task<UserDto> UpdateUserGeneralInfo(Guid userId, UpdateUserGeneralInfo info)
        {
            var user = await _unitOfWork.userIntegration.GetUserById(userId);
            bool check = false;
            if (!string.IsNullOrEmpty(info.Firstname))
            {
                user.FirstName = info.Firstname;
                check = true;
            }

            if (!string.IsNullOrEmpty(info.Lastname))
            {
                user.LastName = info.Lastname;
                check = true;
            }

            if (!string.IsNullOrEmpty(info.Age))
            {

                try
                {
                    byte age = byte.Parse(info.Age);

                    user.Age = age;
                    check = true;
                }
                catch (Exception e)
                {
                    throw new Exception("Age must be number");
                }
            }

            if (check)
            {
                await _unitOfWork.userIntegration.UpdateUser(user);
                await Set();
            }

            return user.ParseToDto();
        }

        public async Task<UserDto> UpdateUserName(Guid userId, UpdateUserNameModel model)
        {
            var user = await _unitOfWork.userIntegration.GetUserById(userId);
            await CheckForExist(model.UserName);
            user.UserName = model.UserName;
            await _unitOfWork.userIntegration.UpdateUser(user);
            await Set();
            return user.ParseToDto();
        }

        private async Task Set()
        {
            var users = await _unitOfWork.userIntegration.GetAllUsers();
            _memoryCacheManager.GetOrUpdateDtos(Key, users.ParseToDtos());
        }
         
    }

}
