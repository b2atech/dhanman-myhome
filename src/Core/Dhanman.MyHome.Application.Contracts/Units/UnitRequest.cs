namespace Dhanman.MyHome.Application.Contracts.Units;

public class UnitRequest
{
    #region Properties
    public string Status { get; set; }
    public string Flat {  get; set; }
    public string FlatType { get; set; }
    public string Occupant { get; set; }
    public string Occupancy { get; set; }
    public int PhoneExtention { get; set; }
    public int EIntercom { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public int NumberOfMembers { get; set; }
    public string Society { get; set; }

    #endregion

    #region Constructor
    public UnitRequest(string status, string flat, string flatType, string occupant, string occupancy, int phoneExtention, int eIntercom, string latitude, string longitude, int numberOfMembers, string society)
    {
        Status = status;
        Flat = flat;
        FlatType = flatType;
        Occupant = occupant;
        Occupancy = occupancy;
        PhoneExtention = phoneExtention;
        EIntercom = eIntercom;
        Latitude = latitude;
        Longitude = longitude;
        NumberOfMembers = numberOfMembers;
        Society = society;
    }
    #endregion
}
