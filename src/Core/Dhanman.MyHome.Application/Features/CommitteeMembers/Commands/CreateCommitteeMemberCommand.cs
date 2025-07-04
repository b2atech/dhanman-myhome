using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.CommitteeMembers.Commands;

public sealed class CreateCommitteeMemberCommand : ICommand<Result<EntityCreatedResponse>>
{
    public Guid UserId { get; }
    public Guid ApartmentId { get; }
    public DateTime EffectiveStartDate { get; }
    public DateTime EffectiveEndDate { get; }
    public int RoleId { get; }
    public int PortfolioId { get; }

    public Guid CreatedBy { get; }

    public CreateCommitteeMemberCommand( Guid userId, Guid apartmentId, DateTime effectiveStartDate, DateTime effectiveEndDate, int roleId, int portfolioId, Guid createdBy)
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