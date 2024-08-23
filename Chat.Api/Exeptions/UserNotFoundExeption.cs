namespace Chat.Api.Exeptions
{
    public class UserNotFoundExeption : Exception
    {
        public UserNotFoundExeption() : base("User not found") { }  
    }
}
