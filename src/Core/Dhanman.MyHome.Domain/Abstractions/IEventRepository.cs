
using Dhanman.MyHome.Domain.Entities.Events;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IEventRepository
{
    #region Methodes

    Task<Event?> GetBydIdIntAsync(int id);

    void Insert(Event events);

    #endregion
}
