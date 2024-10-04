using Chat.Client.DTOs;
using Chat.Client.Models;
using Chat.Client.Repositories.Contracts;
using Chat.Client.Service;
using System.Net;
using System.Net.Http.Json;

namespace Chat.Client.Repositories
{
    public class ChatIntegration : IChatIntegration
    {
        private readonly StorageService _storageService;
        private readonly HttpClient _httpClient;

        public async Task<Tuple<HttpStatusCode, object>> GetChat(Guid toUserId)
        {
            await AddTokenToHeader();

            var url = "api/users/user_id/chats";

            var result = await _httpClient.PostAsJsonAsync(url, toUserId);

            var statusCode = result.StatusCode;
            object response;

            if (statusCode == HttpStatusCode.OK)
            {
                response = (await result.Content.ReadFromJsonAsync<ChatDto>())!;
            }
            else
            {
                response = await result.Content.ReadAsStringAsync();
            }
            return new(statusCode, response);
        }


        public async Task<Tuple<HttpStatusCode, object>> GetChatMessages(Guid chatId)
        {
            await AddTokenToHeader();

            var url = $"api/users/user_id/chats/{chatId}/messages";

            var result = await _httpClient.GetAsync(url);

            var statusCode = result.StatusCode;

            object response;

            if(statusCode == HttpStatusCode.OK)
            {
                response = (await result.Content.ReadFromJsonAsync<List<MessageDto>>())!;
            }
            else 
            {
                response = (await result.Content.ReadFromJsonAsync<string>())!;
            }
            return new(statusCode, response);
        }


        public async Task<Tuple<HttpStatusCode, object>> GetUserChats()
        {
            await AddTokenToHeader();

            var url = "api/user/user_id/chats";

            var result = await _httpClient.GetAsync(url);

            var statusCode = result.StatusCode;

            object response;

            if (statusCode == HttpStatusCode.OK)
            {
                response = (await result.Content.ReadFromJsonAsync<List<ChatDto>>())!;
            }
            else
            {
                response = (await result.Content.ReadFromJsonAsync<string>())!;
            }
            return new(statusCode, response);
        }

        public async Task<Tuple<HttpStatusCode, object>> SendTextMessage(Guid chatId, TextModel model)
        {
            await AddTokenToHeader();

            var url = $"api/user/user_id/chats/{chatId}/messages/send-text-message"; 

            var result = await _httpClient.PostAsJsonAsync(url, model);

            var statusCode = result.StatusCode;

            object response;

            if (statusCode == HttpStatusCode.OK)
            {
                response = (await result.Content.ReadFromJsonAsync<MessageDto>())!;
            }
            else
            {
                response = (await result.Content.ReadFromJsonAsync<string>())!;
            }
            return new(statusCode, response);
        }

        private async Task AddTokenToHeader()
        {
            string? token = await _storageService.GetToken();

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
