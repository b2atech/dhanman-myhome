﻿using Dhanman.MyHome.Domain.Authorization;
using System.ComponentModel;
using System.Security.Claims;

namespace Dhanman.Infrastructure.Authentication.Permissions;

internal static class PermissionExtensions
{
    internal static IEnumerable<Permission> UnpackPermissions(this string packedPermissions) =>
        packedPermissions.Split(',').Select(Enum.Parse<Permission>);

    internal static Claim? FindPermissionsClaim(this IEnumerable<Claim> claims) =>
        claims.SingleOrDefault(c => c.Type == PermissionConstants.PermissionsClaimType);

    internal static bool CheckIfPermissionIsAllowed(this string packedPermissions, string permissionName)
    {
        Permission[] permissions = packedPermissions.UnpackPermissions().ToArray();

        if (!Enum.TryParse(permissionName, true, out Permission permission))
        {
            throw new InvalidEnumArgumentException($"{permissionName} could not be converted to a {nameof(Permission)}.");
        }

        return permissions.CheckIfPermissionIsAllowed(permission);
    }

    internal static bool CheckIfPermissionIsAllowed(this Permission[] permissions, Permission permission) =>
            permissions.Contains(permission) || permissions.Contains(Permission.AccessEverything);
}