namespace Dhanman.MyHome.Application.Contracts.VisitorApprovals;

public sealed class VisitorApprovalsInfoByIdResponse
{
    #region Properties
    public int VisitorId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string ContactNumber { get; }
    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }
    public TimeOnly EntryTime { get; }
    public TimeOnly ExitTime { get; }
    public string CreatedByFirstName { get; }
    public string CreatedByLastName { get; }
    public string UnitName { get; }
    public string ApartmentName { get; }
    public string CityName { get; }

    #endregion

    #region Constructors

    public VisitorApprovalsInfoByIdResponse(int visitor_Id, string first_Name, string last_Name, string contact_Number, DateOnly startDate, DateOnly endDate, TimeOnly entryTime, TimeOnly exitTime, string createdByFirstName, string createdByLastName, string unitName, string apartmentName, string cityName)
    {
        VisitorId = visitor_Id;
        FirstName = first_Name;
        LastName = last_Name;
        ContactNumber = contact_Number;
        StartDate = startDate;
        EndDate = endDate;
        EntryTime = entryTime;
        ExitTime = exitTime;
        CreatedByFirstName = createdByFirstName;
        CreatedByLastName = createdByLastName;
        UnitName = unitName;
        ApartmentName = apartmentName;
        CityName = cityName;
    }

    #endregion
}
