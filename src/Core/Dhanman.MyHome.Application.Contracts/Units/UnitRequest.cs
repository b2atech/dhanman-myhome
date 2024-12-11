namespace Dhanman.MyHome.Application.Contracts.Units;

public class UnitRequest
{
    #region Properties
    public string Society { get; set; }
    public string Flat { get; set; }
    public string FlatType { get; set; }
    public string OccupantType { get; set; }
    public string Status { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public int NumberOfMembers { get; set; }
    public decimal SqFt { get; set; }
    public decimal BHK { get; set; }

    #endregion

    #region Constructor
    public UnitRequest(string society, string flat, string flatType, string occupantType, string status,
        string latitude, string longitude, int numberOfMembers, decimal sqFt, decimal bHK)
    {
        Society = society;
        Flat = flat;
        FlatType = flatType;
        OccupantType = occupantType;
        Status = status;
        Latitude = latitude;
        Longitude = longitude;
        NumberOfMembers = numberOfMembers;
        SqFt = sqFt;
        BHK = bHK;
    }
    #endregion
}
