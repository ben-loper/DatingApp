namespace Infrastructure.Exceptions
{
    public class UserDoesNotExistException(string message) : Exception(message)
    {
    }
}
