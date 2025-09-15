using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Application.Contracts.Notifications
{
    public class CreateUserFcmTokenRequest
    {
        #region Properties
        public Guid UserId { get; set; }
        public string DeviceId { get; set; }
        public string FCMToken { get; set; }
        #endregion

        #region Constructors
        public CreateUserFcmTokenRequest()
        {
        }

        public CreateUserFcmTokenRequest(Guid userId, string deviceId, string fcmToken)
        {
            UserId = userId;
            DeviceId = deviceId;
            FCMToken = fcmToken;
        }
        #endregion
    }
}
