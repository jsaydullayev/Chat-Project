using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Chat.Api.Helpers
{
    public class UserHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetUserId()
        {
            var userId =Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return userId;
        }
    }
}
