using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;

public sealed class GetVisitorByContactQuery : IQuery<Result<VisitorByContactListResponse>>
{
    public Guid ApartmentId { get; }
    public string? ContactNumber { get; }
    public string? Email { get; set; }

    public GetVisitorByContactQuery(Guid apartmentId, string contactNumber, string email)
    {
        ApartmentId = apartmentId;
        ContactNumber = contactNumber;
        Email = email;
    }
}