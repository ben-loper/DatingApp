namespace Infrastructure.Exceptions
{
    public class TokenKeyTooShortException(string message) : Exception(message)
    {
    }
}
