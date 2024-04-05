namespace Dhanman.MyHome.Application.Contracts.Units;

public class CreateUnitRequest
{
    #region Properties
    public string Status { get; set; }
    public string Flat {  get; set; }
    public string FlatType { get; set; }
    public string Occupant { get; set; }
    public string Occupancy { get; set; }
    public string PrimaryEIntercom { get; set; }
    public string EIntercom { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public int NumberOfMembers { get; set; }
    public string Society { get; set; }
    public Guid CreatedBy { get; set; }

    #endregion

    #region Constructor
    public CreateUnitRequest(string status, string flat, string flatType, string occupant, string occupancy, string primaryEIntercom, string eIntercom, string latitude, string longitude, int numberOfMembers, string society, Guid createdBy)
    {
        Status = status;
        Flat = flat;
        FlatType = flatType;
        Occupant = occupant;
        Occupancy = occupancy;
        PrimaryEIntercom = primaryEIntercom;
        EIntercom = eIntercom;
        Latitude = latitude;
        Longitude = longitude;
        NumberOfMembers = numberOfMembers;
        Society = society;
        CreatedBy = createdBy;
    }
    #endregion
}
