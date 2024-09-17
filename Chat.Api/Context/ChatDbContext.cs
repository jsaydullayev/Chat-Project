using Chat.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Context;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options){ }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Entities.Chat> Chats { get; set; }
    public DbSet<UserChat> UserChats { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var password = "admin";
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Admin",
            LastName = "Admin",
            UserName = "admin",
            Gender = Constants.UserConstants.Male,
            Role = Constants.UserConstants.AdminRole
        };

        var passwordHash = new PasswordHasher<User>().HashPassword(user, password);
        user.PasswordHash = passwordHash;

        modelBuilder.Entity<User>().HasData(new List<User>() 
        {
            user
        });
    }
}