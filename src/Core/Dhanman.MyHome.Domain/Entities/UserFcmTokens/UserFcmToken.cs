using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.UserFcmTokens
{
    public class UserFcmToken : EntityInt
    {
        #region Properties
        public Guid  UserId { get; set; }
        public string DeviceId { get; set; }
        public string FCMToken { get; set; }
        #endregion

        #region Constructors
        public UserFcmToken() { }

        public UserFcmToken(int id, Guid userId, string deviceId, string fCMToken)
        {
            Id = id;
            UserId = userId;
            DeviceId = deviceId;
            FCMToken = fCMToken;
        }
        #endregion
    }
}
