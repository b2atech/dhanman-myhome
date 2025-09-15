using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Features.Companies.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Application.Features.UserFcmTokens;

public class SaveUserFcmTokenCommandHandler : ICommandHandler<SaveUserFcmTokenCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IUserFcmTokenRepository _userFcmTokenRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructor
    public SaveUserFcmTokenCommandHandler(IUserFcmTokenRepository userFcmTokenRepository, IMediator mediator)
    {
        _userFcmTokenRepository = userFcmTokenRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(SaveUserFcmTokenCommand request, CancellationToken cancellationToken)
    {
        var userFcmToken = await _userFcmTokenRepository.GetByUserIdAndDeviceIdAsync(request.UserId, request.DeviceId);

        if (userFcmToken == null)
        {
            // For identity, use parameterless constructor and set properties.
            userFcmToken = new Domain.Entities.UserFcmTokens.UserFcmToken
            {
                UserId = request.UserId,
                DeviceId = request.DeviceId,
                FCMToken = request.FCMToken
            };
            _userFcmTokenRepository.Insert(userFcmToken);
        }
        else
        {
            userFcmToken.FCMToken = request.FCMToken;
            _userFcmTokenRepository.Update(userFcmToken);
        }

        // If you have a domain event, publish that instead. For now, this matches your pattern.
        await _mediator.Publish(new EntityCreatedResponse(userFcmToken.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(userFcmToken.Id));
    }
    #endregion
}