using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;

public sealed class GetVisitorByContactQuery : IQuery<Result<VisitorByContactListResponse>>
{
    public Guid ApartmentId { get; }
    public string ContactNumber { get; }

    public GetVisitorByContactQuery(Guid apartmentId, string contactNumber)
    {
        ApartmentId = apartmentId;
        ContactNumber = contactNumber;
    }
}