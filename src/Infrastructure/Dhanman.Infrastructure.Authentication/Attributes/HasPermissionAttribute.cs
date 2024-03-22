using Dhanman.MyHome.Domain.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Dhanman.Infrastructure.Authentication.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission requiredPermission)
        : base(requiredPermission.ToString())
    {
    }
}
