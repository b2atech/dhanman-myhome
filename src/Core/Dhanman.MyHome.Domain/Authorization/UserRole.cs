namespace Dhanman.MyHome.Domain.Authorization;

public sealed class UserRole
{
    public UserRole(Guid userId, string roleName)
        : this()
    {
        UserId = userId;
        RoleName = roleName;
    }

    private UserRole()
    {
        RoleName = string.Empty;
    }

    public Guid UserId { get; }

    public string RoleName { get; }
}
