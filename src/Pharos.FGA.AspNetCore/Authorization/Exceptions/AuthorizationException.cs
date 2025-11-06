namespace Pharos.FGA.AspNetCore.Authorization.Exceptions;

public class AuthorizationException : Exception
{
    public AuthorizationException(string message, Exception? ex = null) : base(message, ex)
    {
    }
}