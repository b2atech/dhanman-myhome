using Dhanman.MyHome.Domain.Entities.Gates;
using Dhanman.MyHome.Domain.Entities.MeetingAgendaItems;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IMeetingAgendaItemRepository
{
    Task<MeetingAgendaItem> GetByIntIdAsync(int id);
    void InsertInt(MeetingAgendaItem entity);
    void UpdateInt(MeetingAgendaItem entity);
    void DeleteInt(MeetingAgendaItem entity);
}
