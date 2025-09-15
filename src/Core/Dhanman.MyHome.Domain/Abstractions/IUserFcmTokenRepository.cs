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
}