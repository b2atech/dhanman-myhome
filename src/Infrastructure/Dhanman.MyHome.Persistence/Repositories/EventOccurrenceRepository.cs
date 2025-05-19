using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.EventOccurrences;

namespace Dhanman.MyHome.Persistence.Repositories;

public sealed class EventOccurrenceRepository : IEventOccurrenceRepository
{
    private readonly IApplicationDbContext _dbContext;

    public EventOccurrenceRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;


    public Task<EventOccurrence> GetByIntIdAsync(int id) =>_dbContext.GetBydIdIntAsync<EventOccurrence>(id);

    public void Insert(EventOccurrence eventOccurrence) => _dbContext.InsertInt(eventOccurrence);

    public void Update(EventOccurrence eventOccurrence) => _dbContext.UpdateInt(eventOccurrence);

    public void Delete(EventOccurrence eventOccurrence) => _dbContext.RemoveInt(eventOccurrence);

}