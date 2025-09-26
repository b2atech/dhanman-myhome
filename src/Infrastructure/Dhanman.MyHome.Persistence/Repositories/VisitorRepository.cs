using Dapper;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Enums;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Dhanman.MyHome.Persistence.Repositories;

public class VisitorRepository : IVisitorRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public VisitorRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public Task<Visitor?> GetByIntIdAsync(int id) => _dbContext.GetBydIdIntAsync<Visitor>(id);

    //public async Task<int> GetLastVisitorIdAsync()
    //{
    //    return await _dbContext.SetInt<Visitor>()
    //        .IgnoreQueryFilters()
    //        //.OrderByDescending(g => g.Id)
    //        //.Select(g => g.Id)
    //        //.FirstOrDefaultAsync();
    //        .MaxAsync(b => b.Id);
    //}

    public void Insert(Visitor visitor) => _dbContext.InsertInt(visitor);

    public void Update(Visitor visitor) => _dbContext.UpdateInt(visitor);

    public void Delete(Visitor visitor) => _dbContext.RemoveInt(visitor);


    public async Task<VisitorApprovalActionResponse?> TakeVisitorActionAsync(
        int visitorLogId,
        int unitId,
        Guid residentUserId,
        VisitorStatus action,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"SELECT * FROM public.take_visitor_action(@p_visitor_log_id, @p_unit_id, @p_resident_user, @p_action_status)";

        using var conn = _dbContext.Database.GetDbConnection();

        if (conn.State == ConnectionState.Closed)
            await conn.OpenAsync(cancellationToken);

        return await conn.QueryFirstOrDefaultAsync<VisitorApprovalActionResponse>(
            new CommandDefinition(
                sql,
                new
                {
                    p_visitor_log_id = visitorLogId,
                    p_unit_id = unitId,
                    p_resident_user = residentUserId,
                    p_action_status = (int)action
                },
                cancellationToken: cancellationToken));
    }

    #endregion


}
