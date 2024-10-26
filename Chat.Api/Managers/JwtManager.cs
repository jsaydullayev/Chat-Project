using Chat.Api.Entities;
using Chat.Api.Helpers;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Chat.Api.Managers
{
    public class JwtManager
    {
        private readonly IConfiguration _configuration;
        private JwtParameters JwtParam {  get; set; }
        public JwtManager(IConfiguration configuration)
        {
            _configuration = configuration;
            JwtParam = _configuration.GetSection("JwtParameters")
                .Get<JwtParameters>()!;
        }

        public string GenerateToken(User user)
        {
            var key = System.Text.Encoding.UTF32.GetBytes(JwtParam.Key);
            var sigingKey = new SigningCredentials(new SymmetricSecurityKey(key), "HS256");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role!)
            };

            var security = new JwtSecurityToken(issuer: JwtParam.Issuer,
                audience: JwtParam.Audience,
                signingCredentials: sigingKey,
                claims: claims, expires: DateTime.Now.AddSeconds(10)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(security);

            return token;
        }
    }
}
