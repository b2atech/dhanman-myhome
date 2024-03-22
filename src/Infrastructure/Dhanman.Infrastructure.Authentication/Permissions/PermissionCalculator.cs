using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Authorization;

namespace Dhanman.Infrastructure.Authentication.Permissions;

internal sealed class PermissionCalculator
{
    private readonly IApplicationDbContext _dbContext;

    internal PermissionCalculator(IApplicationDbContext dbContext) => _dbContext = dbContext;

    internal static async Task<Permission[]> CalculatePermissionsForUserIdAsync(Guid userId)
    {
        await Task.Delay(1);

        return new[]
        {
                Permission.AccessEverything
        };
    }
}