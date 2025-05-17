using System.Xml.Linq;

namespace Dhanman.MyHome.Application.Contracts.CommitteeMembers;

public sealed class CreateCommitteeMemberRequest
{
    public Guid UserId { get; set; }
    public Guid ApartmentId { get; set; }
    public DateTime EffectiveStartDate { get; set; }
    public DateTime EffectiveEndDate { get; set; }
    public int RoleId { get; set; }
    public int PortfolioId { get; set; }
    public Guid CreatedBy { get; set; }

    #region Constructors
    public CreateCommitteeMemberRequest()
    {

    }
    #endregion
}
