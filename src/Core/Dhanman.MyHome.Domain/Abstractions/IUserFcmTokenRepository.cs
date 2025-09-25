using Dhanman.MyHome.Domain.Entities.UserFcmTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IUserFcmTokenRepository
{
    Task<UserFcmToken?> GetByUserIdAndDeviceIdAsync(Guid userId, string deviceId);
    void Insert(UserFcmToken token);
    void Update(UserFcmToken token);

    /// <summary>
    /// Returns all FCM tokens and devices for residents of a given unit.
    /// </summary>
    Task<IReadOnlyList<UnitResidentFcmTokenResponse>> GetUnitResidentFcmTokensAsync(
        int unitId,
        CancellationToken cancellationToken = default);
}