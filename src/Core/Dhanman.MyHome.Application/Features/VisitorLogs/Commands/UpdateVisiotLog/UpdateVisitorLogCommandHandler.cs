using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.VisitorLogs.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.UpdateVisiotLog;

public class UpdateVisitorLogCommandHandler : ICommandHandler<UpdateVisitorLogCommand, Result<EntityUpdatedResponse>>
{
     #region Properties

        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public UpdateVisitorLogCommandHandler(IMediator mediator, IApplicationDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
  public async Task<Result<EntityUpdatedResponse>> Handle(UpdateVisitorLogCommand request, CancellationToken cancellationToken)
{
    var parameter = new NpgsqlParameter
    {
        ParameterName = "p_visitor_log_ids",
        NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer,
        Value = request.VisitorLogIds.ToArray()
    };

    await _dbContext.Database.ExecuteSqlRawAsync(
        "SELECT * FROM public.check_out(@p_visitor_log_ids)",
        parameter
    );

    // Publish event for each updated ID
    foreach (var id in request.VisitorLogIds)
    {
        await _mediator.Publish(new VisitorLogUpdatedEvent(id), cancellationToken);
    }

    return Result.Success(new EntityUpdatedResponse(request.VisitorLogIds.First()));
}

    #endregion
}
