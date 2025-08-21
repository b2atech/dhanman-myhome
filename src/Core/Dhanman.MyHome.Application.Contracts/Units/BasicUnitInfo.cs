namespace Dhanman.MyHome.Application.Contracts.Units;

public class BasicUnitInfo
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public string UserName { get; set; }
    public Guid ApartmentId { get; set; }

    public BasicUnitInfo(int id, int unitId, string unitName, string userName, Guid apartmentId)
    {
        Id = id;
        UnitId = unitId;
        UnitName = unitName;
        UserName = userName;
        ApartmentId = apartmentId;
    }
}
