using Dhanman.MyHome.Domain.Entities.FacilityBookings;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IBookingFacilitesRepository
{
    #region Methodes
    Task<FacilityBooking> GetBydIdIntAsync(int id);
    #endregion
}
