namespace Chat.Api.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public byte? Age { get; set; }
        public List<UserChatDto> UserChats { get; set; }
    }
}
