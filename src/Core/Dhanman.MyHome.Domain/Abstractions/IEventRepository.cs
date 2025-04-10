
using Dhanman.MyHome.Domain.Entities.Events;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IEventRepository
{
    #region Methodes

    Task<Event?> GetBydIdAsync(Guid id);
    void Insert(Event events);
    void Update(Event events);
    void Delete(Event events);
    #endregion
}
