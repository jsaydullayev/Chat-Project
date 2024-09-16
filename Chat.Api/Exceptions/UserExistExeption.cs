namespace Chat.Api.Exceptions
{
    public class UserExistExeption : Exception
    {
        public UserExistExeption() : base("User already exist") { }
    }
}   
