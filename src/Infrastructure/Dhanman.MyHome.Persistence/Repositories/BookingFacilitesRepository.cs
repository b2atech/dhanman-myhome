using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.BookingFacilites;

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

    public Task<BookingFacilitie?> GetBydIdIntAsync(int id) => _dbContext.GetBydIdIntAsync<BookingFacilitie>(id);

    /*public void Insert(BookingFacilitie bookingFacilitie) => _dbContext.InsertInt(bookingFacilitie)*/

    #endregion
}
