using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Dhanman.Infrastructure.Authentication.Permissions;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    /// <inheritdoc />
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        Claim? permissionsClaim = context.User?.Claims.FindPermissionsClaim();

        if (permissionsClaim is null)
        {
            return Task.CompletedTask;
        }

        if (permissionsClaim.Value.CheckIfPermissionIsAllowed(requirement.PermissionName))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
