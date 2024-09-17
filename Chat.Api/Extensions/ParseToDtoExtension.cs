using Chat.Api.DTOs;
using Chat.Api.Entities;
using Mapster;
using System.Globalization;

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
            var dtos = new List<UserDto>();
            if (users == null || users.Count == 0)
                return dtos;

            dtos.AddRange(users.Select(user => user.ParseToDto()));
            return dtos;
        }

        public static ChatDto ParseToDto(this Entities.Chat chat)
        {
            ChatDto dto = chat.Adapt<ChatDto>();
            return dto;
        }

        public static List<ChatDto> ParseToDtos(this List<Entities.Chat>? chats)
        {
            var dtos = new List<ChatDto>();
            if (chats == null || chats.Count == 0)
                return dtos;

            dtos.AddRange(chats.Select(chat => chat.ParseToDto()));
            return dtos;
        }

        public static MessageDto ParseToDto(this Message message)
        {
            MessageDto dto = message.Adapt<MessageDto>();
            return dto;
        }

        public static List<MessageDto> ParseToDtos(this List<Message>? messages)
        {
            var dtos = new List<MessageDto>();
            if(messages == null || messages.Count == 0)
                return dtos;

            dtos.AddRange(messages.Select(message => message.ParseToDto()));
            return dtos;
        }
    }
}