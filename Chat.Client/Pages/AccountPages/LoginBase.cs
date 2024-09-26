using Blazored.LocalStorage;
using Chat.Client.Models;
using Chat.Client.Repositories.Contracts;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace Chat.Client.Pages.AccountPages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        IUserIntegration userIntegration { get; set; }
        [Inject]
        NavigationManager Manager { get; set; }
        [Inject]
        ILocalStorageService StorageService { get; set; }

        protected LoginModel Model = new();

        protected async Task LoginClicked()
        {
            var (statusCode, response) = await userIntegration.Login(Model);

            bool isOk = statusCode == System.Net.HttpStatusCode.OK;

            bool isBadRequest = statusCode == HttpStatusCode.BadRequest;

            if (isOk)
            {
                await StorageService.SetItemAsStringAsync("token", response);
                Manager.NavigateTo($"/login-successfully/{response}");
            }
            else if (isBadRequest)
            {
                Manager.NavigateTo($"error/{response}");
            }
        }
    }
}
