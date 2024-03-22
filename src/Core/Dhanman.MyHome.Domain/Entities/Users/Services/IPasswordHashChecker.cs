namespace Dhanman.MyHome.Domain.Entities.Users.Services;

public interface IPasswordHashChecker
{
    bool HashesMatch(string passwordHash, string providedPassword);
}
