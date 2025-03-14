namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class GetUnitIdbyUserIdResponse
{

    public Guid UserId { get; set; }
    public Guid ApartmentId { get; set; }
    public List<int> UnitIds { get; set; }

    public GetUnitIdbyUserIdResponse(Guid userId, Guid apartmentId, List<int> unitIds)
    {
        UserId = userId;
        ApartmentId = apartmentId;
        UnitIds = unitIds;
    }
}
