using Chat.Client.Models;
using Chat.Client.Pages.AccountPages;
using System.Net;

namespace Chat.Client.Repositories.Contracts;
public interface IUserIntegration
{
    Task<Tuple<HttpStatusCode, string>> Login(LoginModel model);
    Task<Tuple<HttpStatusCode, string>> Register(RegisterModel model);
    Task<Tuple<HttpStatusCode, object>> GetAllusers();
    Task<Tuple<HttpStatusCode, object>> GetProfile();
}
