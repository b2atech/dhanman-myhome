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
    public string eIntercom { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public int NumberOfMembers { get; set; }
    public string Society { get; set; }
    public Guid CreatedBy { get; set; }

    #endregion

    #region Constructor
    public CreateUnitRequest(CreateUnitRequestParams parameters)
    {
        Status = parameters.Status;
        Flat = parameters.Flat;
        FlatType = parameters.FlatType;
        Occupant = parameters.Occupant;
        Occupancy = parameters.Occupancy;
        PrimaryEIntercom = parameters.PrimaryEIntercom;
        NumberOfMembers = parameters.NumberOfMembers;
        Society = parameters.Society;
        CreatedBy = parameters.CreatedBy;
    }
    #endregion
}
public class CreateUnitRequestParams
{
    public string Status { get; set; }
    public string Flat { get; set; }
    public string FlatType { get; set; }
    public string Occupant { get; set; }
    public string Occupancy { get; set; }
    public string PrimaryEIntercom { get; set; }
    public int NumberOfMembers { get; set; }
    public string Society { get; set; }
    public Guid CreatedBy { get; set; }
}
