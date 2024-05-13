using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.UnitServiceProviders.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.UnitServiceProviders;
using MediatR;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Commands.CreateUnitServiceProvider;

internal class UnitServiceProviderCommandHandler : ICommandHandler<CreateUnitServiceProviderCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IUnitServiceProviderRespository _unitServiceProviderRespository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public UnitServiceProviderCommandHandler(IUnitServiceProviderRespository unitServiceProviderRespository, IMediator mediator)
    {
        _unitServiceProviderRespository = unitServiceProviderRespository;
        _mediator = mediator;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateUnitServiceProviderCommand request, CancellationToken cancellationToken)
    {

        var unitServiceProvider = new UnitServiceProvider(request.Id, request.UnitId, request.ServiceProviderId, request.Start, request.End);

        _unitServiceProviderRespository.InsertInt(unitServiceProvider);

        await _mediator.Publish(new UnitServiceProviderCreatedEvent(unitServiceProvider.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(unitServiceProvider.Id));
    }
    #endregion
}
