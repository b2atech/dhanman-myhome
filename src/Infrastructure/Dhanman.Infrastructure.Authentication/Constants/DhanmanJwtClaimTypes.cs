using Dhanman.Infrastructure.Authentication.Permissions;

namespace Dhanman.Infrastructure.Authentication.Constants;

internal class DhanmanJwtClaimTypes
{
    internal const string UserId = "userId";

    internal const string Email = "email";

    internal const string Name = "name";

    internal const string Permissions = PermissionConstants.PermissionsClaimType;
}
