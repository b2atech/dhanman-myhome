using Dhanman.MyHome.Domain.Entities.Gates;
using Dhanman.MyHome.Domain.Entities.MeetingAgendaItems;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IMeetingAgendaItemRepository
{
    Task<MeetingAgendaItem> GetByIntIdAsync(int id);
    void InsertInt(MeetingAgendaItem meetingAgendaItem);
    void UpdateInt(MeetingAgendaItem meetingAgendaItem);
    void DeleteInt(MeetingAgendaItem meetingAgendaItem);
}
