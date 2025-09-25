using Dapper;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.UserFcmTokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Persistence.Repositories;

public class UserFcmTokenRepository : IUserFcmTokenRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public UserFcmTokenRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<UserFcmToken?> GetByUserIdAndDeviceIdAsync(Guid userId, string deviceId) =>
        await _dbContext.SetInt<UserFcmToken>()
            .Where(x => x.UserId == userId && x.DeviceId == deviceId)
            .FirstOrDefaultAsync();

    public void Insert(UserFcmToken userFcmToken) => _dbContext.InsertInt(userFcmToken);

    public void Update(UserFcmToken userFcmToken) => _dbContext.UpdateInt(userFcmToken);

    public async Task<IReadOnlyList<UnitResidentFcmTokenResponse>> GetUnitResidentFcmTokensAsync(
        int unitId,
        CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT * FROM public.unit_resident_fcm_tokens(@p_unit_id)";

        using var conn = _dbContext.Database.GetDbConnection();
        if (conn.State == ConnectionState.Closed)
            await conn.OpenAsync(cancellationToken);

        var results = await conn.QueryAsync<UnitResidentFcmTokenResponse>(
            new CommandDefinition(
                sql,
                new { p_unit_id = unitId },
                cancellationToken: cancellationToken
            ));

        return results.ToList();
    }
    #endregion
}