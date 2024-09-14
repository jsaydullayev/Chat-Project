using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities;

public class User
{
    public  Guid Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    [Required]
    public string userName { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public string Gender { get; set; }
    public string? Bio {  get; set; }
    public string? PhotoData { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public byte? Age { get; set; }
    public List<UserChat>? userChats { get; set; }
}