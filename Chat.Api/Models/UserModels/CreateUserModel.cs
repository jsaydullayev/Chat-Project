using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Models.UserModels
{
    public class CreateUserModel
    {
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required] 
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } 
        [Required]
        public string Gender { get; set; }
    }
}
