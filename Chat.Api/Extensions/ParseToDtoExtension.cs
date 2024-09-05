using Chat.Api.DTOs;
using Chat.Api.Entities;
using Mapster;

namespace Chat.Api.Extentions
{
    public static class ParseToDtoExtension
    {
        public static UserDto ParseToDto(this User user)
        {
            return user.Adapt<UserDto>();
        }
        public static List<UserDto> ParseToDtos(this List<User>? users)
        {
            if (users is null || users.Count == 0)
            {
                return new List<UserDto>();
            }
            return users.Select(user => user.ParseToDto()).ToList();
        }
    }
}
