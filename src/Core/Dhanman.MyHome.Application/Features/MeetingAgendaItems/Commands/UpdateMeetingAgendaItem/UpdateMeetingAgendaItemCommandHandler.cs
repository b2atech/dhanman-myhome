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

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Commands.UpdateMeetingAgendaItem;

public sealed class UpdateMeetingAgendaItemCommandHandler : ICommandHandler<UpdateMeetingAgendaItemCommand, Result<EntityUpdatedResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserContextService _userContextService;

    public UpdateMeetingAgendaItemCommandHandler(IApplicationDbContext dbContext,
        IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _userContextService = userContextService;
    }

    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateMeetingAgendaItemCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _userContextService.GetCurrentUserId();

        // Step 1: Convert agenda items to JSON (PostgreSQL expects 'item_text' and 'order_no' keys)
        var jsonAgendaItems = request.AgendaItems.Select(item => new
        {
            id = item.Id,
            item_text = item.ItemText,
            order_no = item.OrderNo
        });

        var jsonString = JsonSerializer.Serialize(jsonAgendaItems);

        await using var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "CALL public.upsert_meeting_agenda_items(@p_event_uuid, @p_occ_date, @p_user_uuid, @p_agenda_items)";
        command.CommandType = System.Data.CommandType.Text;

        command.Parameters.Add(new NpgsqlParameter("p_event_uuid", NpgsqlDbType.Uuid) { Value = request.EventId });
        command.Parameters.Add(new NpgsqlParameter("p_occ_date", NpgsqlDbType.Date) { Value = request.OccurrenceDate });
        command.Parameters.Add(new NpgsqlParameter("p_user_uuid", NpgsqlDbType.Uuid) { Value = currentUserId });
        command.Parameters.Add(new NpgsqlParameter("p_agenda_items", NpgsqlDbType.Jsonb) { Value = jsonString });

        await command.ExecuteNonQueryAsync(cancellationToken);

        return Result.Success(new EntityUpdatedResponse(new List<int>()));
    }
}
