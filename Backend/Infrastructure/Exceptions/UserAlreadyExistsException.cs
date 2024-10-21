namespace Infrastructure.Exceptions
{
    public class UserAlreadyExistsException(string message) : Exception(message)
    {
    }
}
