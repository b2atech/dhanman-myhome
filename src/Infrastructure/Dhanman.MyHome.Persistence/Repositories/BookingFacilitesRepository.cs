using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.FacilityBookings;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class BookingFacilitesRepository: IBookingFacilitesRepository
{

    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public BookingFacilitesRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods

    public Task<FacilityBooking?> GetBydIdIntAsync(int id) => _dbContext.GetBydIdIntAsync<FacilityBooking>(id);

    /*public void Insert(BookingFacilitie bookingFacilitie) => _dbContext.InsertInt(bookingFacilitie)*/

    #endregion
}
