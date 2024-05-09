using Dhanman.MyHome.Domain.Entities.BookingFacilites;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IBookingFacilitesRepository
{
    #region Methodes
    Task<BookingFacilitie?> GetBydIdIntAsync(int id);
    #endregion
}
