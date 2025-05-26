using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.MeetingDetails;
using Dhanman.MyHome.Application.Contracts.MeetingParticipants;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Text.Json;

namespace Dhanman.MyHome.Application.Features.MeetingDetails.Queries;

public class GetMeetingDetailsQueryHandler : IQueryHandler<GetMeetingDetailsQuery, Result<MeetingDetailsDto>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetMeetingDetailsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<MeetingDetailsDto>> Handle(GetMeetingDetailsQuery request, CancellationToken cancellationToken)
{
        await using var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT public.get_meeting_data(@EventId, @OccurrenceDate)";
        command.CommandType = System.Data.CommandType.Text;

        command.Parameters.Add(new NpgsqlParameter("EventId", NpgsqlDbType.Uuid) { Value = request.EventId });
        command.Parameters.Add(new NpgsqlParameter("OccurrenceDate", NpgsqlDbType.Date) { Value = request.OccurrenceDate });

        var jsonResult = await command.ExecuteScalarAsync(cancellationToken) as string;

        if (string.IsNullOrEmpty(jsonResult))
            return Result.Failure<MeetingDetailsDto>(Errors.General.EntityNotFound);

        var root = JsonDocument.Parse(jsonResult).RootElement;

        var dto = new MeetingDetailsDto
        {
            AgendaItems = root.GetProperty("agenda").EnumerateArray()
                .Select(x => new MeetingAgendaDto
                {
                    Id = x.GetProperty("id").GetInt32(),
                    ItemText = x.GetProperty("item_text").GetString() ?? "",
                    OrderNo = x.GetProperty("order_no").GetInt32()
                }).ToList(),

            ParticipantItems = root.GetProperty("participants").EnumerateArray()
                .Select(x => new MeetingParticipantDto
                {
                    Id = x.GetProperty("id").GetInt32(),
                    UserId = x.GetProperty("user_id").GetGuid(),
                    UserName = x.GetProperty("user_name").GetString() ?? "",
                }).ToList(),

            ActionItems = root.GetProperty("actions").EnumerateArray()
                .Select(x => new MeetingActionItemDto
                {
                    Id = x.GetProperty("id").GetInt32(),
                    ActionDescription = x.GetProperty("action_description").GetString() ?? "",
                    AssignedToUserId = x.GetProperty("assigned_to_user_id").GetGuid(),
                    AssignedToUserName = x.GetProperty("assigned_to_user_name").GetString() ?? "",
                }).ToList(),

            NoteText = root.TryGetProperty("notes", out var notes) && notes.ValueKind == JsonValueKind.Array
                ? notes.EnumerateArray()
                    .Select(n => n.TryGetProperty("note_text", out var t) ? t.GetString() : "")
                    .FirstOrDefault()
                : null
        };

        return Result.Success(dto);
    }
    #endregion
}
