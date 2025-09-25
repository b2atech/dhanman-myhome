using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Domain.Entities.UserFcmTokens;
public class UnitResidentFcmTokenDto
{
    public int ResidentId { get; set; }
    public Guid UserId { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public string FcmToken { get; set; } = string.Empty;
}
public class UnitResidentFcmTokenResponse : UnitResidentFcmTokenDto { }
