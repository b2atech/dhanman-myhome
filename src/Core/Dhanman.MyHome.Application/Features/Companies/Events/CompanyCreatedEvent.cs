using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Companies.Events;

public sealed class CompanyCreatedEvent : IEvent
{
    public Guid CompanyId { get; }
    public CompanyCreatedEvent(Guid companyId) => CompanyId = companyId;
}
