using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Commands.CheckinServiceProvider;

public class CheckinServiceProviderCommandHandler : ICommandHandler<CheckinServiceProviderCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserContextService _userContextService;
    #endregion

    #region Constructor
    public CheckinServiceProviderCommandHandler(IApplicationDbContext dbContext, IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _userContextService = userContextService;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CheckinServiceProviderCommand request, CancellationToken cancellationToken)
    {
        var result = await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var sql = "SELECT * FROM public.checkin_service_provider(@p_apartment_id, @p_pin, @p_created_by,@p_entry_time)";

                var parameters = new[] {
                new NpgsqlParameter("p_apartment_id", request.ApartmentId),
                new NpgsqlParameter("p_pin", request.Pin),
                new NpgsqlParameter("p_created_by", _userContextService.CurrentUserId),
                new NpgsqlParameter("p_entry_time", DateTime.UtcNow)
            };

                var logId = await _dbContext.Database
                .ExecuteSqlRawAsync(sql, parameters, cancellationToken);

                if (logId == null || logId == 0)
                {
                    return Result.Failure<EntityCreatedResponse>(Errors.ServiceProvider.NotFound);
                }

                return new EntityCreatedResponse(logId);
            });

        return result;
    }
    #endregion
}
