namespace Dhanman.MyHome.Application.Abstractions.Authentication;

public interface IUserIdentifierProvider
{
    Guid UserId { get; }
}