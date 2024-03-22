using Microsoft.AspNetCore.Authorization;

namespace Dhanman.Infrastructure.Authentication.Permissions;

internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    internal PermissionRequirement(string permissionName) => PermissionName = permissionName;

    internal string PermissionName { get; }
}