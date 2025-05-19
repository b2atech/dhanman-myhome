using Dhanman.MyHome.Domain.Entities.EventOccurrences;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IEventOccurrenceRepository
{
    Task<EventOccurrence?> GetByIntIdAsync(int id);
    void Insert(EventOccurrence eventOccurrence);
    void Update(EventOccurrence eventOccurrence);
    void Delete(EventOccurrence eventOccurrence);
}
    