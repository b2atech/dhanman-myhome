using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.UserFcmTokens
{
    public class UserFcmTokenDto : EntityInt
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FcmToken { get; set; }
    }

}
