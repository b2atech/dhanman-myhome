using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.MeetingParticipants;
using MediatR;

namespace Dhanman.MyHome.Application.Features.MeetingParticipants.Commands.UpdateMeetingParticipant;
 
public class UpdateMeetingParticipantCommandHandler : ICommandHandler<UpdateMeetingParticipantCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IMeetingParticipantRepository _meetingParticipantRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public UpdateMeetingParticipantCommandHandler(IMeetingParticipantRepository meetingParticipantRepository, IMediator mediator)
    {
        _meetingParticipantRepository = meetingParticipantRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(UpdateMeetingParticipantCommand request, CancellationToken cancellationToken)
    {
        // Fetch the current list of product IDs assigned to the vendor
        var currentUsers = await _meetingParticipantRepository.GetUserIdsByOccurrenceIdAsync(request.OccurrenceId);

        // Find products that need to be added (those in the request but not in the current list)
        var toAdd = request.UserIds.Except(currentUsers).ToList();

        // Find products that need to be removed (those in the current list but not in the request)
        var toRemove = currentUsers.Except(request.UserIds).ToList();

        int lastId = await _meetingParticipantRepository.GetLastInsertedIdAsync();
        int newId = lastId + 1;

        // Add new participat to the meeting
        foreach (var userId in toAdd)
        {
            var meetingParticipant = await _meetingParticipantRepository.GetByOccurrenceIdAndUserIdAsync(request.OccurrenceId, userId);
            if (meetingParticipant != null)
            {
                meetingParticipant.IsDeleted = false;
            }
            else
            {
                meetingParticipant = new MeetingParticipant(newId, request.OccurrenceId, userId, request.Role);
                newId++;
                _meetingParticipantRepository.AddParticipantToMeeting(meetingParticipant);
            }
        }

        // Remove participant that are no longer assigned to the meeting
        foreach (var userId in toRemove)
        {
            var meetingParticipant = await _meetingParticipantRepository.GetByOccurrenceIdAndUserIdAsync(request.OccurrenceId, userId);
                                                                  
            if (meetingParticipant != null)
            {
                meetingParticipant.IsDeleted = true;
            }
        }

        // Save changes to the repository
        await _meetingParticipantRepository.SaveChangesAsync();

        return Result.Success(new EntityCreatedResponse(request.OccurrenceId));

    }
    #endregion

}