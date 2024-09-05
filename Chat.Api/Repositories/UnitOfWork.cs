using Chat.Api.Context;

namespace Chat.Api.Repositories
{
    public class UnitOfWork(ChatDbContext context) : IUnitOfWork
    {
        private IUserRepository? _userRepository { get; }
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(context);

        private IChatRepository? _chatRepository { get; }
        public IChatRepository ChatRepository => _chatRepository ?? new ChatRepository(context);
    }
}
