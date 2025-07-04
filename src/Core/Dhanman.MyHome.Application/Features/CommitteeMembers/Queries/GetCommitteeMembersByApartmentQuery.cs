using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.CommitteeMembers;

namespace Dhanman.MyHome.Application.Features.CommitteeMembers.Queries;

public class GetCommitteeMembersByApartmentQuery : IQuery<Result<List<CommitteeMemberResponse>>>
{
    public Guid ApartmentId { get; }

    public GetCommitteeMembersByApartmentQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }

}
