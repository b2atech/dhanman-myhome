using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Notifications;
using Dhanman.MyHome.Application.Features.Notifications.Commands.SendPushNotifications;
using Dhanman.MyHome.Application.Features.Notifications.Commands.SendUnitPushNotifications;
using Dhanman.MyHome.Application.Features.ResidentTokens.Commands.SaveTokens;
using Dhanman.MyHome.Application.Features.UserFcmTokens;
using Dhanman.MyHome.Domain;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ComminicationController : ApiController
{
    #region Constructor
    public ComminicationController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }
    #endregion

    #region Methods
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Write")]
    [HttpPost(ApiRoutes.PushNotification.CreatePushNotification)]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendPushNotification([FromBody] GuestPushNotificationRequest request) =>
         await Result.Create(request, Errors.General.BadRequest)
        .Map(value => new SendPushNotificationCommand(
               value.ResidentId,
               value.GuestName,
               value.GuestId))
           .Bind(command => Mediator.Send(command))
          .Match(Ok, BadRequest);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Write")]
    [HttpPost(ApiRoutes.PushNotification.RequestApprovalAction)]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RequestApprovalAction([FromBody] RequestApprovalActionRequest request) =>
         await Result.Create(request, Errors.General.BadRequest)
        .Map(value => new SendRequestApprovalActionCommand(
               value.UnitId ,
               value.GuestName,
               value.GuestId))
           .Bind(command => Mediator.Send(command))
          .Match(Ok, BadRequest);

    [HttpPost(ApiRoutes.PushNotification.CreateUnitPushNotification)]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendUnitPushNotification([FromRoute] int unitId, [FromBody] UnitPushNotificationRequest request) =>
    await Result.Create(request, Errors.General.BadRequest)
        .Map(value => new SendRequestApprovalActionCommand(
            unitId,
            value.GuestName,
            value.GuestId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Resident.Write")]
    [HttpPost(ApiRoutes.PushNotification.CreateResidentToken)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateResidentToken([FromBody] CreateResidentTokenRequest request) =>
        await Result.Create(request, Errors.General.BadRequest)
       .Map(value => new SaveResidentTokenCommand(
              value.ResidentId,
              value.FCMToken))
          .Bind(command => Mediator.Send(command))
         .Match(Ok, BadRequest);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Resident.Write")]
    [HttpPost(ApiRoutes.PushNotification.CreateUserFcmToken)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUserFcmToken([FromBody] CreateUserFcmTokenRequest request) =>
    await Result.Create(request, Errors.General.BadRequest)
        .Map(value => new SaveUserFcmTokenCommand(
            value.UserId,
            value.DeviceId,
            value.FCMToken,
            value.Platform))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
    #endregion
}
