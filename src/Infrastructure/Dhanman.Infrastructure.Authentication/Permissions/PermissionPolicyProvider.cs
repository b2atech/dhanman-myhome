﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Dhanman.Infrastructure.Authentication.Permissions;

internal sealed class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionPolicyProvider"/> class.
    /// </summary>
    /// <param name="options">The authorization options.</param>
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }

    /// <inheritdoc />
    public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName) =>
        await base.GetPolicyAsync(policyName) ??
        new AuthorizationPolicyBuilder().AddRequirements(new PermissionRequirement(policyName)).Build();
}