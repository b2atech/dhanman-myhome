using Dhanman.MyHome.Domain.Entities.MeetingParticipants;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IMeetingParticipantRepository
{
    // Fetch the current list of user IDs associated with a Meeting
    Task<List<Guid>> GetUserIdsByOccurrenceIdAsync(int occurrenceId);     //occurrenceId instead of userId
                     
    Task<MeetingParticipant?> GetByOccurrenceIdAndUserIdAsync(int occurrenceId, Guid userId);

    void AddParticipantToMeeting(MeetingParticipant meetingParticipant);

    void RemoveParticipantFromMeeting(MeetingParticipant meetingParticipant);

    Task<int> GetLastInsertedIdAsync();

    Task SaveChangesAsync();

}