using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Events;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class EventRepository: IEventRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public EventRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;



    #endregion

    #region Methods
    public Task<Event?> GetBydIdAsync(Guid id) => _dbContext.GetBydIdAsync<Event>(id);
    public void Insert(Event events) => _dbContext.Insert(events);
    public void Update(Event events) => _dbContext.Update(events);
    public void Delete(Event events) => _dbContext.Remove(events);
    #endregion
}
