using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.CommitteeMembers;

public class CommitteeMember : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    public Guid UserId { get; set; }
    public Guid ApartmentId { get; set; }
    public DateTime EffectiveStartDate { get; set; }
    public DateTime EffectiveEndDate { get; set; }
    public int RoleId { get; set; }
    public int PortfolioId { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; protected set; }

    public CommitteeMember() { }

    public CommitteeMember(Guid userId,Guid apartmentId, DateTime effectiveStartDate, DateTime effectiveEndDate, int roleId, int portfolioId,Guid createdBy)
    {
        UserId = userId;
        ApartmentId = apartmentId;
        EffectiveStartDate = effectiveStartDate;
        EffectiveEndDate = effectiveEndDate;
        RoleId = roleId;
        PortfolioId = portfolioId;
        CreatedBy = createdBy;
    }
}
