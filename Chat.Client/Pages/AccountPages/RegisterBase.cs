using Chat.Client.Models;
using Chat.Client.Repositories.Contracts;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace Chat.Client.Pages.AccountPages
{
    public class RegisterBase : ComponentBase
    {

        [Inject]
        public IUserIntegration userIntegration { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected RegisterModel Model { get; set; }


        protected override void OnInitialized()
        {

            Model = new();
        }

        protected async Task RegisterClicked()
        {
            var (statusCode, response) =
                await userIntegration.Register(model: Model);

            if (statusCode == HttpStatusCode.OK)
                NavigationManager.NavigateTo("/login");
            else if (statusCode == HttpStatusCode.BadRequest)
                NavigationManager.NavigateTo($"/error/{response}");

            //Custom parsing
            /*var result = await UserIntegration.Register(Model);

            HttpStatusCode statusCode2 = result.Item1;
            string response2 = result.Item2;*/

        }
    }
}
