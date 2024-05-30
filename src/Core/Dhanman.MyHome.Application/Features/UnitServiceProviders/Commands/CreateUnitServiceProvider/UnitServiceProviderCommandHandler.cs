using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
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
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public UnitServiceProviderCommandHandler(IUnitServiceProviderRespository unitServiceProviderRespository, IMediator mediator, IApplicationDbContext dbContext)
    {
        _unitServiceProviderRespository = unitServiceProviderRespository;
        _mediator = mediator;
        _dbContext = dbContext;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateUnitServiceProviderCommand request, CancellationToken cancellationToken)
    {
        List<UnitServiceProvider> unitServiceProviders = new List<UnitServiceProvider>();

        foreach (int unitId in request.UnitIds)
        {
            var unitServiceProvider = new UnitServiceProvider(request.Id, unitId, request.ServiceProviderId, request.Start, request.End);

            unitServiceProviders.Add(unitServiceProvider);
        }
         await _dbContext.SetInt<UnitServiceProvider>().AddRangeAsync(unitServiceProviders, cancellationToken);
        
        var unitIds = unitServiceProviders.Select(x => x.UnitId).ToList();

        await _mediator.Publish(new UnitServiceProviderCreatedEvent(unitIds), cancellationToken);

        return Result.Success(new EntityCreatedResponse(unitIds));
    }
    #endregion
}
