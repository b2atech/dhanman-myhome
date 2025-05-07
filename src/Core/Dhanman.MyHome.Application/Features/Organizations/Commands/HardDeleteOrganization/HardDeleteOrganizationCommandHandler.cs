using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Organizations.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;


namespace Dhanman.MyHome.Application.Features.Organizations.Commands.HardDeleteOrganization;

public class HardDeleteOrganizationCommandHandler : ICommandHandler<HardDeleteOrganizationCommand, Result<EntityDeletedResponse>>
{
    #region Properties

    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public HardDeleteOrganizationCommandHandler(
    IApplicationDbContext dbContext,
        IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }


    #endregion

    #region Methodes
    public async Task<Result<EntityDeletedResponse>> Handle(HardDeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.Database.ExecuteSqlRawAsync(
                   "CALL public.hard_delete_org_community(@p_org_id)",

               new NpgsqlParameter("p_org_id", NpgsqlDbType.Uuid) { Value = request.OrganizationId }
           );

        await _mediator.Publish(new OrganiztionHardDeletedEvent(request.OrganizationId), cancellationToken);

        return Result.Success(new EntityDeletedResponse(request.OrganizationId));
    }
    #endregion
}
