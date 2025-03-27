namespace Dhanman.MyHome.Application.Contracts.VisitorApprovals;

public sealed class VisitorApprovalsInfoByIdResponse
{
    #region Properties
    public int Visitor_Id { get; }
    public string First_Name { get; }
    public string Last_Name { get; }
    public string Contact_Number { get; }
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
        Visitor_Id = visitor_Id;
        First_Name = first_Name;
        Last_Name = last_Name;
        Contact_Number = contact_Number;
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
