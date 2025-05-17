using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.CommitteeMembers;

namespace Dhanman.MyHome.Application.Features.CommitteeMembers.Queries;

public sealed class GetCommitteeAllMembersByApartmentQuery : IQuery<Result<CommitteeAllMemberListResponse>>
{
    public Guid ApartmentId { get; }

    public GetCommitteeAllMembersByApartmentQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
}