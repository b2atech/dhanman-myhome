using Dhanman.MyHome.Domain.Entities.Users;

namespace Dhanman.MyHome.Application.Abstractions.Cryptography;

public interface IPasswordHasher
{
    string HashPassword(Password password);
}
