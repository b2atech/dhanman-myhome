namespace Dhanman.MyHome.Application.Contracts.BookingFacilites;

public class BookingFacilitesResponse
{

    public int Id { get; }
    public string Name { get; }
    public int BuildingId { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }

    public BookingFacilitesResponse(int id, string name, int buildingId)
    {
        Id = id;
        Name = name;
        BuildingId = buildingId;
    }
}
