namespace Dhanman.MyHome.Domain.Exceptions.Base;

public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message)
        : base(message)
    {
    }
}
