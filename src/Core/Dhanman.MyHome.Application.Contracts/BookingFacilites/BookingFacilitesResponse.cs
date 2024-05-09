namespace Dhanman.MyHome.Application.Contracts.BookingFacilites;

public class BookingFacilitesResponse
{

    public int Id { get; }
    public string Name { get; }
    public int BuildingId { get; }

    public BookingFacilitesResponse(int id, string name, int buildingId)
    {
        Id = id;
        Name = name;
        BuildingId = buildingId;
    }
}
