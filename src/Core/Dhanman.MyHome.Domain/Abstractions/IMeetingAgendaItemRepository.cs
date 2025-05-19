using Dhanman.MyHome.Domain.Entities.Gates;
using Dhanman.MyHome.Domain.Entities.MeetingAgendaItems;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IMeetingAgendaItemRepository
{
    Task<MeetingAgendaItem> GetByIntIdAsync(int id);
    void Insert(MeetingAgendaItem meetingAgendaItem);
    void Update(MeetingAgendaItem meetingAgendaItem);
    void Delete(MeetingAgendaItem meetingAgendaItem);
}
