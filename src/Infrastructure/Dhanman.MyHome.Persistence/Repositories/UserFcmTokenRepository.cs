using B2aTech.CrossCuttingConcern.Persistence;
using Dapper;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.UserFcmTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    private readonly IServiceScopeFactory _serviceScopeFactory;
    #endregion

    #region Constructor
    public UserFcmTokenRepository(IApplicationDbContext dbContext, IServiceScopeFactory serviceScopeFactory)
    {
        _dbContext = dbContext;
        _serviceScopeFactory = serviceScopeFactory;
    }
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
        try
        {
            const string sql = "SELECT * FROM public.unit_resident_fcm_tokens(@p_unit_id)";

            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var conn = dbContext.Database.GetDbConnection(); // <-- Use the scoped dbContext here!
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync(cancellationToken);

            var results = await conn.QueryAsync<UnitResidentFcmTokenResponse>(
                new CommandDefinition(
                    sql,
                    new { p_unit_id = unitId },
                    cancellationToken: cancellationToken
                ));

            foreach (var item in results)
            {
                Console.WriteLine($"ResidentId: {item.ResidentId}, UserId: {item.UserId}, DeviceId: {item.DeviceId}, FcmToken: {item.FcmToken}");
            }

            return results.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching FCM tokens for unit {unitId}: {ex.Message}");
            throw;
        }
    }
    #endregion
}