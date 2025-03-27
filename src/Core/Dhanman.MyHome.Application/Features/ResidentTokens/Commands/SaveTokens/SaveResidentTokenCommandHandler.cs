using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Companies.Events;
using Dhanman.MyHome.Domain.Abstractions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.ResidentTokens.Commands.SaveTokens;

public class SaveResidentTokenCommandHandler : ICommandHandler<SaveResidentTokenCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentTokenRepository _residentTokenRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructor
    public SaveResidentTokenCommandHandler(IResidentTokenRepository residentTokenRepository, IMediator mediator)
    {
        _residentTokenRepository = residentTokenRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(SaveResidentTokenCommand request, CancellationToken cancellationToken)
    {
        var residentToken = await _residentTokenRepository.GetByResidentIdAsync(request.ResidentId);

        if(residentToken == null)
        {
            residentToken = new Domain.Entities.ResidentTokens.ResidentToken(Guid.NewGuid(), request.ResidentId, request.FCMToken);
            _residentTokenRepository.Insert(residentToken);
        }
        else
        {
            _residentTokenRepository.Update(residentToken);
        }

        await _mediator.Publish(new CompanyCreatedEvent(residentToken.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(residentToken.Id));
    }
    #endregion
}
