using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Authorization;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class PermissionsController : ApiController
{
    private readonly IPermissionService _permissionService;
    public PermissionsController(IMediator mediator, IUserContextService userContextService, IPermissionService permissionService)
        : base(mediator, userContextService)
    {
        _permissionService = permissionService;
    }

    #region UserControllers

    // Endpoint to clear the cache for all permissions
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.Admin")]
    [HttpPost(ApiRoutes.Permissions.ClearAllPermissionsCache)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult ClearAllPermissionsCache()
    {
        try
        {
            _permissionService.ClearAllPermissionsCache();
            return Ok(new { message = "All permissions cache cleared successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Error while clearing all permissions cache.", details = ex.Message });
        }
    }

    // Endpoint to clear the cache for specific user's permissions
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Write")]
    [HttpPost(ApiRoutes.Permissions.ClearUserPermissionsCache)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult ClearUserPermissionsCache(Guid userId, Guid organizationId)
    {
        try
        {
            _permissionService.ClearUserPermissionsCache(userId, organizationId);
            return Ok(new { message = "User permissions cache cleared successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Error while clearing user permissions cache.", details = ex.Message });
        }
    }

    #endregion

}