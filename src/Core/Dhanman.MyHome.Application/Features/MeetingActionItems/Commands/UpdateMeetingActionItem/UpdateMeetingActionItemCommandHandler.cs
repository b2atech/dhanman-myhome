using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Text.Json;

namespace Dhanman.MyHome.Application.Features.MeetingActionItems.Commands.UpdateMeetingActionItem;

public sealed class UpdateMeetingActionItemCommandHandler : ICommandHandler<UpdateMeetingActionItemCommand, Result<EntityUpdatedResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserContextService _userContextService;

    public UpdateMeetingActionItemCommandHandler(IApplicationDbContext dbContext,
        IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _userContextService = userContextService;
    }

    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateMeetingActionItemCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _userContextService.CurrentUserId;

        var jsonActionItems = request.ActionItems.Select(item => new
        {
            id = item.Id,
            action_description = item.ActionDescription,
            assigned_to_user_id = item.AssignedToUserId,
        });

        var jsonString = JsonSerializer.Serialize(jsonActionItems);

        await using var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "CALL public.upsert_meeting_action_items(@p_event_uuid, @p_occ_date, @p_user_uuid, @p_action_items)";
        command.CommandType = System.Data.CommandType.Text;

        command.Parameters.Add(new NpgsqlParameter("p_event_uuid", NpgsqlDbType.Uuid) { Value = request.EventId });
        command.Parameters.Add(new NpgsqlParameter("p_occ_date", NpgsqlDbType.Date) { Value = request.OccurrenceDate });
        command.Parameters.Add(new NpgsqlParameter("p_user_uuid", NpgsqlDbType.Uuid) { Value = currentUserId });
        command.Parameters.Add(new NpgsqlParameter("p_action_items", NpgsqlDbType.Jsonb) { Value = jsonString });

        await command.ExecuteNonQueryAsync(cancellationToken);

        return Result.Success(new EntityUpdatedResponse(new List<int>()));
    }
}
