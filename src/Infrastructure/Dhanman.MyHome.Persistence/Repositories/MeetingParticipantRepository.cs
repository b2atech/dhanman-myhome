using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.MeetingParticipants;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal class MeetingParticipantRepository : IMeetingParticipantRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MeetingParticipantRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public void AddParticipantToMeeting(MeetingParticipant meetingParticipant)
    {
        _dbContext.Add<MeetingParticipant>(meetingParticipant);

    }

    public async Task<MeetingParticipant?> GetByOccurrenceIdAndUserIdAsync(int occurrenceId, Guid userId)
    {
        return await _dbContext.SetInt<MeetingParticipant>()
                .FirstOrDefaultAsync(mp => mp.OccurrenceId == occurrenceId && mp.UserId == userId);

    }

    public async Task<int> GetLastInsertedIdAsync()
    {
        // Fetch the last inserted ID based on the existing entries
        var lastParticipant = await _dbContext.SetInt<MeetingParticipant>()
            .IgnoreQueryFilters()
            .OrderByDescending(mp => mp.Id)
            .FirstOrDefaultAsync();

        // Return the last ID or 0 if no participant exist
        return lastParticipant?.Id ?? 0;
    }
   
    public async Task<List<Guid>> GetUserIdsByOccurrenceIdAsync(int occurrenceId)
    {
        return await _dbContext.SetInt<MeetingParticipant>()
               .Where(mp => mp.OccurrenceId == occurrenceId && !mp.IsDeleted)
               .Select(mp => mp.UserId)
               .ToListAsync();
    }

    public void RemoveParticipantFromMeeting(MeetingParticipant meetingParticipant)
    {
        _dbContext.SetInt<MeetingParticipant>().Remove(meetingParticipant);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}