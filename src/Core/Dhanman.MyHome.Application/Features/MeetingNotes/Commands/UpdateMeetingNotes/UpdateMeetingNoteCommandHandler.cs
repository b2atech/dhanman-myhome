using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Dhanman.MyHome.Application.Features.MeetingNotes.Commands.UpdateMeetingNotes;

public sealed class UpdateMeetingNoteCommandHandler : ICommandHandler<UpdateMeetingNoteCommand, Result<EntityUpdatedResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserContextService _userContextService;

    public UpdateMeetingNoteCommandHandler(IApplicationDbContext dbContext,
        IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _userContextService = userContextService;
    }

    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateMeetingNoteCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _userContextService.GetCurrentUserId();

        await using var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "CALL public.upsert_meeting_note(@p_event_uuid, @p_occ_date, @p_user_uuid, @p_note_text)";
        command.CommandType = System.Data.CommandType.Text;

        command.Parameters.Add(new NpgsqlParameter("p_event_uuid", NpgsqlDbType.Uuid) { Value = request.EventId });
        command.Parameters.Add(new NpgsqlParameter("p_occ_date", NpgsqlDbType.Date) { Value = request.OccurrenceDate });
        command.Parameters.Add(new NpgsqlParameter("p_user_uuid", NpgsqlDbType.Uuid) { Value = currentUserId });
        command.Parameters.Add(new NpgsqlParameter("p_note_text", NpgsqlDbType.Text) { Value = request.NoteText });

        await command.ExecuteNonQueryAsync(cancellationToken);

        return Result.Success(new EntityUpdatedResponse(new List<int>()));
    }
}
