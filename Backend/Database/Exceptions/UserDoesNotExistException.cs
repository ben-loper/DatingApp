namespace Database.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException(string message) : base(message) { }
    }
}
