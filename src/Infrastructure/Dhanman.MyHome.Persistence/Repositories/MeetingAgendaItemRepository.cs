using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Gates;
using Dhanman.MyHome.Domain.Entities.MeetingAgendaItems;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

public class MeetingAgendaItemRepository : IMeetingAgendaItemRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion
    #region Constructor
    public MeetingAgendaItemRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion
    #region Methods

    public Task<MeetingAgendaItem?> GetByIntIdAsync(int id)  => _dbContext.GetBydIdIntAsync<MeetingAgendaItem>(id);

    public void Delete(MeetingAgendaItem meetingAgendaItem) => _dbContext.RemoveInt(meetingAgendaItem);

    public void Insert(MeetingAgendaItem meetingAgendaItem) => _dbContext.InsertInt(meetingAgendaItem);

    public void Update(MeetingAgendaItem meetingAgendaItem) => _dbContext.UpdateInt(meetingAgendaItem);

    #endregion
}
