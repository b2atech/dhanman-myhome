using Dhanman.Shared.Contracts.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Application.Features.UserFcmTokens.Events
{
    public class UserFcmTokenCreatedEvent : IEvent
    {
        #region Properties
        public int Id { get; }
        #endregion

        #region Constructors
        public UserFcmTokenCreatedEvent(int id) => Id = id;
        #endregion

    }
}
