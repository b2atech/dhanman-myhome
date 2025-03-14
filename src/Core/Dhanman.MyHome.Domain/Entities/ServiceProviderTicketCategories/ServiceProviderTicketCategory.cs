using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ServiceProviderTicketCategories;

public class ServiceProviderTicketCategory: EntityInt, ISoftDeletableEntity
{

    #region Properties
    public int ServiceProviderId { get; set; }
    public int TicketCategoryId { get; set; }

    #endregion



    #region Deleteable Properties
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    #endregion

    #region Constructor
    public ServiceProviderTicketCategory(int id, int serviceProviderId, int ticketCategoryId)
    {
        Id = id;
        ServiceProviderId = serviceProviderId;
        TicketCategoryId = ticketCategoryId;
    }
    #endregion
}
