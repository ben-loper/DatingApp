namespace Infrastructure.Exceptions
{
    public class IncorrectPasswordException(string message) : Exception(message)
    {
    }
}
