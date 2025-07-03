using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Text.Json;

namespace Dhanman.MyHome.Application.Features.MeetingParticipants.Commands.UpdateMeetingParticipant;
 
public class UpdateMeetingParticipantCommandHandler : ICommandHandler<UpdateMeetingParticipantCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserContextService _userContextService;
    #endregion

    #region Constructors
    public UpdateMeetingParticipantCommandHandler(IApplicationDbContext dbContext,
        IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _userContextService = userContextService;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateMeetingParticipantCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _userContextService.CurrentUserId;

        // Step 1: Convert user list to JSONB format expected by the procedure
        var jsonParticipants = request.UserIds.Select(userId => new
        {
            id = "-1",
            user_id = userId
        });
        var jsonString = JsonSerializer.Serialize(jsonParticipants);

        await using var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "CALL public.upsert_meeting_participants(@p_event_uuid, @p_occ_date, @p_user_uuid, @p_participant_user_ids)";
        command.CommandType = System.Data.CommandType.Text;

        command.Parameters.Add(new NpgsqlParameter("p_event_uuid", NpgsqlDbType.Uuid) { Value = request.EventId });
        command.Parameters.Add(new NpgsqlParameter("p_occ_date", NpgsqlDbType.Date) { Value = request.OccurrenceDate });
        command.Parameters.Add(new NpgsqlParameter("p_user_uuid", NpgsqlDbType.Uuid) { Value = currentUserId });
        command.Parameters.Add(new NpgsqlParameter("p_participant_user_ids", NpgsqlDbType.Jsonb) { Value = jsonString });

        await command.ExecuteNonQueryAsync(cancellationToken);

        return Result.Success(new EntityUpdatedResponse(new List<int>()));
    }
    #endregion

}