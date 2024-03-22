namespace Dhanman.MyHome.Domain.Authorization;

public sealed class UserPaidModules
{
    public UserPaidModules(Guid userId, PaidModules paidModules)
     : this()
    {
        UserId = userId;
        PaidModules = paidModules;
    }

    private UserPaidModules()
    {
    }

    public Guid UserId { get; }

    public PaidModules PaidModules { get; }
}