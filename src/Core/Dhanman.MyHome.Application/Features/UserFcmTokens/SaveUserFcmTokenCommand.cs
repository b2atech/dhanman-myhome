using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Application.Features.UserFcmTokens;

public class SaveUserFcmTokenCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public Guid UserId { get; set; }
    public string DeviceId { get; set; }
    public string FCMToken { get; set; }
    public string Platform { get; set; }
    #endregion

    #region Constructors
    public SaveUserFcmTokenCommand(Guid userId, string deviceId, string fcmToken,string platform)
    {
        UserId = userId;
        DeviceId = deviceId;
        FCMToken = fcmToken;
        Platform = platform;
    }
    #endregion
}