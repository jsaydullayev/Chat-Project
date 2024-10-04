using Chat.Client.Models;
using System.Net;

namespace Chat.Client.Repositories.Contracts;
public interface IUserIntegration
{
    Task<Tuple<HttpStatusCode, string>> Login(LoginModel model);
    Task<Tuple<HttpStatusCode, string>> Register(RegisterModel model);
    Task<Tuple<HttpStatusCode, object>> GetAllusers();
    Task<Tuple<HttpStatusCode, object>> GetProfile();
    Task<Tuple<HttpStatusCode, object>> UpdateUserGeneralInfo(UpdateGeneralInfo model);
}
