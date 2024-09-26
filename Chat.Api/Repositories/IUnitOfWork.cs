namespace Chat.Api.Repositories
{
    public interface IUnitOfWork
    {
        IuserIntegration userIntegration { get; }
        IChatRepository ChatRepository { get; }
        IUserChatRepository UserChatRepository { get; }
        IMessageRepository MessageRepository { get; }
    }
}
