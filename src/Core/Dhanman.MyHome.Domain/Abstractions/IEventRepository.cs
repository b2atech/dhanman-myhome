
using Dhanman.MyHome.Domain.Entities.Events;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IEventRepository
{
    #region Methodes

    Task<Event?> GetBydIdAsync(Guid id);

    void Insert(Event events);

    #endregion
}
