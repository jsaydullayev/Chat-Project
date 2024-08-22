using Chat.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Context;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options){ }
    
    public DbSet<User> users { get; set; }
    public DbSet<Entities.Chat> chats { get; set; }
    public DbSet<UserChat> userChats { get; set; }
    public DbSet<Message> messages { get; set; }
    public DbSet<Content> contents { get; set; }
}